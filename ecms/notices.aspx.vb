Public Class notices
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        PageTitle.InnerText = WCS.GetTitle("Title", "Aboutus")
        Content1.Attributes.Add("name", "description")
        Content1.Attributes.Add("content", WCS.GetTitle("MetaContent", "Aboutus"))
        Content2.Attributes.Add("name", "keywords")
        Content2.Attributes.Add("content", WCS.GetTitle("MetaKeywords", "Aboutus"))
        Label1.Text = WCS.GetWebContent("法律声明")

    End Sub

End Class