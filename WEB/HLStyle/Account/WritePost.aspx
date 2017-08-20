<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="WritePost.aspx.cs" Inherits="WEB.WritePost" %>
<%@ MasterType virtualpath="~/Web.Master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <asp:ScriptManager ID="ScriptManager"
        runat="server" />
    <script type="text/javascript">
        function text_field_clicked(){
            if (document.getElementById("Central_block_central_col_Text_post").value == 'Ваш текст...' && document.getElementById("Central_block_central_col_Text_post") === document.activeElement)
                document.getElementById("Central_block_central_col_Text_post").value = ''
            else if (document.getElementById("Central_block_central_col_Text_post").value.length > 0)
            {
                return
            }
            else if (document.getElementById("Central_block_central_col_Text_post").value.length == 0 && document.getElementById("Central_block_central_col_Text_post") != document.activeElement)
                document.getElementById("Central_block_central_col_Text_post").value = 'Ваш текст...'

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Central_block_left_col" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Central_block_central_col" runat="server">
    <asp:UpdatePanel runat="server" ID="Content_panel">
        <ContentTemplate>
            <asp:UpdatePanel class="post" runat="server" ID="Content_new_post">
                
            </asp:UpdatePanel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Central_block_right_col" runat="server">
</asp:Content>
