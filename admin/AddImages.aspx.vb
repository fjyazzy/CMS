Imports System.IO
Imports System.Drawing
Imports System.Drawing.Imaging
Public Class AddImages
    Inherits System.Web.UI.Page
    Public Conn As New ADODB.Connection
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cc.Connecttodb()
        Conn.Open(cc.setConstr(1))
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim DBord, DBname, lx, jg As String
        DBord = Request("DBord")
        DBname = Request("DBname")
        lx = Request("lx")

        Dim rs As New ADODB.Recordset
        rs.Open("select * from " & DBname & "_images where id=" & lx, Conn, 1, 3)
        rs.AddNew()
        rs.Fields("Imgname").Value = TxtImgName.Text
        rs.Fields("Imgurl").Value = SaveImage(DBord)
        rs.Fields("Pid").Value = lx
        rs.Update()
        rs.Close()

        Conn.Close()

        jg = "<script language = ""javascript"" >"
        jg &= "window.close();"
        jg &= "</script>"
        Response.Write(jg)


    End Sub
    Private Function SaveImage(ByVal DBord As String) As String
        Dim ImagePath, nf As String
        Dim jiok As Integer = 0
        ImagePath = PicUrl & "\" & CC.DBord2path(DBord)
        nf = ""
        ' 保存原图图片，生成缩略图，制作水印，水印缩略图
        If Not File1.PostedFile Is Nothing And File1.PostedFile.ContentLength > 0 Then
            Dim fnnn As String = File1.PostedFile.FileName
            Dim SaveLocation, SaveLocation2, SaveLocation3 As String
            nf = Year(Now) & Month(Now) & Day(Now) & Hour(Now) & Minute(Now) & Second(Now) & "_" & fnnn
            If CC.CheckExt(nf) <> "" Then
                '保存原图和缩略图
                SaveLocation = ImagePath & "\" & nf
                Try
                    File1.PostedFile.SaveAs(SaveLocation)
                    Dim image As System.Drawing.Image = Image.FromFile(SaveLocation)
                    '加水印
                    Try
                        Dim g As Graphics = Graphics.FromImage(image)
                        '加文字水印，注意，这里的代码和以下加图片水印的代码不能共存
                        'g.DrawImage(image, 0, 0, image.Width, image.Height)
                        'Dim f As Font = New Font("黑体", 32)
                        '不透明
                        'Dim b As Brush = New SolidBrush(Color.White)
                        '半透明
                        'Dim b As Brush = New SolidBrush(Color.FromArgb(64, 255, 255, 255))
                        'g.DrawString("郑志勇 Zhengzhiyong 。inc", f, b, 30, 50)
                        'g.DrawString("www.chinaeds.com", f, b, 30, 90)
                        'g.Dispose()

                        '//加图片水印
                        Dim copyImage As System.Drawing.Image = Image.FromFile(ImagePath & "\sy2.gif")
                        Dim h As Single
                        h = (image.Width / copyImage.Width) * copyImage.Height
                        Dim imgOutput As New Bitmap(copyImage, image.Width, h)
                        '水印  New Single() {0, 0, 0, 0.5F, 0}
                        Dim ptsArray As Single()() = {New Single() {1, 0, 0, 0, 0}, New Single() {0, 1, 0, 0, 0}, New Single() {0, 0, 1, 0, 0}, New Single() {0, 0, 0, 0.5F, 0}, New Single() {0, 0, 0, 0, 1}}
                        Dim clrMatrix As New ColorMatrix(ptsArray)
                        Dim imgAttributes As New ImageAttributes
                        imgAttributes.SetColorMatrix(clrMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap)
                        g.DrawImage(imgOutput, New Rectangle(0, (image.Height - h) / 2, image.Width, image.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imgAttributes)
                        g.Dispose()

                        '//保存加水印过后的图片
                        nf = Year(Now) & Month(Now) & Day(Now) & Hour(Now) & Minute(Now) & Second(Now) & "_2_" & fnnn
                        SaveLocation2 = ImagePath & "\" & nf
                        image.Save(SaveLocation2)

                        jiok = 1
                    Catch Exc As Exception
                        nf = Year(Now) & Month(Now) & Day(Now) & Hour(Now) & Minute(Now) & Second(Now) & "_" & fnnn
                        Response.Write(CC.Alert("Error:这种图片不能制作水印! "))
                    End Try

                    '//制作并保存成缩略图
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

                Catch Exc As Exception
                    Response.Write(CC.Alert("Error:这种图片保存错误! " & SaveLocation & Exc.Message.ToString))
                End Try

            End If

        End If
        Return nf

    End Function

End Class