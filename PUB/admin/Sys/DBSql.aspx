<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DBSql.aspx.vb" Inherits="Cms1.DBSql" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>数据库执行工具</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width:600px">
            <tr>
                <td>
                    <asp:DropDownList ID="DDDBord" runat="server" style="width:100%">
                        <asp:ListItem Value="1">主数据库（cmis1）</asp:ListItem>
                        <asp:ListItem Value="2">移动端页面数据库(WXCMS)</asp:ListItem>
                        <asp:ListItem Value="3">考试系统数据库（Exam）</asp:ListItem>
                        <asp:ListItem Value="4">雄风电气报价数据库（Xfdq）</asp:ListItem>
                        <asp:ListItem Value="5">网聊系统数据库(LoveU)</asp:ListItem>
                        <asp:ListItem Value="6">电子产品数据库(Ecms)</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="TxtSQL" runat="server" style="width:100%;height:200px;" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Excute sql" style="width:100%" />
                </td>
            </tr>
        </table>
        <asp:Label ID="Label1" runat="server"></asp:Label>
        <br />
    
    </div>
    </form>
</body>
</html>
