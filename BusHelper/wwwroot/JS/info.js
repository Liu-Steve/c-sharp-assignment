let normalColor = '#20C997';
let abnormalColor = '#EEAA22';

var vm = new Vue({
    el: "#time",
    data: {
        time: Date()
    }
})

var imgSrc = new Vue({
    el: "#img-real",
    data: {
        src: "fake_data/default.jpg"
    }
})

var po = new Vue({
    el: "#po",
    data: {
        possible: [0.2, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5],
        color: [
            normalColor,
            normalColor,
            normalColor,
            normalColor,
            normalColor,
            normalColor,
            normalColor,
            normalColor
        ]
    }
})

var re = new Vue({
    el: "#record",
    data: {
        record: [0, 0, 0, 0, 0, 0, 0, 0],
        color: [
            normalColor,
            normalColor,
            normalColor,
            normalColor,
            normalColor,
            normalColor,
            normalColor,
            normalColor
        ]
    }
})

var con = new Vue({
    el: "#con",
    data: {
        condition: [75, 95, 70, 36.9, 96],
        alcohol: "0",
        color: [
            normalColor,
            normalColor,
            normalColor,
            normalColor,
            normalColor,
            normalColor
        ]
    }
})

var driver = new Vue({
    el: "#driver",
    data: {
        roadId: "道路",
        busId: "车牌号",
        name: "姓名"
    }
})

var phone = new Vue({
    el: "#phone",
    data: {
        phone: "电话号码"
    }
})



// function drawOneRect(name, i) {
//     var canvas = document.getElementById(name);
//     canvas.width = canvas.width; //清空
//     var context = canvas.getContext('2d');
//     context.fillStyle = "#20c997"
//     var l = Math.round(random * 100);
//     context.fillRect(0, 0, l, 2);
// }

// function drawRect() {
//     for (i = 0; i < 8; i++) {
//         drawOneRect("rectangle" + i, i);
//     }
// }

//normal-condition
// function setCon() {
//     Vue.set(con.condition, 0, 75 + Math.round(Math.random() * 5));
//     Vue.set(con.condition, 1, 95 + Math.round(Math.random() * 5));
//     Vue.set(con.condition, 2, 70 + Math.round(Math.random() * 5));
//     Vue.set(con.condition, 3, (36.3 + (Math.random() * 0.5)).toFixed(2));
//     Vue.set(con.condition, 4, 96 + Math.round(Math.random() * 2));
// }

// var imgCont = 1;

// function setImg() {
//     if (imgCont == 42) {
//         imgCont = 1;
//     }
//     imgSrc.src = "fake_data/" + imgCont + ".jpg";
//     imgCont = imgCont + 1;
// }

window.setInterval(() => {
    setTimeout(() => {
        vm.time = Date();
        // for (i = 0; i < 8; i++) {
        //     var random = Math.random() * 0.2;
        //     Vue.set(po.possible, i, random.toFixed(2));
        // }
        // // drawRect();
        // setCon();
        // setImg();
    }, 0)
}, 1000)

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

// var isSpread1 = false;
// $('#1-100').on('click', function() {
//     if (!isSpread1) {
//         $.getJSON("json/bus_data.json", function(data) {
//             for (i = 0; i < 15; i++) {
//                 let li = document.createElement('li');
//                 let cont = document.createElement('div');
//                 cont.setAttribute('style', 'background: #060C20;');
//                 let a = document.createElement('a');
//                 a.setAttribute('href', 'info.html');
//                 a.setAttribute('target', '_blank');
//                 a.innerHTML = (i + 1) + "-" + data[0][i];
//                 cont.appendChild(a);
//                 li.appendChild(cont);
//                 document.getElementById('1-100-list').appendChild(li);
//             }
//         });
//         // let arry = new Array('-鄂A·JD343', '-鄂A·13495', '-鄂A·C6103', '-鄂A·C1180', '-鄂A·888U8', '-鄂A·H6213',
//         //     '-鄂A·E3923', '-鄂A·6265B', '-鄂A·A9965', '-鄂A·M5852', '-鄂A·54374', '-鄂A·15466', '-鄂A·B0888',
//         //     '-鄂A·V6600', '-鄂A·9P80M')

//     } else {
//         document.getElementById('1-100-list').innerHTML = "";
//     }
//     isSpread1 = !isSpread1;
// })

// var isSpread2 = false;
// $('#101-200').on('click', function() {
//     if (!isSpread2) {
//         $.getJSON("json/bus_data.json", function(data) {
//             for (i = 0; i < 15; i++) {
//                 let li = document.createElement('li');
//                 let cont = document.createElement('div');
//                 cont.setAttribute('style', 'background: #060C20;');
//                 let a = document.createElement('a');
//                 a.setAttribute('href', 'info.html');
//                 a.setAttribute('target', '_blank');
//                 a.innerHTML = (i + 1) + "-" + data[1][i];
//                 cont.appendChild(a);
//                 li.appendChild(cont);
//                 document.getElementById('101-200-list').appendChild(li);
//             }
//         });
//     } else {
//         document.getElementById('101-200-list').innerHTML = "";
//     }
//     isSpread2 = !isSpread2;
// })

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

