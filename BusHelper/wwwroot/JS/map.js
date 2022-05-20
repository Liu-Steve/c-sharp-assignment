//base map
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

//add bus 810
let show = function(data) {
    //console.log(data.data.busline_list[0].stations);
    let xs = data.data.busline_list[0].xs.split(',').map(Number);
    let ys = data.data.busline_list[0].ys.split(',').map(Number);
    let pos = [];
    //let stations = [];
    //console.log(xs, ys);
    for (let i = 0; i < xs.length; i++) {
        pos.push([ys[i], xs[i]]);
    }
    //console.log(pos);
    var polyline = L.polyline(pos, { color: 'red' });
    this.map.addLayer(polyline);

    let sta = data.data.busline_list[0].stations;
    for (let i = 0; i < sta.length; i++) {
        let p = sta[i].xy_coords.split(';').map(Number);
        //stations.push(p[1], p[0]);
        //console.log(sta[i].xy_coords, p);
        var marker = L.marker([p[1], p[0]]);
        this.map.addLayer(marker);
    }
}

let Ajax = function() {
    $.getJSON("../json/bus810.json", function(data) {
        show(data);
    });
}();