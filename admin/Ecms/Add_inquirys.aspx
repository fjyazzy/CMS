<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Add_inquirys.aspx.vb" Inherits="Cms1.Add_inquirys" %>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>询价单管理</title>
    <link runat="server" id="lLink" rel="stylesheet" href="../../cmscss/home/style1.css" />
    <link rel="stylesheet" href="../../cmscss/Sdialog.css" />
    <script type="text/javascript" src="../../cmsjs/Sdialog.js"></script>
</head>	
<body>
		<form id="Form1" method="post" runat="server">
			<TABLE border="1" style="width:100%;">
				<TR>
					<TD>品名型号</TD>
					<TD colspan="3"><asp:textbox id="type" runat="server" Width="700px" CssClass="txt" Height="24px"></asp:textbox></TD>
				</TR>
				<TR>
					<TD>指定封装</TD>
					<TD><asp:textbox id="fz" runat="server" Width="300px" CssClass="txt" Height="24px"></asp:textbox></TD>
					<TD>客户姓名</TD>
					<TD><asp:textbox id="name" runat="server" Width="300px" CssClass="txt" Height="24px"></asp:textbox>(职位)</TD>
				</TR>
				<TR>
					<td>购买数量</td>
					<TD><asp:textbox id="sl" runat="server" Width="300px" CssClass="txt" Height="24px"></asp:textbox></TD>
					<TD>公司名称</TD>
					<TD><asp:textbox id="company" runat="server" Width="350px" CssClass="txt" Height="24px"></asp:textbox></TD>
				</TR>
				<tr>
					<TD>品牌要求</TD>
					<TD><asp:textbox id="manufactory" runat="server" Width="300px" CssClass="txt" Height="24px"></asp:textbox></TD>
					<TD>联系电话</TD>
					<TD><asp:textbox id="phone" runat="server" Width="350px" CssClass="txt" Height="24px"></asp:textbox></TD>
				</tr>
				<tr>
					<TD>年份批号</TD>
					<TD><asp:textbox id="ph" runat="server" Width="300px" CssClass="txt" Height="24px"></asp:textbox></TD>
					<TD>联系地址</TD>
					<TD><asp:textbox id="Address" runat="server" Width="380px" CssClass="txt" Height="24px"></asp:textbox></TD>
				</tr>
				<TR>
					<TD>报出价格</TD>
					<TD><asp:textbox id="bj" runat="server" Width="300px" CssClass="txt" Height="24px"></asp:textbox></TD>
					<TD>传真号码</TD>
					<TD><asp:textbox id="fax" runat="server" Width="309px" CssClass="txt" Height="24px"></asp:textbox></TD>
				</TR>
				<tr>
					<TD>接受价位</TD>
					<TD><asp:textbox id="jsj" runat="server" Width="300px" CssClass="txt" Height="24px"></asp:textbox></TD>
					<TD>电子邮件</TD>
					<TD><asp:textbox id="email" runat="server" Width="309px" CssClass="txt" Height="24px"></asp:textbox></TD>
				</tr>
				<TR>
					<TD>货源情况</TD>
					<TD colspan="3"><asp:TextBox id="hyqk" runat="server" Width="700px" Height="24px" style="OVERFLOW-Y:visible" TextMode="MultiLine"></asp:TextBox>(可换行)</TD>
                </TR>
				<TR>
					<TD>备注信息</TD>
					<TD colspan="3"><asp:textbox id="khbz" runat="server" Width="700px" Height="24px" style="OVERFLOW-Y:visible" TextMode="MultiLine"></asp:textbox>(可换行)</TD>
				</TR>
				<TR>
					<TD>重要程度</TD>
					<TD>
						<asp:dropdownlist id="zycd" runat="server">
							<asp:ListItem Value="询价">询价</asp:ListItem>
							<asp:ListItem Value="成交意向">成交意向</asp:ListItem>
							<asp:ListItem Value="一般">一般</asp:ListItem>
							<asp:ListItem Value="重要">重要</asp:ListItem>
							<asp:ListItem Value="很重要">很重要</asp:ListItem>
						</asp:dropdownlist></TD>
					<TD>客户网站</TD>
					<TD><asp:textbox id="ywbz" runat="server" Width="309px" CssClass="txt" Height="24px"></asp:textbox></TD>
				</TR>
				<TR>
					<TD>处理进度</TD>
					<TD colspan="3">
						<asp:dropdownlist id="Status" runat="server" Width="144px" AutoPostBack="True">
							<asp:ListItem Value="0">挂单</asp:ListItem>
							<asp:ListItem Value="1">跟单</asp:ListItem>
							<asp:ListItem Value="2">弃单</asp:ListItem>
							<asp:ListItem Value="3">成功交易</asp:ListItem>
						</asp:dropdownlist>
						<asp:button id="Button1" runat="server" Width="86px" Text="保存" CssClass="btn"></asp:button>
						<asp:Button id="Button2" runat="server" CssClass="btn" Text="生成定单" Width="86px"></asp:Button>
						<asp:Button id="Button3" runat="server" Width="100px" Text="克隆此询价单" CssClass="btn"></asp:Button>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>