Public Class Header
    Inherits System.Web.UI.Page

#Region " Web ������������ɵĴ��� "

    '�õ����� Web ���������������ġ�
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label

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
        cc.Connecttodb()
        If Conn.State = 0 Then Conn.Open(cc.setConstr(1))
        If cc.getQx(Request.Cookies("username").Value, "998") = 0 Then
            Response.Write("û��ʹ�ö����˵���Ŀ��Ȩ��,����ϵϵͳ����Ա!")
            Response.End()
        End If
        cc.getSoftinfo("1")
        llink.Attributes.Add("href", "../cmscss/home/style" & SYSTEMSTYLE & ".css")

        Label1.Text = "<img valign=bottom src=../images/home/inlogo.gif>" & strDwmc & "-"
        Label1.Text &= SOFTNAME & SOFTVERSION & "<img src=../images/home/r.gif>"
        Label1.Text &= Server.UrlDecode(Request.Cookies("name").Value) & " <img src=../images/home/address.gif> "

        Label1.Text &= "<img src=../images/home/l.gif> "
        Label1.Text &= "<a target=bmenu href=bmenu.aspx>ϵͳ����</a> | "
        Label1.Text &= "<a target=bmenu href=search.aspx>ȫ������</a> | "
        Label1.Text &= "<a target=mainx href=../accounts/help.aspx>����</a> | "
        Label1.Text &= "<A target=mainx href=../accounts/chgpw.aspx>�����޸�</A> |"
        Label1.Text &= "<A target=_parent href=login.aspx>���µ�¼</A> | "
        Label1.Text &= "<A target=_parent href=logout.ashx>�˳�ϵͳ</A> "

    End Sub
    Private Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Unload
        If Conn.State = 1 Then Conn.Close()
    End Sub

End Class
