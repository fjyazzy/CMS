Public Class Cat_column
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Label1.Text = WCS.GetCategoryList()
    End Sub

End Class