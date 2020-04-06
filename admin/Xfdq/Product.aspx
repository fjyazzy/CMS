<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Product.aspx.vb" Inherits="Cms1.Product" %>
<%@ Register tagPrefix="TableList" tagname="Tlistdata" src="../../components/Table_MX.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>产品及服务管理</title>
    <link runat="server" id="lLink" rel="stylesheet" href="../../cmscss/home/style1.css" />
    <link rel="stylesheet" href="../../cmscss/Sdialog.css" />
    <script type="text/javascript" src="../../cmsjs/Sdialog.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <TableList:Tlistdata  id="tadata"  class="logins" 
        DBOrd="4" DBname="Product" TjExpression=""
        runat="server">
    </TableList:Tlistdata>
    </form>
</body>
</html>
