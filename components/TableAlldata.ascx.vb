
Public Class TableAlldata
    Inherits System.Web.UI.UserControl
    Property DBOrd() As String
        Get
            Return Me.ViewState("DbOrd")
        End Get
        Set(ByVal value As String)
            Me.ViewState("DbOrd") = value
        End Set
    End Property
    Property TableName() As String
        Get
            Return Me.ViewState("TableName")
        End Get
        Set(ByVal value As String)
            Me.ViewState("TableName") = value
        End Set
    End Property
    Property TjExpression() As String
        Get
            Return Me.ViewState("TjExpression")
        End Get
        Set(ByVal value As String)
            Me.ViewState("TjExpression") = value
        End Set
    End Property

    Dim Conn As New ADODB.Connection
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cc.Connecttodb()
        If Conn.State = 0 Then Conn.Open(cc.setConstr(DBOrd))

        Dim jg As String = ""

        '获取网页参数
        Dim gn, xID As String
        gn = Request("gn")
        xID = CInt(Request("id"))
        '删除项目功能 ver1.0
        If Gn = "del" Then
            Conn.Execute("delete from " & TableName & " where id=" & xID)
        End If

        '提交信息写入数据库
        If IsPostBack = True Then
            Dim rs As New ADODB.Recordset
            rs.Open("select  * from " & TableName, Conn, 1, 3)
            While Not rs.EOF
                rs.Fields("itemtext").Value = Request("f" & rs.Fields("itemno").Value)
                rs.Update()
                rs.MoveNext()
            End While
            rs.Close()
        End If


        jg &= "<form>"
        jg &= MakeTablePage(DBOrd, TableName)
        jg &= "</form>"
        Label1.Text = jg

    End Sub
    Function MakeTablePage(ByVal DBOrd As String, ByVal TableName As String) As String

        Dim jg As String = ""
        jg = "<table class=""tdcontent"">"
        jg &= "<tr class=""thcontent""><td>项目号</td><td>项目名称</td><td>数值</td></tr>"
        Dim rs As New ADODB.Recordset
        rs.Open("select  * from " & TableName, Conn, 1, 1)
        While Not rs.EOF
            jg &= "<tr>"
            jg &= "<td>" & rs.Fields("itemno").Value & "</td>"
            jg &= "<td>" & rs.Fields("itemname").Value & "</td>"
            jg &= "<td><input type= Text name=f" & rs.Fields("itemno").Value
            jg &= "  size=" & rs.Fields("itemtext").DefinedSize
            jg &= "  value='" & rs.Fields("itemtext").Value & "' />"
            jg &= "<a href='" & HttpContext.Current.Request.Url.AbsolutePath & "?gn=del&id=" & rs.Fields("id").Value & "'>"
            jg &= "<img src = ""../../images/fun/del.png"" alt=""删除项目""></a>"
            jg &= "</td></tr>"
            rs.MoveNext()
        End While
        rs.Close()
        jg &= "<tr><td colspan=3>"
        jg &= "<input type=submit class=""btnlogin"" name=denglu value=保存>"
        jg &= "&nbsp<a class=""btnlogin"" href=""#"" onclick=""" & cc.ShowDialog(1, "../Additems.aspx?dbord=" & DBOrd & "&dbname=" & TableName， "添加项目", 500, 400) & """> + 增加一项&nbsp;&nbsp;</a>"
        jg &= "</td></tr>"
        jg &= "</table>"

        '设置弹出窗口
        jg &= TLS.setdialog

        Return jg
    End Function

End Class