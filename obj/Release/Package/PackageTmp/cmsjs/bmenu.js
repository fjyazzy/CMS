function bb(va)
{  
    if (va == 13) {
        var aa = document.form1.Skey.value;
        var bb = '';
        var kssj = document.form1.kssj.value;
        var jssj = document.form1.jssj.value;
        var o = document.getElementsByTagName("input");
        for (var i = 0; i < o.length; i++) {
            if (o[i].type == "checkbox" && o[i].checked) {
                bb = bb + o[i].value + '|';
            }
        }
        window.parent.mainx.location.href = 'SearchResult.aspx?Skey=' + escape(aa) + '&ck=' + bb + '&kssj=' + kssj + '&jssj=' + jssj;
    }
}

function ShowHideDiv(DivId)
{
    if(document.all[DivId].style.display=='none'){ 
        document.all[DivId].style.display='';
    }
    else{ 
        document.all[DivId].style.display='none';
    }
    return 0;
}
