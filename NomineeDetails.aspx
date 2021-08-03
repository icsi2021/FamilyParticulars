<%@ Page Title="Nominee Details" Language="C#" MasterPageFile="~/MainFile.master"
    AutoEventWireup="true" CodeFile="NomineeDetails.aspx.cs" Inherits="NomineeDetails" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="scrmanager" runat="server">
    </asp:ScriptManager>
    <script src="JS/jquery-1.10.2.js" type="text/javascript"></script>
    <script src="JS/bootstrap.min.js" type="text/javascript"></script>
    <link href="CSS/datepicker3.css" rel="stylesheet" type="text/css" media="all" />
    <script src="JS/bootstrap-datepicker.js" type="text/javascript"></script>
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css" rel="stylesheet" type="text/css" />
    <link href="CSS/Site.css" rel="stylesheet" type="text/css" />
    <link href="CSS/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="CSS/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .stylmain {
        }

        #dialog, #P1PreviewDialog {
            width: 100%!important;
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
        $(document).ready(function () {
            $("#myModal").css('display', 'none');
            $("#content").css('display', 'none');
            $("#Preview").css('display', 'none');
            $("#P1Preview").css('display', 'none');
        });
        function alertMessage() {
            alert('Saved Successfully..!');
        }
        function alertMessageSave() {
            alert("Enter Value Properly..");
        }
    </script>

    <script type="text/javascript">
        function alertDelete() {
            alert("Deleted Sucessfully..");
        }
    </script>

    <script type="text/javascript">
        function Notification() {
            alert("Member Name Already Added..");
        }
    </script>

    <script type="text/javascript">
        function PercentageMessage() {
            alert("Please Enter Total Share Less then 100 %");
        }
        function Fetch(ssoitemvalue) {
            $.get("Ajax.aspx", { ReqCase: "StatusApiCall", ReqVal: ssoitemvalue }, function (data) {
                if (data != '') {
                    $("#myModal").modal();
                    $("#content").html(data);
                    $("#content").css('display', 'block');
                }
                else {

                    alert("No Record Found.");
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
                else {
                    alert("No Record Found.");
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
    <div class="content" style="margin: 0 0 13px 0px; padding: 10px 0px 0px 0px">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-body">
                        <div id="DivProvidentFund" runat="server" style="border-bottom: solid 1px #eeee; border-top: solid 1px #eeee; padding-top: 1%;">
                            <label style="padding: 5px;">
                                Provident Fund</label>
                            <div style="border: 1px solid #03a9f4; height: 100%; padding: 7px 0px 0px 0px; background: whitesmoke;" class="col-md-12">
                                <div class="col-md-12">
                                    <div class="box box-primary">
                                        <div class="box-body no-padding">
                                            <asp:UpdatePanel ID="UpdateProvident" runat="server" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <asp:GridView ID="GD_ProvidentFund" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                                        PageSize="30" EmptyDataText="No Records Found" ShowHeaderWhenEmpty="true" GridLines="None"
                                                        AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" Width="100%" OnRowDataBound="GD_ProvidentFund_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No" ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                    <asp:Label ID="lblrowprovident" runat="server" Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Name<br/> Of Nominee<br/> or Nominees" ControlStyle-Font-Size="Smaller"
                                                                HeaderStyle-Font-Size="Smaller">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="GD_ProvidentFundtxtname" runat="server" ReadOnly="true" AutoPostBack="true"
                                                                        OnSelectedIndexChanged="GD_ProvidentFundtxtname_SelectedIndexChanged" CssClass="form-control">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Nominee<br/> Relationship<br/> With Employee" ControlStyle-Font-Size="Smaller"
                                                                HeaderStyle-Font-Size="Smaller">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtrelationship" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Date<br/> Of Birth <br/>of(mention Year<br/> of Birth<br/> if Exact DOB <br/>is not known)"
                                                                ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="dobcal" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="If the nominee<br/>is a minor,<br/>state the name<br/>of the guardian<br/>and the relationship<br/>with the member"
                                                                ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="chkisminor" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="chkisminor_SelectedIndexChanged">
                                                                        <asp:ListItem Text="Yes" Value="1">Yes</asp:ListItem>
                                                                        <asp:ListItem Selected="True" Text="No" Value="0">No</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Name<br/> of Gurdian<br/> & RelationShip" ControlStyle-Font-Size="Smaller"
                                                                HeaderStyle-Font-Size="Smaller">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtnameofgurdian" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Percentage Share to each Nominee"
                                                                ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtpercentage" runat="server" Text="0" AutoPostBack="true" onkeypress="return event.charCode >= 48 && event.charCode <= 57"
                                                                        MaxLength="3" CssClass="form-control"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Del" HeaderStyle-Wrap="true" HeaderStyle-Width="50px"
                                                                ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                                                <ItemStyle Width="50px" />
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnimagedeleteProvident" runat="server" ImageUrl="~/Images/Delete.png"
                                                                        Style="margin-top: 0px;" Width="20px" CssClass="imgbtns" ToolTip="Delete Row"
                                                                        OnClick="btnimagedeleteProvident_Click" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:ImageButton ID="BtnAddProvident" runat="server" Visible="true" ImageUrl="~/Images/add.png"
                                                        Width="20px" CssClass="imgbtns" ToolTip="Add New Row" OnClick="BtnAddProvident_Click" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <asp:Button ID="btnprovidentpreview" runat="server" Text="Preview"
                                    CssClass="col-md-4" Style="height: 35px; width: 140px; border-radius: 10px; margin: 5px 5px 0px 0px; color: white; background-color: #083b82; font-size: 20px" OnClick="btnprovidentpreview_Click" />

                                <asp:Button ID="btnprovidentrefresh" runat="server" Text="Refresh"
                                    CssClass="col-md-4" Style="height: 35px; width: 140px; border-radius: 10px; margin: 5px 5px 0px 0px; color: white; background-color: #083b82; font-size: 20px" OnClick="btnprovidentrefresh_Click" />

                                <asp:Button ID="btnprovidnet" runat="server" Text="Save Provident"
                                    CssClass="col-md-4" Style="height: 35px; width: 200px; border-radius: 10px; margin: 5px 5px 0px 0px; color: white; background-color: #083b82; font-size: 20px" OnClick="btnprovidnet_Click" />

                                <asp:Button ID="btnprovidentprint" runat="server" Text="Print"
                                    CssClass="col-md-4" Style="height: 35px; width: 140px; border-radius: 10px; margin: 5px 5px 0px 0px; color: white; background-color: #083b82; font-size: 20px" OnClick="btnprovidentprint_Click" />
                                <button type="button" class="col-md-4" data-toggle="collapse" data-target="#contenttextProvident"
                                    style="height: 35px; width: 140px; border-radius: 10px; margin: 5px 5px 0px 0px; color: white; background-color: #083b82; font-size: 20px">
                                    Information</button>
                            </div>
                            <br />
                            <br />
                            <div class="col-md-12 collapse" id="contenttextProvident">
                                <b style="color: Red">Note**</b>
                                <ul>
                                    <li>Spouse, legitimate children, step children, parents, 
                                        sisters and minor brothers who are wholly dependent on the member</li>
                                </ul>
                            </div>
                        </div>
                        <div id="DivEmployeePensionTrust" runat="server">
                            <label style="padding: 5px;">
                                Employee Pension Trust</label>
                            <div style="border: 1px solid #03a9f4; height: 100%; padding: 7px 0px 0px 0px; background: whitesmoke;" class="col-md-12">
                                <div class="col-md-12">
                                    <div class="box box-primary">
                                        <div class="box-body no-padding">
                                            <asp:UpdatePanel ID="UpdatePension" runat="server" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <asp:GridView ID="GD_EmployeePensionTrust" runat="server" AutoGenerateColumns="false"
                                                        AllowPaging="true" PageSize="30" EmptyDataText="No Records Found" ShowHeaderWhenEmpty="true"
                                                        GridLines="None" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                                                        Width="100%" OnRowDataBound="GD_EmployeePensionTrust_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No" ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                    <asp:Label ID="lblrowpension" runat="server" Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Name Of<br/> Nominee <br/>or Nominees" ControlStyle-Font-Size="Smaller"
                                                                HeaderStyle-Font-Size="Smaller">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="GD_EmployeePensionTrusttxtname" runat="server" ReadOnly="true"
                                                                        AutoPostBack="true" OnSelectedIndexChanged="GD_EmployeePensionTrusttxtname_SelectedIndexChanged" CssClass="form-control">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Nominee<br/> Relationship<br/> With Employee" ControlStyle-Font-Size="Smaller"
                                                                HeaderStyle-Font-Size="Smaller">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtrelationship" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Date Of<br/> Birth of<br/>(mention Year <br/>of Birth if <br/>Exact DOB is<br/> not known)"
                                                                ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="dobcal" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Percentage Share to each Nominee"
                                                                ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtpercentagePension" runat="server" Text="0" AutoPostBack="true"
                                                                        OnTextChanged="txtpercentagePension_TextChanged" MaxLength="3" CssClass="form-control"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Del" HeaderStyle-BackColor="#3d8eb9" HeaderStyle-ForeColor="White"
                                                                HeaderStyle-Wrap="true" HeaderStyle-Width="50px" ControlStyle-Font-Size="Smaller"
                                                                HeaderStyle-Font-Size="Smaller">
                                                                <ItemStyle Width="50px" />
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnimagedeletePensiontrust" runat="server" ImageUrl="~/Images/Delete.png"
                                                                        Style="margin-top: 0px;" Width="20px" CssClass="imgbtns" ToolTip="Delete Row"
                                                                        OnClick="btnimagedeletePensiontrust_Click" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:ImageButton ID="btnaddpensiontrust" runat="server" Visible="true" ImageUrl="~/Images/add.png"
                                                        Width="20px" CssClass="imgbtns" ToolTip="Add New Row" OnClick="btnaddpensiontrust_Click" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <asp:Button ID="btnpensionpreview" runat="server" CssClass="col-md-4" Style="height: 35px; width: 140px; border-radius: 10px; margin: 5px 5px 0px 0px; color: white; background-color: #083b82; font-size: 20px" Text="Preview"
                                    OnClick="btnpensionpreview_Click" />
                                <asp:Button ID="btnpensionrefresh" runat="server" CssClass="col-md-4" Style="height: 35px; width: 140px; border-radius: 10px; margin: 5px 5px 0px 0px; color: white; background-color: #083b82; font-size: 20px" Text="Refresh"
                                    OnClick="btnpensionrefresh_Click" />
                                <asp:Button ID="btnPension" runat="server" Text="Save Pension" Width="200px"
                                    CssClass="col-md-4" Style="height: 35px; width: 140px; border-radius: 10px; margin: 5px 5px 0px 0px; color: white; background-color: #083b82; font-size: 20px" OnClick="btnPension_Click" />

                                <asp:Button ID="btnpensionprint" runat="server" Text="Print"
                                    CssClass="col-md-4" Style="height: 35px; width: 140px; border-radius: 10px; margin: 5px 5px 0px 0px; color: white; background-color: #083b82; font-size: 20px" OnClick="btnpensionprint_Click" />
                            </div>
                            <br />
                            <br />
                            <div class="col-md-12">
                                <p>
                                    <b style="color: Red">Note**</b>
                                </p>
                                <br />
                                <ul>
                                    <li>"Family" Shall mean the Employee and his/her spouse.</li>
                                </ul>
                            </div>
                        </div>
                        <div id="DivNewPensionTrust" runat="server" style="border-bottom: solid 1px #eeee; border-top: solid 1px #eeee; padding-top: 1%">
                            <label style="padding: 5px;">
                                New Pension Trust</label>
                            <div style="border: 1px solid #03a9f4; height: 100%; padding: 7px 0px 0px 0px; background: whitesmoke;" class="col-md-12">
                                <div class="col-md-12">
                                    <div class="box box-primary">
                                        <div class="box-body no-padding">
                                            <asp:UpdatePanel ID="UpdateNewPensionTrust" runat="server" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <asp:GridView ID="GridViewNewPensionTrust" runat="server" AutoGenerateColumns="false"
                                                        AllowPaging="true" PageSize="30" EmptyDataText="No Records Found" ShowHeaderWhenEmpty="true"
                                                        GridLines="None" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                                                        Width="100%" OnRowDataBound="GridViewNewPensionTrust_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No" ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                    <asp:Label ID="lblrowpension" runat="server" Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Name Of<br/> Nominee <br/>or Nominees" ControlStyle-Font-Size="Smaller"
                                                                HeaderStyle-Font-Size="Smaller">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="GD_EmployeeNewPensionTrusttxtname" runat="server" ReadOnly="true"
                                                                        AutoPostBack="true" OnSelectedIndexChanged="GD_EmployeeNewPensionTrusttxtname_SelectedIndexChanged" CssClass="form-control">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Nominee<br/> Relationship<br/> With Employee" ControlStyle-Font-Size="Smaller"
                                                                HeaderStyle-Font-Size="Smaller">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtrelationship" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Date Of<br/> Birth of<br/>(mention Year <br/>of Birth if <br/>Exact DOB is<br/> not known)"
                                                                ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="dobcal" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Percentage Share to each Nominee"
                                                                ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtpercentageNewPension" runat="server" Text="0" onkeypress="return event.charCode >= 48 && event.charCode <= 57" AutoPostBack="true"
                                                                        MaxLength="3" CssClass="form-control"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Del" HeaderStyle-ForeColor="Black" HeaderStyle-Wrap="true"
                                                                HeaderStyle-Width="50px" ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                                                <ItemStyle Width="50px" />
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnimagedeleteNewPensiontrust" runat="server" ImageUrl="~/Images/Delete.png"
                                                                        Style="margin-top: 0px;" Width="20px" CssClass="imgbtns" ToolTip="Delete Row"
                                                                        OnClick="btnimagedeleteNewPensiontrust_Click" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:ImageButton ID="ImageButtonNewPensionTrust" runat="server" Visible="true" ImageUrl="~/Images/add.png"
                                                        Width="20px" CssClass="imgbtns" ToolTip="Add New Row" OnClick="ImageButtonNewPensionTrust_Click" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <asp:Button ID="btnNewPensionTrustPreview" runat="server" CssClass="col-md-4" Style="height: 35px; width: 140px; border-radius: 10px; margin: 5px 5px 0px 0px; color: white; background-color: #083b82; font-size: 20px"
                                    Text="Preview" OnClick="btnNewPensionTrustPreview_Click" />
                                <asp:Button ID="btnNewPensionRefresh" runat="server" Text="Refresh"
                                    CssClass="col-md-4" Style="height: 35px; width: 140px; border-radius: 10px; margin: 5px 5px 0px 0px; color: white; background-color: #083b82; font-size: 20px" OnClick="btnNewPensionRefresh_Click" />

                                <asp:Button ID="btnNewPensionTrust" runat="server" Text="Save Pension"
                                    CssClass="col-md-4" Style="height: 35px; width: 180px; border-radius: 10px; margin: 5px 5px 0px 0px; color: white; background-color: #083b82; font-size: 20px" OnClick="btnNewPensionTrust_Click" />

                                <asp:Button ID="btnNewPensionTrustPrint" runat="server" CssClass="col-md-4" Style="height: 35px; width: 140px; border-radius: 10px; margin: 5px 5px 0px 0px; color: white; background-color: #083b82; font-size: 20px"
                                    Text="Print" OnClick="btnNewPensionTrustPrint_Click" />
                                <button type="button" class="col-md-4" data-toggle="collapse" data-target="#contenttext"
                                    style="height: 35px; width: 140px; border-radius: 10px; margin: 5px 5px 0px 0px; color: white; background-color: #083b82; font-size: 20px">
                                    Information</button>
                            </div>
                            <br />
                            <br />
                            <div class="col-md-12 collapse" id="contenttext">
                                <p>
                                    <b style="color: Red">Annexure A**</b>
                                </p>
                                <p>
                                    <strong>Definition of&nbsp; Dependents ( for Family Particulars &amp; Benevolent Fund)</strong>
                                </p>
                                <table width="100%" border="solid 1px">
                                    <tbody>
                                        <tr>
                                            <td width="66"></td>
                                            <td width="343">
                                                <p>
                                                    <strong>Married Employee</strong>
                                                </p>
                                            </td>
                                            <td width="322">
                                                <p>
                                                    <strong>Unmarried Employee</strong>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="66">
                                                <p>
                                                    Male
                                                </p>
                                            </td>
                                            <td width="343">
                                                <p>
                                                    <strong>Parents</strong>if income from all sources &nbsp;&lt; 4500 p.m
                                                </p>
                                                <p>
                                                    <strong>Wife </strong>if she is not employed or, if employed, her office does not
                                                     provide medical facility or she is not availing her medical/LTC facility from her office.
                                                </p>
                                                <p>
                                                    <strong>Children :</strong> Maximum 2 dependent children.
                                                Maximum 2 dependent children. The term dependent children is 
                                                defined further, as under Son: till he starts earning or attains the age of 25 
                                                years or gets married, whichever is earlier. (2) Daughter: till she starts earning or attains 
                                                the age of 25 years or gets married, whichever is earlier 
                                                (3) married daughters who have been divorced, abandoned or separated 
                                                from their husbands and widowed daughters and are residing with the 
                                                employee and are wholly dependent on the employee 
                                                (4) Disabled Son or Daughter suffering from permanent 
                                                disability of any kind (physical or mental); No age limit.
                                                </p>
                                            </td>
                                            <td width="310">
                                                <p>
                                                    <strong>Parents </strong>if income from all sources &nbsp;&lt; 4500 p.m
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="66">
                                                <p>
                                                    Female
                                                </p>
                                            </td>
                                            <td width="343">
                                                <p>
                                                    Can opt for&nbsp; <strong>either parents or inlaws</strong> if income from all sources
                                                 &nbsp;&lt; 4500 p.m
                                                </p>
                                                <p>
                                                    <strong>Husband</strong> if he is not employed or , if employed, his company does
                                                     not provide medical/LTC facility or he is not availing his medical facility from his
                                                     office.
                                                </p>
                                                <p>
                                                    <strong>Children :</strong> Maximum 2 dependent children. 
                                                The term dependent children is defined further, as under Son: 
                                                till he starts earning or attains the age of 25 years or gets married, whichever is earlier. 
                                                (2) Daughter: till she starts earning or attains the age of 25 years 
                                                or gets married, whichever is earlier 
                                                (3) married daughters who have been divorced, abandoned or 
                                                separated from their husbands and widowed daughters and are 
                                                residing with the employee and are wholly dependent on the employee 
                                                (4) Disabled Son or Daughter suffering from permanent disability 
                                                of any kind (physical or mental); No age limit
                                                </p>
                                            </td>
                                            <td width="310">
                                                <p>
                                                    Can opt for&nbsp; <strong>either parents or inlaws</strong> if income from all sources
                                                 &nbsp;&lt; 4500 p.m
                                                </p>
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
                                    <li>In case the father&rsquo;s monthly income is less than 4500 p.m, only then the parents
                                        (both father &amp; mother) can be shown as the employee&rsquo;s dependents.</li>
                                </ul>
                                <ul>
                                    <li>In case the father of the employee is not there, only then the mother can be shown
                                         as the employee&rsquo;s dependent provided her monthly income is less than 4500
                                         p.m.</li>
                                </ul>
                            </div>
                        </div>
                        <div id="DivGratuity" style="border-bottom: solid 1px #eeee; border-top: solid 1px #eeee; padding-top: 1%">
                            <label style="padding: 5px;">
                                Gratuity</label>
                            <div style="border: 1px solid #03a9f4; height: 100%; padding: 7px 0px 0px 0px; background: whitesmoke;" class="col-md-12">
                                <div class="col-md-12">
                                    <div class="box box-primary">
                                        <div class="box-body no-padding">
                                            <asp:UpdatePanel ID="UpdateGratuity" runat="server" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <asp:GridView ID="GD_Gratuity" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                                        PageSize="30" EmptyDataText="No Records Found" ShowHeaderWhenEmpty="true" GridLines="None"
                                                        AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" Width="100%" OnRowDataBound="GD_Gratuity_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No" ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                    <asp:Label ID="lblrowgratuity" runat="server" Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Name Of<br/> Nominee<br/> or Nominees" ControlStyle-Font-Size="Smaller"
                                                                HeaderStyle-Font-Size="Smaller">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="GD_Gratuitytxtname" runat="server" ReadOnly="true" AutoPostBack="true"
                                                                        OnSelectedIndexChanged="GD_Gratuitytxtname_SelectedIndexChanged" CssClass="form-control">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Nominee<br/> Relationship<br/> With Employee" ControlStyle-Font-Size="Smaller"
                                                                HeaderStyle-Font-Size="Smaller">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtrelationship" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Date Of <br/>Birth of<br/>(mention Year <br/>of Birth <br/>if Exact DOB <br/>is not known)"
                                                                ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="dobcal" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Percentage Share to each Nominee"
                                                                ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtpercentageGratuity" runat="server" Text="0" onkeypress="return event.charCode >= 48 && event.charCode <= 57" AutoPostBack="true"
                                                                        MaxLength="3" CssClass="form-control"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Del" HeaderStyle-Wrap="true" HeaderStyle-Width="50px"
                                                                ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                                                <ItemStyle Width="50px" />
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnimagedeleteGratuity" runat="server" ImageUrl="~/Images/Delete.png"
                                                                        Style="margin-top: 0px;" Width="20px" CssClass="imgbtns" ToolTip="Delete Row"
                                                                        OnClick="btnimagedeleteGratuity_Click" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:ImageButton ID="btnaddgratuity" runat="server" Visible="true" ImageUrl="~/Images/add.png"
                                                        Width="20px" CssClass="imgbtns" ToolTip="Add New Row" OnClick="btnaddgratuity_Click" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <asp:Button ID="btngratuitypreview" runat="server" Text="Preview" CssClass="col-md-4" Style="height: 35px; width: 140px; border-radius: 10px; margin: 5px 5px 0px 0px; color: white; background-color: #083b82; font-size: 20px"
                                    OnClick="btngratuitypreview_Click" />
                                <asp:Button ID="btngratuityrefresh" runat="server" CssClass="col-md-4" Style="height: 35px; width: 140px; border-radius: 10px; margin: 5px 5px 0px 0px; color: white; background-color: #083b82; font-size: 20px" Text="Refresh"
                                    OnClick="btngratuityrefresh_Click" />
                                <asp:Button ID="btngratuity" runat="server" Text="Save Gratuity" CssClass="col-md-4" Style="height: 35px; width: 160px; border-radius: 10px; margin: 5px 5px 0px 0px; color: white; background-color: #083b82; font-size: 20px"
                                    OnClick="btngratuity_Click" />
                                <asp:Button ID="btngratuityprint" runat="server" CssClass="col-md-4" Style="height: 35px; width: 140px; border-radius: 10px; margin: 5px 5px 0px 0px; color: white; background-color: #083b82; font-size: 20px" Text="Print"
                                    OnClick="btngratuityprint_Click" />
                                <button type="button" class="col-md-4" data-toggle="collapse" data-target="#contenttextGratuity"
                                    style="height: 35px; width: 140px; border-radius: 10px; margin: 5px 5px 0px 0px; color: white; background-color: #083b82; font-size: 20px">
                                    Information</button>
                            </div>
                            <br />
                            <br />
                            <div class="col-md-12 collapse" id="contenttextGratuity">
                                <p>
                                    <b style="color: Red">Annexure B**</b>
                                </p>
                                <p>
                                    <strong>FOR THE PURPOSE OF GRATUITY RULE “FAMILY” MEANS</strong>
                                </p>
                                <table width="100%">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <b>1.</b>&nbsp;In the case of a male employee, himself, his wife, his children,<br />
                                                whether married or unmarried his dependent parents and the dependent parents of
                                                <br />
                                                his wife and the widow and children of his predeceased son, if any;
                                                <br />
                                                The dependent parents shall have the same meaning as defined in Chapter II of Service Rules.
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>2.</b>&nbsp;In the case of a female employee, herself, her husband, her children,
                                                <br />
                                                whether married or unmarried, her dependent parents and the dependent parents of her
                                                <br />
                                                husband and the widow and children of her predeceased son, if any. 
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>EXPLANATION:</b>&nbsp;Where the proposal law of an employee permits the adoption
                                                by him of a child any child lawfully<br />
                                                adopted by him shall be deemed to be included in his family where a child of an
                                                employee has been adopted by<br />
                                                another person and such adoption is,under the personal law of person making such
                                                adoption lawfully,<br />
                                                Such child shall be deemed to be excluded from the family of employee.
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div id="DivBenevolentFund" style="border-bottom: solid 1px #eeee; border-top: solid 1px #eeee; padding-top: 1%">
                            <label style="padding: 5px;">
                                BenevolentFund</label>
                            <div style="border: 1px solid #03a9f4; height: 100%; padding: 7px 0px 0px 0px; background: whitesmoke;" class="col-md-12">
                                <div class="col-md-12">
                                    <div class="box box-primary">
                                        <div class="box-body no-padding">
                                            <asp:UpdatePanel ID="updatebenevolent" runat="server" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <asp:GridView ID="GD_BenevolentFund" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                                        PageSize="30" EmptyDataText="No Records Found" ShowHeaderWhenEmpty="true" GridLines="None"
                                                        AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" Width="100%" OnRowDataBound="GD_BenevolentFund_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No" ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                    <asp:Label ID="lblrowbenevolent" runat="server" Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Name Of<br/> Nominee <br/>or Nominees" ControlStyle-Font-Size="Smaller"
                                                                HeaderStyle-Font-Size="Smaller">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="GD_BenevolentFundtxtname" runat="server" ReadOnly="true" AutoPostBack="true"
                                                                        OnSelectedIndexChanged="GD_BenevolentFundtxtname_SelectedIndexChanged" CssClass="form-control">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Nominee<br/> Relationship<br/> With Employee" ControlStyle-Font-Size="Smaller"
                                                                HeaderStyle-Font-Size="Smaller">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtrelationship" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Date Of<br/> Birth of<br/>(mention Year of <br/>Birth if Exact <br/>DOB is not known)"
                                                                ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="dobcal" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Percentage Share to each Nominee"
                                                                ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtpercentshareBenevolent" runat="server" Text="0" onkeypress="return event.charCode >= 48 && event.charCode <= 57" AutoPostBack="true"
                                                                        MaxLength="3" CssClass="form-control"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Del" HeaderStyle-Wrap="true" HeaderStyle-Width="50px"
                                                                ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                                                <ItemStyle Width="50px" />
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnimagedeletebenevolent" runat="server" ImageUrl="~/Images/Delete.png"
                                                                        Style="margin-top: 0px;" Width="20px" CssClass="imgbtns" ToolTip="Delete Row"
                                                                        OnClick="btnimagedeletebenevolent_Click" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:ImageButton ID="btnaddbenevolent" runat="server" Visible="true" ImageUrl="~/Images/add.png"
                                                        Width="20px" CssClass="imgbtns" ToolTip="Add New Row" OnClick="btnaddbenevolent_Click" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <asp:Button ID="btnbenevolentpreview" runat="server" CssClass="col-md-4" Style="height: 35px; width: 140px; border-radius: 10px; margin: 5px 5px 0px 0px; color: white; background-color: #083b82; font-size: 20px" Text="Preview"
                                    OnClick="btnbenevolentpreview_Click" />
                                <asp:Button ID="btnbenevolentrefresh" runat="server" CssClass="col-md-4" Style="height: 35px; width: 140px; border-radius: 10px; margin: 5px 5px 0px 0px; color: white; background-color: #083b82; font-size: 20px" Text="Refresh"
                                    OnClick="btnbenevolentrefresh_Click" />
                                <asp:Button ID="btnbenevolent" runat="server" Text="Save Benevolent"
                                    CssClass="col-md-4" Style="height: 35px; width: 200px; border-radius: 10px; margin: 5px 5px 0px 0px; color: white; background-color: #083b82; font-size: 20px" OnClick="btnbenevolent_Click" />
                                <asp:Button ID="btnbenevolentprint" runat="server" CssClass="col-md-4" Style="height: 35px; width: 140px; border-radius: 10px; margin: 5px 5px 0px 0px; color: white; background-color: #083b82; font-size: 20px" Text="Print"
                                    OnClick="btnbenevolentprint_Click" />
                                <button type="button" class="col-md-4" data-toggle="collapse" data-target="#contenttextbenevolent"
                                    style="height: 35px; width: 140px; border-radius: 10px; margin: 5px 5px 0px 0px; color: white; background-color: #083b82; font-size: 20px">
                                    Information</button>
                            </div>
                            <br />
                            <br />
                            <div class="col-md-12 collapse" id="contenttextbenevolent">
                                <div class="form-group">
                                    <label>
                                        <b style="color: Red">Note**</b></label><br />
                                    <ul>
                                        <li>Spouse & Dependent Children of employees</li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div id="DivEnchasment" style="border-bottom: solid 1px #eeee; border-top: solid 1px #eeee; padding-top: 1%">
                            <label style="padding: 5px;">
                                EL/HPL Encashment</label>
                            <div style="border: 1px solid #03a9f4; height: 100%; padding: 7px 0px 0px 0px; background: whitesmoke;" class="col-md-12">
                                <div class="col-md-12">
                                    <div class="box box-primary">
                                        <div class="box-body no-padding">
                                            <asp:UpdatePanel ID="updateenashment" runat="server" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <asp:GridView ID="GD_Encashment" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                                        PageSize="30" EmptyDataText="No Records Found" ShowHeaderWhenEmpty="true" GridLines="None"
                                                        AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" Width="100%" OnRowDataBound="GD_Encashment_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No" ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                    <asp:Label ID="lblrowencashment" runat="server" Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Name Of<br/> Nominee <br/>or Nominees" ControlStyle-Font-Size="Smaller"
                                                                HeaderStyle-Font-Size="Smaller">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="GD_Encashmenttxtname" runat="server" ReadOnly="True" AutoPostBack="true"
                                                                        OnSelectedIndexChanged="GD_Encashmenttxtname_SelectedIndexChanged" CssClass="form-control">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Nominee<br/> Relationship<br/> With Employee" ControlStyle-Font-Size="Smaller"
                                                                HeaderStyle-Font-Size="Smaller">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtrelationship" runat="server" ReadOnly="True" CssClass="form-control"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Date Of <br/>Birth of<br/>(mention Year<br/> of Birth if <br/>Exact DOB is<br/> not known)"
                                                                ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="dobcal" runat="server" ReadOnly="True" CssClass="form-control"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Percentage Share to each Nominee"
                                                                ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtpercentageEncahsment" runat="server" Text="0" onkeypress="return event.charCode >= 48 && event.charCode <= 57" AutoPostBack="true"
                                                                        MaxLength="3" CssClass="form-control"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Del" HeaderStyle-Wrap="true" HeaderStyle-Width="50px"
                                                                ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                                                <ItemStyle Width="50px" />
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnimagedeleteencashment" runat="server" ImageUrl="~/Images/Delete.png"
                                                                        Width="20px" CssClass="imgbtns" ToolTip="Delete Row" OnClick="btnimagedeleteencashment_Click" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:ImageButton ID="btnaddencashment" runat="server" Visible="true" ImageUrl="~/Images/add.png"
                                                        Width="20px" CssClass="imgbtns" ToolTip="Add New Row" OnClick="btnaddencashment_Click" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <asp:Button ID="btnencashPreview" runat="server" CssClass="col-md-4" Style="height: 35px; width: 140px; border-radius: 10px; margin: 5px 5px 0px 0px; color: white; background-color: #083b82; font-size: 20px" Text="Preview"
                                    OnClick="btnencashPreview_Click" />
                                <asp:Button ID="btnencashrefresh" runat="server" CssClass="col-md-4" Style="height: 35px; width: 140px; border-radius: 10px; margin: 5px 5px 0px 0px; color: white; background-color: #083b82; font-size: 20px" Text="Refresh"
                                    OnClick="btnencashrefresh_Click" />
                                <asp:Button ID="btnsubmit" runat="server" CssClass="col-md-4" Style="height: 35px; width: 200px; border-radius: 10px; margin: 5px 5px 0px 0px; color: white; background-color: #083b82; font-size: 20px" Text="Save Encashment"
                                    OnClick="btnsubmit_Click" />
                                <asp:Button ID="btnencashprint" runat="server" CssClass="col-md-4" Style="height: 35px; width: 140px; border-radius: 10px; margin: 5px 5px 0px 0px; color: white; background-color: #083b82; font-size: 20px" Text="Print"
                                    OnClick="btnencashprint_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="Employeeclub" style="text-align: center">
        <asp:Button ID="btnclubpreview" runat="server" Style="background-color: #083b82" ForeColor="White" Text="Consolidated View"
            CssClass="btn" OnClick="btnclubpreview_Click" />
        <asp:Button ID="btnclubprint" runat="server" Style="background-color: #083b82" ForeColor="White" Text="Consolidated Print"
            CssClass="btn" OnClick="btnclubprint_Click" />
    </div>
    <div style="padding-left: 1%">
        <asp:CheckBox ID="ChkConfirm" runat="server" Text=" I hereby Declare." />
    </div>
    <div style="width: 100%; text-align: center">
        <asp:Button ID="btn_confirm" runat="server" Style="background-color: #083b82" ForeColor="White"
            Width="150px" Text="Final Confirmation" CssClass="btn" OnClick="btn_confirm_Click" />
    </div>
    <div class="modal fade" id="myModal" role="dialog">
        <div class="modal-dialog" id="dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">Report</h4>
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
