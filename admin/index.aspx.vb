Public Class index
    Inherits System.Web.UI.Page
    Dim Conn As New ADODB.Connection
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cc.Connecttodb()
        If Conn.State = 0 Then Conn.Open(cc.setConstr(1))
        cc.getSysinfo("1")
        cc.getSoftinfo("1")
        PageTitle.Text = strDwmc & "-" & SOFTNAME & SOFTVERSION
    End Sub
    Private Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Unload
        If Conn.State = 1 Then Conn.Close()
    End Sub

End Class