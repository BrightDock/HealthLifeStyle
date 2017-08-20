<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="Products_add.aspx.cs" Inherits="WEB.Products_add" %>
<%@ MasterType virtualpath="~/Web.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel='stylesheet' type='text/css' href='/css/add_prod.css' />
    <link rel='stylesheet' type='text/css' href='/css/font-awesome.min.css' />
    <asp:ScriptManager runat="server"></asp:ScriptManager>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Central_block_left_col" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Central_block_central_col" runat="server">
    <asp:UpdatePanel ID="Content" runat="server">
        <ContentTemplate>
            <asp:Label runat="server" Text="Название"></asp:Label><asp:TextBox runat="server" ID="Name_product" CausesValidation="True" MaxLength="40" AutoPostBack="True" OnTextChanged="isNameExist"></asp:TextBox>
            <asp:Label runat="server" Text="Кол- во Ккал"></asp:Label><asp:TextBox runat="server" ID="KKal_product" MaxLength="5"></asp:TextBox>
            <asp:Label runat="server" Text="Тип продукта"></asp:Label><asp:DropDownList runat="server" ID="prod_type" DataSourceID="HealthBD" DataTextField="Name_type" DataValueField="ID" AppendDataBoundItems="True"></asp:DropDownList>
            <asp:SqlDataSource ID="HealthBD" runat="server" ConnectionString="<%$ ConnectionStrings:BD_healthConnectionString %>" SelectCommand="SELECT * FROM [Products_types]"></asp:SqlDataSource>
            <asp:Label runat="server" Text="Жиры"></asp:Label><asp:TextBox runat="server" ID="Fats_product" MaxLength="5"></asp:TextBox>
            <asp:Label runat="server" Text="Углеводы"></asp:Label><asp:TextBox runat="server" ID="Carbohydrats_product" MaxLength="5"></asp:TextBox>
            <asp:Label runat="server" Text="Белки"></asp:Label><asp:TextBox runat="server" ID="Proteins_product" MaxLength="5"></asp:TextBox>
            <asp:Label runat="server" ID="status_label"></asp:Label>
            <asp:Label runat="server" ID="updating" CssClass="fa fa-refresh fa-spin fa-3x fa-fw" Visible="false"></asp:Label>
            <asp:Button runat="server" ID="Apply" OnClick="Apply_Click" Text="Подтвердить" />

            
            <asp:RegularExpressionValidator ID="REV_Kkal" runat="server" ValidationExpression = "\-?\d+(\,\d{0,})?" ControlToValidate="KKal_product" Display="Dynamic"
                 ErrorMessage="Поле 'Кол- во Ккал' должно состоять только из цифр!" SetFocusOnError="True"
             />
            <asp:RegularExpressionValidator ID="REV_fats" runat="server" ValidationExpression = "\-?\d+(\,\d{0,})?" ControlToValidate="Fats_product" Display="Dynamic"
                 ErrorMessage="Поле 'Жиры' должно состоять только из цифр!" SetFocusOnError="True"
             />
            <asp:RegularExpressionValidator ID="REV_carbohydrates" runat="server" ValidationExpression = "\-?\d+(\,\d{0,})?" ControlToValidate="Carbohydrats_product" Display="Dynamic"
                 ErrorMessage="Поле 'Углеводы' должно состоять только из цифры!" SetFocusOnError="True"
             />
            <asp:RegularExpressionValidator ID="REV_proteins" runat="server" ValidationExpression = "\-?\d+(\,\d{0,})?" ControlToValidate="Proteins_product" Display="Dynamic"
                 ErrorMessage="Поле 'Белки' должно состоять только из цифр!" SetFocusOnError="True"
             />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel ID="Login" runat="server"></asp:Panel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Central_block_right_col" runat="server">
</asp:Content>
