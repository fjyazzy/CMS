<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="index.aspx.vb" Inherits="Cms1.index" %>
<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title runat="server" id="PageTitle">管理系统</title>
</head>

    	<FRAMESET rows="30,*" id="mainFrame" frameborder="no" border="0" framespacing="0">
		<frame id="topMenu" name="topMneu" src="header.aspx" scrolling="no">
		<FRAMESET cols="130,12,*" id="main2" frameborder="no" border="0" framespacing="0">
			<FRAME name="bmenu" src="bmenu.aspx" scrolling="no" frameborder="no">
			<FRAME name="bar" src="bar.htm" scrolling="no" frameborder="no">
            <Frameset id="Contentx" rows="99%,1%,*" frameborder="yes" border="3" framespacing="0">
                    <frame id="mainx" name="mainx" src="aboutus.aspx" scrolling="yes">
                    <frame id="mainx1" name="mainx1" src="" scrolling="yes">
                    <frame id="mainx2" name="mainx2" src="" scrolling="yes">
           </Frameset>
		</FRAMESET>
	</FRAMESET>

</html>
