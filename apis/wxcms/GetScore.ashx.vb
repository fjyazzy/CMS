Imports System.Web
Imports System.Web.Services

Public Class GetScore
    Implements System.Web.IHttpHandler
    Dim JS As New Cms1.JsonClass
    Dim Conn As New ADODB.Connection

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        cc.Connecttodb()
        cc.getSoftinfo("3")
        If Conn.State = 0 Then Conn.Open(cc.setConstr(2))
        Dim jsonstr As String
        Dim xh As String
        xh = context.Request("xh")

        Dim rs As New ADODB.Recordset
        rs.Open("select * from homework where xsno='" & xh & "'", Conn, 1, 1)
        If rs.EOF Then
            jsonstr = "{""msgid"":""001""}"
        Else
            jsonstr = "{""msgid"":""001"",""xh"":""" & xh & ""","
            jsonstr &= """zf"":""" & rs.Fields("zf").Value & ""","
            jsonstr &= """zy1"":""" & rs.Fields("zy1").Value & ""","
            jsonstr &= """zy2"":""" & rs.Fields("zy2").Value & ""","
            jsonstr &= """zy3"":""" & rs.Fields("zy3").Value & ""","
            jsonstr &= """zy4"":""" & rs.Fields("zy4").Value & ""","
            jsonstr &= """zy5"":""" & rs.Fields("zy5").Value & ""","
            jsonstr &= """zy6"":""" & rs.Fields("zy6").Value & ""","
            jsonstr &= """zy7"":""" & rs.Fields("zy7").Value & ""","
            jsonstr &= """zy8"":""" & rs.Fields("zy8").Value & ""","
            jsonstr &= """zy9"":""" & rs.Fields("zy9").Value & ""","
            jsonstr &= """zy10"":""" & rs.Fields("zy10").Value & ""","
            jsonstr &= """zy11"":""" & rs.Fields("zy11").Value & ""","
            jsonstr &= """zy12"":""" & rs.Fields("zy12").Value & ""","
            jsonstr &= """zy13"":""" & rs.Fields("zy13").Value & ""","
            jsonstr &= """zy14"":""" & rs.Fields("zy14").Value & ""","
            jsonstr &= """zy15"":""" & rs.Fields("zy15").Value & ""","
            jsonstr &= """zy16"":""" & rs.Fields("zy16").Value & """"
            jsonstr &= "}"
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