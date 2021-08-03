<%@ Page Title="" Language="C#" MasterPageFile="~/MainFile.master" AutoEventWireup="true"
    CodeFile="AdminDetails.aspx.cs" Inherits="AdminDetails" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
            width: 100% !important;
        }
    </style>

    <script type="text/javascript">
        var Messagevalue = "";
        $(document).ready(function () {

            $("#<%=gridUserDetails.ClientID%>").prepend($("<thead></thead>").append($("#<%=gridUserDetails.ClientID%>").find("tr:first"))).dataTable({ "scrollY": "1000px", "scrollCollapse": true, "paging": true, });

            $("#myModal").css('display', 'none');
            $("#content").css('display', 'none');
            $('#<%=gridUserDetails.ClientID %>').DataTable();
            $('#btnsave').click(function () {
                var mess = Messagevalue.split('~');
                //if ($('#txtmessage').val() == "" && mess == "1") {
                //    alert("Enter Comments;");
                //    return;
                //}
                var ssoitemvalue = Messagevalue + "~" + $('#txtmessage').val();
                $.get("Ajax.aspx", { ReqCase: "SetMessage", ReqVal: ssoitemvalue }, function (data) {
                    if (data != '') {
                        window.location.reload();
                    }
                });
            });
        });
       <%-- function Refresh() {
            $('#<%=gridUserDetails.ClientID %>').DataTable();
        }--%>
        function Fetch(ssoitemvalue) {
            $.get("Ajax.aspx", { ReqCase: "StatusApiCall", ReqVal: ssoitemvalue }, function (data) {
                if (data != '') {
                    $("#myModal").modal();
                    $("#content").html(data);
                    $("#content").css('display', 'block');
                }
            });
        }
        function showmessage(value) {
            Messagevalue = value;
            $('#divapprove').modal();
            $('#P1').css('display', 'block');
            Refresh();
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
        function UplaodDocs(Values) {
            window.open(Values, "Upload Attach", "status=1,height=400,width=900,resizable=0");
            Refresh();
        }
        function View(Values) {
            window.location.href = Values;
        }
    </script>
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-body">
                    <label style="padding: 5px;">
                        Employee Details</label>
                    <div style="border: 1px solid #03a9f4; height: 100%; padding: 7px 0px 0px 0px; background: whitesmoke;">
                        <div class="col-md-12">
                            <div class="box box-primary">
                                <div class="box-body no-padding">
                                    <asp:UpdatePanel ID="updategrid" runat="server" UpdateMode="Always">
                                        <ContentTemplate>
                                            <asp:GridView ID="gridUserDetails" runat="server" AutoGenerateColumns="false" OnRowCommand="gridUserDetails_RowCommand"
                                                CssClass="display" ShowHeaderWhenEmpty="true" GridLines="None" AlternatingRowStyle-CssClass="alt"
                                                PagerStyle-CssClass="pgr" EmptyDataText="No Records Found">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.NO" ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="EMP CODE" ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblempcode" runat="server" Text='<%#Eval("EMP_CODE") %>'></asp:Label>
                                                            <asp:Label ID="lblemail" runat="server" Text='<%#Eval("EmailID")%>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="EMP NAME" ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblempname" runat="server" Text='<%#Eval("EMPLOYEE_NAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DESIGNATION" ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldesignation" runat="server" Text='<%#Eval("DESIGNATION") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DOB" Visible="false" ControlStyle-Font-Size="Smaller"
                                                        HeaderStyle-Font-Size="Smaller">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldob" runat="server" Text='<%#Eval("DOB")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="FN_YR" Visible="false" ControlStyle-Font-Size="Smaller"
                                                        HeaderStyle-Font-Size="Smaller">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblfnyr" runat="server" Text='<%#Eval("FN_YR") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="EMPLOYEE<br/>STATUS" Visible="false" ControlStyle-Font-Size="Smaller"
                                                        HeaderStyle-Font-Size="Smaller">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblempstatus" runat="server" Text='<%#Eval("EMPLOYEE_STATUS") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="REASON FOR<br/>END EMP" Visible="false" ControlStyle-Font-Size="Smaller"
                                                        HeaderStyle-Font-Size="Smaller">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblreasonforendemp" runat="server" Text='<%#Eval("REAGON_FOR_END_EMP") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="MARITAL<br/>STATUS" Visible="false" ControlStyle-Font-Size="Smaller"
                                                        HeaderStyle-Font-Size="Smaller">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblempmartialstatus" runat="server" Text='<%#Eval("MARITAL_STATUS") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="GENDER" Visible="false" ControlStyle-Font-Size="Smaller"
                                                        HeaderStyle-Font-Size="Smaller">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgender" runat="server" Text='<%#Eval("SEX") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PRESENT<br/> ADDRESS" Visible="false" ControlStyle-Font-Size="Smaller"
                                                        HeaderStyle-Font-Size="Smaller">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpaddress" runat="server" Text='<%#Eval("P_ADDRESS") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="STATUS" ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("IsApproved").ToString()=="1"? "PENDING" : "REJECTED" %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="IS ACTIVE" Visible="false" ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkapproved" runat="server" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Approved" Text='<%#Eval("IsApproved").ToString()=="1"?"ACTIVE":"INACTIVE"%>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee Status" ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkactive" runat="server" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Login" Text='<%#Eval("IsActive").ToString()=="1"?"ACTIVE":"INACTIVE"%>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="VIEW" Visible="false" ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkview" runat="server" CommandArgument='<%# Container.DataItemIndex %>' CommandName="View" Text="View"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="APPROVE" ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkapprove" runat="server" CommandArgument='<%# Container.DataItemIndex %>' CommandName="APPROVE" Text="APPROVE"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="HARDCOPY RECEIVED STATUS" Visible="false" ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblstatushardcopy" runat="server" Text='<%#Eval("IsApproved").ToString()=="1"? "PENDING" : "REJECTED" %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="REPORT" ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkReport" runat="server" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Report" Text="REPORT"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="STATUS" ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                                        <ItemTemplate>
                                                            <%--<asp:LinkButton ID="lnkReport" runat="server" CommandArgument='<%# Container.DataItemIndex %>' PostBackUrl="~/AdminDetailsemp.aspx?Myid=<='<%#Eval("EMP_CODE") %>' CommandName="STATUS" Text="ClICK"></asp:LinkButton>--%>
                                                            <a href="AdminDetailsemp.aspx?Myid=<%# Eval("EMP_CODE")%>'">
                                                                <asp:Label ID="lblstta" runat="server" Text='CLICK'></asp:Label></a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="IS EDIT" Visible="false" ControlStyle-Font-Size="Smaller"
                                                        HeaderStyle-Font-Size="Smaller">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkedit" runat="server" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Editable" Text='<%#Eval("IsEdit").ToString()=="1"?"Non Edit":"Edit"%>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="RESET" Visible="false" ControlStyle-Font-Size="Smaller"
                                                        HeaderStyle-Font-Size="Smaller">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkreset" runat="server" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Reset" Text="Reset"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="FILE" Visible="false" ControlStyle-Font-Size="Smaller"
                                                        HeaderStyle-Font-Size="Smaller">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkattach" runat="server" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Attach" Text="Link"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:Button ID="btnedit" runat="server" CssClass="buttonlogin" Visible="false" Style="font-size: smaller"
                                                Width="80px" Text="All Edit" OnClick="btnedit_Click" CausesValidation="false" />
                                            <asp:Button ID="btnnonedit" runat="server" CssClass="buttonlogin" Visible="false"
                                                Style="font-size: smaller" Width="80px" Text="All Non Edit" OnClick="btnnonedit_Click"
                                                CausesValidation="false" />
                                            <asp:Button ID="btnallreport" runat="server" CssClass="buttonlogin" Visible="false"
                                                Style="font-size: smaller" Width="140px" Text="All Emp Report" OnClick="btnallreport_Click"
                                                CausesValidation="false" />
                                            <asp:Button ID="btnfetch" runat="server" Text="Fetch Employee" Visible="false" CssClass="buttonlogin"
                                                Style="font-size: smaller" Width="140px" CausesValidation="false" OnClick="btnfetch_Click" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
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
    <div class="modal fade" id="divapprove" role="dialog">
        <div class="modal-dialog" id="Div2">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
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
    <asp:Label ID="lblError" runat="server"></asp:Label>
</asp:Content>
