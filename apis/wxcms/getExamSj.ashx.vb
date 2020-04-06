Imports System.Web
Imports System.Web.Services

Public Class getExamSj
    Implements System.Web.IHttpHandler
    Dim JS As New Cms1.JsonClass
    Dim Conn As New ADODB.Connection
    '根据试卷号返回整份试卷JSON数据
    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        cc.Connecttodb()
        If Conn.State = 0 Then Conn.Open(cc.setConstr(2))

        Dim jsonstr As String
        jsonstr = JS.CreateEmptyJson()
        Dim name() As String = {"tx", "th", "fs"}
        Dim value() As String = {"", "", ""}

        Dim rs As New ADODB.Recordset
        rs.Open("select * from exam_sj where sjh='" & cc.Checkstr(context.Request("sjh")) & "' order by sj,tx,th", Conn, 1, 1)
        While Not rs.EOF
            value(0) = rs.Fields("tx").Value
            value(1) = rs.Fields("th").Value
            value(2) = rs.Fields("fs").Value
            jsonstr = JS.AddNode(jsonstr, name, value, False)
            rs.MoveNext()
        End While
        rs.Close()
        Conn.Close()

        value(0) = "|"
        value(1) = "|"
        value(2) = "|"
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