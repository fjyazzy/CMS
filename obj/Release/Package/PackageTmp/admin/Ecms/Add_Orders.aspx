<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Add_Orders.aspx.vb" Inherits="Cms1.Add_Orders" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>订单管理</title>
    <link runat="server" id="lLink" rel="stylesheet" href="../../cmscss/home/style1.css" />
    <link rel="stylesheet" href="../../cmscss/Sdialog.css" />
    <script type="text/javascript" src="../../cmsjs/Sdialog.js"></script>
</head>	
	<body>
		<form id="Form1" method="post" runat="server">
			<table style="width:100%;" border="1">
				<tr>
					<td>客户姓名 <strong><font color="red">*</strong></font></td>
					<td><asp:textbox id="xm" runat="server" Width="260px" CssClass="txt"></asp:textbox>(职位)</td>
					<td>个人手机</td>
					<td><asp:textbox id="mobile" runat="server" Width="260px" CssClass="txt"></asp:textbox></td>
				</tr>
				<tr>
					<td>办公电话 <strong><font color="red">*</strong></font></td>
					<td><asp:textbox id="phone" runat="server" Width="260px" CssClass="txt"></asp:textbox></td>					
                                        <td>传真号码</td>
					<td><asp:textbox id="fax" runat="server" Width="260px" CssClass="txt"></asp:textbox></td>
			        </tr>
				<tr>
					<td>公司名称</td>
					<td><asp:textbox id="company" runat="server" CssClass="txt" Width="300px"></asp:textbox></td>
					<td><asp:Label id="Label2" runat="server" Visible="False">填单员</asp:Label>填单人员</td>
					<td><asp:TextBox id="tdy" runat="server" Width="192px"  CssClass="txt" Visible="False"></asp:TextBox></td>
				</tr>
				<tr>
                <td>收货地址 <strong><font color="red">*</strong></font></td>
					<td colspan="3"><asp:textbox id="Address" runat="server" Width="600px" CssClass="txt" style="OVERFLOW-Y:visible" TextMode="MultiLine"></asp:textbox>(可换行)</td>
				</tr>
				<tr>
					<td>货物信息 <strong><font color="red">*</strong></font></td>
					<td colspan="3"><asp:textbox id="bz" runat="server" Width="600px" CssClass="txt" style="OVERFLOW-Y:visible" TextMode="MultiLine"></asp:textbox>(可换行)</td>
				</tr>
				<tr>
					<td>备注信息</td>
					<td colspan="3"><asp:textbox id="fhfy" runat="server" style="OVERFLOW-Y:visible" TextMode="MultiLine" CssClass="txt" Width="600px"></asp:textbox>(可换行)</td>
                </tr>
				<tr>
					<td>进帐金额 <strong><font color="red">*</strong></font></td>
					<td><asp:textbox id="jzje" runat="server" Width="192px" CssClass="txt"></asp:textbox></td>
					<td>汇款日期</td>
					<td><asp:textbox id="hkrq" runat="server" Width="192px" CssClass="txt"></asp:textbox></td>
				</tr>
				<tr>
					<td>采购金额 <strong><font color="red">*</strong></font></td>
					<td><asp:textbox id="cgje" runat="server" Width="192px" CssClass="txt">0</asp:textbox></td>
					<td>发货时间</td>
					<td><asp:textbox id="fhsj" runat="server" CssClass="txt" Width="192px"></asp:textbox></td>
				</tr>
				<tr>
					<td>发货方式 <strong><font color="red">*</strong></font></td>
					<td><asp:DropDownList id="fhfs" runat="server" Width="192px" CssClass="txt" ></asp:DropDownList></td>
					<td><font color="red">运单号码</font></td>
					<td><asp:textbox id="ydh" runat="server" Width="192px" CssClass="txt"></asp:textbox></td>
				</tr>
				<tr>
					<td>进款帐户 <strong><font color="red">*</strong></td>
					<td><asp:DropDownList id="hklx" runat="server" Width="192px" CssClass="txt" ></asp:DropDownList></td>
					<td>运费结算 <strong><font color="red">*</strong></font></td>
					<td><asp:textbox id="yhjsfs" runat="server" Width="20px" CssClass="txt"></asp:textbox>(注：0为本公司付费,1为对方付费)</td>
				</tr>
				<tr>
					<td>税率及其他</td>
					<td><asp:textbox id="Tax" runat="server" CssClass="txt" Width="192px">0</asp:textbox>           
              <strong><font color="red"><asp:Label id="Label1" runat="server"></asp:Label></strong></font></td>
					<td>快递运费 <strong><font color="red">*</strong></font></td>
					<td><asp:textbox id="yf" runat="server" CssClass="txt" Width="192px">0</asp:textbox></td>
				</tr>
				<tr>
					<td>业务单状态</td>
                    <td colspan="3"><asp:dropdownlist  id="Status" runat="server" Width="144px" AutoPostBack="true">
							<asp:ListItem Value="0">已发货</asp:ListItem>
							<asp:ListItem Value="1">未到帐</asp:ListItem>
							<asp:ListItem Value="2">欠款单</asp:ListItem>
							<asp:ListItem Value="3">退款单</asp:ListItem>
							<asp:ListItem Value="4">已结算</asp:ListItem>
							<asp:ListItem Value="5">已到帐</asp:ListItem>
						</asp:dropdownlist>
						<asp:button id="Button1" runat="server" CssClass="btn" Width="96px" Text="保存"></asp:button>
						<asp:Button id="Button2" runat="server" CssClass="btn" Width="96px" Text="克隆此业务单"></asp:Button>
						<asp:Button id="Button3" runat="server" CssClass="btn" Width="96px" Text="打印快递单"></asp:Button></td>
				</tr>
			</table>
		</form>
	</body>
</html>
