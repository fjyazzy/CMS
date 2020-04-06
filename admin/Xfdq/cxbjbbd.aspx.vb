Public Class cxbjbbd
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
        rs.Open("select * from cxbjb where id=" & Cid, Conn, 1, 1)
        If rs.EOF Then
            dh = "单号不存在！"
        Else
            dh = rs.Fields("cxbjbh").Value
            Lbsxdh.Text = dh
            Lbsxdw.Text = rs.Fields("sxdw").Value
            Lbsxrq.Text = rs.Fields("sxrq").Value
            Lbmpsj.Text = rs.Fields("mpsj").Value
            Lbxzms.Text = rs.Fields("xzms").Value

            Lbzj1.Text = rs.Fields("zj").Value
            Lbzj2.Text = Lbzj1.Text

            Lbbz.Text = rs.Fields("bz").Value

        End If

        rs.Close()

        Return dh
    End Function


    Private Function getNr(ByVal dh As String) As String
        Dim jg As String = ""
        '<tr>
        '<td>序号</td>
        '<td>类型</td>
        '<td>名称</td>
        '<td>重/数量</td>
        '<td>单价(元)</td>
        '<td>金额（元）</td>
        '<td>备注</td>
        '</tr>

        Dim rs As New ADODB.Recordset
        rs.Open("select * from cxbjb2 where cxbjbh='" & dh & "' order by lx", Conn, 1, 1)
        While Not rs.EOF
            jg &= "<tr>"
            jg &= "<td>" & rs.Fields(1).Value & "</td>"
            jg &= "<td>" & rs.Fields(2).Value & "</td>"
            jg &= "<td>" & rs.Fields(3).Value & "</td>"
            jg &= "<td>" & rs.Fields(4).Value & "</td>"
            jg &= "<td>" & rs.Fields(5).Value & "</td>"
            jg &= "<td>" & rs.Fields(6).Value & "</td>"
            jg &= "<td>" & rs.Fields(7).Value & "</td>"
            jg &= "</tr>"

            rs.MoveNext()
        End While
        rs.Close()

        Return jg
    End Function


End Class