<%@ Page Title="Поиск" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="WEB.Account.Search" UICulture="ru" Culture="ru-RU" %>

<%@ MasterType VirtualPath="~/Web.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <asp:ScriptManager ID="ScriptManager"
        runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Central_block_left_col" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Central_block_central_col" runat="Server">
    <h1 style="text-align: center; margin-top: 3rem; font-weight: 400;">Результаты поиска</h1>
    <asp:UpdatePanel ID="Content" runat="server" class="searchPanel" UpdateMode="Always">
        <ContentTemplate>
                <asp:UpdatePanel runat="server" ID="notFoundSearch" class="">
                    <ContentTemplate>
                        <asp:TextBox runat="server" ID="search_box" CssClass="search_box" Text="Что найти?" OnTextChanged="search_box_TextChanged"></asp:TextBox>
                        <asp:LinkButton ID="search_button" runat="server" ToolTip="Поиск" OnClick="search_button_Click" OnClientClick="return;">
                    <i class="fa fa-search" aria-hidden="true"></i>
                        </asp:LinkButton>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="search_box" />
                    </Triggers>
                </asp:UpdatePanel>
            <asp:UpdatePanel  runat="server" ID="resultsPanel" Class="resultsPanel">
                <ContentTemplate>
                    <asp:Panel runat="server" ID="items"></asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <input type="checkbox" hidden="hidden" id="searchBPCheckbox" />
            <label for="searchBPCheckbox"></label>
            <asp:Panel ID="searchParameters" runat="server" CssClass="searchParametersBox">
                <asp:Menu runat="server" ID="searchMenu" CssClass="searchMenu sticky" IncludeStyleBlock="False" SkipLinkText="" MaximumDynamicDisplayLevels="2" StaticDisplayLevels="2">
                    <Items>
                        <asp:MenuItem Text="Все" Selected="True"></asp:MenuItem>
                        <asp:MenuItem Text="Люди">
                            <asp:MenuItem Text="Возраст">
                            </asp:MenuItem>
                            <asp:MenuItem Text="Пол"></asp:MenuItem>
                        </asp:MenuItem>
                        <asp:MenuItem Text="Публикации"></asp:MenuItem>
                    </Items>
                </asp:Menu>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Central_block_right_col" runat="Server">
</asp:Content>
