Public Class webpages
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
        Response.Write("111111111111111111111:" & DBord_ecms)

        ''这里可修改tddata参数
        ' 设置子表的访问条件x
        Dim Cid As Integer
        Dim rs As New ADODB.Recordset
        Cid = CInt(Request("Cid"))
        rs.Open("select  * from Category_Webpages where id=" & Cid, Conn, 1, 3)
        If rs.EOF Then
            tadata.TjExpression = " 1=0 "
        Else
            tadata.TjExpression = " lxbh='" & rs.Fields("lxbh").Value & "'"
        End If
        rs.Close()


    End Sub

End Class