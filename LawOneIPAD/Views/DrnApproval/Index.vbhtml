@Code
    Layout = Nothing
    ViewBag.Title = "DRN Approvals"
    ViewBag.Active = "drn"
End Code
@Html.Partial("Header") 
        <form method="post" action="~/DrnApproval/Approve">
        <table class="table table-bordered table-striped table-condensed ">
            <thead>
            <script type="text/javascript">
                    function DoNav(theUrl) {
//                        document.location.html = theUrl;
                        
                        window.location.href = theUrl;
                    }
                    function ChangeColor(tableRow, highLight) {
                        if (highLight) {
                            tableRow.style.backgroundColor = '#dcfac9';
                        }
                        else {
                            tableRow.style.backgroundColor = 'white';
                        }
                    }
           </script>
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
                @For Each row As LawOne.Module.DepositRequestNoteApproval In ViewBag.drnapprovals
                   @<tr id="@row.Oid";
                   @*onclick="DoNav('@Url.Action("ViewPDF", "DrnApproval", New With {.oid = row.AttachedFile.Oid})');"*@
                   onclick="DoNav('@Url.Action("Index", "Drnfulldetails", New With {.oid = row.AttachedFile.Oid})')"
                    onmouseover="ChangeColor(this, true);" 
                    onmouseout="ChangeColor(this, false);">
                   <td >@row.Oid</td>
                   <td>@row.FileRef</td>
                    <td>@row.DepositRequestNote.Notes</td>
                    <td>@row.DateCreated.ToShortDateString()</td>
                    <td>@row.ApprovalStage</td>
                  @* <td>@row.AttachedFile.Oid</td>*@

                    <@*td><a href="~/DrnApproval/ViewPDF?oid=@row.AttachedFile.Oid" >@row.AttachedFile</a></td>*@

                    @*<td>@row.AttachedFile</td>*@
                    @*<td><input type="checkbox" value="@row.Oid" name="approvals[]"></td>*@
                     @*<td><a href="~/DrnApproval/Approve?oid=@row.Oid" target="_self"><input type="button" style="background-color:#00FF33;" value="Approve"/></a></td>
                     <td><a href="~/DrnApproval/Decline?oid=@row.Oid" target="_self"><input type="button"style="background-color:#FF0000;"  value="Decline"/></a></td>*@
                     @*<td><a href="~/DrnApproval/Approve?oid=@row.Oid" target="_self"><input type="button" class="btn btn-success" value="Approve"/></a></td>
                     <td><a href="~/DrnApproval/Decline?oid=@row.Oid" target="_self"><input type="button" class="btn btn-danger" value="Decline"/></a></td>*@

                    
                   @* <td><a href="~/DrnApproval/Approve?oid=@row.Oid">Approve</a></td>
                    <td><a href="~/DrnApproval/Decline?oid=@row.Oid">Decline</a></td>*@
                    </tr> 
                              
                Next
            </tbody>
        </table>
        </form>
@Html.Partial("Footer") 