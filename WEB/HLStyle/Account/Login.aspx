<%@ Page Title="Вход | HLStyle" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="~/Account/Login.aspx.cs" Inherits="WEB.Account.Login" UICulture="ru" Culture="ru-RU" %>

<%@ MasterType VirtualPath="~/Web.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <asp:ScriptManager ID="ScriptManager"
        runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Central_block_left_col" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Central_block_central_col" runat="Server">
    <h1 style="text-align: center; margin-top: 15px; font-weight: 400;">Пожалуйста, войдите</h1>

    <asp:UpdatePanel runat="server" class="loginContainer">
        <ContentTemplate>
            <asp:Label ID="Status_label" runat="server" Text=""></asp:Label>
            <div>
                <span class="user_page_link_pic"></span>
                <label for="name">Логин:</label>
                <asp:TextBox runat="server" ID="name" MaxLength="40" />
            </div>
            <div>
                <span class="user_logout_link_pic" style="margin-left: 0px;"></span>
                <label for="password">Пароль:</label>
                <asp:TextBox runat="server" type="password" ID="password" MaxLength="50" />
            </div>
            <asp:Button ID="Enter" runat="server" type="submit" OnClick="LoginAction_Click" Text="Войти"></asp:Button>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="Enter" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Central_block_right_col" runat="Server">
</asp:Content>
