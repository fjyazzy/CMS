Imports System.IO
Imports System.Net
Imports System.Security.Cryptography
Imports System.Web
Imports System.Web.Services
Imports System.Xml
Public Class wxv2Class
    Public Function ReadMessage2XML(ByVal context As HttpContext) As XmlDocument
        Dim postString As String
        Dim stream As Stream
        stream = context.Request.InputStream
        Dim postBytes(stream.Length) As Byte
        stream.Read(postBytes, 0, stream.Length)
        postString = Encoding.UTF8.GetString(postBytes)
        Dim xml As New XmlDocument()
        xml.InnerXml = postString
        Return xml
    End Function
    Public Function ReadMessage2String(ByVal context As HttpContext) As String
        Dim postString As String
        Dim stream As Stream
        stream = context.Request.InputStream
        Dim postBytes(stream.Length) As Byte
        stream.Read(postBytes, 0, stream.Length)
        postString = Encoding.UTF8.GetString(postBytes)
        Dim xml As String
        xml = postString
        Return xml
    End Function
    Public Function phaseJSON(ByVal jsonStr As String, ByVal item As String) As String
        Dim newJsonStr, jg As String
        Dim f, g As Object
        Dim i As Integer
        jg = ""
        newJsonStr = Replace(jsonStr, "{", "")
        newJsonStr = Replace(newJsonStr, "}", "")
        newJsonStr = Replace(newJsonStr, """", "")
        f = Split(newJsonStr, ",")
        For i = 0 To UBound(f)
            g = Split(f(i), ":")
            If g(0) = item Then
                jg = g(1)
            End If
        Next
        Return jg
    End Function

End Class
