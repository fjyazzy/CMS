Public Class score
    Inherits System.Web.UI.Page
    Dim Conn As New ADODB.Connection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If cc.getQx(Request.Cookies("Username").Value, "6001") = 0 Then
            Response.Write("没有权限")
            Response.End()
        End If

        cc.Connecttodb()
        If Conn.State = 0 Then Conn.Open(cc.setConstr(3))
        cc.getSoftinfo("1")
        lLink.Attributes.Add("href", "../../cmscss/home/style" & SYSTEMSTYLE & ".css")
        If Not IsPostBack Then
            getkmm()
            getbjm()
        End If

        tadata.TjExpression = " km='" & DDKmm.SelectedValue & "' and bjh='" & DDBJM.SelectedValue & "' "

    End Sub
    Private Sub getkmm()
        Dim rs As New ADODB.Recordset
        Dim sql As String
        DDKmm.Items.Clear()
        sql = "select * from exam"
        rs.Open(sql, Conn, 1, 1)
        While Not rs.EOF
            Dim ll As New ListItem
            ll.Text = rs.Fields("examname").Value
            ll.Value = rs.Fields("examno").Value
            DDKmm.Items.Add(ll)
            rs.MoveNext()
        End While
        rs.Close()

    End Sub
    Private Sub getbjm()
        Dim rs As New ADODB.Recordset
        Dim sql As String
        DDBJM.Items.Clear()
        sql = "select * from exam_bj"
        rs.Open(sql, Conn, 1, 1)
        While Not rs.EOF
            Dim ll As New ListItem
            ll.Text = rs.Fields("bjm").Value
            ll.Value = rs.Fields("bjh").Value
            DDBJM.Items.Add(ll)
            rs.MoveNext()
        End While
        rs.Close()

    End Sub


End Class