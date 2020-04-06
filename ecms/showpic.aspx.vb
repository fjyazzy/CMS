Public Class showpic
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        PageTitle.InnerText = WCS.GetTitle("Title", "Showpic")
        Content1.Attributes.Add("name", "description")
        Content1.Attributes.Add("content", WCS.GetTitle("MetaContent", "Showpic"))
        Content2.Attributes.Add("name", "keywords")
        Content2.Attributes.Add("content", WCS.GetTitle("MetaKeywords", "Showpic"))
        Label1.Text = "<img id='Img1'  name='Img1' src='photos/" & Request("i") & "' border=0 width=600>"
    End Sub

End Class