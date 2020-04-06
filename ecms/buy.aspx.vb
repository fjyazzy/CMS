Public Class buy
    Inherits System.Web.UI.Page


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim conn As New ADODB.Connection
        CC.Connecttodb()
        If conn.State = 0 Then conn.Open(CC.setConstr(DBord_ecms))
        PageTitle.InnerText = WCS.GetTitle("Title", "Buy")
        Content1.Attributes.Add("name", "description")
        Content1.Attributes.Add("content", WCS.GetTitle("MetaContent", "Buy"))
        Content2.Attributes.Add("name", "keywords")
        Content2.Attributes.Add("content", WCS.GetTitle("MetaKeywords", "Buy"))

        Dim uid As Integer
        If Request("type1") <> "" Then
            If UCase(JYPW.Text) <> Session("VCode") Then
                Panel2.Visible = False
                Panel3.Visible = False
                Panel4.Visible = True
            Else
                Panel4.Visible = False
            End If

            If Panel4.Visible = False Then
                uid = 1
                If Request("name") = "" Or Request("tel") = "" Or Request("address") = "" Then
                    uid = 0
                End If
                If uid = 1 Then
                    If Request("type1") <> "" Then
                        saveOrderItem(Request("type1"), Request("sl1"), Request("manufactory1"), Request("fz1"), Request("jsj1"))
                    End If
                    If Request("type2") <> "" Then
                        saveOrderItem(Request("type2"), Request("sl2"), Request("manufactory2"), Request("fz2"), Request("jsj2"))
                    End If
                    If Request("type3") <> "" Then
                        saveOrderItem(Request("type3"), Request("sl3"), Request("manufactory3"), Request("fz3"), Request("jsj3"))
                    End If
                    Panel1.Visible = True
                    Panel2.Visible = False
                Else
                    Panel2.Visible = False
                    Panel3.Visible = True
                End If
            End If
        End If
        Label2.Text = WCS.lxfs
        If Request("p") <> "" Then
            type1.Text = Request("p")
        End If

    End Sub

    Sub saveOrderItem(ByVal type, ByVal sl, ByVal manufactory, ByVal fz, ByVal jsj)
        Dim rs As New ADODB.Recordset
        Dim sql As String
        Dim conn As New ADODB.Connection
        CC.Connecttodb()
        If conn.State = 0 Then conn.Open(CC.setConstr(DBord_ecms))

        sql = "SELECT top 1 * from inquirys "
        rs = Server.CreateObject("ADODB.Recordset")
        rs.Open(sql, conn, 1, 3, 1)
        rs.AddNew()
        rs.Fields("type").Value = CC.Checkstr(type)
        rs.Fields("sl").Value = CC.Checkstr(sl)
        rs.Fields("manufactory").Value = manufactory
        rs.Fields("fz").Value = CC.Checkstr(fz)
        rs.Fields("ph").Value = ""
        rs.Fields("jsj").Value = CC.Checkstr(jsj)
        rs.Fields("mobile").Value = ""
        rs.Fields("name").Value = Request("name")
        rs.Fields("phone").Value = Request("tel")
        rs.Fields("fax").Value = Request("fax")
        rs.Fields("email").Value = Request("email")
        rs.Fields("company").Value = Request("company")
        rs.Fields("address").Value = Request("address")
        rs.Fields("khbz").Value = Request("resume")
        rs.Fields("ywbz").Value = ""
        rs.Fields("ywy").Value = Request("ywy")
        rs.Fields("bj").Value = ""
        rs.Fields("bjsj").Value = Now
        rs.Fields("zycd").Value = ""
        rs.Fields("hyqk").Value = ""
        rs.Fields("cgjl").Value = ""
        rs.Fields("gxsj").Value = Now
        rs.Fields("isdel").Value = "0"
        rs.Fields("status").Value = "0"
        rs.Update()
        rs.Close()
        conn.Close()

    End Sub


End Class