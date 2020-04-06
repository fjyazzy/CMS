<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="score.aspx.vb" Inherits="Cms1.score" %>
<%@ Register tagPrefix="TableList" tagname="Tlistdata" src="../../components/Table_MX.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link runat="server" id="lLink" rel="stylesheet" href="../../cmscss/home/style1.css" />
    <link rel="stylesheet" href="../../cmscss/Sdialog.css" />
    <script type="text/javascript" src="../../cmsjs/Sdialog.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:DropDownList ID="DDKmm" runat="server" Width="200" AutoPostBack="true"></asp:DropDownList>
    <asp:DropDownList ID="DDBJM" runat="server" Width="200" AutoPostBack="true"></asp:DropDownList>
    <TableList:Tlistdata id="tadata" 
        class="logins"         DBORD="3"         DBname="homework"        TjExpression=""
        runat="server">
    </TableList:Tlistdata>
    </form>
</body>
</html>
