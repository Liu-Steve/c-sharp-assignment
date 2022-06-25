//login token
let token = window.name;

// base map

let lmap = L.map("map", {
    minZoom: 1,
    maxZoom: 18,
    center: [30.6, 114.3],
    zoom: 11,
    zoomDelta: 0.5,
    fullscreenControl: false,
    zoomControl: false,
    attributionControl: false
});

this.map = lmap;

//http://map.geoq.cn/ArcGIS/rest/services/ChinaOnlineCommunity/MapServer/tile/{z}/{y}/{x} //arcgis在线地图
//http://webrd0{s}.is.autonavi.com/appmaptile?lang=zh_cn&size=1&scale=1&style=8&x={x}&y={y}&z={z} //gaode
//http://wprd0{s}.is.autonavi.com/appmaptile?lang=zh_cn&size=1&style=7&x={x}&y={y}&z={z}&scl=1&ltype=3
this.baseLayer = L.tileLayer("http://webrd0{s}.is.autonavi.com/appmaptile?lang=zh_cn&size=1&style=8&x={x}&y={y}&z={z}&ltype=5", {
    attribution: '&copy; 高德地图',
    maxZoom: 18,
    minZoom: 1,
    subdomains: "1234"
});

this.roadLayer = L.tileLayer("http://wprd01.is.autonavi.com/appmaptile?lang=zh_cn&x={x}&y={y}&z={z}&size=1&scl=1&style=8&ltype=11", {
    maxZoom: 18,
    minZoom: 15,
})

this.map.addLayer(this.baseLayer);
this.map.addLayer(this.roadLayer);

// add buses
const staIcons = [];

function make_marker_icon(color_marker) {
    let filter = `filter: drop-shadow(${color_marker} 20px 0);
    -webkit-filter: drop-shadow(${color_marker} 20px 0);`
    let svgContent = `<div class="icon"><img style="${filter}" src="../img/dot.svg"/></div>`;
    var staIcon = L.divIcon({
        //iconUrl: '../img/dot.svg',
        iconSize: [10, 10],
        iconAnchor: [5, 5],
        className: "custom-color-icon",
        html: svgContent
    });
    return staIcon;
}
var color = ['#5DAC81', '#00AA90', '#24936E', '#00896C', '#227D51', '#1B813E']

//绘图，传入数据，指定颜色
let show = function(data, color_line) {
    let xs = data.data.busline_list[0].xs.split(',').map(Number);
    let ys = data.data.busline_list[0].ys.split(',').map(Number);
    let pos = [];
    for (let i = 0; i < xs.length; i++) {
        pos.push([ys[i], xs[i]]);
    }
    var polyline = L.polyline(pos, { color: color_line });
    this.map.addLayer(polyline);

    let sta = data.data.busline_list[0].stations;
    for (let i = 0; i < sta.length; i++) {
        let p = sta[i].xy_coords.split(';').map(Number);
        let staIcon = make_marker_icon(color_line);
        var marker = L.marker([p[1], p[0]], { icon: staIcon });
        marker.setZIndexOffset(-500);
        marker.setOpacity(0);
        lmap.addLayer(marker);
        staIcons.push(marker);
    }
}

let Ajax = function() {
    $.ajax({
        type: "GET",
        url: "https://localhost:7198/BusInfo/getAllRoads",
        // data: { username: $("#username").val(), content: $("#content").val() },
        // dataType: "json",
        timeout: 5000, //连接超时时间
        success: function(road) { //成功则解析每一个json，得到每一个json的键
            var array = eval(road);
            for (var i = 0; i < array.length; i++) {
                $.getJSON("../json/" + array[i].RoadInfo, function(data) {
                    show(data, color[i % 6]);
                });
            }
        }
    });
    // $.getJSON("../json/bus287.json", function(data) {
    //     show(data, '#5DAC81');
    // });
    // $.getJSON("../json/bus520.json", function(data) {
    //     show(data, '#00AA90');
    // });
    // $.getJSON("../json/bus521.json", function(data) {
    //     show(data, '#24936E');
    // });
    // $.getJSON("../json/bus725.json", function(data) {
    //     show(data, '#00896C');
    // });
    // $.getJSON("../json/bus740.json", function(data) {
    //     show(data, '#227D51');
    // });
    // $.getJSON("../json/bus810.json", function(data) {
    //     show(data, '#1B813E');
    // });
}();

let showStations = function() {
    staIcons.forEach((item) => {
        item.setOpacity(1);
    });
};

let hideStations = function() {
    staIcons.forEach((item) => {
        item.setOpacity(0);
    });
};

map.on("zoomend", (e) => {
    let scale = e.target.getZoom();
    if (scale > 12)
        showStations();
    else
        hideStations();
});

// bus icons
let busGoodIcon = L.icon({
    iconUrl: '../img/bus_good.svg',
    iconSize: [30, 30],
    iconAnchor: [15, 15]
})

