// map_message.js for map.html
// 控制map页面左下角堆叠的消息框
var rowRoot = document.getElementById("pop-msg-group");
var audioMsg = {
    // "msg1":{
    "name": "张三",
    "busNo": "8",
    "plateNum": "鄂A·JD343",
    "audioUrl": "./media/audio/music.mp3"
        // }
};

var exception = {
    "name": "刘涛",
    "busNo": "33",
    "plateNum": "鄂A·09475",
    "info": "刘涛频繁打哈欠。",
    "warn": true
};

var audioInterval = 100;
var audioTimer = null;

function showAudioMsg() {
    var docFrag = document.createDocumentFragment();
    var tempNode = document.getElementsByTagName("template")[0].content.cloneNode(true);
    tempNode.getElementById("title").innerHTML = '来自' + audioMsg.name + "&nbsp;|&nbsp;" + audioMsg.busNo + "路&nbsp;|&nbsp;" + audioMsg.plateNum + '的语音';

    tempNode.querySelector("audio").src = audioMsg.audioUrl;
    tempNode.querySelector("audio").setAttribute('id', audioMsg.name);
    docFrag.appendChild(tempNode);
    var rowRoot = document.getElementById("pop-msg-group");
    rowRoot.appendChild(docFrag);
    delete docFrag;
}

function showWeakAlert() {
    var docFrag = document.createDocumentFragment();
    var tempNode = document.getElementsByTagName("template")[1].content.cloneNode(true);
    tempNode.getElementById("title").innerHTML = exception.name + "&nbsp;|&nbsp;" + exception.busNo + "路&nbsp;|&nbsp;" + exception.plateNum;
    tempNode.getElementById("alertInfo").innerHTML = exception.info;
    docFrag.appendChild(tempNode);
    var rowRoot = document.getElementById("pop-msg-group");
    rowRoot.appendChild(docFrag);
    delete docFrag;
}

function clearMsgCard(e) {
    rowRoot.removeChild(e);
    updateLocation();
}

function updateLocation() {
    var all = rowRoot.children;
    var nextBottom = 10;
    for (i = all.length - 1; i >= 0; i--) {
        var window = all[i];
        var bottom = parseInt(window.style.bottom);
        bottom = nextBottom;
        window.style.bottom = bottom + "px";
        var hight = window.clientHeight + 10;
        nextBottom += hight;
    }
}

function detail() {
    window.open("info.html", "_blank");
}

function start() {
    showAudioMsg();
    showWeakAlert();
}

// 弹出语音信息时间间隔, ms
audioTimer = setTimeout(start, audioInterval);
setTimeout(showWeakAlert, 100);
setTimeout(updateLocation, 100);