Public Class csbjb2
    Inherits System.Web.UI.Page
    Public Conn As New ADODB.Connection
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If cc.getQx(Request.Cookies("Username").Value, "4020") = 0 Then
            Response.Write("没有权限")
            Response.End()
        End If
        If Conn.State = 0 Then Conn.Open(cc.setConstr(4))
        cc.getSoftinfo("4")
        lLink.Attributes.Add("href", "../../cmscss/home/style" & SYSTEMSTYLE & ".css")


        ''这里可修改tddata参数
        ' 设置子表的访问条件
        Dim Cid As Integer
        Dim rs As New ADODB.Recordset
        Cid = CInt(Request("Cid"))
        rs.Open("select  * from csbjb where id=" & Cid, Conn, 1, 3)
        If rs.EOF Then
            tadata.TjExpression = " 1=0 "
        Else
            tadata.TjExpression = " csbjbh='" & rs.Fields("csbjbh").Value & "'"
        End If
        rs.Close()


    End Sub



End Class