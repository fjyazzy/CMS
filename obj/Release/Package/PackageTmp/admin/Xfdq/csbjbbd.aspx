<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="csbjbbd.aspx.vb" Inherits="Cms1.csbjbbd" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>承试报价单</title>
</head>
<body>
    <form id="form1" runat="server">
    <table border="1" style="border-collapse:collapse;width:721pt" >
        <tr style="height:31.5pt">
            <td colspan="11"><h3 style="text-align:center;">福建雄风电气有限公司</h3></td>
        </tr>
        <tr style="height:26.25pt">
            <td colspan="11"><h4 style="text-align:center;">报 价 汇 总 单</h4></td>
        </tr>
        <tr>
            <td colspan="8">询价单位:<asp:Label ID="Lbxjdw" runat="server" Text="Label"></asp:Label>
            </td>
            <td colspan="3">报价编号:<asp:Label ID="Lbbjbh" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr style="height:14.25pt">
            <td colspan="8">工程名称：<asp:Label ID="Lbgcmc" runat="server" Text="Label"></asp:Label>
            </td>
            <td colspan="3">报价日期:<asp:Label ID="Lbbjrq" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="8">联系人:<asp:Label ID="Lblxr" runat="server" Text="Label"></asp:Label>
            </td>
            <td colspan="3">电话传真:<asp:Label ID="Lbdhcz" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr style="height:28.5pt">
            <td>项目</td>
            <td>类别</td>
            <td>人员名称</td>
            <td>设备名称</td>
            <td>配件名称</td>
            <td>其他</td>
            <td>单位</td>
            <td>数量</td>
            <td>单价</td>
            <td>金额</td>
            <td>备注</td>
        </tr>
         <asp:Label ID="Lbbgnr" runat="server" Text=""></asp:Label>
        <tr>
            <td class="auto-style1"></td>
            <td class="auto-style1">　</td>
            <td colspan="7" class="auto-style1">报价总合计：<asp:Label ID="Lbbjhj1" runat="server" Text=""></asp:Label></td>
            <td colspan="3" class="auto-style1"><asp:Label ID="Lbbjhj2" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td height="58" width="72">报价说明</td>
            <td colspan="10">
                1.报价依据为本次所收到的资料；若材料可调改，价格可以再商榷。<br />
                2.其余不详之处请再洽商<br />
                3.本报价含税含运费，不含现场费用和现场运卸费用
            </td>
        </tr>
        <tr style="height:14.25pt">
            <td>　</td>
            <td colspan="2">报价：<asp:Label ID="Lbbj" runat="server" Text=""></asp:Label>　</td>
            <td colspan="4">报价单有效期：<asp:Label ID="Lbyxq" runat="server" Text=""></asp:Label></td>
            <td>　</td>
            <td>销售经理：<asp:Label ID="Lbxsjl" runat="server" Text=""></asp:Label></td>
            <td>　</td>
            <td>　</td>
        </tr>
    </table>
    </form>
</body>
</html>