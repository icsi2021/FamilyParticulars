<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportDetails.aspx.cs" Inherits="ReportDetails"
    MasterPageFile="~/MainFile.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="scrmanager" runat="server">
    </asp:ScriptManager>

    <script src="JS/jquery-1.10.2.js" type="text/javascript"></script>

    <link href="CSS/datepicker3.css" rel="stylesheet" type="text/css" />

    <script src="JS/bootstrap-datepicker.js" type="text/javascript"></script>

    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css"
        rel="stylesheet" type="text/css" />
    <link href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css"
        rel="stylesheet" type="text/css" />
    <link href="CSS/Site.css" rel="stylesheet" type="text/css" />
    <link href="CSS/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="CSS/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .stylmain
        {
        }
        .styl a
        {
            color: black;
            padding: 12px 16px;
            text-decoration: none;
            display: block;
        }
        .styl a:hover
        {
            background-color: #ddd;
        }
        .stylmain:hover .styl
        {
            display: block;
        }
        .stylmain:hover .styl
        {
            background-color: #3e8e41;
        }
    </style>
    <style type="text/css">
       
        .imgbtns
        {
            margin: 10px 5px 10px 5px;
        }
        .imgbtns2
        {
            margin: -8px 0px 0px 0px;
        }
        .btn
        {
            height: 35px;
            border: none;           
        }
        #TranType, #lblTo
        {
            cursor: pointer;
        }
        footer
        {
            display: none;
        }
        .grdrqr
        {
            background: red;
            color: white;
            border: 1px solid red;
        }
        select, input[type=text], input[type=number]
        {
            border: 1px solid #ccc;
        }
        select:focus, input[type=text]:focus, input[type=number]:focus, textarea:focus
        {
            border: 1px solid #ccc;
        }
    </style>
    <script type="text/javascript">
    function alertMessage1()
    {
    alert("Enter No.")
    }
    </script>
    
    <div class="box-body">
        <label id="TranType" style="padding: 5px; text-align: center; margin-left: 48%">
            Report</label>
        <div id="dvTransType" style="border: 1px solid #03a9f4; text-align: center; background: whitesmoke;">
            <div class="col-md-12">
                <label style="margin-right: 85px">
                    Report:<span style="color: red; font-weight: bolder"></span></label>
                <asp:DropDownList ID="ddlreport" runat="server">                    
                </asp:DropDownList>
            </div>
            <div class="col-md-12">
                <label style="margin-right: 85px">
                    No:<span style="color: red; font-weight: bolder"></span></label>
                <asp:TextBox ID="txtnon" runat="server">
                </asp:TextBox>
            </div>
            <div class="col-md-12">
            <span style="margin-right:100%">
                    <asp:Button ID="btnsearch" runat="server" Style="font-size: smaller" Text="btnsearch" CssClass="btn" OnClick="btnsearch_Click" />
                    </span>
                </div>                
        </div>
    </div>    
</asp:Content>
