Imports System.Web
Imports System.Web.Services

Public Class getProfiles
    Implements System.Web.IHttpHandler
    Dim JS As New Cms1.JsonClass
    Dim Conn As New ADODB.Connection

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        cc.Connecttodb()
        If Conn.State = 0 Then Conn.Open(cc.setConstr(2))

        Dim jsonstr As String
        Dim openId, xsno, icount, zjf, zcj As String
        openId = context.Request("openId")
        xsno = "10"
        zjf = "10"
        icount = "10"
        zcj = "10"
        Dim rs As New ADODB.Recordset
        rs.Open("select * from custom where openid='" & openId & "'", Conn, 1, 1)
        If Not rs.EOF Then
            xsno = rs.Fields("xsno").Value
            zjf = rs.Fields("zjf").Value
            icount = rs.Fields("icount").Value
        End If
        rs.Close()
        rs.Open("select * from homework where xsno='" & xsno & "'", Conn, 1, 1)
        If Not rs.EOF Then
            zcj = rs.Fields("zf").Value
        End If
        rs.Close()

        Conn.Close()
        jsonstr = "{""msgid"":""001"",""icount"":""" & icount & """,""zjf"":""" & zjf & """,""zcj"":""" & zcj & """}"

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