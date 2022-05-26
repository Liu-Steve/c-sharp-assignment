var seen_msg={
    "driver1":{
        "name":"李四",
        "busNo":"8",
        "plateNum":"鄂A·JD343",
        "imgUrl":"./media/img/driver4.jpg",
        "audioUrl":"./media/audio/music.mp3"
    },
    "driver2":{
        "name":"赵晗",
        "busNo":"84",
        "plateNum":"鄂A·13495",
        "imgUrl":"./media/img/driver12.jpg",
        "audioUrl":"./media/audio/music.mp3"
    },
    "driver3":{
        "name":"李少波",
        "busNo":"39",
        "plateNum":"鄂A·C6103",
        "imgUrl":"./media/img/driver17.jpg",
        "audioUrl":"./media/audio/music.mp3"
    },
    "driver4":{
        "name":"王蕊",
        "busNo":"339",
        "plateNum":"鄂A·C1180",
        "imgUrl":"./media/img/driver21.jpg",
        "audioUrl":"./media/audio/music.mp3"
    }
};

var unseen_msg={
    "driver1":{
        "name":"赵晗",
        "busNo":"84",
        "plateNum":"鄂A·13495",
        "imgUrl":"./media/img/driver11.jpg",
        "audioUrl":"./media/audio/music.mp3"
    },
    "driver2":{
        "name":"李四",
        "busNo":"8",
        "plateNum":"鄂A·JD343",
        "imgUrl":"./media/img/driver7.jpg",
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
            =msg_json[keys[i]].name+"&nbsp;|&nbsp;"+msg_json[keys[i]].busNo+"路&nbsp;|&nbsp;"+msg_json[keys[i]].plateNum;
        tempNode.querySelector("img").src = msg_json[keys[i]].imgUrl;
        tempNode.querySelector("audio").src = msg_json[keys[i]].audioUrl;
        tempNode.querySelector("audio").setAttribute('id',msg_json[keys[i]].name);
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
}
function showUnseenMsg(){
    clearRow();
    showMsg(unseen_msg);
}
function showSeenMsg(){
    clearRow();
    showMsg(seen_msg);
}

var un=2;

var red1=new Vue({
    el:"#red1",
    data:{
        unread:2
    }
})

var red2=new Vue({
    el:"#red2",
    data:{
        unread:2
        }
})

let zhangse=document.getElementById('zhangse');
zhangse.addEventListener('play', function(){
 if(un>0)
 {
    un=un-1;
 }
red1.unread=un;
red2.unread=un;
});

$('#wanger').click(function(){
    if(un>0)
    {
       un=un-1;
    }
   red1.unread=un;
   red2.unread=un;
   })




