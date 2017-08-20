<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="~/Account/Settings.aspx.cs" Inherits="WEB.Settings" %>

<%@ MasterType VirtualPath="~/Web.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../scripts/textarea_autosize.js"></script>
    <asp:ScriptManager ID="ScriptManager"
        runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Central_block_left_col" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Central_block_central_col" runat="server">
    <asp:UpdatePanel ID="Content" runat="server">
        <ContentTemplate>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Central_block_right_col" runat="server">
</asp:Content>
