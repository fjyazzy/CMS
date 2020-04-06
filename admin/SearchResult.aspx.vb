Public Class SearchResult
    Inherits System.Web.UI.Page

    Dim Conn As New ADODB.Connection
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '在此处放置初始化页的用户代码
        Dim i As Integer
        Dim Skeyx, Ckx, Cky As String

        If cc.getQx(Request.Cookies("Username").Value, "998") = 0 Then
            Response.Write("没有权限")
            Response.End()
        End If
        If Conn.State = 0 Then Conn.Open(cc.setConstr(DBord_ecms))
        cc.getSoftinfo(dbord_ecms)
        lLink.Attributes.Add("href", "../../cmscss/home/style" & SYSTEMSTYLE & ".css")

        Label1.Text = ""
        Skeyx = cc.Checkstr(Request("skey"))
        Ckx = cc.Checkstr(Request("ck"))
        If Skeyx = "" Or Ckx = "" Then
            Response.End()
        End If


        Dim f
        f = Split(Ckx, "|")
        Cky = ""
        For i = 0 To UBound(f)
            If f(i) = "XM" Then
                Cky &= "XM|"
            End If
            If f(i) = "TYPE" Then
                Cky &= "TYPE|"
            End If
            If f(i) = "DH" Then
                Cky &= "DH|"
            End If
            If f(i) = "BZ" Then
                Cky &= "BZ|"
            End If
            If f(i) = "CS" Then
                Cky &= "CS|"
            End If
            If f(i) = "DZ" Then
                Cky &= "DZ|"
            End If
        Next

        For i = 0 To UBound(f)
            If f(i) = "KC" Then
                Label1.Text &= GetKc(cc.Checkstr(Skeyx), Cky)
            End If
            If f(i) = "ZL" Then
                Label1.Text &= Getzl(cc.Checkstr(Skeyx), Cky)
            End If
            If f(i) = "GYS" Then
                Label1.Text &= GetGys(cc.Checkstr(Skeyx), Cky)
            End If
            If f(i) = "ORDER" Then
                Label1.Text &= GetOrder(cc.Checkstr(Skeyx), Cky)
            End If
            If f(i) = "YWD" Then
                Label1.Text &= Getywd(cc.Checkstr(Skeyx), Cky)
            End If

        Next


    End Sub

    Function GetKc(ByVal skey, ByVal cky)

        Dim rs As New ADODB.Recordset
        Dim jg, jgx As String
        Dim i As Integer

        jg = "========================库存商品查找结果==================" & cky & "==========<br><ul>"

        Dim tjxx As String
        tjxx = " where ( isdel=0 ) and ( 1<>1  "
        Dim f
        f = Split(cky, "|")
        For i = 0 To UBound(f)

            If f(i) = "XM" Then
                tjxx &= " or cgxx like '%" & skey & "%'"
            End If
            If f(i) = "TYPE" Then
                tjxx &= " or type like '%" & skey & "%'"
            End If
            If f(i) = "DH" Then
                tjxx &= " or cgxx like '%" & skey & "%'"
            End If
            If f(i) = "BZ" Then
                tjxx &= " or Ghsid like '%" & skey & "%' or shortdesc like '%" & skey & "%' or cgxx like '%" & skey & "%' or productdesc like '%" & skey & "%' "
            End If
            If f(i) = "CS" Then
                tjxx &= " or manufactory like '%" & skey & "%'"
            End If
            If f(i) = "DZ" Then
                tjxx &= " or Ghsid like '%" & skey & "%' or shortdesc like '%" & skey & "%' or cgxx like '%" & skey & "%' or productdesc like '%" & skey & "%' "
            End If

        Next
        tjxx &= " )"

        rs.Open("SELECT top 20  * from ecms_products " & tjxx & " order by id desc", Conn, 0, 1, 1)
        While Not rs.EOF
            jg &= "<li><A target=_blank href=ecms/add_e_products.aspx?id=" & rs.Fields("id").Value & "&v=0&dbn=products>"
            jgx = ""
            For i = 0 To rs.Fields.Count - 1
                jgx &= "|" & rs.Fields(i).Value
            Next
            jg &= Left(jgx, 130) & "...</a>"
            rs.MoveNext()
        End While
        jg &= "</ul>"
        rs.Close()

        Return jg

    End Function
    Function Getzl(ByVal skey, ByVal cky)

        Dim rs As New ADODB.Recordset
        Dim jg, jgx As String
        Dim i As Integer


        Dim tjxx As String
        tjxx = " where ( 1<>1  "
        Dim f
        f = Split(cky, "|")
        For i = 0 To UBound(f)
            If f(i) = "XM" Then
            End If
            If f(i) = "TYPE" Then
                tjxx &= " or type like '%" & skey & "%'"
            End If
            If f(i) = "DH" Then
            End If
            If f(i) = "BZ" Then
                tjxx &= " or shortdesc like '%" & skey & "%' "
            End If
            If f(i) = "CS" Then
                tjxx &= " or manufactory like '%" & skey & "%'"
            End If
            If f(i) = "DZ" Then
            End If
        Next
        tjxx &= " )"



        jg = "========================资料查找结果===================" & cky & "=========<br><ul>"
        rs.Open("SELECT top 20  * from manual " & tjxx & " order by id desc", Conn, 0, 1, 1)
        While Not rs.EOF
            jg &= "<li><A target=_blank href=Additem_mx.aspx?dbord=6&id=" & rs.Fields("id").Value & "&v=0&DBname=manual>"
            jgx = ""
            For i = 0 To rs.Fields.Count - 1
                jgx &= "|" & rs.Fields(i).Value
            Next
            jg &= Left(jgx, 130) & "...</a>"
            rs.MoveNext()
        End While
        jg &= "</ul>"
        rs.Close()

        Return jg

    End Function
    Function GetGys(ByVal skey, ByVal cky)

        Dim rs As New ADODB.Recordset
        Dim jg, jgx As String
        Dim i As Integer
        jg = "========================供应商资料查找结果================" & cky & "============<br><ul>"


        Dim tjxx As String
        tjxx = " where ( isdel=0 ) and ( 1<>1  "
        Dim f
        f = Split(cky, "|")
        For i = 0 To UBound(f)

            If f(i) = "XM" Then
                tjxx &= " or lxxx like '%" & skey & "%' or company like '%" & skey & "%'"
            End If
            If f(i) = "TYPE" Then
                tjxx &= " or zyyw like '%" & skey & "%'"
            End If
            If f(i) = "DH" Then
                tjxx &= " or lxxx like '%" & skey & "%'"
            End If
            If f(i) = "BZ" Then
                tjxx &= " or zyyw like '%" & skey & "%'"
            End If
            If f(i) = "CS" Then
                tjxx &= " or company like '%" & skey & "%'"
            End If
            If f(i) = "DZ" Then
                tjxx &= " or bz like '%" & skey & "%'"
            End If

        Next
        tjxx &= " )"


        rs.Open("SELECT top 20  * from gys " & tjxx & " order by id desc", Conn, 0, 1, 1)
        While Not rs.EOF
            jg &= "<li><A target=_blank href=Additem_mx.aspx?dbord=6&id=" & rs.Fields("id").Value & "&v=0&dbn=gys>"
            jgx = ""
            For i = 0 To rs.Fields.Count - 1
                jgx &= "|" & rs.Fields(i).Value
            Next
            jg &= Left(jgx, 130) & "...</a>"
            rs.MoveNext()
        End While
        jg &= "</ul>"
        rs.Close()

        Return jg

    End Function
    Function GetOrder(ByVal skey, ByVal cky)

        Dim rs As New ADODB.Recordset
        Dim jg, jgx As String
        Dim i As Integer
        jg = "========================询价单资料查找结果===================" & cky & "=========<br><ul>"


        Dim tjxx As String
        tjxx = " where ( isdel=0 ) And ywy='" & Server.UrlDecode(Request.Cookies("name").Value) & "' and ( 1<>1  "
        Dim f
        f = Split(cky, "|")
        For i = 0 To UBound(f)

            If f(i) = "XM" Then
                tjxx &= " or name like '%" & skey & "%'"
            End If
            If f(i) = "TYPE" Then
                tjxx &= " or type like '%" & skey & "%'"
            End If
            If f(i) = "DH" Then
                tjxx &= " or phone like '%" & skey & "%'"
            End If
            If f(i) = "BZ" Then
                tjxx &= " or khbz like '%" & skey & "%'"
            End If
            If f(i) = "CS" Then
                tjxx &= " or manufactory like '%" & skey & "%' or company like '%" & skey & "%'"
            End If
            If f(i) = "DZ" Then
                tjxx &= " or address like '%" & skey & "%'"
            End If
        Next
        tjxx &= " )"


        rs.Open("SELECT top 20  * from Inquirys " & tjxx & " order by id desc", Conn, 0, 1, 1)
        While Not rs.EOF
            jg &= "<li><A target=_blank href=ecms/add_Inquirys.aspx?id=" & rs.Fields("id").Value & "&v=0&dbn=orders>"
            jgx = ""
            For i = 0 To rs.Fields.Count - 1
                jgx &= "|" & rs.Fields(i).Value
            Next
            jg &= Left(jgx, 130) & "...</a>"
            rs.MoveNext()
        End While
        jg &= "</ul>"
        rs.Close()

        Return jg

    End Function
    Function Getywd(ByVal skey, ByVal cky)

        Dim rs As New ADODB.Recordset
        Dim jg, jgx As String
        Dim i As Integer
        jg = "========================定单资料查找结果==================" & cky & "==========<br><ul>"

        Dim tjxx As String
        tjxx = " where ( isdel=0 ) And tdy='" & Server.UrlDecode(Request.Cookies("name").Value) & "' and ( 1<>1  "
        Dim f
        f = Split(cky, "|")
        For i = 0 To UBound(f)

            If f(i) = "XM" Then
                tjxx &= " or xm like '%" & skey & "%' or company like '%" & skey & "%'"
            End If
            If f(i) = "TYPE" Then
                tjxx &= " or bz like '%" & skey & "%'"
            End If
            If f(i) = "DH" Then
                tjxx &= " or phone like '%" & skey & "%'"
            End If
            If f(i) = "BZ" Then
                tjxx &= " or bz like '%" & skey & "%' or fhfy like '%" & skey & "%' or ydh like '%" & skey & "%'"
            End If
            If f(i) = "CS" Then
                tjxx &= " or company like '%" & skey & "%'"
            End If
            If f(i) = "DZ" Then
                tjxx &= " or address like '%" & skey & "%'"
            End If
        Next
        tjxx &= " )"


        rs.Open("SELECT top 20  * from Orders " & tjxx & " order by id desc", Conn, 0, 1, 1)
        While Not rs.EOF
            jg &= "<li><A target=SboxResult href=ecms/add_orders.aspx?id=" & rs.Fields("id").Value & "&v=0&dbn=ywd>"
            jgx = ""
            For i = 0 To rs.Fields.Count - 1
                jgx &= "|" & rs.Fields(i).Value
            Next
            jg &= Left(jgx, 130) & "...</a>"
            rs.MoveNext()
        End While
        jg &= "</ul>"
        rs.Close()

        Return jg

    End Function



End Class