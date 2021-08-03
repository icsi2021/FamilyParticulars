<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadAttachment.aspx.cs" Inherits="UploadAttachment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- Basic Page Needs
================================================== -->
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <!-- Mobile Specific Metas
================================================== -->
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <!-- For Search Engine Meta Data  -->
    <meta name="description" content="" />
    <meta name="keywords" content="" />
    <meta name="author" content="yoursite.com" />
    <title>ICSI</title>
    <!-- Favicon -->
    
</head>

<body>
    <form id="form1" runat="server">
        <div>
            <br />
           
            <asp:FileUpload ID="FileUpload" runat="server" />
            <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
            <hr />
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
    </form>
</body>
</html>
