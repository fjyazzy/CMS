Imports System.IO
Imports System.Drawing
Imports System.Drawing.Imaging
Public Class Add_E_Products
    Inherits System.Web.UI.Page

    Dim DbName As String
    Dim Conn As New ADODB.Connection
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
        '在此处放置初始化No的用户代码
        DbName = "Ecms_products"

        If Request("v") = 1 Then
            Button2.Visible = False
            Button5.Visible = False
        End If

        If cc.getQx(Request.Cookies("Username").Value, "3001") = 0 Then
            Response.Write("没有权限")
            Response.End()
        End If
        If Conn.State = 0 Then Conn.Open(CC.setConstr(DBord_ecms))
        CC.getSoftinfo(DBord_ecms)
        lLink.Attributes.Add("href", "../../cmscss/home/style" & SYSTEMSTYLE & ".css")

        If Not IsPostBack Then
            SetCategoryIDlist()
            editone(cc.Checkstr(Request("id")))
        End If

    End Sub
    Private Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Unload
        If Conn.State = 1 Then Conn.Close()
    End Sub
    Private Sub editone(ByVal id As Integer)
        Dim cid As Integer
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
            cid = rs.Fields("Categoryid").Value
            Label1.Text = Zllist(rs.Fields("type").Value)
            Dim Imageurl As String
            Imageurl = "..\..\Center_Photos\" & CC.DBord2path(DBord_ecms)
            Label2.Text = "<a href='" & Imageurl & "/" & rs.Fields("productimg").Value & "' target=_blank align=center><img src='" & Imageurl & "/t_" & rs.Fields("productimg").Value & "' border=0></a>"
            rs.Close()

            SetCategoryIDlist()
            categoryid.SelectedValue = cid

            Ywy.Text = Server.UrlDecode(Request.Cookies("name").Value)
            uptime.Text = Now


            Button2.Text = "保存编辑"
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

            Ywy.Text = Server.UrlDecode(Request.Cookies("name").Value)
            uptime.Text = Now
            ywy_2.Text = Server.UrlDecode(Request.Cookies("name").Value)
            uptime_2.Text = Now

            Button2.Text = "保存添加"
        End If
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim sql, fd As String
        Dim rs As New ADODB.Recordset
        If Button2.Text = "保存添加" Then
            sql = "select top 1  * from " & DbName & ""
            rs.Open(sql, Conn, 1, 3)
            rs.AddNew()
            rs.Fields("isdel").Value = "0"
            fd = ""
        Else
            sql = "select  * from " & DbName & " where id=" & cc.Checkstr(Request("id"))
            rs.Open(sql, Conn, 1, 3)
            If System.Convert.IsDBNull(rs.Fields("productImg").Value) = True Then
                fd = ""
            Else
                fd = rs.Fields("productImg").Value
            End If

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


        ' 保存原图图片，生成缩略图，制作水印，水印缩略图
        If Not File1.PostedFile Is Nothing And File1.PostedFile.ContentLength > 0 Then
            Dim ImagePath As String = ""
            Dim Imageurl As String = ""
            Dim jiok As Integer = 0

            ImagePath = Center_PicUrl & "\" & CC.DBord2path(DBord_ecms)
            Imageurl = "..\..\Center_Photos\" & CC.DBord2path(DBord_ecms)

            '删除原来的产品图片
            If Len(fd) > 5 Then
                System.IO.File.Delete(ImagePath & "\t_" & fd)
                System.IO.File.Delete(ImagePath & "\" & fd)
            End If

            Dim fnnn As String
            fnnn = CC.CheckFileName(Type.Text)
            Dim SaveLocation, SaveLocation2, SaveLocation3, nf As String
            nf = Year(Now) & Month(Now) & Day(Now) & Hour(Now) & Minute(Now) & Second(Now) & "_" & fnnn & "_" & CC.CheckFileName(System.IO.Path.GetFileName(File1.PostedFile.FileName))
            If CC.CheckExt(nf) <> "" Then
                '保存原图和缩略图
                SaveLocation = ImagePath & "/" & nf
                Try
                    File1.PostedFile.SaveAs(SaveLocation)
                    '//保存成缩略图
                    Dim image As System.Drawing.Image = Image.FromFile(SaveLocation)
                    '加水印
                    Try
                        Dim g As Graphics = Graphics.FromImage(image)
                        '//加图片水印
                        Dim copyImage As System.Drawing.Image = Image.FromFile(ImagePath + "/logo_sy.gif")
                        Dim h As Single
                        h = (image.Width / copyImage.Width) * copyImage.Height
                        Dim imgOutput As New Bitmap(copyImage, image.Width, h)
                        '水印  New Single() {0, 0, 0, 0.5F, 0}
                        Dim ptsArray As Single()() = {New Single() {1, 0, 0, 0, 0}, New Single() {0, 1, 0, 0, 0}, New Single() {0, 0, 1, 0, 0}, New Single() {0, 0, 0, 0.5F, 0}, New Single() {0, 0, 0, 0, 1}}
                        Dim clrMatrix As New ColorMatrix(ptsArray)
                        Dim imgAttributes As New ImageAttributes
                        imgAttributes.SetColorMatrix(clrMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap)
                        '居中
                        'g.DrawImage(imgOutput, New Rectangle(0, (image.Height - h) / 2, image.Width, image.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imgAttributes)
                        '置底
                        g.DrawImage(imgOutput, New Rectangle(0, image.Height - copyImage.Height * 2, image.Width, image.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imgAttributes)
                        '不放大水印
                        'g.DrawImage(imgOutput, New Rectangle(0, image.Height - copyImage.Height * 2, copyImage.Width, copyImage.Height), 0, 0, copyImage.Width, copyImage.Height, GraphicsUnit.Pixel, imgAttributes)
                        g.Dispose()

                        '//保存加水印过后的图片,删除原始图片
                        nf = Year(Now) & Month(Now) & Day(Now) & Hour(Now) & Minute(Now) & Second(Now) & "_2_" & fnnn & "_" & CC.CheckFileName(System.IO.Path.GetFileName(File1.PostedFile.FileName))
                        SaveLocation2 = ImagePath & "/" & nf
                        image.Save(SaveLocation2)

                        jiok = 1

                    Catch Exc As Exception
                        nf = Year(Now) & Month(Now) & Day(Now) & Hour(Now) & Minute(Now) & Second(Now) & "_" & fnnn & "_" & CC.CheckFileName(System.IO.Path.GetFileName(File1.PostedFile.FileName))
                        Response.Write(CC.Alert("Error:这种图片不能制作水印! " & Exc.Message))
                    End Try

                    Dim thisFormat = image.RawFormat
                    Dim imgOutput2 As New Bitmap(image, 100, 100)
                    SaveLocation3 = ImagePath & "\T_" & nf
                    imgOutput2.Save(SaveLocation3, thisFormat)
                    imgOutput2.Dispose()
                    image.Dispose()

                    If jiok = 1 Then
                        '删除原图片
                        System.IO.File.Delete(SaveLocation)
                    End If

                    Label2.Text = "<a href='" & Imageurl & "/" & nf & "' target=_blank align=center><img src='" & Center_PicUrl & "/t_" & nf & "' border=0></a>"
                    rs.Fields("productImg").Value = nf
                Catch Exc As Exception
                    Response.Write(CC.Alert("Error:这种图片保存错误! "))
                End Try

            End If

        End If

        rs.Update()
        rs.Close()

        ' cc.ProductIntoManual(Type.Text)
        Response.Write(cc.ClosePage)

    End Sub
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim rs As New ADODB.Recordset
        Dim sql As String
        sql = "select top 1  * from " & DbName & ""
        rs.Open(sql, Conn, 1, 3)
        rs.AddNew()
        rs.Fields("productImg").Value = ""
        rs.Fields("categoryid").Value = categoryid.SelectedValue
        rs.Fields("type").Value = Type.Text
        rs.Fields("manufactory").Value = manufactory.Text
        rs.Fields("fz").Value = fz.Text
        rs.Fields("sl").Value = "1"
        rs.Fields("qssl").Value = qssl.Text
        rs.Fields("ph").Value = ph.Text
        rs.Fields("shortdesc").Value = shortdesc.Text
        rs.Fields("price").Value = price.Text
        rs.Fields("productDesc").Value = Productdesc.Text
        rs.Fields("uptime").Value = Now
        rs.Fields("ywy").Value = Server.UrlDecode(Request.Cookies("username").Value)
        rs.Fields("cgxx").Value = Cgxx.Text
        rs.Fields("cgjl").Value = Cgjl.Text
        rs.Fields("isdel").Value = 0
        rs.Fields("Visittime").Value = Now

        rs.Fields("ghsid").Value = Ghsxx.Text
        rs.Update()
        rs.Close()
        Response.Write(cc.Alert("克隆完成!"))
        Response.Write(cc.ClosePage)

    End Sub

    Private Sub SetCategoryIDlist()

        categoryid.Items.Clear()
        Dim rs As New ADODB.Recordset
        rs = Conn.Execute("select * from Category_products order by id desc")
        While Not rs.EOF
            categoryid.Items.Add(New ListItem(rs.Fields("catalogname").Value, rs.Fields("id").Value))
            rs.MoveNext()
        End While
        rs.Close()

    End Sub


    Function Zllist(ByVal x As String) As String
        Dim sql, jg As String
        Dim rs As New ADODB.Recordset
        sql = "SELECT top 5 * FROM Manual where type like '%" & x & "%'"
        rs.Open(sql, Conn, 1, 1)
        jg = "<ul>"
        While Not rs.EOF
            jg &= "<li>" & rs.Fields("type").Value & " -<a href=add_manual.aspx?id=" & rs.Fields("id").Value & ">编辑</a>-<a href=add_manual.aspx?gn=del&id=" & rs.Fields("id").Value & ">删除</a>" & ShowAtt(rs.Fields("furl").Value)
            rs.MoveNext()
        End While
        rs.Close()
        jg &= "</ul>"

        Return jg

    End Function
    Function ShowAtt(ByVal s As String) As String
        Dim f() As String
        Dim i As Integer
        Dim jg As String = ""
        f = Split(s, "|")
        For i = 0 To UBound(f) - 1 Step 2
            If Left(f(i), 1) = "\" Then
                jg &= "|<a target=_blank href=""" & "/DOC" & f(i) & """>" & f(i) & "(" & f(i + 1) & ")</a>"
            Else
                If File.Exists(Server.MapPath("../../manual/" & f(i))) Then
                    jg &= "|<a target=_blank href=""" & "../../manual/" & f(i) & """>" & f(i) & "(" & f(i + 1) & ")</a>"
                Else
                    jg &= "|<a target=_blank href=""http://www.baidu.com/s?tn=zzy71616_pg&wd=" & f(i) & """>" & f(i) & "(" & f(i + 1) & ")</a>"
                End If
            End If
        Next
        Return jg

    End Function



End Class