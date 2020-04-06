<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ks2.aspx.vb" Inherits="Cms1.ks2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link runat="server" id="lLink" rel="stylesheet" href="../cmscss/style.css" />
    <style type="text/css">
*{
	margin:0;
	padding: 0;
}
#container{
	width: 90%;
	background:#daf8f9;
	height:auto;
	margin: 0 auto;
 
}
#nav{
	position:fixed; 
	top:0; 
	width:90%; 
	height: 30px; 
	background: #eee;
}
#main{
	width:100%;
	margin-top:35px;
	padding-top: 10px;
}
</style>

</head>
<body>
<form id="form1" runat="server">
<div id="container">
	<div id="nav">
	<asp:Label ID="Label2" runat="server" Text="Label" Font-Bold="true" ForeColor="#FF3300"></asp:Label>
	</div>
	<div id="main">
		      <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label><br />
        <asp:Button ID="Button1" runat="server" Text="提交试卷" Height="45px" Width="501px" />
	</div>
</div>    
</form>
</body>
</html>
