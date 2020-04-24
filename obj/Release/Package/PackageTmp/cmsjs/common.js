function x() {
    alert("测试本文件是否可用！");
}
function openWin(url, text, winInfo) {
    var winObj = window.open(url, text, winInfo);
    var loop = setInterval(function (){
        if (winObj.closed){
            clearInterval(loop);
            //alert('closed')
            parent.location.reload();
            //opener.location.reload();
        }
    },1);
}