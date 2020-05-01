function changeFsize() {
    var a = window.frameElement.id;
    if (a == "mainx") {
        top.document.all('Contentx').rows = '99%,1%,*'
    };
}

/*
弹出窗口调用过程
Sdialog.js中的showDetail在类中Commonclass中被函数ShowDialog调用
showDialog函数被类TableListCS和组件TableAlldata调用。
TableListCS被组件table_MX调用

Sdialog.js-->类commonclass-->类tableListCs--->组件table_mx

*/

function showDetail(fn, bt, w, h) {
    //背景
    var bgObj = document.getElementById("bgDiv");
    bgObj.style.width = document.body.offsetWidth + "px";
    bgObj.style.height = screen.height + "px";

    //定义窗口
    var msgObj = document.getElementById("msgDiv");
    msgObj.style.marginTop = -75 + document.documentElement.scrollTop + "px";
    msgObj.style.width = w + "px";
    msgObj.style.height = h + "px";

    //让主窗口可拖动
    msgObj.onmousedown = function (event) {
        msgObj.style.cursor = "move";

        var ev = event || window.event;
        event.stopPropagation();
        var disX = ev.clientX - msgObj.offsetLeft;
        var disY = ev.clientY - msgObj.offsetTop - 70;
        document.onmousemove = function (event) {
            var ev = event || window.event;
            msgObj.style.left = ev.clientX - disX + "px";
            msgObj.style.top = ev.clientY - disY + "px";
        };
    };
    msgObj.onmouseup = function () {
        document.onmousemove = null;
        this.style.cursor = "default";
    };


    //关闭
    document.getElementById("msgShut").onclick = function () {
        bgObj.style.display = msgObj.style.display = "none";
    }
    msgObj.style.display = bgObj.style.display = "block";

    //设置文本内容
    var msgDetail = document.getElementById("msgDetail");
    msgDetail.innerHTML = "<p align=center>" + bt + "</p><p align=center><iframe src='" + fn + "' name=top1 height=" + (h-80) + " width=98% id='myiframe'></iframe></p>";

}
