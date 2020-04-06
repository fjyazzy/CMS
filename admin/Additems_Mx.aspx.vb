Public Class Additems_Mx
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
        Cid = CInt(Request("cid"))

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

        '删除数据库-附加图片功能 ver1.0
        If pgn = "del" Then
            Conn.Execute("delete from " & Dbname & "_IMAGES where id=" & pID)
        End If
        Label2.Text = ShowOneForm(DBOrd, Cid, xID, FieldName, FieldType, FieldLen, isPIC)

        '根据数据库的表名判断有无子表
        '案例参看xfdq系统的承试报价表
        Label1.Text = TLS.Addfunction(Dbname, xID, 1)


    End Sub


    ''' <summary>
    ''' 保存按钮的功能
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim i As Integer
        Dim rs As New ADODB.Recordset
        Dim jg As String

        rs.Open("select * from " & DBName & " where id=" & xID, Conn, 1, 3)
        If xID = "0" Then
            rs.AddNew()
        End If
        For i = 1 To FieldNum
            rs.Fields(i).Value = Request("F" & i)
        Next
        rs.Update()
        rs.Close()
        Conn.Close()

        jg = "<script language = ""javascript"" >"
        jg &= "parent.location.reload();"
        jg &= "</script>"
        Response.Write(jg)

    End Sub


    ''' <summary>
    ''' 主界面的入口函数
    ''' </summary>
    ''' <param name="xID"></param>  '主数据库ID
    ''' pgn，pid  增加图片删除功能
    ''' <param name="FieldName"></param>
    ''' <param name="FieldType"></param>
    ''' <param name="FieldLen"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ShowOneForm(ByVal DBord, ByVal Cid, ByVal xID, ByVal FieldName(), ByVal FieldType(), ByVal FieldLen(), ByVal isPIC) As String
        Dim jgx, jgy As String
        jgx = ""
        jgy = ""

        If xID > 0 Then
            '编辑功能 ver1.0
            jgx = DrawOnePage(FieldName, FieldType, FieldLen)
            Dim rs As New ADODB.Recordset
            rs.Open("select * from " & Dbname & " where ID=" & xID, Conn, 1, 1)
            Dim i As Integer
            If Not rs.EOF Then
                For i = 1 To FieldNum
                    If System.Convert.IsDBNull(rs.Fields(i).Value) Then
                        jgx = Replace(jgx, "<<V" & i & ">>", "-")
                    Else
                        jgx = Replace(jgx, "<<V" & i & ">>", rs.Fields(i).Value)
                    End If
                Next
            Else
                For i = 1 To FieldNum
                    jgx = Replace(jgx, "<<V" & i & ">>", "-")
                Next
            End If
            '图片部分
            If isPIC = "1" Then
                jgy = "<tr><td colspan=2 class=""txtdata"">图片列表<td></tr>"
                jgy &= "<tr><td colspan=2 class=""txtdata"">"
                jgy &= GetImage(DBord, xID)
                jgy &= "<a href=# onclick=openWin('addImages.aspx?DBord=" & DBord & "&dbname=" & Dbname & "&lx=" & xID & "','','')>"
                jgy &= " + 添加图片...</a><td></tr>"
            Else
                jgy = ""
            End If
            jgx = Replace(jgx, "<<Imageinfo>>", jgy)
            rs.Close()
        Else
            '添加功能 ver1.0
            jgx = DrawOnePage(FieldName, FieldType, FieldLen)
            Dim i As Integer

            '如果副目录存在，查找目录编号并写到关联字段中
            If Cid > 0 Then
                Select Case Dbname
                    Case "Product"
                        jgx = Replace(jgx, "<<V1>>", Get_p_C_bh(Cid）)
                    Case Else
                        jgx = Replace(jgx, "<<V1>>", "-")
                End Select
            Else
                jgx = Replace(jgx, "<<V1>>", "-")
            End If

            '通用配置
            For i = 2 To FieldNum
                jgx = Replace(jgx, "<<V" & i & ">>", "-")
            Next
            '不需要图片部分
            jgx = Replace(jgx, "<<Imageinfo>>", "")
        End If

        Return jgx

    End Function

    ''' <summary>
    ''' 检索产品的目录编号
    ''' </summary>
    ''' <param name="bh"></param>
    ''' <returns></returns>
    Private Function Get_p_C_bh(ByVal bh As String) As String
        Dim rs As New ADODB.Recordset
        Dim jg As String = ""
        rs.Open("select * from Category where id=" & bh, Conn, 1, 1)
        If rs.EOF Then
            jg = "请输入目录分类号"
        Else
            jg = rs.Fields("itemname").Value
        End If
        rs.Close()
        Return jg
    End Function

    ''' <summary>
    ''' 显示一条记录的附件图片信息
    ''' </summary>
    ''' <param name="xid"></param>
    ''' <returns></returns>
    Public Function GetImage(ByVal xid As Long, ByVal DBord As Integer) As String
        Dim jgx As String = ""
        Dim ImagePath As String = ""
        ImagePath = "../Center_photos/" & CC.DBord2path(DBord) & "/"

        Dim xUrl As String = HttpContext.Current.Request.Url.Query
        '去除URL中多余的部分
        Dim i As Integer = InStr(xUrl, "&pgn")
        If i > 0 Then
            xUrl = xUrl.Substring(0, i - 1)
        End If

        Dim rs As New ADODB.Recordset
        rs.Open("Select * from " & Dbname & "_images where pid=" & xid, Conn, 1, 1)
        While Not rs.EOF()
            jgx &= "<a target=_blank href=" & ImagePath & Replace(rs.Fields("imgurl").Value, "T_", "") & ">"
            jgx &= "<img src = " & ImagePath & rs.Fields("imgurl").Value & " border=0></a>"
            jgx &= "<a href='" & xUrl & "&pgn=del&pid=" & rs.Fields("id").Value & "'>"
            jgx &= "<img src = ""../images/fun/del.png"" alt=""删除图片""></a>"
            rs.MoveNext()
        End While
        rs.Close()
        Return jgx
    End Function
    ''' <summary>
    '''  画一个单条记录数据维护表格（不包含数据）
    '''  把数据部门用 V1,V2...暂时占位
    '''  把图片部分用 ImageInfo 变量占位
    ''' </summary>
    ''' <param name="FieldName"></param>
    ''' <param name="FieldType"></param>
    ''' <param name="FieldLen"></param>
    ''' <returns></returns>
    Private Function DrawOnePage(ByVal FieldName(), ByVal FieldType(), ByVal FieldLen()) As String
        Dim jgx As String
        Dim i As Integer
        jgx = "<table border=1 cellspacing=0 cellpadding=1 width=98% style=""border-collapse:collapse;"">"
        jgx &= "<tr><td colspan=2 bgcolor=""#eeeeee"" class=""txtdata"">数据维护界面</td></tr>"
        For i = 1 To FieldNum
            jgx &= "<tr><td class=""txtdata"">" & FieldName(i) & "</td><td>"
            Select Case FieldType(i)
                Case "T"
                    jgx &= "<TextArea class=""txtdata"" rows=3 cols=" & FieldLen(i) & " name=F" & i & " ><<V" & i & ">></TextArea>"
                Case Else
                    jgx &= "<input class=""txtdata"" type=text size=" & FieldLen(i) & " name=F" & i & " value='<<V" & i & ">>'>"
            End Select
            jgx &= "</td></tr>"
        Next
        jgx &= "<<Imageinfo>>"
        jgx &= "</table>"
        Return jgx
    End Function


End Class