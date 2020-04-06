Imports System.Web
Imports System.Web.Services

Public Class getOpenid
    Implements System.Web.IHttpHandler
    Dim JS As New Cms1.JsonClass
    Dim Conn As New ADODB.Connection

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        cc.Connecttodb()
        cc.getSoftinfo("3")
        If Conn.State = 0 Then Conn.Open(cc.setConstr(2))

        Dim rUrl As String
        rUrl = "https://api.weixin.qq.com/sns/jscode2session?"
        rUrl &= "appid" & "=" & AppId & "&"
        rUrl &= "secret" & "=" & AppSecret & "&"
        rUrl &= "grant_type" & "=authorization_code&"
        rUrl &= "js_code" & "=" & context.Request("code")

        Dim jsonstr2 As String
        Dim Openid As String
        jsonstr2 = cc.getWebpage(rUrl, "GET")
        Openid = JS.phaseJSON(jsonstr2, "openid")

        Dim rs As New ADODB.Recordset
        rs.Open("select * from custom where openid='" & Openid & "'", Conn, 1, 3)
        If rs.EOF Then
            rs.AddNew()
            rs.Fields("openid").Value = Openid
            rs.Fields("icount").Value = 1
        Else
            rs.Fields("icount").Value = CInt(rs.Fields("icount").Value) + 1
        End If
        rs.Update()
        rs.Close()
        Conn.Close()


        Dim httpMethod As String = context.Request.HttpMethod.ToUpper()
        If httpMethod = "GET" Then

        End If
        If httpMethod = "POST" Then

        End If
        context.Response.ContentType = "applciation/json;charset=UTF-8"
        context.Response.Write(jsonstr2)


    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property


End Class