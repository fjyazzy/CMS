Imports System.Web
Imports System.Web.Services

Public Class getMsg
    Implements System.Web.IHttpHandler
    Dim JS As New Cms1.JsonClass
    Dim Conn As New ADODB.Connection

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        cc.Connecttodb()
        If Conn.State = 0 Then Conn.Open(cc.setConstr(2))

        Dim jsonstr, openid As String
        jsonstr = JS.CreateEmptyJson()
        Dim name() As String = {"msgid", "msgtitle", "msgnr", "msgtime", "ifread"}
        Dim value() As String = {"", "", "", "", ""}
        openid = context.Request("openid")

        Dim rs As New ADODB.Recordset
        rs.Open("select * from umessage where openid='" & openid & "' order by msgid", Conn, 1, 1)
        While Not rs.EOF
            value(0) = rs.Fields("msgid").Value
            value(1) = rs.Fields("msgtitle").Value
            value(2) = rs.Fields("msgnr").Value
            value(3) = rs.Fields("msgtime").Value
            value(4) = rs.Fields("ifread").Value
            jsonstr = JS.AddNode(jsonstr, name, value, False)
            rs.MoveNext()
        End While
        rs.Close()
        Conn.Close()

        value(0) = "|"
        value(1) = "|"
        value(2) = "|"
        value(3) = "|"
        value(4) = "|"
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