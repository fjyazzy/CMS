<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="jxfw.aspx.vb" Inherits="Cms1.jxfw" %>
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
        	<TABLE class="tablexx" style="height:100%;" border="0">
				<tr>
					<td class="tdheader" colspan="3" style="height:60px;"><asp:label style="Z-INDEX: 0" id="Label2" runat="server"></asp:label></td>
				</tr>
                <tr>
					<td colspan="3" style="height:20px;text-align:center;"><asp:label style="Z-INDEX: 0" id="dhl" runat="server"></asp:label></td>
				</tr>

				<tr>
                    <td style="width:20%">&nbsp;</td>
					<td style="width:60%;text-align:left;">
                        <asp:Label ID="nr" runat="server" Text=""></asp:Label>
                    </td>
                    <td style="width:20%">&nbsp;</td>
				</tr>
				</TABLE>
		    <PAGEBottomHTML:BottomBAR id="bottombar1" DBOrd="4" runat="server"></PAGEBottomHTML:BottomBAR>

    </form>
</body>
</html>
