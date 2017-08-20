<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="News.aspx.cs" Inherits="WEB.News" UICulture="ru-RU" %>
<%@ MasterType virtualpath="~/Web.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <asp:ScriptManager ID="ScriptManager"
        runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Central_block_left_col" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Central_block_central_col" Runat="Server">
    <h1 style="text-align: center; margin-top: 15px; font-weight: 400;">Новости</h1>
    <asp:UpdatePanel ID="Content" UpdateMode="Conditional" runat="server"></asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Central_block_right_col" Runat="Server">
</asp:Content>
