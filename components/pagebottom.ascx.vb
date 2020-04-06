Public Class pagebottom
    Inherits System.Web.UI.UserControl
    Property DBOrd() As String
        Get
            Return Me.ViewState("DbOrd")
        End Get
        Set(ByVal value As String)
            Me.ViewState("DbOrd") = value
        End Set
    End Property

    Dim Conn As New ADODB.Connection
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cc.Connecttodb()
        Conn.Open(cc.setConstr(DBOrd))
        Dim rs As New ADODB.Recordset
        Dim jg As String
        Dim i As Integer
        i = 1
        jg = "<div class=""footer""><br/>"
        rs.Open("select * from department order by id", Conn, 1, 1)
        While Not rs.EOF
            Select Case rs.Fields("itemno").Value
                Case "001", "002", "003", "004"
                    jg &= rs.Fields("itemname").Value & ":" & rs.Fields("itemtext").Value & "&nbsp;&nbsp;"
                    If i = 2 Then
                        i = 1
                        jg &= "<br />"
                    Else
                        i = i + 1
                    End If
                Case Else
            End Select
            rs.MoveNext()
        End While
        rs.Close()
        jg &= "<br /></div>"

        Label1.Text = jg

    End Sub

End Class