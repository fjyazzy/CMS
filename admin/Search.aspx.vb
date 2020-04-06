Public Class Search
    Inherits System.Web.UI.Page

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
        If Request("id") <> "" Then
            Dim rs As New ADODB.Recordset
            rs.Open("select  * from sysmenuitems where menuid='" & cc.Checkstr(Request("id")) & "' order by orderid", Conn, 1, 1)
            Label1.Text &= "<ul>"
            While Not rs.EOF
                If HaveQx(rs.Fields("qxid").Value) Then
                    Label1.Text &= "<li><A target=""mainx"" href=""" & rs.Fields("itemtext").Value & """>" & rs.Fields("itemname").Value & "</a></li>"
                End If
                rs.MoveNext()
            End While
            Label1.Text &= "</ul>"
            rs.Close()
        End If
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