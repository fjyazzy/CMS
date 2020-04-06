Public Class Menuitem
    Inherits System.Web.UI.Page
    Public Conn As New ADODB.Connection
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If cc.getQx(Request.Cookies("Username").Value, "1005") = 0 Then
            Response.Write("没有权限")
            Response.End()
        End If
        If Conn.State = 0 Then Conn.Open(cc.setConstr(1))
        cc.getSoftinfo("1")
        lLink.Attributes.Add("href", "../../cmscss/home/style" & SYSTEMSTYLE & ".css")

        ''这里可修改tddata参数
        ' 设置子表的访问条件x
        Dim Cid As Integer
        Dim rs As New ADODB.Recordset
        Cid = CInt(Request("Cid"))
        rs.Open("select  * from sysmenus where id=" & Cid, Conn, 1, 3)
        If rs.EOF Then
            tadata.TjExpression = " 1=0 "
        Else
            tadata.TjExpression = " menuid='" & rs.Fields("menuid").Value & "'"
        End If
        rs.Close()

        Conn.Close()

    End Sub

End Class