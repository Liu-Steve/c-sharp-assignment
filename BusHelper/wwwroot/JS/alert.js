// 获取弹窗元素
var modal = document.getElementById("simpleModal");

var linking = document.getElementById('link');

// console.log("Haven getten the modal by id!")


// 获取关闭弹窗按钮元素
var closeBtn = document.getElementById("linkBtn");

var closeBtnLink = document.getElementsByClassName("closeBtn")[0];

// 监听关闭弹窗事件
closeBtn.addEventListener("click", closeModal);

closeBtnLink.addEventListener('click', closeLink);

// 监听window关闭弹窗事件
window.addEventListener("click", outsideClick);

// 弹窗事件
function openModal() {
    modal.style.display = "block";
}

// 关闭弹框事件
function closeModal() {
    modal.style.display = "none";
    linking.style.display = "block";
    setTimeout(() => {
        let link = document.getElementById('link');
        let linkImg = document.getElementById('linkImg');
        link.removeChild(linkImg);
        let linkAlready = document.getElementById('linkCharacter');
        linkAlready.innerHTML = "链接已建立：通话时长-{{linkingTime}}";
        p.innerHTML = "温馨提示：所有您和司机进行的言论沟通将被录音保存";
        link.appendChild(linkAlready);
        link.appendChild(p);
        let linkingTime = new Vue({
            el: '#linkCharacter',
            data: {
                linkingTime: "00: 00: 00"
            }
        })
        var timer = 0
        window.setInterval(() => {
            setTimeout(() => {
                timer = timer + 1;
                linkingTime.linkingTime = "00:00:0" + timer;
            }, 0)
        }, 1000)
    }, 2000)
}

//关闭连接弹窗
function closeLink() {
    linking.style.display = "none";
}

// outsideClick
function outsideClick(e) {
    if (e.target == modal) {
        modal.style.display = "none";
    }
}

setTimeout(() => {
    openModal();
}, 1000)