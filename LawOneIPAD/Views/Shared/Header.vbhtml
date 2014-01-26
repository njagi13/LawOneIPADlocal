<!DOCTYPE html>
<html>
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>@ViewBag.Title</title>
    <link href="@Url.Content("~/assets/twitter/css/bootstrap.min.css")" rel="stylesheet" type="text/css" />
    <link href="@Url.Content("~/assets/css/style.css")" rel="stylesheet" type="text/css" />
    <script src="@Url.Content("~/assets/js/jquery-1.9.1.min.js")"></script>
    <script src="@Url.Content("~/assets/twitter/js/bootstrap.min.js")"></script>
    <script src="@Url.Content("~/assets/js/knockout-2.1.0.js")"></script>
</head>
<body>
    <div class="container">
    <nav class="navbar navbar-inverse" role="navigation">
  <!-- Brand and toggle get grouped for better mobile display -->
  <div class="navbar-header">
    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
      <span class="sr-only">Toggle navigation</span>
      <span class="icon-bar"></span>
      <span class="icon-bar"></span>
      <span class="icon-bar"></span>
    </button>
    <a class="navbar-brand" href="#">Law One</a>
  </div>

  <!-- Collect the nav links, forms, and other content for toggling -->
  <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
    <ul class="nav navbar-nav">
      <li><a href="~/instructionapproval" id="ins">Instruction Approvals</a></li>
      <li><a href="~/drnapproval" id="drn">DRN Approvals</a></li>      
      <li><a href="~/frfapproval" id="frf">FRF Approvals</a></li>      
    </ul>
    <ul class="nav navbar-nav navbar-right">
      <li><a href="~/account/logout">Logout</a></li>
    </ul>
  </div><!-- /.navbar-collapse -->
</nav>
<div class="page-header">
    <h1>@ViewBag.Title</h1>
</div>