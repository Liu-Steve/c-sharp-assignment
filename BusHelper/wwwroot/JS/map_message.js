// map_message.js for map.html
// 控制map页面左下角堆叠的消息框
var alreadyAudioUnread = new Array();

var alreadyAlertUnread = new Array();

var rowRoot = document.getElementById("pop-msg-group");

var audioMsg = {
    "name": "张三",
    "busNo": "8",
    "plateNum": "鄂A·73788",
    "audioUrl": "./media/audio/music.mp3",
    "msgId": ""
};

var exception = {
    "name": "刘涛",
    "busNo": "33",
    "plateNum": "鄂A·JD343",
    "info": "刘涛频繁打哈欠。",
    "alertId": ""
};

var audioInterval = 100;
var audioTimer = null;

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

//上传录音
const sendAudioFile = blob => {
    var fileName = "manager-" + (new Date().getTime()) + ".ogg";

    var data = {
        BusId: "鄂A·73788",
        Content: fileName
    }
    $.ajax({
        type: "POST",
        url: "/BusInfo/PostManagerMsg",
        data: JSON.stringify(data),
        //dataType: "json",
        contentType: "application/json",
        timeout: 5000, //连接超时时间
        success: function(data) {
            console.log(data)
        }
    });

    var file = new window.File(
        [blob],
        fileName
    );
    const formData = new FormData();
    formData.append('audio-file', file);
    return fetch('/BusInfo/Upload', {
        method: 'POST',
        body: formData
    });
};

//绑定录音按钮
var bindRecord = (btn, audio, sendBtn, parent, msgId) => {
    if (navigator.mediaDevices.getUserMedia) {
        const constraints = {
            audio: true
        };
        navigator.mediaDevices.getUserMedia(constraints).then(
            stream => {
                console.log("授权成功！");
                const recordBtn = btn;
                const mediaRecorder = new MediaRecorder(stream);
                var chunks = [];
                recordBtn.onclick = () => {
                    if (mediaRecorder.state === "recording") {
                        mediaRecorder.stop();
                        console.log(chunks)
                        mediaRecorder.onstop = e => {
                            var blob = new Blob(chunks, {
                                type: "audio/ogg; codecs=opus"
                                    //type: "audio/mp3"
                            });
                            chunks = [];
                            var audioURL = window.URL.createObjectURL(blob);
                            const audioSrc = audio;
                            audioSrc.src = audioURL;
                            sendBtn.onclick = function() {
                                //sendAudioFile(blob);
                                setAudioRead(parent, msgId);
                            };
                        };
                        recordBtn.textContent = "重录";
                        console.log("录音结束");
                        audio.setAttribute("style", "width: 98%; height: 45px; margin: 5px 0px");
                        sendBtn.setAttribute("style", "margin: 0px 3px;");
                        updateLocation();
                    } else {
                        mediaRecorder.start();
                        mediaRecorder.ondataavailable = function(e) {
                            chunks.push(e.data);
                        };
                        console.log(chunks)
                        console.log("录音中...");
                        recordBtn.textContent = "录音中";
                    }
                    console.log("录音器状态：", mediaRecorder.state);
                };

            },
            () => {
                console.error("授权失败！");
            }
        );
    } else {
        console.error("浏览器不支持 getUserMedia");
    }
};

function showAudioMsg() {
    var docFrag = document.createDocumentFragment();
    var tempNode = document.getElementsByTagName("template")[0].content.cloneNode(true);
    tempNode.getElementById("title").innerHTML = '来自' + audioMsg.name + "&nbsp;|&nbsp;" + audioMsg.busNo + "路&nbsp;|&nbsp;";
    tempNode.getElementById("busId-audio").innerHTML = exception.plateNum + '的语音';
    tempNode.querySelector("source").src = "https://safengine.xyz/BusInfo/DownAudio?fileName=" + audioMsg.audioUrl;
    // tempNode.querySelector("audio").setAttribute('id', audioMsg.msgId);
    let recordBtn = tempNode.querySelector(".record");
    let recordAudio = tempNode.querySelector(".audio-manager");
    let sendBtn = tempNode.querySelector(".send");
    let parent = tempNode.getElementById("parent");
    bindRecord(recordBtn, recordAudio, sendBtn, parent, audioMsg.msgId);
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

//去掉已经处理过的信息，修改未读属性为已读，在屏幕上去掉，调整位置，同时从保存信息的去重表中去掉
function clearMsgCard(e, msgId) {
    rowRoot.removeChild(e);
    updateLocation();
    removeAudio(msgId);
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
        if (alreadyAudioUnread.indexOf(array[i].msgId) == -1) {
            alreadyAudioUnread.push(array[i].msgId);
            audioMsg = array[i];
            showAudioMsg();
        }
    }
    updateLocation();
}

//去重，避免每s发起请求，未处理的弱预警反复被推送到前端
function avoidAlertRepeat(data) {
    var array = eval(data);
    for (var i = 0; i < array.length; i++) {
        //如果在现存数组中找不到这个元素，就加进去
        if (alreadyAlertUnread.indexOf(array[i].alertId) == -1) {
            alreadyAlertUnread.push(array[i].alertId);
            exception = array[i];
            showWeakAlert();
        }
    }
    updateLocation();
}

//去掉音频去重表里面的元素
function removeAudio(toDelete) {
    alreadyAudioUnread.splice(alreadyAlertUnread.indexOf(toDelete), 1);
}

//去掉预警去重表里面的元素
function removeAlert(toDelete) {
    alreadyAlertUnread.splice(alreadyAlertUnread.indexOf(toDelete), 1);
}

//将未读语音消息设置为已读
function setAudioRead(parent, msgId) {
    $.ajax({
        type: "POST",
        url: "/BusInfo/MarkAudioRead",
        data: "'" + msgId + "'",
        //dataType: "json",
        contentType: "application/json",
        timeout: 5000, //连接超时时间
        success: function() { //成功则更新数据
            clearMsgCard(parent, msgId);
        }
    });
}

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

window.setInterval(() => {
    setTimeout(() => {
        $.ajax({
            type: "GET",
            url: "/BusInfo/getUnreadAlert",
            //dataType: "json",
            contentType: "application/json",
            timeout: 5000, //连接超时时间
            success: function(data) { //成功则更新数据
                avoidAlertRepeat(data);
            }
        });
    }, 0)
}, 1000)