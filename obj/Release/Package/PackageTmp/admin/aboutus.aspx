<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="aboutus.aspx.vb" Inherits="Cms1.aboutus" %>
<%@ Register tagPrefix="PageBottomhtml" tagname="Bottombar" src="../components/pagebottom.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>关于我们</title>
    <link runat="server" id="lLink" rel="stylesheet" href="../cmscss/style.css" />
</head>
<body style="background:url(../images/home/bj2.jpg) top center no-repeat; background-size:cover;">
    <form id="form1" runat="server">
        <div class="footer"><br/>
        <PAGEBottomHTML:BottomBAR id="bottombar1" DBOrd="1" runat="server"></PAGEBottomHTML:BottomBAR>
        </div>
    </form>
</body>
</html>
