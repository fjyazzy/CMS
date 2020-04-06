Public Class csbjbbd
    Inherits System.Web.UI.Page
    Public Conn As New ADODB.Connection
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cc.Connecttodb()
        If Conn.State = 0 Then Conn.Open(cc.setConstr("4"))
        Dim Cid, dh As String
        Cid = CInt(Request("Cid"))
        dh = setMainform(Cid)
        Lbbgnr.Text = getNr(dh)

    End Sub

    Private Function setMainform(ByVal Cid As String) As String
        Dim rs As New ADODB.Recordset
        Dim dh As String = ""
        rs.Open("select * from csbjb where id=" & Cid, Conn, 1, 1)
        If rs.EOF Then
            dh = "单号不存在！"
        Else
            dh = rs.Fields("csbjbh").Value
            Lbxjdw.Text = rs.Fields("xjdw").Value
            Lbbjbh.Text = dh
            Lbgcmc.Text = rs.Fields("gcmc").Value
            Lbbjrq.Text = rs.Fields("bjsj").Value
            Lblxr.Text = rs.Fields("lxr").Value
            Lbbjrq.Text = rs.Fields("bjsj").Value
            Lbdhcz.Text = rs.Fields("dhcz").Value

            Lbbjhj1.Text = rs.Fields("total").Value
            Lbbjhj2.Text = Lbbjhj1.Text

            Lbbj.Text = rs.Fields("bj").Value
            Lbyxq.Text = rs.Fields("bjdyxq").Value
            Lbxsjl.Text = rs.Fields("xsl").Value


        End If

        rs.Close()

        Return dh
    End Function


    Private Function getNr(ByVal dh As String) As String
        Dim jg As String = ""
        '<tr>
        '<td>1项目</td>
        '<td>2类别</td>
        '<td>3人员名称</td>
        '<td>4设备名称</td>
        '<td>5配件名称</td>
        '<td>6其他</td>
        '<td>7单位</td>
        '<td>8数量</td>
        '<td>9单价</td>
        '<td>10金额</td>
        '<td>11备注</td>
        '</tr>
        Dim rs As New ADODB.Recordset
        rs.Open("select * from csbjb2 where csbjbh='" & dh & "' order by lb", Conn, 1, 1)
        While Not rs.EOF
            jg &= "<tr>"
            jg &= "<td>" & rs.Fields(1).Value & "</td>"
            jg &= "<td>" & rs.Fields(2).Value & "</td>"
            jg &= "<td>" & rs.Fields(3).Value & "</td>"
            jg &= "<td>" & rs.Fields(4).Value & "</td>"
            jg &= "<td>" & rs.Fields(5).Value & "</td>"
            jg &= "<td>" & rs.Fields(6).Value & "</td>"
            jg &= "<td>" & rs.Fields(7).Value & "</td>"
            jg &= "<td>" & rs.Fields(8).Value & "</td>"
            jg &= "<td>" & rs.Fields(9).Value & "</td>"
            jg &= "<td>" & rs.Fields(10).Value & "</td>"
            jg &= "<td>" & rs.Fields(11).Value & "</td>"
            jg &= "</tr>"

            rs.MoveNext()
        End While
        rs.Close()

        Return jg
    End Function

End Class