Public Partial Class hi
    Inherits System.Web.UI.Page
    Dim Conn As New ADODB.Connection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        cc.Connecttodb()
        Conn.Open(cc.setConstr(5))
        'Response.Write(GetStatus(1))
        If GetStatus(1, Request("mac")) = False Then
            Response.Write(ShowTXT())
        End If

    End Sub

    Private Function ShowTXT() As String
        Dim jg As String
        jg = ""
        Dim rsx As New ADODB.Recordset
        rsx = Conn.Execute("SELECT TOP 1 * FROM meiyan ORDER BY RND(id)")
        jg = rsx.Fields(1).Value
        rsx.Close()
        Return jg
    End Function

    Private Function GetStatus(ByVal n As Integer, ByVal strMacAddress As String) As Boolean
        Dim jg As Boolean
        Dim strdate As String
        Dim rsx As New ADODB.Recordset
        strdate = Format(Now(), "yyyyMMdd")
        rsx.Open("SELECT * FROM pushtype where MacAddress='" & strMacAddress & "' and lx=" & n & " and  sj='" & strdate & "'", Conn, 1, 3)
        If rsx.EOF Then
            rsx.AddNew()
            rsx.Fields(1).Value = strdate
            rsx.Fields(2).Value = n
            rsx.Fields(3).Value = true
            rsx.Fields(4).Value = strMacAddress
            rsx.Update()
            jg = False
        Else
            jg = rsx.Fields(3).Value
            If jg = False Then
                rsx.Fields(3).Value = true
                rsx.Update()
            End If
        End If
        rsx.Close()

        Return jg
    End Function

End Class