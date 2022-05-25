// base map
let lmap = L.map("map", {
    minZoom: 10,
    maxZoom: 15,
    center: [30.6, 114.3],
    zoom: 11,
    zoomDelta: 0.5,
    fullscreenControl: false,
    zoomControl: false,
    attributionControl: false
});

this.map = lmap;

//http://map.geoq.cn/ArcGIS/rest/services/ChinaOnlineCommunity/MapServer/tile/{z}/{y}/{x}//arcgis在线地图
this.baseLayer = L.tileLayer("http://webrd0{s}.is.autonavi.com/appmaptile?lang=zh_cn&size=1&scale=1&style=8&x={x}&y={y}&z={z}", {
    attribution: '&copy; 高德地图',
    maxZoom: 15,
    minZoom: 10,
    subdomains: "1234"
});

this.map.addLayer(this.baseLayer);

// add bus 810
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
        lmap.addLayer(marker);
    }
}

let Ajax = function() {
    $.getJSON("../json/bus287.json", function(data) {
        show(data, '#770077');
    });
    $.getJSON("../json/bus520.json", function(data) {
        show(data, '#007777');
    });
    $.getJSON("../json/bus521.json", function(data) {
        show(data, '#777700');
    });
    $.getJSON("../json/bus725.json", function(data) {
        show(data, '#000077');
    });
    $.getJSON("../json/bus740.json", function(data) {
        show(data, '#770000');
    });
    $.getJSON("../json/bus810.json", function(data) {
        show(data, '#007700');
    });
}();

// bus icons
let busGoodIcon = L.icon({
    iconUrl: '../img/bus_good.svg',
    iconSize: [30, 30],
    iconAnchor: [15, 15]
})

let busIcon = [];

let removeBusIcons = function() {
    busIcon.forEach(icon => {
        this.map.removeLayer(icon);
    });
}

let updateBuses = function() {
    $.get("/BusInfo/GetBusPos", function(data, status) {
        if (status == "success") {
            removeBusIcons();
            data.forEach(element => {
                let x = element["x"];
                let y = element["y"];
                let marker = L.marker([y, x], { icon: busGoodIcon });
                marker.bindPopup("<b>Hello world!</b><br>I am a popup.");
                lmap.addLayer(marker);
                busIcon.push(marker)
            });
        }
    });
}

updateBuses();
setInterval(updateBuses, 1000);

// side bar
let slide_img_rotate = false;
$('.btn').on('click', function() {
    $('.sidebar').toggleClass('side');
    let slide_img = document.getElementById('slide-img')
    if (!slide_img_rotate)
        slide_img.setAttribute('style', 'transform:rotate(180deg)');
    else
        slide_img.removeAttribute('style');
    slide_img_rotate = !slide_img_rotate;
})
var isSpread1 = false;
$('#1-100').on('click', function() {
    if (!isSpread1) {
        $.getJSON("json/bus_data.json", function(data) {
            for (i = 0; i < 15; i++) {

                let li = document.createElement('li');
                let cont = document.createElement('div');
                cont.setAttribute('style', 'background: #060C20;');
                let p = document.createElement('p');
                p.innerHTML = (i + 1) + "-" + data[0][i];
                cont.appendChild(p);
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
                let p = document.createElement('p');
                p.innerHTML = (i + 1) + "-" + data[1][i];
                cont.appendChild(p);
                li.appendChild(cont);
                document.getElementById('101-200-list').appendChild(li);
            }
        });
    } else {
        document.getElementById('101-200-list').innerHTML = "";
    }
    isSpread2 = !isSpread2;
})