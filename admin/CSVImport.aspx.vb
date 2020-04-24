Imports System.IO

Public Class CSVImport
    Inherits System.Web.UI.Page
    Public Dbord, DBname, TableMode, Cid, SaveLocation As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CC.Connecttodb()
        Dbord = Request("dbord")
        DBname = Request("dbname")
        TableMode = Request("TableMode")
        Cid = Request("cid")

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim jg As String = ""
        Dim jgx As String = ""
        Dim iok As Integer = 0

        jgx = uploadCSV(Dbord)
        If jgx = "0" Then
            jg = ".....文件导入失败！<br>"
            iok = 1
        Else
            jg &= jgx
        End If

        If iok = 0 Then
            jgx = phaseFiles(Dbord, DBname， TableMode)
            If Len(jgx) < 8 Then
                jg = jgx & ".....导入文件数据结构不正确！<br>"
                iok = 1
            Else
                jg &= jgx
            End If

        End If

        If iok = 0 Then
            jg &= importData(Dbord, DBname)
        End If

        Label1.Text &= jg

        'jg = "<script language = ""javascript"" >"
        'jg &= "parent.location.reload();"
        'jg &= "</script>"
        'Response.Write(jg)

    End Sub


    ' 文件上传
    Function uploadCSV(ByVal DBord As String) As String
        Dim jg As String = ""
        '保存CSV文件
        If Not File1.PostedFile Is Nothing And File1.PostedFile.ContentLength > 0 Then
            Dim filename, fsize As String
            Dim mPath As String = ""
            mPath = Center_CsvUrl & "\" & CC.DBord2path(DBord)
            filename = CC.CheckFileName(System.IO.Path.GetFileName(File1.PostedFile.FileName))
            fsize = FormatNumber(File1.PostedFile.ContentLength / 1024, 1) & "k"

            SaveLocation = mPath & "/" & filename
            File1.PostedFile.SaveAs(SaveLocation)

            jg = ".....文件导入成功:" & filename & "|" & fsize & "<br>"
        Else
            jg = "0"
        End If
        Return jg
    End Function

    '文件分析
    Function phaseFiles(ByVal DBord As String, ByVal DBname As String, ByVal TableMode As String) As String
        Dim line As String = ""
        Dim jg As String = ""
        Dim f1, f2 As Object
        f1 = Split(TableMode, "|")
        Dim objStreamReader As New StreamReader(SaveLocation, System.Text.Encoding.Default)
        line = objStreamReader.ReadLine()
        line = objStreamReader.ReadLine()
        f2 = CC.CsvSplit(line)
        objStreamReader.Close()
        If UBound(f1) = UBound(f2) Then
            jg = ".....导入文件数据结构分析成功！<br>"
        Else
            jg = UBound(f1) & ":" & UBound(f2)
        End If


        Return jg
    End Function


    Function importData(ByVal DBord As String, ByVal DBname As String) As String
        Dim line As String = ""
        Dim jg As String = ""
        Dim i, iEdit, iAdd, iTotal As Integer
        Dim f2 As Object
        Dim Conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        If Conn.State = 0 Then Conn.Open(CC.setConstr(DBord))
        iEdit = 0
        iAdd = 0
        iTotal = 0
        Dim objStreamReader As New StreamReader(SaveLocation, System.Text.Encoding.Default)
        line = objStreamReader.ReadLine()
        While objStreamReader.Peek() <> -1
            line = objStreamReader.ReadLine()
            f2 = CC.CsvSplit(line)
            rs.Open("select * from " & DBname & " where id=" & f2(0), Conn, 1, 3)
            If (rs.EOF) Then
                rs.AddNew()
                iAdd = iAdd + 1
            Else
                iEdit = iEdit + 1
            End If
            For i = 1 To UBound(f2)
                rs.Fields(i).Value = f2(i)
            Next
            rs.Update()
            rs.Close()
        End While
        objStreamReader.Close()
        iTotal = iEdit + iAdd
        jg &= "共导入数据" & iTotal & "条,其中:添加记录" & iAdd & "条,修改记录" & iEdit & "条<br>"

        Return jg
    End Function

End Class