<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="MyResults.aspx.cs" Inherits="WEB.Logon" UICulture="ru" Culture="ru-RU" %>

<%@ MasterType VirtualPath="~/Web.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <asp:ScriptManager ID="ScriptManager"
        runat="server" />

    <script src="https://code.highcharts.com/highcharts.js"></script>
    <script src="https://code.highcharts.com/modules/data.js"></script>
    <script src="https://code.highcharts.com/modules/drilldown.js"></script>

    <script type="text/javascript">
        var example = 'line-labels',
        theme = 'default';

        $(document).ready(function () {
                Highcharts.chart('container', {
                    chart: {
                        type: 'line'
                    },
                    title: {
                        text: ' '
                    },
                    subtitle: {
                        text: ' '
                    },
                    xAxis: {
                        categories: dates
                    },
                    yAxis: {
                        title: {
                            text: 'Мой вес'
                        }
                    },
                    plotOptions: {
                        line: {
                            dataLabels: {
                                enabled: true
                            },
                            enableMouseTracking: false
                        }
                    },
                    series: [{
                        name: 'myDayWeight',
                        data: weightData
                    }]
                });
            });

        jQuery(document).ready(function () { jQuery("#view-menu").click(function (e) { jQuery("#wrap").toggleClass("toggled") }), jQuery("#sidebar-close").click(function (e) { jQuery("#wrap").removeClass("toggled") }), jQuery(document).keydown(function (e) { var t; "INPUT" != e.target.tagName && (39 == e.keyCode ? t = document.getElementById("next-example") : 37 == e.keyCode && (t = document.getElementById("previous-example")), t && (location.href = t.href)) }), jQuery("#switcher-selector").bind("change", function () { var e = jQuery(this).val(); return e && (window.location = e), !1 }) });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Central_block_left_col" runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Central_block_central_col" runat="Server">
    <asp:Panel runat="server" CssClass="dayResult post">
        <h2 runat="server" id="date_results">Мои результаты</h2>
        <asp:Panel runat="server" ID="Content_Panel">
            <h3>Как изменялся мой вес</h3>
            <asp:UpdatePanel runat="server" ID="Chart_panel">
                <ContentTemplate>
                    <div id="container"></div>
                    <asp:Label ID="Label" runat="server" Text=""></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel runat="server" ID="Data_panel">
                <ContentTemplate>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </asp:Panel>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Central_block_right_col" runat="Server">
</asp:Content>
