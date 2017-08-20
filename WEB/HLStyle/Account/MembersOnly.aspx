<%@ Page Title="Л.Р. №2 | HealthyLifeStyle" Language="C#" Async="true" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="~/Account/MembersOnly.aspx.cs" Inherits="WEB.MembersOnly" UICulture="en" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ MasterType virtualpath="~/Web.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/spec_style.css" rel="stylesheet" />
    <style>
        .ajax__slider_h_rail {
            margin: 10px auto;
            background: none;
            border-bottom: 1px solid black;
        }
        .slide_bar {
            margin: 15px auto;
            width: 310px;
        }
            .slide_bar::after {
                content: ".";
                display: block;
                height: 0;
                clear: both;
                visibility: hidden;
            }
            .slide_bar span {
                float: left;
                display: block;
                margin: 0px 10px;
                width: auto;
            }
            .slide_bar input {
                display: none;
            }
        .ajax__slider_h_rail {
            float: left;
            display: block;
            margin: 0px;
            outline: none;
        }
        .input_data {
            width: auto;
            margin: 20px auto;
            display:inline-block;
        }
            .input_data span {
                width: 100%;
            }
            .input_data input {
                width: auto;
                padding: 0px 5px;
            }
        span#Central_block_central_col_Result {
            margin: 20px 0px;
        }
            span#Central_block_central_col_Result p {
                display: inline-block;
                padding:0px 5px;
                width: 25%;
            }
                span#Central_block_central_col_Result p + p {
                    border-left: 1px solid black;
                }
    </style>
    <script runat="server">
        
    </script>
    <script type="text/javascript">
        function loading() {
            document.getElementById('Central_block_central_col_txtSliderValue').addEventListener("change", "loading");
            document.getElementById('Central_block_central_col_wait').className = 'visible';
            document.getElementById('Central_block_central_col_Result').innerHTML = '';
        }
    </script>
    <script type="text/javascript">
        function val_change() {
            document.getElementById("Central_block_central_col_txtSliderValue").setAttribute("Text", document.getElementById("Central_block_central_col_txtSliderValue").innerHTML);
            loading();
        };
        $(function () {
            var $cont = $(".twitter-container"),
            prd = setInterval(function () {
                if ($cont.find("> iframe").contents().find(".twitter-timeline").length > 0) {
                    clearInterval(prd)
                    // Теперь можно работать с фреймом
                    $cont.find("> iframe").contents().find(".twitter-timeline").Background = "transparent"
                }
            }, 100);
        });
    </script>
    <asp:ScriptManager ID="ScriptManager"
        runat="server" />
</asp:Content>

<asp:Content ID="Central_block_left" ContentPlaceHolderID="Central_block_left_col" runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Central_block_central_col" Runat="Server">
    <h1 style="text-align: center;">Подсчёт</h1>

    <asp:UpdatePanel ID="UpdatePanel"
        UpdateMode="Conditional"
        runat="server">
        <ContentTemplate>
            <div class="base_var">
                <asp:Label runat="server"><h4>Заготовки:</h4></asp:Label>
                <div>
                    <button runat="server" onclick="loading();" onserverclick="value_in_point">175 элемент</button>
                </div>
                <div>
                    <button runat="server" onclick="loading();" onserverclick="value_in_point">350 элемент</button>
                </div>
                <div>
                    <button runat="server" onclick="loading();" onserverclick="value_in_point">750 элемент</button>
                </div>
            </div>
            <asp:Label ID="Label9" runat="server" Text="<h4>Входная функция:</h4>"></asp:Label>
            <div style="text-align:left; margin: 10px 0px;">
                <asp:Label ID="Label2" runat="server" Text="y=ax^2lnx"></asp:Label>
                <asp:Label ID="Label3" runat="server" Text="y=1"></asp:Label>
                <asp:Label ID="Label4" runat="server" Text="y=e^axcosbx"></asp:Label>
            </div>
            
            <asp:Label ID="Label10" runat="server" Text="<h4>Входные параметры:</h4>"></asp:Label>
            <div class="input_data">
                <asp:Label ID="Label1" runat="server" Text="A"></asp:Label>
                <asp:TextBox ID="A" runat="server" Text="-0,5" AutoCompleteType="Disabled" AutoPostBack="false"></asp:TextBox>
            </div>
            <div class="input_data">
                <asp:Label ID="Label5" runat="server" Text="B"></asp:Label>
                <asp:TextBox ID="B" runat="server" Text="2" AutoCompleteType="Disabled" AutoPostBack="false"></asp:TextBox>
            </div>
            <div class="input_data">
                <asp:Label ID="Label6" runat="server" Text="Начало"></asp:Label>
                <asp:TextBox ID="start" runat="server" Text="0" AutoCompleteType="Disabled" AutoPostBack="false"></asp:TextBox>
            </div>
            <div class="input_data">
                <asp:Label ID="Label7" runat="server" Text="Конец"></asp:Label>
                <asp:TextBox ID="end" runat="server" Text="3" AutoCompleteType="Disabled" AutoPostBack="false"></asp:TextBox>
            </div>
            
            <div class="slide_bar">
                <asp:Label ID="Label8" runat="server" Text="ΔX"></asp:Label>
                <asp:TextBox runat="server" ID="txtSlider" OnTextChanged="txtSlider_TextChanged" onchange="javascript: val_change();" AutoPostBack="True" ValidateRequestMode="Disabled"></asp:TextBox>
                <ajaxToolkit:SliderExtender runat="server" ID="sliderExtender"
                    TargetControlID="txtSlider" BoundControlID="txtSliderValue"
                    Minimum="0" Maximum="1" Steps="250" Decimals="3" Length="200"
                    EnableHandleAnimation="True"
                    BehaviorID="txtSlider" RaiseChangeOnlyOnMouseUp="True" />
                <span runat="server" id="txtSliderValue"></span>
            </div>

            <asp:Button ID="Calc" runat="server" Text="Расчитать" OnClientClick="loading()" OnClick="Calcing" />
            <asp:Label runat="server" ID="wait"><i class="fa fa-refresh fa-spin fa-3x fa-fw" aria-hidden="true"></i></asp:Label>
    <asp:UpdatePanel ID="UP"
        UpdateMode="Conditional"
        runat="server">
        <ContentTemplate>
            <asp:Label ID="Result" runat="server" Text="Здесь будет результат"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Central_block_right_col" Runat="Server">
    <div class="video_elem" runat="server" id="myTwitter">
        <a class="twitter-timeline" href="https://twitter.com/Kuz_Dmitry" data-widget-id="719614135764434945" data-tweet-limit="1">Твіти від користувача(ки) @Kuz_Dmitry</a>
<script>!function(d,s,id){var js,fjs=d.getElementsByTagName(s)[0],p=/^http:/.test(d.location)?'http':'https';if(!d.getElementById(id)){js=d.createElement(s);js.id=id;js.src=p+"://platform.twitter.com/widgets.js";fjs.parentNode.insertBefore(js,fjs);}}(document,"script","twitter-wjs");</script>
    </div>
</asp:Content>
