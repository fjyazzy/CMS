Public Class C_manual
    Inherits System.Web.UI.Page
    Public Conn As New ADODB.Connection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If CC.getQx(Request.Cookies("Username").Value, "3002") = 0 Then
            Response.Write("没有权限")
            Response.End()
        End If
        If Conn.State = 0 Then Conn.Open(CC.setConstr(DBord_ecms))
        CC.getSoftinfo(DBord_ecms)
        lLink.Attributes.Add("href", "../../cmscss/home/style" & SYSTEMSTYLE & ".css")

        '设置数据库
        tadata.DBOrd = DBord_ecms



    End Sub

End Class