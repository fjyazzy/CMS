
Imports System.IO
Imports System.Net
Imports System.Security.Cryptography
Imports System.Web
Imports System.Web.Services
Imports System.Xml
Public Class WXMessTxt
    Public ToUserName As String '开发者微信号
    Public FromUserName As String '发送方帐号（一个OpenID）
    Public CreateTime As String '消息创建时间（整型）
    Public MsgType As String 'text
    Public Content As String '文本消息内容
    Public MsgId As String '消息id，64位整型
End Class
Public Structure ACCESSTOKEN
    Public access_token As String '获取到的凭证
    Public expires_in As String '凭证有效时间， 单位 ： 秒
End Structure
Public Class WXV
    Implements System.Web.IHttpHandler
    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property
    Dim Token As String = "fjsdxy"
    Dim AppID As String = "wx4aa2731f04858cae"
    Dim AppSecret As String = "fe5a567782c2080634b2a450c240beda"

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim httpMethod As String = context.Request.HttpMethod.ToUpper()
        If httpMethod = "GET" Then
            wxyz(context)   '用于微信验证
        End If
        If httpMethod = "POST" Then

            Dim wx As New WXMessTxt
            Dim xml As New XmlDocument()
            xml = W2C.ReadMessage2XML(context)
            wx.ToUserName = xml.SelectSingleNode("xml").SelectSingleNode("ToUserName").InnerText
            wx.FromUserName = xml.SelectSingleNode("xml").SelectSingleNode("FromUserName").InnerText
            wx.MsgType = xml.SelectSingleNode("xml").SelectSingleNode("MsgType").InnerText
            wx.Content = xml.SelectSingleNode("xml").SelectSingleNode("Content").InnerText


            Dim rss As String
            rss = "<xml>"
            rss &= "<ToUserName><![CDATA[" & wx.FromUserName & "]]></ToUserName>"
            rss &= "<FromUserName><![CDATA[" & wx.ToUserName & "]]></FromUserName>"
            rss &= "<CreateTime>" & DateTime.Now.Ticks & "</CreateTime>"
            rss &= "<MsgType><![CDATA[text]]></MsgType>"
            rss &= "<Content><![CDATA[你好,有什么问题需要咨询吗？]]></Content>"
            rss &= "</xml>"

            context.Response.ContentType = "text/plain"
            context.Response.Write(rss)
        End If
    End Sub

    Function GetContent(ByVal xml As XmlDocument, ByVal NodeName As String) As String
        Dim xn As XmlNode
        Dim tmp As String
        xn = xml.SelectSingleNode("/xml/" + NodeName)
        tmp = xn.InnerText
        Return tmp
    End Function

    Function GetToken() As String
        Dim res As String = ""
        Dim req As HttpWebRequest
        Dim wr As WebResponse
        Dim myResponse As HttpWebResponse
        req = WebRequest.Create("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" & AppID & "&secret=" & AppSecret)
        req.Method = "GET"
        wr = req.Getresponse()
        myResponse = req.Getresponse()
        Dim reader As StreamReader
        reader = New StreamReader(myResponse.GetresponseStream(), Encoding.UTF8)
        Dim content As String
        content = reader.ReadToEnd()

        Dim myACCESSTOKEN As ACCESSTOKEN
        myACCESSTOKEN.access_token = W2C.phaseJSON(content, "access_token")
        myACCESSTOKEN.expires_in = W2C.phaseJSON(content, "expires_in")

        res = myACCESSTOKEN.access_token
        Return res

    End Function

    Public Function GetPage(ByVal posturl As String, ByVal postdata As String) As String
        Dim outstream, instream As Stream
        Dim sr As StreamReader
        Dim hhresponse As HttpWebResponse
        Dim hhrequest As HttpWebRequest
        Dim Encoding As Encoding
        Dim cookieContainer As New CookieContainer
        Encoding = Encoding.UTF8
        Dim data As Byte()
        data = Encoding.GetBytes(postdata)
        hhrequest = WebRequest.Create(posturl)
        hhrequest.CookieContainer = cookieContainer
        hhrequest.AllowAutoRedirect = true
        hhrequest.Method = "POST"
        hhrequest.ContentType = "application/x-www-form-urlencoded"
        hhrequest.ContentLength = data.Length
        outstream = hhrequest.GetrequestStream()
        outstream.Write(data, 0, data.Length)
        outstream.Close()
        try
            Dim content As String
            hhresponse = hhrequest.Getresponse()
            instream = hhresponse.GetresponseStream()
            sr = New StreamReader(instream, Encoding)
            content = sr.ReadToEnd()
            Return content
        Catch ex As Exception
            Return (ex.Message)
        End try
    End Function

    Sub wxyz(ByVal context As HttpContext)
        context.Response.ContentType = "text/plain"
        Dim echoStr As String = context.Request("echoStr")
        If Not String.IsNullOrEmpty(echoStr) Then
            Dim signature As String = context.Request.QueryString("signature") ' 微信加密签名
            Dim timestamp As String = context.Request.QueryString("timestamp") ' 时间戳
            Dim nonce As String = context.Request.QueryString("nonce") ' 随机数
            echoStr = context.Request.QueryString("echostr") ' 随机字符串
            ' 微信请求参数非空验证
            If Not String.IsNullOrEmpty(signature) AndAlso Not String.IsNullOrEmpty(timestamp) AndAlso Not String.IsNullOrEmpty(nonce) AndAlso Not String.IsNullOrEmpty(echoStr) Then
                If CheckSignature(signature, timestamp, nonce, Token) Then
                    context.Response.Write(echoStr) '验证通过，响应微信公众平台后台服务器
                    Exit Sub
                End If
            End If
        End If

    End Sub
    Public Function CheckSignature(ByVal signature As String, ByVal timestamp As String, ByVal nonce As String, ByVal WeiXinToken As String) As Boolean
        Dim ArrTmp(3) As String
        ArrTmp(0) = WeiXinToken
        ArrTmp(1) = timestamp
        ArrTmp(2) = nonce
        Array.Sort(ArrTmp)
        '字典排序
        Dim tmpStr As String
        tmpStr = String.Join("", ArrTmp)
        tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1")
        tmpStr = tmpStr.ToLower()
        If tmpStr = signature Then
            Return true
        Else
            Return False
        End If
    End Function

End Class