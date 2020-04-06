Public Class products
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '在此处放置初始化页的用户代码
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        CC.Connecttodb()
        If conn.State = 0 Then conn.Open(CC.setConstr(DBord_ecms))

        Dim rs2 As New ADODB.Recordset
        Dim sql, jg, q, PID, jgx1, jgx2, jgx3 As String
        Dim cid As Integer

        cid = CLng(CC.Checkstr(Request("id")))
        q = CC.Checkstr(Request("q"))
        PID = CC.Checkstr(Request("p"))

        sql = "SELECT  * from ecms_products where isdel=0 and id=" & cid
        jg = ""
        rs.Open(sql, conn, 1, 1, 1)

        If Not rs.EOF Then
            Dim P_title, P_Meta_C, P_Meta_Key

            P_title = WCS.GetTitle("Title", "Product")
            P_title = Replace(P_title, "<公司名>", GSMC)
            P_title = Replace(P_title, "<网站名>", WZMC)
            If System.Convert.IsDBNull(rs.Fields("price").Value) = True Then
                P_title = Replace(P_title, "<产品价格>", "电询或面议")
            Else
                P_title = Replace(P_title, "<产品价格>", rs.Fields("price").Value)
            End If
            P_title = Replace(P_title, "<产品描述>", rs.Fields("shortdesc").Value)
            P_title = Replace(P_title, "<厂商>", rs.Fields("manufactory").Value)
            P_title = Replace(P_title, "<产品型号>", rs.Fields("type").Value)

            P_Meta_C = WCS.GetTitle("MetaContent", "Product")
            P_Meta_C = Replace(P_Meta_C, "<公司名>", GSMC)
            P_Meta_C = Replace(P_Meta_C, "<网站名>", WZMC)
            If System.Convert.IsDBNull(rs.Fields("price").Value) = True Then
                P_Meta_C = Replace(P_Meta_C, "<产品价格>", "电询或面议")
            Else
                P_Meta_C = Replace(P_Meta_C, "<产品价格>", rs.Fields("price").Value)
            End If

            P_Meta_C = Replace(P_Meta_C, "<产品描述>", rs.Fields("shortdesc").Value)
            P_Meta_C = Replace(P_Meta_C, "<厂商>", rs.Fields("manufactory").Value)
            P_Meta_C = Replace(P_Meta_C, "<产品型号>", rs.Fields("type").Value)


            P_Meta_Key = WCS.GetTitle("MetaKeywords", "Product")
            P_Meta_Key = Replace(P_Meta_Key, "<公司名>", GSMC)
            P_Meta_Key = Replace(P_Meta_Key, "<网站名>", WZMC)
            If System.Convert.IsDBNull(rs.Fields("price").Value) = True Then
                P_Meta_Key = Replace(P_Meta_Key, "<产品价格>", "电询或面议")
            Else
                P_Meta_Key = Replace(P_Meta_Key, "<产品价格>", rs.Fields("price").Value)
            End If

            P_Meta_Key = Replace(P_Meta_Key, "<产品描述>", rs.Fields("shortdesc").Value)
            P_Meta_Key = Replace(P_Meta_Key, "<厂商>", rs.Fields("manufactory").Value)
            P_Meta_Key = Replace(P_Meta_Key, "<产品型号>", rs.Fields("type").Value)


            PageTitle.InnerText = P_title
            Content1.Attributes.Add("name", "description")
            Content1.Attributes.Add("content", P_Meta_C)
            Content2.Attributes.Add("name", "keywords")
            Content2.Attributes.Add("content", P_Meta_Key)

            '生成产品基本信息 -- jgx1
            jgx1 = ""
            jgx1 &= "<h3>" & rs.Fields("type").Value & "&nbsp&nbsp<a href=Buy.aspx?p=" & rs.Fields("type").Value & ">购买本产品>></a></h3>"
            sql = "SELECT  * from category_products where id=" & rs.Fields("id").Value
            rs2.Open(sql, conn, 1, 1, 1)
            If Not rs2.EOF Then
                jgx1 &= "<h4>所在目录:<a href=catalogs.aspx?id=" & rs2.Fields("catalogid").Value & ">" & rs2.Fields("catalogname").Value & "</a></h4>"
            End If
            rs2.Close()
            jgx1 &= "<table border=0 width=100% cellpadding=3 cellspacing=1 bgcolor=#999999>"
            jgx1 &= "<tr><td bgcolor=#ffffff>厂商</td><td bgcolor=#ffffff>" & rs.Fields("manufactory").Value & "</td></tr>"
            jgx1 &= "<tr><td bgcolor=#ffffff>封装</td><td bgcolor=#ffffff>" & rs.Fields("fz").Value & "</td></tr>"
            jgx1 &= "<tr><td bgcolor=#ffffff>批号</td><td bgcolor=#ffffff>" & rs.Fields("ph").Value & "</td></tr>"
            jgx1 &= "<tr><td bgcolor=#ffffff>价格</td><td bgcolor=#ffffff>" & "电询</td></tr>"
            jgx1 &= "<tr><td bgcolor=#ffffff>起售数量</td><td bgcolor=#ffffff>" & rs.Fields("qssl").Value & "</td></tr>"
            jgx1 &= "<tr><td bgcolor=#ffffff>描述</td><td bgcolor=#ffffff>" & rs.Fields("shortdesc").Value & "</td></tr>"
            jgx1 &= "<tr><td colspan=2 bgcolor=#ffffff>"

            jgx1 &= "查看<a target=_blank href=manual.aspx?skey=" & rs.Fields("type").Value & ">" & rs.Fields("type").Value & "资料, 相关资料</a>"

            jgx1 &= "</td></tr>"
            jgx1 &= "<tr><td colspan=2 bgcolor=#abcdef>"
            jgx1 &= "<a href=Buy.aspx?p=" & rs.Fields("type").Value & "><font color=red size=4>购买" & rs.Fields("type").Value & "</a>"
            jgx1 &= "</td></tr>"
            jgx1 &= "</table>"

            '生成产品图片信息 -- jgx2 
            jgx2 = ""
            If Len(rs.Fields("productimg").Value) > 5 Then
                jgx2 &= "<a href=showpic.aspx?i=" & CC.Checkstr(rs.Fields("productimg").Value) & " target=_blank><img src='photos/" & CC.Checkstr(rs.Fields("productimg").Value) & "' alt=" & rs.Fields("type").Value & " border=0 width=300></a>"
            Else
                jgx2 &= "<img src='images/Come_on.gif' border=0 width=300 alt=" & rs.Fields("type").Value & " >"
            End If

            '生成产品说明信息  -- jgx3
            jgx3 = ""
            If System.Convert.IsDBNull(rs.Fields("productdesc").Value) = True Then
                jgx3 &= "<b>产品说明</b><br>请来电咨询!"
            Else
                jgx3 &= "<b>产品说明</b><br>" & Replace(Replace(rs.Fields("productdesc").Value, vbCrLf, "<br>"), " ", "&nbsp") & "<br>"
            End If


            '产品页面最终排版
            jg = "<table width=998 border=0 style='font-size:12pt;'>"
            jg &= "<tr>"
            jg &= "<td width=280 valign=top>"
            jg &= WCS.lxfs()
            jg &= "<h3>最新更新</h3>"
            jg &= WCS.SearchP(9, 0, 0, "Products.aspx")
            jg &= "</td>"
            jg &= "<td valign=top>"
            jg &= "<table border=0>"
            jg &= "<tr>"
            jg &= "<td width=345 valign=top>" & jgx2 & "</td>"
            jg &= "<td>" & jgx1 & "</td>"
            jg &= "</tr>"
            jg &= "<tr>"
            jg &= "<td colspan=2 style=""word-break:break-all;"">" & jgx3 & "</td>"
            jg &= "</tr>"
            jg &= "</table>"
            jg &= "<p><h3>类似" & rs.Fields("type").Value & "产品</h3>"
            jg &= WCS.SearchP(1, Left(rs.Fields("type").Value, 2), 1, "Catalogs.aspx")
            jg &= "</p>"
            jg &= "</td>"
            jg &= "</tr>"
            jg &= "</table>"

        Else
            jg &= "<font color=red>错误ID号!"
        End If
        rs.Close()

        conn.Execute("update ecms_products set visittime='" & Now & "' where id=" & cid)
        conn.Close()
        Label1.Text = jg


    End Sub

End Class