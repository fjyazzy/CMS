Imports System.Web
Imports System.Web.Services

Public Class getcompanyinfo
    Implements System.Web.IHttpHandler
    Dim JS As New Cms1.JsonClass
    Dim Conn As New ADODB.Connection

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        Dim jsonstr As String
        jsonstr = JS.CreateEmptyJson()
        Dim name() As String = {"name", "age"}
        Dim value() As String = {"", ""}

        value(0) = "LeonWu"
        value(1) = "18"
        jsonstr = JS.AddNode(jsonstr, name, value, False)
        value(0) = "Zhengzy"
        value(1) = "19"
        jsonstr = JS.AddNode(jsonstr, name, value, true)

        Dim httpMethod As String = context.Request.HttpMethod.ToUpper()
        If httpMethod = "GET" Then

        End If
        If httpMethod = "POST" Then

        End If
        context.Response.ContentType = "applciation/json;charset=UTF-8"
        context.Response.Write(jsonstr)

    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class