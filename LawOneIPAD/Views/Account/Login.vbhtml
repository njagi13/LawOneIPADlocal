@Code
    Layout = Nothing
End Code

<!DOCTYPE html>

<html>
<head runat="server">
<h3 align="center">Welcome to Lawone Mobile Devices Application</h3>
<h4 align="center">Log on to continue to Lawone</h4>
    <meta name="viewport" content="width=device-width" />
    <title>Login</title>
    <link href="@Url.Content("~/assets/twitter/css/bootstrap.min.css")" rel="stylesheet" type="text/css" />
    <link href="@Url.Content("~/assets/css/style.css")" rel="stylesheet" type="text/css" />
    <script src="@Url.Content("~/assets/js/jquery-1.9.1.min.js")"></script>
    <script src="@Url.Content("~/assets/js/twitter/js/bootstrap.min.js")"></script>
    <script src="@Url.Content("~/assets/js/knockout-2.1.0.js")"></script>
</head>
<body>
    <div class="container">
    <div>
    <form role="form" class="form-horizontal" method="post" action="~/Account/Authorize">
        <input type="hidden" value="@ViewBag.returnurl" name="returnurl" />
<div class="form-group">
    <label for="username" class="col-sm-2 control-label">Username</label>
    <div class="col-sm-10">
      <input type="text" class="form-control" id="username" name="username" placeholder="Username">
    </div>
  </div>
  <div class="form-group">
    <label for="password" class="col-sm-2 control-label">Password</label>
    <div class="col-sm-10">
      <input type="password" class="form-control" id="password" name="password" placeholder="Password">
    </div>
  </div>
  <div class="form-group">
    <div class="col-sm-offset-2 col-sm-10">
      <div class="checkbox">
        <label>
          <input type="checkbox"> Remember me
        </label>
      </div>
    </div>
  </div>
  <div class="form-group">
    <div class="col-sm-offset-2 col-sm-10">
      <button type="submit" class="btn btn-default" style="background-color:#33ccff">Login</button>
    </div>
  </div>
</form>
    </div>
        
    </div>
</body>
</html>
