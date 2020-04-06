Public Class Ecms_Products
    Inherits System.Web.UI.Page

    Public Conn As New ADODB.Connection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If cc.getQx(Request.Cookies("Username").Value, "3001") = 0 Then
            Response.Write("没有权限")
            Response.End()
        End If
        If Conn.State = 0 Then Conn.Open(cc.setConstr(DBord_ecms))
        cc.getSoftinfo(dbord_ecms)
        lLink.Attributes.Add("href", "../../cmscss/home/style" & SYSTEMSTYLE & ".css")


        '设置数据库
        tadata.DBOrd = DBord_ecms

        ''这里可修改tddata参数
        ' 设置子表的访问条件x
        Dim Cid As Integer
        Cid = CInt(Request("Cid"))
        tadata.TjExpression = " categoryid=" & Cid


    End Sub

End Class