<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Sales.aspx.vb" Inherits="Cms1.Sales" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table align="center" border="0" style="width:800px;text-align:center;">
        <tr><td colspan="2"><h3>交易管理系统</h3></td></tr>
        <tr><td style="width:100px;text-align:left;"><h4>询价单</h4></td><td style="text-align:left;">
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            </td></tr>
        <tr><td style="width:100px;text-align:left;"><h4>定单</h4></td><td style="text-align:left;">
            <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
            </td></tr>
    </table>
    </div>
    </form>
</body>
</html>
