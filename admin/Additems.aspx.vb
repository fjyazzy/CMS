Public Class Additems
    Inherits System.Web.UI.Page
    Public Conn As New ADODB.Connection
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cc.Connecttodb()
        Dim DBOrd As Integer
        DBOrd = Request("DBOrd")
        If Conn.State = 0 Then Conn.Open(cc.setConstr(DBOrd))

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim DBname, jg As String
        DBname = Request("DBname")

        Dim rs As New ADODB.Recordset
        rs.Open("select * from " & DBname, Conn, 1, 3)
        rs.AddNew()
        rs.Fields("itemno").Value = itemno.Text
        rs.Fields("itemname").Value = itemname.Text
        rs.Fields("itemtext").Value = itemtext.Text
        rs.Update()
        rs.Close()

        jg = "<script language = ""javascript"" >"
        jg &= "parent.location.reload();"
        jg &= "</script>"
        Response.Write(jg)

    End Sub

    Private Sub Additems_Unload(sender As Object, e As EventArgs) Handles Me.Unload
        Conn.Close()
    End Sub

End Class