<%@ Page Title="404" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="404.aspx.cs" Inherits="WEB.NotFound" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <asp:ScriptManager ID="ScriptManager"
        runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Central_block_left_col" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Central_block_central_col" runat="server">
    <asp:Panel runat="server" CssClass="Message404">
        <h1>404</h1>
        <h3>Данной страницы не существует, либо она была удалена.</h3>
        <asp:UpdatePanel runat="server" ID="notFoundSearch">
            <ContentTemplate>
                <p>Попробуйте воспользоваться поиском:</p>
                <asp:TextBox runat="server" ID="search_box" CssClass="search_box" Text="Что найти?" OnTextChanged="search_box_TextChanged"></asp:TextBox>
                <asp:LinkButton ID="search_button" runat="server" ToolTip="Поиск" OnClick="search_button_Click">
                    <i class="fa fa-search" aria-hidden="true"></i>
                </asp:LinkButton>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="search_button" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Central_block_right_col" runat="server">
</asp:Content>
