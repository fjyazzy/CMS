Public Class JsonClass
    '获取JSON string 的节点数量
    Public Function getNodeNum(ByVal jsonStr As String) As Integer
        Dim iLen As Integer = 0

        Return iLen
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

    '创建一个空的JSON  节点
    Public Function CreateEmptyJson() As String
        Dim JsonStr As String
        JsonStr = "'[]'"
        Return JsonStr
    End Function

    Public Function AddNode(ByVal JsonStr As String, ByVal name() As String, ByVal value() As String, ByVal isEnd As Boolean) As String
        Dim i As Integer = 0

        '找到可以在JSON字符串中插入新数值的位置
        '就是 结束符号]的前面
        '然后把json 字符串分成前后两个部分
        Dim iend As Integer = 0
        Dim HalfaStr, HalfbStr As String
        iend = InStr(JsonStr, "]")
        HalfaStr = Mid(JsonStr, 1, iend - 1)
        HalfbStr = Mid(JsonStr, iend)
        '按JSON格式加入新的数值
        HalfaStr = HalfaStr & "{"
        For i = 0 To UBound(name) - 1
            HalfaStr = HalfaStr & """" & name(i) & """:"
            HalfaStr = HalfaStr & """" & value(i) & ""","
        Next
        HalfaStr = HalfaStr & """" & name(i) & """:"
        HalfaStr = HalfaStr & """" & value(i) & """"
        HalfaStr = HalfaStr & "}"
        If Not isEnd Then
            HalfaStr = HalfaStr & ","
        End If

        JsonStr = HalfaStr & HalfbStr
        Return JsonStr
    End Function


End Class
