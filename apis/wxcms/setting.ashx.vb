Imports System.Web
Imports System.Web.Services

Public Class setting
    Implements System.Web.IHttpHandler
    Dim JS As New Cms1.JsonClass
    Dim Conn As New ADODB.Connection

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        cc.Connecttodb()
        cc.getSoftinfo("3")
        If Conn.State = 0 Then Conn.Open(cc.setConstr(2))
        Dim jsonstr As String
        Dim openId, gitno, xsno, bjname, xsname As String
        openId = context.Request("openId")
        gitno = context.Request("gitno")
        xsno = context.Request("xsno")
        bjname = context.Request("bjname")
        xsname = context.Request("xsname")

        Dim rs As New ADODB.Recordset
        rs.Open("select * from custom where openid='" & Openid & "'", Conn, 1, 3)
        If rs.EOF Then
            rs.AddNew()
            rs.Fields("openid").Value = openId
            rs.Fields("gitno").Value = gitno
            rs.Fields("xsno").Value = xsno
            rs.Fields("bjname").Value = bjname
            rs.Fields("xsname").Value = xsname
            rs.Fields("icount").Value = 1
        Else
            rs.Fields("gitno").Value = gitno
            rs.Fields("xsno").Value = xsno
            rs.Fields("bjname").Value = bjname
            rs.Fields("xsname").Value = xsname
            rs.Fields("icount").Value = CInt(rs.Fields("icount").Value) + 1
        End If
        rs.Update()
        rs.Close()
        Conn.Close()

        jsonstr = "{""msgid"":""001""}"

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