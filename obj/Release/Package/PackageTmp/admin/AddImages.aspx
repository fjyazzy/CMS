<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AddImages.aspx.vb" Inherits="Cms1.AddImages" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>添加图片</title>
<link runat="server" id="lLink" rel="stylesheet" href="../../cmscss/home/style1.css" />
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr><td>1.输入图片说明</td><td>
            <asp:TextBox ID="TxtImgName" runat="server" Width="296px"></asp:TextBox>
        </td></tr>
        <tr><td colspan="2">
            2.<input id="File1" runat="server" style="width: 402px;" name="File1"  title="上传图片" type="file" />
        </td></tr>
        <tr><td colspan="2">
            3.<asp:Button ID="Button1" runat="server" CssClass="txtbutton" Text="保存图片"   Width="95%" />
       </td></tr>
       </table>
    </form>
</body>
</html>
