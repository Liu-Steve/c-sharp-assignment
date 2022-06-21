
var un = 2;

var red1 = new Vue({
    el: "#red1",
    data: {
        unread: 2
    }
})

var red2 = new Vue({
    el: "#red2",
    data: {
        unread: 2
    }
})

var red3 = new Vue({
    el: "#red3",
    data: {
        unread: 2
    }
})

var red4 = new Vue({
    el: "#red4",
    data: {
        unread: 2
    }
})

let zhangsan = document.getElementById('张三');
zhangsan.addEventListener('play', function() {
    if (un > 0) {
        un = un - 1;
    }
    red1.unread = un;
    red2.unread = un;
    red3.unread = un;
    red4.unread = un;
});

let lisi = document.getElementById('李四');
lisi.addEventListener('play', function() {
    if (un > 0) {
        un = un - 1;
    }
    red1.unread = un;
    red2.unread = un;
    red3.unread = un;
    red4.unread = un;
});