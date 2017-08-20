<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WEB.WebForm1" %>

<%@ MasterType VirtualPath="~/Web.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Central_block_left" ContentPlaceHolderID="Central_block_left_col" runat="Server">
</asp:Content>

<asp:Content ID="body" ContentPlaceHolderID="Central_block_central_col" runat="server">
    <div class="element_possibility post">
        <div class="possibility_main_wrapper">
            <div class="pic_possibility">
                <img alt="" src="img/libra.jpg" />
            </div>
            <div class="slogan_possibility">
                <div class="text_possibility">
                    <span>С Healthy Lifestyle вы можете контролировать количество съеденных калорий и вести личный дневник приёмов пищи.</span>
                </div>
            </div>
        </div>
        <div class="likes_possibility">
            <p>Соцсети: </p>
        </div>
    </div>
    <div class="element_possibility post">
        <div class="possibility_main_wrapper">
            <div class="pic_possibility">
                <img style="right: 130px; bottom: -11px;" alt="" src="img/App_Health.PNG" />
            </div>
            <div class="slogan_possibility">
                <div class="text_possibility">
                    <span>Удобное и красивое приложение на PC.</span>
                </div>
            </div>
        </div>
        <div class="likes_possibility">
            <p>Соцсети: </p>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Central_block_right_col" runat="Server">
</asp:Content>
