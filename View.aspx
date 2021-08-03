<%@ Page Title="" Language="C#" MasterPageFile="~/MainFile.master" AutoEventWireup="true"
    CodeFile="View.aspx.cs" Inherits="View" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="scrmanager" runat="server">
    </asp:ScriptManager>

    <script src="JS/jquery-1.10.2.js" type="text/javascript"></script>

    <script src="JS/bootstrap.min.js" type="text/javascript"></script>   

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
    </style>
    <script type="text/javascript">
        var Messagevalue = "";
        $(document).ready(function () {
            $("#myModal").css('display', 'none');
            $("#content").css('display', 'none');
            $('#btnsave').click(function () {
                var ssoitemvalue = Messagevalue + "~" + $('#txtmessage').val();
                $.get("Ajax.aspx", { ReqCase: "SetMessageNew", ReqVal: ssoitemvalue }, function (data) {
                    if (data != '') {
                        window.location.reload();
                    }
                });
            });
            $('#close').click(function () {
                var ssoitemvalue = Messagevalue + "~" + $('#txtmessage').val();
                $.get("Ajax.aspx", { ReqCase: "SetMessageNew", ReqVal: ssoitemvalue }, function (data) {
                    if (data != '') {
                        window.location.reload();
                    }
                });
            });
        });
        function showmessage(value) {
            Messagevalue = value;
            $('#divapprove').modal();
            $('#P1').css('display', 'block');
        }
    </script>

    <div class="content" style="margin: 0 0 13px 0px; padding: 10px 0px 0px 0px">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-body">
                        <label id="TranType" style="padding: 5px;">
                            EMPLOYEE DETAIL</label>
                        <div id="dvTransType" style="border: 1px solid #03a9f4; height: 190px; padding: 7px 0px 0px 0px; background: whitesmoke;">
                            <div class="col-md-2">
                                <label>
                                    Employee Code<span style="color: red; font-weight: bolder"></span>
                                </label>
                                <br />
                                <asp:Label ID="txtEMPCode" runat="server"></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <label>
                                    Employee Name<span style="color: red; font-weight: bolder"></span></label><br />
                                <asp:Label ID="txtName" runat="server"></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <label>
                                    Department<span style="color: red; font-weight: bolder"></span></label><br />
                                <asp:Label ID="txtdepartment" runat="server"></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <label>
                                    Designation<span style="color: red; font-weight: bolder"></span></label><br />
                                <asp:Label ID="txtdesignation" runat="server"></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <label>
                                    Financial Year<span style="color: red; font-weight: bolder"></span></label><br />
                                <asp:Label ID="txtfinancialyear" runat="server"></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <label>
                                    DOB<span style="color: red; font-weight: bolder"></span></label><br />
                                <asp:Label ID="txtDocDate" runat="server" Placeholder="Date"></asp:Label>
                            </div>
                            <%--<div class="col-md-2" visible="false">
                                    <label>R.End.Employment<span style="color: red; font-weight: bolder"></span></label><br />
                                    <asp:Label ID="ddlreasonforendofemployment" runat="server" ></asp:Label>
                        </div>  --%>
                            <div class="col-md-2">
                                <label>
                                    Address<span style="color: red; font-weight: bolder"></span></label>
                                <asp:Label ID="txtpaddress" runat="server"></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <label>
                                    Corr. Address<span style="color: red; font-weight: bolder"></span></label><br />
                                <asp:Label ID="txtcaddress" runat="server"></asp:Label>
                            </div>
                            <%--<div class="col-md-2" visible="false">
                                    <label>Religion<span style="color: red; font-weight: bolder"></span></label><br />
                                    <asp:Label ID="txtreligion" runat="server"></asp:Label>
                        </div>  --%>
                            <div class="col-md-2">
                                <label>
                                    Gender<span style="color: red; font-weight: bolder"></span></label><br />
                                <asp:Label ID="ddlgender" runat="server"></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <label>
                                    Martial Status<span style="color: red; font-weight: bolder"></span></label><br />
                                <asp:Label ID="ddlmaritalstatus" runat="server">                                   
                                </asp:Label>
                            </div>
                            <div class="col-md-2">
                                <label>
                                    Employment Type<span style="color: red; font-weight: bolder"></span></label><br />
                                <asp:Label ID="ddlEmploymentstatus" runat="server"></asp:Label>
                            </div>
                            <%--<div class="col-md-2" visible="false">                                                           
                                    <label>Comments<span style="color: red; font-weight: bolder"></span></label>
                                    <asp:Label ID="txtcomments" runat="server">
                                    </asp:Label>
                        </div>   --%>
                        </div>
                        <p>
                        </p>
                        <div id="DIVFamilyDetails" runat="server">
                            <asp:UpdatePanel ID="UPFamilyDetails" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                <ContentTemplate>
                                    <label style="padding: 5px; border-radius: 2px;">
                                        FAMILY DETAILS</label>
                                    <span style="float: right"><b>Approved</b><asp:CheckBox ID="chkfamilyapproved" runat="server" OnCheckedChanged="chkfamilyapproved_CheckedChanged" AutoPostBack="true" />
                                        <b>Rejected</b><asp:CheckBox ID="chkfamilyreject" runat="server" OnCheckedChanged="chkfamilyreject_CheckedChanged" AutoPostBack="true" />
                                        <b>Hard Copy Received</b><asp:CheckBox ID="chkfamilyhardcopy" runat="server" OnCheckedChanged="chkfamilyhardcopy_CheckedChanged" AutoPostBack="true" />
                                        <asp:Button ID="btnsavefamily" runat="server" Text="Save" Style="color: white; Background: #02646f; border-radius: 10px; width: 90px" OnClick="btnsavefamily_Click" />
                                    </span>
                                    <div style="border: 1px solid #03a9f4; height: 100%; padding: 7px 0px 0px 0px; background: whitesmoke;">
                                        <div class="col-md-12">
                                            <div class="box box-primary">
                                                <div class="box-body no-padding">
                                                    <asp:GridView ID="GD_FamilyDetails" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                                        PageSize="30" EmptyDataText="No Records Found" CssClass="table table-hover" ShowHeaderWhenEmpty="true"
                                                        GridLines="None" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No" HeaderStyle-Wrap="true" HeaderStyle-Width="50px">
                                                                <ItemStyle Width="50px" />
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Family<br/>Member<br/> Name" HeaderStyle-Wrap="true"
                                                                HeaderStyle-Width="50px">
                                                                <ItemStyle Width="50px" />
                                                                <ItemTemplate>
                                                                    <%# Eval("FML_MEMBER_NAME")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Relation<br/>Type" HeaderStyle-Wrap="true" HeaderStyle-Width="50px">
                                                                <ItemStyle Width="50px" />
                                                                <ItemTemplate>
                                                                    <%# Eval("REL_TYPE_ID")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="DOB" HeaderStyle-Wrap="true" HeaderStyle-Width="50px">
                                                                <ItemStyle Width="50px" />
                                                                <ItemTemplate>
                                                                    <%# Eval("DOB")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Monthly<br/>Income" HeaderStyle-Wrap="true" HeaderStyle-Width="50px">
                                                                <ItemStyle Width="50px" />
                                                                <ItemTemplate>
                                                                    <%# Eval("MTHLY_INCOME")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Occupation" HeaderStyle-Wrap="true" HeaderStyle-Width="50px" Visible="false">
                                                                <ItemStyle Width="50px" />
                                                                <ItemTemplate>
                                                                    <%# Eval("OCCUPATION")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Dep<br/>On<br/>Emp" HeaderStyle-Wrap="true" HeaderStyle-Width="50px" Visible="false">
                                                                <ItemStyle Width="50px" />
                                                                <ItemTemplate>
                                                                    <%# Eval("DEPENT_ON_EMP")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Stay<br/>With.Emp" HeaderStyle-Wrap="true" HeaderStyle-Width="50px" Visible="false">
                                                                <ItemStyle Width="50px" />
                                                                <ItemTemplate>
                                                                    <%# Eval("STAY_WITH_EMP")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Spouse<br/>Working" HeaderStyle-Wrap="true" HeaderStyle-Width="50px" Visible="false">
                                                                <ItemStyle Width="50px" />
                                                                <ItemTemplate>
                                                                    <%# Eval("SPOUSE_WORKING")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Spouse<br/>Working.In" HeaderStyle-Wrap="true" HeaderStyle-Width="50px">
                                                                <ItemStyle Width="50px" />
                                                                <ItemTemplate>
                                                                    <%# Eval("SPOUSE_WORKING_IN")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Spouse<br/>Medical<br/>FROM.Off" HeaderStyle-Wrap="true"
                                                                HeaderStyle-Width="50px">
                                                                <ItemStyle Width="50px" />
                                                                <ItemTemplate>
                                                                    <%# Eval("SPOUSE_MEDICAL_FROM_OFFICE")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Spouse<br/>Ltc<br/>Frm.Off" HeaderStyle-Wrap="true"
                                                                HeaderStyle-Width="50px">
                                                                <ItemStyle Width="50px" />
                                                                <ItemTemplate>
                                                                    <%# Eval("SPOUSE_LTC_FROM_OFFICE")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Emp<br/>Code" Visible="false" HeaderStyle-BackColor="#3d8eb9"
                                                                HeaderStyle-ForeColor="White" HeaderStyle-Wrap="true" HeaderStyle-Width="50px">
                                                                <ItemStyle Width="50px" />
                                                                <ItemTemplate>
                                                                    <%# Eval("EMP_CODE")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Attach" Visible="false" HeaderStyle-BackColor="#3d8eb9"
                                                                HeaderStyle-ForeColor="White" HeaderStyle-Wrap="true" HeaderStyle-Width="50px">
                                                                <ItemStyle Width="50px" />
                                                                <ItemTemplate>
                                                                    <asp:Image ID="ImageShow" runat="server" ImageUrl='<%# "Handler.ashx?ImID="+ Eval("EMP_CODE") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <p>
                        </p>
                        <div id="DivProvidentFund" runat="server">
                            <asp:UpdatePanel ID="UPProvidentFund" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                <ContentTemplate>
                                    <label style="padding: 5px; border-radius: 2px;">
                                        Provident Fund</label>
                                    <span style="float: right"><b>Approved</b><asp:CheckBox ID="chkprovidentapproved" runat="server" OnCheckedChanged="chkprovidentapproved_CheckedChanged" AutoPostBack="true" />
                                        <b>Rejected</b><asp:CheckBox ID="chkprovidentrejected" runat="server" OnCheckedChanged="chkprovidentrejected_CheckedChanged" AutoPostBack="true" />
                                        <b>Hard Copy Received</b><asp:CheckBox ID="chkprovidenthardcopy" runat="server" OnCheckedChanged="chkprovidenthardcopy_CheckedChanged" AutoPostBack="true" />
                                        <asp:Button ID="btnprovident" runat="server" Text="Save" Style="color: white; Background: #02646f; border-radius: 10px; width: 90px" OnClick="btnprovident_Click" />
                                    </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<div
                                        style="border: 1px solid #03a9f4; height: 100%; padding: 7px 0px 0px 0px; border-radius: 5px 5px 5px 5px; background: whitesmoke;">
                                        <div class="col-md-12">
                                            <div class="box box-primary">
                                                <div class="box-body no-padding">
                                                    <asp:GridView ID="GD_ProvidentFund" runat="server" Width="100%" AutoGenerateColumns="false"
                                                        AllowPaging="true" PageSize="30" EmptyDataText="No Records Found" ShowHeaderWhenEmpty="true"
                                                        GridLines="None" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                                                        CssClass="table table-hover">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Name<br/> Of Nominee<br/> or Nominees">
                                                                <ItemTemplate>
                                                                    <%# Eval("FML_MEMBER_NAME")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Date<br/> Of Birth <br/>of(mention Year<br/> of Birth<br/> if Exact DOB <br/>is not known)">
                                                                <ItemTemplate>
                                                                    <%# Eval("DOB")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Nominee<br/> Is Minor" Visible="false">
                                                                <ItemTemplate>
                                                                    <%# Eval("ProvidentIsMinor")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Name<br/> of Gurdian<br/> & RelationShip">
                                                                <ItemTemplate>
                                                                    <%# Eval("ProvidentGurdian")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="% of Amount<br/> of Share to <br/>be paid to<br/> each nominee">
                                                                <ItemTemplate>
                                                                    <%# Eval("ProvidentShare")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <p>
                        </p>
                        <div id="DivEmployeePensionTrust" runat="server">
                            <asp:UpdatePanel ID="UPEmployeePensionTrust" runat="server" UpdateMode="Conditional"
                                ChildrenAsTriggers="true">
                                <ContentTemplate>
                                    <label style="padding: 5px; border-radius: 2px;">
                                        Employee Pension Trust</label>
                                    <span style="float: right"><b>Approved</b><asp:CheckBox ID="chkpensionapproved" runat="server" />
                                        <b>Rejected</b><asp:CheckBox ID="chkpensionrejected" runat="server" />
                                        <b>Hard Copy Received</b><asp:CheckBox ID="chkpensionhardcopy" runat="server" />
                                        <asp:Button ID="btnpension" runat="server" Text="Save" Style="color: white; Background: #02646f; border-radius: 10px; width: 90px" OnClick="btnpension_Click" />
                                    </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<div
                                        style="border: 1px solid #03a9f4; height: 100%; padding: 7px 0px 0px 0px; border-radius: 5px 5px 5px 5px; background: whitesmoke;">
                                        <div class="col-md-12">
                                            <div class="box box-primary">
                                                <div class="box-body no-padding">
                                                    <asp:GridView ID="GD_EmployeePensionTrust" runat="server" Width="100%" AutoGenerateColumns="false"
                                                        AllowPaging="true" PageSize="30" EmptyDataText="No Records Found" ShowHeaderWhenEmpty="true"
                                                        GridLines="None" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                                                        CssClass="table table-hover">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Name Of<br/> Nominee <br/>or Nominees">
                                                                <ItemTemplate>
                                                                    <%# Eval("FML_MEMBER_NAME")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Nominee<br/> Relationship<br/> With Employee">
                                                                <ItemTemplate>
                                                                    <%# Eval("REL_TYPE_ID")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Date Of<br/> Birth of<br/>(mention Year <br/>of Birth if <br/>Exact DOB is<br/> not known)">
                                                                <ItemTemplate>
                                                                    <%# Eval("DOB")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="% of Amount<br/> of Share to<br/> be paid to<br/> each nominee">
                                                                <ItemTemplate>
                                                                    <%# Eval("PensionShare")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <p>
                        </p>
                        <div id="DivNewPensionTrust" runat="server">
                            <asp:UpdatePanel ID="UpdatePanelNewPensionTrust" runat="server" UpdateMode="Conditional"
                                ChildrenAsTriggers="true">
                                <ContentTemplate>
                                    <label style="padding: 5px; border-radius: 2px;">
                                        Employee Pension Trust</label>
                                    <span style="float: right"><b>Approved</b><asp:CheckBox ID="chknewpensionapproved"
                                        runat="server" OnCheckedChanged="chknewpensionapproved_CheckedChanged" AutoPostBack="true" />
                                        <b>Rejected</b><asp:CheckBox ID="chknewpensionrejected" runat="server" OnCheckedChanged="chknewpensionrejected_CheckedChanged" AutoPostBack="true" />
                                        <b>Hard Copy Received</b><asp:CheckBox ID="chknewpensionhardcopy" runat="server" OnCheckedChanged="chknewpensionhardcopy_CheckedChanged" AutoPostBack="true" />
                                        <asp:Button ID="btnnewpension" runat="server" Text="Save" Style="color: white; Background: #02646f; border-radius: 10px; width: 90px" OnClick="btnnewpension_Click" />
                                    </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<div
                                        style="border: 1px solid #03a9f4; height: 100%; padding: 7px 0px 0px 0px; border-radius: 5px 5px 5px 5px; background: whitesmoke;">
                                        <div class="col-md-12">
                                            <div class="box box-primary">
                                                <div class="box-body no-padding">
                                                    <asp:GridView ID="gridviewnewpension" runat="server" Width="100%" AutoGenerateColumns="false"
                                                        AllowPaging="true" PageSize="30" EmptyDataText="No Records Found" ShowHeaderWhenEmpty="true"
                                                        GridLines="None" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                                                        CssClass="table table-hover">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Name Of<br/> Nominee <br/>or Nominees">
                                                                <ItemTemplate>
                                                                    <%# Eval("FML_MEMBER_NAME")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Nominee<br/> Relationship<br/> With Employee">
                                                                <ItemTemplate>
                                                                    <%# Eval("REL_TYPE_ID")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Date Of<br/> Birth of<br/>(mention Year <br/>of Birth if <br/>Exact DOB is<br/> not known)">
                                                                <ItemTemplate>
                                                                    <%# Eval("DOB")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="% of Amount<br/> of Share to<br/> be paid to<br/> each nominee">
                                                                <ItemTemplate>
                                                                    <%# Eval("PensionShare")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <p>
                        </p>
                        <div id="DivGratuity" runat="server">
                            <asp:UpdatePanel ID="UPGratuity" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                <ContentTemplate>
                                    <label style="padding: 5px; border-radius: 2px;">
                                        Gratuity</label>
                                    <span style="float: right"><b>Approved</b><asp:CheckBox ID="chkgratuityapproved"
                                        runat="server" OnCheckedChanged="chkgratuityapproved_CheckedChanged" AutoPostBack="true" />
                                        <b>Rejected</b><asp:CheckBox ID="chkgratuityrejected" runat="server" OnCheckedChanged="chkgratuityrejected_CheckedChanged" AutoPostBack="true" />
                                        <b>Hard Copy Received</b><asp:CheckBox ID="chkgratuityhardcopy" runat="server" OnCheckedChanged="chkgratuityhardcopy_CheckedChanged" AutoPostBack="true" />
                                        <asp:Button ID="btngratuity" runat="server" Text="Save" Style="color: white; Background: #02646f; border-radius: 10px; width: 90px" OnClick="btngratuity_Click" />
                                    </span>
                                    <div style="border: 1px solid #03a9f4; height: 100%; padding: 7px 0px 0px 0px; border-radius: 5px 5px 5px 5px; background: whitesmoke;">
                                        <div class="col-md-12">
                                            <div class="box box-primary">
                                                <div class="box-body no-padding">
                                                    <asp:GridView ID="GD_Gratuity" runat="server" AutoGenerateColumns="false" Width="100%"
                                                        AllowPaging="true" PageSize="30" EmptyDataText="No Records Found" ShowHeaderWhenEmpty="true"
                                                        GridLines="None" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                                                        CssClass="table table-hover">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Name Of<br/> Nominee<br/> or Nominees">
                                                                <ItemTemplate>
                                                                    <%# Eval("FML_MEMBER_NAME")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Nominee<br/> Relationship<br/> With Employee">
                                                                <ItemTemplate>
                                                                    <%# Eval("REL_TYPE_ID")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Date Of <br/>Birth of<br/>(mention Year <br/>of Birth <br/>if Exact DOB <br/>is not known)">
                                                                <ItemTemplate>
                                                                    <%# Eval("DOB")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="% of Amount<br/> of Share to <br/>be paid to<br/> each nominee">
                                                                <ItemTemplate>
                                                                    <%# Eval("GratuityShare")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <p>
                        </p>
                        <div id="DivBenevolentFund" runat="server">
                            <asp:UpdatePanel ID="UPBenevolentFund" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                <ContentTemplate>
                                    <label style="padding: 5px; border-radius: 2px;">
                                        BenevolentFund</label>
                                    <span style="float: right"><b>Approved</b><asp:CheckBox ID="chkbenevelentapproved"
                                        runat="server" OnCheckedChanged="chkbenevelentapproved_CheckedChanged" AutoPostBack="true" />
                                        <b>Rejected</b><asp:CheckBox ID="chkbenevolentrejected" runat="server" OnCheckedChanged="chkbenevolentrejected_CheckedChanged" AutoPostBack="true" />
                                        <b>Hard Copy Received</b><asp:CheckBox ID="chkbenevolenthardcopy" runat="server" OnCheckedChanged="chkbenevolenthardcopy_CheckedChanged" AutoPostBack="true" />
                                        <asp:Button ID="btnbenevolent" runat="server" Text="Save" Style="color: white; Background: #02646f; border-radius: 10px; width: 90px" OnClick="btnbenevolent_Click" />
                                    </span>
                                    <div style="border: 1px solid #03a9f4; height: 100%; padding: 7px 0px 0px 0px; border-radius: 5px 5px 5px 5px; background: whitesmoke;">
                                        <div class="col-md-12">
                                            <div class="box box-primary">
                                                <div class="box-body no-padding">
                                                    <asp:GridView ID="GD_BenevolentFund" runat="server" Width="100%" AutoGenerateColumns="false"
                                                        AllowPaging="true" PageSize="30" EmptyDataText="No Records Found" ShowHeaderWhenEmpty="true"
                                                        GridLines="None" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                                                        CssClass="table table-hover">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Name Of<br/> Nominee <br/>or Nominees">
                                                                <ItemTemplate>
                                                                    <%# Eval("FML_MEMBER_NAME")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Nominee<br/> Relationship<br/> With Employee">
                                                                <ItemTemplate>
                                                                    <%# Eval("REL_TYPE_ID")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Date Of<br/> Birth of<br/>(mention Year of <br/>Birth if Exact <br/>DOB is not known)">
                                                                <ItemTemplate>
                                                                    <%# Eval("DOB")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="% of Amount<br/> of Share to <br/>be paid to<br/> each nominee">
                                                                <ItemTemplate>
                                                                    <%# Eval("BenevolentShare")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <p>
                        </p>
                        <div id="DivEnchasment" runat="server">
                            <asp:UpdatePanel ID="UPEncashment" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                <ContentTemplate>
                                    <label style="padding: 5px; border-radius: 2px;">
                                        Encashment</label>
                                    <span style="float: right"><b>Approved</b><asp:CheckBox ID="chkencashmentapproved"
                                        runat="server" OnCheckedChanged="chkencashmentapproved_CheckedChanged" AutoPostBack="true" />
                                        <b>Rejected</b><asp:CheckBox ID="chkencashmentrejected" runat="server" OnCheckedChanged="chkencashmentrejected_CheckedChanged" AutoPostBack="true" />
                                        <b>Hard Copy Received</b><asp:CheckBox ID="chkencashmenthardcopy" runat="server" OnCheckedChanged="chkencashmenthardcopy_CheckedChanged" AutoPostBack="true" />
                                        <asp:Button ID="btnencashment" runat="server" Text="Save" Style="color: white; Background: #02646f; border-radius: 10px; width: 90px" OnClick="btnencashment_Click" />
                                    </span>
                                    <div style="border: 1px solid #03a9f4; height: 100%; padding: 7px 0px 0px 0px; border-radius: 5px 5px 5px 5px; background: whitesmoke;">
                                        <div class="col-md-12">
                                            <div class="box box-primary">
                                                <div class="box-body no-padding">
                                                    <asp:GridView ID="GD_Encashment" runat="server" Width="100%" AutoGenerateColumns="false"
                                                        AllowPaging="true" PageSize="30" EmptyDataText="No Records Found" ShowHeaderWhenEmpty="true"
                                                        GridLines="None" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                                                        CssClass="table table-hover">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Name Of<br/> Nominee <br/>or Nominees">
                                                                <ItemTemplate>
                                                                    <%# Eval("FML_MEMBER_NAME")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Nominee<br/> Relationship<br/> With Employee">
                                                                <ItemTemplate>
                                                                    <%# Eval("REL_TYPE_ID")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Date Of <br/>Birth of<br/>(mention Year<br/> of Birth if <br/>Exact DOB is<br/> not known)">
                                                                <ItemTemplate>
                                                                    <%# Eval("DOB")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="% of Amount<br/> of Share to<br/> be paid to <br/>each nominee">
                                                                <ItemTemplate>
                                                                    <%# Eval("EncashShare")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="divapprove" role="dialog">
        <div class="modal-dialog" id="Div2">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" id="close">
                        &times;</button>
                </div>
                <div class="modal-body">
                    <p id="P1">
                        <b>Enter Approve/Reject Comments</b><textarea cols="60" rows="5" id="txtmessage"
                            name="txtmessage" style="width: 100%"></textarea>
                    </p>
                </div>
                <div class="modal-footer">
                    <input type="button" class="btn btn-default" id="btnsave" value="Submit" />
                </div>
            </div>
        </div>
    </div>
    <div id="editor">
    </div>
</asp:Content>
