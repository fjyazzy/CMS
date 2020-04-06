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
		<table border="0" cellSpacing="0" cellPadding="0" width="100%" height="100%">
			<tr>
				<td colSpan="2" height="200" class="menu" ></td>
			</tr>
			<tr>
				<td height="100%" vAlign="top" colSpan="2" align="center">
						<table border="0" width="100%">
			            <tr>
				        <td colspan="2" valign="top">
					    <asp:Label id="Label1" runat="server"></asp:Label>
				        </td>
			            </tr>
		                </table>
                </td>
			</tr>
		</table>
	</body>
</HTML>
