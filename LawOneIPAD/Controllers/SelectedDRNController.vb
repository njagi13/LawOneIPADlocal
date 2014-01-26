Imports LawOne.Module
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.IO
Imports System.Data.SqlClient

Namespace LawOneIPAD
    Public Class SelectedDRNController
        Inherits System.Web.Mvc.Controller

        '
        ' GET: /SelectedDRN

        Function Index() As ActionResult


            Return View()
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