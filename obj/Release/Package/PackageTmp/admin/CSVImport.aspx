<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CSVImport.aspx.vb" Inherits="Cms1.CSVImport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>CSV文件批量导入</title>
    <link runat="server" id="lLink" rel="stylesheet" href="../../cmscss/home/style1.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h3>CSV文件批量导入注意事项</h3>
    <ol>
        <li>导入的文件格式必须为CSV格式，CSV格式可通过Excel文件导出生成</li>
        <li>导入规则为按关键字检索，若存在相同关键字则覆盖，若不存在关键字则追加。</li>
        <li>CSV文件字段顺序必须和下载的excel字段顺序一致。</li>
    </ol>
    </div>
        <asp:FileUpload ID="File1" runat="server" Width="534px" />
        <br />
        <asp:Button ID="Button1" runat="server" Text="导入" Width="537px" />
        <br />
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    </form>
</body>
</html>
