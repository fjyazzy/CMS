Public Class chgpw
    Inherits System.Web.UI.Page

    Dim Conn As New ADODB.Connection
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        cc.Connecttodb()
        If Conn.State = 0 Then Conn.Open(cc.setConstr(1))
        Dim yfm, xm, sql As String
        Dim rs As New ADODB.Recordset
        yfm = Server.UrlDecode(Request.Cookies("username").Value)
        If Not IsPostBack Then
        Else
            If mm1.Text = mm2.Text Then
                sql = "select  * from Users where username='" & yfm & "'"
                rs.Open(sql, Conn, 1, 3)
                If rs.Fields("password").Value <> jmm.Text Then
                    rs.Close()
                    Label1.Text = ("<font color=red>旧密码错误!</font>")
                    Exit Sub
                End If
                rs.Fields("password").Value = mm1.Text
                rs.Update()
                rs.Close()
                Label1.Text = ("<font color=red>密码修改完成!</font>")
            Else
                Label1.Text = ("<font color=red>两次新密码不同!</font>")
            End If
        End If

    End Sub
    Private Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Unload
        If Conn.State = 1 Then Conn.Close()
    End Sub


End Class