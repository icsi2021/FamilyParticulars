<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FamilyDetails.aspx.cs" Inherits="FamilyDetails" MasterPageFile="~/MainFile.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="scrmanager" runat="server">
    </asp:ScriptManager>

    <script src="JS/jquery-1.10.2.js" type="text/javascript"></script>

    <script src="JS/bootstrap.min.js" type="text/javascript"></script>

    <link href="CSS/datepicker3.css" rel="stylesheet" type="text/css" media="all" />

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

    <script type="text/javascript">
        function alertMessage() {
            alert('Saved Successfully..!');
        }
    </script>

    <script type="text/javascript">
        function alertDelete() {
            alert("Deleted Sucessfully..");
        }



    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#myModal").css('display', 'none');
            $("#content").css('display', 'none');
            $("#Preview").css('display', 'none');
            $("#P1Preview").css('display', 'none');
        });
        function alertMessage1() {
            alert("Please Save Family Details , Before Adding Nominee Details.");
        }
        function alertDeleteMessage() {
            alert("First Row Will Not Delete.");
        }
        function Fetch(ssoitemvalue) {
            $.get("Ajax.aspx", { ReqCase: "StatusApiCall", ReqVal: ssoitemvalue }, function (data) {
                if (data != '') {
                    $("#myModal").modal();
                    $("#content").html(data);
                    $("#content").css('display', 'block');
                }
            });
        }
        function Preview(ssoitemvalue) {
            $.get("Ajax.aspx", { ReqCase: "StatusApiCall", ReqVal: ssoitemvalue }, function (data) {
                if (data != '') {
                    $("#Preview").modal();
                    $("#P1Preview").html(data);
                    $("#P1Preview").css('display', 'block');
                }
            });
        }
        function printFunc() {
            var divToPrint = document.getElementById('content');
            var htmlToPrint = '' +
                '<style type="text/css">' +
                'table th, table td {' +
                'border:1px solid #000;' +
                'padding;0.5em;' +
                '}' +
                '</style>';
            htmlToPrint += divToPrint.outerHTML;
            newWin = window.open("");
            newWin.document.write("<h3 align='center'>Report</h3>");
            newWin.document.write(htmlToPrint);
            newWin.print();
            newWin.close();
        }
    </script>

    <script type="text/javascript">
        function UplaodDocs(Values) {
            window.open(Values, "Upload Attach", "status=1,height=400,width=900,resizable=0");
        }
    </script>
    <asp:Label ID="lblrowprovident" runat="server" Visible="false"></asp:Label>
    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-body">
                    <label id="TranType" style="padding: 5px;">
                        EMPLOYEE DETAIL</label>
                    <div id="dvTransType" style="border: 1px solid #03a9f4; padding: 7px 0px 0px 0px; height: 250px; background: whitesmoke;">
                        <div class="col-md-2">
                            <label>
                                Employee Code<span style="color: red; font-weight: bolder">*</span></label>
                            <asp:TextBox ID="txtEMPCode" runat="server" Enabled="false"></asp:TextBox>
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
                    <p>
                    </p>
                    <div id="DIVFamilyDetails">
                        <div style="border: 1px solid #03a9f4; padding: 7px 0px 0px 0px; background: whitesmoke;">
                            <label style="padding: 5px;">
                                FAMILY DETAILS</label>
                            <div class="col-md-12">
                                <div class="box box-primary">
                                    <div class="box-body no-padding">
                                        <asp:UpdatePanel ID="updatefamily" runat="server" UpdateMode="Always">
                                            <ContentTemplate>
                                                <asp:GridView ID="GD_FamilyDetails" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                                    PageSize="30" EmptyDataText="No Records Found" CssClass="table table-hover" ShowHeaderWhenEmpty="true"
                                                    GridLines="None" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                                                    OnRowCreated="GD_FamilyDetails_RowCreated" Width="100%" OnRowCommand="GD_FamilyDetails_RowCommand" OnRowDataBound="GD_FamilyDetails_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" HeaderStyle-Wrap="true" ControlStyle-Font-Size="Smaller"
                                                            HeaderStyle-Font-Size="Smaller">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                                <asp:Label ID="labelrow" runat="server" Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Name of the<br/>Family<br> members" HeaderStyle-Wrap="true"
                                                            ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller" HeaderStyle-Width="20%">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlREL_TYPE_ID" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlREL_TYPE_ID_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Relationship<br/>with the <br/>employee" HeaderStyle-Wrap="true"
                                                            ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller" HeaderStyle-Width="20%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblFML_MEMBER_NAME" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                                                <asp:TextBox ID="TxtotherRel" runat="server" Visible="false"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Whether Date Of Birth (DOB) is actual  or not Date Of Birth of family (Member actual  DOB or Declared  DOB if actual  DOB is not known)"
                                                            HeaderStyle-Wrap="true" ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller" HeaderStyle-Width="30%">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlchkdob" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Text="Yes" Value="1" Selected="True">Yes</asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="0">No</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DOB/Birth Year"
                                                            HeaderStyle-Wrap="true" ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller" HeaderStyle-Width="20%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dobcal" runat="server" data-provide="datepicker" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Monthly<br/>Income PM" HeaderStyle-Wrap="true" ControlStyle-Font-Size="Smaller"
                                                            HeaderStyle-Font-Size="Smaller" HeaderStyle-Width="20%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblMTHLY_INCOME" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Profession<br/>Occupation" HeaderStyle-Wrap="true"
                                                            ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller" HeaderStyle-Width="30%">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="lblOCCUPATION" runat="server" ReadOnly="true" CssClass="form-control">
                                                                    <asp:ListItem Text="Service-Central" Value="1">Service-Central</asp:ListItem>
                                                                    <asp:ListItem Text="Service-State" Value="2">Service-State</asp:ListItem>
                                                                    <asp:ListItem Text="Business" Value="3">Business</asp:ListItem>
                                                                    <asp:ListItem Text="Professional practice" Value="4">Professional practice</asp:ListItem>
                                                                    <asp:ListItem Text="Doctor" Value="5">Doctor</asp:ListItem>
                                                                    <asp:ListItem Text="Engineer" Value="6">Engineer</asp:ListItem>
                                                                    <asp:ListItem Text="Student" Value="7">Student</asp:ListItem>
                                                                    <asp:ListItem Text="HouseWife" Value="8">HouseWife</asp:ListItem>
                                                                    <asp:ListItem Text="Other" Value="9">Other</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Dependent<br/>on Employee<br/>Yes/No" HeaderStyle-Wrap="true"
                                                            ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller" HeaderStyle-Width="20%">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="chkDEPENT_ON_EMP" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Text="Yes" Value="1">Yes</asp:ListItem>
                                                                    <asp:ListItem Selected="True" Text="No" Value="0">No</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Whether staying<br/> with the<br/>employee Yes/No"
                                                            HeaderStyle-Wrap="true" ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller" HeaderStyle-Width="20%">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="lblSTAY_WITH_EMP" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Text="Yes" Value="1">Yes</asp:ListItem>
                                                                    <asp:ListItem Selected="True" Text="No" Value="0">No</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Whether <br/>Spouse is<br/>Working" HeaderStyle-Wrap="true"
                                                            ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="lblSPOUSE_WORKING" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Text="Yes" Value="1">Yes</asp:ListItem>
                                                                    <asp:ListItem Selected="True" Text="No" Value="0">No</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Spouse<br/>Working.In" HeaderStyle-Wrap="true" ControlStyle-Font-Size="Smaller"
                                                            HeaderStyle-Font-Size="Smaller">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblSPOUSE_WORKING_IN" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Whether Spouse is Availing <br/>Medical from his/her office in <br/>respect of the Employee"
                                                            HeaderStyle-Wrap="true" ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="lblSPOUSE_MEDICAL_FROM_OFFICE" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Text="Yes" Value="1">Yes</asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="0" Selected="True">No</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Whether Spouse is Availing <br/>LTC facility from his/her office in <br/>respect of the employee"
                                                            HeaderStyle-Wrap="true" ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="lblSPOUSE_LTC_FROM_OFFICE" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Text="Yes" Value="1">Yes</asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="0" Selected="True">No</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Attachment Reg.<br/>Medical/<br/>LTC Certificate"
                                                            HeaderStyle-Wrap="true" ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnattach" runat="server" Text="Attach" CommandArgument='<%# Container.DataItemIndex %>'
                                                                    CommandName="Attach" Style="border-style: double;" CssClass="form-control" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Del" HeaderStyle-Wrap="true" ControlStyle-Font-Size="Smaller"
                                                            HeaderStyle-Font-Size="Smaller">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnimagedelete" runat="server" ImageUrl="~/Images/Delete.png"
                                                                    Style="margin-top: 0px;" Width="20px" CssClass="imgbtns" ToolTip="Delete Row"
                                                                    OnClick="btnimagedelete_Click" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:ImageButton ID="BtnAddFamilyDetails" runat="server" Visible="true" ImageUrl="~/Images/add.png"
                                                    Style="margin-top: -30px;" Width="20px" CssClass="imgbtns" ToolTip="Add New Row"
                                                    OnClick="BtnAddFamilyDetails_Click" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <p>
                </p>
                <div class="col-md-12">
                    <div class="form-group">
                        <label>
                            &nbsp;</label><div class="input-group">
                                <asp:Button ID="btnpreview" runat="server" Style="font-size: smaller" Width="80px"
                                    Text="Preview" CssClass="btn" OnClick="btnpreview_Click" Visible="false" />&nbsp;&nbsp;

                                <asp:Button ID="btnsubmit" runat="server" CssClass="col-md-4" Style="height: 35px; width: 140px; border-radius: 10px; margin: 5px 5px 0px 0px; color: white; background-color: #083b82; font-size: 20px"
                                    Text="Save" OnClick="btnsubmit_Click" />&nbsp;&nbsp;&nbsp;

                                <asp:Button ID="btnreset" runat="server" Text="Refresh" CssClass="col-md-4" Style="height: 35px; width: 140px; border-radius: 10px; margin: 5px 5px 0px 0px; color: white; background-color: #083b82; font-size: 20px" OnClick="btnreset_Click" />&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnAddnominee" Visible="false" runat="server" Style="font-size: smaller"
                                    Width="150px" Text="Add Nominee Details" CssClass="btn" OnClick="btnAddnominee_Click" />
                                &nbsp;&nbsp;&nbsp;<asp:Button ID="btnreport" runat="server" CssClass="col-md-4" Style="height: 35px; width: 140px; border-radius: 10px; margin: 5px 5px 0px 0px; color: white; background-color: #083b82; font-size: 20px"
                                    Text="Print Report" OnClick="btnreport_Click" />
                            </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <p>
                    <b style="color: Red">Note**</b>
                </p>
                <p>
                    <b>Please click on “Add Nominee Details button” below to fill in the following forms
                                :- </b>
                </p>
                <ul>
                    <li>ICSI Employees Benevolent Fund Form.</li>
                    <li>Form of nomination under the payment of EL/HPL Encashment Amount.</li>
                    <li>Form for Nomination under Payment of Gratuity Act.</li>
                    <li>Employees Provident Fund Form (Applicable to employees who have joined ICSI before
                                01.01.2005).</li>
                    <li>Employees New Pension Fund Trust (Applicable to employees who have joined ICSI after
                                01.01.2005.</li>
                </ul>
            </div>
            <div class="col-md-12" style="display:none">
                <p>
                    <b style="color: Red">Annexure A**</b>
                </p>
                <p>
                    <strong>Definition of&nbsp; Dependents ( for Family Particulars &amp; Benevolent Fund)</strong>
                </p>
                <table width="100%" border="1">
                    <tbody>
                        <tr>
                            <td><strong>Male</strong></td>
                            <td><strong>Female</strong></td>
                        </tr>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td><strong>Married Employee</strong></td>
                                        <td>
                                            <p>
                                                <strong>Parents</strong>if income from all sources &nbsp;&lt; 1500 p.m
                                            </p>
                                            <p>
                                                <strong>Wife </strong>if she is not employed or, if employed, her office does not
                                    provide medical facility or she is not availing her medical facility from her office.
                                            </p>
                                            <p>
                                                <strong>Children :</strong> Maximum 2 dependent children (including step children)
                                    if they are not employed. The term dependant children is defined further, as under:
                                    (1) Son: till he starts earning or attains the age of 25 years or gets married,
                                    whichever is earlier. (2) Daughter : till she starts earning or attains the age
                                    of 25 years or gets married, whichever is earlier.(3) Disabled Son or Daughter suffering
                                    from permanent disability of any kind (physical or mental) : No age limit.
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><strong>Unmarried Employee</strong></td>
                                        <td>
                                            <p>
                                                <strong>Parents </strong>if income from all sources &nbsp;&lt; 1500 p.m
                                            </p>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td><strong>Married Employee</strong></td>
                                        <td>
                                            <p>
                                                Can opt for&nbsp; <strong>either parents or inlaws</strong> if income from all sources
                                    &nbsp;&lt; 1500 p.m
                                            </p>
                                            <p>
                                                <strong>Husband</strong> if he is not employed or , if employed, his company does
                                    not provide medical facility or he is not availing his medical facility from his
                                    office.
                                            </p>
                                            <p>
                                                <strong>Children :</strong> Maximum 2 dependent children (including step children)
                                    if they are not employed. The term dependant children is defined further, as under:
                                    (1) Son: till he starts earning or attains the age of 25 years or gets married,
                                    whichever is earlier. (2) Daughter : till she starts earning or attains the age
                                    of 25 years or gets married, whichever is earlier. (3) Disabled Son or Daughter
                                    suffering from permanent disability of any kind (physical or mental) : No age limit
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><strong>Unmarried Employee</strong></td>
                                        <td>
                                            <p>
                                                Can opt for&nbsp; <strong>either parents or inlaws</strong> if income from all sources
                                    &nbsp;&lt; 1500 p.m
                                            </p>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <p>
                    <strong>&nbsp; Note : </strong>
                </p>
                <ul>
                    <li>Income denotes income from pension also.</li>
                </ul>
                <ul>
                    <li>In case the father&rsquo;s monthly income is less than 1500 p.m, only then the parents
                        (both father &amp; mother) can be shown as the employee&rsquo;s</li>
                </ul>
                <ul>
                    <li>In case the father of the employee is not there, only then the mother can be shown
                        as the employee&rsquo;s dependent provided her monthly income is less than 1500
                        p.m.</li>
                </ul>
            </div>
        </div>
    </div>
    <div class="modal fade" id="myModal" role="dialog">
        <div class="modal-dialog" id="dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                </div>
                <div class="modal-body">
                    <p id="content">
                    </p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                    <button type="button" class="btn btn-default" onclick="printFunc()">
                        Print</button>
                </div>
            </div>
        </div>
    </div>
    <div style="padding-left: 1%">
        <asp:CheckBox ID="ChkConfirm" runat="server" Text=" I hereby declare " />
    </div>
    <div style="width: 100%; text-align: center">
        <asp:Button ID="btn_confirm" runat="server" Style="background-color: #083b82" ForeColor="White"
            Width="150px" Text="Final Confirmation" CssClass="btn" OnClick="btn_confirm_Click" />
    </div>
    <div class="modal fade" id="Preview" role="dialog">
        <div class="modal-dialog" id="P1PreviewDialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                </div>
                <div class="modal-body">
                    <p id="P1Preview">
                    </p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>
    <div id="editor">
    </div>
</asp:Content>
