@Code
    Layout = Nothing
    ViewBag.Title = "Instruction Approvals"
    ViewBag.Active = "ins"
End Code
@Html.Partial("Header") 

        <form method="post" action="~/InstructionApproval/Approve">
        <table class="table table-bordered table-striped table-condensed ">
            <thead>
            <tr>
                <th bgcolor="#33ccff">File</th>
                <th bgcolor="#33ccff">Notes</th>
                <th bgcolor="#33ccff">Date</th>
                <th bgcolor="#33ccff">Stage</th>
                <th bgcolor="#33ccff">Attachment</th>
                <th bgcolor="#33ccff"></th>
                <th bgcolor="#33ccff"></th>
            </tr>
            </thead>
            <tbody>
                @For Each row As LawOne.Module.InstructionApproval In ViewBag.instructionapprovals
                    @<tr id="@row.Oid">
                    <td>@row.FileRef</td>
                    <td>@row.Notes</td>
                    <td>@row.DateCreated.ToShortDateString()</td>
                    <td>@row.ApprovalStage</td>
                    <td><a href="~/InstructionApproval/ViewPDF?oid=@row.AttachedFile.Oid " >@row.AttachedFile</a></td>
                
                    
                    <td><a href="~/InstructionApproval/Approve?oid=@row.Oid" target="_self"><input type="button" class="btn btn-success" value="Approve"/></a></td>
                     <td><a href="~/InstructionApproval/Decline?oid=@row.Oid" target="_self"><input type="button" class="btn btn-danger" value="Decline"/></a></td>
               @*     <td><a href="~/InstructionApproval/Approve?oid=@row.Oid">Approve</a></td>
                    <td><a href="~/Instn ructionApproval/Decline?oid=@row.Oid">Decline</a></td>*@
                    </tr>                    
                Next
            </tbody>
        </table>
        <script  language="javascript" type="text/javascript" >
            function checkdata(form) {
                var str = form.oid.value;

                if (str.length = 0) { err.message = "Invalid email" } 
            }
        </script>
        </form>
@Html.Partial("Footer") 
