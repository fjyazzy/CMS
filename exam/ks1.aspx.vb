Public Class ks1
    Inherits System.Web.UI.Page
    Dim Conn As New ADODB.Connection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cc.Connecttodb()
        If Conn.State = 0 Then Conn.Open(cc.setConstr(3))
        cc.getSoftinfo("1")

        If Not IsPostBack Then
            getksxm()
            getbjm()
            setZH()
        End If

        Button1.Visible = False
        Button1.Enabled = False
        If DateDiff("n", strKSKS, Now) > 0 Then
            Button1.Visible = true
            Button1.Enabled = true
            Button2.Visible = False
            Button2.Enabled = False
        End If
        If DateDiff("n", strJSKS, Now) > 0 Then
            Button1.Visible = False
            Button1.Enabled = False
            Button2.Visible = true
            Button2.Enabled = true
        End If

    End Sub

    ' TABLE-> exam :保存考试名称，考试号，考试时间，考试总分
    ' TABLE-> exam_KS :保存参加考试的考生的基本信息，含考试号，考生号，考生姓名，
    '                  班级号,座号，开始开始时间和结束时间,试卷号，得分
    ' TABLE-> exam_SJ:保存每个学生的试卷信息，一个学生一份试卷一个试卷号
    '                 含试卷号，题型，题号，学生回答，得分
    '                 本表在学生选择开始考试后，抽题产生。
    ' TABLE-> XZ:   选择题库
    ' TABLE-> WD:   问答题库

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim rs As New ADODB.Recordset
        Dim sql, sjh As String
        sjh = DDKSxm.SelectedValue & DDBJM.SelectedValue & DDzh.SelectedValue
        If Button1.Text = "开始考试" Then
            Response.Redirect("ks2.aspx?sjh=" & sjh)
        End If
        If Button1.Text = "开始抽题" Then
            sql = "select * from exam_sj where sjh='" & sjh & "' "
            rs.Open(sql, Conn, 1, 3)
            If rs.EOF Then
                selectItem(DDKSxm.SelectedValue, sjh)
            End If
            rs.Close()
            Button1.Text = "开始考试"
        End If
        If Button1.Text = "注册考试" Then
            If Len(trim(xm.Text)) < 1 Then
                Label1.Text = "<font color=red>考生姓名必须输入，否则无法进行考试！</font>"
                Exit Sub
            End If
            sql = "select * from exam_ks where sjh='" & sjh & "' "
            rs.Open(sql, Conn, 1, 3)
            If rs.EOF Then
                '根据考生号生成exam_KS信息并开始抽题产生exam_SJ试卷信息
                rs.AddNew()
                rs.Fields("examno").Value = DDKSxm.SelectedValue
                rs.Fields("bjm").Value = DDBJM.SelectedItem.Text
                rs.Fields("xm").Value = xm.Text
                rs.Fields("zh").Value = DDzh.SelectedValue
                rs.Fields("remoteip").Value = Request.ServerVariables("REMOTE_ADDR")
                rs.Fields("kstime").Value = Now
                rs.Fields("sjh").Value = sjh
                rs.Update()
                rs.Close()
                Button1.Text = "开始抽题"
            Else
                rs.Close()
                Button1.Text = "开始考试"
            End If
        End If

    End Sub

    ''' <summary>
    ''' 抽题函数
    ''' </summary>
    ''' <param name="examno"></param>
    ''' <param name="sjh"></param>
    ''' <returns></returns>
    Private Function selectItem(ByVal examno As String, ByVal sjh As String) As Integer
        Dim sql, stk As String
        Dim rs As New ADODB.Recordset
        Dim jg As Integer = 0
        Dim xzzfs, tkzfs, wdzfs As Integer
        xzzfs = tkzfs = wdzfs = 0
        stk = ""
        '访问exam取得试卷结构
        sql = "select * from exam where examno='" & examno & "' "
        rs.Open(sql, Conn, 1, 1)
        If Not rs.EOF Then
            stk = rs.Fields("examtk").Value
            xzzfs = rs.Fields("xz").Value
            tkzfs = rs.Fields("tk").Value
            wdzfs = rs.Fields("wd").Value
        End If
        rs.Close()

        '从试题库中，按试卷结构（题型，分数）组卷
        Label2.Text = "开始组卷<br>"
        TXct(sjh, "exam_" & stk & "_xz", "XZ", xzzfs)
        TXct(sjh, "exam_" & stk & "_tk", "TK", tkzfs)
        TXct(sjh, "exam_" & stk & "_wd", "WD", wdzfs)

        '保存题目到exam_sj
        Return jg
    End Function
    ''' <summary>
    ''' 根据题型和分数，调用CTX抽出符合试卷要求数量的题号
    ''' 调用insert1item保存到数据库中
    ''' </summary>
    ''' <param name="sjh"></param>
    ''' <param name="stk">试题库</param>
    ''' <param name="tx"></param>
    ''' <param name="txzfs"></param>
    Private Sub TXct(ByVal sjh As String, ByVal stk As String, ByVal tx As String, ByVal txzfs As Integer)
        Dim ct(2, 50) As Integer
        Dim i, th, fs As Integer

        For i = 0 To 50
            ct(0, i) = 0
            ct(1, i) = 0
        Next
        '开始抽题
        If txzfs > 0 Then
            ct = ctx(stk, txzfs)
            For i = 0 To 50
                If (ct(0, i) = 0) Then Exit For
                th = ct(0, i)
                fs = ct(1, i)
                insert1item(sjh, stk, tx, th, fs)
            Next
        End If

    End Sub
    ''' <summary>
    ''' 抽出符合试卷要求数量的题号
    ''' </summary>
    ''' <param name="stk">题型题库</param>
    ''' <param name="zf">题型总分数</param>
    ''' <returns>
    ''' 返回题号和分数
    ''' </returns>
    Function ctx(ByVal stk As String， ByVal zf As Integer) As Integer(,)
        Dim ctx1(2, 50), ctx2(2, 50), zts, k, l, n, ret As Integer

        '从数据库中取出到矩阵中
        Dim sql As String
        Dim rs As New ADODB.Recordset
        zts = 0
        sql = "select id,fs from " & stk
        rs.Open(sql, Conn, 1, 1)
        While Not rs.EOF()
            ctx1(0, zts) = rs.Fields("id").Value
            ctx1(1, zts) = rs.Fields("fs").Value
            zts = zts + 1
            rs.MoveNext()
        End While
        rs.Close()
        zts = zts - 1

        '从矩阵中取出满足数量的题目
        k = 0
        Label2.Text = Label2.Text & "<br>题库：" & stk & "总分:" & zf & "|总提数:" & zts & "|选中题：<br>"
        Randomize()
        Do While zf > 0

            '检查有没有重复
            Label2.Text = Label2.Text & "第" & k + 1 & "轮:"
            ret = 0
            While ret = 0
                ret = 1
                l = Int(zts * Rnd())
                For n = 0 To k - 1
                    If ctx2(0, n) = ctx1(0, l) Then
                        ret = 0
                        Exit For
                    End If
                    Label2.Text = Label2.Text & ".."
                Next
            End While

            ctx2(0, k) = ctx1(0, l)
            ctx2(1, k) = ctx1(1, l)
            Label2.Text = Label2.Text & "选中:id" & l & "-" & ctx2(0, k) & "<br>"
            zf = zf - ctx2(1, k)
            k = k + 1
        Loop

        Return ctx2
    End Function


    Private Sub insert1item(ByVal sjh As String, ByVal stk As String, ByVal tx As String, ByVal th As String, ByVal fs As String)
        Dim sql As String
        Dim rs As New ADODB.Recordset
        sql = "select * from exam_sj where sjh='" & sjh & "' "
        rs.Open(sql, Conn, 1, 3)
        rs.AddNew()
        rs.Fields("sjh").Value = sjh
        rs.Fields("stk").Value = stk
        rs.Fields("tx").Value = tx
        rs.Fields("th").Value = th
        rs.Fields("fs").Value = fs
        rs.Update()
        rs.Close()
    End Sub

    ''' <summary>
    ''' 制作页面使用，获得考试项目
    ''' </summary>
    Private Sub getksxm()
        Dim rs As New ADODB.Recordset
        Dim sql As String
        DDKSxm.Items.Clear()
        sql = "select * from exam"
        rs.Open(sql, Conn, 1, 1)
        While Not rs.EOF
            Dim ll As New ListItem
            ll.Text = rs.Fields("examname").Value
            ll.Value = rs.Fields("examno").Value
            DDKSxm.Items.Add(ll)
            rs.MoveNext()
        End While
        rs.Close()

    End Sub

    ''' <summary>
    ''' 制作页面使用，班级
    ''' </summary>
    Private Sub getbjm()
        Dim rs As New ADODB.Recordset
        Dim sql As String
        DDBJM.Items.Clear()
        sql = "select * from exam_bj"
        rs.Open(sql, Conn, 1, 1)
        While Not rs.EOF
            Dim ll As New ListItem
            ll.Text = rs.Fields("bjm").Value
            ll.Value = rs.Fields("bjh").Value
            DDBJM.Items.Add(ll)
            rs.MoveNext()
        End While
        rs.Close()

    End Sub
    ''' <summary>
    ''' 制作页面使用，座号
    ''' </summary>
    Private Sub setZH()
        DDzh.Items.Clear()
        Dim i As Integer
        For i = 1 To 50
            DDzh.Items.Add(i.ToString().PadLeft(2, "0"))
        Next
    End Sub




    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim rs As New ADODB.Recordset
        Dim sql, sjh As String
        sjh = DDKSxm.SelectedValue & DDBJM.SelectedValue & DDzh.SelectedValue
        If Button2.Text = "开始考试" Then
            Response.Redirect("ks2.aspx?sjh=" & sjh)
        End If
        If Button2.Text = "开始抽题" Then
            Conn.Execute("delete from exam_sj where sjh='" & sjh & "' ")
            selectItem(DDKSxm.SelectedValue, sjh)
            Button2.Text = "开始考试"
        End If
        If Button2.Text = "模拟考试" Then
            If Len(trim(xm.Text)) < 1 Then
                Label1.Text = "<font color=red>考生姓名必须输入，否则无法进行考试！</font>"
                Exit Sub
            End If
            sql = "select * from exam_ks where sjh='" & sjh & "' "
            rs.Open(sql, Conn, 1, 3)
            If rs.EOF Then
                '根据考生号生成exam_KS信息并开始抽题产生exam_SJ试卷信息
                rs.AddNew()
                rs.Fields("examno").Value = DDKSxm.SelectedValue
                rs.Fields("bjm").Value = DDBJM.SelectedItem.Text
                rs.Fields("xm").Value = xm.Text
                rs.Fields("zh").Value = DDzh.SelectedValue
                rs.Fields("remoteip").Value = Request.ServerVariables("REMOTE_ADDR")
                rs.Fields("kstime").Value = Now
                rs.Fields("sjh").Value = sjh
                rs.Update()
                rs.Close()
            End If
            Button2.Text = "开始抽题"
        End If
    End Sub
End Class