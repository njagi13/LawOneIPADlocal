Imports DevExpress.Xpo
Imports LawOne.Module
Imports DevExpress.Data.Filtering

Namespace LawOneIPAD
    Public Class AccountController
        Inherits System.Web.Mvc.Controller

        '
        ' GET: /Account

        Function Index() As ActionResult
            Return View()
        End Function

        Function Login() As ViewResult
            ViewBag.returnurl = Request("returnurl")
            Return View()
        End Function
        <HttpPost()> _
        Function Authorize() As ActionResult
            Dim username As String = Request("username")
            Dim password As String = Request("password")
            Dim uow As Session = New Session()
            uow.ConnectionString = My.Settings.ConStr
            Dim user As DevExpress.Persistent.BaseImpl.User = uow.FindObject(Of Employee)(CriteriaOperator.Parse("UserName = ?", username))
            Dim emp As Employee
            If user IsNot Nothing Then
                If user.ComparePassword(password) Then
                    FormsAuthentication.RedirectFromLoginPage(username, True)
                    ''add the employee to the session
                    emp = user
                    Session("employee") = emp
                    Return RedirectToAction("Index", "InstructionApproval")
                End If
            End If
            Return RedirectToAction("Login")
        End Function

        Function Logout() As ActionResult
            FormsAuthentication.SignOut()
            Return RedirectToAction("Login")
        End Function
    End Class
End Namespace