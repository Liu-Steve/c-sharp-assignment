// map_message.js for map.html
// 控制map页面左下角堆叠的消息框
var alreadyAudioUnread = new Array();

var alreadyAlertUnread = new Array();

var rowRoot = document.getElementById("pop-msg-group");

var audioMsg = {
    "name": "张三",
    "busNo": "8",
    "plateNum": "鄂A·73788",
    "audioUrl": "./media/audio/music.mp3"
};

var exception = {
    "name": "刘涛",
    "busNo": "33",
    "plateNum": "鄂A·JD343",
    "info": "刘涛频繁打哈欠。",
    "warn": true
};

var audioInterval = 100;
var audioTimer = null;

function showAudioMsg() {
    var docFrag = document.createDocumentFragment();
    var tempNode = document.getElementsByTagName("template")[0].content.cloneNode(true);
    tempNode.getElementById("title").innerHTML = '来自' + audioMsg.name + "&nbsp;|&nbsp;" + audioMsg.busNo + "路&nbsp;|&nbsp;";
    tempNode.getElementById("busId-audio").innerHTML = exception.plateNum + '的语音';
    tempNode.querySelector("audio").src = "/BusInfo/getUnreadAudio " + audioMsg.audioUrl;
    tempNode.querySelector("audio").setAttribute('id', audioMsg.name);
    docFrag.appendChild(tempNode);
    var rowRoot = document.getElementById("pop-msg-group");
    rowRoot.appendChild(docFrag);
    delete docFrag;
}

function showWeakAlert() {
    var docFrag = document.createDocumentFragment();
    var tempNode = document.getElementsByTagName("template")[1].content.cloneNode(true);
    tempNode.getElementById("title").innerHTML = exception.name + "&nbsp;|&nbsp;" + exception.busNo + "路&nbsp;|&nbsp;";
    tempNode.getElementById("busId-weak").innerHTML = exception.plateNum;
    tempNode.getElementById("alertInfo").innerHTML = exception.info;
    docFrag.appendChild(tempNode);
    var rowRoot = document.getElementById("pop-msg-group");
    rowRoot.appendChild(docFrag);
    delete docFrag;
}

//去掉已经处理过的信息，在屏幕上去掉，调整位置，同时从保存信息的去重表中去掉
function clearMsgCard(e) {
    rowRoot.removeChild(e);
    updateLocation();
    removeAudio(e.querySelector("audio").src);
}

//更新左下角消息的位置
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

//携带ID信息跳转到新的网页
function detail(info) {
    window.open("info.html?busId=" + info.innerHTML, "_blank");
}

// function start() {
//     showAudioMsg();
//     showWeakAlert();
// }

//去重，避免每s发起请求，未读消息反复被推送到前端
function avoidAudioRepeat(data) {
    var array = eval(data);
    for (var i = 0; i < array.length; i++) {
        //如果在现存数组中找不到这个元素，就加进去
        if (alreadyAudioUnread.indexOf(array[i].audioUrl) == -1) {
            alreadyAudioUnread.push(array[i].audioUrl);
            audioMsg = array[i];
            showAudioMsg();
        }
    }
    updateLocation();
}

//去重，避免每s发起请求，未处理的弱预警反复被推送到前端
function avoidAlertRepeat() {
    //如果在现存数组中找不到这个元素，就加进去
    if (alreadyAlertUnread.indexOf(recording) == -1) {
        alreadyAlertUnread.push(recording);
    }
    showWeakAlert();
}

//去掉音频去重表里面的元素
function removeAudio(toDelete) {
    alreadyAudioUnread.splice(alreadyAlertUnread.indexOf(toDelete), 1);
}

//去掉预警去重表里面的元素
function removeAlert(toDelete) {
    alreadyAlertUnread.splice(alreadyAlertUnread.indexOf(toDelete), 1);
}

// 弹出语音信息时间间隔, ms
// audioTimer = setTimeout(start, audioInterval);
// setTimeout(showWeakAlert, 100);
// setTimeout(updateLocation, 100);


//1s钟发送一次数据更新，请求最近的未读消息
window.setInterval(() => {
    setTimeout(() => {
        $.ajax({
            type: "GET",
            url: "/BusInfo/getUnreadAudio",
            //dataType: "json",
            contentType: "application/json",
            timeout: 5000, //连接超时时间
            success: function(data) { //成功则更新数据
                avoidAudioRepeat(data);
            }
        });
    }, 0)
}, 1000)