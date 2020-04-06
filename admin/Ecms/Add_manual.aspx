<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Add_manual.aspx.vb" Inherits="Cms1.Add_manual" %>
<html>
	<HEAD>
		<title>资料管理</title>
		<base target="_self">
		<META content="No-cache" http-equiv="Pragma">
		<META content="No-cache" http-equiv="Cache-Control">
		<META content="0" http-equiv="Expires">
		<LINK runat="server" id="lLink" rel="stylesheet" href="../../includes/style.css" />
			<script src="../../includes/function.js"></script>
	    <style type="text/css">
            .auto-style1 {
                height: 23px;
            }
        </style>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE style="border-collapse:collapse" cellpadding=2 border="1" borderColor= "#cccc99" bgcolor="#ECF5FF" width="100%">
				<TR>
					<TD height="23" width="48">型号</TD>
					<TD height="23" colSpan="3"><asp:textbox style="OVERFLOW-Y: visible" id="Type" runat="server" CssClass="txt" TextMode="MultiLine"
							Width="728px"></asp:textbox></TD>
				</TR>
				<TR>
					<TD height="23" width="48">说明</TD>
					<TD height="23" colSpan="3"><asp:textbox style="OVERFLOW-Y: visible" id="shortdesc" runat="server" TextMode="MultiLine" Width="472px"></asp:textbox></TD>
				</TR>
				<TR>
					<TD height="23" width="48">厂商</TD>
					<TD height="23"><asp:textbox id="manufactory" runat="server" CssClass="txt" Width="280px"></asp:textbox></TD>
					<TD height="23" width="48">目录</TD>
					<TD height="23"><asp:dropdownlist id="categoryid" runat="server" CssClass="txt" Width="152px"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD width="48" class="auto-style1">积分</TD>
					<TD class="auto-style1"><asp:textbox id="point" runat="server" CssClass="txt" Width="136px">0</asp:textbox></TD>
					<TD class="auto-style1">点击数</TD>
					<TD class="auto-style1"><asp:textbox id="clicknum" runat="server" CssClass="txt" Width="112px">0</asp:textbox></TD>
				</TR>
				<TR>
					<TD height="23" width="48">&nbsp;</TD>
					<TD height="23" colSpan="3"><asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                        <input id="File1" title="上传图片" size="25" type="file" name="File1" runat="server" /></TD>
				</TR>
				<TR>
					<TD height="25" width="48"><FONT face="宋体">&nbsp;</FONT></TD>
					<TD height="25" colSpan="3"><asp:button id="Button1" runat="server" CssClass="btn" Width="81px" Text="保存"></asp:button><asp:button id="Button3" runat="server" CssClass="btn" Width="81px" Text="克隆"></asp:button></TD>
				</TR>
				</TABLE>
		</form>
	</body>
</HTML>

