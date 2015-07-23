<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControleEditaisLicAnexo.ascx.cs" Inherits="Medusa.Sistemas.EditaisLic.ControleEditaisLicAnexo" %>

<div class="conteudo">

    <asp:Panel ID="panelCadastro" runat="server">


        <asp:GridView ID="grid" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" Caption="Lista de anexos"
            CssClass="tableView"
            OnPageIndexChanging="grid_PageIndexChanging" OnRowCreated="grid_RowCreated"
            OnRowEditing="grid_RowEditing" OnSorting="grid_Sorting" Width="30%" OnRowDeleting="grid_RowDeleting" OnSelectedIndexChanging="grid_SelectedIndexChanging">
            <Columns>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Styles/img/delete_img.png"
                            CausesValidation="false" CommandName="Delete" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="descrição">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbDescricao" runat="server" CommandName="Select" CausesValidation="false" Text='<%# Bind("descricao")%>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ordem" HeaderText="ordem" />
                <asp:CheckBoxField DataField="login_obrigatorio" HeaderText="login obrigatório?">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:CheckBoxField>
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
                <td class="esquerdo">descrição:<asp:Label ID="txtCodigo" runat="server" Font-Bold="False" Text="0" Visible="False" />
                    &nbsp;</td>
                <td class="direito">
                    <ctx:cTexto ID="cTextoDescricao" runat="server" ValidationGroup="anexo" MaxLength="500" Width="500" EnableValidator="True" />
                </td>
            </tr>
            <tr>
                <td class="esquerdo" >arquivo:</td>
                <td class="direito">
                    <ajaxToolkit:AsyncFileUpload ID="AsyncFileUpload1" runat="server" UploadingBackColor="Yellow"
                        OnUploadedComplete="ProcessUpload" ThrobberID="spanUploading" UploaderStyle="Modern"
                        Width="300px" Visible="true" OnClientUploadStarted="uploadStarted"/>
                    <span id="spanUploading" runat="server" visible="false">
                        <img
                            src="../../../Styles/img/loading2.gif" alt="carregando" /></span>
                </td>
            </tr>

            <tr>
                <td class="esquerdo">ordem:</td>
                <td class="direito" >
                    <cint:cInteiro ID="cIntOrdem" runat="server" ValidationGroup="anexo" EnableValidator="True" />
                </td>
            </tr>
            <tr>
                <td class="esquerdo">login obrigatório?</td>
                <td>
                    <asp:RadioButtonList ID="RbLoginObrigatorio" runat="server" RepeatDirection="Horizontal" ValidationGroup="anexo">
                        <asp:ListItem Value="True">sim</asp:ListItem>
                        <asp:ListItem Value="False">não</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <caption>
                <asp:Panel ID="pArquivo" runat="server">
                    <tr>
                        <td class="esquerdo">baixar arquivo:</td>
                        <td>
                            <asp:LinkButton ID="lkArquivo" runat="server" OnClick="lkArquivo_Click"></asp:LinkButton>
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
                            <asp:Button ID="btInserir0" runat="server" Height="26px" OnClick="btInserir_Click" Text="inserir" ValidationGroup="anexo" />
                            <asp:Button ID="btCancelar0" runat="server" CausesValidation="False" OnClick="btCancelar_Click" Text="cancelar" />
                        </div>
                    </th>
                </tr>
            </caption>
        </table>
    </asp:Panel>
</div>
