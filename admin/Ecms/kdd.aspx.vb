Public Class kdd
    Inherits System.Web.UI.Page
    Dim Conn As New ADODB.Connection
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If cc.getQx(Request.Cookies("Username").Value, "3005") = 0 Then
            Response.Write("没有权限")
            Response.End()
        End If
        If Conn.State = 0 Then Conn.Open(cc.setConstr(DBord_ecms))

        Dim rs As New ADODB.Recordset
        Dim sql As String
        sql = "select  * from orders where id=" & cc.Checkstr(Request("id"))
        rs.Open(sql, Conn, 1, 3)
        Select Case Request("lx")
            Case "sj"
                GetBuju("收据", rs)
            Case "shd"
                GetBuju("送货单", rs)
            Case Else
                GetBuju(rs.Fields("fhfs").Value, rs)
        End Select
        rs.Close()
        Conn.Close()


    End Sub

    Sub GetBuju(ByVal xid As String, ByVal rsy As ADODB.Recordset)
        Dim buju As String

        ' 获取排版方式数据
        Dim rs As New ADODB.Recordset
        Dim sql As String
        sql = "select  * from webpages where lxbh='012' and xm='" & xid & "'"
        rs.Open(sql, conn, 1, 3)
        buju = rs.Fields("pb").Value
        rs.Close()
        ' 输出快递单
        Dim f(), g() As String
        Dim i As Integer
        f = Split(buju, vbCrLf)
        For i = 0 To UBound(f)
            g = Split(f(i), "|")
            If UBound(g) > 2 Then
                If InStr(g(4), "-") > 0 Then
                    g(4) = rsy.Fields(Mid(g(4), InStr(g(4), "-") + 1)).Value
                Else
                    g(4) = sjzh(g(4))
                End If
                Response.Write("<div id=item_" & i & " 	style='left:" & g(0) + 15 & "px;top:" & g(1) + 15 & "px;position:Absolute;font-size:" & g(2) & "pt;font-weight:" & g(3) & "' >" & g(4) & "</div>")
            End If
        Next i



    End Sub

    Function sjzh(ByVal str As String) As String
        ' 获取票据的基础数据
        Dim strJg As String
        Dim rs2 As New ADODB.Recordset
        strJg = ""
        rs2.Open("SELECT  * from WebPages where xm='" & str & "'", Conn, 1, 3, 1)
        If Not rs2.EOF Then
            strJg = rs2.Fields("nr").Value
        Else
            strJg = "数据错误！"
        End If
        rs2.Close()
        Return strJg
    End Function

End Class