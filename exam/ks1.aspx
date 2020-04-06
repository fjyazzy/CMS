<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ks1.aspx.vb" Inherits="Cms1.ks1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link runat="server" id="lLink" rel="stylesheet" href="../cmscss/home/style1.css" />
</head>
<body>
    <form id="form1" runat="server">

<table>
    <tr>
        <td>&nbsp;</td>
        <td>
            <asp:Label ID="Label1" runat="server" Text="考试系统"></asp:Label>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>
            <table>
                <tr>
                    <td>考试科目</td>
                    <td>
                        <asp:DropDownList ID="DDKSxm" runat="server" Width="200">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>班级</td>
                    <td>
                        <asp:DropDownList ID="DDBJM" runat="server" Width="200">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>姓名</td>
                    <td>
                        <asp:TextBox ID="xm" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>座号</td>
                    <td>
                       <asp:DropDownList ID="DDzh" runat="server" Width="200"></asp:DropDownList>
                    </td>
                </tr>
            </table>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>
            <asp:Button ID="Button1" runat="server" Text="注册考试" ToolTip="点击这可开始考试" />
            <asp:Button ID="Button2" runat="server" Text="模拟考试" ToolTip="点击这可开始考试" />
        </td>
        <td>&nbsp;</td>
    </tr>

</table>
        <asp:Label ID="Label2" runat="server"></asp:Label>
    </form>
</body>
</html>