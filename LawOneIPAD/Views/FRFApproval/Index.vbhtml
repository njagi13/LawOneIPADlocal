@Code
    Layout = Nothing
    ViewBag.Title = "FRF Approvals"
    ViewBag.Active = "frf"
End Code
@Html.Partial("Header")         
        <form method="post" action="~/FRFApproval/Approve">
        <table class="table table-bordered table-striped table-condensed ">
            <thead>
            <tr>
                <th bgcolor="#33ccff">File</th>
                <th bgcolor="#33ccff">Date</th>
                <th bgcolor="#33ccff">Stage</th>
                <th bgcolor="#33ccff">Attachment</th>
                <th bgcolor="#33ccff"></th>
                <th bgcolor="#33ccff"></th>
            </tr>
            </thead>
            <tbody>
                @For Each row As LawOne.Module.FRFApprovals In ViewBag.frfapprovals
                    @<tr id="@row.Oid">
                    <td>@row.FRF.File</td>
                    <td>@row.DateCreated.ToShortDateString()</td>
                    <td>@row.ApprovalStage</td>
                   @* <td>@row.AttachedFile</td>*@
                      <td><a href="~/FRFApproval/ViewPDF?oid=@row.AttachedFile.Oid " >@row.AttachedFile</a></td>
                    @*<td><input type="checkbox" value="@row.Oid" name="approvals[]"></td>*@
                     <td><a href="~/FRFApproval/Approve?oid=@row.Oid" target="_self"><input type="button" class="btn btn-success" value="Approve"/></a></td>
                     <td><a href="~/FRFApproval/Decline?oid=@row.Oid" target="_self"><input type="button" class="btn btn-danger" value="Decline"/></a></td>                  
                    </tr>                    
                Next
            </tbody>
        </table>
        </form>
@Html.Partial("Footer") 