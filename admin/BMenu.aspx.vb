Public Class BMenu
    Inherits System.Web.UI.Page

#Region " Web ������������ɵĴ��� "

    '�õ����� Web ���������������ġ�
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label

    'ע��: ����ռλ�������� Web ���������������ġ�
    '��Ҫɾ�����ƶ�����
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: �˷��������� Web ����������������
        '��Ҫʹ�ô���༭���޸�����
        InitializeComponent()
    End Sub

#End Region

    Protected llink As System.Web.UI.HtmlControls.HtmlGenericControl
    Dim Conn As New ADODB.Connection
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '�ڴ˴����ó�ʼ��No���û�����
        If cc.getQx(Request.Cookies("Username").Value, "998") = 0 Then
            Response.Write("û��Ȩ��")
            Response.End()
        End If
        If Conn.State = 0 Then Conn.Open(cc.setConstr(1))
        cc.getSoftinfo("1")
        llink.Attributes.Add("href", "../cmscss/home/style" & SYSTEMSTYLE & ".css")
        Label1.Text = ""


        Dim rs2 As New ADODB.Recordset
        Dim rs As New ADODB.Recordset
        Dim uname As String = Request.Cookies("username").Value
        Dim f As Object
        f = Split(Server.UrlDecode(Request.Cookies("qxj").Value), "|")
        Dim j, k, Mnums As Integer
        'K�����������Ƿ���ʾ�Ŀ���
        Mnums = 1
        Label1.Text = "<table border = ""0"" width=""100%"">"
        rs2.Open("Select  * from sysmenus order by menuOrder", Conn, 1, 1)
        While Not rs2.EOF
            k = 0
            For j = 0 To UBound(f) - 1
                If rs2.Fields("qxid").Value = f(j) Then
                    k = 1
                    Exit For
                End If
            Next
            If k = 1 Then

                Label1.Text &= "<tr style = ""CURSOR: pointer"" onclick=""ShowHideDiv('menu" & Mnums & "')"">"
                Label1.Text &= "<td Class=""myMenu"" align=left>"
                Label1.Text &= "<img alt = ""|"" src=""../images/home/plus.gif"">&nbsp;"
                Label1.Text &= rs2.Fields("MenuName").Value.Padright(7, " ")
                Label1.Text &= "</td>"
                Label1.Text &= "</tr>"

                Label1.Text &= "<tr><td>"
                Label1.Text &= "<div id = ""menu" & Mnums & """ style=""POSITION: relative; DISPLAY: none"">"
                '��ȡ�Ӳ˵�
                Dim mid As String
                mid = rs2.Fields("menuid").Value
                rs.Open("Select  * from sysmenuitems where menuid='" & mid & "' order by orderid", Conn, 1, 1)
                Label1.Text &= "<ul>"
                While Not rs.EOF
                    If HaveQx(rs.Fields("qxid").Value) Then
                        If trim(rs.Fields("itemname").Value) = "---" Then
                            Label1.Text &= "</ul><hr width=80% size=1 bgcolor=#fff><ul>"
                        Else
                            Label1.Text &= "<li><A href=# onclick=""top.document.all('Contentx').rows='99%,1%,*';top.document.all('mainx').src='" & rs.Fields("itemtext").Value & "'"">" & rs.Fields("itemname").Value & "</a></li>"
                        End If
                    End If
                    rs.MoveNext()
                End While
                Label1.Text &= "</ul>"
                rs.Close()

                Label1.Text &= "</div>"
                Label1.Text &= "</td></tr>"

                Mnums = Mnums + 1
            End If
            rs2.MoveNext()
        End While
        rs2.Close()
        Label1.Text &= "</table>"
        Label2.text = ssl()
        Conn.Close()

    End Sub
    Private Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Unload
        If Conn.State = 1 Then Conn.Close()
    End Sub
    Function HaveQx(ByVal n As String)
        Dim f As Object
        Dim j, k As Integer
        f = Split(Server.UrlDecode(Request.Cookies("qxj").Value), "|")
        k = 0
        For j = 0 To UBound(f)
            If n = f(j) Then
                k = 1
                Exit For
            End If
        Next
        Return k
    End Function

    Function ssl() As String
        Dim jg As String = ""

        jg &= "<table border=""0"" width=""100%"">"
        jg &= "<tr style=""CURSOR: pointer"" onclick=""ShowHideDiv('menua1')"">"
        jg &= "<td Class=""myMenu"" align=center><img alt=""|"" src=""../images/home/plus.gif"">��ѯ��Ʒ��Ϣ</td>"
        jg &= "</tr>"
        jg &= "<tr>"
        jg &= " <td>"
        jg &= "<div id=""menua1"" style=""POSITION: relative; DISPLAY: none"">"
        jg &= " <input value=""KC"" type=""checkbox"" name=""Ck1"" checked>���"
        jg &= " <input value=""ZL"" type=""checkbox"" name=""Ck1"">����<br>"
        jg &= "	<input value=""YWD"" type=""checkbox"" name=""Ck1"" checked>����"
        jg &= " <input value=""ORDER"" type=""checkbox"" name=""Ck1"" checked>ѯ�۵�"
        jg &= "<br>"
        jg &= "<input value=""GYS"" type=""checkbox"" name=""Ck1"">��Ӧ��"
        jg &= "</div>"
		jg &= "</td>"
		jg &= "</tr>"
        jg &= "<tr style=""CURSOR: pointer;"" onclick=""ShowHideDiv('menua2')"" >"
        jg &= "<td Class=""myMenu"" align=center><img  alt=""|"" src=""../images/home/plus.gif"">��ѯҵ����Ϣ</td>"
        jg &= "</tr>"
        jg &= "	<tr>"
        jg &= " <td><div id=""menua2"" style=""POSITION: relative; DISPLAY: none"">"
        jg &= "<input value=""XM"" type=""checkbox"" name=""Ck2"" checked>����"
        jg &= "<input value=""TYPE"" type=""checkbox"" name=""Ck2"" checked>�ͺ�<br>"
        jg &= "<input value=""DH"" type=""checkbox"" name=""Ck2"" > �绰"
        jg &= "<input value=""BZ"" type=""checkbox"" name=""Ck2"">��ע<br>"
        jg &= "<input value=""CS"" type=""checkbox"" name=""Ck2"">����"
        jg &= "<input value=""DZ"" type=""checkbox"" name=""Ck2"">��ַ<br>"
        jg &= "</div>"
        jg &= "</td>"
        jg &= "</tr>"
		jg &= "<tr style= ""CURSOR: pointer"" onclick=""ShowHideDiv('menua3')"">"
        jg &= "<td Class=""myMenu"" align=center><img  alt=""|"" src=""../images/home/plus.gif"">ѡ��ʱ�䷶Χ</td>"
        jg &= "</tr>"
        jg &= "<tr>"
        jg &= "<td><div id=""menua3"" style=""POSITION: relative; DISPLAY: none"">"
        jg &= "��: <input size=""12"" name=""kssj"" value=''><BR>"
        jg &= "��: <input size=""12"" name=""jssj"" value=''></div>"
        jg &= "</td>"
        jg &= "</tr>"
        jg &= "<tr>"
        jg &= "<td>"
        jg &= "<input id=""Skey"" name=""Skey"" size=""15""><br>"
        jg &= "<input id=""Button"" Class=""btnlogin"" type=""button"" value="" ��   �� "" onclick=""gosearch()"">"
        jg &= "</td></tr></table>"


        Return jg
    End Function
End Class
