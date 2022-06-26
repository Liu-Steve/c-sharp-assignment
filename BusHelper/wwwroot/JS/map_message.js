// // map_message.js for map.html
// // 控制map页面左下角堆叠的消息框

// var audioMsg ={
//     // "msg1":{
//         "name": "张三",
//         "busNo": "8",
//         "plateNum": "鄂A·JD343",
//         "audioUrl": "./media/audio/music.mp3"
//     // }
// };
// var exception ={
//         "name": "刘涛",
//         "busNo": "33",
//         "plateNum": "鄂A·09475",
//         "info": "刘涛频繁打哈欠。",
//         "warn" : true
// };

// var audioInterval=200;
// var audioTimer=null;

// function showAudioMsg() {
//     var docFrag = document.createDocumentFragment();
//     var tempNode = document.getElementsByTagName("template")[0].content.cloneNode(true);
//     tempNode.getElementById("title").innerHTML = audioMsg.name + "&nbsp;|&nbsp;" + audioMsg.busNo + "路&nbsp;|&nbsp;" + audioMsg.plateNum;        

//     tempNode.querySelector("audio").src = audioMsg.audioUrl;
//     tempNode.querySelector("audio").setAttribute('id', audioMsg.name);
//     docFrag.appendChild(tempNode);
//     var rowRoot = document.getElementById("pop-msg-group");
//     rowRoot.appendChild(docFrag);
//     delete docFrag;
// }
// function showWeakAlert() {
//     var docFrag = document.createDocumentFragment();
//     var tempNode = document.getElementsByTagName("template")[1].content.cloneNode(true);
//     tempNode.getElementById("title").innerHTML = exception.name + "&nbsp;|&nbsp;" + exception.busNo + "路&nbsp;|&nbsp;" + exception.plateNum;        
//     tempNode.getElementById("alertInfo").innerHTML = exception.info;
//     docFrag.appendChild(tempNode);
//     var rowRoot = document.getElementById("pop-msg-group");
//     rowRoot.appendChild(docFrag);
//     delete docFrag;


// }

// function clearMsgCard() {
//     var rowRoot = document.getElementById("pop-msg-group");
//     var a=1;
//     while (rowRoot.hasChildNodes()&& a>0) {
//         rowRoot.removeChild(rowRoot.firstChild);
//         a--;
//     }
// }


// function move(){
//     console.log("call the move()");
//     var window = document.getElementsByName("pop-window")[0];
//     console.log(window);
//     var bottom = parseInt(window.style.bottom);
//     console.log(bottom);
//     bottom += 200;
//     window.style.bottom = bottom + "px";
// }
// function start(){    
//     clearMsgCard();
//     showAudioMsg();
// }

// // 弹出语音信息时间间隔, ms
// audioTimer= setTimeout(start, audioInterval);
// setTimeout(move, 3500);
// setTimeout(showWeakAlert,3500);