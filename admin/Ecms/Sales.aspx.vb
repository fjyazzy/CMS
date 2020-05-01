Public Class Sales
    Inherits System.Web.UI.Page
    Public Conn As New ADODB.Connection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If cc.getQx(Request.Cookies("Username").Value, "3005") = 0 Then
            Response.Write("没有权限")
            Response.End()
        End If
        If Conn.State = 0 Then Conn.Open(CC.setConstr(DBord_ecms))
        CC.getSoftinfo(DBord_ecms)
        lLink.Attributes.Add("href", "../../cmscss/home/style" & SYSTEMSTYLE & ".css")


        Dim qzui As String = "href=# onclick=""top.document.all('Contentx').rows='5%,95%,*';top.document.all('mainx1').src='ecms/"
        Label1.Text = ""
        Dim cid, i As Integer
        Dim sl(10) As Integer
        For i = 0 To 9
            sl(i) = 0
        Next
        Dim rs As New ADODB.Recordset
        rs.Open("select COUNT(id) as nums,status from Inquirys group by status  order by status", Conn, 1, 3)
        While Not rs.EOF
            cid = IIf(System.Convert.IsDBNull(rs.Fields(1).Value), 9, rs.Fields(1).Value)
            sl(cid) = rs.Fields(0).Value
            rs.MoveNext()
        End While
        rs.Close()
        Label1.Text &= "<a " & qzui & "Inquirys.aspx?cid=0'"">挂单(" & sl(0) & ")</a>"
        Label1.Text &= " | <a " & qzui & "Inquirys.aspx?cid=1'"">跟单(" & sl(1) & ")</a>"
        Label1.Text &= " | <a " & qzui & "Inquirys.aspx?cid=2'"">弃单(" & sl(2) & ")</a>"
        Label1.Text &= " | <a " & qzui & "Inquirys.aspx?cid=3'"">成功交易(" & sl(3) & ")</a>"

        For i = 0 To 9
            sl(i) = 0
        Next
        Label2.Text = ""
        rs.Open("select COUNT(id) as nums,status from Orders group by status  order by status", Conn, 1, 3)
        While Not rs.EOF
            cid = IIf(System.Convert.IsDBNull(rs.Fields(1).Value), 9, rs.Fields(1).Value)
            sl(cid) = rs.Fields(0).Value
            rs.MoveNext()
        End While
        rs.Close()
        Label2.Text &= "<a " & qzui & "Orders.aspx?cid=0'"">已发货(" & sl(0) & ")</a>"
        Label2.Text &= " | <a " & qzui & "Orders.aspx?cid=1'"">未到帐(" & sl(1) & ")</a>"
        Label2.Text &= " | <a " & qzui & "Orders.aspx?cid=2'"">欠款单(" & sl(2) & ")</a>"
        Label2.Text &= " | <a " & qzui & "Orders.aspx?cid=3'"">退款单(" & sl(3) & ")</a>"
        Label2.Text &= " | <a " & qzui & "Orders.aspx?cid=4'"">已结算(" & sl(4) & ")</a>"
        Label2.Text &= " | <a " & qzui & "Orders.aspx?cid=5'"">已到帐(" & sl(5) & ")</a>"




    End Sub

End Class