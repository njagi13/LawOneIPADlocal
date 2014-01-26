@ModelType LawOne.Module.DepositRequestNoteApproval
@Code
    Layout = Nothing
    ViewBag.Title = "DRN Approvals - Decline Message"
    ViewBag.Active = "drn"
End Code
@Html.Partial("Header") 
<div>
<form class="form-horizontal" role="form" method="post" action="~/DrnApproval/Decline_Confirm">
  <div class="form-group">
    <label for="message" class="col-sm-2 control-label">Message</label>
    <div class="col-sm-10">
      <textarea name="message" id="message" rows="5" cols="10" class="form-control"></textarea>
    </div>
  </div>
  <div class="form-group">
    <div class="col-sm-offset-2 col-sm-10">
      <button class="btn btn-success" style="width:80px"> OK  </button>
      <input Type="button" VALUE="Cancel" class="btn btn-danger" onClick="history.go(-1);return true;">
      <input type="hidden" name="oid" value="@Model.Oid" />
    </div>
  </div>
</form>
</div>
@Html.Partial("Footer") 