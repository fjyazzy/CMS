Public Class c_baseinfo
    Inherits System.Web.UI.Page
    Public Conn As New ADODB.Connection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If cc.getQx(Request.Cookies("Username").Value, "3008") = 0 Then
            Response.Write("没有权限")
            Response.End()
        End If
        If Conn.State = 0 Then Conn.Open(cc.setConstr(DBord_ecms))
        cc.getSoftinfo(dbord_ecms)
        lLink.Attributes.Add("href", "../../cmscss/home/style" & SYSTEMSTYLE & ".css")


        '设置数据库
        tadata.DBOrd = DBord_ecms


    End Sub

End Class