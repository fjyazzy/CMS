Public Class Header
    Inherits System.Web.UI.Page

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label

    '注意: 以下占位符声明是 Web 窗体设计器所必需的。
    '不要删除或移动它。
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: 此方法调用是 Web 窗体设计器所必需的
        '不要使用代码编辑器修改它。
        InitializeComponent()
    End Sub

#End Region

    Protected llink As System.Web.UI.HtmlControls.HtmlGenericControl
    Dim Conn As New ADODB.Connection
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '在此处放置初始化No的用户代码
        cc.Connecttodb()
        If Conn.State = 0 Then Conn.Open(cc.setConstr(1))
        If cc.getQx(Request.Cookies("username").Value, "998") = 0 Then
            Response.Write("没有使用顶部菜单项目的权限,请联系系统管理员!")
            Response.End()
        End If
        cc.getSoftinfo("1")
        llink.Attributes.Add("href", "../cmscss/home/style" & SYSTEMSTYLE & ".css")

        Label1.Text = "<img valign=bottom src=../images/home/inlogo.gif>" & strDwmc & "-"
        Label1.Text &= SOFTNAME & SOFTVERSION & "<img src=../images/home/r.gif>"
        Label1.Text &= Server.UrlDecode(Request.Cookies("name").Value) & " <img src=../images/home/address.gif> "

        Label1.Text &= "<img src=../images/home/l.gif> "
        Label1.Text &= "<a target=bmenu href=bmenu.aspx>系统功能</a> | "
        Label1.Text &= "<a target=bmenu href=search.aspx>全局搜索</a> | "
        Label1.Text &= "<a target=mainx href=../accounts/help.aspx>帮助</a> | "
        Label1.Text &= "<A target=mainx href=../accounts/chgpw.aspx>密码修改</A> |"
        Label1.Text &= "<A target=_parent href=login.aspx>重新登录</A> | "
        Label1.Text &= "<A target=_parent href=logout.ashx>退出系统</A> "

    End Sub
    Private Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Unload
        If Conn.State = 1 Then Conn.Close()
    End Sub

End Class
