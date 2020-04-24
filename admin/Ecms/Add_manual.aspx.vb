Imports System.IO
Public Class Add_manual
    Inherits System.Web.UI.Page
    Dim DbName As String
    Dim Conn As New ADODB.Connection
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '在此处放置初始化No的用户代码
        DbName = "Manual"

        If Request("v") = 1 Then
            Button1.Visible = False
            Button3.Visible = False
        End If

        If CC.getQx(Request.Cookies("Username").Value, "3002") = 0 Then
            Response.Write("没有权限")
            Response.End()
        End If
        If Conn.State = 0 Then Conn.Open(CC.setConstr(DBord_ecms))
        CC.getSoftinfo(DBord_ecms)
        lLink.Attributes.Add("href", "../../cmscss/home/style" & SYSTEMSTYLE & ".css")


        If Request("gn") = "del" And Request("id") <> "" Then
            Conn.Execute("delete from manual where id=" & CC.Checkstr(Request("id")))
            Response.Write("删除成功！")
            Response.End()
        End If


        If Not IsPostBack Then
            SetCategoryIDlist()
            editone(CC.Checkstr(Request("id")))
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

            '显示资料文件名
            Label1.Text = ShowAtt(rs.Fields("Furl").Value)

            rs.Close()
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
            point.Text = "0"
            clicknum.Text = "0"
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
        Else
            sql = "select  * from " & DbName & " where id=" & CC.Checkstr(Request("id"))
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
                Catch ex As Exception
                End Try
            End If
        Next


        '保存资料文件
        If Not File1.PostedFile Is Nothing And File1.PostedFile.ContentLength > 0 Then
            Dim filename, fsize, jg As String
            Dim mPath As String = ""
            mPath = Center_ManualUrl & "\" & CC.DBord2path(DBord_ecms)

            filename = CC.CheckFileName(System.IO.Path.GetFileName(File1.PostedFile.FileName))
            fsize = FormatNumber(File1.PostedFile.ContentLength / 1024, 1) & "k"
            File1.PostedFile.SaveAs(mPath & "/" & filename)
            jg = filename & "|" & fsize
            rs.Fields("Furl").Value = jg
        End If

        Try
            rs.Fields("ywy").Value = Server.UrlDecode(Request.Cookies("name").Value)
            rs.Fields("uptime").Value = Now
            rs.Update()
        Catch ex As Exception
            rs.CancelUpdate()
            Response.Write(CC.Alert("字段内容没有填写完整!建议检查是否遗漏输入的内容!"))
        End Try
        rs.Close()
        Response.Write(CC.ClosePage)
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim rs As New ADODB.Recordset
        Dim sql As String
        sql = "select top 1  * from " & DbName & ""
        rs.Open(sql, Conn, 1, 3)
        rs.AddNew()
        rs.Fields("furl").Value = ""
        rs.Fields("fsize").Value = ""
        rs.Fields("categoryid").Value = categoryid.SelectedValue
        rs.Fields("type").Value = Type.Text
        rs.Fields("manufactory").Value = manufactory.Text
        rs.Fields("ywy").Value = Server.UrlDecode(Request.Cookies("username").Value)
        If IsNumeric(clicknum.Text) = False Then
            rs.Fields("clicknum").Value = "0"
        Else
            rs.Fields("clicknum").Value = clicknum.Text
        End If
        If IsNumeric(point.Text) = False Then
            rs.Fields("point").Value = "0"
        Else
            rs.Fields("point").Value = point.Text
        End If
        rs.Fields("shortdesc").Value = shortdesc.Text
        rs.Fields("uptime").Value = Now
        rs.Fields("VisitTime").Value = Now


        rs.Update()
        rs.Close()
        Response.Write(CC.ClosePage)

    End Sub
    Private Sub SetCategoryIDlist()
        Dim rs As New ADODB.Recordset
        rs = Conn.Execute("select * from Category_manuals order by id desc")
        While Not rs.EOF
            categoryid.Items.Add(New ListItem(rs.Fields("catalogname").Value, rs.Fields("id").Value))
            rs.MoveNext()
        End While
        rs.Close()
    End Sub

    Function ShowAtt(ByVal s)
        Dim f As Object
        Dim i As Integer
        Dim jg As String
        Dim mPath As String = ""
        Dim murl As String = ""

        mPath = Center_ManualUrl & "\" & CC.DBord2path(DBord_ecms)
        murl = "..\..\Center_manuals\" & CC.DBord2path(DBord_ecms)

        f = Split(s, "|")
        jg = "<ul>"
        For i = 0 To UBound(f) - 1 Step 2
            If File.Exists(mPath & "/" & f(i)) Then
                jg &= "<li><a target=_blank href=" & murl & "\" & f(i) & ">" & f(i) & "(" & f(i + 1) & ")</a>"
            Else
                jg &= "<li>服务器上资料文件不存在，请重新上传！"
            End If
        Next
        jg &= "<ul>"
        Return jg

    End Function

End Class