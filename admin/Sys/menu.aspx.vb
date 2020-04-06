Public Class menu
    Inherits System.Web.UI.Page
    Public Conn As New ADODB.Connection
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If cc.getQx(Request.Cookies("Username").Value, "1003") = 0 Then
            Response.Write("没有权限")
            Response.End()
        End If
        If Conn.State = 0 Then Conn.Open(cc.setConstr(1))
        cc.getSoftinfo("1")
        lLink.Attributes.Add("href", "../../cmscss/home/style" & SYSTEMSTYLE & ".css")

        ''这里可修改tddata参数
        'tadata.TjExpression = " km='" & DDKmm.SelectedValue & "' and bjh='" & DDBJM.SelectedValue & "' "

    End Sub

End Class