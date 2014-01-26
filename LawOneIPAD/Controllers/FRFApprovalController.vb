Imports LawOne.Module
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Data.SqlClient
Imports System.IO
Namespace LawOneIPAD
    <Authorize()> _
    Public Class FRFApprovalController
        Inherits System.Web.Mvc.Controller

        '
        ' GET: /FRFApproval

        Function Index() As ActionResult
            Dim uow As New Session()
            uow.ConnectionString = My.Settings.ConStr
            Dim frfapprovals As ICollection
            Dim frfapprovalClass As DevExpress.Xpo.Metadata.XPClassInfo
            Dim sortProps As DevExpress.Xpo.SortingCollection
            ' Obtain the persistent object class info required by the GetObjects method 
            frfapprovalClass = uow.GetClassInfo(Of FRFApprovals)()
            ' Create a sort list if objects must be processed in a specific order 
            sortProps = New SortingCollection(Nothing)
            sortProps.Add(New SortProperty("DateCreated", DevExpress.Xpo.DB.SortingDirection.Descending))
            ' Call GetObjects 
            Try
                Dim emp As Employee = Session("Employee")
                frfapprovals = uow.GetObjects(frfapprovalClass, CriteriaOperator.Parse("PartnerApproval=? and ApprovalStage = ? and PartnerApproving = ?", FRFStatus.Unapproved, FRFStatus.FeeEarnerApproved, emp.Oid), sortProps, 1000, False, False)
                'frfapprovals = uow.GetObjects(frfapprovalClass, CriteriaOperator.Parse("DateCreated < ?", Now), sortProps, 1000, False, False)
                ViewBag.frfapprovals = frfapprovals
            Catch ex As Exception

            End Try
            
            ''Return View(instructionapprovals)
            Return View()
        End Function
        Sub approve(ByVal Oid As String)
            Try
                Dim uow As New UnitOfWork()
                Dim guid As New Guid(Oid)
                uow.ConnectionString = My.Settings.ConStr
                Dim iapp As DepositRequestNoteApproval = uow.GetObjectByKey(Of DepositRequestNoteApproval)(guid)
                If iapp IsNot Nothing Then
                    If iapp.ApprovalStage = DRNStatus.AccountsApproved Then
                        iapp.PartnerApproval = DRNStatus.PartnerApproved
                        iapp.Save()
                    End If
                End If
                uow.CommitChanges()
                Redirect("~/DrnApproval")
            Catch ex As Exception

            End Try

        End Sub

        Sub decline(ByVal Oid As String)
            Try
                Dim uow As New UnitOfWork()
                Dim guid As New Guid(Oid)
                uow.ConnectionString = My.Settings.ConStr
                Dim iapp As DepositRequestNoteApproval = uow.GetObjectByKey(Of DepositRequestNoteApproval)(guid)
                If iapp IsNot Nothing Then
                    If iapp.ApprovalStage = DRNStatus.AccountsApproved Then
                        iapp.PartnerApproval = DRNStatus.PartnerDeclined
                        iapp.Save()
                    End If
                End If
                uow.CommitChanges()
                Redirect("~/DrnApproval")
            Catch ex As Exception

            End Try

        End Sub



        Function ViewPDF() As ActionResult
            Dim stream, mystream As MemoryStream
            If Not (Request.HttpMethod = "POST") Then
                Dim Oid As String = Request.QueryString.Get("oid")
                Dim sqlcon As New SqlConnection(My.Settings.ConStr)
                sqlcon.Open()
                Dim sqlcmd As SqlCommand = New SqlCommand()
                Dim query As String = String.Format("select top 1 * from filedata where oid = '{0}'", Oid)
                sqlcmd.CommandText = query
                sqlcmd.Connection = sqlcon
                Dim dr As SqlDataReader = sqlcmd.ExecuteReader()
                If dr.HasRows Then
                    dr.Read()
                    Dim filename As String = dr.Item("FileName")
                    Dim content As Byte() = dr.Item("Content")
                    Dim savename As New StringBuilder()
                    Dim path As String = Environment.GetEnvironmentVariable("Temp")
                    path += "\"
                    savename.Append(path + filename)
                    stream = New MemoryStream(content)
                    'save the stream to a temporary file
                    'stream.Write(content, 0, content.Length)
                    'mystream = stream
                    'stream.Close()
                    'Dim fileData = System.IO.File.ReadAllBytes(savename.ToString())
                    'Dim contentDisposition = String.Format("inline; filename={0}", filename)
                    'System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", contentDisposition)
                    'System.Web.HttpContext.Current.Response.ContentType = "application/pdf"
                    'System.Web.HttpContext.Current.Response.BinaryWrite(fileData)
                    'System.IO.File.Delete(savename.ToString())

                End If
                'Return File(Path + filename, System.Net.Mime.MediaTypeNames.Application.Pdf, filename)

            End If
            Return File(stream, "application/pdf")
        End Function
    End Class
End Namespace