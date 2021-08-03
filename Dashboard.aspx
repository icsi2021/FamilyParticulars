<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Dashboard"
    MasterPageFile="~/MainFile.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link rel="stylesheet" type="text/css" href="style.css" media="all" />
    <link href="CSS/Site.css" rel="stylesheet" type="text/css" />
    <link href="CSS/bootstrap.css" rel="stylesheet" />
    <link href="CSS/bootstrap.min.css" rel="stylesheet" />

    <style type="text/css">
        label {
            font-weight: normal;
        }

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
        label, p, ul, ol, a, blockquote, input, textarea, select, [type=date], [type=text], [type=email], span {
            font-size: 1.3rem !important;
            line-height: 25px;
            color: #5d5d5d;
        }

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
            background-color: #083b82;
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
                    background-color: #083b82;
                    color: white;
                }
    </style>
    <style type="text/css">       
        #dialog,#P1PreviewDialog
        {
        width:100%!important;
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
        #<%=txtcaddress.ClientID %>,#<%=txtpaddress.ClientID %>
        {
        margin:0px;
        height:110px;
        width:100%;
        overflow-y:hidden;
        overflow-x:hidden;
        resize:none;
        }
        
    </style>
    <style type="text/css">
        .imgbtns {
            margin: 10px 5px 10px 5px;
        }

        .imgbtns2 {
            margin: -8px 0px 0px 0px;
        }

        .btn {
            height: 35px;
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
                border: 1px solid #ccc;
            }
    </style>
    <style type="text/css">
        body {
            font-family: Arial;
        }

        /* Style the tab */
        .tab {
            overflow: hidden;
            border: 1px solid #ccc;
            background-color: #f1f1f1;
        }

            /* Style the buttons inside the tab */
            .tab button {
                background-color: inherit;
                float: left;
                border: none;
                outline: none;
                cursor: pointer;
                padding: 14px 16px;
                transition: 0.3s;
                font-size: 17px;
            }

                /* Change background color of buttons on hover */
                .tab button:hover {
                    background-color: #ddd;
                }

                /* Create an active/current tablink class */
                .tab button.active {
                    background-color: #ccc;
                }

        /* Style the tab content */
        .tabcontent {
            display: none;
            padding: 6px 12px;
            border: 1px solid #ccc;
            border-top: none;
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
    <script type="text/javascript">
        function openDetails(evt, details) {
            var i, tabcontent, tablinks;
            tabcontent = document.getElementsByClassName("tabcontent");
            for (i = 0; i < tabcontent.length; i++) {
                tabcontent[i].style.display = "none";
            }
            tablinks = document.getElementsByClassName("tablinks");
            for (i = 0; i < tablinks.length; i++) {
                tablinks[i].className = tablinks[i].className.replace(" active", "");
            }
            document.getElementById(details).style.display = "block";

            evt.currentTarget.className += " active";
        }
    </script>
    <div class="col-md-12" style="background-color: #D3D3D3;"></div>
    <span class="col-md-2" style="margin-top: 1%">
        <asp:Label ID="lblwelcomemessage" runat="server"></asp:Label></span>
    <div class="col-md-8" style="width: 100%">
        <label id="TranType" style="padding: 1px; text-align: left; font-size: x-large">
            Employee Detail</label><br />
        <div id="dvTransType" style="border: 1px solid #03a9f4; padding: 7px 0px 0px 0px; height: 220px; background: whitesmoke;">
            <div class="col-md-2">
                <label>
                    Employee Code<span style="color: red; font-weight: bolder">*</span></label>
                <asp:TextBox ID="txtEMPCode" runat="server" Enabled="false"></asp:TextBox>
                <asp:Label ID="lblrowprovident" runat="server" Visible="false"></asp:Label>
            </div>
            <div class="col-md-2">
                <label>
                    Employee Name<span style="color: red; font-weight: bolder"></span></label>
                <asp:TextBox ID="txtName" runat="server" Enabled="false"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <label>
                    Dept<span style="color: red; font-weight: bolder"></span></label>
                <asp:TextBox ID="txtdepartment" runat="server" Enabled="false"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <label>
                    Designation<span style="color: red; font-weight: bolder"></span></label>
                <asp:TextBox ID="txtdesignation" runat="server" Enabled="false"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <label>
                    Financial Year<span style="color: red; font-weight: bolder"></span></label>
                <asp:TextBox ID="txtfinancialyear" runat="server" Enabled="false"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <label>
                    Date Of Birth<span style="color: red; font-weight: bolder"></span></label>
                <asp:TextBox ID="txtDocDate" runat="server" Enabled="false"></asp:TextBox>
            </div>
            <div class="col-md-2" style="display: none">
                <label>
                    Reason for EOE<span style="color: red; font-weight: bolder"></span></label>
                <asp:TextBox ID="ddlreasonforendofemployment" runat="server" Enabled="false"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <label>
                    Permanent Address<span style="color: red; font-weight: bolder"></span></label>
                <asp:TextBox ID="txtpaddress" runat="server" Enabled="false" TextMode="MultiLine"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <label>
                    Present Address<span style="color: red; font-weight: bolder"></span></label>
                <asp:TextBox ID="txtcaddress" runat="server" Enabled="false" TextMode="MultiLine"></asp:TextBox>
            </div>
            <div class="col-md-2" style="display: none">
                <label>
                    Religion<span style="color: red; font-weight: bolder"></span></label>
                <asp:TextBox ID="txtreligion" runat="server" Enabled="false"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <label>
                    Gender<span style="color: red; font-weight: bolder"></span></label>
                <asp:TextBox ID="ddlgender" runat="server" Enabled="false">                                          
                </asp:TextBox>
            </div>
            <div class="col-md-2">
                <label>
                    Marital Status <span style="color: red; font-weight: bolder"></span>
                </label>
                <asp:TextBox ID="ddlmaritalstatus" runat="server" Enabled="false">                                       
                </asp:TextBox>
            </div>
            <div class="col-md-2">
                <label>
                    Employment Type<span style="color: red; font-weight: bolder"></span></label>
                <asp:TextBox ID="ddlEmploymentstatus" runat="server" Enabled="false">
                </asp:TextBox>
            </div>

            <div class="col-md-2">
                <label>
                    Blood Group<span style="color: red; font-weight: bolder"></span></label>
                <asp:TextBox ID="txtBlood" runat="server" Enabled="false">
                </asp:TextBox>
            </div>
            <div class="col-md-2">
                <label>
                    Mobile<span style="color: red; font-weight: bolder"></span></label>
                <asp:TextBox ID="txtMobile" runat="server" Enabled="false">
                </asp:TextBox>
            </div>
            <div class="col-md-2">
                <label>
                    Bank Name<span style="color: red; font-weight: bolder"></span></label>
                <asp:TextBox ID="txtbankname" runat="server" Enabled="false">
                </asp:TextBox>
            </div>
            <div class="col-md-2">
                <label>
                    Account No<span style="color: red; font-weight: bolder"></span></label>
                <asp:TextBox ID="txtaccountno" runat="server" Enabled="false">
                </asp:TextBox>
            </div>
            <div class="col-md-2">
                <label>
                    IFSC Code<span style="color: red; font-weight: bolder"></span></label>
                <asp:TextBox ID="txtifsc" runat="server" Enabled="false">
                </asp:TextBox>
            </div>
            <div class="col-md-2" style="display: none">
                <label>
                    Comments<span style="color: red; font-weight: bolder"></span></label>
                <asp:TextBox ID="txtcomments" runat="server" Enabled="false">
                </asp:TextBox>
            </div>
        </div>
        <div style="font-size: medium;"><b>ICSI Family Particulars and Nominee Details.</b></div>
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
    <br />
    <div>
        <div class="tab" align="center">
            <input type="button" class="tablinks" Style="background-color: #083b82; color : White;" onclick="openDetails(event, 'Family',this)" value="Family Detalis" />
            <input type="button" class="tablinks" Style="background-color: #083b82; color : White;" onclick="openDetails(event, 'Provident',this)" value="Provident Fund" />
            <input type="button" class="tablinks" Style="background-color: #083b82; color : White;" onclick="openDetails(event, 'Employee',this)" value="Employee Pension Trust" />
            <input type="button" class="tablinks" Style="background-color: #083b82; color : White;" onclick="openDetails(event, 'Gratuity',this)" value="Gratuity Details" />
            <input type="button" class="tablinks" Style="background-color: #083b82; color : White;" onclick="openDetails(event, 'Benevolent',this)" value="Benevolent Fund Details" />
            <input type="button" class="tablinks" Style="background-color: #083b82; color : White;" onclick="openDetails(event, 'Encashment',this)" value="EL/HPL Encashment Details" />
        </div>
        <div id="Family" class="tabcontent">
            <asp:GridView ID="GvSaleRecordDetails" runat="server" Width="100%"
                BackColor="White" BorderColor="#000" BorderStyle="None" EmptyDataText="No Data Found" BorderWidth="1px"
                CellPadding="3" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" ShowHeader="true" Style="text-align: center;">
                <PagerStyle CssClass="cssPager" />
                <Columns>
                    <asp:TemplateField HeaderText="Product Code" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblProductCode" runat="server" Text='<%# Eval("RowNumber")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText=" Name">
                        <ItemTemplate>
                            <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("FML_MEMBER_NAME")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Relation">
                        <ItemTemplate>
                            <asp:Label ID="lblProductRate" runat="server" Text='<%# Eval("REL_TYPE_ID") %>'></asp:Label>

                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="DOB">

                        <ItemTemplate>
                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("DOB")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Emp Code">
                        <ItemTemplate>
                            <asp:Label ID="lblTaxRate" runat="server" Text='<%# Eval("EMP_CODE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Monthly Income">

                        <ItemTemplate>
                            <asp:Label ID="lblTaxAmount" runat="server" Text='<%# Eval("MTHLY_INCOME")%>'></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Occupation">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalAmount" runat="server" Text='<%# Eval("OCCUPATION") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="ModiDate">

                        <ItemTemplate>
                            <asp:Label ID="lblIGST" runat="server" Text='<%# Eval("ModiDate")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">

                        <ItemTemplate>
                            <asp:Label ID="lblIGST" runat="server" Text='<%#Eval("Status").ToString()=="1"?"ACTIVE":"INACTIVE"%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
                <FooterStyle BackColor="Black" ForeColor="#000066" />
                <HeaderStyle BackColor="#083b82" Font-Bold="False" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="left" />
                <RowStyle ForeColor="#000000" />
                <%--ForeColor="#000066"--%>
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <%--  <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />--%>
            </asp:GridView>
        </div>
        <div id="Provident" class="tabcontent">
            <asp:GridView ID="GvSaleRecordDetails1" runat="server" Width="100%"
                BackColor="White" BorderColor="#000" BorderStyle="None" EmptyDataText="No Data Found" BorderWidth="1px"
                CellPadding="3" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" ShowHeader="true" Style="text-align: center;">
                <PagerStyle CssClass="cssPager" />
                <Columns>
                    <asp:TemplateField HeaderText="Product Code" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblProductCode" runat="server" Text='<%# Eval("RowNumber")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText=" Name">
                        <ItemTemplate>
                            <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("FML_MEMBER_NAME")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Relation">
                        <ItemTemplate>
                            <asp:Label ID="lblProductRate" runat="server" Text='<%# Eval("REL_TYPE_ID") %>'></asp:Label>

                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DOB">
                        <ItemTemplate>
                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("DOB")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Emp Code">
                        <ItemTemplate>
                            <asp:Label ID="lblTaxRate" runat="server" Text='<%# Eval("EMP_CODE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%-- <asp:TemplateField HeaderText="Monthly Income" >                         
                        <ItemTemplate>
                            <asp:Label ID="lblTaxAmount" runat="server" Text='<%# Eval("MTHLY_INCOME")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> --%>

                    <asp:TemplateField HeaderText="ModiDate">

                        <ItemTemplate>
                            <asp:Label ID="lblIGST" runat="server" Text='<%# Eval("ModiDate")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Provident Share">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalAmount" runat="server" Text='<%# Eval("ProvidentShare") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Status">

                        <ItemTemplate>
                            <asp:Label ID="lblIGST" runat="server" Text='<%#Eval("Status").ToString()=="1"?"ACTIVE":"INACTIVE"%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#083b82" Font-Bold="False" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="left" />
                <RowStyle ForeColor="#000000" />
                <%--ForeColor="#000066"--%>
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <%--<SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />--%>
            </asp:GridView>
        </div>
        <div id="Employee" class="tabcontent">
            <asp:GridView ID="GvSaleRecordDetails2" runat="server" Width="100%"
                BackColor="White" BorderColor="#000" BorderStyle="None" EmptyDataText="No Data Found" BorderWidth="1px"
                CellPadding="3" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" ShowHeader="true" Style="text-align: center;">
                <PagerStyle CssClass="cssPager" />
                <Columns>
                    <asp:TemplateField HeaderText="Product Code" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblProductCode" runat="server" Text='<%# Eval("RowNumber")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText=" Name">
                        <ItemTemplate>
                            <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("FML_MEMBER_NAME")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Relation">
                        <ItemTemplate>
                            <asp:Label ID="lblProductRate" runat="server" Text='<%# Eval("REL_TYPE_ID") %>'></asp:Label>

                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="DOB">

                        <ItemTemplate>
                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("DOB")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Emp Code">
                        <ItemTemplate>
                            <asp:Label ID="lblTaxRate" runat="server" Text='<%# Eval("EMP_CODE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%-- <asp:TemplateField HeaderText="Monthly Income" >                         
                        <ItemTemplate>
                            <asp:Label ID="lblTaxAmount" runat="server" Text='<%# Eval("MTHLY_INCOME")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> --%>

                    <asp:TemplateField HeaderText="ModiDate">

                        <ItemTemplate>
                            <asp:Label ID="lblIGST" runat="server" Text='<%# Eval("ModiDate")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Pension Share">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalAmount" runat="server" Text='<%# Eval("PensionShare") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Status">

                        <ItemTemplate>
                            <asp:Label ID="lblIGST" runat="server" Text='<%#Eval("Status").ToString()=="1"?"ACTIVE":"INACTIVE"%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#083b82" Font-Bold="False" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="left" />
                <RowStyle ForeColor="#000000" />
                <%--ForeColor="#000066"--%>
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <%--<SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />--%>
            </asp:GridView>
        </div>
        <div id="Gratuity" class="tabcontent">
            <asp:GridView ID="GvSaleRecordDetails3" runat="server" Width="100%"
                BackColor="White" BorderColor="#000" BorderStyle="None" EmptyDataText="No Data Found" BorderWidth="1px"
                CellPadding="3" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" ShowHeader="true" Style="text-align: center;">
                <PagerStyle CssClass="cssPager" />
                <Columns>
                    <asp:TemplateField HeaderText="Product Code" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblProductCode" runat="server" Text='<%# Eval("RowNumber")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText=" Name">
                        <ItemTemplate>
                            <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("FML_MEMBER_NAME")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Relation">
                        <ItemTemplate>
                            <asp:Label ID="lblProductRate" runat="server" Text='<%# Eval("REL_TYPE_ID") %>'></asp:Label>

                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="DOB">

                        <ItemTemplate>
                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("DOB")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Emp Code">
                        <ItemTemplate>
                            <asp:Label ID="lblTaxRate" runat="server" Text='<%# Eval("EMP_CODE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%-- <asp:TemplateField HeaderText="Monthly Income" >                         
                        <ItemTemplate>
                            <asp:Label ID="lblTaxAmount" runat="server" Text='<%# Eval("MTHLY_INCOME")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> --%>

                    <asp:TemplateField HeaderText="ModiDate">

                        <ItemTemplate>
                            <asp:Label ID="lblIGST" runat="server" Text='<%# Eval("ModiDate")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Gratuity Share">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalAmount" runat="server" Text='<%# Eval("GratuityShare") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Status">

                        <ItemTemplate>
                            <asp:Label ID="lblIGST" runat="server" Text='<%#Eval("Status").ToString()=="1"?"ACTIVE":"INACTIVE"%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#083b82" Font-Bold="False" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="left" />
                <RowStyle ForeColor="#000000" />
                <%--ForeColor="#000066"--%>
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <%--<SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />--%>
            </asp:GridView>
        </div>
        <div id="Benevolent" class="tabcontent">
            <asp:GridView ID="GvSaleRecordDetails4" runat="server" Width="100%"
                BackColor="White" BorderColor="#000" BorderStyle="None" EmptyDataText="No Data Found" BorderWidth="1px"
                CellPadding="3" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" ShowHeader="true" Style="text-align: center;">
                <PagerStyle CssClass="cssPager" />
                <Columns>
                    <asp:TemplateField HeaderText="Product Code" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblProductCode" runat="server" Text='<%# Eval("RowNumber")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText=" Name">
                        <ItemTemplate>
                            <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("FML_MEMBER_NAME")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Relation">
                        <ItemTemplate>
                            <asp:Label ID="lblProductRate" runat="server" Text='<%# Eval("REL_TYPE_ID") %>'></asp:Label>

                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="DOB">

                        <ItemTemplate>
                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("DOB")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Emp Code">
                        <ItemTemplate>
                            <asp:Label ID="lblTaxRate" runat="server" Text='<%# Eval("EMP_CODE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%-- <asp:TemplateField HeaderText="Monthly Income" >                         
                        <ItemTemplate>
                            <asp:Label ID="lblTaxAmount" runat="server" Text='<%# Eval("MTHLY_INCOME")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> --%>

                    <asp:TemplateField HeaderText="ModiDate">

                        <ItemTemplate>
                            <asp:Label ID="lblIGST" runat="server" Text='<%# Eval("ModiDate")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Benevolent Share">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalAmount" runat="server" Text='<%# Eval("BenevolentShare") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Status">

                        <ItemTemplate>
                            <asp:Label ID="lblIGST" runat="server" Text='<%#Eval("Status").ToString()=="1"?"ACTIVE":"INACTIVE"%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#083b82" Font-Bold="False" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="left" />
                <RowStyle ForeColor="#000000" />
                <%--ForeColor="#000066"--%>
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
        </div>
        <div id="Encashment" class="tabcontent">
            <asp:GridView ID="GvSaleRecordDetails5" runat="server" Width="100%"
                BackColor="White" BorderColor="#000" BorderStyle="None" EmptyDataText="No Data Found" BorderWidth="1px"
                CellPadding="3" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" ShowHeader="true" Style="text-align: center;">
                <PagerStyle CssClass="cssPager" />
                <Columns>
                    <asp:TemplateField HeaderText="Product Code" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblProductCode" runat="server" Text='<%# Eval("RowNumber")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText=" Name">
                        <ItemTemplate>
                            <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("FML_MEMBER_NAME")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Relation">
                        <ItemTemplate>
                            <asp:Label ID="lblProductRate" runat="server" Text='<%# Eval("REL_TYPE_ID") %>'></asp:Label>

                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="DOB">

                        <ItemTemplate>
                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("DOB")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Emp Code">
                        <ItemTemplate>
                            <asp:Label ID="lblTaxRate" runat="server" Text='<%# Eval("EMP_CODE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%-- <asp:TemplateField HeaderText="Monthly Income" >                         
                        <ItemTemplate>
                            <asp:Label ID="lblTaxAmount" runat="server" Text='<%# Eval("MTHLY_INCOME")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> --%>

                    <asp:TemplateField HeaderText="ModiDate">

                        <ItemTemplate>
                            <asp:Label ID="lblIGST" runat="server" Text='<%# Eval("ModiDate")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Encash Share">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalAmount" runat="server" Text='<%# Eval("EncashShare") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Status">

                        <ItemTemplate>
                            <asp:Label ID="lblIGST" runat="server" Text='<%#Eval("Status").ToString()=="1"?"ACTIVE":"INACTIVE"%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#083b82" Font-Bold="False" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="left" />
                <RowStyle ForeColor="#000000" />
                <%--ForeColor="#000066"--%>
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <%--<SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />--%>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
