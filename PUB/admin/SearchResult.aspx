<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SearchResult.aspx.vb" Inherits="Cms1.SearchResult" %>
<HTML>
	<HEAD>
		<title>公共服务检索</title>
		<LINK runat="server" id="lLink" rel="stylesheet" href="../cmscss/style.css" />
        <link rel="stylesheet" href="../cmscss/Sdialog.css" />
        <script type="text/javascript" src="../cmsjs/Sdialog.js"></script>
	</HEAD>
<body style="background:url(../images/home/bj2.jpg) top center no-repeat; background-size:cover;">
    <form id="form2" runat="server">
        <table style="WORD-BREAK: break-all" width="100%" height="90%">
            <tr>
                <td style="text-align:center;">
                    <asp:TextBox ID="skey" runat="server" Width="423px"></asp:TextBox><asp:CheckBox ID="CK1" runat="server" />精确
                    <asp:Button ID="Button1" runat="server" Text="站内搜索" />
                </td>
            </tr>
            <tr><td style="background-color:#ffffff;"><asp:label style="Z-INDEX: 0; LINE-HEIGHT: 150%" id="Label1" runat="server"></asp:label></td></tr>
        </table>
        <div class="footer"><br/>
        </div>
    </form>
</body>
</HTML>