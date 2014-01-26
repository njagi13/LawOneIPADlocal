Imports LawOne.Module
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Data.SqlClient
Imports System.IO
Namespace LawOneIPAD
    <Authorize()> _
    Public Class DrnApprovalController
        Inherits System.Web.Mvc.Controller

        '
        ' GET: /DrnApproval

        Function Index() As ActionResult
            Dim uow As New Session()
            uow.ConnectionString = My.Settings.ConStr
            Dim drnapprovals As ICollection
            Dim drnapprovalClass As DevExpress.Xpo.Metadata.XPClassInfo
            Dim sortProps As DevExpress.Xpo.SortingCollection
            ' Obtain the persistent object class info required by the GetObjects method 
            drnapprovalClass = uow.GetClassInfo(Of DepositRequestNoteApproval)()
            ' Create a sort list if objects must be processed in a specific order 
            sortProps = New SortingCollection(Nothing)
            sortProps.Add(New SortProperty("DateCreated", DevExpress.Xpo.DB.SortingDirection.Descending))
            ' Call GetObjects 
            'instructionapprovals = uow.GetObjects(instructionapprovalClass, CriteriaOperator.Parse("ApprovalStatus = ?", InstructionApprovalStatus.PartnerApprovalPending), sortProps, 0, patcher, True)
            Try
                Dim emp As Employee = Session("Employee")
                drnapprovals = uow.GetObjects(drnapprovalClass, CriteriaOperator.Parse("AccountsApproval = ? and PartnerApproving = ?", DRNStatus.AccountsApproved, emp.Oid), sortProps, 1000, False, False)
                ' drnapprovals = uow.GetObjects(drnapprovalClass, CriteriaOperator.Parse("AccountsApproval = ? and PartnerApproving = ?", DRNStatus.AccountsApproved, emp.Oid), sortProps, 1000, False, False)
                ViewBag.drnapprovals = drnapprovals
            Catch ex As Exception

            End Try
            
            ''Return View(instructionapprovals)
            Return View()
        End Function
        Function approve(ByVal Oid As String) As ActionResult
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
            Catch ex As Exception

            End Try
            Return RedirectToAction("Index")
        End Function

        Function decline(ByVal Oid As String) As ActionResult
            Dim iapp As DepositRequestNoteApproval = Nothing
            Try
                Dim uow As New UnitOfWork()
                Dim guid As New Guid(Oid)
                uow.ConnectionString = My.Settings.ConStr
                iapp = uow.GetObjectByKey(Of DepositRequestNoteApproval)(guid)

            Catch ex As Exception

            End Try
            Return View(iapp)
        End Function

        Function decline_confirm(ByVal Oid As String, ByVal message As String) As ActionResult
            Dim iapp As DepositRequestNoteApproval = Nothing
            Try
                Dim uow As New UnitOfWork()
                Dim guid As New Guid(Oid)
                uow.ConnectionString = My.Settings.ConStr
                iapp = uow.GetObjectByKey(Of DepositRequestNoteApproval)(guid)
                If iapp IsNot Nothing Then
                    If iapp.AccountsApproval = DRNStatus.AccountsApproved Then
                        iapp.PartnerApproval = DRNStatus.PartnerDeclined
                        iapp.DateOfApproval = Now
                        iapp.Save()
                        ''create the message
                        Dim msg As New MyMessages(uow)
                        Dim str As String = message
                        Dim insnotes As String
                        insnotes = "DRN No. " + iapp.DepositRequestNote.Ref.ToString() + " File No. " + iapp.DepositRequestNote.FileRef.ToString()
                        msg.Receiver = iapp.fSender
                        str += " (Sent by " + iapp.PartnerApproving.Initials + ") " + insnotes
                        msg.Notes = str
                        msg.TimeSent = Now
                        If iapp.AttachedFile IsNot Nothing Then msg.AttachedFile = iapp.AttachedFile
                        msg.Save()
                    End If
                End If
                uow.CommitChanges()
            Catch ex As Exception

            End Try
            Return RedirectToAction("Index")
        End Function

        Function ViewPDF(oid As String) As ActionResult
            Dim stream, mystream As MemoryStream
            If Not (Request.HttpMethod = "POST") Then
                'oid = Request.QueryString.Get("oid")
                Dim sqlcon As New SqlConnection(My.Settings.ConStr)
                sqlcon.Open()
                Dim sqlcmd As SqlCommand = New SqlCommand()
                Dim query As String = String.Format("select top 1 * from filedata where oid = '{0}'", oid)
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