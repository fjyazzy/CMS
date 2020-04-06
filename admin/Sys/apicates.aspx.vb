Public Class apicates
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Conn As New ADODB.Connection
        If CC.getQx(Request.Cookies("Username").Value, "1008") = 0 Then
            Response.Write("没有权限")
            Response.End()
        End If
        If Conn.State = 0 Then Conn.Open(CC.setConstr(1))
        CC.getSoftinfo("1")
        lLink.Attributes.Add("href", "../../cmscss/home/style" & SYSTEMSTYLE & ".css")

    End Sub
End Class