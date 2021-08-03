<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Attachment.aspx.cs" Inherits="Attachment" MasterPageFile="~/MainFile.master" %>

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

        label, p, ul, ol, a, blockquote, input, textarea, select, [type=date], [type=text], [type=email], span {
            font-size: 15px;
            line-height: 25px;
            color: #5d5d5d;
        }
    </style>
    <script type="text/javascript">
        function alertDelete() {
            alert("Successfully Deleted.");
        }
        function alertMessage() {
            alert("Successfully Added.");
        }
    </script>
    <div id="DIVFamilyDetails">
        <div style="border: 1px solid black; padding: 0px 0px 0px 0px; background: whitesmoke;">
            <div class="single-service" style="background: #f2f2f2;">
                <div class="row" style="background: #fff;">
                    <div class="medium-10 small-12 columns margin-top">
                        <div class="block-info">
                            <div class="inner-title">
                                <h3 style="text-align: left; margin-left: 2%"><b>Upload Attachment</b></h3>
                            </div>
                            <div class="medium-6 small-12 columns margin-top-10 padding-0">
                                <div class="appointment-page_1">
                                    <div class="form-content" style="margin-left: 5%">
                                        <div class="form-group">
                                            <asp:Label ID="lblEmpName" runat="server" Text="Employee Code"></asp:Label>
                                        </div>
                                        <div class="form-group" style="display:inline-flex">
                                            <asp:TextBox ID="TxtEmpCode" runat="server" CssClass="form-control" Height="45px"></asp:TextBox>&nbsp;&nbsp;
                                            <asp:FileUpload ID="FileUpload" runat="server"/>
                                        </div>
                                        <div>                                            
                                            <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-primary" Text="Upload" OnClick="btnUpload_Click" />
                                        </div>
                                        <div>
                                            <asp:GridView ID="GridView" runat="server" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
                                                RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White" AlternatingRowStyle-ForeColor="#000"
                                                AutoGenerateColumns="false" Width="50%" OnRowCommand="GridView_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Emp Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblempcode" runat="server" Text='<%# Eval("EMPCODE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Member Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblmembername" runat="server" Text='<%# Eval("MEMBERNAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="File Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblfilename" runat="server" Text='<%# Eval("FILENAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Download Link">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" CommandName="Download"
                                                                CommandArgument='<%# Container.DataItemIndex %>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Delete">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkdelete" runat="server" Text="Delete" CommandName="Delete"
                                                                CommandArgument='<%# Container.DataItemIndex %>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

