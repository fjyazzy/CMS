Imports System.IO
Public Class manual1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim conn As New ADODB.Connection
        CC.Connecttodb()
        If conn.State = 0 Then conn.Open(CC.setConstr(DBord_ecms))

        Dim rs As New ADODB.Recordset
        Dim sql, jg As String
        Dim cid As String
        cid = CLng(CC.Checkstr(Request("id")))
        jg = ""

        sql = "SELECT  * from manual where id=" & cid
        rs.Open(sql, conn, 1, 3, 1)
        If Not rs.EOF Then
            Dim m_title, M_Meta_C, M_Meta_Key
            m_title = WCS.GetTitle("Title", "Manual")
            m_title = Replace(m_title, "<网站名>", WZMC)
            m_title = Replace(m_title, "<产品描述>", rs.Fields("shortdesc").Value)
            m_title = Replace(m_title, "<厂商>", rs.Fields("manufactory").Value)
            m_title = Replace(m_title, "<资料名称>", rs.Fields("type").Value)
            m_title = Replace(m_title, "<产品型号>", rs.Fields("type").Value)

            M_Meta_C = WCS.GetTitle("MetaContent", "Manual")
            M_Meta_C = Replace(M_Meta_C, "<公司名>", GSMC)
            M_Meta_C = Replace(M_Meta_C, "<网站名>", WZMC)
            M_Meta_C = Replace(M_Meta_C, "<产品描述>", rs.Fields("shortdesc").Value)
            M_Meta_C = Replace(M_Meta_C, "<厂商>", rs.Fields("manufactory").Value)
            M_Meta_C = Replace(M_Meta_C, "<资料名称>", rs.Fields("type").Value)
            M_Meta_C = Replace(M_Meta_C, "<产品型号>", rs.Fields("type").Value)

            M_Meta_Key = WCS.GetTitle("MetaKeywords", "Manual")
            M_Meta_Key = Replace(M_Meta_Key, "<公司名>", GSMC)
            M_Meta_Key = Replace(M_Meta_Key, "<网站名>", WZMC)
            M_Meta_Key = Replace(M_Meta_Key, "<产品描述>", rs.Fields("shortdesc").Value)
            M_Meta_Key = Replace(M_Meta_Key, "<厂商>", rs.Fields("manufactory").Value)
            M_Meta_Key = Replace(M_Meta_Key, "<资料名称>", rs.Fields("type").Value)
            M_Meta_Key = Replace(M_Meta_Key, "<产品型号>", rs.Fields("type").Value)

            PageTitle.InnerText = m_title
            Content1.Attributes.Add("name", "description")
            Content1.Attributes.Add("content", M_Meta_C)
            Content2.Attributes.Add("name", "keywords")
            Content2.Attributes.Add("content", M_Meta_Key)

            jg &= "<h3>资料相关产品</h3><table border=0 width=100%>"
            jg &= "<tr><td colspan=3>"
            jg &= WCS.SearchP(1, Left(rs.Fields("type").Value, 2), 1, "Catalogs.aspx")
            jg &= "</td></tr></table>"

            rs.Fields("clicknum").Value = CInt(rs.Fields("clicknum").Value) + 1
            rs.Fields("VisitTime").Value = Now
            rs.Update()

        End If
        rs.Close()

        If cid = "" Then
            cid = 0
        End If
        Label1.Text = xxzl(cid) & jg
        conn.Close()

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="cid"></param>
    ''' <returns></returns>
    Function xxzl(ByVal cid)

        Dim rs As New ADODB.Recordset
        Dim rs2 As New ADODB.Recordset
        Dim sql, jg, saveFileName, jf As String
        Dim doit As Integer
        Dim conn As New ADODB.Connection
        CC.Connecttodb()
        If conn.State = 0 Then conn.Open(CC.setConstr(DBord_ecms))
        doit = 1
        jg = ""
        saveFileName = ""

        sql = "SELECT top 1 * from manual where id=" & cid
        rs.Open(sql, conn, 1, 3, 1)
        If rs.EOF Then
            jg = "资料不存在!"
            doit = 0
        Else
            jf = rs.Fields("point").Value
            saveFileName = rs.Fields("furl").Value
            '’If Request.Cookies("nickname") Is Nothing Then
            'If jf = 0 Then
            'Else
            'jg = "本项目下载需要积分,请注册为本站用户,免费获得10个积分!!"
            'doit = 0
            'End If
            '  Else
            'sql = "SELECT * from bmis_user where nickname='" & Request.Cookies("nickname").Value & "'"
            'rs2.Open(sql, Conn, 1, 3, 1)
            'If Not rs2.EOF Then
            ' If CInt(rs2.Fields("upoint").Value) >= jf Then
            'Else
            'Label1.Text = "你的积分不够!!"
            'doit = 0
            'End If
            '   Else
            '  Label1.Text = "登陆错误,请重新登陆系统!!"
            ' doit = 0
            'End If
            '   rs2.Close()
            'End If
        End If


        If doit = 1 Then
            jg = "<table style=""font-size:14px;width:100%"">"
            jg = jg & "<tr><td bgcolor=#ffffff>资料名称</td><td  bgcolor=#ffffff>" & rs.Fields("type").Value & "</td></tr>"
            sql = "SELECT  * from Category_manuals where id=" & rs.Fields("Categoryid").Value
            rs2.Open(sql, conn, 1, 3, 1)
            If Not rs2.EOF Then
                jg = jg & "<tr><td  bgcolor=#ffffff>所在目录</td><td  bgcolor=#ffffff><a href=mcatalogs.aspx?id=" & rs2.Fields("id").Value & ">" & rs2.Fields("catalogname").Value & "</a></td></tr>"
            End If
            rs2.Close()
            jg = jg & "<tr><td bgcolor=#ffffff>厂商</td><td  bgcolor=#ffffff>" & rs.Fields("manufactory").Value & "</td></tr>"
            jg = jg & "<tr><td bgcolor=#ffffff>描述</td><td  bgcolor=#ffffff>" & rs.Fields("shortdesc").Value & "</td></tr>"
            jg = jg & "<tr><td bgcolor=#ffffff>点击查看</td><td  bgcolor=#ffffff>"
            jg = jg & ShowAtt(saveFileName)
            jg = jg & "</td></tr></table>"
        End If

        rs.Close()
        Return jg

    End Function
    Public Sub SetResponseHeader(ByVal sContentType As String, ByVal sFileName As String, ByVal bSaveFile As Boolean)
        Dim sContentDisposition As String = ""
        With Response
            If bSaveFile Then
                sContentDisposition = "attachment; "
            End If
            If Len(sFileName) > 0 Then
                sContentDisposition = sContentDisposition & "filename=" & sFileName
            End If
            If Len(sContentDisposition) > 0 Then
                .AddHeader("Content-disposition", sContentDisposition)
            End If
            .ContentType = sContentType
        End With
    End Sub
    Function ct(ByVal extension)
        Dim contentType As String
        Select Case UCase(extension)
            Case "*"
                contentType = "application/octet-stream"
            Case ("323")
                contentType = "text/h323"
            Case ("ACX")
                contentType = "application/internet-property-stream"
            Case ("AI")
                contentType = "application/postscript"
            Case ("AIF")
                contentType = "audio/x-aiff"
            Case ("AIFC")
                contentType = "audio/x-aiff"
            Case ("AIFF")
                contentType = "audio/x-aiff"
            Case ("ASF")
                contentType = "video/x-ms-asf"
            Case ("SR")
                contentType = "video/x-ms-asf"
            Case ("SX")
                contentType = "video/x-ms-asf"
            Case ("AU")
                contentType = "audio/basic"
            Case ("AVI")
                contentType = "video/x-msvideo"
            Case ("AXS")
                contentType = "application/olescript"
            Case ("BAS")
                contentType = "text/plain"
            Case ("BCPIO")
                contentType = "application/x-bcpio"
            Case ("BIN")
                contentType = "application/octet-stream"
            Case ("BMP")
                contentType = "image/bmp"
            Case ("C")
                contentType = "text/plain"
            Case ("CAT")
                contentType = "application/vnd.ms-pkiseccat"
            Case ("CDF")
                contentType = "application/x-cdf"
            Case ("CER")
                contentType = "application/x-x509-ca-cert"
            Case ("CLASS")
                contentType = "application/octet-stream"
            Case ("CLP")
                contentType = "application/x-msclip"
            Case ("CMX")
                contentType = "image/x-cmx"
            Case ("COD")
                contentType = "image/cis-cod"
            Case ("CPIO")
                contentType = "application/x-cpio"
            Case ("CRD")
                contentType = "application/x-mscardfile"
            Case ("CRL")
                contentType = "application/pkix-crl"
            Case ("CRT")
                contentType = "application/x-x509-ca-cert"
            Case ("CSH")
                contentType = "application/x-csh"
            Case ("CSS")
                contentType = "text/css"
            Case ("DCR")
                contentType = "application/x-director"
            Case ("DER")
                contentType = "application/x-x509-ca-cert"
            Case ("DIR")
                contentType = "application/x-director"
            Case ("DLL")
                contentType = "application/x-msdownload"
            Case ("DMS")
                contentType = "application/octet-stream"
            Case ("DOC")
                contentType = "application/msword"
            Case ("DOT")
                contentType = "application/msword"
            Case ("DVI")
                contentType = "application/x-dvi"
            Case ("DXR")
                contentType = "application/x-director"
            Case ("EPS")
                contentType = "application/postscript"
            Case ("ETX")
                contentType = "text/x-setext"
            Case ("EVY")
                contentType = "application/envoy"
            Case ("EXE")
                contentType = "application/octet-stream"
            Case ("FIF")
                contentType = "application/fractals"
            Case ("FLR")
                contentType = "x-world/x-vrml"
            Case ("GIF")
                contentType = "image/gif"
            Case ("GTAR")
                contentType = "application/x-gtar"
            Case ("GZ")
                contentType = "application/x-gzip"
            Case ("H")
                contentType = "text/plain"
            Case ("HDF")
                contentType = "application/x-hdf"
            Case ("HLP")
                contentType = "application/winhlp"
            Case ("HQX")
                contentType = "application/mac-binhex40"
            Case ("HTA")
                contentType = "application/hta"
            Case ("HTC")
                contentType = "text/x-component"
            Case ("HTM")
                contentType = "text/html"
            Case ("HTML")
                contentType = "text/html"
            Case ("HTT")
                contentType = "text/webviewhtml"
            Case ("ICO")
                contentType = "image/x-icon"
            Case ("IEF")
                contentType = "image/ief"
            Case ("III")
                contentType = "application/x-iphone"
            Case ("INS")
                contentType = "application/x-internet-signup"
            Case ("ISP")
                contentType = "application/x-internet-signup"
            Case ("JFIF")
                contentType = "image/pipeg"
            Case ("JPE")
                contentType = "image/jpeg"
            Case ("JPEG")
                contentType = "image/jpeg"
            Case Else
                contentType = "application/octet-stream"
        End Select

        Return contentType
    End Function
    Function ShowAtt(ByVal s)
        Dim f As Object
        Dim i As Integer
        Dim jg As String
        f = Split(s, "|")
        jg = "<ul>"
        For i = 0 To UBound(f) - 1 Step 2
            If Left(f(i), 1) = "\" Then
                jg &= "<li><a target=_blank href=viewManual.aspx?p=" & Server.UrlEncode("/DOC" & f(i)) & ">" & f(i) & "(" & f(i + 1) & ")<img src=""images/pdf.gif"" border=0></a>"
            Else
                If File.Exists(Server.MapPath("manual/" & f(i))) Then
                    jg &= "<li><a target=_blank href=viewManual.aspx?p=" & Server.UrlEncode("manual/" & f(i)) & ">" & f(i) & "(" & f(i + 1) & ")<img src=""images/pdf.gif"" border=0></a>"
                Else
                    jg &= "<li><a target=_blank href=viewManual.aspx?p=" & Server.UrlEncode("/icdemi.pdf") & ">" & f(i) & "(" & f(i + 1) & ")<img src=""images/pdf.gif"" border=0></a>"
                End If
            End If
        Next
        jg &= "<ul>"
        Return jg

    End Function


End Class