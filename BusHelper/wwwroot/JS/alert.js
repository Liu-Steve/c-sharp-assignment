// 获取弹窗元素
var modal = document.getElementById("simpleModal");

// console.log("Haven getten the modal by id!")


// 获取关闭弹窗按钮元素
var closeBtn = document.getElementsByClassName("closeBtn")[0];

// 监听关闭弹窗事件
closeBtn.addEventListener("click", closeModal);

// 监听window关闭弹窗事件
window.addEventListener("click", outsideClick);

// 弹窗事件
function openModal() {
    modal.style.display = "block";
    console.log("open the Model!");
}

// 关闭弹框事件
function closeModal() {
    modal.style.display = "none";
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
