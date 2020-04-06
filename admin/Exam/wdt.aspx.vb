Public Class wdt
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If cc.getQx(Request.Cookies("Username").Value, "6009") = 0 Then
            Response.Write("没有权限")
            Response.End()
        End If

    End Sub

End Class