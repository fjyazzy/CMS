Public Class Add_inquirys
    Inherits System.Web.UI.Page
    Dim DbName As String
    Dim Conn As New ADODB.Connection
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '在此处放置初始化No的用户代码
        DbName = "Inquirys"

        If Request("v") = 1 Then
            Button1.Visible = False
            Button2.Visible = False
            Button3.Visible = False
        End If


        If cc.getQx(Request.Cookies("Username").Value, "3005") = 0 Then
            Response.Write("没有权限")
            Response.End()
        End If
        If Conn.State = 0 Then Conn.Open(cc.setConstr(DBord_ecms))
        cc.getSoftinfo(dbord_ecms)
        lLink.Attributes.Add("href", "../../cmscss/home/style" & SYSTEMSTYLE & ".css")


        If Not IsPostBack Then
            editone(cc.Checkstr(Request("id")))
        End If
    End Sub
    Private Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Unload
        If Conn.State = 1 Then Conn.Close()
    End Sub
    Private Sub editone(ByVal id)
        If id > 0 Then
            Dim sql As String = ""
            Dim rs As New ADODB.Recordset
            Try
                Dim i, k As Integer
                For i = 1 To Page.Controls.Count - 1
                    If Page.Controls(i).Controls.Count > 0 Then
                        k = i
                    End If
                Next
                sql = "select  * from " & DbName & " where id=" & id
                rs.Open(sql, Conn, 1, 1)
                For i = 0 To Page.Controls(k).Controls.Count - 1
                    If (Page.Controls(k).Controls(i).GetType.ToString = "System.Web.UI.WebControls.TextBox") Then
                        Dim txbk As TextBox = Page.Controls(k).Controls(i)
                        Try
                            txbk.Text = rs.Fields(Page.Controls(k).Controls(i).ClientID.ToString).Value
                        Catch ex As Exception
                        End Try
                    End If
                    If (Page.Controls(k).Controls(i).GetType.ToString = "System.Web.UI.WebControls.DropDownList") Then
                        Dim Ddbk As DropDownList = Page.Controls(k).Controls(i)
                        Try
                            Ddbk.SelectedValue = rs.Fields(Page.Controls(k).Controls(i).ClientID.ToString).Value
                        Catch
                        End Try
                    End If

                Next
                rs.Close()
            Catch ex As Exception
                Response.Write(sql)
            End Try
            Button1.Text = "保存编辑"
        Else
            Dim i, k As Integer
            For i = 1 To Page.Controls.Count - 1
                If Page.Controls(i).Controls.Count > 0 Then
                    k = i
                End If
            Next
            For i = 0 To Page.Controls(k).Controls.Count - 1
                If (Page.Controls(k).Controls(i).GetType.ToString = "System.Web.UI.WebControls.TextBox") Then
                    Dim txbk As TextBox = Page.Controls(k).Controls(i)
                    txbk.Text = ""
                End If
            Next
            Button1.Text = "保存添加"
        End If
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim sql As String
        Dim rs As New ADODB.Recordset
        If Button1.Text = "保存添加" Then
            sql = "select top 1  * from " & DbName & ""
            rs.Open(sql, Conn, 1, 3)
            rs.AddNew()
            rs.Fields("isdel").Value = "0"
        Else
            sql = "select  * from " & DbName & " where id=" & cc.Checkstr(Request("id"))
            rs.Open(sql, Conn, 1, 3)
        End If
        Dim i, k As Integer
        For i = 1 To Page.Controls.Count - 1
            If Page.Controls(i).Controls.Count > 0 Then
                k = i
            End If
        Next
        For i = 0 To Page.Controls(k).Controls.Count - 1
            If (Page.Controls(k).Controls(i).GetType.ToString = "System.Web.UI.WebControls.TextBox") Then
                Dim txbk As TextBox = Page.Controls(k).Controls(i)
                rs.Fields(Page.Controls(k).Controls(i).ClientID.ToString).Value = txbk.Text
            End If
            If (Page.Controls(k).Controls(i).GetType.ToString = "System.Web.UI.WebControls.DropDownList") Then
                Dim Ddbk As DropDownList = Page.Controls(k).Controls(i)
                Try
                    rs.Fields(Page.Controls(k).Controls(i).ClientID.ToString).Value = Ddbk.SelectedValue
                Catch ex As Exception
                End Try
            End If

        Next

        Try
            rs.Fields("ywy").Value = Server.UrlDecode(Request.Cookies("name").Value)
            rs.Fields("bjsj").Value = Now
            rs.Update()
        Catch ex As Exception
            rs.CancelUpdate()
            Response.Write(cc.Alert("字段内容没有填写完整!建议检查是否遗漏输入的内容!"))
        End Try
        rs.Close()
        Response.Write("<script language=javascript>window.parent.mainx.location.href='order.aspx'</script>")
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim rs As New ADODB.Recordset
        Dim sql As String
        sql = "select top 1  * from " & DbName & ""
        rs.Open(sql, Conn, 1, 3)
        rs.AddNew()

        rs.Fields("type").Value = type.Text
        rs.Fields("sl").Value = sl.Text
        rs.Fields("name").Value = name.Text
        rs.Fields("email").Value = email.Text
        rs.Fields("phone").Value = phone.Text
        rs.Fields("manufactory").Value = manufactory.Text
        rs.Fields("fz").Value = fz.Text
        rs.Fields("phone").Value = phone.Text
        rs.Fields("fax").Value = fax.Text
        rs.Fields("jsj").Value = jsj.Text
        rs.Fields("ph").Value = ph.Text
        rs.Fields("mobile").Value = ""
        rs.Fields("khbz").Value = khbz.Text
        rs.Fields("address").Value = Address.Text
        rs.Fields("ywbz").Value = ywbz.Text

        rs.Fields("hyqk").Value = hyqk.Text
        rs.Fields("cgjl").Value = ""

        rs.Fields("bj").Value = bj.Text
        If rs.Fields("bjsj").Value = "" Then
            rs.Fields("bjsj").Value = Now
        End If
        rs.Fields("zycd").Value = zycd.SelectedValue
        rs.Fields("company").Value = company.Text
        If rs.Fields("ywy").Value = "" Then
            rs.Fields("ywy").Value = Server.UrlDecode(Request.Cookies("name").Value)
        End If
        rs.Fields("status").Value = Status.SelectedValue
        rs.Fields("gxsj").Value = Now
        rs.Fields("isdel").Value = 0

        rs.Update()
        rs.Close()

        Response.Write("<script language=javascript>alert(""克隆完成!"");</script>")
        Response.Write(cc.ClosePage)

    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim rs As New ADODB.Recordset
        Dim sql As String
        sql = "select top 1  * from ywd"
        rs.Open(sql, Conn, 1, 3)
        rs.AddNew()
        rs.Fields("hkrq").Value = ""
        rs.Fields("hklx").Value = ""
        rs.Fields("tdy").Value = Server.UrlDecode(Request.Cookies("name").Value)
        rs.Fields("tdsj").Value = Now
        rs.Fields("fhfs").Value = "申通快递"
        rs.Fields("ydh").Value = ""
        Try
            rs.Fields("jzje").Value = FormatNumber(CSng(bj.Text) * CSng(sl.Text), 2)
        Catch
            rs.Fields("jzje").Value = "0"
        End Try

        rs.Fields("xm").Value = name.Text
        rs.Fields("phone").Value = phone.Text
        rs.Fields("fax").Value = fax.Text
        rs.Fields("mobile").Value = ""
        rs.Fields("yf").Value = "0"
        rs.Fields("cgje").Value = "0"
        rs.Fields("ml").Value = "0"
        rs.Fields("fhfy").Value = khbz.Text
        rs.Fields("fhsj").Value = ""
        rs.Fields("yhjsfs").Value = "0"
        rs.Fields("company").Value = company.Text
        rs.Fields("address").Value = Address.Text

        rs.Fields("bz").Value = type.Text & ":" & manufactory.Text & " " & ph.Text & " " & fz.Text & " " & sl.Text
        rs.Fields("status").Value = "1"
        rs.Fields("isdel").Value = 0

        rs.Update()
        rs.Close()
        Response.Write("<script language=javascript>alert(""定单生成完成!"")</script>")

    End Sub


End Class