// // 用于生成map.html界面的司机消息，并在点击之后remove该消息


// var msg_json = {
//     "driver1": {
//         "name": "张三",
//         "busNo": "8",
//         "plateNum": "鄂A·7788",
//         "imgUrl": "./media/img/driver11.jpg",
//         "audioUrl": "./media/audio/music.mp3"
//     }
// };

// function createCard() {
//     var cardRoot = document.getElementById("pop-card-root");
//     var keys = Object.keys(msg_json);
//     var tempNode = document.getElementsByTagName("template")[0].content.cloneNode(true);
//     tempNode.getElementById("title").innerHTML = msg_json[keys[0]].name + "&nbsp;|&nbsp;" + msg_json[keys[0]].busNo + "路&nbsp;|&nbsp;" + msg_json[keys[0]].plateNum;
//     tempNode.querySelector("img").src = msg_json[keys[0]].imgUrl;
//     tempNode.querySelector("audio").src = msg_json[keys[0]].audioUrl;
//     cardRoot.appendChild(tempNode);
//     console.log("create a card!");
// }
// setTimeout(createCard,3000);


// var card = document.getElementById("pop-card-root");
// console.log("card=");
// console.log(card);
// card.onclick=function(){
//     console.log("click the pop panel")
//     card.style.display="none";
//     console.log("delete a card!");
// };