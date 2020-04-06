Public Class pagebottom1
    Inherits System.Web.UI.UserControl
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Label1.Text = WCS.GetWebContent("页面底部")
    End Sub

End Class