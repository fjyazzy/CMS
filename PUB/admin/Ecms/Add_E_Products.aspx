<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Add_E_Products.aspx.vb" Inherits="Cms1.Add_E_Products" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>单个产品信息管理</title>
    <link runat="server" id="lLink" rel="stylesheet" href="../../cmscss/home/style1.css" />
    <link rel="stylesheet" href="../../cmscss/Sdialog.css" />
    <script type="text/javascript" src="../../cmsjs/Sdialog.js"></script>
</head>
<body>
		<form id="Form1" method="post" runat="server">
			<table border="1">
				<tr>
					<td><strong>目录</strong></td>
					<td colspan="3"><asp:dropdownlist id="categoryid" runat="server" CssClass="txt" Width="168px" style="Z-INDEX: 0"></asp:dropdownlist></td>
					<td colspan="3" rowspan="5">
						<asp:panel id="Panel1" runat="server" Width="100px" Height="100px" BorderStyle="Solid" BorderWidth="1px"
							BackColor="White">
							<asp:Label id="Label2" runat="server">No Pic</asp:Label>
						</asp:panel><input id="File1" title="上传图片" size="25" type="file" name="File1" runat="server" /></td>
				</tr>
				<tr>
					<td><strong style="Z-INDEX: 0">型号</strong></td>
					<td><strong><asp:textbox id="Type" runat="server" Width="262px" style="Z-INDEX: 0"></asp:textbox></strong></td>
					<td><strong style="Z-INDEX: 0">货物品质</strong></td>
					<td>
						<asp:dropdownlist id="Hwpz" runat="server" CssClass="txt" Width="168px">
							<asp:ListItem Value="全新原装">全新原装</asp:ListItem>
							<asp:ListItem Value="全新散装">全新散装</asp:ListItem>
							<asp:ListItem Value="翻新旧货">翻新旧货</asp:ListItem>
							<asp:ListItem Value="原字旧货">原字旧货</asp:ListItem>
							<asp:ListItem Value="拆机旧货">拆机旧货</asp:ListItem>
							<asp:ListItem Value="新货打字">新货打字</asp:ListItem>
							<asp:ListItem Value="旧货打字">旧货打字</asp:ListItem>
						</asp:dropdownlist></td>

				</tr>
                    <tr>
					<td><strong>厂商</strong></td>
					<td>
						<asp:textbox id="manufactory" runat="server" Width="262px"></asp:textbox>
						</td>
					<td><strong style="Z-INDEX: 0">库存数量</strong></td>
					<td>
						<asp:textbox id="sl" runat="server" Width="105px"></asp:textbox>
						<asp:dropdownlist id="sl_a" runat="server" CssClass="txt" Width="56px">
							<asp:ListItem Value="盒">盒</asp:ListItem>
							<asp:ListItem Value="个">个</asp:ListItem>
							<asp:ListItem Value="管">管</asp:ListItem>
							<asp:ListItem Value="箱">箱</asp:ListItem>
							<asp:ListItem Value="件">件</asp:ListItem>
							<asp:ListItem Value="盘">盘</asp:ListItem>
							<asp:ListItem Value="包">包</asp:ListItem>
						</asp:dropdownlist></td>

				</tr>
				<tr>
					<td><strong style="Z-INDEX: 0">封装</strong></td>
					<td><strong style="Z-INDEX: 0"><asp:textbox id="fz" runat="server" Width="262px" style="Z-INDEX: 0"></asp:textbox>
							</strong></td>
					<td><strong style="Z-INDEX: 0">采购价格</strong></td>
					<td>
						<strong><asp:textbox id="price" runat="server" Width="119px" CssClass="auto-style6"></asp:textbox></strong></td>

				</tr>
				<tr>
					<td><strong>批号</strong></td>
					<td>
						<asp:textbox id="ph" runat="server" Width="262px"></asp:textbox></td>
					<td><strong style="Z-INDEX: 0">批发价格</strong></td>
					<td>
						<asp:textbox id="pfjg" runat="server" Width="120px"></asp:textbox>
					</td>
				</tr>
				<tr>
					<td><strong>最小包装</strong></td>
					<td>
						<asp:textbox id="qssl" runat="server" Width="262px"></asp:textbox>
						<asp:dropdownlist id="qssl_a" runat="server" CssClass="txt" Width="56px">
							<asp:ListItem Value="盒">盒</asp:ListItem>
							<asp:ListItem Value="个">个</asp:ListItem>
							<asp:ListItem Value="管">管</asp:ListItem>
							<asp:ListItem Value="箱">箱</asp:ListItem>
							<asp:ListItem Value="件">件</asp:ListItem>
							<asp:ListItem Value="盘">盘</asp:ListItem>
							<asp:ListItem Value="包">包</asp:ListItem>
						</asp:dropdownlist></td>
					<td><strong>零售价格</strong></td>
					<td>
						<asp:textbox id="lsjg" runat="server" Width="180px"></asp:textbox>
						</td>
					<td colspan="3"><asp:button id="Button2" runat="server" CssClass="btn" Width="78px" Text="保存"></asp:button>&nbsp;
						<asp:button id="Button5" tabIndex="1" runat="server" CssClass="btn" Width="78px" Text="克隆此库存"></asp:button></td>
				</tr>
				<tr>
					<td><strong>供应商信息</strong></td>
					<td colspan="6">
						<asp:textbox style="OVERFLOW-Y: visible" id="Cgxx" runat="server" Width="800px" TextMode="MultiLine"></asp:textbox>(可换行)</td>
				</tr>
				<tr>
					<td><strong>采购记录</strong></td>
					<td colspan="6">
						<asp:textbox style="OVERFLOW-Y: visible" id="Cgjl" runat="server" Width="800px" TextMode="MultiLine"></asp:textbox>(可换行)</td>
				</tr>
				<tr>
					<td><strong>简要说明</strong></td>
					<td colspan="6">
						<asp:textbox id="shortdesc" runat="server" Width="800px"></asp:textbox></td>
				</tr>
				<tr>
					<td><strong>产品说明</strong></td>
					<td colspan="6">
						<asp:textbox style="OVERFLOW-Y: visible" id="Productdesc" runat="server" Width="800px" TextMode="MultiLine"></asp:textbox>(可换行)</td>
				</tr>
				<tr>
					<td><strong>应用说明</strong></td>
					<td colspan="6">
						<asp:textbox id="shortdesc2" runat="server" Width="800px"></asp:textbox>(可换行)</td>
				</tr>
				<tr>
					<td><strong><strong style="Z-INDEX: 0">首次录入人</strong></strong></td>
					<td>
							<asp:textbox id="ywy_2" runat="server" Width="120px" ReadOnly="true" BackColor="#FFE0C0"></asp:textbox></td>
					<td><strong>首次录入时间</strong></td>
					<td>
						<asp:textbox id="uptime_2" runat="server" Width="189px" ReadOnly="true" BackColor="#FFE0C0" CssClass="auto-style6"></asp:textbox></td>
					<td><strong style="Z-INDEX: 0">点击数</strong></td>
					<td  colspan="2" class="auto-style2">
						<asp:textbox id="C_NUMS" runat="server" Width="120px"></asp:textbox></td>
				</tr>
				<tr>
					<td><strong style="Z-INDEX: 0">最后编辑人</strong></td>
					<td>
						<strong>
							<asp:textbox id="Ywy" runat="server" Width="120px" ReadOnly="true" BackColor="#FFE0C0"></asp:textbox></strong></td>
					<td><strong>编辑时间</strong></td>
					<td>
						<asp:textbox id="uptime" runat="server" Width="168px" ReadOnly="true" BackColor="#FFE0C0"></asp:textbox></td>
					<td><strong>备注</strong></td>
					<td  colspan="2">
						<asp:textbox id="Ghsxx" runat="server" Width="120px" Height="20px"></asp:textbox></td>
				</tr>
				<tr>
					<td><strong>相关资料信息</strong></td>
					<td colspan="6"><asp:label id="Label1" runat="server"></asp:label></td>
				</tr>
			</table>
		</form>
	</body>
</html>
