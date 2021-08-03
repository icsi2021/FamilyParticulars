<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <link rel='shortcut icon' type='image/x-icon' href="/Content/images/favicon.ico" />
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
    <link href="https://cdn.datatables.net/1.10.19/css/dataTables.bootstrap.min.css"
        rel="stylesheet" type="text/css" />
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css"
        rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <link href="css/Footer.css" rel="stylesheet" />
    <title>Family Particulars</title>
    <style>
        .dsp_portal_logo_sm {
            width: 80px;
            padding-top: 15px;
        }
    </style>
    <script>
        function preventBack() {
            window.history.forward();
        }
        window.onunload = function () {
            null;
        };
        setTimeout("preventBack()", 0);
    </script>
</head>
<body>
    <div class="main-new_home" style="overflow-x: hidden">
        <section id="Icsi_New_top_header">
            <div class="container">
                <div class="row">
                    <div class="col-md-6">
                        <a href="https://www.icsi.edu/home">
                            <img src="Images/logo_full.png" alt="logo" class="img-responsive" id="mainLogo">
                        </a>
                    </div>
                    <div class="col-md-4" style="float: right; padding-top: 30px">
                        <h2 class="logo_text_DSp" style="text-align: right; color: darkblue"><b>Family Particular</b></h2>
                    </div>
                </div>
            </div>
            <div class="container-fluid" style="padding: 0px;">
                <div class="row">
                    <div class="no-padding col-md-12">
                        <div class="moto_img">
                            <img src="Images/moto.jpg" alt="moto" width="100%" class="img-responsive" />
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section>
            <div class="top_brandsnav container-fluid" style="padding: 0px;">
                <nav class="navbar navbar-expand-lg navbar-dark" style="background: linear-gradient(to right, #083b82, #061f42); height: 35px; border-bottom: 1px solid #fcd73c;">
                    <div class="collapse navbar-collapse" id="navbarNavAltMarkup">
                        <asp:Label class="nav-item nav-link active" Style="color: white; font-size: medium; float: left; padding-top: 10px" ID="lblmessage" runat="server" Visible="false"></asp:Label>

                        <asp:Label Style="color: white; float: left; padding-top: 10px" ID="lblwelcomeback" runat="server" Visible="false"></asp:Label>
                        <div class="navbar-nav" style="float: right; padding-top: 10px">
                            <%--<a class="nav-item nav-link active" href="MSMERegistration.aspx" style="color: white;font-size:medium" id="Msme" runat="server" visible="false">MSME Registration<span class="sr-only"></span></a>&nbsp;&nbsp;
                            <a class="nav-item nav-link active" href="Registration.aspx" style="color: white;font-size:medium" id="Pcs" runat="server" visible="false">PCS Regsitration <span class="sr-only"></span></a>&nbsp;&nbsp;--%>
                        </div>
                    </div>
                </nav>
            </div>
        </section>

        <section id="dsp_portals_mainhome" style="padding-top: 32px">
            <div class="container">
                <div class="row">
                    <form id="form1" runat="server">
                        <div class="user_login" style="background-color: transparent">
                            <div class="user_cont" style="border: ghostwhite; border-style: solid; box-shadow: 5px 10px ghostwhite; width: 112%">
                                <h3>user login</h3>
                                <div class="log_space">
                                    <asp:TextBox ID="txtUserName" runat="server" TabIndex="2" placeholder="Enter User Name" class="pwd" Width="95%"></asp:TextBox>
                                </div>
                                <div class="log_space">
                                    <asp:TextBox type="txtPassword" runat="server" TabIndex="3" ID="txtPassword" class="pwd" placeholder="Enter Password" TextMode="Password" Width="95%"></asp:TextBox>
                                </div>
                                <div class="log_space">
                                    <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary" Width="30%" OnClick="btnLogin_Click" />                       
                                </div>
                                <div id="err" style="color: #ff0000; font-size: 12px;"><span id="errLabel" class="alerttext" name="errLabel" runat="server" /></div>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </section>
    </div>
    <section class="icsiFooter">
        <footer>
            <div class="footer">
                <div class="footer_cont">
                    <div class="foot_menu">
                        <p>
                            Best viewed on screen resolution 1024x768 pixels<br />
                            Website best viewed in IE9, Mozila 38.0 and above, Chrome 39.0, Safari 5.0.1
                        </p>
                    </div>
                    <div class="copyright">
                        <p>Copyright © 2020. All Rights Reserved. The Institute of Company Secretaries of India</p>
                        <em>Powered by:
                      <a href="#">
                          <img src="Images/icsi_icon.jpg" alt="logo"></a></em>
                    </div>
                    <asp:Label class="nav-item nav-link active" Style="color: tomato; float: left; padding-top: 20px" ID="lblvisisited" runat="server" Visible="false"></asp:Label>
                    <div class="clear"></div>
                </div>
            </div>
        </footer>
    </section>
</body>
</html>

