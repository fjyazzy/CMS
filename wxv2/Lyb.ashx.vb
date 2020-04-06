
Imports System.IO
Imports System.Net
Imports System.Security.Cryptography
Imports System.Web
Imports System.Web.Services
Imports System.Xml
Public Structure LybMessage
    Public message_id As String
    Public openId As String
    Public message As String
    Public token As String
End Structure
Public Class Lyb
    Implements System.Web.IHttpHandler

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        context.Response.ContentType = "text/plain"
        Dim httpMethod As String = context.Request.HttpMethod.ToUpper()
        If httpMethod = "GET" Then
            context.Response.Write("Hello World!")
        End If
        If httpMethod = "POST" Then
            Dim xml As String
            xml = W2C.ReadMessage2String(context)

            Dim lmg As LybMessage
            lmg.message_id = W2C.phaseJSON(xml, "message_id")
            lmg.openId = W2C.phaseJSON(xml, "openId")
            lmg.message = W2C.phaseJSON(xml, "message")
            lmg.token = W2C.phaseJSON(xml, "token")

            Dim rss As String
            rss = "{""message_id"": """ & lmg.message_id & ""","
            rss &= """openId"": """ & lmg.message_id & ""","
            rss &= """message"": """ & lmg.message & """, "
            rss &= """token"": """ & lmg.token & """}"

            context.Response.ContentType = "application/json"
            context.Response.Write(rss)

        End If

    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class