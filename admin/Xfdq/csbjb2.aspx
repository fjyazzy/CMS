<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="csbjb2.aspx.vb" Inherits="Cms1.csbjb2" %>
<%@ Register tagPrefix="TableList" tagname="Tlistdata" src="../../components/Table_MX.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>承试报价单细项</title>
    <link runat="server" id="lLink" rel="stylesheet" href="../../cmscss/home/style1.css" />
    <link rel="stylesheet" href="../../cmscss/Sdialog.css" />
    <script type="text/javascript" src="../../cmsjs/Sdialog.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <TableList:Tlistdata  id="tadata"  class="logins" 
        DBOrd="4" DBname="Csbjb2" TjExpression=""
        runat="server">
    </TableList:Tlistdata>
    </form>
</body>
</html>
