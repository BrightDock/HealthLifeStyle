<%@ Page Title="Регистрация | HLStyle" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="~/Account/Registration.aspx.cs" Inherits="WEB.Account.Registration" %>

<%@ MasterType VirtualPath="~/Web.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <asp:ScriptManager ID="ScriptManager"
        runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Central_block_left_col" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Central_block_central_col" runat="Server">
    <h1 style="text-align: center; margin-top: 15px; font-weight: 400;">Регистрация</h1>
    <asp:UpdatePanel ID="Content" UpdateMode="Conditional" runat="server">
        <ContentTemplate>



            <asp:Panel ID="RegistrationContainer" runat="server" CssClass="RegistrationContainer">
                <asp:Label ID="Error_label" runat="server" Text=""></asp:Label>
                <div class="data_member">
                    <label for="user_name">Имя:</label>
                    <span class="user_results_link_pic"></span>
                    <input type="text" id="user_name" runat="server" maxlength="25"/>
                </div>
                <div class="data_member">
                    <label for="user_surname">Фамилия:</label>
                    <span class="user_results_link_pic"></span>
                    <input type="text" id="user_surname" runat="server" maxlength="25"/>
                </div>
                <div class="data_member">
                    <label for="user_login">Логин:</label>
                    <span class="user_results_link_pic"></span>
                    <input type="text" id="user_login" runat="server" maxlength="20"/>
                </div>
                <div class="data_member">
                    <label for="user_password">Пароль:</label>
                    <span class="user_results_link_pic"></span>
                    <input type="password" id="user_password" runat="server" maxlength="20" />
                </div>
                <div class="data_member">
                    <label for="user_password_check">Повторите пароль:</label>
                    <span class="user_results_link_pic"></span>
                    <input type="password" id="user_password_check" runat="server" maxlength="20" />
                </div>
                <div class="data_member" id="gender_checkbox">
                    <label for="gender_checkbox">Пол:</label>
                    <input type="checkbox" id="gender" runat="server" />
                    <label for="Central_block_central_col_gender" runat="server">Женский</label>
                </div>
                <div class="data_member">
                    <label for="user_birthday">Дата рождения:</label>
                    <span class="user_results_link_pic"></span>
                    <input type="date" id="user_birthday" runat="server" />
                </div>
                <div class="data_member">
                    <label for="FileUpload">Фото:</label>
                    <asp:FileUpload ID="FileUpload" runat="server" EnableViewState="True" />
                </div>
                <div>
                    <asp:Button ID="Submit_add" runat="server" Text="Зарегистрироваться" OnClick="Add_account" />
                </div>

            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="Submit_add" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Central_block_right_col" runat="Server">
</asp:Content>
