<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddEmployee.aspx.cs" Inherits="AddEmployee"
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
    function alertmessage()
    {
    alert("Something Went Wrong.");
    }
    function alertMessage1()
    {
    alert("Employee Details Save..");
    }
    function Message()
    {    alert("Please Enter Employee Details.");}
    </script>

    <div class="box-body">
        <label id="TranType" style="padding: 5px; text-align: center; margin-left: 48%">
            ADD RETIRED EMPLOYEE</label>
        <div id="dvTransType" style="border: 1px solid #03a9f4; text-align: center; background: whitesmoke;">
           
             <div class="col-md-3">
                <label>
                    Employee Code<span style="color: red; font-weight: bolder">*</span></label>
                <asp:TextBox ID="txtEMPCode" runat="server" Cssclass="form-control email" AutoPostBack="true" OnTextChanged="txtEMPCode_TextChanged"></asp:TextBox>
            </div>
               <div class="col-md-3">
                <label>
                    Employee Name<span style="color: red; font-weight: bolder"></span></label>
                <asp:TextBox ID="txtName" runat="server" Cssclass="form-control email"></asp:TextBox>
            </div>


            <div class="col-md-3">
                <label >
                    Email ID<span style="color: red; font-weight: bolder"></span></label>
                <asp:TextBox ID="txtemaild" runat="server" Cssclass="form-control email"></asp:TextBox>
            </div>


            <div class="col-md-3">
                <label >
                    Department<span style="color: red; font-weight: bolder"></span></label>
                <asp:TextBox ID="txtdepartment" runat="server" Cssclass="form-control email"></asp:TextBox>
            </div>


            <div class="col-md-3">
                <label >
                    Designation<span style="color: red; font-weight: bolder"></span></label>
                <asp:TextBox ID="txtdesignation" runat="server" Cssclass="form-control email"></asp:TextBox>
            </div>


            <div class="col-md-3">
                <label >
                    Financial Year<span style="color: red; font-weight: bolder"></span></label>
                <asp:TextBox ID="txtfinancialyear" runat="server" Cssclass="form-control email"></asp:TextBox>
            </div>


            <div class="col-md-3">
                <label >
                    Date Of Birth<span style="color: red; font-weight: bolder"></span></label><asp:TextBox
                        ID="txtDocDate" runat="server" data-provide="datepicker" Cssclass="form-control email" Width="550px"></asp:TextBox>
                </div>


                <div class="col-md-3">
                    <label>
                        Reason for EOE<span style="color: red; font-weight: bolder"></span></label>
                    <asp:TextBox ID="ddlreasonforendofemployment" runat="server" Cssclass="form-control email"></asp:TextBox>
                </div>

               
                <div class="col-md-3">
                    <label >
                        Religion<span style="color: red; font-weight: bolder"></span></label>
                    <asp:TextBox ID="txtreligion" runat="server" Cssclass="form-control email"></asp:TextBox>
                </div>


                <div class="col-md-3">
                    <label >
                        Gender<span style="color: red; font-weight: bolder"></span></label>
                    <asp:DropDownList ID="ddlgender" runat="server" Cssclass="form-control email">
                        <asp:ListItem Selected="True" Text="MALE" Value="MALE">MALE</asp:ListItem>
                        <asp:ListItem Text="FEMALE" Value="FEMALE">FEMALE</asp:ListItem>
                    </asp:DropDownList>
                </div>


                <div class="col-md-3">
                    <label>
                        Marital Status <span style="color: red; font-weight: bolder"></span>
                    </label>
                    <asp:DropDownList ID="ddlmaritalstatus" runat="server" Cssclass="form-control email">
                        <asp:ListItem Selected="True" Text="Married" Value="Married">Married</asp:ListItem>
                        <asp:ListItem Text="Single" Value="Single">Single</asp:ListItem>
                        <asp:ListItem Text="Divorced" Value="Divorced">Divorced</asp:ListItem>
                        <asp:ListItem Text="Widow" Value="Widow">Widow</asp:ListItem>
                    </asp:DropDownList>
                </div>


                <div class="col-md-3">
                    <label >
                        Employment Type<span style="color: red; font-weight: bolder"></span></label>
                        <asp:DropDownList ID="ddlEmploymentstatus" runat="server" Cssclass="form-control email">
                        <asp:ListItem Selected="True" Text="Retired" Value="Employee">Employee</asp:ListItem>
                        <asp:ListItem Text="On Probation" Value="On Probation">On Probation</asp:ListItem>
                        <asp:ListItem Text="Retired" Value="Retired">Retired</asp:ListItem>
                        <asp:ListItem Text="Confirmed in the Institute" Value="Confirmed in the Institute">Confirmed in the Institute</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="col-md-3">
                    <label >
                        Blood Group<span style="color: red; font-weight: bolder"></span></label>
                    <asp:TextBox ID="txtBlood" runat="server" Cssclass="form-control email"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <label >
                        Mobile Number<span style="color: red; font-weight: bolder"></span></label>
                    <asp:TextBox ID="TxtMoble" runat="server" Cssclass="form-control email"></asp:TextBox>
                </div>

                 <div class="col-md-3">
                    <label >
                        Permanent Address<span style="color: red; font-weight: bolder"></span></label>
                    <asp:TextBox ID="txtpaddress" runat="server" TextMode="MultiLine" Cssclass="form-control email"></asp:TextBox>
                </div>


                <div class="col-md-3">
                    <label >
                        Present Address<span style="color: red; font-weight: bolder"></span></label>
                    <asp:TextBox ID="txtcaddress" runat="server" TextMode="MultiLine" Cssclass="form-control email"></asp:TextBox>
                </div>


                <div class="col-md-3">
                    <label >
                        Comments<span style="color: red; font-weight: bolder"></span></label>
                    <asp:TextBox ID="txtcomments" runat="server" TextMode="MultiLine" Cssclass="form-control email">
                    </asp:TextBox>
                </div>
            </div>
        </div>
        <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
        <div class="col-md-3">
            <div class="form-group">
                <label>
                    &nbsp;</label><div class="input-group">
                        <asp:Button ID="btnsubmit" runat="server" CssClass="col-md-3" Style="height:35px; width:140px; border-radius:10px; margin:5px 5px 0px 0px; color:white;background-color:#02646f; font-size:20px"
                            Text="Save" OnClick="btnsubmit_Click" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btclose" runat="server" Text="Close" CssClass="col-md-3" Style="height:35px; width:140px; border-radius:10px; margin:5px 5px 0px 0px; color:white;background-color:#02646f; font-size:20px" OnClick="btnclose_Click" />
                    </div>
            </div>
        </div>
    
</asp:Content>
