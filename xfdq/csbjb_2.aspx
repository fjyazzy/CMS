<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="csbjb_2.aspx.vb" Inherits="Cms1.csbjb_2" %>
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
                <td style="text-align:center;height:20px;" colspan="2">
					    <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
					</td>
                <tr>
					<td style="text-align:center;height:30px;" colspan="2"><h3>步骤一：计算劳务工资</h3></td>
				</tr>
				<tr><td style="width:30%">&nbsp;</td>
					<td>
                        <table style="width:50%">
                            <tr>
                                <td>选择人员:</td>
                                <td><asp:TextBox ID="xjdw" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align:center;">
                                    <asp:Button ID="Button1" runat="server" Text="下一步" Width="135px" />
                                </td>
                            </tr>
                        </table>
                    </td>
				</tr>
				</TABLE>
		    <PAGEBottomHTML:BottomBAR id="bottombar1" DBOrd="4" runat="server"></PAGEBottomHTML:BottomBAR>

    </form>
</body>
</html>