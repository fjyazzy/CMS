<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="login.aspx.vb" Inherits="Cms1.login" %>
<%@ Register tagPrefix="LoginApi" tagname="loginbar" src="../components/LoginApi.ascx" %>
<%@ Register tagPrefix="PageBottomhtml" tagname="Bottombar" src="../components/pagebottom.ascx" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title runat="server" id="PageTitle">管理系统</title>
    <LINK rel="stylesheet" href="../cmscss/style.css">
	<script src="../cmsjs/commjs.js"></script>
</head>
<body style="margin:0px auto;background:url(../images/home/bj.jpg) top center no-repeat; background-size:cover;">
    <form id="form1" runat="server">
        	<TABLE class="tablexx" style="height:100%;">
				<tr>
					<td class="tdheader" style="height:60px;"><asp:label style="Z-INDEX: 0" id="Label2" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td align="center"><LoginApi:LoginBAR id="lBar" runat="server"></LoginApi:LoginBAR></td>
				</tr>
				<tr>
					<td>&nbsp;</td>
				</tr>
				</TABLE>
		    <PAGEBottomHTML:BottomBAR id="bottombar1" DBOrd="1" runat="server"></PAGEBottomHTML:BottomBAR>

    </form>
</body>
</html>
