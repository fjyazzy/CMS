<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="company.aspx.vb" Inherits="Cms1.company1" %>
<%@ Register tagPrefix="TableAlldata" tagname="TAdata" src="../../components/TableAlldata.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>公司基本信息配置</title>
<link runat="server" id="lLink" rel="stylesheet" href="../../cmscss/home/style1.css" />
<link rel="stylesheet" href="../../cmscss/Sdialog.css" />
<script type="text/javascript" src="../../cmsjs/Sdialog.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <table>
            <tr><td><h3>公司基本信息维护</h3>
            </td></tr>
            <tr><td>
                <TableAlldata:TAdata id="tadata" class="logins" 
                    DBOrd="4" TableName="department" TjExpression=""
                    runat="server">
                </TableAlldata:TAdata>
           </td></tr>
        </table>

    </form>
</body>
</html>