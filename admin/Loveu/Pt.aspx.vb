Public Partial Class Pt
    Inherits System.Web.UI.Page
    Dim Conn As New ADODB.Connection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cc.Connecttodb()
        Conn.Open(cc.setConstr(5))
        If Request("c") = "" Then
            Response.Write(gt(Request("mac"), Request("ln")))
        Else
            pt(Request("mac"), Request("c"))
        End If
    End Sub
    Private Sub pt(ByVal mac, ByVal c)
        Dim rsx As New ADODB.Recordset
        rsx.Open("SELECT top 1  * FROM replyMessage ", Conn, 1, 3)
        rsx.AddNew()
        rsx.Fields(1).Value = mac
        rsx.Fields(2).Value = Now
        rsx.Fields(3).Value = c
        rsx.Update()
        rsx.Close()
    End Sub
    Private Function gt(ByVal mac, ByVal ln) As String
        Dim jg As String
        Dim rsx As New ADODB.Recordset
        jg = ""
        rsx.Open("SELECT * FROM replyMessage where id>" & ln & " order by id desc ", Conn, 1, 3)
        While Not rsx.EOF
            jg = rsx.Fields(1).Value & "(" & rsx.Fields(2).Value & "):" & rsx.Fields(3).Value & vbCrLf & jg
            rsx.MoveNext()
        End While
        rsx.Close()
        Return jg
    End Function

End Class