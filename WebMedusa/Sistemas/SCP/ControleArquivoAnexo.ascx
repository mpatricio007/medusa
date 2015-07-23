<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControleArquivoAnexo.ascx.cs" Inherits="Medusa.Sistemas.SCP.ControleArquivoAnexo1" %>
<style type="text/css">
    .auto-style1 {
        color: #FF0000;
    }
</style>
<div class="conteudo">
    <asp:Panel ID="panelCadastro" runat="server">
        <asp:GridView ID="grid" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" 
            width="30%" Caption="Lista de anexos"  
            OnPageIndexChanging="grid_PageIndexChanging" OnRowCreated="grid_RowCreated" CssClass="mGrid"
            OnRowEditing="grid_RowEditing" OnSorting="grid_Sorting" OnRowDeleting="grid_RowDeleting" OnSelectedIndexChanging="grid_SelectedIndexChanging">
            <Columns>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Styles/img/delete_img.png"
                            CausesValidation="false" CommandName="Delete" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="anexo">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbArquivo" runat="server" CommandName="Select" CausesValidation="false" Text='<%# Bind("nome_arquivo")%>'/>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
            </Columns>
            <HeaderStyle HorizontalAlign="Left" />
            <SortedAscendingCellStyle
                CssClass="SortedAscendingCellStyle" />
            <SortedAscendingHeaderStyle
                CssClass="SortedAscendingHeaderStyle" />
            <SortedDescendingCellStyle
                CssClass="SortedDescendingCellStyle" />
            <SortedDescendingHeaderStyle
                CssClass="SortedDescendingHeaderStyle " />
        </asp:GridView>
        <table class="cadastro" >
            <tr>
                <th colspan="2" >cadastro de anexos</th>
            </tr>

            <tr>
                <td class="esquerdo">nome do arquivo:<asp:Label ID="txtCodigo" runat="server" Font-Bold="False" Text="0" Visible="False" />
                    &nbsp;</td>
                <td class="direito">
                    <ctx:cTexto ID="cTextoNome" runat="server" ValidationGroup="arquivo" MaxLength="500" Width="500" EnableValidator="True" />
                </td>
            </tr>
            <tr>
                <td class="esquerdo" >arquivo:</td>
                <td class="direito">
                    <ajaxToolkit:AsyncFileUpload ID="AsyncFileUpload1" runat="server" UploadingBackColor="Yellow"
                        OnUploadedComplete="ProcessUpload" ThrobberID="spanUploading" UploaderStyle="Modern"
                        Width="300px" Visible="true" />
                    <span id="spanUploading" runat="server" visible="false">
                        <img
                            src="../../Styles/img/loading2.gif" alt="carregando" /></span>
                    <span class="auto-style1"><strong>**somente arquivos PDF**</strong></span></td>
            </tr>

            <caption>
                <asp:Panel ID="pArquivo" runat="server">
                    <tr>
                        <td class="esquerdo">baixar arquivo:</td>
                        <td>
                            <asp:LinkButton ID="lkArquivo" runat="server" OnClick="lkArquivo_Click" ValidationGroup="arquivo"></asp:LinkButton>
                        </td>
                    </tr>
                </asp:Panel>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">
                        <div id="dGravacao1">
                            <asp:Button ID="btInserir0" runat="server" Height="26px" OnClick="btInserir_Click" Text="inserir" ValidationGroup="arquivo" />
                            <asp:Button ID="btCancelar0" runat="server" CausesValidation="False" OnClick="btCancelar_Click" Text="cancelar" />
                        </div>
                    </th>
                </tr>
            </caption>
        </table>
    </asp:Panel>


</div>