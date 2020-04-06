Public Class chgpw
    Inherits System.Web.UI.Page

    Dim Conn As New ADODB.Connection
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        cc.Connecttodb()
        If Conn.State = 0 Then Conn.Open(cc.setConstr(1))
        Dim yfm, xm As String
        yfm = Server.UrlDecode(Request.Cookies("username").Value)
        xm = Server.UrlDecode(Request.Cookies("name").Value)
        If Not IsPostBack Then
        Else
            If mm1.Text = mm2.Text Then
                Dim sql As String
                Dim rs As New ADODB.Recordset
                Select Case Request.Cookies("rylx").Value
                    Case 1
                        sql = "select  * from DGSX_XS where xsdm='" & yfm & "'"
                        rs.Open(sql, Conn, 1, 3)
                        If rs.Fields("mm").Value <> jmm.Text Then
                            rs.Close()
                            Label1.Text = ("<font color=red>旧密码错误!</font>")
                            Exit Sub
                        End If
                        rs.Fields("mm").Value = mm1.Text
                        rs.Update()
                        rs.Close()
                    Case 2, 4, 5, 6, 7, 8, 9, 11, 12, 53
                        sql = "select  * from MyUsers where xm='" & xm & "'"
                        rs.Open(sql, Conn, 1, 3)
                        If rs.Fields("mm").Value <> jmm.Text Then
                            rs.Close()
                            Label1.Text = ("<font color=red>旧密码错误!</font>")
                            Exit Sub
                        End If
                        rs.Fields("mm").Value = mm1.Text
                        rs.Update()
                        rs.Close()
                    Case 55
                        sql = "select  * from DGSX_XWFDJS where xm='" & xm & "'"
                        rs.Open(sql, Conn, 1, 3)
                        If rs.Fields("mm").Value <> jmm.Text Then
                            rs.Close()
                            Label1.Text = ("<font color=red>旧密码错误!</font>")
                            Exit Sub
                        End If
                        rs.Fields("mm").Value = mm1.Text
                        rs.Update()
                        rs.Close()
                    Case 51 '班委
                        sql = "select  * from DM_bjk where dm='" & yfm & "'"
                        rs.Open(sql, Conn, 1, 3)
                        If rs.Fields("mm").Value <> jmm.Text Then
                            rs.Close()
                            Label1.Text = ("<font color=red>旧密码错误!</font>")
                            Exit Sub
                        End If
                        rs.Fields("mm").Value = mm1.Text
                        rs.Update()
                        rs.Close()
                    Case 52 '楼委
                        sql = "select  * from GY_LHLC where dm='" & yfm & "'"
                        rs.Open(sql, Conn, 1, 3)
                        If rs.Fields("mm").Value <> jmm.Text Then
                            rs.Close()
                            Label1.Text = ("<font color=red>旧密码错误!</font>")
                            Exit Sub
                        End If
                        rs.Fields("mm").Value = mm1.Text
                        rs.Update()
                        rs.Close()


                End Select
                Label1.Text = ("<font color=red>密码修改完成!</font>")
            Else
                Label1.Text = ("<font color=red>两次新密码不同!</font>")
            End If
        End If

    End Sub
    Private Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Unload
        If Conn.State = 1 Then Conn.Close()
    End Sub


End Class