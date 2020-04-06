Public Class sumzf
    Inherits System.Web.UI.Page
    Dim Conn As New ADODB.Connection
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If cc.getQx(Request.Cookies("Username").Value, "6004") = 0 Then
            Response.Write("没有权限")
            Response.End()
        End If
        If Conn.State = 0 Then Conn.Open(cc.setConstr(3))
        cc.getSoftinfo("4")
        lLink.Attributes.Add("href", "../cmscss/home/style" & SYSTEMSTYLE & ".css")

        getksxm()
        getbjm()
    End Sub


    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Conn.Execute("update  homework set zf=zy1+zy2+zy3+zy4+zy5+zy6+zy7+zy8+zy9+zy10+zy11+zy12+zy13 ")
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim rs As New ADODB.Recordset
        Dim tx, sjjg, da As String
        Dim th, fs, df As Integer
        sjjg = ""
        rs.Open("select * from exam_sj order by tx,th", Conn, 1, 3)
        While Not rs.EOF
            tx = rs.Fields("tx").Value
            th = rs.Fields("th").Value
            fs = rs.Fields("fs").Value
            If System.Convert.IsDBNull(rs.Fields("xs_ans").Value) = true Then
                df = 0
            Else
                da = trim(rs.Fields("xs_ans").Value)
                df = pddf(tx, th, da, fs)
            End If

            rs.Fields("df").Value = df
            rs.Update()
            rs.MoveNext()
        End While
        rs.Close()
    End Sub

    Private Function pddf(ByVal tx As String, ByVal th As Integer, ByVal da As String, ByVal fs As Integer) As Integer
        Dim rs As New ADODB.Recordset
        Dim df As Integer
        df = 0
        Select Case tx
            Case "XZ"
                rs.Open("select * from xz where id=" & th & "", Conn, 1, 1)
                If Not rs.EOF Then
                    If trim(rs.Fields("answer").Value) = da Then
                        df = fs
                    Else
                        df = 0
                    End If
                End If
                rs.Close()
            Case "TK"
                rs.Open("Select * from tk where id=" & th & "", Conn, 1, 1)
                If Not rs.EOF Then
                    If trim(rs.Fields("answer").Value) = da Then
                        df = fs
                    Else
                        df = 0
                    End If
                End If
                rs.Close()
            Case "WD"
                rs.Open("Select * from wd where id=" & th & "", Conn, 1, 1)
                If Not rs.EOF Then
                    If trim(rs.Fields("answer").Value) = da Then
                        df = fs
                    Else
                        df = 0
                    End If
                End If
                rs.Close()
        End Select


        Return df
    End Function

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Conn.Execute("delete from exam_ks")
        Conn.Execute("delete from exam_sj")
        Button3.Text = "考试已经启动！"
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