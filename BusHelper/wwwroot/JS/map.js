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
let staIcon = L.icon({
    iconUrl: '../img/dot.svg',
    iconSize: [10, 10],
    iconAnchor: [5, 5]
});

let show = function(data) {
    let xs = data.data.busline_list[0].xs.split(',').map(Number);
    let ys = data.data.busline_list[0].ys.split(',').map(Number);
    let pos = [];
    for (let i = 0; i < xs.length; i++) {
        pos.push([ys[i], xs[i]]);
    }
    var polyline = L.polyline(pos, { color: '#88FF88' });
    this.map.addLayer(polyline);

    let sta = data.data.busline_list[0].stations;
    for (let i = 0; i < sta.length; i++) {
        let p = sta[i].xy_coords.split(';').map(Number);
        var marker = L.marker([p[1], p[0]], { icon: staIcon });
        lmap.addLayer(marker);
    }
}

let Ajax = function() {
    $.getJSON("../json/bus810.json", function(data) {
        show(data);
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
var isSpread = false;
$('#city').on('click', function() {
    if (!isSpread) {
        for (i = 0; i < 30; i++) {
            let li = document.createElement('li');
            let cont = document.createElement('div');
            cont.setAttribute('style', 'background: #FFFFFF;padding: 5px;');
            let p = document.createElement('p');
            p.innerHTML = '301-鄂A·JD343';
            cont.appendChild(p);
            li.appendChild(cont);
            document.getElementById('city-list').appendChild(li);
        }
    } else {
        document.getElementById('city-list').innerHTML = "";
    }
    isSpread = !isSpread;
})