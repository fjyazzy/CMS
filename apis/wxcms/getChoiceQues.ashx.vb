Imports System.Web
Imports System.Web.Services

Public Class getChoiceQues
    Implements System.Web.IHttpHandler
    Dim Conn As New ADODB.Connection
    '根据题型，题号返回题目及题目相关信息
    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        cc.Connecttodb()
        If Conn.State = 0 Then Conn.Open(cc.setConstr(2))
        Dim jsonstrjg, th, tx As String
        jsonstrjg = ""
        th = cc.Checkstr(context.Request("id"))
        tx = cc.Checkstr(context.Request("tx"))
        Select Case tx
            Case "XZ"
                jsonstrjg = getxz(th)
            Case "WD"

            Case "TK"

        End Select

        Conn.Close()
        Dim httpMethod As String = context.Request.HttpMethod.ToUpper()
        If httpMethod = "GET" Then
        End If
        If httpMethod = "POST" Then
        End If
        context.Response.ContentType = "applciation/json;charset=UTF-8"
        context.Response.Write(jsonstrjg)

    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

    Private Function getxz(ByVal th As Long) As String
        Dim jsonstr As String
        Dim name() As String = {"th", "ques", "option", "type", "ans"}
        Dim value() As String = {"", "", "", "", ""}
        Dim has, i As Integer
        has = 0
        i = 1
        Dim rs As New ADODB.Recordset
        rs.Open("select * from XZ where id=" & th, Conn, 1, 1)
        If rs.EOF Then
            jsonstr = "{""th"":""|"", ""ques"":""|"", ""option"":""[]"", ""type"":""|"", ""ans"":""|"", ""ts"":""|""}"
        Else
            jsonstr = "{""th"":""" & rs.Fields("id").Value & """, "
            jsonstr &= """ques"":""" & rs.Fields("subject").Value & """, "
            jsonstr &= """option"":["
            For i = 1 To 6
                If rs.Fields("option" & i).Value <> "" Then
                    If has = 1 Then
                        jsonstr &= ","
                    Else
                        has = 1
                    End If
                    jsonstr &= """" & rs.Fields("option" & i).Value & """"
                End If
            Next
            jsonstr &= "], "
            jsonstr &= """type"":""" & rs.Fields("mode").Value & """, "
            jsonstr &= """ans"":""" & rs.Fields("answer").Value & ""","
            jsonstr &= """ts"":""" & rs.Fields("ts").Value & """}"
        End If
        rs.Close()

        Return jsonstr
    End Function

End Class