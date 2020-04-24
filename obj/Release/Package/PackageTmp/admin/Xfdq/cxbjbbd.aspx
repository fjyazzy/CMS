<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="cxbjbbd.aspx.vb" Inherits="Cms1.cxbjbbd" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>承修报价单</title>
    <style type="text/css">
        .auto-style1 {
            height: 14pt;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="1" style="border-collapse: collapse;width:500pt;text-align:left;width：667px">
            <tr style="height:14.25pt">
                <td colspan="7"> <h3 style="text-align:center;">变压器修理清单</h3></td>
            </tr>
            <tr style="height:14.25pt">
                <td colspan="7" >送修单号：<asp:Label ID="Lbsxdh" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr style="height:14.25pt">
                <td colspan="7" >送修单位：<asp:Label ID="Lbsxdw" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr style="height:14.25pt">
                <td colspan="7" >送修日期：<asp:Label ID="Lbsxrq" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="7" class="auto-style1" >铭牌数据：<asp:Label ID="Lbmpsj" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr style="height:14.25pt">
                <td colspan="7" >现状描述：<asp:Label ID="Lbxzms" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr style="height:29.25pt">
                <td>序号</td>
                <td>类型</td>
                <td>名称</td>
                <td>重/数量</td>
                <td>单价(元)</td>
                <td>金额（元）</td>
                <td>备注</td>
            </tr>

            <asp:Label ID="LBbgnr" runat="server" Text=""></asp:Label>

            <tr style="height:15.75pt">
                <td>&nbsp;</td>
                <td colspan="3">合计：<asp:Label ID="Lbzj1" runat="server" Text=""></asp:Label>
                </td>
                <td colspan="3">小写：<asp:Label ID="Lbzj2" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr style="height:14.25pt">
                <td colspan="7">重要备注:<asp:Label ID="Lbbz" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td colspan="5">福建雄风电气有限公司</td>
            </tr>

        </table>
    
    </div>
    </form>
</body>
</html>
