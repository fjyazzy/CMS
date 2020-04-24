<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Search.aspx.vb" Inherits="Cms1.Search" %>
<HTML>
	<HEAD>
		<title>Search</title>
		<base target="_self">
		<META content="No-cache" http-equiv="Pragma">
		<META content="No-cache" http-equiv="Cache-Control">
		<META content="0" http-equiv="Expires">
		<link runat="server" id="lLink" rel="stylesheet" href="../cmscss/home/style1.css" />
		<script type="text/javascript" src="../cmsjs/bmenu.js"></script>
	</HEAD>
	<body onkeydown="bb(event.keyCode);">
		<table border="0" cellSpacing="0" cellPadding="0" width="100%" height="100%">
			<tr>
				<td colSpan="2" class="gndh" ></td>
			</tr>
			<tr>
				<td height="100%" vAlign="top" colSpan="2" align="center">
					<form action="#" id="form1" name="form1">
						<table border="0" width="100%">
							<tr style="CURSOR: pointer" onclick="ShowHideDiv('menu1')">
								<td class="myMenu" align=center><img alt="|" src="../images/home/plus.gif">查询产品信息<img  alt="|" src="../images/home/plus.gif"></td>
							</tr>
							<tr>
								<td>
									<div id="menu1" style="POSITION: relative; DISPLAY: none">
										<input value="KC" type="checkbox" name="Ck1" checked>库存 
                                        <input value="ZL" type="checkbox" name="Ck1">资料<br>
										<input value="YWD" type="checkbox" name="Ck1" checked>订单 
                                        <input value="ORDER" type="checkbox" name="Ck1" checked>询价单
										<br>
										<input value="GYS" type="checkbox" name="Ck1">供应商
									</div>
								</td>
							</tr>
							<tr style="CURSOR: pointer" onclick="ShowHideDiv('menu2')">
								<td class="myMenu" align=center><img  alt="|" src="../images/home/plus.gif">查询业务信息<img  alt="|" src="../images/home/plus.gif"></td>
							</tr>
							<tr>
								<td><div id="menu2" style="POSITION: relative; DISPLAY: none">
										<input value="XM" type="checkbox" name="Ck2" checked>姓名 
                                        <input value="TYPE" type="checkbox" name="Ck2" checked>型号<br>
										<input value="DH" type="checkbox" name="Ck2">电话 
                                        <input value="BZ" type="checkbox" name="Ck2">备注<br>
										<input value="CS" type="checkbox" name="Ck2">厂商 
                                        <input value="DZ" type="checkbox" name="Ck2">地址<br>
									</div>
								</td>
							</tr>
							<tr style="CURSOR: pointer" onclick="ShowHideDiv('menu3')">
								<td class="myMenu" align=center><img  alt="|" src="../images/home/plus.gif">选择时间范围<img  alt="|" src="../images/home/plus.gif"></td>
							</tr>
							<tr>
								<td><div id="menu3" style="POSITION: relative; DISPLAY: none">
										从:<input size="12" name="kssj" value=''><BR>
										到:<input size="12" name="jssj" value=''></div>
								</td>
							</tr>
							<tr>
								<td>
									<input id="Skey" name="Skey" size="15"><br>
									<input id="Button" class="btnlogin" type="button" value=" 搜   索 " onclick="gosearch()">
								</td>
							</tr>
                            			<tr>
				<td class="myMenu" height="1" colSpan="2">
				</td>
			</tr>
			<tr>
				<td colspan="2" valign="top">
					<asp:Label id="Label1" runat="server"></asp:Label>
				</td>
			</tr>

			</table>
					</form>
				</td>
			</tr>
		</table>
	</body>
</HTML>

