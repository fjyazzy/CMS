Public Class Career
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        PageTitle.InnerText = WCS.GetTitle("Title", "Career")
        Content1.Attributes.Add("name", "description")
        Content1.Attributes.Add("content", WCS.GetTitle("MetaContent", "Career"))
        Content2.Attributes.Add("name", "keywords")
        Content2.Attributes.Add("content", WCS.GetTitle("MetaKeywords", "Career"))
        Label1.Text = WCS.GetWebContent("招聘")


    End Sub

End Class