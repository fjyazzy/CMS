Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text
Imports System.Drawing.Image
Imports System.Drawing.Imaging
Public Class vcode
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim code As String = GenerateVCodeImage()
        Session("VCode") = code
    End Sub
    ''' 产生随机数（包含字母与数字）用于校验码
    Private Function generateVCode(ByVal CodeLength As Integer) As String
        Dim VCode As String = String.Empty
        Dim randObj As New Random
        Dim c As Integer = 63
        For i As Byte = 1 To CodeLength
            c = randObj.Next(10)
            'While (c = 0)
            'c = randObj.Next(10)
            'End While
            If c >= 10 Then
                c += 7
            End If
            c += 48
            VCode += Chr(c)
        Next
        Return VCode
    End Function
    '产生随机的笔触样式(用于图像的背景)
    Private Function generateHatchStyle() As HatchStyle
        Dim slist As New ArrayList
        For Each style As HatchStyle In System.Enum.GetValues(GetType(HatchStyle))
            slist.Add(style)
        Next
        Dim randObj As New Random
        Dim index As Integer = randObj.Next(slist.Count - 1)
        Return CType(slist(index), HatchStyle)
    End Function
    ''' 产生随机数校验码图像
    Private Function GenerateVCodeImage()
        Dim oBitmap As Bitmap = New Bitmap(90, 35)
        Dim oGraphic As Graphics = Graphics.FromImage(oBitmap)
        Dim foreColor As System.Drawing.Color
        Dim backColor As System.Drawing.Color
        Dim sText As String = generateVCode(5) '获取校验码字符串
        Dim sFont As String = "Comic Sans MS" '设置自己喜欢的字体
        '前景、背景的颜色
        foreColor = Color.FromArgb(220, 220, 220)
        backColor = Color.FromArgb(190, 190, 190)
        '设置用于背景的画笔
        Dim oBrush As New HatchBrush(CType(generateHatchStyle(), HatchStyle), foreColor, backColor)
        '用于输出校验码的画笔
        Dim oBrushWrite As New SolidBrush(Color.Gray)
        '生成的图像矩形大小
        oGraphic.FillRectangle(oBrush, 0, 0, 100, 50)
        oGraphic.TextRenderingHint = TextRenderingHint.AntiAlias
        Dim oFont As New Font(sFont, 14)
        Dim oPoint As New PointF(5.0F, 4.0F)
        oGraphic.DrawString(sText, oFont, oBrushWrite, oPoint)
        Response.ContentType = "image/jpeg"
        oBitmap.Save(Response.OutputStream, ImageFormat.Jpeg)
        oBitmap.Dispose()
        Return sText
    End Function

End Class