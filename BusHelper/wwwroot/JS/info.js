let img_rotate = false;
$('.btn').on('click', function() {
    $('.sidebar').toggleClass('side');
    let slide_img = document.getElementById('slide-img')
    if (!img_rotate)
        slide_img.setAttribute('style', 'transform:rotate(180deg)');
    else
        slide_img.removeAttribute('style');
    img_rotate = !img_rotate;
})
var isSpread1 = false;
$('#1-100').on('click', function() {
    if (!isSpread1) {
        $.getJSON("json/bus_data.json", function(data) {
            for (i = 0; i < 15; i++) {

                let li = document.createElement('li');
                let cont = document.createElement('div');
                cont.setAttribute('style', 'background: #060C20;');
                let a = document.createElement('a');
                a.setAttribute('href', 'info.html');
                a.setAttribute('target', '_blank');
                a.innerHTML = (i + 1) + "-" + data[0][i];
                cont.appendChild(a);
                li.appendChild(cont);
                document.getElementById('1-100-list').appendChild(li);
            }
        });
        // let arry = new Array('-鄂A·JD343', '-鄂A·13495', '-鄂A·C6103', '-鄂A·C1180', '-鄂A·888U8', '-鄂A·H6213',
        //     '-鄂A·E3923', '-鄂A·6265B', '-鄂A·A9965', '-鄂A·M5852', '-鄂A·54374', '-鄂A·15466', '-鄂A·B0888',
        //     '-鄂A·V6600', '-鄂A·9P80M')

    } else {
        document.getElementById('1-100-list').innerHTML = "";
    }
    isSpread1 = !isSpread1;
})

var isSpread2 = false;
$('#101-200').on('click', function() {
    if (!isSpread2) {
        $.getJSON("json/bus_data.json", function(data) {
            for (i = 0; i < 15; i++) {
                let li = document.createElement('li');
                let cont = document.createElement('div');
                cont.setAttribute('style', 'background: #060C20;');
                let a = document.createElement('a');
                a.setAttribute('href', 'info.html');
                a.setAttribute('target', '_blank');
                a.innerHTML = (i + 1) + "-" + data[1][i];
                cont.appendChild(a);
                li.appendChild(cont);
                document.getElementById('101-200-list').appendChild(li);
            }
        });
    } else {
        document.getElementById('101-200-list').innerHTML = "";
    }
    isSpread2 = !isSpread2;
})

var isLinking = false;
$('#contact').on('click', function() {
    if (!isLinking) {
        let cont = document.createElement('div');
        cont.setAttribute('id', 'linking');
        cont.setAttribute('class', 'd-flex flex-row container-test justify-content-center pt-5');
        let img = document.createElement('img');
        img.setAttribute('src', 'img/loading.gif');
        img.setAttribute('width', '30px');
        img.setAttribute('height', '30px');
        let h3 = document.createElement('h3');
        h3.setAttribute('class', 'linking');
        h3.innerHTML = "链接建立中";
        cont.appendChild(img);
        cont.appendChild(h3);
        document.getElementById('real-time').appendChild(cont);
        // } else {
        //     document.getElementById('101-200-list').innerHTML = "";
    }
    // isSpread2 = !isSpread2;
    setTimeout(() => {
        let linking = document.getElementById('linking');
        document.getElementById('real-time').removeChild(linking);
        let cont1 = document.createElement('div');
        cont1.setAttribute('class', 'd-flex flex-row container-test justify-content-center pt-5');
        cont1.setAttribute('id', 'linking-time');
        let h3 = document.createElement('h3');
        h3.setAttribute('class', 'linking');
        h3.innerHTML = "链接已建立：通话时长-{{linkingTime}}";
        let cont2 = document.createElement('div');
        cont2.setAttribute('class', 'd-flex flex-row container-test justify-content-center');
        let p = document.createElement('p');
        p.setAttribute('class', 'mini-text');
        p.innerHTML = "温馨提示：所有您和司机进行的言论沟通将被录音保存";
        cont1.appendChild(h3);
        cont2.appendChild(p);
        document.getElementById('real-time').appendChild(cont1);
        document.getElementById('real-time').appendChild(cont2);
        let linkingTime = new Vue({
            el: '#linking-time',
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
})