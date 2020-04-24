<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ks3.aspx.vb" Inherits="Cms1.ks3" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:DropDownList ID="DDKSxm" runat="server" Width="200"></asp:DropDownList>
        <asp:DropDownList ID="DDBJM" runat="server" Width="200"></asp:DropDownList>
        <asp:DropDownList ID="DDKSH" runat="server" AutoPostBack="true" Width="231px"></asp:DropDownList>
        <asp:Button ID="Button2" runat="server" Text="删除考卷" />
        <br />
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Button ID="Button1" runat="server" Text="保存批改试卷结果" Height="43px" Width="476px" />
      </div></form>
</body>
</html>
