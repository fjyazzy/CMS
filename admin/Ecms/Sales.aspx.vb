Public Class Sales
    Inherits System.Web.UI.Page
    Public Conn As New ADODB.Connection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If cc.getQx(Request.Cookies("Username").Value, "3005") = 0 Then
            Response.Write("没有权限")
            Response.End()
        End If
        If Conn.State = 0 Then Conn.Open(cc.setConstr(DBord_ecms))
        Label1.Text = ""
        Dim cid As Integer
        Dim rs As New ADODB.Recordset
        rs.Open("select COUNT(id) as nums,status from Inquirys group by status  order by status", Conn, 1, 3)
        While Not rs.EOF
            cid = IIf(System.Convert.IsDBNull(rs.Fields(1).Value), 9, rs.Fields(1).Value)
            Select Case cid
                Case 0
                    Label1.Text &= "<a href=Inquirys.aspx?cid=0>挂单(" & rs.Fields(0).Value & ")</a>"
                Case 1
                    Label1.Text &= " | <a href=Inquirys.aspx?cid=1>跟单(" & rs.Fields(0).Value & ")</a>"
                Case 2
                    Label1.Text &= " | <a href=Inquirys.aspx?cid=2>弃单(" & rs.Fields(0).Value & ")</a>"
                Case 3
                    Label1.Text &= " | <a href=Inquirys.aspx?cid=3>成功交易(" & rs.Fields(0).Value & ")</a>"
            End Select
            rs.MoveNext()
        End While
        rs.Close()

        Label2.Text = ""
        rs.Open("select COUNT(id) as nums,status from Orders group by status  order by status", Conn, 1, 3)
        While Not rs.EOF
            cid = IIf(System.Convert.IsDBNull(rs.Fields(1).Value), 9, rs.Fields(1).Value)

            Select Case cid
                Case 0
                    Label2.Text &= "<a href=Orders.aspx?cid=0>已发货(" & rs.Fields(0).Value & ")</a>"
                Case 1
                    Label2.Text &= " | <a href=Orders.aspx?cid=1>未到帐(" & rs.Fields(0).Value & ")</a>"
                Case 2
                    Label2.Text &= " | <a href=Orders.aspx?cid=2>欠款单(" & rs.Fields(0).Value & ")</a>"
                Case 3
                    Label2.Text &= " | <a href=Orders.aspx?cid=3>退款单(" & rs.Fields(0).Value & ")</a>"
                Case 4
                    Label2.Text &= " | <a href=Orders.aspx?cid=4>已结算(" & rs.Fields(0).Value & ")</a>"
                Case 5
                    Label2.Text &= " | <a href=Orders.aspx?cid=5>已到帐(" & rs.Fields(0).Value & ")</a>"
            End Select

            rs.MoveNext()
        End While
        rs.Close()




    End Sub

End Class