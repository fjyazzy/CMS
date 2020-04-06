<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="chgpw.aspx.vb" Inherits="Cms1.chgpw" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体"></FONT><FONT face="宋体"></FONT><FONT face="宋体"></FONT><FONT face="宋体">
			</FONT>
			<br>
			<TABLE id="Table1" bgcolor="#ffffff" cellSpacing="1" align="center" cellPadding="1" width="300"
				border="1">
				<tr class="myMenu2">
					<td colspan="2" height="40"><b>修改个人密码</b></td>
				</tr>
				<tr>
					<td>旧密码</td>
					<td>
						<asp:TextBox id="jmm" runat="server" Width="144px" TextMode="Password"></asp:TextBox></td>
				</tr>
				<tr>
					<td>新密码</td>
					<td>
						<asp:TextBox id="mm1" runat="server" TextMode="Password"></asp:TextBox></td>
				</tr>
				<tr>
					<td>重复新密码</td>
					<td>
						<asp:TextBox id="mm2" runat="server" TextMode="Password"></asp:TextBox></td>
				</tr>
				<tr>
					<td colSpan="2">
						<asp:Button id="Button1" runat="server" Text="保存修改"></asp:Button></td>
				</tr>
				<tr>
					<td colSpan="2">
						<asp:Label id="Label1" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label></td>
				</tr>
			</TABLE>
		</form>
</body>
</html>
