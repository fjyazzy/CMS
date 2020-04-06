<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Additems.aspx.vb" Inherits="Cms1.Additems" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>添加项目</title>
<link runat="server" id="lLink" rel="stylesheet" href="../../cmscss/home/style1.css" />

</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr><td>
           项目号：<asp:TextBox ID="itemno" runat="server" Width="355px"></asp:TextBox>
        </td></tr>
        <tr><td>
           项目名：<asp:TextBox ID="itemname" runat="server" Width="355px"></asp:TextBox>
        </td></tr>
        <tr><td>
           项目内容：<asp:TextBox ID="itemtext" runat="server" Width="355px"></asp:TextBox>
        </td></tr>
        <tr><td>
            <asp:Button ID="Button1" runat="server" CssClass="txtbutton" Text="保存项目"   Width="98%" />
       </td></tr>
       </table>
    </form>
</body>
</html>