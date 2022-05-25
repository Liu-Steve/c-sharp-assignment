document.querySelector("#contact").addEventListener("click", () => {
    new $Msg({
        content: "我的自定义弹窗好了",
        type: "success",
        cancle: function() {
            let cancle = new $Msg({
                content: "我是取消后的回调"
            })
        },
        confirm: function() {
            new $Msg({ content: "我是确定后的回调" })
        }
    })
})