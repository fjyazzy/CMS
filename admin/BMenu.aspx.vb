Public Class BMenu
    Inherits System.Web.UI.Page

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label

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
        If cc.getQx(Request.Cookies("Username").Value, "998") = 0 Then
            Response.Write("没有权限")
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
        'K设置用来做是否显示的开关
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
                '获取子菜单
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


End Class
