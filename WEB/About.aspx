<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="WEB.WebForm2" %>
<%@ MasterType virtualpath="~/Web.Master" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Central_block_left_col" Runat="Server">
    
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Central_block_central_col" Runat="Server">
    <h1 style="text-align: center; font-weight: 400;">О Нас<asp:Label ID="CompanyName" Text="Вас приветствует сайт" runat="server" Width="100%" Font-Size="20px" ></asp:Label></h1>
    
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Central_block_right_col" Runat="Server">

</asp:Content>
