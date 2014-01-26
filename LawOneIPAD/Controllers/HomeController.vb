Imports System.Net
Imports System.Web.Http
<Authorize()> _
Public Class HomeController
    Inherits System.Web.Mvc.Controller
    Public Function Index() As ViewResult
        Return View()
    End Function
End Class
