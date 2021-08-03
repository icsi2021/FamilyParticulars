<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="Edit"
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
     #ct100_btnback{display:none;}
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
    function Alertmessage()
    {
    alert("SuccessFully Changed.");
    } 
    function Notification()
    {
    alert("Something Went Wrong.");
    } 
    function ErrorAlert()
    {
    alert("Enter Employee Code");
    }   
    </script>

    <div class="box-body">
        <label id="TranType" style="padding: 5px; text-align: center; margin-left: 48%">Edit/Non Edit</label>            
            <div id="dvTransType" style="border: 1px solid #03a9f4; text-align: center; background: whitesmoke;">
                <div class="col-md-12">
                    <label style="margin-right: 30px">
                        Employment Type<span style="color: red; font-weight: bolder"></span></label>
                    <asp:DropDownList ID="ddlemptype" runat="server">
                        <asp:ListItem Selected="True" Text="Retired" Value="All">All</asp:ListItem>
                        <asp:ListItem Text="Single" Value="Single">Single</asp:ListItem>
                    </asp:DropDownList>
                    <label style="margin-right: 30px">
                        Edit Type<span style="color: red; font-weight: bolder"></span></label>
                    <asp:DropDownList ID="ddledit" runat="server">
                        <asp:ListItem Selected="True" Text="Yes" Value="Yes">Yes</asp:ListItem>
                        <asp:ListItem Text="No" Value="No">No</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-12">
                    <label style="margin-right: 85px">
                        EMP Code<span style="color: red; font-weight: bolder"></span></label>
                    <asp:TextBox ID="txtempcode" runat="server">
                    </asp:TextBox>
                </div>
            </div>
            <div class="col-md-12">
                <div class="form-group" style="margin-left: 50%">
                    <label>
                        &nbsp;</label><div class="input-group">
                            <asp:Button ID="btnsubmit" runat="server" Style="font-size: smaller" Width="80px"
                                Text="Save" CssClass="btn" OnClick="btnsubmit_Click" />
                        </div>
                </div>
            </div>
    </div>
</asp:Content>
