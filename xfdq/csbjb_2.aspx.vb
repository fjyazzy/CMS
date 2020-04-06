Public Class csbjb_2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim csbjbh As String
        Dim jg As String
        csbjbh = Request("csbjbh")
        jg = "<a href = csbjb_1.aspx?csbjbh=" & csbjbh & "> 1.基本信息</a>|"
        jg &= "<a href =csbjb_2.aspx?csbjbh=" & csbjbh & " > 2.劳务工资</a>|"
        jg &= "<a href = csbjb_3.aspx?csbjbh=" & csbjbh & " > 3.设备工具</a>|"
        jg &= "<a href = csbjb_4.aspx?csbjbh=" & csbjbh & " > 4.配件</a>|"
        jg &= "<a href = csbjb_5.aspx?csbjbh=" & csbjbh & " > 5.材料</a>|"
        jg &= "<a href = csbjb_6.aspx?csbjbh=" & csbjbh & " > 6.交通</a>|"
        jg &= "<a href =csbjb_7.aspx?csbjbh=" & csbjbh & " > 7.管理费、税费及其他</a>|"

        Label3.Text = jg

    End Sub

End Class