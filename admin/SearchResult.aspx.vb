Public Class SearchResult
    Inherits System.Web.UI.Page

    Dim Conn As New ADODB.Connection
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '在此处放置初始化页的用户代码
        If CC.getQx(Request.Cookies("Username").Value, "998") = 0 Then
            Response.Write("没有权限")
            Response.End()
        End If
        If Conn.State = 0 Then Conn.Open(CC.setConstr(DBord_ecms))
        CC.getSoftinfo(DBord_ecms)
        lLink.Attributes.Add("href", "../cmscss/home/style" & SYSTEMSTYLE & ".css")


        '设置弹出窗口
        Label1.Text = TLS.setdialog

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Label1.Text &= GetKc()
        Label1.Text &= Getzl()
        Label1.Text &= GetGys()
        Label1.Text &= GetInquirys()
        Label1.Text &= GetOrders()

    End Sub
    Function tbl(bt, DBname, TjExpression, efilename, px, OrderExpression, skey2) As String
        Dim rs As New ADODB.Recordset
        Dim jg, jgx, sql As String
        Dim i, k As Integer
        Dim rn, pn As Long


        '如果当前库不是主数据库，
        '新建主数据库连接conn2
        '在主数据库中取表结构信息
        Dim Conn2 As New ADODB.Connection
        Dim TableMode, isPIC, fileName2, DWidth, DHeight As String
        If Conn2.State = 0 Then Conn2.Open(CC.setConstr(1))
        rs.Open("select  * from TBDict where TBName='" & DBname & "'", Conn2, 1, 3)
        TableMode = ""
        DWidth = "600"
        DHeight = "400"
        If Not rs.EOF Then
            TableMode = rs.Fields("TableMode").Value
            isPIC = rs.Fields("isPIC").Value
            fileName2 = rs.Fields("DfileName").Value
            DWidth = rs.Fields("DWidth").Value
            DHeight = rs.Fields("DHeight").Value
        End If
        rs.Close()
        Conn2.Close()


        jgx = bt
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
        jgx &= "找到相关结果共:" & rn & "个，页码(" & px & "/" & pn & "):"
        '根据当前的页号，计算要显示的序列
        k = px \ 10
        For i = k * 10 + 1 To k * 10 + 10
            If i > pn Then Exit For
            If i = px Then
                jgx &= "<a href=searchresult.aspx?dbname=" & DBname & "&px=" & i & "&skey=" & skey2 & "><font color=#000><b>" & i & "</b></font></a> "
            Else
                jgx &= "<a href=searchresult.aspx?dbname=" & DBname & "&px=" & i & "&skey=" & skey2 & ">" & i & "</a> "
            End If
        Next

        '正式显示数据
        If px = 1 Then
            sql = "select top " & PAGENUMS & " * from " & DBname & TjExpression & OrderExpression
        Else
            sql = "select top " & PAGENUMS & " * from " & DBname & TjExpression
            sql &= " AND ( "
            sql &= " id NOT IN ( SELECT TOP " & PAGENUMS * (px - 1) & " id FROM " & DBname & TjExpression & OrderExpression & ")"
            sql &= ") "
            sql &= OrderExpression
        End If

        rs.Open(sql, Conn, 1, 1)
        jgx &= "<ul>"
        While Not rs.EOF
            jgx &= "<li><A href=# onclick=""" & CC.ShowDialog(1, efilename & "?dbord=" & DBord_ecms & "&dbname=" & DBname & "&TableMode=" & TableMode & "&id=" & rs.Fields("id").Value, "修改结果数据", DWidth, DHeight) & """ >"
            jg = ""
            For i = 0 To rs.Fields.Count - 1
                jg &= "|" & rs.Fields(i).Value
            Next
            jgx &= Left(jg, 130) & "...</a></li>"
            rs.MoveNext()
        End While
        jgx &= "</ul>"
        rs.Close()

        Return jgx

    End Function

    Function GetKc()
        Dim skey2, tjxx, jg, zls As String
        skey2 = skey.Text

        Dim f As Object
        Dim i As Integer
        zls = "cgxx|type|Ghsxx|cgjl|manufactory|fz|shortdesc|Productdesc"
        f = Split(zls, "|")
        tjxx = " where "
        For i = 1 To UBound(f)
            tjxx &= f(i) & " Like '%" & skey2 & "%' "
            If i < UBound(f) Then
                tjxx &= " OR "
            End If
        Next

        jg = tbl("库存商品-", "ecms_products", tjxx, "ecms/add_e_products.aspx", 1, " order by id desc", skey.Text)
        Return jg
    End Function
    Function Getzl()

        Dim skey2, tjxx, jg, zls As String
        skey2 = skey.Text
        Dim f As Object
        Dim i As Integer
        zls = "type|manufactory|furl|shortdesc"
        f = Split(zls, "|")
        tjxx = " where "
        For i = 1 To UBound(f)
            tjxx &= f(i) & " Like '%" & skey2 & "%' "
            If i < UBound(f) Then
                tjxx &= " OR "
            End If
        Next

        tjxx = " where "
        tjxx &= "  type like '%" & skey2 & "%'"
        tjxx &= " or manufactory like '%" & skey2 & "%'"
        tjxx &= " or furl like '%" & skey2 & "%'"
        tjxx &= " Or shortdesc Like '%" & skey2 & "%'"

        jg = tbl("PDF资料库-", "manual", tjxx, "ecms/Add_manual.aspx", 1, " order by id desc", skey.Text)
        Return jg

    End Function
    Function GetGys()

        Dim skey2, tjxx, jg, zls As String
        skey2 = skey.Text
        Dim f As Object
        Dim i As Integer
        zls = "lxxx|company|zyyw|bz"
        f = Split(zls, "|")
        tjxx = " where "
        For i = 1 To UBound(f)
            tjxx &= f(i) & " Like '%" & skey2 & "%' "
            If i < UBound(f) Then
                tjxx &= " OR "
            End If
        Next

        jg = tbl("供应商资料 -", "gys", tjxx, "Additems_mx.aspx", 1, " order by id desc", skey.Text)
        Return jg

    End Function
    Function GetInquirys()
        'add_Inquirys.aspx
        'Inquirys
        Dim skey2, tjxx, jg, zls As String
        skey2 = skey.Text

        Dim f As Object
        Dim i As Integer
        zls = "type|manufactory|name|fz|phone|company|address|khbz"
        f = Split(zls, "|")
        tjxx = " where "
        For i = 1 To UBound(f)
            tjxx &= f(i) & " Like '%" & skey2 & "%' "
            If i < UBound(f) Then
                tjxx &= " OR "
            End If
        Next

        jg = tbl("询价单资料 -", "Inquirys", tjxx, "ecms/Add_inquirys.aspx", 1, " order by id desc", skey.Text)
        Return jg


    End Function
    Function GetOrders()

        Dim skey2, tjxx, jg, zls As String
        skey2 = skey.Text
        Dim f As Object
        Dim i As Integer
        zls = "xm|fhfy|phone|company|address|bz"
        f = Split(zls, "|")
        tjxx = " where "
        For i = 1 To UBound(f)
            tjxx &= f(i) & " Like '%" & skey2 & "%' "
            If i < UBound(f) Then
                tjxx &= " OR "
            End If
        Next


        jg = tbl("定单资料 -", "Orders", tjxx, "ecms/Add_orders.aspx", 1, " order by id desc", skey.Text)
        Return jg

    End Function

End Class