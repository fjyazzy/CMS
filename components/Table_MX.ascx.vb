'这个控件包含数据库列表， 删除， 编辑， 添加等功能
Public Class TableListMX
    Inherits System.Web.UI.UserControl
    ''' <summary>从页面取数据库序号
    ''' </summary>
    ''' <returns></returns>
    Property DBOrd() As String
        Get
            Return Me.ViewState("DbOrd")
        End Get
        Set(ByVal value As String)
            Me.ViewState("DbOrd") = value
        End Set
    End Property
    ''' <summary>
    ''' 从页面取表名
    ''' </summary>
    ''' <returns></returns>
    Property DBName() As String
        Get
            Return Me.ViewState("DbName")
        End Get
        Set(ByVal value As String)
            Me.ViewState("DbName") = value
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

    Public Conn As New ADODB.Connection
    Public FieldName(100), FieldLen(100), FieldType(100), TjExpression2 As String
    Public FieldNum As Integer
    Public FileName As String
    ''' <summary>
    ''' 加载页面需要的基础数据
    ''' 搭建页面的框架
    ''' 实现三个功能
    ''' 1.数据列表
    ''' 2.修改一行数据
    ''' 3.删除一行数据
    ''' 4.添加一行数据
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cc.Connecttodb()
        If Conn.State = 0 Then Conn.Open(cc.setConstr(DBOrd))

        FileName = HttpContext.Current.Request.Url.AbsolutePath
        cc.getSoftinfo(DBOrd)

        '从TBDICT表中获取表结构说明

        Dim TableMode As String = ""
        Dim fileName2 As String = ""
        Dim isPIC As String = ""
        Dim DWidth As String = ""
        Dim DHeight As String = ""
        Dim Ordkey As String = ""

        Dim rs As New ADODB.Recordset
        Dim Conn2 As New ADODB.Connection
        '如果当前库不是主数据库，
        '新建主数据库连接conn2
        '在主数据库中取表结构信息
        If DBOrd <> 1 Then
            If Conn2.State = 0 Then Conn2.Open(cc.setConstr(1))
            rs.Open("select  * from TBDict where TBName='" & DBName & "'", Conn2, 1, 3)
        Else
            rs.Open("select  * from TBDict where TBName='" & DBName & "'", Conn, 1, 3)
        End If
        If Not rs.EOF Then
            TableMode = rs.Fields("TableMode").Value
            isPIC = rs.Fields("isPIC").Value
            fileName2 = rs.Fields("DfileName").Value
            DWidth = rs.Fields("DWidth").Value
            DHeight = rs.Fields("DHeight").Value
            TjExpression2 = rs.Fields("TjExpress").Value
            If rs.Fields("orderkey").Value = "-" Then
                Ordkey = " id "
            Else
                Ordkey = rs.Fields("orderkey").Value
            End If
        End If
        rs.Close()
        If DBOrd <> 1 Then
            Conn2.Close()
        End If

        '通过TableMode解析字段定制界面
        Dim i As Integer
        Dim f, g As Object
        f = Split(TableMode, "|")
        FieldNum = UBound(f)
        For i = 0 To FieldNum
            g = Split(f(i), ":")
            FieldName(i) = g(0)
            FieldLen(i) = g(1)
            FieldType(i) = g(2)
        Next

        '获取网页参数
        ' Cid用于传递父目录ID
        Dim gn, xID, pgn, pID, Cid, lx, px, jg, Skey As String
        gn = Request("gn")
        pgn = Request("pgn")
        xID = CInt(Request("id"))
        pID = CInt(Request("pid"))
        Cid = CInt(Request("Cid"))
        Skey = Request("Skey")
        If Skey = "" Then
            Skey = ""
        End If

        '根据表格条件和检索条件构造 正式条件表达式
        If TjExpression = "" Then
            TjExpression = " 1=1 "
        End If
        If TjExpression2 <> "-" And Skey <> "" Then
            TjExpression2 = Replace(TjExpression2, "<<SKEY>>", Skey)
        Else
            TjExpression2 = " 1=1 "
        End If
        TjExpression = " where  (" & TjExpression2 & ") and (" & TjExpression & ")"

        'Response.Write(TjExpression)
        'Response.Write(DBName & ":" & TableMode)
        'Response.End()


        lx = Request("lx")
        px = Request("px")
        If px = "" Then
            px = "1"
        End If

        If gn = "Excel" Then
            TLS.MExcel(Conn, DBName, FieldNum, FieldName, TjExpression, Skey)
            Exit Sub
        End If

        '公共界面
        If Not IsPostBack Then
            '删除主数据库功能 ver1.0
            If gn = "del" Then
                Conn.Execute("delete from " & DBName & " where id=" & xID)
            End If

            jg = TLS.ShowPage(Conn, DBOrd, DBName, TableMode, FieldNum, FieldName, FileName, fileName2, TjExpression, isPIC, lx, px, DWidth, DHeight, Cid, Skey, Ordkey)
            '设置弹出窗口
            jg &= TLS.setdialog

            Label1.Text = jg

        End If
    End Sub



End Class