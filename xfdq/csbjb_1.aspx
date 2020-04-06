<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="csbjb_1.aspx.vb" Inherits="Cms1.csbjb_1" %>
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
        	<TABLE class="tablexx" style="height:100%;"  border="0">
				<tr>
					<td class="tdheader" colspan="2" style="height:60px;"><asp:label style="Z-INDEX: 0" id="Label2" runat="server"></asp:label></td>
				</tr>
                <tr>
					<td style="text-align:center;height:100px;" colspan="2"><h3>承试报价表</h3></td>
				</tr>
				<tr><td style="width:30%">&nbsp;</td>
                    <td >
                        <table style="width:50%">
                            <tr>
                                <td>询价单位:</td>
                                <td><asp:TextBox ID="xjdw" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>报价编号:</td>
                                <td><asp:TextBox ID="csbjbh" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>工程名称:</td>
                                <td><asp:TextBox ID="gcmc" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>报价日期:</td>
                                <td><asp:TextBox ID="bjrq" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>联系人:</td>
                                <td><asp:TextBox ID="lxr" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>电话传真:</td>
                                <td><asp:TextBox ID="dhcz" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align:center;">
                                    <asp:Button ID="Button1" runat="server" Text="开始制作承试报价表" Width="135px" />
                                </td>
                            </tr>

                        </table>
                    </td>
				</tr>
				<tr>
					<td>&nbsp;</td>
                    <td>&nbsp;</td>
				</tr>
				</TABLE>
		    <PAGEBottomHTML:BottomBAR id="bottombar1" DBOrd="4" runat="server"></PAGEBottomHTML:BottomBAR>

    </form>
</body>
</html>