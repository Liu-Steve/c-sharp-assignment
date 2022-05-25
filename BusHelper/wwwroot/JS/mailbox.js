var seen_msg={
    "driver1":{
        "name":"李四",
        "busNo":"8",
        "plateNum":"鄂A·7788",
        "imgUrl":"./media/img/driver1.jpg",
        "audioUrl":"./media/audio/music.mp3"
    },
    "driver2":{
        "name":"李四光",
        "busNo":"84",
        "plateNum":"鄂A·7788",
        "imgUrl":"./media/img/driver2.jpg",
        "audioUrl":"./media/audio/music.mp3"
    },
    "driver3":{
        "name":"李少波",
        "busNo":"39",
        "plateNum":"鄂A·7788",
        "imgUrl":"./media/img/driver3.jpg",
        "audioUrl":"./media/audio/music.mp3"
    },
    "driver4":{
        "name":"王铎翰",
        "busNo":"339",
        "plateNum":"鄂A·73788",
        "imgUrl":"./media/img/driver4.jpg",
        "audioUrl":"./media/audio/music.mp3"
    }
};

var unseen_msg={
    "driver1":{
        "name":"张瑟",
        "busNo":"8",
        "plateNum":"鄂A·7788",
        "imgUrl":"./media/img/driver5.jpg",
        "audioUrl":"./media/audio/music.mp3"
    },
    "driver2":{
        "name":"王二",
        "busNo":"84",
        "plateNum":"鄂A·7788",
        "imgUrl":"./media/img/driver6.jpg",
        "audioUrl":"./media/audio/music.mp3"
    }
};

showMsg(unseen_msg);

function clearRow(){
    var rowRoot = document.getElementById("card-group");
    while(rowRoot.hasChildNodes()){
        rowRoot.removeChild(rowRoot.firstChild);
    }
}

function showMsg(msg_json){
    var keys=Object.keys(msg_json);
    var docFrag = document.createDocumentFragment();
    for(var i =0; i<keys.length; i++){
        var tempNode=document.getElementsByTagName("template")[0].content.cloneNode(true);        
        tempNode.getElementById("title").innerHTML 
            =msg_json[keys[i]].name+"&amp;nbsp;|&amp;nbsp;"+msg_json[keys[i]].busNo+"路&amp;nbsp;|&amp;nbsp;"+seen_msg[keys[i]].plateNum;
        tempNode.querySelector("img").src = msg_json[keys[i]].imgUrl;
        tempNode.querySelector("audio").src = msg_json[keys[i]].audioUrl;
        docFrag.appendChild(tempNode);
        // console.log("tempNode:\n");
        // console.log(tempNode);
    }
    var rowRoot = document.getElementById("card-group");
    // console.log("row root:");
    // console.log("测试")
    // console.log(rowRoot);
    rowRoot.appendChild(docFrag);
    // console.log(docFrag);
    delete docFrag;
}

function showAllMsg(){
    clearRow();
    showMsg(unseen_msg);
    showMsg(seen_msg);
    // this.className+=" active";
    // var btn1=document.getElementById("unSeenBtn");
    // btn1.removeClass("active");
    // var btn2=document.getElementById("seenBtn");
    // btn2.removeClass("active");
}
function showUnseenMsg(){
    clearRow();
    showMsg(unseen_msg);
    // this.className+=" active";
    // var btn1=document.getElementById("allBtn");
    // btn1.removeClass("active");
    // var btn2=document.getElementById("seenBtn");
    // btn2.removeClass("active");
}
function showSeenMsg(){
    clearRow();
    showMsg(seen_msg);
    // this.className+=" active";
    // var btn1=document.getElementById("unSeenBtn");
    // btn1.removeClass("active");
    // var btn2=document.getElementById("allBtn");
    // btn2.removeClass("active");
}
