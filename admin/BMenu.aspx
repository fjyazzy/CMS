<%@ Page Language="vb" AutoEventWireup="false" Codebehind="BMenu.aspx.vb" Inherits="Cms1.BMenu"%>
<HTML>
	<HEAD>
		<title>Menu</title>
		<base target="_self">
		<META content="No-cache" http-equiv="Pragma">
		<META content="No-cache" http-equiv="Cache-Control">
		<META content="0" http-equiv="Expires">
		<link runat="server" id="lLink" rel="stylesheet" href="../cmscss/home/style1.css" />
		<script type="text/javascript" src="../cmsjs/bmenu.js"></script>
	</HEAD>
	<body onkeydown="bb(event.keyCode)">
        <form id="form1" runat="server">
		<table border="0" cellSpacing="0" cellPadding="0" width="100%" height="100%">
			<tr><td class="gndh" ></td></tr>
            <tr><td class="myMenu" height="1"></td></tr>
			<tr><td valign="top"><asp:Label id="Label1" runat="server"></asp:Label></td></tr>
			<tr><td height="70%;"></td></tr>
	    </table>
        </form>
	</body>
</HTML>
