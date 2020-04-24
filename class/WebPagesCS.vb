Imports System.IO
Public Class WebPagesCS
    ' 从systeminfo表中取nr数据
    Public Function GetSystemInfo(ByVal itemno As String) As String
        Dim Conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        Dim jg As String = ""
        CC.Connecttodb()
        If Conn.State = 0 Then Conn.Open(CC.setConstr(DBord_ecms))
        rs.Open("SELECT  * from systeminfo where itemno='" & itemno & "'", Conn, 1, 3, 1)
        If Not rs.EOF Then
            jg = Replace(rs.Fields("itemtext").Value, vbCrLf, " ")
        End If
        rs.Close()
        Conn.Close()
        Return jg
    End Function
    ' 从Webpages表中取nr数据
    Public Function GetWebContent(ByVal mc As String) As String
        Dim Conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        Dim jg As String = ""
        CC.Connecttodb()
        If Conn.State = 0 Then Conn.Open(CC.setConstr(DBord_ecms))
        rs.Open("SELECT  * from webpages where xm='" & mc & "'", Conn, 1, 3, 1)
        If Not rs.EOF Then
            jg = Replace(rs.Fields("nr").Value, vbCrLf, " ")
        End If
        rs.Close()
        Conn.Close()
        Return jg
    End Function
    ' 从Webpages表中按TITLE格式取nr数据
    Public Function GetTitle(ByVal str1 As String, ByVal str2 As String) As String
        'str1="Title | MetaContent | MetaKeywords"
        'str2 ="页面名称"
        Dim rsx As New ADODB.Recordset
        Dim Conn As New ADODB.Connection
        Dim jg As String
        CC.Connecttodb()
        If Conn.State = 0 Then Conn.Open(CC.setConstr(DBord_ecms))
        Try
            rsx.Open("select  * from webpages where xm='" & str1 & "_" & str2 & "'", Conn, 1, 1)
            jg = rsx.Fields("nr").Value
            rsx.Close()
        Catch ex As Exception
            jg = ("select  * from xxfb where lx='" & str1 & "_" & str2 & "'")
        End Try
        Return jg

    End Function
    ' 从Category_product表中取分类列表
    Public Function GetCategoryList() As String
        Dim rs As New ADODB.Recordset
        Dim sql, jg As String
        Dim conn As New ADODB.Connection
        Dim lixa As Integer
        CC.Connecttodb()
        If conn.State = 0 Then conn.Open(CC.setConstr(DBord_ecms))
        BJS = GetSystemInfo("013")
        jg = "<div style=""cursor:hand;background-color:" & BJS & ";"" onmouseover=""ShowDiv('clist')"" onmouseout=""HideDiv('clist')"">产品分类</div>"
        sql = "SELECT  * from category_products"
        rs.Open(sql, conn, 0, 1, 1)
        jg &= "<div id=""clist"" style=""POSITION: absolute; DISPLAY: none;background-color:" & BJS & ";""  onmouseover=""ShowDiv('clist')""  onmouseout=""HideDiv('clist')""><table border=0 width=100% >"
        While Not rs.EOF
            If lixa = 0 Then
                jg &= "<tr>"
            End If
            jg &= "<td>"
            jg &= "<a href=catalogs.aspx?id=" & rs.Fields("id").Value & " alt=""" + rs.Fields("catalogDesc").Value + """>"
            jg &= "<b>" & rs.Fields("catalogname").Value & "</b></a>"
            jg &= "</td>"

            rs.MoveNext()
            If lixa = 0 Then
                jg &= "</tr>"
                lixa = 0
            Else
                lixa = lixa + 1
            End If
        End While

        rs.Close()
        conn.Close()
        jg &= "</table></div>"

        Return jg

    End Function
    ' 制作搜索框
    Public Function GetSearchBox()
        Dim jg As String
        jg = "<form action=catalogs.aspx>"
        jg &= "<input type=text name=skey size=60>"
        jg &= "<input type=submit name='搜索' value='搜索'>"
        jg &= "</form>"
        Return jg

    End Function

    ' 显示产品列表
    Function SearchP(ByVal mode As String, ByVal Skey As String, ByVal pagex As String, ByVal thisprog As String) As String

        Dim rs As New ADODB.Recordset
        Dim sql, jg As String
        Dim conn As New ADODB.Connection
        Dim page As Integer
        Dim i, j As Integer
        If Trim(pagex) = "" Then
            page = 1
        Else
            page = CInt(pagex)
        End If
        jg = ""

        CC.Connecttodb()
        If conn.State = 0 Then conn.Open(CC.setConstr(DBord_ecms))
        BJS = GetSystemInfo("013")
        ANS = GetSystemInfo("012")

        Select Case mode
            Case 1
                '带关键字
                sql = "SELECT top 200 id,type,fz,manufactory,shortDesc,productimg from ecms_products where isdel=0 and (type like '" & Skey & "%') order by visitTime desc, uptime desc,id desc "
            Case 2
                '带类型type
                sql = "SELECT top 200 id,type,fz,manufactory,shortDesc,productimg from ecms_products where isdel=0 and categoryid=" & Skey & " order by visitTime desc , uptime desc,id desc"
            Case 3
                sql = "SELECT top 15  id,type,fz,manufactory,shortDesc,productimg from ecms_products where isdel=0 order by visitTime desc ,uptime desc,id desc"
            Case 4
                sql = "SELECT top 15 id,type,fz,manufactory,shortDesc,productimg from ecms_products where isdel=0 and categoryid=" & Skey & " order by visitTime desc ,uptime desc,id desc"
            Case 6
                sql = "SELECT top 25 id,type from ecms_products where isdel=0 and categoryid=" & Skey & " order by visitTime desc ,uptime desc,id desc"
            Case 8
                sql = "SELECT top 8 id,type,productimg  from ecms_products where isdel=0 and len(productimg)>5 order by visitTime desc ,uptime desc,id desc"
            Case 15
                sql = "SELECT TOP 8 id,type type FROM ecms_Products WHERE (IsDel = 0) ORDER BY VisitTime DESC"
            Case 9
                sql = "SELECT top 15 id,type  from ecms_products where isdel=0 order by visitTime desc ,uptime desc,id desc"
            Case 10
                sql = "SELECT top 200 id,type,fz,manufactory,shortDesc,productimg from ecms_products where isdel=0 and (type like '%" & Skey & "%' or shortDesc like '%" & Skey & "%') order by visitTime desc ,uptime desc,id desc"
            Case Else
                sql = "SELECT top 200 id,type,fz,manufactory,shortDesc,productimg from ecms_products where isdel=0 and (type like '%" & Skey & "%' or manufactory like '%" & Skey & "%' or shortdesc like '%" & Skey & "%')  order by visitTime desc ,uptime desc,id desc"
        End Select

        rs.Open(sql, conn, 1, 1, 1)
        If rs.EOF Then
            jg = "<table border=0 widht=998><tr><td>您要的型号我们正在更新中..<a target=_blank href=Buy.aspx?p=" & Skey & ">&nbsp&nbsp购买 请输入您要找的型号:" & Skey & " &nbsp;&nbsp;</a></td></tr></table>"
            Return jg
        End If

        jg &= ("<table border=0 width=100% cellpadding=0 callspacing=0 bgcolor=" & BJS & ">")
        Select Case mode
            Case 2, 10
                rs.PageSize = 20
                jg &= "<tr bgcolor=" & BJS & "><td align=right colspan=12>"
                If page = 0 Then page = 1
                If page < 1 Then page = 1
                If page > rs.PageCount Then page = rs.PageCount
                If page <> 1 Then
                    jg &= "&nbsp;<A class=btn3 HREF=" & thisprog & "?mode=" & mode & "&skey=" & Skey & "&id=" & Skey & "&page=1 >首页</A>"
                    jg &= "&nbsp;<A class=btn3 HREF=" & thisprog & "?mode=" & mode & "&skey=" & Skey & "&id=" & Skey & "&page=" & (page - 1) & ">上一页</A>"
                End If
                If (page <> rs.PageCount) Then
                    jg &= "&nbsp;<A class=btn3 HREF=" & thisprog & "?mode=" & mode & "&skey=" & Skey & "&id=" & Skey & "&page=" & (page + 1) & ">下一页</A>"
                    jg &= "&nbsp;<A class=btn3 HREF=" & thisprog & "?mode=" & mode & "&skey=" & Skey & "&id=" & Skey & "&page=" & rs.PageCount & ">尾页</A>"
                End If
                jg &= "&nbsp;页码：" & page & "/" & rs.PageCount
                jg &= "</td></tr>"
            Case 3, 4
                rs.PageSize = 12
                page = 1
            Case 8, 15
                rs.PageSize = 28
                page = 1
            Case 9, 6
                rs.PageSize = 1000
                page = 1
            Case Else
                page = 1
        End Select
        rs.AbsolutePage = page

        '设置模式 1：原文  2：html
        Dim imode As Integer = 2

        Select Case mode
            Case 8
                For i = 1 To rs.PageSize
                    If j = 0 Then
                        jg &= ("<tr>")
                    End If
                    Dim ll As String
                    If imode = 1 Then
                        ll = "Product.aspx?id=" & rs.Fields("id").Value & "&p=" & rs.Fields("type").Value & "&type=" & rs.Fields("type").Value
                    Else
                        ll = Checkstr(rs.Fields("type").Value) & "_" & rs.Fields("id").Value & ".html"
                    End If
                    jg &= ("<td><a target=" & rs.Fields("type").Value & " href=""" & ll & """ target=blank><img src=photos/t_" & rs.Fields("productimg").Value & " width=100 height=100 border=0></a></td>")
                    If j = 7 Then
                        jg &= ("</tr>")
                        j = 0
                    Else
                        j = j + 1
                    End If
                    rs.MoveNext()
                    If rs.EOF Then Exit For
                Next
            Case 15
                jg &= ("<tr><td>热门搜索:")
                For i = 1 To rs.PageSize
                    jg &= ("<A href=catalogs.aspx?Mode=1&amp;skey=" & rs.Fields("type").Value & " target=" & rs.Fields("type").Value & ">" & rs.Fields("type").Value & "</a>| ")
                    rs.MoveNext()
                    If rs.EOF Then Exit For
                Next
                jg &= ("</td></tr>")
            Case 6
                jg &= ("<tr><td>")
                For i = 1 To rs.PageSize
                    Dim ll As String
                    If imode = 1 Then
                        ll = "Product.aspx?id=" & rs.Fields("id").Value & "&p=" & rs.Fields("type").Value & "&type=" & rs.Fields("type").Value
                    Else
                        ll = Checkstr(rs.Fields("type").Value) & "_" & rs.Fields("id").Value & ".html"
                    End If

                    jg &= ("<a href=""" & ll & """ target=" & rs.Fields("type").Value & ">" & rs.Fields("type").Value & "</a> | ")
                    rs.MoveNext()
                    If rs.EOF Then Exit For
                Next
                jg &= ("</td></tr>")

            Case 9
                For i = 1 To rs.PageSize
                    jg &= ("<tr>")
                    Dim ll As String
                    If imode = 1 Then
                        ll = "Product.aspx?id=" & rs.Fields("id").Value & "&p=" & rs.Fields("type").Value & "&type=" & rs.Fields("type").Value
                    Else
                        ll = Checkstr(rs.Fields("type").Value) & "_" & rs.Fields("id").Value & ".html"
                    End If
                    jg &= ("<td><a href=""" & ll & """ target=" & rs.Fields("type").Value & ">" & rs.Fields("type").Value & "</a></td>")
                    jg &= ("</tr>")
                    rs.MoveNext()
                    If rs.EOF Then Exit For
                Next
            Case 3
                jg &= ("<tr bgcolor='" & ANS & "' height=25><td colspan=7><strong> &nbsp;◎最新上架产品及资料<strong></td></tr>")
                For i = 1 To rs.PageSize
                    jg &= ("<tr bgcolor=" & BJS & " onmouseout='this.bgColor=""" & BJS & """' onmouseover='this.bgColor=""" & ANS & """'>")

                    Dim ll As String
                    If imode = 1 Then
                        ll = "Product.aspx?id=" & rs.Fields("id").Value & "&p=" & rs.Fields("type").Value & "&type=" & rs.Fields("type").Value
                    Else
                        ll = Checkstr(rs.Fields("type").Value) & "_" & rs.Fields("id").Value & ".html"
                    End If
                    jg &= ("<td><a href=""" & ll & """ target=" & rs.Fields("type").Value & ">" & rs.Fields("type").Value & "</a></td>")

                    jg &= ("<td>" & rs.Fields("manufactory").Value & "</td>")
                    If Len(Trim(rs.Fields("fz").Value)) > 0 Then
                        jg &= ("<td>" & Left(Trim(rs.Fields("fz").Value), 10) & "...</td>")
                    Else
                        jg &= ("<td>" & rs.Fields("fz").Value & "</td>")
                    End If
                    If Len(Trim(rs.Fields("shortDesc").Value)) > 0 Then
                        jg &= ("<td>" & Left(Trim(rs.Fields("shortDesc").Value), 10) & "...</td>")
                    Else
                        jg &= ("<td>" & rs.Fields("shortDesc").Value & "</td>")
                    End If

                    If rs.Fields("productimg").Value <> "" Then
                        jg &= ("<td><a href='<<Remote_Center_Photos>>/" & rs.Fields("productimg").Value & "' target=" & rs.Fields("type").Value & "><img src=images/pic.gif border=0></td>")
                    Else
                        jg &= ("<td><a href='images/logo.gif' target=" & rs.Fields("type").Value & "><img src=images/pic.gif border=0  alt=" & rs.Fields("type").Value & "></td>")
                    End If
                    jg &= ("<td><a target=_blank href=Mcatalogs.aspx?skey=" & rs.Fields("type").Value & "&type=" & rs.Fields("type").Value & " ><img src=images/pdf.gif border=0 alt=" & rs.Fields("type").Value & "></a></td>")
                    jg &= ("<td><a href=Buy.aspx?p=" & rs.Fields("type").Value & "&id=" & rs.Fields("id").Value & "&p=" & rs.Fields("type").Value & "&type=" & rs.Fields("type").Value & " target=" & rs.Fields("type").Value & " ><img src=images/buy.gif border=0></a></td>")

                    jg &= ("</tr>")
                    jg &= "<tr><td colspan=10 bgcolor=#bfbfbf height=1></td></tr>"

                    rs.MoveNext()
                    If rs.EOF Then Exit For
                Next

            Case Else
                jg &= ("<tr bgcolor=" & ANS & "><td>产品型号</td><td>厂商</td><td>封装</td><td>简要描述</td><td>图片</td><td>资料</td><td>订购</td></tr> ")
                For i = 1 To rs.PageSize
                    jg &= ("<tr bgcolor=" & BJS & " onmouseout='this.bgColor=""" & BJS & """' onmouseover='this.bgColor=""" & ANS & """'>")
                    Dim ll As String
                    If imode = 1 Then
                        ll = "Product.aspx?id=" & rs.Fields("id").Value & "&p=" & rs.Fields("type").Value & "&type=" & rs.Fields("type").Value
                    Else
                        ll = Checkstr(rs.Fields("type").Value) & "_" & rs.Fields("id").Value & ".html"
                    End If
                    jg &= ("<td><a href=""" & ll & """ target=" & rs.Fields("type").Value & ">" & rs.Fields("type").Value & "</a></td>")
                    jg &= ("<td>" & rs.Fields("manufactory").Value & "</td>")
                    jg &= ("<td>" & rs.Fields("fz").Value & "</td>")
                    jg &= ("<td>" & rs.Fields("shortDesc").Value & "</td>")
                    If rs.Fields("productimg").Value <> "" Then
                        jg &= ("<td><a href='<<Remote_Center_Photos>>/" & rs.Fields("productimg").Value & "' target=" & rs.Fields("type").Value & "><img src=images/pic.gif border=0></td>")
                    Else
                        jg &= ("<td><a href='images/logo.gif' target=" & rs.Fields("type").Value & "><img src=images/pic.gif border=0  alt=" & rs.Fields("type").Value & "></td>")
                    End If

                    jg &= ("<td><a target=_blank href=Mcatalogs.aspx?skey=" & rs.Fields("type").Value & "&type=" & rs.Fields("type").Value & " ><img src=images/pdf.gif border=0 alt=" & rs.Fields("type").Value & "></a></td>")
                    jg &= ("<td><a href=Buy.aspx?p=" & rs.Fields("type").Value & "&id=" & rs.Fields("id").Value & "&p=" & rs.Fields("type").Value & "&type=" & rs.Fields("type").Value & " target=" & rs.Fields("type").Value & " ><img src=images/buy.gif border=0></a></td>")

                    jg &= ("</tr>")
                    jg &= "<tr><td colspan=10 bgcolor=#bfbfbf height=1></td></tr>"

                    rs.MoveNext()
                    If rs.EOF Then Exit For
                Next


        End Select
        jg &= ("</table>")
        rs.Close()
        conn.Close()
        Return jg

    End Function
    ' 显示产品目录列表
    Function pCateList(ByVal thisprog As String) As String
        Dim rs As New ADODB.Recordset
        Dim sql, jg As String
        Dim conn As New ADODB.Connection
        Dim lixa As Integer

        CC.Connecttodb()
        If conn.State = 0 Then conn.Open(CC.setConstr(DBord_ecms))

        sql = "SELECT  * from category_products"
        rs.Open(sql, conn, 0, 1, 1)
        jg = "<table border=0 width=100% >"
        While Not rs.EOF
            If lixa = 0 Then
                jg &= "<tr>"
            End If
            jg &= "<td><table border=0 width=100% cellspacing=1 cellpadding=0>"
            jg &= "<tr><td>类别:<a href=catalogs.aspx?id=" & rs.Fields("id").Value & "><b>" & rs.Fields("catalogname").Value & "</b></a>"
            jg &= " - " & rs.Fields("catalogDesc").Value
            jg &= "</td></tr><tr><td>"
            ' jg &= cc.SearchP(4, rs.Fields("id").Value, 1, thisprog)
            jg &= "</td></tr></table>"
            jg &= "</td>"

            rs.MoveNext()
            If lixa = 0 Then
                jg &= "</tr>"
                lixa = 0
            Else
                lixa = lixa + 1
            End If
        End While

        rs.Close()
        conn.Close()
        jg &= "</table>"
        Return jg
    End Function
    ' 显示资料目录列表
    Function CateList(ByVal thisprog) As String
        Dim rs As New ADODB.Recordset
        Dim sql, jg As String
        Dim conn As New ADODB.Connection
        CC.Connecttodb()
        If conn.State = 0 Then conn.Open(CC.setConstr(DBord_ecms))

        sql = "SELECT  * from category_manuals"
        rs.Open(sql, conn, 0, 1, 1)
        jg = ""
        While Not rs.EOF
            jg = jg & "<h3>类别:<a href=category.aspx?id=" & rs.Fields("id").Value & "><b>" & rs.Fields("catalogname").Value & "</b></a></h3>"
            jg = jg & "<table style=""width:100%""><tr><td>"
            jg = jg & SearchZl(2, rs.Fields("id").Value, 1, thisprog)
            jg = jg & "</td></tr></table>"
            rs.MoveNext()
        End While
        rs.Close()
        conn.Close()
        jg = jg & "</table>"
        Return jg

    End Function
    ' 显示资料列表
    Function SearchZl(ByVal mode As Integer, ByVal Skey As String, ByVal page As Integer, ByVal thisprog As String) As String
        Dim rs As New ADODB.Recordset
        Dim sql, jg As String
        Dim conny As New ADODB.Connection
        Dim i As Integer
        jg = ""
        CC.Connecttodb()
        If conny.State = 0 Then conny.Open(CC.setConstr(DBord_ecms))
        BJS = GetSystemInfo("013")
        ANS = GetSystemInfo("012")

        Select Case mode
            Case 1
                sql = "SELECT  top 200 id,type,manufactory,shortDesc,clicknum from manual where type like '%" & Skey & "%' or manufactory like '%" & Skey & "%' or shortdesc like '%" & Skey & "%'"
            Case 2
                sql = "SELECT  top 200 id,type,manufactory,shortDesc,clicknum from manual where categoryid=" & Skey & " order by id desc"
            Case 3
                sql = "SELECT top 20  id,type,manufactory,shortDesc,clicknum from manual order by id desc"
            Case 4
                sql = "SELECT top 20 id,type,manufactory,shortDesc,clicknum from manual where categoryid=" & Skey & " order by id desc"
            Case Else
                sql = "SELECT  top 200 id,type,manufactory,shortDesc,clicknum from manual where type like '%" & Skey & "%' or manufactory like '%" & Skey & "%' or shortdesc like '%" & Skey & "%'  order by id"
        End Select
        rs.Open(sql, conny, 1, 1, 1)
        If rs.EOF Then
            Return "数据更新中!"
        End If

        jg &= ("<table border=0 width=100% >")
        If mode <> 3 And mode <> 4 Then
            jg &= "<tr bgcolor=" & BJS & "><td align=right colspan=12>"
            If page = 0 Then page = 1
            If page < 1 Then page = 1
            If page > rs.PageCount Then page = rs.PageCount
            If page <> 1 Then
                jg &= "&nbsp;<A class=btn3 HREF=" & thisprog & "?mode=" & mode & "&skey=" & Skey & "&page=1>首页</A>"
                jg &= "&nbsp;<A class=btn3 HREF=" & thisprog & "?mode=" & mode & "&skey=" & Skey & "&page=" & (page - 1) & ">上一页</A>"
            End If
            If (page <> rs.PageCount) Then
                jg &= "&nbsp;<A class=btn3 HREF=" & thisprog & "?mode=" & mode & "&skey=" & Skey & "&page=" & (page + 1) & ">下一页</A>"
                jg &= "&nbsp;<A class=btn3 HREF=" & thisprog & "?mode=" & mode & "&skey=" & Skey & "&page=" & rs.PageCount & ">尾页</A>"
            End If
            jg &= "&nbsp;页码：" & page & "/" & rs.PageCount
            jg &= "</td></tr>"
        Else
            page = 1
        End If

        rs.PageSize = 20
        rs.AbsolutePage = page


        jg &= ("<tr bgcolor=" & ANS & "><td width=600>资料名称</td><td>厂商</td><td>简要描述</td></tr> ")
        For i = 1 To rs.PageSize
            If rs.EOF Then Exit For
            jg &= ("<tr  bgcolor=" & BJS & " onmouseout='this.bgColor=""" & BJS & """' onmouseover='this.bgColor=""" & ANS & """'>")
            If Len(rs.Fields("type").Value) < 60 Then
                jg &= ("<td><a href=Manual.aspx?id=" & rs.Fields("id").Value & "&p=" & rs.Fields("type").Value & " target=blank>" & rs.Fields("type").Value & "(" & rs.Fields("clicknum").Value & ")</a></td>")
            Else
                jg &= ("<td><a href=Manual.aspx?id=" & rs.Fields("id").Value & "&p=" & rs.Fields("type").Value & " target=blank>" & Left(rs.Fields("type").Value, 60) & "...(" & rs.Fields("clicknum").Value & ")</a></td>")
            End If
            jg &= ("<td>" & rs.Fields("manufactory").Value & "</td>")

            If Len(rs.Fields("shortDesc").Value) < 60 Then
                jg &= ("<td>" & rs.Fields("shortDesc").Value & "</td>")
            Else
                jg &= ("<td>" & Left(rs.Fields("shortDesc").Value, 60) & "...</td>")
            End If


            jg &= ("</tr>")
            rs.MoveNext()
            If rs.EOF Then Exit For
        Next
        jg &= ("</table>")
        rs.Close()
        conny.Close()
        Return jg


    End Function
    Function lxfs() As String
        Dim jg As String
        jg = "<table border=1 cellspacing=1 cellpadding=2 bordercolor=#abcdef width=280 bgcolor=" & BJS & ">"
        jg &= "<tr><td colspan=2 align=center height=22  background=images/menubj.gif style='color:white;font-size:11pt;'><b>公司联系方式</b></td></tr>"


        Dim rsy As New ADODB.Recordset
        Dim Conny As New ADODB.Connection
        CC.Connecttodb()
        If Conny.State = 0 Then Conny.Open(CC.setConstr(DBord_ecms))

        rsy.Open("select xm,nr from webpages where lxbh='015' order by id", Conny, 1, 1)
        While Not rsy.EOF
            Select Case Trim(rsy.Fields("xm").Value)
                Case "电话"
                    jg &= "<tr><td bgcolor=" & ANS & " align=center>销售电话</td>"
                    jg &= "<td>" & rsy.Fields("nr").Value & "</td></tr>"
                Case "传真"
                    jg &= "<tr><td bgcolor=" & ANS & " align=center>传真</td>"
                    jg &= "<td>" & rsy.Fields("nr").Value & "</td></tr>"
                Case "手机"
                    jg &= "<tr><td bgcolor=" & ANS & " align=center>手机热线</td>"
                    jg &= "<td>" & rsy.Fields("nr").Value & "</td></tr>"
                Case "QQ"
                    jg &= "<tr><td bgcolor=" & ANS & " align=center>联系QQ</td>"
                    jg &= "<td><a target=blank href=tencent://message/?uin=" & rsy.Fields("nr").Value & "&amp;Site=Leo&amp;Menu=yes><img border=0 SRC=http://wpa.qq.com/pa?p=1:" & rsy.Fields("nr").Value & ":10>(" & rsy.Fields("nr").Value & ")</a></td></tr>"
                Case "淘宝"
                    jg &= "<tr><td bgcolor=" & ANS & " align=center>淘宝联系</td>"
                    jg &= "<td><a target=""_blank"" href=""http://amos1.taobao.com/msg.ww?v=2&uid=" & rsy.Fields("nr").Value & "&s=1"" ><img border=""0"" src=""http://amos1.taobao.com/online.ww?v=2&uid=" & rsy.Fields("nr").Value & "&s=1"" alt=""" & rsy.Fields("nr").Value & """ /></a></td></tr>"
                Case "阿里旺旺"
                    jg &= "<tr><td bgcolor=" & ANS & " align=center>旺旺联系</td>"
                    jg &= "<td><a target=""_blank"" href=""http://amos.alicdn.com/getcid.aw?v=2&uid=" & rsy.Fields("nr").Value & "&site=cntaobao&s=1&groupid=0&charset=utf-8""><img border=""0"" src=""http://amos.alicdn.com/online.aw?v=2&uid=" & rsy.Fields("nr").Value & "&site=cntaobao&s=1&charset=utf-8"" alt=""点击这里给我发消息"" title=""点击这里给我发消息"" /></a></td></tr>"
                Case "SKYPE"
                    jg &= "<tr><td bgcolor=" & ANS & " align=center>SKYPE联系</td>"
                    jg &= "<td><a href=callto://" & rsy.Fields("nr").Value & "><img src=http://goodies.skype.com/graphics/skypeme_btn_small_yellow.gif border=0></a></td></tr>"
                Case "MSN"
                    jg &= "<tr><td bgcolor=" & ANS & " align=center>MSN联系</td>"
                    jg &= "<td><a href=""msnim:chat?contact=" & rsy.Fields("nr").Value & """><img border=0 size=18 SRC=images/msn.gif>:" & rsy.Fields("nr").Value & "</a></td></tr>"
            End Select
            rsy.MoveNext()
        End While
        rsy.Close()

        jg &= "</table>"
        Conny.Close()
        Return jg
    End Function
    '显示单个资料页面
    Function xxzl(ByVal cid)
        Dim rs As New ADODB.Recordset
        Dim rs2 As New ADODB.Recordset
        Dim sql, jg, saveFileName, jf As String
        Dim doit As Integer
        Dim conn As New ADODB.Connection
        CC.Connecttodb()
        If conn.State = 0 Then conn.Open(CC.setConstr(DBord_ecms))
        doit = 1
        jg = ""
        saveFileName = ""
        sql = "SELECT top 1 * from manual where id=" & cid
        rs.Open(sql, conn, 1, 3, 1)
        If rs.EOF Then
            jg = "资料不存在!"
            doit = 0
        Else
            jf = rs.Fields("point").Value
            saveFileName = rs.Fields("furl").Value
        End If
        If doit = 1 Then
            jg = "<table style=""font-size:14px;width:100%"">"
            jg = jg & "<tr><td bgcolor=#ffffff>资料名称</td><td  bgcolor=#ffffff>" & rs.Fields("type").Value & "</td></tr>"
            sql = "SELECT  * from Category_manuals where id=" & rs.Fields("Categoryid").Value
            rs2.Open(sql, conn, 1, 3, 1)
            If Not rs2.EOF Then
                jg = jg & "<tr><td  bgcolor=#ffffff>所在目录</td><td  bgcolor=#ffffff><a href=mcatalogs.aspx?id=" & rs2.Fields("id").Value & ">" & rs2.Fields("catalogname").Value & "</a></td></tr>"
            End If
            rs2.Close()
            jg = jg & "<tr><td bgcolor=#ffffff>厂商</td><td  bgcolor=#ffffff>" & rs.Fields("manufactory").Value & "</td></tr>"
            jg = jg & "<tr><td bgcolor=#ffffff>描述</td><td  bgcolor=#ffffff>" & rs.Fields("shortdesc").Value & "</td></tr>"
            jg = jg & "<tr><td bgcolor=#ffffff>点击查看</td><td  bgcolor=#ffffff>"
            jg = jg & ShowAtt(saveFileName)
            jg = jg & "</td></tr></table>"
        End If
        rs.Close()
        Return jg
    End Function
    Function ShowAtt(ByVal s)
        Dim f As Object
        Dim i As Integer
        Dim jg, mfile As String
        f = Split(s, "|")
        jg = "<ul>"
        For i = 0 To UBound(f) - 1 Step 2
            mfile = Center_ManualUrl & "\" & CC.DBord2path(DBord_ecms) & "\" & f(i)
            If File.Exists(mfile) Then
                jg &= "<li><a target=_blank href=viewManual.aspx?p=" & f(i) & ">" & f(i) & "(" & f(i + 1) & ")<img src=""images/pdf.gif"" border=0></a>"
            Else
                jg &= "<li><a target=_blank href=viewManual.aspx?p=" & "noManual.pdf>" & f(i) & "(" & f(i + 1) & ")<img src=""images/pdf.gif"" border=0></a>"
            End If
        Next
        jg &= "<ul>"
        Return jg

    End Function
    Sub countManual(ByVal cid)
        Dim rsy As New ADODB.Recordset
        Dim Conny As New ADODB.Connection
        CC.Connecttodb()
        If Conny.State = 0 Then Conny.Open(CC.setConstr(DBord_ecms))
        rsy.Open("select * from manual where id=" & cid, Conny, 1, 3)
        If Not rsy.EOF Then
            rsy.Fields("clicknum").Value = CInt(rsy.Fields("clicknum").Value) + 1
            rsy.Fields("VisitTime").Value = Now
            rsy.Update()
        End If
        rsy.Close()
        Conny.Close()
    End Sub
    Sub countProduct(ByVal cid)
        Dim rsy As New ADODB.Recordset
        Dim Conny As New ADODB.Connection
        CC.Connecttodb()
        If Conny.State = 0 Then Conny.Open(CC.setConstr(DBord_ecms))
        rsy.Open("select * from ecms_products where id=" & cid, Conny, 1, 3)
        If Not rsy.EOF Then
            rsy.Fields("VisitTime").Value = Now
            rsy.Update()
        End If
        rsy.Close()
        Conny.Close()
    End Sub
    Function getCateName4cid(ByVal Cid) As String
        Dim jg As String = ""
        Dim rs2 As New ADODB.Recordset
        Dim Conn As New ADODB.Connection
        Dim sql As String
        CC.Connecttodb()
        If Conn.State = 0 Then Conn.Open(CC.setConstr(DBord_ecms))
        sql = "SELECT * from category_products where id=" & Cid
        rs2.Open(sql, Conn, 1, 1, 1)
        If Not rs2.EOF Then
            jg &= "<h4>所在目录:<a href=catalogs.aspx?id=" & rs2.Fields("id").Value & ">" & rs2.Fields("catalogname").Value & "</a></h4>"
        End If
        rs2.Close()
        Return jg
    End Function
    Function GetManual4id(ByVal cid) As String
        'fv(0) rs.Fields("shortdesc").Value
        'fv(1) rs.Fields("manufactory").Value
        'fv(2) rs.Fields("type").Value
        '用“|”分割各个字段
        Dim jg As String = ""
        Dim rsy As New ADODB.Recordset
        Dim Conny As New ADODB.Connection
        CC.Connecttodb()
        If Conny.State = 0 Then Conny.Open(CC.setConstr(DBord_ecms))
        rsy.Open("select * from manual where id=" & cid, Conny, 1, 1)
        If Not rsy.EOF Then
            jg &= rsy.Fields("shortdesc").Value & "|"
            jg &= rsy.Fields("manufactory").Value & "|"
            jg &= rsy.Fields("type").Value & "|"
        Else
            jg = "1|2|3|4|5|6|7|8|"
        End If
        rsy.Close()
        Conny.Close()

        Return jg
    End Function
    Function GetProduct4id(ByVal cid) As String
        ' fv(0) rs.Fields("price").Value / 电询或面议
        ' fv(1) rs.Fields("shortdesc").Value
        ' fv(2) rs.Fields("manufactory").Value
        ' fv(3) rs.Fields("type").Value’
        ' fv(4) rs.Fields("productimg").Value
        ' fv(5) rs.Fields("productdesc").Value
        ' fv(6) rs.Fields("categoryid").Value
        ' fv(7) rs.Fields("fz").Value
        ' fv(8) rs.Fields("ph").Value
        ' fv(9) rs.Fields("qssl").Value
        '用“|”分割各个字段
        Dim jg As String = ""
        Dim rsy As New ADODB.Recordset
        Dim Conny As New ADODB.Connection
        CC.Connecttodb()
        If Conny.State = 0 Then Conny.Open(CC.setConstr(DBord_ecms))
        rsy.Open("SELECT  * from ecms_products where isdel=0 and id=" & cid, Conny, 1, 1)
        If Not rsy.EOF Then
            jg &= rsy.Fields("price").Value & "|"
            jg &= rsy.Fields("shortdesc").Value & "|"
            jg &= rsy.Fields("manufactory").Value & "|"
            jg &= rsy.Fields("type").Value & "|"
            jg &= rsy.Fields("productimg").Value & "|"
            jg &= rsy.Fields("productdesc").Value & "|"
            jg &= rsy.Fields("categoryid").Value & "|"
            jg &= rsy.Fields("fz").Value & "|"
            jg &= rsy.Fields("ph").Value & "|"
            jg &= rsy.Fields("qssl").Value & "|"
        Else
            jg &= "1|2|3|4|5|6|7|8|9|10|"
        End If
        rsy.Close()
        Conny.Close()

        Return jg

    End Function
    Function saveOrderItem(ByVal name, ByVal tel, ByVal fax, ByVal email, ByVal company, ByVal address, ByVal resumex, ByVal ywy, ByVal type, ByVal sl, ByVal manufactory, ByVal fz, ByVal jsj) As String
        Dim rs As New ADODB.Recordset
        Dim sql, jg As String
        Dim conn As New ADODB.Connection
        CC.Connecttodb()
        If conn.State = 0 Then conn.Open(CC.setConstr(DBord_ecms))
        jg = "ok"
        Try
            sql = "SELECT top 1 * from inquirys "
            rs.Open(sql, conn, 1, 3, 1)
            rs.AddNew()
            rs.Fields("type").Value = CC.Checkstr(type)
            rs.Fields("sl").Value = CC.Checkstr(sl)
            rs.Fields("manufactory").Value = manufactory
            rs.Fields("fz").Value = CC.Checkstr(fz)
            rs.Fields("ph").Value = ""
            rs.Fields("jsj").Value = CC.Checkstr(jsj)
            rs.Fields("mobile").Value = ""
            rs.Fields("name").Value = CC.Checkstr(name)
            rs.Fields("phone").Value = CC.Checkstr(tel)
            rs.Fields("fax").Value = CC.Checkstr(fax)
            rs.Fields("email").Value = CC.Checkstr(email)
            rs.Fields("company").Value = CC.Checkstr(company)
            rs.Fields("address").Value = CC.Checkstr(address)
            rs.Fields("khbz").Value = CC.Checkstr(resumex)
            rs.Fields("ywbz").Value = ""
            rs.Fields("ywy").Value = CC.Checkstr(ywy)
            rs.Fields("bj").Value = ""
            rs.Fields("bjsj").Value = Now
            rs.Fields("zycd").Value = ""
            rs.Fields("hyqk").Value = ""
            rs.Fields("cgjl").Value = ""
            rs.Fields("gxsj").Value = Now
            rs.Fields("isdel").Value = "0"
            rs.Fields("status").Value = "0"
            rs.Update()
            rs.Close()
        Catch ex As Exception
            jg = ex.Message
        End Try
        conn.Close()
        Return jg
    End Function

    Function Checkstr(ByVal Str As String) As String
        Dim sql_injdata As String
        Dim SQL_inj() As String
        Dim i As Integer
        sql_injdata = "'|and|exec|insert|select|delete|update|count|*|%|chr|mid|master|truncate|char|declare|>|<|script|object|applet|/|\|#|"
        If Str = "" Then
            Checkstr = ""
            Exit Function
        End If
        SQL_inj = Split(sql_injdata, "|")
        For i = 0 To UBound(SQL_inj)
            Str = Replace(Str, SQL_inj(i), "_", 1, -1, 1)
        Next
        Checkstr = Replace(Str, " ", "_", 1, -1, 1)
    End Function
    Public Sub SetResponseHeader(ByVal sContentType As String, ByVal sFileName As String, ByVal bSaveFile As Boolean)
        'Dim sContentDisposition As String = ""
        'With Response
        'If bSaveFile Then
        'sContentDisposition = "attachment; "
        'End If
        'If Len(sFileName) > 0 Then
        'sContentDisposition = sContentDisposition & "filename=" & sFileName
        'End If
        'If Len(sContentDisposition) > 0 Then
        '.AddHeader("Content-disposition", sContentDisposition)
        'End If
        '.ContentType = sContentType
        'End With
    End Sub
    Function ct(ByVal extension)
        Dim contentType As String
        Select Case UCase(extension)
            Case "*"
                contentType = "application/octet-stream"
            Case ("323")
                contentType = "text/h323"
            Case ("ACX")
                contentType = "application/internet-property-stream"
            Case ("AI")
                contentType = "application/postscript"
            Case ("AIF")
                contentType = "audio/x-aiff"
            Case ("AIFC")
                contentType = "audio/x-aiff"
            Case ("AIFF")
                contentType = "audio/x-aiff"
            Case ("ASF")
                contentType = "video/x-ms-asf"
            Case ("SR")
                contentType = "video/x-ms-asf"
            Case ("SX")
                contentType = "video/x-ms-asf"
            Case ("AU")
                contentType = "audio/basic"
            Case ("AVI")
                contentType = "video/x-msvideo"
            Case ("AXS")
                contentType = "application/olescript"
            Case ("BAS")
                contentType = "text/plain"
            Case ("BCPIO")
                contentType = "application/x-bcpio"
            Case ("BIN")
                contentType = "application/octet-stream"
            Case ("BMP")
                contentType = "image/bmp"
            Case ("C")
                contentType = "text/plain"
            Case ("CAT")
                contentType = "application/vnd.ms-pkiseccat"
            Case ("CDF")
                contentType = "application/x-cdf"
            Case ("CER")
                contentType = "application/x-x509-ca-cert"
            Case ("CLASS")
                contentType = "application/octet-stream"
            Case ("CLP")
                contentType = "application/x-msclip"
            Case ("CMX")
                contentType = "image/x-cmx"
            Case ("COD")
                contentType = "image/cis-cod"
            Case ("CPIO")
                contentType = "application/x-cpio"
            Case ("CRD")
                contentType = "application/x-mscardfile"
            Case ("CRL")
                contentType = "application/pkix-crl"
            Case ("CRT")
                contentType = "application/x-x509-ca-cert"
            Case ("CSH")
                contentType = "application/x-csh"
            Case ("CSS")
                contentType = "text/css"
            Case ("DCR")
                contentType = "application/x-director"
            Case ("DER")
                contentType = "application/x-x509-ca-cert"
            Case ("DIR")
                contentType = "application/x-director"
            Case ("DLL")
                contentType = "application/x-msdownload"
            Case ("DMS")
                contentType = "application/octet-stream"
            Case ("DOC")
                contentType = "application/msword"
            Case ("DOT")
                contentType = "application/msword"
            Case ("DVI")
                contentType = "application/x-dvi"
            Case ("DXR")
                contentType = "application/x-director"
            Case ("EPS")
                contentType = "application/postscript"
            Case ("ETX")
                contentType = "text/x-setext"
            Case ("EVY")
                contentType = "application/envoy"
            Case ("EXE")
                contentType = "application/octet-stream"
            Case ("FIF")
                contentType = "application/fractals"
            Case ("FLR")
                contentType = "x-world/x-vrml"
            Case ("GIF")
                contentType = "image/gif"
            Case ("GTAR")
                contentType = "application/x-gtar"
            Case ("GZ")
                contentType = "application/x-gzip"
            Case ("H")
                contentType = "text/plain"
            Case ("HDF")
                contentType = "application/x-hdf"
            Case ("HLP")
                contentType = "application/winhlp"
            Case ("HQX")
                contentType = "application/mac-binhex40"
            Case ("HTA")
                contentType = "application/hta"
            Case ("HTC")
                contentType = "text/x-component"
            Case ("HTM")
                contentType = "text/html"
            Case ("HTML")
                contentType = "text/html"
            Case ("HTT")
                contentType = "text/webviewhtml"
            Case ("ICO")
                contentType = "image/x-icon"
            Case ("IEF")
                contentType = "image/ief"
            Case ("III")
                contentType = "application/x-iphone"
            Case ("INS")
                contentType = "application/x-internet-signup"
            Case ("ISP")
                contentType = "application/x-internet-signup"
            Case ("JFIF")
                contentType = "image/pipeg"
            Case ("JPE")
                contentType = "image/jpeg"
            Case ("JPEG")
                contentType = "image/jpeg"
            Case Else
                contentType = "application/octet-stream"
        End Select

        Return contentType
    End Function


End Class