//司机的信息在一次页面打开的过程中不会变动，只需请求一次
$.ajax({
    type: "POST",
    url: "/BusInfo/getBusInfo",
    data: '"鄂A·73788"',
    dataType: "json",
    contentType: "application/json",
    timeout: 5000, //连接超时时间
    success: function(data) { //成功则更新数据
        driver.roadId = data.roadId;
        driver.name = data.name;
        driver.busId = '"鄂A·73788"';
        phone.phone = data.phone;
    }
});

function pressureIsNormal(sPressure, dPressure) {
    return sPressure >= 90 && sPressure <= 140 && dPressure >= 60 && dPressure <= 90;
}

function spo2IsNormal(spo2) {
    return spo2 >= 94;
}

function heartRateIsNormal(heartRate) {
    return heartRate >= 60 && heartRate <= 100;
}

function temperatureIsNormal(temperature) {
    return temperature >= 36.1 && temperature <= 37.3;
}

function alcoholIsNormal(alcohol) {
    return alcohol >= 0 && alcohol <= 200;
}

var realPic = null
    //1s钟发送一次数据更新，请求该车最近的五个指标八个状态，以及图片名称
window.setInterval(() => {
    setTimeout(() => {
        $.ajax({
            type: "POST",
            url: "/BusInfo/getRealTime",
            data: '"鄂A·73788"',
            dataType: "json",
            contentType: "application/json",
            timeout: 5000, //连接超时时间
            success: function(data) { //成功则更新数据
                Vue.set(con.condition, 0, data.HeartRate);
                Vue.set(con.condition, 2, data.LowBloodPressure + "/" + data.HighBloodPressure);
                Vue.set(con.condition, 3, data.Temperature);
                Vue.set(con.condition, 4, data.BloodOxygen);
                //Vue.set(con.alcohol, 1, data.Alcohol);
                con.alcohol = data.Alcohol;
                Vue.set(con.color, 0, heartRateIsNormal(data.HeartRate) ? normalColor : abnormalColor);
                Vue.set(con.color, 2, pressureIsNormal(data.HighBloodPressure, data.LowBloodPressure) ? normalColor : abnormalColor);
                Vue.set(con.color, 3, temperatureIsNormal(data.Temperature) ? normalColor : abnormalColor);
                Vue.set(con.color, 4, spo2IsNormal(data.BloodOxygen) ? normalColor : abnormalColor);
                Vue.set(con.color, 5, alcoholIsNormal(data.Alcohol) ? normalColor : abnormalColor);
                Vue.set(po.possible, 0, data.Smoke);
                Vue.set(po.possible, 1, data.CloseEye);
                Vue.set(po.possible, 2, data.Yawn);
                Vue.set(po.possible, 3, data.UsingPhone);
                Vue.set(po.possible, 4, data.NoSafetyBelt);
                Vue.set(po.possible, 5, data.LookAround);
                Vue.set(po.possible, 6, data.LeavingSteering);
                Vue.set(po.possible, 7, data.Conflict);
                for (i = 0; i < 8; ++i)
                    Vue.set(po.color, i, (po.possible[i] < 0.5) ? normalColor : abnormalColor);
                realPic = data.realPic;
            }
        });

    }, 0)
}, 1000)

//1s钟发送一次数据更新，请求该车最近的图片流
window.setInterval(() => {
    setTimeout(() => {
        if (realPic != null) {
            $.ajax({
                type: "POST",
                url: "/BusInfo/getRealPic",
                data: "'" + realPic + "'",
                //dataType: "json",
                contentType: "application/json",
                timeout: 5000, //连接超时时间
                success: function(data) { //成功则更新数据
                    $("#img-real").attr("src", "data:image/jpg;base64," + data);
                }
            });
        }
    }, 0)
}, 1000)

//1s发送一次请求，请求该车最近的异常行为记录
window.setInterval(() => {
    setTimeout(() => {
        $.ajax({
            type: "POST",
            url: "/BusInfo/getRecord",
            data: '"鄂A·73788"',
            dataType: "json",
            contentType: "application/json",
            timeout: 5000, //连接超时时间
            success: function(data) { //成功则更新数据
                Vue.set(re.record, 0, data.Smoke);
                Vue.set(re.record, 1, data.CloseEye);
                Vue.set(re.record, 2, data.Yawn);
                Vue.set(re.record, 3, data.UsingPhone);
                Vue.set(re.record, 4, data.NoSafetyBelt);
                Vue.set(re.record, 5, data.LookAround);
                Vue.set(re.record, 6, data.LeavingSteering);
                Vue.set(re.record, 7, data.Conflict);
                for (i = 0; i < 8; ++i)
                    Vue.set(re.color, i, (re.record[i] <= 10) ? normalColor : abnormalColor);
            }
        });
    }, 0)
}, 1000)