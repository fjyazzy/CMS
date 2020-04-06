<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Cxbitem.aspx.vb" Inherits="Cms1.Cxbitem" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>承修报价单细项编辑</title>
    <link runat="server" id="lLink" rel="stylesheet" href="../../cmscss/home/style1.css" />
    <link rel="stylesheet" href="../../cmscss/Sdialog.css" />
    <script type="text/javascript" src="../../cmsjs/Sdialog.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table><tr>
            <td style="width:40%">
                <table>
            <tr>
                <td>单号</td>
                <td>
                    <asp:TextBox ID="Txtdh" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>类型</td>
                <td>
                    <asp:DropDownList ID="DDlx" runat="server" Width="192px" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>名称</td>
                <td>
                    <asp:TextBox ID="Txtmc" runat="server" Width="210px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style5">单位</td>
                <td class="auto-style6">
                    <asp:TextBox ID="Txtdw" runat="server" Width="141px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style5">数量</td>
                <td class="auto-style6">
                    <asp:TextBox ID="TxtSl" runat="server" Width="141px" AutoPostBack="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style5">单价</td>
                <td class="auto-style6">
                    <asp:TextBox ID="Txtdj" runat="server" Width="141px" AutoPostBack="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style5">金额</td>
                <td class="auto-style6">
                    <asp:TextBox ID="TxtJe" runat="server" Width="141px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style5">备注</td>
                <td class="auto-style6">
                    <asp:TextBox ID="Txtbz" runat="server" Width="228px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style5">&nbsp;</td>
                <td class="auto-style6">
                    <asp:Button ID="Button1" runat="server" Text="保存" Width="100%" />
                </td>
            </tr>
        </table>
            </td>
            <td style="width:60%">
                <table>
                    <tr><td>搜:<asp:TextBox ID="skey" runat="server" Width="239px" AutoPostBack="true"></asp:TextBox></td></tr>
                    <tr><td><asp:ListBox ID="LBcc" runat="server" Height="246px" Width="459px" AutoPostBack="true"></asp:ListBox></td></tr>
                </table>
            </td>
       </tr></table>
        
    </div>
    </form>
</body>
</html>
