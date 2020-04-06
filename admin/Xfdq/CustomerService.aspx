<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CustomerService.aspx.vb" Inherits="Cms1.CustomerService" %>
<%@ Register tagPrefix="TableAlldata" tagname="TAdata" src="../../components/TableAlldata.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>客服管理</title>
    <link runat="server" id="lLink" rel="stylesheet" href="../../cmscss/home/style1.css" />
    <link rel="stylesheet" href="../../cmscss/Sdialog.css" />
    <script type="text/javascript" src="../../cmsjs/Sdialog.js"></script>
</head>
<body>
    <form id="form1" runat="server">
      <TableAlldata:TAdata id="tadata" class="logins" 
       DBOrd="4" TableName="CustomerService" TjExpression=""
       runat="server">
     </TableAlldata:TAdata>

    </form>
</body>
</html>