let brief = "";
$.get("../brief.html", function(data, status) {
    if (status == "success")
        brief = data;
});

//let busIcon = [];
const busIcon = new Map();

let removeBusIcons = function() {
    busIcon.forEach(icon => {
        this.map.removeLayer(icon);
    });
}

let updateBuses = function() {
    $.get("/BusInfo/GetBusPos", function(data, status) {
        if (status == "success") {
            //removeBusIcons();
            data.forEach(element => {
                let x = element["x"];
                let y = element["y"];
                let bus_id = element["busID"];
                if (busIcon.has(bus_id)) {
                    let icon = busIcon.get(bus_id);
                    icon.setLatLng([y, x]);
                } else {
                    let marker = L.marker([y, x], { icon: busGoodIcon });
                    marker.bindPopup(brief + "");
                    lmap.addLayer(marker);
                    busIcon.set(bus_id, marker);
                }
            });
        }
    });
}

updateBuses();
setInterval(updateBuses, 1000);

let focus_bus = function() {
    let bus = undefined;
    busIcon.forEach((val) => {
        bus = val;
    });
    if (bus == undefined)
        return;
    let new_center = bus.getLatLng();
    lmap.flyTo(new_center, 15);
}

// side bar
let click_red = false;
let slide_img_rotate = false;
let red_slide_img_rotate = false;
let slide = document.getElementById('slide-img');
let slide_img_red = document.getElementById('slide-img-red');

$('.btn').on('click', function() {
    if (click_red) {
        click_red = false;
        return;
    }
    if (red_slide_img_rotate) {
        $('.sidebar').toggleClass('side-red');
        $('.btn').toggleClass('btn-clicked');
        $('.btn-red').toggleClass('btn-clicked-red');
        slide_img_red.removeAttribute('style');
        red_slide_img_rotate = false;
    } //如果此前红色按钮被点击过，那么就去除红色按钮给予的属性，并且恢复按钮形状，并设置为未点击

    $('.sidebar').toggleClass('side');
    $('.btn-red').toggleClass('btn-red-clicked');

    if (!slide_img_rotate)
        slide.setAttribute('style', 'transform:rotate(180deg)');
    else
        slide.removeAttribute('style');
    slide_img_rotate = !slide_img_rotate;
})


$('.btn-red').on('click', function() {
    click_red = true;
    if (slide_img_rotate) {
        $('.sidebar').toggleClass('side');
        $('.btn-red').toggleClass('btn-red-clicked');
        slide.removeAttribute('style');
        slide_img_rotate = false;
    }

    $('.sidebar').toggleClass('side-red');
    $('.btn').toggleClass('btn-clicked');
    $('.btn-red').toggleClass('btn-clicked-red');

    if (!red_slide_img_rotate)
        slide_img_red.setAttribute('style', 'transform:rotate(180deg)');
    else
        slide_img_red.removeAttribute('style');
    red_slide_img_rotate = !red_slide_img_rotate;
})

var isSpread1 = false;
$('#1-100').on('click', function() {
    if (!isSpread1) {
        $.getJSON("json/bus_data.json", function(data) {
            for (i = 0; i < 15; i++) {

                let li = document.createElement('li');
                let cont = document.createElement('div');
                cont.setAttribute('style', 'background: #060C20;');
                let a = document.createElement('a');
                a.setAttribute('href', '#a');
                a.setAttribute('onClick', 'focus_bus()');
                a.innerHTML = (i + 1) + "-" + data[0][i];
                cont.appendChild(a);
                li.appendChild(cont);
                document.getElementById('1-100-list').appendChild(li);
            }
        });
    } else {
        document.getElementById('1-100-list').innerHTML = "";
    }
    isSpread1 = !isSpread1;
})

var isSpread2 = false;
$('#101-200').on('click', function() {
    if (!isSpread2) {
        $.getJSON("json/bus_data.json", function(data) {
            for (i = 0; i < 15; i++) {

                let li = document.createElement('li');
                let cont = document.createElement('div');
                cont.setAttribute('style', 'background: #060C20;');
                let a = document.createElement('a');
                a.setAttribute('href', '#a');
                a.setAttribute('onClick', 'focus_bus()');
                a.innerHTML = (i + 101) + "-" + data[0][i];
                cont.appendChild(a);
                li.appendChild(cont);
                document.getElementById('101-200-list').appendChild(li);
            }
        });
    } else {
        document.getElementById('101-200-list').innerHTML = "";
    }
    isSpread2 = !isSpread2;
})

let optionStr = '<option selected>select0</option>';
$("#field").append(optionStr);
$("#field").selectpicker('refresh');
optionStr = '<option value="1">select1</option>';
$("#field").append(optionStr);
$("#field").selectpicker('refresh');