<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Sales.aspx.vb" Inherits="Cms1.Sales" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>销售管理</title>
    <link runat="server" id="lLink" rel="stylesheet" href="../../cmscss/home/style1.css" />
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" style="width:100%;background-color:#eeeeee">
        <tr>
            <td style="width:60px;"><h3>询价单</h3></td>
            <td><asp:Label ID="Label1" runat="server" Text=""></asp:Label></td>
            <td style="width:60px;"><h3>定单</h3></td>
            <td><asp:Label ID="Label2" runat="server" Text=""></asp:Label></td>
            <td></td>
        </tr>
    </table>
    </form>
</body>
</html>
