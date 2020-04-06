Imports System.Web
Imports System.Web.Services

Public Class logout
    Implements System.Web.IHttpHandler

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim aCookie As HttpCookie
        Dim cookieName As String
        Dim limit, i As Integer
        limit = context.Request.Cookies.Count
        For i = 0 To limit
            cookieName = context.Request.Cookies(i).Name
            aCookie = New HttpCookie(cookieName)
            aCookie.Expires = DateTime.Now.AddDays(-1)
            context.Response.Cookies.Add(aCookie)
        Next i
        context.Response.ContentType = "text/html"
        context.Response.Write("<script language=javascript>window.close();</script>")

    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class