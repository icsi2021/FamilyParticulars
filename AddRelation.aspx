<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddRelation.aspx.cs" Inherits="AddRelation"
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
            RELATION MASTER</label>
        <div id="dvTransType" style="border: 1px solid #03a9f4; text-align: center; background: whitesmoke;">
            <div class="col-md-12">
                <label style="margin-right: 30px">
                    Enter Relation<span style="color: red; font-weight: bolder">*</span></label>
                <asp:TextBox ID="txtRelation" runat="server"></asp:TextBox>
                 <asp:Label ID="lblno1" runat="server" Visible="false" Text=''></asp:Label>
                <asp:Label ID="Lbluname" runat="server" Visible="false" Text=''></asp:Label>
                <asp:Label ID="Lbluname1" runat="server" Visible="false" Text=''></asp:Label>
                <asp:Label ID="uderval" runat="server" Visible="false" Text=''></asp:Label>
            </div>     
               
                
            </div>
        <%--</div>--%>
        <div class="col-md-12">
            <div class="form-group" style="margin-left: 50%">
                <label>
                    &nbsp;</label><div class="input-group">
                        <asp:Button ID="btnsubmit" runat="server" CssClass="col-md-4" Style="height:35px; width:140px; border-radius:10px; margin:5px 5px 0px 0px; color:white;background-color:#02646f; font-size:20px"
                            Text="Save" OnClick="btnsubmit_Click" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btclose" runat="server" Text="Close" CssClass="col-md-4" Style="height:35px; width:140px; border-radius:10px; margin:5px 5px 0px 0px; color:white;background-color:#02646f; font-size:20px" OnClick="btnclose_Click" />
                    </div>
            </div>
        </div>

         <asp:GridView ID="GvSaleRecordDetails" runat="server"  Width="100%"
                BackColor="White" BorderColor="#000" BorderStyle="None"  EmptyDataText="No Data Found" BorderWidth="1px"  
                CellPadding="3"  AutoGenerateColumns="False"  ShowHeaderWhenEmpty="true" ShowHeader="true"  style="text-align:center;">
                <PagerStyle CssClass="cssPager" />
                <Columns>
                     <asp:TemplateField HeaderText="S.No." >
                        <%-- <HeaderTemplate>
                             ProductCode
                         </HeaderTemplate>--%>
                        <ItemTemplate>
                            <asp:Label ID="lblProductCode" runat="server" Text='<%# Eval("TYPE_ID")%>'></asp:Label>                           
                        </ItemTemplate>
                    </asp:TemplateField>                  

                     <asp:TemplateField HeaderText="Relation">                         
                        <ItemTemplate>
                            <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("TYPE_NAME")%>'></asp:Label>
                        </ItemTemplate>                  
                    </asp:TemplateField>
                    

                     <asp:TemplateField HeaderText="Create Date">                         
                        <ItemTemplate>
                            <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("IsActualDate")%>'></asp:Label>
                        </ItemTemplate>                  
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Create UserName">                         
                        <ItemTemplate>
                            <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("CUsername")%>'></asp:Label>
                        </ItemTemplate>                  
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Delete Date">                         
                        <ItemTemplate>
                            <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("Mdate")%>'></asp:Label>
                        </ItemTemplate>                  
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Delete UserName">                         
                        <ItemTemplate>
                            <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("MUsername")%>'></asp:Label>
                        </ItemTemplate>                  
                    </asp:TemplateField>

                     
                     <asp:TemplateField HeaderText="Type">                         
                        <ItemTemplate>
                            <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("Type")%>'></asp:Label>
                        </ItemTemplate>                  
                    </asp:TemplateField>
                    
                    
                    
                    
                             
                     <asp:TemplateField HeaderText="Action">                         
                        <ItemTemplate>                               
                             <a href="AddRelation.aspx?Myid=hos<%# Eval("TYPE_ID")%>'"><asp:Label ID="lblHnameName" runat="server" Text='Delete'></asp:Label></a>                         
                            
                             </ItemTemplate>
                    </asp:TemplateField>
                 
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="Black" Font-Bold="False" ForeColor="White"    />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="left" />
                <RowStyle ForeColor="#000000" />   <%--ForeColor="#000066"--%>
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
              <%--  <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />--%>

            </asp:GridView>

        <asp:GridView ID="GvSaleRecordDetails1" runat="server"  Width="100%"
                BackColor="White" BorderColor="#000" BorderStyle="None"  EmptyDataText="No Data Found" BorderWidth="1px"  
                CellPadding="3"  AutoGenerateColumns="False"  ShowHeaderWhenEmpty="true" ShowHeader="true"  style="text-align:center;">
                <PagerStyle CssClass="cssPager" />
                <Columns>
                     <asp:TemplateField HeaderText="S.No." >
                        <%-- <HeaderTemplate>
                             ProductCode
                         </HeaderTemplate>--%>
                        <ItemTemplate>
                            <asp:Label ID="lblProductCode" runat="server" Text='<%# Eval("TYPE_ID")%>'></asp:Label>                           
                        </ItemTemplate>
                    </asp:TemplateField>                  

                     <asp:TemplateField HeaderText="Relation">                         
                        <ItemTemplate>
                            <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("TYPE_NAME")%>'></asp:Label>
                        </ItemTemplate>                  
                    </asp:TemplateField>
                    

                     <asp:TemplateField HeaderText="Create Date">                         
                        <ItemTemplate>
                            <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("IsActualDate")%>'></asp:Label>
                        </ItemTemplate>                  
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Create UserName">                         
                        <ItemTemplate>
                            <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("CUsername")%>'></asp:Label>
                        </ItemTemplate>                  
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Delete Date">                         
                        <ItemTemplate>
                            <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("Mdate")%>'></asp:Label>
                        </ItemTemplate>                  
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Delete UserName">                         
                        <ItemTemplate>
                            <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("MUsername")%>'></asp:Label>
                        </ItemTemplate>                  
                    </asp:TemplateField>

                     
                     <asp:TemplateField HeaderText="Type">                         
                        <ItemTemplate>
                            <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("Type")%>'></asp:Label>
                        </ItemTemplate>                  
                    </asp:TemplateField>
                    
                    
                    
                    
                             
                    <%-- <asp:TemplateField HeaderText="Action">                         
                        <ItemTemplate>                               
                             <a href="AddRelation.aspx?Myid=hos<%# Eval("TYPE_ID")%>'"><asp:Label ID="lblHnameName" runat="server" Text='Delete'></asp:Label></a>                         
                            
                             </ItemTemplate>
                    </asp:TemplateField>--%>
                 
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="Black" Font-Bold="False" ForeColor="White"    />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="left" />
                <RowStyle ForeColor="#000000" />   <%--ForeColor="#000066"--%>
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
              <%--  <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />--%>

            </asp:GridView>
    </div>
</asp:Content>
