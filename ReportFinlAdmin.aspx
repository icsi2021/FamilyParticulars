<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportFinlAdmin.aspx.cs" Inherits="ReportFinlAdmin" MasterPageFile="~/MainFile.master" %>




<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
    <asp:ScriptManager ID="scrmanager" runat="server">
    </asp:ScriptManager>
    <link rel="stylesheet" type="text/css" href="style.css" media="all" />
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
          label, p, ul, ol, a, blockquote, input, textarea, select, [type=date], [type=text], [type=email], span {
          font-size: 15px;
          line-height: 25px;
          color: #5d5d5d;
        }
          .row {
            max-width: 100%;
            margin-right: auto;
            margin-left: auto;
        }
    </style>
    <script type="text/javascript">
    function alertDelete()
    {
    alert("Successfully Deleted.");
    }
    function alertMessage()
    {
    alert("Successfully Added.");
    }
    </script>
    <div id="DIVFamilyDetails">
        <%--label style="padding: 5px;">
            Master Relation</label>--%>
     <div style="border: 1px solid black; padding: 0px 0px 0px 0px; background: whitesmoke;">   
      <div class="single-service" style="background:#f2f2f2;">
        <div class="row" style="background:#fff;">
            <div class="medium-2 small-12 columns sidebar margin-top" >
                <div class="widget">
                    <div class="widget-content">
                        <ul class="menu vertical" id="ensbledata1" runat="server">                            
                           <%-- <li class="active"><a href="Dashboard.aspx">My Dashboard</a></li>--%>
                            <li class="active"><a href="ReportFinlAdmin1.aspx?Myid=1">Family Details</a></li>
                            <li class="active"><a href="ReportFinlAdmin.aspx?Myid=2">Provident Fund</a></li>
                            <li class="active"><a href="ReportFinlAdmin.aspx?Myid=3">New Pension Trust</a></li>
                            <li class="active"><a href="ReportFinlAdmin.aspx?Myid=4">Gratuity</a></li>
                            <li class="active"><a href="ReportFinlAdmin.aspx?Myid=5">Benevolent Fund</a></li>
                            <li class="active"><a href="ReportFinlAdmin.aspx?Myid=6">Encashment</a></li>
                           <%-- <li><a href="#">Settings</a></li>--%>
                            <%--<li><a href="Logout.aspx">Logout</a></li>--%>
                        </ul>
                    </div><!-- Widget Content /-->
                </div><!-- widget /-->
            </div> <!-- Sidebar /-->


            <div class="medium-10 small-12 columns margin-top">                
                <div class="block-info">    
                    <div class="inner-title margin-bottom-20">
						<h4><asp:Label ID="lbltname" runat="server" Text=""></asp:Label></h4>
                       
					</div>                    
                    <div class="appointment-page_1 module">  
                       <%-- <div class="well">--%>
                            <div class="row" style="display:inline-flex">                      
                               
                                <asp:Label ID="hddstr1" runat="server" Visible="false" Text="Label"></asp:Label>
                                <div class="medium-2 small-12 columns">
                                    <label class="control-label">&nbsp;</label>
                                    <div style="display:grid;">
                                        <%--<asp:Button ID="submit1" runat="server" Text="Submit" OnClick="submit1_Click" class="button primar margin-bottom-10" />--%>
                                    </div>
                                </div>
                                 <div class="medium-12 small-12 columns" style="display:inline-flex">                                                                          
                                     <b>EmpID:</b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtusername" runat="server" Height="30px" Width="100%"></asp:TextBox> &nbsp;&nbsp;&nbsp;&nbsp;
                                     <asp:Button ID="btnsunbitt" runat="server" Text="Submit" Height="30px" OnClick="btnsunbitt_Click"/>&nbsp;&nbsp;&nbsp;&nbsp;
                                      <input type="button" onclick="tableToExcel('example', 'W3C Example Table')" class="button primar margin-bottom-10" style="background-color:#083b82; margin:5px 5px 0px 0px;color:white; border-radius:10px; font-size:20px; width:100px" value="Export" style="margin-top:-35px;"/>                                       
                                 </div>
                            </div> 
                       <%-- </div>--%>
                        
                              
                        <div class="medium-12 small-12 columns" id="idColumn">
                            <table id="example" class="table table-striped table-bordered" style="width:100%">
                                <thead>
                                    <tr style="color:whitesmoke; background-color:#083b82">
                                       <%-- <th>ID</th>--%>
                                        <th>Name</th>
                                        <th>Relation</th>
                                        <th>DOB</th>
                                       <%-- <th>Monthly Income</th>
                                        <th>Occupation</th>--%>
                                        <th>Emp Code</th>
                                        <th>ModiDate</th>
                                        <th>Provident Share</th> 
                                        <th>Status</th>                          
                                    </tr>
                                </thead>
                                <tbody>
                                    <div class="price-list module" id="abc">
		                                <div id="idRow1" runat="server">
                                            <%--<div class="medium-12 small-12 columns" id="idColumn">
                                                <tr>
                                                    <td><asp:Label ID="lblHname" runat="server" Text="Varinder"></asp:Label></td>
                                                    <td><asp:Label ID="lblAdd" runat="server" Text="Label"></asp:Label></td>
                                                    <td><asp:Label ID="LblConName" runat="server" Text="Label"></asp:Label></td>
                                                    <td><asp:Label ID="lblConNo" runat="server" Text="Label"></asp:Label></td>
                                                    <td><asp:Label ID="lblpin" runat="server" Text="Label"></asp:Label></td>
                                                    <td><asp:Label ID="lblstate" runat="server" Text="Label"></asp:Label></td>
                                                    <td><asp:Label ID="lblcity" runat="server" Text="Label"></asp:Label></td>
                                                    <td><asp:Label ID="lblday" runat="server" Text="Label"></asp:Label></td>
                                                    <td><asp:Label ID="lbltime" runat="server" Text="Label"></asp:Label></td>
                                                    <td><asp:Label ID="lblfee" runat="server" Text="Label"></asp:Label></td>
                                                </tr>
                                             </div>--%>
                                        </div>
                                        
                                    </div> 
                                </tbody>
                            </table>
                        </div>  
                    </div>                     
                                


              

                </div><!-- Row /-->
             </div>





            



                </div>
                   
            </div><!-- Left section /-->          

        </div><!-- Row /-->
    </div>



    <script type="text/javascript">
       <%-- $("#<%=gridUserDetails5.ClientID%>").prepend($("<thead></thead>").append($("#<%=gridUserDetails5.ClientID%>").find("tr:first"))).dataTable(
             { "scrollY": "1000px", "scrollCollapse": true, "paging": false, "language": { "search": "Search" } });--%>
        var tableToExcel = (function () {
            var uri = 'data:application/vnd.ms-excel;base64,'
              , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>'
              , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
              , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
            return function (table, name) {
                if (!table.nodeType) table = document.getElementById(table)
                var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML }
                window.location.href = uri + base64(format(template, ctx))
            }
        })()
</script>
            
      
</asp:Content>
