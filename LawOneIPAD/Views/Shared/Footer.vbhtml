    </div>@*end container*@

@*modal dialog*@
<div class="modal fade" id="modal">
  <div class="modal-dialog">
  <form role="form">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
        <h4 class="modal-title">Decline @ViewBag.Title</h4>
      </div>
      <div class="modal-body">
        <p>Enter the decline message</p>
        <div>
            <textarea name="message" id="message" class="form-control"></textarea>
        </div>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        <button type="button" class="btn btn-primary">Save changes</button>
      </div>
    </div><!-- /.modal-content -->
    </form>
  </div><!-- /.modal-dialog -->
</div><!-- /.modal -->
<script>
    $(document).ready(function () {
        $("#@Viewbag.Active").addClass("active");
    });       
</script>
</body>
</html>