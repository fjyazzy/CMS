Imports System.Web
Imports System.Web.Services

Public Class Ecms_OpenApi
    Implements System.Web.IHttpHandler

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim jg As String = ""
        Dim a As String = CC.Checkstr(context.Request("a"))
        DBord_ecms = CC.Checkstr(context.Request("DBord"))
        Select Case a
            Case "GetSystemInfo"
                Dim itemno As String = CC.Checkstr(context.Request("itemno"))
                jg = WCS.GetSystemInfo(itemno)
            Case "GetWebContent"
                Dim mc As String = CC.Checkstr(context.Request("mc"))
                jg = WCS.GetWebContent(mc)
            Case "GetTitle"
                Dim str1 As String = CC.Checkstr(context.Request("str1"))
                Dim str2 As String = CC.Checkstr(context.Request("str2"))
                jg = WCS.GetTitle(str1, str2)
            Case "GetCategoryList"
                jg = WCS.GetCategoryList()
            Case "SearchP"
                Dim mode As String = CC.Checkstr(context.Request("mode"))
                Dim skey As String = CC.Checkstr(context.Request("skey"))
                Dim pagex As String = CC.Checkstr(context.Request("pagex"))
                Dim thisprog As String = CC.Checkstr(context.Request("thisprog"))
                jg = WCS.SearchP(mode, skey, pagex, thisprog)
            Case "pCateList"  '产品目录
                Dim thisprog As String = CC.Checkstr(context.Request("thisprog"))
                jg = WCS.pCateList(thisprog)
            Case "CateList"   '资料目录
                Dim thisprog As String = CC.Checkstr(context.Request("thisprog"))
                jg = WCS.CateList(thisprog)
            Case "SearchZl"
                Dim mode As String = CC.Checkstr(context.Request("mode"))
                Dim skey As String = CC.Checkstr(context.Request("skey"))
                Dim pagex As String = CC.Checkstr(context.Request("pagex"))
                Dim thisprog As String = CC.Checkstr(context.Request("thisprog"))
                jg = WCS.SearchZl(mode, skey, pagex, thisprog)
            Case "lxfs"
                jg = WCS.lxfs()
            Case "xxzl"
                Dim cid As String = CC.Checkstr(context.Request("cid"))
                jg = WCS.xxzl(cid)
            Case "countManual"
                Dim cid As String = CC.Checkstr(context.Request("cid"))
                WCS.countManual(cid)
            Case "countProduct"
                Dim cid As String = CC.Checkstr(context.Request("cid"))
                WCS.countProduct(cid)
            Case "getCateName4cid"
                Dim cid As String = CC.Checkstr(context.Request("cid"))
                jg = WCS.getCateName4cid(cid)
            Case "GetManual4id"
                Dim cid As String = CC.Checkstr(context.Request("cid"))
                jg = WCS.GetManual4id(cid)
            Case "GetProduct4id"
                Dim cid As String = CC.Checkstr(context.Request("cid"))
                jg = WCS.GetProduct4id(cid)
            Case "saveOrderItem"
                Dim name As String = CC.Checkstr(context.Request("name"))
                Dim tel As String = CC.Checkstr(context.Request("tel"))
                Dim fax As String = CC.Checkstr(context.Request("fax"))
                Dim email As String = CC.Checkstr(context.Request("email"))
                Dim company As String = CC.Checkstr(context.Request("company"))
                Dim address As String = CC.Checkstr(context.Request("address"))
                Dim resumex As String = CC.Checkstr(context.Request("resumex"))
                Dim ywy As String = CC.Checkstr(context.Request("ywy"))
                Dim Type As String = CC.Checkstr(context.Request("Type"))
                Dim sl As String = CC.Checkstr(context.Request("sl"))
                Dim manufactory As String = CC.Checkstr(context.Request("manufactory"))
                Dim fz As String = CC.Checkstr(context.Request("fz"))
                Dim jsj As String = CC.Checkstr(context.Request("jsj"))
                jg = WCS.saveOrderItem(name, tel, fax, email, company, address, resumex, ywy, Type, sl, manufactory, fz, jsj)
        End Select

        context.Response.ContentType = "text/plain"
        context.Response.Write(jg)

    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class