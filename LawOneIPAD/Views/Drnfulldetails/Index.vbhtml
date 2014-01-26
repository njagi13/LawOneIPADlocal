@Code
    Layout = Nothing
    ViewData("Title") = "Drnfulldetails"
End Code

<h2>Drnfulldetails</h2>
<iframe src="@Url.Action("ViewPDF", "SelectedDRN", New With {.oid = ViewBag.oid})" width="800" height="600" frameborder="0"></iframe>

<button class="btn btn-success" style="width:80px"> Approve </button>
<button class="btn btn-success" style="width:80px"> Decline </button>
<input Type="button" VALUE="Cancel" class="btn btn-danger" onClick="history.go(-1);return true;">