Imports System
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Configuration
Imports System.Net
Imports System.IO
Imports System.Net.Security
Imports System.Security.Authentication
Imports System.Security.Cryptography.X509Certificates
Imports System.Net.NetworkInformation
Public Class CommonClass
    '配置数据库连接
    '结果: cc.setConstr(1) 为全局变量
    Sub Connecttodb()

        ' 配置web.config  appSettings节点的数值
        ' 使用web.config  for sql server
        Dim servers, dbs, uid, pwd As String
        Dim accessDb(10) As String

        servers = System.Configuration.ConfigurationManager.AppSettings.Item("SQL_SERVERIP")
        dbs = System.Configuration.ConfigurationManager.AppSettings.Item("SQL_DATABASE")
        uid = System.Configuration.ConfigurationManager.AppSettings.Item("SQL_USERNAME")
        pwd = System.Configuration.ConfigurationManager.AppSettings.Item("SQL_PASSWORD")
        ConString(1) = "Driver={SQL Server}; Server=" & servers & "; Database=" & dbs & "; Uid=" & uid & "; Pwd=" & pwd

        '获取Access数据库数量
        DBNUMS = System.Configuration.ConfigurationManager.AppSettings.Item("DBNUMS")
        '配置连接Access数据库
        Dim i As Integer
        For i = 1 To DBNUMS
            accessDb(i) = System.Configuration.ConfigurationManager.AppSettings.Item("ACCESS_DB" & i)
            ConString(i) = "PROVIDER=Microsoft.Jet.OLEDB.4.0;Jet OLEDB:Database Password=05983602120;DATA Source=" & accessDb(i)
        Next

        '设置图片服务器和文档服务器的目录
        Center_PicUrl = System.Configuration.ConfigurationManager.AppSettings.Item("Center_photos")
        Center_ManualUrl = System.Configuration.ConfigurationManager.AppSettings.Item("Center_Manuals")
        Center_CsvUrl = System.Configuration.ConfigurationManager.AppSettings.Item("Center_Csv")


    End Sub
    Function setConstr(ByVal DBOrd As String)
        '1 value="d:\upload\CMSv1\cMIS1.mdb" />
        '2 value="d:\upload\CMSv1\WXCMS.mdb" />
        '3 value="d:\upload\CMSv1\Exam.mdb" />
        '4 value="d:\upload\CMSv1\Xfdq.mdb" />
        '5 value="d:\upload\CMSv1\loveu.mdb" />
        Dim jg As String = ""
        jg = ConString(DBOrd)
        Return jg
    End Function
    '根据DBORD换算图片中心和文档中心目录
    Function DBord2path(ByVal DBord As Integer) As String
        Dim jg As String = ""
        Select Case DBord
            Case 1
                jg = "Sys"
            Case 2
                jg = ""
            Case 3
                jg = "Exam"
            Case 4
                jg = "Xfdq"
            Case 5
                jg = "loveu"
            Case 6
                jg = "Ecms"
            Case 6
                jg = "Ecmscn"
        End Select
        Return jg
    End Function
    '调用功能是可通过函数检查权限
    '结果：1：有权限， 0：没有权限
    Function getQx(ByVal nickname As String, ByVal qxid As Integer) As Integer
        ' 根据用户名判断用户是否拥有本页面的权限
        ' 可放在页面的 pageload 事件中和函数执行前
        Dim ixx, j As Integer
        Dim rsx As New ADODB.Recordset
        Dim Conn As New ADODB.Connection
        Connecttodb()
        If Conn.State = 0 Then Conn.Open(cc.setConstr(1))
        rsx.Open("Select  * from users where Username='" & nickname & "'", Conn, 1, 1)
        If Not rsx.EOF Then
            If System.Convert.IsDBNull(rsx.Fields("qxj").Value) = True Then
                ixx = 0
            Else
                Dim f() As String = Split(rsx.Fields("qxj").Value, "|")
                ixx = 0
                For j = 0 To UBound(f)
                    If qxid = f(j) Then
                        ixx = 1
                        Exit For
                    End If
                Next
            End If
            ' 设置同类数据库ID
            DBord_ecms = rsx.Fields("DBord").Value
        Else
            ixx = 0
        End If
        rsx.Close()
        Conn.Close()

        ' 设置软件使用版权时间截至
        If DateDiff("d", "2020-12-25", Now) > 0 Then
            ixx = 0
        End If

        Return ixx
    End Function
    ' 检查输入字符号串
    Function Checkstr(ByVal Str)
        Dim sql_injdata As String
        Dim SQL_inj
        Dim i As Integer
        sql_injdata = "'|and|exec|insert|select|delete|update|count|"
        sql_injdata &= "*|%|]|[|mid|master|truncate|char|declare|"
        sql_injdata &= ">|<|script|object|applet|/|\|#|"
        If Str = "" Then
            Checkstr = ""
            Exit Function
        End If
        SQL_inj = Split(sql_injdata, "|")
        For i = 0 To UBound(SQL_inj)
            Str = Replace(Str, SQL_inj(i), "_", 1, -1, 1)
        Next
        Checkstr = Replace(Str, " ", "_", 1, -1, 1)
    End Function
    ' 检查扩展名
    '
    Function CheckExt(ByVal fn As String) As String
        Dim intExt As Integer
        Dim str As String
        intExt = InStrRev(fn, ".")
        str = Mid(fn, intExt)
        Dim jg As String
        Select Case LCase(str)
            Case ".gif", ".jpg", ".flv", ".swf", ".mov", ".wmv", ".xls", ".txt", ".doc", ".rar", ".ppt", ".zip", ".pdf", ".png"
                jg = LCase(str)
            Case Else
                jg = ""
        End Select
        Return jg
    End Function
    ' 检查文件名
    Function CheckFileName(ByVal str As String) As String
        Dim sql_injdata As String
        Dim SQL_inj() As String
        Dim i As Integer
        sql_injdata = "'|+|>|<| |?|,|!|@|#|%|*|&|^|\|/|=|""|"
        If str = "" Then
            CheckFileName = ""
            Exit Function
        End If
        SQL_inj = Split(sql_injdata, "|")
        For i = 0 To UBound(SQL_inj)
            str = Replace(str, SQL_inj(i), "_", 1, -1, 1)
        Next
        CheckFileName = Replace(str, "|", "_", 1, -1, 1)
    End Function
    '获得软件的基本信息
    '结果: SOFTNAME,SOFTVERSION 为全局变量
    '设置：
    Sub getSoftinfo(ByVal DBOrd As String)
        Dim rsx As New ADODB.Recordset
        Dim Conn As New ADODB.Connection
        Connecttodb()
        If Conn.State = 0 Then Conn.Open(setConstr(DBOrd))
        rsx.Open("select * from systeminfo", Conn, 1, 1)
        While Not rsx.EOF
            Select Case rsx.Fields("itemno").Value
                Case "001"
                    SOFTNAME = rsx.Fields("itemtext").Value
                Case "002"
                    SOFTVERSION = rsx.Fields("itemtext").Value
                Case "008"
                    SYSTEMSTYLE = rsx.Fields("itemtext").Value
                Case "009"
                    AppId = rsx.Fields("itemtext").Value
                Case "010"
                    AppSecret = rsx.Fields("itemtext").Value
                Case "011"
                    PAGENUMS = rsx.Fields("itemtext").Value
                Case "012"
                    BJS = rsx.Fields("itemtext").Value
                Case "013"
                    ANS = rsx.Fields("itemtext").Value
                Case "021"
                    strDwmc = rsx.Fields("itemtext").Value
                Case Else
            End Select
            rsx.MoveNext()
        End While
        rsx.Close()
        Conn.Close()

    End Sub
    Function Alert(ByVal str) As String
        Return "<script language=javascript>alert('" & str & "')</script>"
    End Function
    Function ClosePage() As String
        Dim jg As String
        jg = "<script language = ""javascript"" >"
        jg &= "parent.location.reload();"
        jg &= "</script>"
        Return jg
    End Function
    Public Function CheckValidationResult(ByVal sender As Object, ByVal certificate As X509Certificate, ByVal chain As X509Chain, ByVal errors As SslPolicyErrors) As Boolean
        Return True
    End Function
    Public Function getWebpage(ByVal url As String, ByVal method1 As String) As String
        Dim httpReq As HttpWebRequest
        If Left(url.ToLower, 5) = "https" Then
            ServicePointManager.ServerCertificateValidationCallback = New RemoteCertificateValidationCallback(AddressOf CheckValidationResult)
            httpReq = Net.WebRequest.CreateDefault(New Uri(url))
        Else
            httpReq = Net.WebRequest.Create(url)
        End If
        Dim wcc As New CookieContainer
        Dim httpResp As System.Net.HttpWebResponse

        Dim Html As String
        Try
            httpReq.Method = method1
            httpReq.KeepAlive = False
            httpReq.CookieContainer = wcc
            httpResp = CType(httpReq.GetResponse(), HttpWebResponse)
            httpResp.Cookies = wcc.GetCookies(httpReq.RequestUri)
            Dim reader As StreamReader = New StreamReader(httpResp.GetresponseStream, System.Text.Encoding.GetEncoding("utf-8"))
            Html = reader.ReadToEnd()
        Catch ex As Exception
            Html = ex.Message.ToString
        End Try

        Return Html
    End Function
    ''' <summary>
    ''' 弹出编辑和添加对话窗
    ''' </summary>
    ''' <param name="imode"></param>
    ''' <param name="strFilename"></param>
    ''' <param name="strTitle"></param>
    ''' <param name="w"></param>
    ''' <param name="h"></param>
    ''' <returns></returns>
    Function ShowDialog(ByVal imode As Integer, ByVal strFilename As String, ByVal strTitle As String， ByVal w As Integer, ByVal h As Integer)
        Dim jg As String = ""
        Select Case imode
            Case 1
                jg = "showDetail('" & strFilename & "','" & strTitle & "'," & w & "," & h & ");"
            Case 2
                jg = ""
            Case 3
                jg = "alert('ok!');"
        End Select

        Return jg
    End Function

    'CSV文件分割
    Function CsvSplit(ByVal lineStr As String) As String()
        Dim f(), g(100) As String
        Dim tempStr As String
        Dim i, j, k As Integer
        j = 0
        f = Split(lineStr, ",")
        For i = 0 To UBound(f)
            If InStr(f(i), """") > 0 Then
                tempStr = Replace(f(i), """", "")
                For k = i + 1 To UBound(f)
                    tempStr = tempStr & "," & Replace(f(k), """", "")
                    If InStr(f(k), """") > 0 Then
                        Exit For
                    End If
                Next
                i = k
                g(j) = tempStr
                j = j + 1
            Else
                g(j) = f(i)
                j = j + 1
            End If
        Next

        k = j - 1
        Dim h(k) As String
        For i = 0 To k
            h(i) = g(i)
        Next

        Return h
    End Function

End Class