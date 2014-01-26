' Note: For instructions on enabling IIS6 or IIS7 classic mode, 
' visit http://go.microsoft.com/?LinkId=9394802
Imports System.Web.Http
Imports DevExpress.Data.Filtering
Imports LawOne.Module
Imports DevExpress.Persistent.BaseImpl

Public Class MvcApplication
    Inherits System.Web.HttpApplication

    Sub Application_Start()
        AreaRegistration.RegisterAllAreas()

        WebApiConfig.Register(GlobalConfiguration.Configuration)
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
        RouteConfig.RegisterRoutes(RouteTable.Routes)
    End Sub

    Public Sub FormsAuthentication_OnAuthenticate(sender As Object, args As FormsAuthenticationEventArgs)
        If FormsAuthentication.CookiesSupported Then
            If Not Request.Cookies(FormsAuthentication.FormsCookieName) Is Nothing Then
                Try
                    Dim ticket As FormsAuthenticationTicket = FormsAuthentication.Decrypt( _
                      Request.Cookies(FormsAuthentication.FormsCookieName).Value)

                    Debug.WriteLine(ticket.Name)
                    Dim identity As System.Security.Principal.GenericIdentity
                    identity = New System.Security.Principal.GenericIdentity(ticket.Name)
                    args.User = New System.Security.Principal.GenericPrincipal(identity, getroles(ticket.Name))
                Catch e As HttpException
                    ' Decrypt method failed.
                End Try
            End If
        Else
            Throw New Exception("Cookieless Forms Authentication is not " & _
                                  "supported for this application.")
        End If
    End Sub

    Public Function getroles(ByVal username As String) As String()
        Dim x As List(Of String) = New List(Of String)
        Dim uow As DevExpress.Xpo.Session = New DevExpress.Xpo.Session()
        uow.ConnectionString = My.Settings.ConStr
        Dim user As DevExpress.Persistent.BaseImpl.User = uow.FindObject(Of Employee)(CriteriaOperator.Parse("UserName = ?", username))
        Dim emp As Employee = user
        For Each rl As Role In emp.Roles
            x.Add(rl.Name)
        Next
        Return x.ToArray()
    End Function
End Class
