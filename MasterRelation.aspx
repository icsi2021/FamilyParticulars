<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MasterRelation.aspx.cs" Inherits="MasterRelation" MasterPageFile="~/MainFile.master" %>

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
        <label style="padding: 5px;">
            Master Relation</label>
        <div style="border: 1px solid #03a9f4; padding: 7px 0px 0px 0px; background: whitesmoke;">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-body no-padding">
                        <asp:UpdatePanel ID="updatefamily" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:GridView ID="Gd_master" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                    PageSize="30" EmptyDataText="No Records Found" CssClass="table table-hover" ShowHeaderWhenEmpty="true"
                                    GridLines="None" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                                    OnRowCreated="Gd_master_RowCreated" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No" HeaderStyle-Wrap="true" ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                                <asp:Label ID="labelrow" runat="server" Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Form Type" HeaderStyle-Wrap="true" ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlform" runat="server">
                                                <asp:ListItem Selected="True" Text="Family Particulars" Value="Family Particulars">Family Particulars</asp:ListItem>
                                                <asp:ListItem Text="New Pension Fund Trust" Value="New Pension Fund Trust">New Pension Fund Trust</asp:ListItem>
                                                <asp:ListItem Text="Pension Fund Trust" Value="Pension Fund Trust">Pension Fund Trust</asp:ListItem>
                                                <asp:ListItem Text="Benevolent Fund" Value="Benevolent Fund">Benevolent Fund</asp:ListItem>
                                                <asp:ListItem Text="Provident Fund" Value="Provident Fund">Provident Fund</asp:ListItem>
                                                <asp:ListItem Text="Gratuity" Value="Gratuity">Gratuity</asp:ListItem>
                                                <asp:ListItem Text="Encashment Fund" Value="Encashment Fund">Encashment Fund</asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name<br/>of the<br/>Family<br> members" HeaderStyle-Wrap="true" ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtfamilyname" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Relationship<br/>with the <br/>employee" HeaderStyle-Wrap="true" ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtrelationship" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Whether Date Of Birth (DOB) is actual  or not" HeaderStyle-Wrap="true" ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlchkdob" runat="server">
                                                    <asp:ListItem Text="Yes" Value="1" Selected="True">Yes</asp:ListItem>
                                                    <asp:ListItem Text="No" Value="0">No</asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date of Birth" HeaderStyle-Wrap="true" ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtdob" runat="server" Width="60px" data-provide="datepicker"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Del" HeaderStyle-Wrap="true" ControlStyle-Font-Size="Smaller" HeaderStyle-Font-Size="Smaller">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnimagedelete" runat="server" ImageUrl="~/Images/Delete.png" Style="margin-top: 0px;" Width="20px" CssClass="imgbtns" ToolTip="Delete Row"
                                                    OnClick="btnimagedelete_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:ImageButton ID="btnaddrow" runat="server" Visible="true" ImageUrl="~/Images/add.png" Style="margin-top: -30px;" Width="20px" CssClass="imgbtns" ToolTip="Add New Row"
                                    OnClick="btnaddrow_Click" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <p>
    </p>
    <div class="col-md-6">
        <div class="form-group">
            <label>
                &nbsp;</label><div class="input-group">
                    <asp:Button ID="btnsubmit" runat="server" Style="font-size: smaller" Width="80px"
                        Text="Save" CssClass="btn" OnClick="btnsubmit_Click" />
                </div>
        </div>
    </div>
</asp:Content>
