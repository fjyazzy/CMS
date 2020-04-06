Public Class ureg
    Inherits System.Web.UI.Page

    Dim Conn As New ADODB.Connection
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cc.Connecttodb()
        If Conn.State = 0 Then Conn.Open(cc.setConstr(1))
        If Not IsPostBack Then
            Button1.Enabled = False
        End If
    End Sub
    Function Savedata()

        If Len(trim(xsdm.Text)) < 3 Then
            Response.Write(cc.Alert("学号必须填写!"))
            Return False
            Exit Function
        End If
        If Len(trim(mm.Text)) < 1 Or mm.Text <> mm2.Text Then
            Response.Write(cc.Alert("登陆系统的密码必须填写或两次密码必须相同!"))
            Return False
            Exit Function
        End If

        Dim sql As String
        Dim rs As New ADODB.Recordset
        sql = "select  * from xjk where xsdm='" & cc.Checkstr(xsdm.Text) & "'"
        rs.Open(sql, Conn, 1, 3)
        rs.Fields("mm").Value = mm.Text
        rs.Fields("dglxdh").Value = lxdh.Text
        rs.Fields("qq").Value = QQ.Text
        rs.Update()
        rs.Close()

        Response.Write(cc.Alert("注册成功,你可以登录系统!"))
        Return true

    End Function
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Savedata() Then
        End If
    End Sub

    Private Sub sfzh_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sfzh.TextChanged
        Dim rs As New ADODB.Recordset
        Dim sql As String
        sql = "select  * from xjk where xsdm='" & cc.Checkstr(xsdm.Text) & "' and sfzh='" & cc.Checkstr(sfzh.Text) & "'"
        rs.Open(sql, Conn, 1, 1)
        If Not rs.EOF Then
            Label1.Text = "姓名:" & rs.Fields("xm").Value & " | 所在班级:" & DM2SM("DM_BJK", "dm", rs.Fields("bjbh").Value, "dmsm")
            Label1.Text &= " | 原始密码:" & rs.Fields("mm").Value
            Button1.Enabled = true
        Else
            Label1.Text = "<font color=red>你的学号信息不准确,请向辅导老师询问!</font>"
            Button1.Enabled = False
        End If
        rs.Close()
    End Sub
    Private Sub xsdm_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles xsdm.TextChanged
        Label1.Text = "请认真填写身份证号！"
        Button1.Enabled = False
    End Sub

    Function DM2SM(ByVal Dbname, ByVal F1, ByVal Str1, ByVal F2) As String
        Dim jg As String
        Dim rsy As New ADODB.Recordset
        Dim Conny As New ADODB.Connection
        Conny.Open(cc.setConstr(1))
        rsy.Open("select " & F2 & "  from " & Dbname & " where " & F1 & "='" & Str1 & "'", Conny, 1, 1)
        If Not rsy.EOF Then
            jg = rsy.Fields(0).Value
        Else
            jg = Str1
        End If
        rsy.Close()
        Conny.Close()
        Return jg
    End Function


End Class