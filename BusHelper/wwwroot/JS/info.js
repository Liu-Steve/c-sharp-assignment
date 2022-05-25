let slide_img_rotate = false;
$('.btn').on('click', function() {
    $('.sidebar').toggleClass('side');
    let slide_img = document.getElementById('slide-img')
    if (!slide_img_rotate)
        slide_img.setAttribute('style', 'transform:rotate(180deg)');
    else
        slide_img.removeAttribute('style');
    slide_img_rotate = !slide_img_rotate;
})
var isSpread1 = false;
$('#1-100').on('click', function() {
    if (!isSpread1) {
        $.getJSON("json/bus_data.json", function(data) {
            for (i = 0; i < 15; i++) {

                let li = document.createElement('li');
                let cont = document.createElement('div');
                cont.setAttribute('style', 'background: #060C20;');
                let p = document.createElement('p');
                p.innerHTML = (i + 1) + "-" + data[0][i];
                cont.appendChild(p);
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
                let p = document.createElement('p');
                p.innerHTML = (i + 1) + "-" + data[1][i];
                cont.appendChild(p);
                li.appendChild(cont);
                document.getElementById('101-200-list').appendChild(li);
            }
        });
    } else {
        document.getElementById('101-200-list').innerHTML = "";
    }
    isSpread2 = !isSpread2;
})