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
    '�������ݿ�����
    '���: cc.setConstr(1) Ϊȫ�ֱ���
    Sub Connecttodb()

        ' ����web.config  appSettings�ڵ����ֵ
        ' ʹ��web.config  for sql server
        Dim servers, dbs, uid, pwd As String
        Dim accessDb(10) As String

        servers = System.Configuration.ConfigurationManager.AppSettings.Item("SQL_SERVERIP")
        dbs = System.Configuration.ConfigurationManager.AppSettings.Item("SQL_DATABASE")
        uid = System.Configuration.ConfigurationManager.AppSettings.Item("SQL_USERNAME")
        pwd = System.Configuration.ConfigurationManager.AppSettings.Item("SQL_PASSWORD")
        ConString(1) = "Driver={SQL Server}; Server=" & servers & "; Database=" & dbs & "; Uid=" & uid & "; Pwd=" & pwd

        '��������Access���ݿ�
        Dim i As Integer
        For i = 1 To 6
            accessDb(i) = System.Configuration.ConfigurationManager.AppSettings.Item("ACCESS_DB" & i)
            ConString(i) = "PROVIDER=Microsoft.Jet.OLEDB.4.0;Jet OLEDB:Database Password=05983602120;DATA Source=" & accessDb(i)
        Next

        '���õ�ǰ�������������ݿ�ID,���ڳ����и�����Ҫ�޸�
        DBord_ecms = System.Configuration.ConfigurationManager.AppSettings.Item("DBORD")
        '����ͼƬ���������ĵ���������Ŀ¼
        PicUrl = System.Configuration.ConfigurationManager.AppSettings.Item("Center_photos")
        ManualUrl = System.Configuration.ConfigurationManager.AppSettings.Item("Center_Manuals")


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
    '����DBORD����ͼƬ���ĺ��ĵ�����Ŀ¼
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

        End Select
        Return jg
    End Function

    '���ù����ǿ�ͨ���������Ȩ��
    '�����1����Ȩ�ޣ� 0��û��Ȩ��
    Function getQx(ByVal nickname As String, ByVal qxid As Integer) As Integer
        ' �����û����ж��û��Ƿ�ӵ�б�ҳ���Ȩ��
        ' �ɷ���ҳ��� pageload �¼��кͺ���ִ��ǰ
        Dim ixx, j As Integer
        Dim rsx As New ADODB.Recordset
        Dim Conn As New ADODB.Connection
        Connecttodb()
        If Conn.State = 0 Then Conn.Open(cc.setConstr(1))

        rsx.Open("Select  * from users where Username='" & nickname & "'", Conn, 1, 1)
        If Not rsx.EOF Then
            If System.Convert.IsDBNull(rsx.Fields("qxj").Value) = true Then
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
        Else
            ixx = 0
        End If
        rsx.Close()
        Conn.Close()

        ' �������ʹ�ð�Ȩʱ�����
        If DateDiff("d", "2020-12-25", Now) > 0 Then
            ixx = 0
        End If

        Return ixx
    End Function



    ' ��������ַ��Ŵ�
    '
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
    ' �����չ��
    '
    Function CheckExt(ByVal fn As String) As String
        Dim intExt As Integer
        Dim str As String
        intExt = InStrRev(fn, ".")
        str = Mid(fn, intExt)
        Dim jg As String
        Select Case LCase(str)
            Case ".gif", ".jpg", ".flv", ".swf", ".mov", ".wmv", ".xls", ".txt", ".doc", ".rar", ".ppt", ".zip", ".pdf"
                jg = LCase(str)
            Case Else
                jg = ""
        End Select
        Return jg
    End Function
    ' ����ļ���
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

    '�������Ļ�����Ϣ
    '���: SOFTNAME,SOFTVERSION Ϊȫ�ֱ���
    '���ã�
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
        try
            httpReq.Method = method1
            httpReq.KeepAlive = False
            httpReq.CookieContainer = wcc
            httpResp = CType(httpReq.GetResponse(), HttpWebResponse)
            httpResp.Cookies = wcc.GetCookies(httpReq.RequestUri)
            Dim reader As StreamReader = New StreamReader(httpResp.GetresponseStream, System.Text.Encoding.GetEncoding("utf-8"))
            Html = reader.ReadToEnd()
        Catch ex As Exception
            Html = ex.Message.ToString
        End try

        Return Html
    End Function


    ''' <summary>
    ''' �����༭����ӶԻ���
    ''' </summary>
    ''' <param name="imode"></param>
    ''' <param name="strFilename"></param>
    ''' <param name="strTitle"></param>
    ''' <param name="w"></param>
    ''' <param name="h"></param>
    ''' <returns></returns>
    Function ShowDialog(ByVal imode As Integer, ByVal strFilename As String, ByVal strTitle As String�� ByVal w As Integer, ByVal h As Integer)
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



    ''' <summary>
    ''' ��������������
    ''' </summary>
    ''' <param name="DBOrd"></param>
    ''' <returns></returns>
    Function webDHL(ByVal DBOrd As String) As String
        Dim jg As String = ""
        Select Case DBOrd
            Case "4"
                jg &= "<a href=index.aspx>��ҳ</a> |"
                jg &= "<a href=syfw.aspx>�������</a> |"
                jg &= "<a href=jxfw.aspx>���޷���</a> |"
                jg &= "<a href=cpdz.aspx>��Ʒ����</a> |"
                jg &= "<a href=Connectus.aspx>��ϵ����</a> |"
                jg &= "<a href=ShopCart.aspx>���ﳵ</a> |"
                jg &= "<a href=me.aspx>�ҵ�</a>"

        End Select
        Return jg
    End Function

    Function webPage(ByVal DBOrd As String, ByVal ItemNo As String) As String
        Dim jg As String = ""
        Dim rsx As New ADODB.Recordset
        Dim Conn As New ADODB.Connection
        Connecttodb()
        If Conn.State = 0 Then Conn.Open(setConstr(DBOrd))
        rsx.Open("select  * from wPages where itemno='" & ItemNo & "'", Conn, 1, 1)
        If Not rsx.EOF Then
            jg = rsx.Fields("itemText").Value
        Else
            jg = "<h3>��ҳ��·��,����ϵ֣־�£�13950937003��</h3>"
        End If
        rsx.Close()
        Return jg
    End Function

End Class