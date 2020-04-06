Public Class viewManual
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Label1.Text = "<iframe src='" & Server.UrlDecode(Request("p")) & "'  width=998 height=600></iframe>"
    End Sub

End Class