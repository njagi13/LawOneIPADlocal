Namespace LawOneIPAD
    Public Class DrnfulldetailsController
        Inherits System.Web.Mvc.Controller

        '
        ' GET: /Drnfulldetails

        Function Index(oid As String) As ActionResult
            ViewBag.oid = oid
            Return View()
        End Function

    End Class
End Namespace