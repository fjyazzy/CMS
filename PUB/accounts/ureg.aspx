<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ureg.aspx.vb" Inherits="Cms1.ureg" %>
<HTML>
	<HEAD>
		<title>注册窗口</title>
		<base target="_self">
		<META content="No-cache" http-equiv="Pragma">
		<META content="No-cache" http-equiv="Cache-Control">
		<META content="0" http-equiv="Expires">
		<LINK rel="stylesheet" href="/EduMis/includes/style.css">
			<script src="/EduMis/includes/function.js"></script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<h3 align="center"><BR>
				注册窗口</h3>
			<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="0" width="620" align="center">
				<tr>
					<td height="22" width="84">学号</td>
					<td height="22"><asp:textbox id="xsdm" runat="server" CssClass="txt" Width="137px" AutoPostBack="true"></asp:textbox></td>
				</tr>
				<tr>
					<td height="22" width="84"><FONT face="宋体">身份证号</FONT></td>
					<td height="22">
						<asp:textbox style="Z-INDEX: 0" id="sfzh" runat="server" Width="137px" CssClass="txt" AutoPostBack="true"></asp:textbox>
						<asp:label style="Z-INDEX: 0" id="Label1" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td height="22" width="84">密码</td>
					<td height="22"><asp:textbox style="Z-INDEX: 0" id="mm" runat="server" CssClass="txt" Width="137px" TextMode="Password"></asp:textbox></td>
				</tr>
				<tr>
					<td height="22" width="84"><FONT face="宋体">密码确认</FONT></td>
					<td height="22"><FONT face="宋体"><asp:textbox style="Z-INDEX: 0" id="mm2" runat="server" CssClass="txt" Width="137px" TextMode="Password"></asp:textbox></FONT></td>
				</tr>
				<tr>
					<td width="84">联系电话</td>
					<td><asp:textbox id="lxdh" runat="server" CssClass="txt" Width="137px"></asp:textbox></td>
				</tr>
				<tr>
					<td width="84">联系QQ</td>
					<td><asp:textbox id="QQ" runat="server" CssClass="txt" Width="137px"></asp:textbox></td>
				</tr>
				<tr>
					<td colSpan="2"><asp:button id="Button1" runat="server" Width="224px" Text="注册" style="Z-INDEX: 0"></asp:button></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
