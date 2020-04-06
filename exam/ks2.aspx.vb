Public Class ks2
    Inherits System.Web.UI.Page
    Dim Conn As New ADODB.Connection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cc.Connecttodb()
        If Conn.State = 0 Then Conn.Open(cc.setConstr(3))
        If Not IsPostBack Then
            Label1.Text = getSJ(cc.Checkstr(Request("sjh")))
            Label2.Text = getks(cc.Checkstr(Request("sjh")))

        End If

    End Sub
    Private Function getks(ByVal sjh As String) As String
        Dim rs As New ADODB.Recordset
        Dim sjjg, bjm, xm, zh, kssj As String
        bjm = ""
        xm = ""
        zh = ""
        kssj = ""
        rs.Open("select * from exam_ks where sjh='" & sjh & "'", Conn, 1, 1)
        If Not rs.EOF Then
            bjm = rs.Fields("bjm").Value
            xm = rs.Fields("xm").Value
            zh = rs.Fields("zh").Value
            kssj = rs.Fields("kstime").Value
        End If
        rs.Close()
        sjjg = "班级：" & bjm & ",&nbsp;姓名:"
        sjjg = sjjg & xm & ",&nbsp;座号:"
        sjjg = sjjg & zh & ",&nbsp;开始考试时间:"
        sjjg = sjjg & kssj & "&nbsp;<hr align=left color=red size=1 width=90%><br>"
        Return sjjg
    End Function
    Private Function getSJ(ByVal sjh As String) As String
        Dim rs As New ADODB.Recordset
        Dim stk, tx, sjjg As String
        Dim th, fs, i As Integer
        sjjg = ""
        i = 1
        rs.Open("select * from exam_sj where sjh='" & sjh & "' order by tx,th", Conn, 1, 1)
        While Not rs.EOF
            stk = rs.Fields("stk").Value
            tx = rs.Fields("tx").Value
            th = rs.Fields("th").Value
            fs = rs.Fields("fs").Value
            sjjg = sjjg & Getst(i, stk, tx, th, fs)
            rs.MoveNext()
            i = i + 1
        End While
        rs.Close()

        Return sjjg
    End Function

    Private Function Getst(ByVal i As Integer, ByVal stk As String, ByVal tx As String, ByVal th As Integer, ByVal fs As Integer) As String
        Dim rs As New ADODB.Recordset
        Dim stnr As String
        stnr = ""

        Select Case tx
            Case "XZ"
                rs.Open("select * from " & stk & " where id=" & th & "", Conn, 1, 1)
                If Not rs.EOF Then
                    stnr = stnr & "<br>" & i & "." & rs.Fields("subject").Value & "(" & fs & "分)<br>"
                    If rs.Fields("mode").Value = "0" Then
                        If rs.Fields("option1").Value <> "-" Then
                            stnr = stnr & "<input type=radio name=xz" & th & " value=1>a." & rs.Fields("option1").Value & "<br>"
                        End If
                        If rs.Fields("option2").Value <> "-" Then
                            stnr = stnr & "<input type=radio name=xz" & th & " value=2>b." & rs.Fields("option2").Value & "<br>"
                        End If
                        If rs.Fields("option3").Value <> "-" Then
                            stnr = stnr & "<input type=radio name=xz" & th & " value=3>c." & rs.Fields("option3").Value & "<br>"
                        End If
                        If rs.Fields("option4").Value <> "-" Then
                            stnr = stnr & "<input type=radio name=xz" & th & " value=4>d ." & rs.Fields("option4").Value & "<br>"
                        End If
                        If rs.Fields("option5").Value <> "-" Then
                            stnr = stnr & "<input type=radio name=xz" & th & " value=5>e ." & rs.Fields("option5").Value & "<br>"
                        End If
                        If rs.Fields("option6").Value <> "-" Then
                            stnr = stnr & "<input type=radio name=xz" & th & " value=6>f ." & rs.Fields("option6").Value & "<br>"
                        End If
                    Else
                        If rs.Fields("option1").Value <> "-" Then
                            stnr = stnr & "<input type=checkbox name=xz" & th & " value=1>a." & rs.Fields("option1").Value & "<br>"
                        End If
                        If rs.Fields("option2").Value <> "-" Then
                            stnr = stnr & "<input type=checkbox name=xz" & th & " value=2>b." & rs.Fields("option2").Value & "<br>"
                        End If
                        If rs.Fields("option3").Value <> "-" Then
                            stnr = stnr & "<input type=checkbox name=xz" & th & " value=3>c." & rs.Fields("option3").Value & "<br>"
                        End If
                        If rs.Fields("option4").Value <> "-" Then
                            stnr = stnr & "<input type=checkbox name=xz" & th & " value=4>d ." & rs.Fields("option4").Value & "<br>"
                        End If
                        If rs.Fields("option5").Value <> "-" Then
                            stnr = stnr & "<input type=checkbox name=xz" & th & " value=5>e ." & rs.Fields("option5").Value & "<br>"
                        End If
                        If rs.Fields("option6").Value <> "-" Then
                            stnr = stnr & "<input type=checkbox name=xz" & th & " value=6>f ." & rs.Fields("option6").Value & "<br>"
                        End If

                    End If
                End If

                rs.Close()
            Case "TK"
                rs.Open("select * from " & stk & " where id=" & th & "", Conn, 1, 1)
                If Not rs.EOF Then
                    stnr = stnr & "<br>" & i & "." & rs.Fields("subject").Value & "(" & fs & "分)<br>"
                End If
                stnr = stnr & "请填写:<input type=text size=50 name=tk" & th & "><br><br>"
                rs.Close()
            Case "WD"
                rs.Open("select * from " & stk & " where id=" & th & "", Conn, 1, 1)
                If Not rs.EOF Then
                    stnr = stnr & "<br>" & i & "." & rs.Fields("subject").Value & "(" & fs & "分)<br>"
                End If
                stnr = stnr & "<textarea cols=180 rows=30 name=wd" & th & "></textarea><br><br>"
                rs.Close()
        End Select

        Return stnr

    End Function

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim rs As New ADODB.Recordset
        Dim tx, sjjg As String
        Dim th As Integer
        sjjg = ""
        Response.Write("回答内容如下：<br>")
        rs.Open("select * from exam_sj where sjh='" & cc.Checkstr(Request("sjh")) & "' order by tx,th", Conn, 1, 3)
        While Not rs.EOF
            tx = rs.Fields("tx").Value
            th = rs.Fields("th").Value
            Response.Write(Request(tx & th) & "<br>")
            rs.Fields("xs_ans").Value = HttpUtility.HtmlEncode(Request(tx & th))
            rs.Update()
            rs.MoveNext()
        End While
        rs.Close()

        rs.Open("select * from exam_ks where sjh='" & cc.Checkstr(Request("sjh")) & "'", Conn, 1, 3)
        If Not rs.EOF Then
            rs.Fields("jstime").Value = Now
            rs.Update()
        End If
        rs.Close()

        Label1.Text = "<br>试卷已提交！"
        Button1.Visible = False
    End Sub
End Class