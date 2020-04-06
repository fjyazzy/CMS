Public Class mcatalogs
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '在此处放置初始化页的用户代码

        PageTitle.InnerText = WCS.GetTitle("Title", "mCatalogs")
        Content1.Attributes.Add("name", "description")
        Content1.Attributes.Add("content", WCS.GetTitle("MetaContent", "mCatalogs"))
        Content2.Attributes.Add("name", "keywords")
        Content2.Attributes.Add("content", WCS.GetTitle("MetaKeywords", "mCatalogs"))

        If Request("id") <> "" Then
            Label1.Text = WCS.SearchZl(2, CC.Checkstr(Request("id")), Request("page"), "mcatalogs.aspx")
        Else
            If Request("skey") <> "" Then
                Label1.Text = WCS.SearchZl(Request("mode"), CC.Checkstr(Request("skey")), Request("page"), "mcatalogs.aspx")
            Else
                Label1.Text = WCS.CateList("mcatalogs.aspx")
            End If
        End If

    End Sub

End Class