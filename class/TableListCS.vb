Imports System
Imports System.Web.HttpResponse
Public Class TableListCS
    Public Sub MExcel(ByVal Conn As ADODB.Connection, ByVal DBname As String, ByVal FieldNum As Integer, ByVal FieldName() As String, ByVal TjExpression As String, ByVal Skey As String)
        Dim i As Integer
        Dim sql As String
        Dim rs As New ADODB.Recordset
        Dim rs2 As New ADODB.Recordset

        System.Web.HttpContext.Current.Response.Clear()
        System.Web.HttpContext.Current.Response.Charset = "UTF-8"
        System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8
        System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default
        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;FileName=" & HttpUtility.UrlEncode("result.csv", System.Text.Encoding.UTF8))
        System.Web.HttpContext.Current.Response.ContentType = "application/x-download"


        For i = 0 To FieldNum
            System.Web.HttpContext.Current.Response.Write(FieldName(i) & ",")
        Next
        System.Web.HttpContext.Current.Response.Write(vbCrLf)
        sql = "select * from " & DBname & TjExpression
        rs.Open(sql, Conn, 1, 1)
        While Not rs.EOF
            For i = 0 To FieldNum
                System.Web.HttpContext.Current.Response.Write(rs.Fields(i).Value & ",")
            Next
            System.Web.HttpContext.Current.Response.Write(vbCrLf)
            rs.MoveNext()
        End While
        rs.Close()

        System.Web.HttpContext.Current.Response.Flush()
        System.Web.HttpContext.Current.Response.Close()
        System.Web.HttpContext.Current.Response.End()

    End Sub

    ''' <summary>
    ''' 显示数据库数据列表
    ''' 显示表格内容
    ''' </summary>
    ''' <param name="FieldName"></param>
    ''' <param name="isPic"></param>
    ''' <param name="lx"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ShowPage(ByVal Conn As ADODB.Connection, ByVal DBORD As String, ByVal DBname As String, ByVal TableMode As String, ByVal FieldNum As Integer, ByVal FieldName() As String, ByVal FileName As String, ByVal FileName2 As String, ByVal TjExpression As String， ByVal isPic As Integer, ByVal lx As String, ByVal px As String, ByVal w As Integer, ByVal h As Integer, ByVal Cid As String， ByVal Skey As String) As String
        Dim jg, jgx, sql, bgc As String
        Dim j, id, k As Integer
        Dim rn, pn As Long
        jg = Drawpage(FieldNum, FieldName)
        Dim rs As New ADODB.Recordset

        '计算页号和显示页码
        rs.Open("select count(id) from " & DBname & TjExpression, Conn, 1, 1)
        If Not rs.EOF Then
            rn = rs.Fields(0).Value
        Else
            rn = 0
        End If
        'pn 为总页面
        pn = CLng(rn / PAGENUMS + 0.5)
        rs.Close()
        jgx = "页码(" & px & "/" & pn & "):"
        '根据当前的页号，计算要显示的序列
        k = px \ 10
        For i = k * 10 + 1 To k * 10 + 10
            If i > pn Then Exit For
            If i = px Then
                jgx &= "<a href=" & FileName & "?Cid=" & Cid & "&px=" & i & "&skey=" & Skey & "><font color=#000><b>" & i & "</b></font></a> "
            Else
                jgx &= "<a href=" & FileName & "?Cid=" & Cid & "&px=" & i & "&skey=" & Skey & ">" & i & "</a> "
            End If
        Next

        jgx &= "<a href=# onclick=""" & cc.ShowDialog(1, "../" & FileName2 & "?dbord=" & DBORD & "&dbname=" & DBname & "&TableMode=" & TableMode & "&Cid=" & Cid & "&id=0&isPIC=" & isPic， "添加项目", w, h) & """> + 添加一条新纪录...</a>"
        jgx &= " | <input type=text name=skey value='" & Skey & "' size=10><input type=button name=search value='搜索' onclick=""location.href='" & FileName & "?Cid=" & Cid & "&px=1&skey='+form1.skey.value"">"
        jgx &= " | <a href=" & FileName & "?Cid=" & Cid & "&skey=" & Skey & "&gn=Excel> 打包EXCEL下载</a>"
        jgx &= " | <a href=# onclick=""" & cc.ShowDialog(1, "../CSVImport.aspx?dbord=" & DBORD & "&dbname=" & DBname & "&TableMode=" & TableMode & "&Cid=" & Cid & "&id=0&isPIC=" & isPic， "CSV格式文件导入", 800, 400) & """>  CSV格式文件导入...</a>"
        jg = Replace(jg, "<<页码信息>>", jgx)

        '正式显示数据
        If px = 1 Then
            sql = "select top " & PAGENUMS & " * from " & DBname & TjExpression
            sql &= " order by id "
        Else
            sql = "select top " & PAGENUMS & " * from " & DBname
            sql &= TjExpression & " AND (id NOT IN "
            sql &= " ( SELECT TOP " & PAGENUMS * (px - 1)
            sql &= " id FROM " & DBname & TjExpression & " ORDER BY id)) order by id"
        End If

        rs.Open(sql, Conn, 1, 1)
        jgx = ""
        bgc = "bgcolor=#ffffff"
        Dim CellNr As String = ""
        While Not rs.EOF
            id = rs.Fields(0).Value
            If bgc = "bgcolor=#ffffff" Then
                bgc = "bgcolor=#eeeeee"
            Else
                bgc = "bgcolor=#ffffff"
            End If
            jgx &= "<tr " & bgc & " >"
            For j = 0 To FieldNum
                '处理一下字段内容
                CellNr = rs.Fields(j).Value.ToString
                CellNr = Replace(CellNr, "<", "&lt;")
                CellNr = Replace(CellNr, ">", "&gt;")

                jgx &= "<td>" & IIf(Len(CellNr) > 30, Left(CellNr, 30) & "...", CellNr) & "</td>"
            Next
            jgx &= "<td>"
            jgx &= "<a href=# onclick=""if(confirm('是否删除本行数据?')) { window.location.href='" & FileName & "?Cid=" & Cid & "&lx=" & lx & "&px=" & px & "&gn=del&id=" & id & "'; }"">删除</a>"
            jgx &= "|"
            jgx &= "<a href=# onclick=""" & cc.ShowDialog(1, "../" & FileName2 & "?Cid=" & Cid & "&dbord=" & DBORD & "&dbname=" & DBname & "&TableMode=" & TableMode & "&id=" & id & "&isPIC=" & isPic， "编辑项目", w， h) & """>修改</a>"

            '根据dbName的额外追加功能
            '案例：参看雄风电气的承修、承试报价表
            jgx &= TLS.Addfunction(DBname, id, 2)

            jgx &= "</td>"
            jgx &= "</tr>"
            rs.MoveNext()
        End While
        rs.Close()
        jg = Replace(jg, "<<数据信息>>", jgx)

        Return jg
    End Function
    ''' <summary>
    ''' 画一个表格
    ''' </summary>
    ''' <param name="FieldName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Drawpage(ByVal FieldNum As Integer, ByVal FieldName() As String) As String
        Dim jgx As String
        Dim i As Integer
        jgx = "<table border=1 cellspacing=0 cellpadding=1 width=98% style=""border-collapse:collapse;"">"
        jgx &= "<tr>"
        For i = 0 To FieldNum
            jgx &= "<td>" & FieldName(i) & "</td>"
        Next
        jgx &= "<td>操作</td>"
        jgx &= "</tr>"
        jgx &= "<<数据信息>>"
        jgx &= "</table>"
        jgx &= "<div>"
        jgx &= "<<页码信息>>"
        jgx &= "</div>"
        Return jgx

    End Function

    Public Function setdialog() As String
        Dim jg As String = ""
        jg &= "<div id = ""msgDiv"" >"
        jg &= "<table border=0 style=""width:100%;""><tr><td><div id=""msgTitle"">窗口</div></td></td>"
        jg &= "<td style=""width:50px;""><div id=""msgShut"">关闭&nbsp;</div></td></tr></table>"
        jg &= "<div id = ""msgDetail"" > </div>"
        jg &= "</div>"
        jg &= "<div id = ""bgDiv""></div>"
        Return jg
    End Function

    Public Function Addfunction(ByVal DbName As String, ByVal xid As String, ByVal iMode As Integer) As String

        Dim jg As String = ""
        Dim ml As String = ""
        Dim Conn2 As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        If Conn2.State = 0 Then Conn2.Open(cc.setConstr(1))
        rs.Open("select  * from TBDict where TBName='" & DbName & "'", Conn2, 1, 1)
        If Not rs.EOF Then
            If Len(trim(rs.Fields("ml").Value)) > 1 Then
                If iMode = 1 Then
                    ml = trim(rs.Fields("ml").Value)
                End If
                Dim i As Integer = 1
                For i = 1 To 3
                    If Len(trim(rs.Fields("furl" & i).Value)) > 1 Then
                        jg &= " | <a target=Chlidb href=" & ml & rs.Fields("furl" & i).Value & "?Cid=" & xid & ">" & rs.Fields("fname" & i).Value & "</a>"
                    End If
                Next
            End If

        End If
        rs.Close()
        Conn2.Close()

        Return jg

    End Function


End Class
