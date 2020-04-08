Public Class Add_Orders
    Inherits System.Web.UI.Page

    Dim DbName As String
    Dim Conn As New ADODB.Connection
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '在此处放置初始化No的用户代码
        DbName = "Orders"

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
            YHZHlist()
            FHFSlist()
            editone(cc.Checkstr(Request("id")))
        End If
    End Sub
    Private Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Unload
        If Conn.State = 1 Then Conn.Close()
    End Sub
    Private Sub editone(ByVal id)
        If id > 0 Then
            Dim sql As String
            Dim rs As New ADODB.Recordset
            sql = "select  * from " & DbName & " where id=" & id
            rs.Open(sql, Conn, 1, 1)
            Dim i, k As Integer
            For i = 1 To Page.Controls.Count - 1
                If Page.Controls(i).Controls.Count > 0 Then
                    k = i
                End If
            Next

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

            Label1.Text = "毛利:" & FormatNumber(CSng(jzje.Text) - CSng(cgje.Text) - CSng(yf.Text) - CSng(Tax.Text), 2)
            tdy.Visible = True


            rs.Close()
            Button1.Text = "保存编辑"
            Button3.Attributes.Add("onclick", "window.open('kdd.aspx?id=" & id & "')")

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
                Try
                    rs.Fields(Page.Controls(k).Controls(i).ClientID.ToString).Value = txbk.Text
                Catch ex As Exception
                End Try
            End If
            If (Page.Controls(k).Controls(i).GetType.ToString = "System.Web.UI.WebControls.DropDownList") Then
                Dim Ddbk As DropDownList = Page.Controls(k).Controls(i)
                Try
                    rs.Fields(Page.Controls(k).Controls(i).ClientID.ToString).Value = Ddbk.SelectedValue
                Catch
                End Try
            End If

        Next

        Try
            If Button1.Text = "保存添加" Then
                rs.Fields("tdy").Value = Server.UrlDecode(Request.Cookies("name").Value)
            End If
            rs.Fields("tdsj").Value = Now
            rs.Update()
        Catch ex As Exception
            rs.CancelUpdate()
            Response.Write(cc.Alert("字段内容没有填写完整!建议检查是否遗漏输入的内容!"))
        End Try
        rs.Close()
        Response.Write(cc.ClosePage)

    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim rs As New ADODB.Recordset
        Dim sql As String
        sql = "select top 1  * from " & DbName & ""
        rs.Open(sql, Conn, 1, 3)
        rs.AddNew()
        rs.Fields("hkrq").Value = hkrq.Text
        rs.Fields("hklx").Value = hklx.SelectedValue
        rs.Fields("tdy").Value = Server.UrlDecode(Request.Cookies("username").Value)
        rs.Fields("tdsj").Value = Now

        rs.Fields("fhfs").Value = fhfs.SelectedValue
        rs.Fields("ydh").Value = ydh.Text
        rs.Fields("jzje").Value = jzje.Text
        rs.Fields("xm").Value = xm.Text
        rs.Fields("phone").Value = phone.Text
        rs.Fields("fax").Value = fax.Text
        rs.Fields("mobile").Value = mobile.Text
        rs.Fields("yf").Value = yf.Text
        rs.Fields("cgje").Value = cgje.Text
        rs.Fields("ml").Value = CSng(jzje.Text) - CSng(cgje.Text) - CSng(yf.Text)

        rs.Fields("fhfy").Value = fhfy.Text
        rs.Fields("fhsj").Value = fhsj.Text
        rs.Fields("yhjsfs").Value = yhjsfs.Text
        rs.Fields("company").Value = company.Text
        rs.Fields("address").Value = Address.Text
        rs.Fields("bz").Value = bz.Text
        rs.Fields("status").Value = Status.SelectedValue
        rs.Fields("isdel").Value = 0

        rs.Update()
        rs.Close()
        Response.Write("<script language=javascript>alert(""克隆完成!"")</script>")
        Response.Write(cc.ClosePage)

    End Sub
    Sub YHZHlist()
        Dim rs As New ADODB.Recordset
        rs = Conn.Execute("select * from webpages where lxbh='014' order by id desc")
        While Not rs.EOF
            hklx.Items.Add(New ListItem(rs.Fields("xm").Value, rs.Fields("xm").Value))
            rs.MoveNext()
        End While
        rs.Close()
    End Sub
    Sub FHFSlist()
        Dim rs As New ADODB.Recordset
        rs = Conn.Execute("select * from webpages where lxbh='012' order by id desc")
        While Not rs.EOF
            fhfs.Items.Add(New ListItem(rs.Fields("xm").Value, rs.Fields("xm").Value))
            rs.MoveNext()
        End While
        rs.Close()
    End Sub


End Class