Partial Class LoginApi
    Inherits System.Web.UI.UserControl
#Region " Web ������������ɵĴ��� "

    '�õ����� Web ���������������ġ�
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'ע��: ����ռλ�������� Web ���������������ġ�
    '��Ҫɾ�����ƶ�����
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: �˷��������� Web ����������������
        '��Ҫʹ�ô���༭���޸�����
        InitializeComponent()
    End Sub

#End Region
    Dim Conn As New ADODB.Connection
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cc.Connecttodb()
        If Conn.State = 0 Then Conn.Open(cc.setConstr(1))
        Dim jg As String = ""

        jg &= "<form>"
        jg &= MakeLoginPage()
        jg &= "</form>"

        '���ش����û���������ֱ�ӵ�¼
        If Request("username") <> "" And Request("pass") <> "" Then
            Dim un As String
            un = Login(cc.Checkstr(Request("username")), cc.Checkstr(Request("pass")), Request("bcmm"))
            If un = "" Then
                If Request.Cookies("username") Is Nothing Then
                    jg &= "<font color=red>���IE�������Cookie���ã�</font>"
                Else
                    Response.Redirect("index.aspx")
                End If
            Else
                jg &= un
            End If
        Else
            ' If IsPostBack = true Then
            jg &= "���������˺ź�����"
            'End If
        End If

        Label1.Text = jg

    End Sub
    Private Function MakeLoginPage() As String
        Dim uname, upass, uck As String
        If IsNothing(Request.Cookies("xm")) = False Then
            uname = Server.UrlDecode(Server.UrlDecode(Request.Cookies("username").Value))
            upass = Server.UrlDecode(Request.Cookies("pass").Value)
            uck = Request.Cookies("BCMM").Value
        Else
            uname = ""
            upass = ""
            uck = "0"
        End If

        Dim jg As String
        jg = ""
        jg &= "<table class=""logincontent"" border=0 >"
        jg &= "<tr><td rowspan=7 width=20></td><td colspan=2 height=10></td></tr>"

        jg &= "<tr><td>�û���</td><td><input type=text name=username  value='" & uname & "' size=12></td></tr>"
        jg &= "<tr><td>����</td><td><input type=password name=pass  value='" & upass & "' size=12></td></tr>"

        If uck = 1 Then
            jg &= "<tr><td colspan=2><input type=checkbox name=bcmm checked value=1 >�����˻���Ϣ</td></tr>"
        Else
            jg &= "<tr><td colspan=2><input type=checkbox name=bcmm value=1 >�����˻���Ϣ</td></tr>"
        End If

        jg &= "<tr><td colspan=2 align=center><input type=submit class=""btnlogin"" name=denglu value=��¼></td></tr>"

        jg &= "<tr><td colspan=2 align=center height=20>"
        jg &= "<a href=../Accounts/forgetpw.aspx>&nbsp;��������&nbsp;</a> "
        jg &= "&nbsp;<A href=../Accounts/ureg.aspx>&nbsp;�û�ע��&nbsp;</a>"
        jg &= "</td></tr>"

        jg &= "<tr><td colspan=3 height=10></td></tr>"
        jg &= "</table>"


        Return jg
    End Function

    Private Function Login(ByVal strUsername As String, ByVal strPassword As String, ByVal bcmm As String) As String
        Dim jg As String = ""

        jg = adminLogin(strUsername, strPassword, bcmm)
        If jg = "" Then
            ' ��������
            If bcmm = "1" Then
                Response.Cookies("pass").Value = Server.UrlEncode(strPassword)
                Response.Cookies("username").Expires = Now.Date.AddDays(30)
                Response.Cookies("pass").Expires = Now.Date.AddDays(30)
                Response.Cookies("name").Expires = Now.Date.AddDays(30)
                Response.Cookies("qxj").Expires = Now.Date.AddDays(30)
                Response.Cookies("bcmm").Expires = Now.Date.AddDays(30)
            End If
        End If
        Return jg
    End Function
    '����˵��
    ' ByVal strUsername As String,
    ' ByVal strPassword As String, 
    ' ByVal bcmm As String, ���Ƿ񱣴�����
    ' ByVal rylx As String:��Ա����
    Private Function adminLogin(ByVal strUsername As String, ByVal strPassword As String, ByVal bcmm As String) As String
        Dim rs As New ADODB.Recordset
        Dim jg As String = ""
        rs = Conn.Execute("select  * from users where username='" & strUsername & "'")
        If rs.EOF Then
            jg = "<font color=red>" & strUsername & "�û��������ڣ�</font>"
        Else
            If trim(rs.Fields("password").Value) = strPassword Then
                Response.Cookies("name").Value = Server.UrlEncode(rs.Fields("name").Value)
                Response.Cookies("username").Value = Server.UrlEncode(rs.Fields("username").Value)
                Response.Cookies("qxj").Value = Server.UrlEncode(rs.Fields("qxj").Value)
                Response.Cookies("BCMM").Value = bcmm
            Else
                jg = "<font color=red>�������</font>"
            End If
        End If
        rs.Close()

        Return jg
    End Function



End Class
