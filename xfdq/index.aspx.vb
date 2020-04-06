Public Class index1

    Inherits System.Web.UI.Page
    Dim Conn As New ADODB.Connection
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
        cc.Connecttodb()
        If Conn.State = 0 Then Conn.Open(cc.setConstr(4))
        cc.getSysinfo("4")
        cc.getSoftinfo("4")
        PageTitle.Text = strDwmc & "-" & SOFTNAME & SOFTVERSION
        Label2.Text = SOFTNAME & SOFTVERSION
        dhl.Text = cc.webDHL("4")
    End Sub
    Private Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Unload
        If Conn.State = 1 Then Conn.Close()
    End Sub


End Class