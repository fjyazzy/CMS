Public Class DBSql
    Inherits System.Web.UI.Page
    Dim Conn As New ADODB.Connection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cc.Connecttodb()
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Label1.Text = "数据库为：" & DDDBord.SelectedValue
        If Conn.State = 0 Then Conn.Open(cc.setConstr(DDDBord.SelectedValue))
        Conn.Execute(TxtSQL.Text)
    End Sub
End Class