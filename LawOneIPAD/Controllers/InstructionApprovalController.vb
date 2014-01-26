Imports LawOne.Module
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Data.SqlClient
Imports System.IO

Namespace LawOneIPAD


    <Authorize()> _
    Public Class InstructionApprovalController
        Inherits System.Web.Mvc.Controller
        'Inherits System.Web.UI.Page

        '
        ' GET: /InstructionApproval
        ''<Authorize(Roles:="Administrators")> _
        <Authorize()> _
        Function Index() As ViewResult
            Dim uow As New Session()
            uow.ConnectionString = My.Settings.ConStr
            Dim instructionapprovals As ICollection
            Dim instructionapprovalClass As DevExpress.Xpo.Metadata.XPClassInfo
            Dim sortProps As DevExpress.Xpo.SortingCollection
            ' Obtain the persistent object class info required by the GetObjects method 
            instructionapprovalClass = uow.GetClassInfo(Of InstructionApproval)()
            ' Create a sort list if objects must be processed in a specific order 
            sortProps = New SortingCollection(Nothing)
            sortProps.Add(New SortProperty("DateCreated", DevExpress.Xpo.DB.SortingDirection.Descending))
            ' Call GetObjects 
            'instructionapprovals = uow.GetObjects(instructionapprovalClass, CriteriaOperator.Parse("ApprovalStatus = ?", InstructionApprovalStatus.PartnerApprovalPending), sortProps, 0, patcher, True)
            Try
                Dim emp As Employee = Session("Employee")
                instructionapprovals = uow.GetObjects(instructionapprovalClass, CriteriaOperator.Parse("ApprovalStatus = ? and PartnerApproving = ?", InstructionApprovalStatus.PartnerApprovalPending, emp.Oid), sortProps, 1000, False, False)
                ViewBag.instructionapprovals = instructionapprovals
            Catch ex As Exception

            End Try

            ''Return View(instructionapprovals)
            Return View()
        End Function
        '<HttpPost()> _
        'Function Approve() As String
        '    Dim x As String = Request("approvals")
        '    Return ""
        'End Function
        Function approve(ByVal Oid As String) As ActionResult
            Try
                Dim uow As New UnitOfWork()
                Dim guid As New Guid(Oid)
                uow.ConnectionString = My.Settings.ConStr
                Dim iapp As InstructionApproval = uow.GetObjectByKey(Of InstructionApproval)(guid)
                If iapp IsNot Nothing Then
                    If iapp.ApprovalStatus = InstructionApprovalStatus.PartnerApprovalPending Then
                        iapp.ApprovalStatus = InstructionApprovalStatus.ExecutionPending
                        iapp.ApprovalStage = InstructionApprovalStage.PartnerApproved
                        iapp.DateOfApproval = Now
                        iapp.Save()
                    End If
                End If
                uow.CommitChanges()
            Catch ex As Exception

            End Try
            Return RedirectToAction("Index")
        End Function
        ''show the decline form
        Function decline(ByVal Oid As String) As ActionResult
            Dim iapp As InstructionApproval = Nothing
            Try
                Dim uow As New UnitOfWork()
                Dim guid As New Guid(Oid)
                uow.ConnectionString = My.Settings.ConStr
                iapp = uow.GetObjectByKey(Of InstructionApproval)(guid)
                'If iapp IsNot Nothing Then
                '    If iapp.ApprovalStatus = InstructionApprovalStatus.PartnerApprovalPending Then
                '        iapp.ApprovalStatus = InstructionApprovalStatus.Declined
                '        iapp.ApprovalStage = InstructionApprovalStage.PartnerDeclined
                '        iapp.DateOfApproval = Now
                '        iapp.Save()
                '    End If
                'End If
                'uow.CommitChanges()
                'Redirect("~/InstructionApproval")
            Catch ex As Exception

            End Try
            Return View(iapp)
        End Function
        <HttpPost()> _
        Function decline_confirm() As ActionResult
            Try
                Dim uow As New UnitOfWork()
                Dim guid As New Guid(Request("Oid"))
                Dim message As String = Request("Message")
                uow.ConnectionString = My.Settings.ConStr
                Dim iapp As InstructionApproval = uow.GetObjectByKey(Of InstructionApproval)(guid)
                If iapp IsNot Nothing Then
                    If iapp.ApprovalStatus = InstructionApprovalStatus.PartnerApprovalPending Then
                        iapp.ApprovalStatus = InstructionApprovalStatus.Declined
                        iapp.ApprovalStage = InstructionApprovalStage.PartnerDeclined
                        iapp.DateOfApproval = Now
                        iapp.Save()
                        ''create the message
                        Dim msg As New MyMessages(uow)
                        Dim str As String = message
                        Dim insnotes As String
                        insnotes = "Instruction issued on " + iapp.Instruction.DateOfEntry.ToShortDateString() + " Notes: " + iapp.Instruction.Notes
                        msg.Receiver = iapp.Instruction.fSender
                        Dim strfileref As String = ""
                        strfileref = iapp.FileRef.FileRef
                        If String.IsNullOrEmpty(strfileref) = True Then
                            str += " (Sent by " + iapp.PartnerApproving.Initials + ") " + insnotes
                        Else
                            str += " (Sent by " + iapp.PartnerApproving.Initials + ") " + insnotes + " file: " + strfileref
                        End If
                        msg.Notes = str
                        msg.TimeSent = Now
                        If iapp.AttachedFile IsNot Nothing Then msg.AttachedFile = iapp.AttachedFile
                        msg.Save()
                    End If
                End If
                uow.CommitChanges()
                ''Redirect("~/InstructionApproval")
            Catch ex As Exception

            End Try
            Return RedirectToAction("Index")
        End Function



        'Public Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.load
        ' [HttpPost]
        Function ViewPDF() As ActionResult
            Dim stream, mystream As MemoryStream
            If Not (Request.HttpMethod = "POST") Then
                Dim oid As String = Request.QueryString.Get("oid")
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