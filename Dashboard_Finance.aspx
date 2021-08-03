<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dashboard_Finance.aspx.cs" Inherits="Dashboard_Finance"
    MasterPageFile="~/MainFile.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link rel="stylesheet" type="text/css" href="style.css" media="all" />
    <script src="JS/jquery-1.10.2.js" type="text/javascript"></script>

    <script src="JS/bootstrap.min.js" type="text/javascript"></script>

    <link href="CSS/datepicker3.css" rel="stylesheet" type="text/css" />

    <script src="JS/bootstrap-datepicker.js" type="text/javascript"></script>

    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css"
        rel="stylesheet" type="text/css" />
    <link href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css"
        rel="stylesheet" type="text/css" />
    <link href="CSS/Site.css" rel="stylesheet" type="text/css" />
    <link href="CSS/bootstrap.css" rel="stylesheet" />
    <link href="CSS/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="CSS/datatable.min.css" />

    <script type="text/javascript" charset="utf8" src="CSS/datatable.min.js"></script>

    <style type="text/css">
        .stylmain {
        }

        .styl a {
            color: black;
            padding: 12px 16px;
            text-decoration: none;
            display: block;
        }

            .styl a:hover {
                background-color: #ddd;
            }

        .stylmain:hover .styl {
            display: block;
        }

        .stylmain:hover .styl {
            background-color: #3e8e41;
        }
    </style>
    <style type="text/css">
        input[type="radio"], input[type="checkbox"] {
            margin: 0 9px 0px 18px;
            margin-top: 1px;
            line-height: normal;
        }

        .imgbtns {
            margin: 10px 5px 10px 5px;
        }

        .imgbtns2 {
            margin: -8px 0px 0px 0px;
        }

        .btn {
            height: 35px;
            background: #03a9f4;
            color: White;
            border: none;
        }

        #TranType, #lblTo {
            cursor: pointer;
        }

        footer {
            display: none;
        }

        .grdrqr {
            background: red;
            color: white;
            border: 1px solid red;
        }

        select, input[type=text], input[type=number] {
            border: 1px solid #ccc;
        }

            select:focus, input[type=text]:focus, input[type=number]:focus, textarea:focus {
                background-color: #FFBFFF;
                border: 1px solid #ccc;
            }

        #dialog {
            width: 100%!important;
        }

        .topnav {
            background-color: #02646f;
            overflow: hidden;
            height: 50px;
        }

            /* Style the links inside the navigation bar */
            .topnav a {
                float: left;
                color: #f2f2f2;
                text-align: center;
                padding: 14px 16px;
                text-decoration: none;
                font-size: 17px;
            }

                /* Change the color of links on hover */
                .topnav a:hover {
                    background-color: #ddd;
                    color: black;
                }

                /* Add a color to the active/current link */
                .topnav a.active {
                    background-color: #02646f;
                    color: white;
                }

        label, p, ul, ol, a, blockquote, input, textarea, select, [type=date], [type=text], [type=email], span {
            font-size: 15px;
            line-height: 25px;
            color: #5d5d5d;
        }
    </style>
    <script type="text/javascript">
        function ShowEdit() {
            window.open("Edit.aspx", "ShowEdit", "status=1,height=300,width=900,resizable=0");
        }
        function ResetPassword() {
            window.open("ResetPassword.aspx", "ResetPassword", "status=1,height=300,width=900,resizable=0");
        }
        function MasterRelation() {
            window.open("MasterRelation.aspx", "MasterRelation", "status=1,height=500,width=900,resizable=0");
        }
    </script>

    <div class="col-md-12" style="background-color: Gray;">
        <span class="col-md-2" style="margin-top: 5%">
            <asp:Label ID="lblwelcomemessage" runat="server"></asp:Label></span>
        <div class="col-md-8" style="margin-top: 3%; margin-left: 10%">
            <div style="margin-left: 5%; font-size: x-large; margin-bottom: 5%;"><b>ICSI Family Particulars and Nominee Details.</b></div>
        </div>
        <div class="medium-4 small-12 columns">
            <div class="well box-primary">
                <h3 class="text-white">Family Detalis</h3>
                <h4 class="text-danger">
                    <span class="label label-danger pull-right"><a href="#">
                        <asp:Label ID="lblBen1" runat="server" Text="Pending" ToolTip=""></asp:Label></a></span>
                    <a href="#" class="text-white"><i class="fa fa-chevron-circle-right"></i>Status</a>
                </h4>
                <h4 class="text-danger">
                    <span class="label label-danger pull-right"><a href="#">
                        <asp:Label ID="lblBen2" runat="server" Text="Pending"></asp:Label></a></span>
                    <a href="#" class="text-white"><i class="fa fa-chevron-circle-right"></i>Hard Copy</a>
                </h4>

                <h4 class="text-danger" style="display: none">
                    <span class="label label-danger pull-right"><a href="#">
                        <asp:Label ID="lblBen3" runat="server" Text="Pending"></asp:Label></a></span>
                    <a href="#" class="text-white"><i class="fa fa-chevron-circle-right">Rejected</i></a>
                </h4>
            </div>
        </div>
        <br />
        <div class="medium-4 small-12 columns">
            <div class="well box-warning">
                <h3 class="text-white">Provident Fund</h3>
                <h4 class="text-danger">
                    <span class="label label-danger pull-right"><a href="#">
                        <asp:Label ID="lblBen4" runat="server" Text="Pending"></asp:Label></a></span>
                    <a href="#" class="text-white"><i class="fa fa-chevron-circle-right"></i>Status</a>
                </h4>
                <h4 class="text-danger">
                    <span class="label label-danger pull-right"><a href="#">
                        <asp:Label ID="lblBen5" runat="server" Text="Pending"></asp:Label></a></span>
                    <a href="#" class="text-white"><i class="fa fa-chevron-circle-right"></i>Hard Copy</a>
                </h4>
                <h4 class="text-danger" style="display: none">
                    <span class="label label-danger pull-right"><a href="#">
                        <asp:Label ID="lblBen6" runat="server" Text="Pending"></asp:Label></a></span>
                    <a href="#" class="text-white"><i class="fa fa-chevron-circle-right"></i>Rejected</a>
                </h4>
            </div>
        </div>

        <div class="medium-4 small-12 columns">
            <div class="well box-danger">
                <h3 class="text-white">Employee Pension Trust</h3>
                <h4 class="text-danger">
                    <span class="label label-danger pull-right"><a href="#">
                        <asp:Label ID="lblBen7" runat="server" Text="Pending"></asp:Label></a></span>
                    <a href="#" class="text-white"><i class="fa fa-chevron-circle-right"></i>Status</a>
                </h4>
                <h4 class="text-danger">
                    <span class="label label-danger pull-right"><a href="#">
                        <asp:Label ID="lblBen8" runat="server" Text="Pending"></asp:Label></a></span>
                    <a href="#" class="text-white"><i class="fa fa-chevron-circle-right"></i>Hard Copy</a>
                </h4>
                <h4 class="text-danger" style="display: none">
                    <span class="label label-danger pull-right"><a href="#">
                        <asp:Label ID="lblBen9" runat="server" Text="Pending"></asp:Label></a></span>
                    <a href="#" class="text-white"><i class="fa fa-chevron-circle-right"></i>Rejected</a>
                </h4>
            </div>
        </div>

        <div class="medium-4 small-12 columns">
            <div class="well box-purple">
                <h3 class="text-white">Gratuity</h3>
                <h4 class="text-danger">
                    <span class="label label-danger pull-right"><a href="#">
                        <asp:Label ID="lblBen10" runat="server" Text="Pending"></asp:Label></a></span>
                    <a href="#" class="text-white"><i class="fa fa-chevron-circle-right"></i>Status</a>
                </h4>
                <h4 class="text-danger">
                    <span class="label label-danger pull-right"><a href="#">
                        <asp:Label ID="lblBen11" runat="server" Text="Pending"></asp:Label></a></span>
                    <a href="#" class="text-white"><i class="fa fa-chevron-circle-right"></i>Hard Copy</a>
                </h4>
                <h4 class="text-danger" style="display: none">
                    <span class="label label-danger pull-right"><a href="#">
                        <asp:Label ID="lblBen12" runat="server" Text="Pending"></asp:Label></a></span>
                    <a href="#" class="text-white"><i class="fa fa-chevron-circle-right"></i>Rejected</a>
                </h4>
            </div>
        </div>

        <div class="medium-4 small-12 columns">
            <div class="well box-info">
                <h3 class="text-white">Benevolent Fund</h3>
                <h4 class="text-danger">
                    <span class="label label-danger pull-right"><a href="#">
                        <asp:Label ID="lblBen13" runat="server" Text="Pending"></asp:Label></a></span>
                    <a href="#" class="text-white"><i class="fa fa-chevron-circle-right"></i>Status</a>
                </h4>
                <h4 class="text-danger">
                    <span class="label label-danger pull-right"><a href="#">
                        <asp:Label ID="lblBen14" runat="server" Text="Pending"></asp:Label></a></span>
                    <a href="#" class="text-white"><i class="fa fa-chevron-circle-right"></i>Hard Copy</a>
                </h4>
                <h4 class="text-danger" style="display: none">
                    <span class="label label-danger pull-right"><a href="#">
                        <asp:Label ID="lblBen15" runat="server" Text="Pending"></asp:Label></a></span>
                    <a href="#" class="text-white"><i class="fa fa-chevron-circle-right"></i>Rejected</a>
                </h4>
            </div>
        </div>

        <div class="medium-4 small-12 columns">
            <div class="well box-green">
                <h3 class="text-white">EL/HPL Encashment</h3>
                <h4 class="text-danger">
                    <span class="label label-danger pull-right"><a href="#">
                        <asp:Label ID="lblBen16" runat="server" Text="Pending"></asp:Label></a></span>
                    <a href="#" class="text-white"><i class="fa fa-chevron-circle-right"></i>Status</a>
                </h4>
                <h4 class="text-danger">
                    <span class="label label-danger pull-right"><a href="#">
                        <asp:Label ID="lblBen17" runat="server" Text="Pending"></asp:Label></a></span>
                    <a href="#" class="text-white"><i class="fa fa-chevron-circle-right"></i>Hard Copy</a>
                </h4>
                <h4 class="text-danger" style="display: none">
                    <span class="label label-danger pull-right"><a href="#">
                        <asp:Label ID="lblBen18" runat="server" Text="Pending"></asp:Label></a></span>
                    <a href="#" class="text-white"><i class="fa fa-chevron-circle-right"></i>Rejected</a>
                </h4>
            </div>
        </div>
        <span class="col-md-2"></span>
    </div>
</asp:Content>
