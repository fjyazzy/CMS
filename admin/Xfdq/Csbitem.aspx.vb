Public Class Csbitem
    Inherits System.Web.UI.Page

    Public Conn As New ADODB.Connection
    Public FieldName(100), FieldLen(100), FieldType(100) As String
    Public FieldNum, xID As Integer
    Public FileName As String
    Public Dbname, isPIC, TableMode As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cc.Connecttodb()
        Dim DBOrd As Integer
        DBOrd = Request("DBOrd")
        If Conn.State = 0 Then Conn.Open(cc.setConstr(DBOrd))

        '获取网页参数
        Dim pgn, pid, Cid As String
        pgn = Request("pgn")
        pid = Request("pid")

        Dbname = Request("Dbname")
        TableMode = Request("TableMode")
        xID = CInt(Request("id"))
        Cid = CInt(Request("Cid"))

        isPIC = Request("isPIC")

        '定制界面
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

        '初始化表单
        If Not IsPostBack Then
            '获取单号
            getdH(Cid)

            lxList()
            fillForm(xID)
            setdDproduct(DDlx.SelectedValue)

        End If

        '删除数据库-附加图片功能 ver1.0
        If pgn = "del" Then
            Conn.Execute("delete from " & Dbname & "_IMAGES where id=" & pid)
        End If

    End Sub
    ''' <summary>
    ''' 获取单号
    ''' </summary>
    ''' <param name="dh"></param>
    Private Sub getdH(ByVal dh As String)
        Dim rs As New ADODB.Recordset
        rs.Open("select * from csbjb where id=" & dh, Conn, 1, 1)
        If rs.EOF Then
            Txtdh.Text = "单号不存在！"
            Button1.Enabled = False
            Button1.Text = dh & ":单号不存在！"
        Else
            Txtdh.Text = rs.Fields("csbjbh").Value
        End If
        rs.Close()
    End Sub
    Protected Sub LBcc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LBcc.SelectedIndexChanged
        Dim strProduct As String
        strProduct = LBcc.SelectedItem.Text
        Dim f() As String
        f = Split(strProduct, "|")
        '跟分类有关的内容
        '需要考虑分类类型的调整
        Select Case DDlx.SelectedValue
            Case "001"
                Txtrymc.Text = f(1)
                Txtsbmc.Text = "-"
                Txtpjmc.Text = "-"
                Txtqt.Text = "-"
            Case "002"
                Txtrymc.Text = "-"
                Txtsbmc.Text = f(1)
                Txtpjmc.Text = "-"
                Txtqt.Text = "-"
            Case "003"
                Txtrymc.Text = "-"
                Txtsbmc.Text = "-"
                Txtpjmc.Text = f(1)
                Txtqt.Text = "-"
            Case Else
                Txtrymc.Text = "-"
                Txtsbmc.Text = "-"
                Txtpjmc.Text = "-"
                Txtqt.Text = f(1)
        End Select
        Txtdw.Text = f(3)
        Txtdj.Text = f(4)
        jsje()

    End Sub

    Protected Sub TxtSl_TextChanged(sender As Object, e As EventArgs) Handles TxtSl.TextChanged
        jsje()
    End Sub
    Protected Sub Txtdj_TextChanged(sender As Object, e As EventArgs) Handles Txtdj.TextChanged
        jsje()
    End Sub
    Private Sub jsje()
        TxtJe.Text = Format(CSng(TxtSl.Text) * CSng(Txtdj.Text))
    End Sub

    Protected Sub skey_TextChanged(sender As Object, e As EventArgs) Handles skey.TextChanged
        setdDproduct(DDlx.SelectedValue)
    End Sub

    Protected Sub fillForm(ByVal id As String)
        If CInt(xID) > 0 Then
            Dim rs As New ADODB.Recordset
            rs.Open("select * from " & Dbname & " where ID=" & xID, Conn, 1, 1)
            If Not rs.EOF Then
                Txtdh.Text = rs.Fields(1).Value
                DDlx.SelectedValue = rs.Fields(2).Value
                Txtrymc.Text = rs.Fields(3).Value
                Txtsbmc.Text = rs.Fields(4).Value
                Txtpjmc.Text = rs.Fields(5).Value
                Txtqt.Text = rs.Fields(6).Value
                Txtdw.Text = rs.Fields(7).Value
                TxtSl.Text = rs.Fields(8).Value
                Txtdj.Text = rs.Fields(9).Value
                TxtJe.Text = rs.Fields(10).Value
                Txtbz.Text = rs.Fields(11).Value

                setdDproduct(DDlx.SelectedValue)

            End If
            rs.Close()

        Else

            '添加必须赋值的字段在这里赋值
            TxtSl.Text = "1"
        End If

    End Sub


    Private Sub lxList()
        DDlx.Items.Clear()
        Dim rs As New ADODB.Recordset
        rs.Open("select * from Category order by itemno", Conn, 1, 3)
        While Not rs.EOF
            Dim ll As New ListItem
            ll.Text = rs.Fields("itemtext").Value
            ll.Value = rs.Fields("itemname").Value
            DDlx.Items.Add(ll)
            rs.MoveNext()
        End While
        rs.Close()
    End Sub
    ''' <summary>
    ''' 保存按钮的功能
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim rs As New ADODB.Recordset

        rs.Open("select * from " & Dbname & " where id=" & xID, Conn, 1, 3)
        If xID = "0" Then
            rs.AddNew()
        End If

        rs.Fields(1).Value = Txtdh.Text
        rs.Fields(2).Value = DDlx.SelectedValue
        rs.Fields(3).Value = Txtrymc.Text
        rs.Fields(4).Value = Txtsbmc.Text
        rs.Fields(5).Value = Txtpjmc.Text
        rs.Fields(6).Value = Txtqt.Text
        rs.Fields(7).Value = Txtdw.Text
        rs.Fields(8).Value = TxtSl.Text
        rs.Fields(9).Value = Txtdj.Text
        rs.Fields(10).Value = TxtJe.Text
        rs.Fields(11).Value = Txtbz.Text

        rs.Update()
        rs.Close()

        '计算总价
        Dim zj As Single = 0.0
        rs.Open("select sum(je) from csbjb2 where csbjbh='" & Txtdh.Text & "'", Conn, 1, 3)
        zj = rs.Fields(0).Value
        rs.Close()
        Conn.Execute("update csbjb set total =" & zj & " where csbjbh='" & Txtdh.Text & "'")


        Conn.Close()
        Response.Write(cc.ClosePage)

    End Sub

    Protected Sub DDlx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDlx.SelectedIndexChanged
        skey.Text = ""
        setdDproduct(DDlx.SelectedValue)
    End Sub


    ''' <summary>
    ''' 设置可选择的内容
    ''' </summary>
    ''' <param name="lxbh"></param>
    Private Sub setdDproduct(ByVal lxbh As String)
        Dim rs As New ADODB.Recordset
        Dim sql As String
        LBcc.Items.Clear()
        If skey.Text <> "" Then
            sql = "select * from Product where lbbh='" & lxbh & "' and ( 品名 like '%" & skey.Text & "%' or 品牌 like '%" & skey.Text & "%' or 描述  like '%" & skey.Text & "%' or 单位 like '%" & skey.Text & "%' )"
        Else
            sql = "select * from Product where lbbh='" & lxbh & "'"
        End If
        rs.Open(sql, Conn, 1, 1)
        While Not rs.EOF
            Dim ll As New ListItem
            Dim xmnr As String = ""
            xmnr &= rs.Fields("品牌").Value
            xmnr &= "|" & rs.Fields("品名").Value
            xmnr &= "|" & rs.Fields("描述").Value
            xmnr &= "|" & rs.Fields("单位").Value
            xmnr &= "|" & rs.Fields("含税价").Value
            ll.Text = xmnr
            ll.Value = rs.Fields("产品号").Value
            LBcc.Items.Add(ll)
            rs.MoveNext()
        End While
        rs.Close()

    End Sub

End Class