Public Class ks3
    Inherits System.Web.UI.Page
    Dim Conn As New ADODB.Connection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cc.Connecttodb()
        If Conn.State = 0 Then Conn.Open(cc.setConstr(3))
        If Not IsPostBack Then
            getksxm()
            getbjm()
            getkshlist()
            Label1.Text = getSJ(DDKSH.SelectedValue)
        End If

    End Sub

    Private Function getSJ(ByVal sjh As String) As String
        Dim rs As New ADODB.Recordset
        Dim tx, sjjg, stk As String
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
            sjjg = sjjg & "<font color=red>答:</font>" & rs.Fields("xs_ans").Value
            sjjg = sjjg & "&nbsp;&nbsp;&nbsp;&nbsp;<font color=red>得分:</font><input type=text name=" & tx & th & " size=3 value='" & rs.Fields("df").Value & "'><br>"
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

        Select Case UCase(tx)
            Case "XZ"
                rs.Open("select * from " & stk & " where id=" & th & "", Conn, 1, 1)
                If Not rs.EOF Then
                    stnr = stnr & "<br>" & i & "." & rs.Fields("subject").Value & "(" & fs & "分)<br>"
                    If rs.Fields("mode").Value = "0" Then
                        If rs.Fields("option1").Value <> "-" Then
                            stnr = stnr & "a." & rs.Fields("option1").Value & "<br>"
                        End If
                        If rs.Fields("option2").Value <> "-" Then
                            stnr = stnr & "b." & rs.Fields("option2").Value & "<br>"
                        End If
                        If rs.Fields("option3").Value <> "-" Then
                            stnr = stnr & "c." & rs.Fields("option3").Value & "<br>"
                        End If
                        If rs.Fields("option4").Value <> "-" Then
                            stnr = stnr & "d ." & rs.Fields("option4").Value & "<br>"
                        End If
                        If rs.Fields("option5").Value <> "-" Then
                            stnr = stnr & "e ." & rs.Fields("option5").Value & "<br>"
                        End If
                        If rs.Fields("option6").Value <> "-" Then
                            stnr = stnr & "f ." & rs.Fields("option6").Value & "<br>"
                        End If
                    Else
                        If rs.Fields("option1").Value <> "-" Then
                            stnr = stnr & "a." & rs.Fields("option1").Value & "<br>"
                        End If
                        If rs.Fields("option2").Value <> "-" Then
                            stnr = stnr & "b." & rs.Fields("option2").Value & "<br>"
                        End If
                        If rs.Fields("option3").Value <> "-" Then
                            stnr = stnr & "c." & rs.Fields("option3").Value & "<br>"
                        End If
                        If rs.Fields("option4").Value <> "-" Then
                            stnr = stnr & "d ." & rs.Fields("option4").Value & "<br>"
                        End If
                        If rs.Fields("option5").Value <> "-" Then
                            stnr = stnr & "e ." & rs.Fields("option5").Value & "<br>"
                        End If
                        If rs.Fields("option6").Value <> "-" Then
                            stnr = stnr & "f ." & rs.Fields("option6").Value & "<br>"
                        End If

                    End If
                End If
                rs.Close()
            Case "TK"
                rs.Open("Select * from " & stk & " where id=" & th & "", Conn, 1, 1)
                If Not rs.EOF Then
                    stnr = stnr & "<br>" & i & "." & rs.Fields("subject").Value & "(" & fs & "分)<br>"
                End If
                rs.Close()
            Case "WD"
                rs.Open("Select * from " & stk & " where id=" & th & "", Conn, 1, 1)
                If Not rs.EOF Then
                    stnr = stnr & "<br>" & i & "." & rs.Fields("subject").Value & "(" & fs & "分)<br>"
                End If
                rs.Close()
        End Select

        Return stnr

    End Function

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim rs As New ADODB.Recordset
        Dim tx, sjjg As String
        Dim th As Integer
        sjjg = ""
        Response.Write("评分结果如下：<br>")
        rs.Open("select * from exam_sj where sjh='" & DDKSH.SelectedValue & "' order by tx,th", Conn, 1, 3)
        While Not rs.EOF
            tx = rs.Fields("tx").Value
            th = rs.Fields("th").Value
            Response.Write(Request(tx & th) & "<br>")
            rs.Fields("df").Value = Request(tx & th)
            rs.Update()
            rs.MoveNext()
        End While
        rs.Close()

        Label1.Text = "<br>试卷已批改！"
    End Sub

    Protected Sub DDKSH_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDKSH.SelectedIndexChanged
        Label1.Text = getSJ(DDKSH.SelectedValue)
    End Sub

    Private Sub getkshlist()
        DDKSH.Items.Clear()
        Dim rs As New ADODB.Recordset
        rs.Open("select * from exam_ks order by sjh", Conn, 1, 3)
        While Not rs.EOF
            Dim ax As New ListItem
            ax.Text = rs.Fields("bjm").Value & "|" & rs.Fields("xm").Value & "|" & rs.Fields("sjh").Value
            ax.Value = rs.Fields("sjh").Value
            DDKSH.Items.Add(ax)
            rs.MoveNext()
        End While
        rs.Close()

    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Conn.Execute("delete from exam_sj where sjh='" & DDKSH.SelectedValue & "'")
        Conn.Execute("delete from exam_ks where sjh='" & DDKSH.SelectedValue & "'")
        getkshlist()
    End Sub



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

End Class