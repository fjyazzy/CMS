Imports System.Web
Imports System.Web.Services

Public Class getPage
    Implements System.Web.IHttpHandler
    Dim JS As New Cms1.JsonClass
    Dim Conn As New ADODB.Connection
    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        cc.Connecttodb()
        If Conn.State = 0 Then Conn.Open(cc.setConstr(2))

        Dim jsonstr As String
        jsonstr = JS.CreateEmptyJson()
        Dim name() As String = {"pagetitle", "pagetext", "fbzz", "fbsj"}
        Dim value() As String = {"", "", "", ""}

        Dim rs As New ADODB.Recordset
        rs.Open("select * from wpages where pageno='" & cc.Checkstr(context.Request("pageno")) & "'", Conn, 1, 1)
        If rs.EOF Then
            value(0) = "|"
            value(1) = "|"
            value(2) = "|"
            value(3) = "|"
            jsonstr = JS.AddNode(jsonstr, name, value, true)
        Else
            value(0) = rs.Fields("pagetitle").Value
            value(1) = rs.Fields("pagetext").Value
            value(2) = rs.Fields("fbzz").Value
            value(3) = rs.Fields("fbsj").Value
            jsonstr = JS.AddNode(jsonstr, name, value, true)
        End If
        rs.Close()
        Conn.Close()

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