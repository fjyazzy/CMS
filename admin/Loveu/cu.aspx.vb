Public Partial Class cu
    Inherits System.Web.UI.Page
    Dim Conn As New ADODB.Connection
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cc.Connecttodb()
        Conn.Open(cc.setConstr(5))
        If Request("lx") = 1 Then
            CkStatus(Request("mac"))
        End If
        If Request("lx") = 2 Then
            logoff(Request("mac"))
        End If

    End Sub

    Private Sub CkStatus(ByVal strMacAddress)
        Dim strdate As String
        Dim rsx As New ADODB.Recordset
        strdate = Format(Now(), "yyyyMMdd")
        rsx.Open("SELECT * FROM useronline where MacAddress='" & strMacAddress & "'", Conn, 1, 3)
        If rsx.EOF Then
            rsx.AddNew()
            rsx.Fields(1).Value = strMacAddress
            rsx.Fields(2).Value = true
            rsx.Fields(3).Value = Now
            rsx.Fields(4).Value = ""
            rsx.Update()
        Else
            If rsx.Fields(2).Value = False Then
                rsx.Fields(2).Value = true
                rsx.Fields(3).Value = Now
                rsx.Fields(4).Value = ""
                rsx.Update()
            End If
        End If
        rsx.Close()

    End Sub

    Private Sub logoff(ByVal strMacAddress)
        Dim strdate As String
        Dim rsx As New ADODB.Recordset
        strdate = Format(Now(), "yyyyMMdd")
        rsx.Open("SELECT * FROM useronline where MacAddress='" & strMacAddress & "'", Conn, 1, 3)
        If rsx.EOF Then
            rsx.AddNew()
            rsx.Fields(1).Value = strMacAddress
            rsx.Fields(2).Value = False
            rsx.Fields(4).Value = Now
            rsx.Update()
        Else
            rsx.Fields(2).Value = False
            rsx.Fields(4).Value = Now
            rsx.Update()
        End If
        rsx.Close()

    End Sub

End Class