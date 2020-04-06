<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="sumzf.aspx.vb" Inherits="Cms1.sumzf" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link runat="server" id="lLink" rel="stylesheet" href="../cmscss/home/style1.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>          
      
          <table>
              <tr>
                  <td >考试科目
                      </td>
                  <td><asp:DropDownList ID="DDKSxm" runat="server" Width="200">
                        </asp:DropDownList></td>
                  <td>&nbsp;</td>
              </tr>
                           <tr>
                  <td >班级</td>
                  <td><asp:DropDownList ID="DDBJM" runat="server" Width="200">
                        </asp:DropDownList></td>
                  <td>&nbsp;</td>
              </tr>
              <tr>
                  <td>1.</td>
                  <td><asp:Button ID="Button3" runat="server" Text="开始考试" /></td>
                  <td>开始考试前建议备份数据库</td>
              </tr>
              <tr>
                  <td>2.</td>
                  <td><asp:Button ID="Button2" runat="server" Text="自动评卷" /></td>
                  <td>&nbsp;</td>
              </tr>
              <tr>
                  <td>3.</td>
                  <td><asp:Button ID="Button1" runat="server" Text="计算总成绩" /></td>
                  <td>&nbsp;</td>
              </tr>
 
 
          </table>
          <br />
    
    </div>
    </form>
</body>
</html>
