<%@ Page Title="" Language="C#" MasterPageFile="~/MainFile.master" AutoEventWireup="true"
    CodeFile="AdminDetailsemp.aspx.cs" Inherits="AdminDetailsemp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link rel="stylesheet" type="text/css" href="../assets/css/DataTables/datatables.css" media="all" />
   <link rel="stylesheet" type="text/css" href="../assets/css/DataTables/datatables/css/dataTables.bootstrap4.min.css" media="all" />
    <script src="JS/jquery-1.10.2.js" type="text/javascript"></script>

    <script src="JS/bootstrap.min.js" type="text/javascript"></script>

    <link href="CSS/datepicker3.css" rel="stylesheet" type="text/css" />

    <script src="JS/bootstrap-datepicker.js" type="text/javascript"></script>

    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css" rel="stylesheet" type="text/css" />
    <link href="CSS/Site.css" rel="stylesheet" type="text/css" />
    <link href="CSS/bootstrap.css" rel="stylesheet" />
    <link href="CSS/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="CSS/datatable.min.css" />
    <script src="JS/jquery-1.10.2.js"></script>
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
            // $('#<%=gridUserDetails.ClientID %>').DataTable({ "info": "false", "searching": "false", "_length": "false", "paging": "false"});

            $("#<%=gridUserDetails.ClientID%>").prepend($("<thead></thead>").append($("#<%=gridUserDetails.ClientID%>").find("tr:first"))).dataTable(
                { "scrollY": "1000px", "scrollCollapse": true, "paging": false, "language": { "search": "Search" } });
            $("#<%=gridUserDetails1.ClientID%>").prepend($("<thead></thead>").append($("#<%=gridUserDetails1.ClientID%>").find("tr:first"))).dataTable(
                { "scrollY": "1000px", "scrollCollapse": true, "paging": false, "language": { "search": "Search" } });
            $("#<%=gridUserDetails2.ClientID%>").prepend($("<thead></thead>").append($("#<%=gridUserDetails2.ClientID%>").find("tr:first"))).dataTable(
               { "scrollY": "1000px", "scrollCollapse": true, "paging": false, "language": { "search": "Search" } });
            $("#<%=gridUserDetails3.ClientID%>").prepend($("<thead></thead>").append($("#<%=gridUserDetails3.ClientID%>").find("tr:first"))).dataTable(
               { "scrollY": "1000px", "scrollCollapse": true, "paging": false, "language": { "search": "Search" } });
            $("#<%=gridUserDetails4.ClientID%>").prepend($("<thead></thead>").append($("#<%=gridUserDetails4.ClientID%>").find("tr:first"))).dataTable(
               { "scrollY": "1000px", "scrollCollapse": true, "paging": false, "language": { "search": "Search" } });
            $("#<%=gridUserDetails5.ClientID%>").prepend($("<thead></thead>").append($("#<%=gridUserDetails5.ClientID%>").find("tr:first"))).dataTable(
               { "scrollY": "1000px", "scrollCollapse": true, "paging": false, "language": { "search": "Search" } });
           
            $("#myModal").css('display', 'none');
            $("#content").css('display', 'none');
            $('#btnsave').click(function () {s
                if ($('#txtmessage').val() == "") {
                    alert("Enter Comments;");
                    return;
                }
                var ssoitemvalue = Messagevalue + "~" + $('#txtmessage').val();
                $.get("Ajax.aspx", { ReqCase: "SetMessage", ReqVal: ssoitemvalue }, function (data) {
                    if (data != '') {
                        window.location.reload();
                    }
                });
            });
        });
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
                                    <div class="medium-2 small-12 columns">
                                    <label class="control-label">Start Date</label>                                   
                                        <input id="dtpFromDate" runat="server" placeholder="DD/MM/YYYY"  name="dtpFromDate" title="Enter Date" type="text"/>
                                         <label class="control-label">End Date</label>                                      
                                        
                                        <input id="dtpToDate" runat="server" placeholder="DD/MM/YYYY"  name="dtpToDate" title="Enter Date" type="text"/>
                                       <asp:Button ID="submit1" runat="server" Text="Submit" OnClick="submit1_Click" class="button primar margin-bottom-10" />
                                    </div>
                                   <h3>Family Detail</h3>
                                    <asp:UpdatePanel ID="updategrid" runat="server" UpdateMode="Always">
                                        <ContentTemplate>
                                             <div class="well box-primary">
                                            <asp:GridView ID="gridUserDetails" runat="server" AutoGenerateColumns="false" OnRowCommand="gridUserDetails_RowCommand"
                                                CssClass="display" ShowHeaderWhenEmpty="true" GridLines="None" AlternatingRowStyle-CssClass="alt"
                                                PagerStyle-CssClass="pgr" EmptyDataText="No Records Found">
                                                <Columns>

                                                   <asp:TemplateField HeaderText="Product Code" Visible="false">                      
                                                         <ItemTemplate>
                                                      <asp:Label ID="lblProductCode" runat="server" Text='<%# Eval("RowNumber")%>'></asp:Label>                           
                                                           </ItemTemplate>
                                                             </asp:TemplateField>                    

                                                        <asp:TemplateField HeaderText=" Name" >                         
                                                         <ItemTemplate>
                                                            <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("FML_MEMBER_NAME")%>'></asp:Label>
                                                             </ItemTemplate>                  
                                                           </asp:TemplateField>                   

                                                            <asp:TemplateField HeaderText="Relation" >                       
                                                            <ItemTemplate>                           
                                                              <asp:Label ID="lblProductRate" runat="server" Text='<%# Eval("REL_TYPE_ID") %>' ></asp:Label>
                           
                                                               </ItemTemplate>
                                                             </asp:TemplateField>
                   
                                                             <asp:TemplateField HeaderText="DOB" >
                        
                                                             <ItemTemplate>
                                                             <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("DOB")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Emp Code">                         
                        <ItemTemplate>
                            <asp:Label ID="lblTaxRate" runat="server" Text='<%# Eval("EMP_CODE") %>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                  <asp:TemplateField HeaderText="Monthly Income" >
                         
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
                    <HeaderStyle BackColor="#02646f" Font-Bold="False" ForeColor="White"    />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="left" />
                   <RowStyle ForeColor="#000000" />   <%--ForeColor="#000066"--%>
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                            </asp:GridView></div>


                                              <h3>Provident Fund Detail</h3>
                                                 <asp:GridView ID="gridUserDetails1" runat="server" AutoGenerateColumns="false" OnRowCommand="gridUserDetails_RowCommand"
                                                CssClass="display" ShowHeaderWhenEmpty="true" GridLines="None" AlternatingRowStyle-CssClass="alt"
                                                PagerStyle-CssClass="pgr" EmptyDataText="No Records Found">
                                                <Columns>

                                                   <asp:TemplateField HeaderText="Product Code" Visible="false">                      
                        <ItemTemplate>
                            <asp:Label ID="lblProductCode" runat="server" Text='<%# Eval("RowNumber")%>'></asp:Label>                           
                        </ItemTemplate>
                    </asp:TemplateField>                    

                     <asp:TemplateField HeaderText=" Name" >                         
                        <ItemTemplate>
                            <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("FML_MEMBER_NAME")%>'></asp:Label>
                        </ItemTemplate>                  
                    </asp:TemplateField>                   

                     <asp:TemplateField HeaderText="Relation" >                       
                        <ItemTemplate>                           
                             <asp:Label ID="lblProductRate" runat="server" Text='<%# Eval("REL_TYPE_ID") %>' ></asp:Label>
                           
                        </ItemTemplate>
                    </asp:TemplateField>                   
                    <asp:TemplateField HeaderText="DOB" >                        
                        <ItemTemplate>
                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("DOB")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Emp Code">                         
                        <ItemTemplate>
                            <asp:Label ID="lblTaxRate" runat="server" Text='<%# Eval("EMP_CODE") %>' ></asp:Label>
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
                    <HeaderStyle BackColor="#02646f" Font-Bold="False" ForeColor="White"    />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="left" />
                   <RowStyle ForeColor="#000000" />   <%--ForeColor="#000066"--%>
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                            </asp:GridView>

                                             <h3>Pension Detail</h3>
                                                 <asp:GridView ID="gridUserDetails2" runat="server" AutoGenerateColumns="false" OnRowCommand="gridUserDetails_RowCommand"
                                                CssClass="display" ShowHeaderWhenEmpty="true" GridLines="None" AlternatingRowStyle-CssClass="alt"
                                                PagerStyle-CssClass="pgr" EmptyDataText="No Records Found">
                                                <Columns>

                                                   <asp:TemplateField HeaderText="Product Code" Visible="false">                      
                        <ItemTemplate>
                            <asp:Label ID="lblProductCode" runat="server" Text='<%# Eval("RowNumber")%>'></asp:Label>                           
                        </ItemTemplate>
                    </asp:TemplateField>                    

                     <asp:TemplateField HeaderText=" Name" >                         
                        <ItemTemplate>
                            <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("FML_MEMBER_NAME")%>'></asp:Label>
                        </ItemTemplate>                  
                    </asp:TemplateField>                   

                     <asp:TemplateField HeaderText="Relation" >                       
                        <ItemTemplate>                           
                             <asp:Label ID="lblProductRate" runat="server" Text='<%# Eval("REL_TYPE_ID") %>' ></asp:Label>
                           
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                    <asp:TemplateField HeaderText="DOB" >
                        
                        <ItemTemplate>
                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("DOB")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Emp Code">                         
                        <ItemTemplate>
                            <asp:Label ID="lblTaxRate" runat="server" Text='<%# Eval("EMP_CODE") %>' ></asp:Label>
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
                            <asp:Label ID="lblTotalAmount1" runat="server" Text='<%# Eval("PensionShare") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Status">
                         
                        <ItemTemplate>
                            <asp:Label ID="lblIGST" runat="server" Text='<%#Eval("Status").ToString()=="1"?"ACTIVE":"INACTIVE"%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                                                    
                                                                       
                     </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <HeaderStyle BackColor="#02646f" Font-Bold="False" ForeColor="White"    />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="left" />
                   <RowStyle ForeColor="#000000" />   <%--ForeColor="#000066"--%>
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                            </asp:GridView>
                                            <h3>Gratuity Detail</h3>
                                             <asp:GridView ID="gridUserDetails3" runat="server" AutoGenerateColumns="false" OnRowCommand="gridUserDetails_RowCommand"
                                                CssClass="display" ShowHeaderWhenEmpty="true" GridLines="None" AlternatingRowStyle-CssClass="alt"
                                                PagerStyle-CssClass="pgr" EmptyDataText="No Records Found">
                                                <Columns>

                                                   <asp:TemplateField HeaderText="Product Code" Visible="false">                      
                        <ItemTemplate>
                            <asp:Label ID="lblProductCode" runat="server" Text='<%# Eval("RowNumber")%>'></asp:Label>                           
                        </ItemTemplate>
                    </asp:TemplateField>                    

                     <asp:TemplateField HeaderText=" Name" >                         
                        <ItemTemplate>
                            <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("FML_MEMBER_NAME")%>'></asp:Label>
                        </ItemTemplate>                  
                    </asp:TemplateField>                   

                     <asp:TemplateField HeaderText="Relation" >                       
                        <ItemTemplate>                           
                             <asp:Label ID="lblProductRate" runat="server" Text='<%# Eval("REL_TYPE_ID") %>' ></asp:Label>
                           
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                    <asp:TemplateField HeaderText="DOB" >
                        
                        <ItemTemplate>
                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("DOB")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Emp Code">                         
                        <ItemTemplate>
                            <asp:Label ID="lblTaxRate" runat="server" Text='<%# Eval("EMP_CODE") %>' ></asp:Label>
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
                            <asp:Label ID="lblTotalAmount1" runat="server" Text='<%# Eval("GratuityShare") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Status">
                         
                        <ItemTemplate>
                            <asp:Label ID="lblIGST" runat="server" Text='<%#Eval("Status").ToString()=="1"?"ACTIVE":"INACTIVE"%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                                                    
                                                                       
                                                </Columns>
                                                  <FooterStyle BackColor="White" ForeColor="#000066" />
                    <HeaderStyle BackColor="#02646f" Font-Bold="False" ForeColor="White"    />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="left" />
                   <RowStyle ForeColor="#000000" />   <%--ForeColor="#000066"--%>
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                            </asp:GridView>


                                             <h3>Benevolent Detail</h3>
                                             <asp:GridView ID="gridUserDetails4" runat="server" AutoGenerateColumns="false" OnRowCommand="gridUserDetails_RowCommand"
                                                CssClass="display" ShowHeaderWhenEmpty="true" GridLines="None" AlternatingRowStyle-CssClass="alt"
                                                PagerStyle-CssClass="pgr" EmptyDataText="No Records Found">
                                                <Columns>

                                                   <asp:TemplateField HeaderText="Product Code" Visible="false">                      
                        <ItemTemplate>
                            <asp:Label ID="lblProductCode" runat="server" Text='<%# Eval("RowNumber")%>'></asp:Label>                           
                        </ItemTemplate>
                    </asp:TemplateField>                    

                     <asp:TemplateField HeaderText=" Name" >                         
                        <ItemTemplate>
                            <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("FML_MEMBER_NAME")%>'></asp:Label>
                        </ItemTemplate>                  
                    </asp:TemplateField>                   

                     <asp:TemplateField HeaderText="Relation" >                       
                        <ItemTemplate>                           
                             <asp:Label ID="lblProductRate" runat="server" Text='<%# Eval("REL_TYPE_ID") %>' ></asp:Label>
                           
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                    <asp:TemplateField HeaderText="DOB" >
                        
                        <ItemTemplate>
                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("DOB")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Emp Code">                         
                        <ItemTemplate>
                            <asp:Label ID="lblTaxRate" runat="server" Text='<%# Eval("EMP_CODE") %>' ></asp:Label>
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
                            <asp:Label ID="lblTotalAmount1" runat="server" Text='<%# Eval("BenevolentShare") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Status">
                         
                        <ItemTemplate>
                            <asp:Label ID="lblIGST" runat="server" Text='<%#Eval("Status").ToString()=="1"?"ACTIVE":"INACTIVE"%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                                                    
                                                                       
                                                </Columns>
                                                 <FooterStyle BackColor="White" ForeColor="#000066" />
                    <HeaderStyle BackColor="#02646f" Font-Bold="False" ForeColor="White"    />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="left" />
                   <RowStyle ForeColor="#000000" />   <%--ForeColor="#000066"--%>
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                            </asp:GridView>

                                            <h3>EncashShare Detail</h3>
                                             <asp:GridView ID="gridUserDetails5" runat="server" AutoGenerateColumns="false" OnRowCommand="gridUserDetails_RowCommand"
                                                CssClass="display" ShowHeaderWhenEmpty="true" GridLines="None" AlternatingRowStyle-CssClass="alt"
                                                PagerStyle-CssClass="pgr" EmptyDataText="No Records Found">
                                                <Columns>

                                                   <asp:TemplateField HeaderText="Product Code" Visible="false">                      
                        <ItemTemplate>
                            <asp:Label ID="lblProductCode" runat="server" Text='<%# Eval("RowNumber")%>'></asp:Label>                           
                        </ItemTemplate>
                    </asp:TemplateField>                    

                     <asp:TemplateField HeaderText=" Name" >                         
                        <ItemTemplate>
                            <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("FML_MEMBER_NAME")%>'></asp:Label>
                        </ItemTemplate>                  
                    </asp:TemplateField>                   

                     <asp:TemplateField HeaderText="Relation" >                       
                        <ItemTemplate>                           
                             <asp:Label ID="lblProductRate" runat="server" Text='<%# Eval("REL_TYPE_ID") %>' ></asp:Label>
                           
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                    <asp:TemplateField HeaderText="DOB" >
                        
                        <ItemTemplate>
                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("DOB")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Emp Code">                         
                        <ItemTemplate>
                            <asp:Label ID="lblTaxRate" runat="server" Text='<%# Eval("EMP_CODE") %>' ></asp:Label>
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

                    <%-- <asp:TemplateField HeaderText="Encash Share">                          
                        <ItemTemplate>
                            <asp:Label ID="lblTotalAmount221" runat="server" Text='<%# Eval("EncashShare") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>

                     <asp:TemplateField HeaderText="Status">                         
                        <ItemTemplate>
                            <asp:Label ID="lblIGST" runat="server" Text='<%#Eval("Status").ToString()=="1"?"ACTIVE":"INACTIVE"%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                                                    
                                                                       
                                                </Columns>
                                                  <FooterStyle BackColor="White" ForeColor="#000066" />
                    <HeaderStyle BackColor="#02646f" Font-Bold="False" ForeColor="White"    />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="left" />
                   <RowStyle ForeColor="#000000" />   <%--ForeColor="#000066"--%>
                   <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
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
                                              <asp:Label ID="hddstr1" runat="server" Visible="false" Text="Label"></asp:Label>
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

    <script>
        $(document).ready(function () {
            $("#datepicker-group").datepicker({
                format: "dd/mm/yyyy",
                todayHighlight: true,
                autoclose: true,
                clearBtn: true
            });
        });
        $(document).ready(function () {
            $("#datepicker-group1").datepicker({
                format: "dd/mm/yyyy",
                todayHighlight: true,
                autoclose: true,
                clearBtn: true
            });
        });

        if (/Mobi/.test(navigator.userAgent)) {
            // if mobile device, use native pickers
            $(".date input").attr("type", "date");
            $(".time input").attr("type", "time");
        } else {
            // if desktop device, use DateTimePicker    
            $("#timepicker").datetimepicker({
                format: "LT",
                icons: {
                    up: "fa fa-chevron-up",
                    down: "fa fa-chevron-down"
                }
            });
        }

</script>
</asp:Content>
