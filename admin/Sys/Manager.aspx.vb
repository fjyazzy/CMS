Public Class Manager
    Inherits System.Web.UI.Page
    Dim Conn As New ADODB.Connection
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If cc.getQx(Request.Cookies("Username").Value, "1004") = 0 Then
            Response.Write("没有权限")
            Response.End()
        End If
        If Conn.State = 0 Then Conn.Open(cc.setConstr(1))
        cc.getSoftinfo("1")
        lLink.Attributes.Add("href", "../../cmscss/home/style" & SYSTEMSTYLE & ".css")
    End Sub

End Class