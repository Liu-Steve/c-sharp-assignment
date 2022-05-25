var seen_msg={
    "driver1":{
        "name":"Lucy",
        "busNo":"8",
        "plateNum":"鄂A·7788",
        "imgUrl":"./media/img/driver1.jpg",
        "audioUrl":"./media/audio/music.mp3"
    },
    "driver2":{
        "name":"Amy",
        "busNo":"84",
        "plateNum":"鄂A·7788",
        "imgUrl":"./media/img/driver2.jpg",
        "audioUrl":"./media/audio/music.mp3"
    },
    "driver3":{
        "name":"Jack",
        "busNo":"39",
        "plateNum":"鄂A·7788",
        "imgUrl":"./media/img/driver3.jpg",
        "audioUrl":"./media/audio/music.mp3"
    },
    "driver4":{
        "name":"Jac3k",
        "busNo":"339",
        "plateNum":"鄂A·73788",
        "imgUrl":"./media/img/driver4.jpg",
        "audioUrl":"./media/audio/music.mp3"
    }
};
var keys=Object.keys(seen_msg);
var docFrag = document.createDocumentFragment();
for(var i =0; i<keys.length; i++){
    var tempNode=document.getElementsByTagName("template")[0].content.cloneNode(true);
    tempNode.getElementById("title").innerHTML 
        = seen_msg[keys[i]].name+"&amp;nbsp;|&amp;nbsp;"+seen_msg[keys[i]].busNo+"路&amp;nbsp;|&amp;nbsp;"+seen_msg[keys[i]].plateNum;
    tempNode.querySelector("img").src = seen_msg[keys[i]].imgUrl;
    tempNode.querySelector("audio").src = seen_msg[keys[i]].audioUrl;
    docFrag.appendChild(tempNode);
    console.log(tempNode);
}
var rowRoot = document.getElementById("card-group");
console.log(rowRoot)
rowRoot.appendChild(docFrag);
console.log(docFrag);
delete docFrag;
