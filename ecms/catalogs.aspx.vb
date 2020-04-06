Public Class catalogs
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        CC.Connecttodb()
        If conn.State = 0 Then conn.Open(CC.setConstr(DBord_ecms))
        Dim pagex As String

        PageTitle.InnerText = WCS.GetTitle("Title", "Catalogs")
        Content1.Attributes.Add("name", "description")
        Content1.Attributes.Add("content", WCS.GetTitle("MetaContent", "Catalogs"))
        Content2.Attributes.Add("name", "keywords")
        Content2.Attributes.Add("content", WCS.GetTitle("MetaKeywords", "Catalogs"))

        If CC.Checkstr(Request("page")) = "" Then
            pagex = 1
        Else
            pagex = CC.Checkstr(Request("page"))
        End If


        If Request("skey") <> "" Then
            Label1.Text = WCS.SearchP(CInt(Request("mode")), CC.Checkstr(Request("skey")), pagex, "Catalogs.aspx")
            Label1.Text &= WCS.SearchZl(1, CC.Checkstr(Request("skey")), pagex, "Catalogs.aspx")
        Else
            If Request("id") <> "" Then
                Label1.Text = WCS.SearchP(2, CC.Checkstr(Request("id")), Request("page"), "Catalogs.aspx")
            Else
                Label1.Text = WCS.pCateList("Catalogs.aspx")
            End If
        End If

        conn.Close()


    End Sub

End Class