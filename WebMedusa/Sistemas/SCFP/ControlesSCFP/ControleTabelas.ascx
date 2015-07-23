<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControleTabelas.ascx.cs" Inherits="Medusa.Sistemas.SCFP.ControlesSCFP.ControleTabelas" %>

<%@ Register Src="ControleFaixaTaxas.ascx" TagName="ControleFaixaTaxas" TagPrefix="uc1" %>

<%@ Register src="ControleBCDedutiveis.ascx" tagname="ControleBCDedutiveis" tagprefix="uc2" %>


<asp:Panel ID="panelGrid" runat="server">
    <asp:GridView ID="grid" runat="server" CssClass="mGrid" DataKeyNames="id_tabela" Width="100%"
        AutoGenerateColumns="False" Caption="Tabelas" ForeColor="White" BorderStyle="Solid"
        GridLines="None" ShowFooter="True" OnRowCommand="grid_RowCommand"
        OnRowCancelingEdit="grid_RowCancelingEdit" OnRowEditing="grid_RowEditing" OnRowUpdating="grid_RowUpdating"
        OnRowDeleting="grid_RowDeleting" OnRowCreated="grid_RowCreated" OnSelectedIndexChanging="grid_SelectedIndexChanging">
        <FooterStyle BackColor="#cccccc" ForeColor="Black" />
        <EmptyDataTemplate>
            <table id="tbAddFaixas" runat="server" class="mGrid" width="100%">
                <tr>
                    <th></th>
                    <th></th>
                    <th>início</th>
                    <th>término</th>
                    <th>mensalmente cumulativo</th>
                </tr>
                <tr>
                    <td >
                        </td>
                    <td >
                        <asp:ImageButton ID="ibAddTable" runat="server" CssClass="add" ImageUrl="~/Styles/img/addImg.png" CommandName="Select" ValidationGroup="newTabela" /></td>
                    <td >
                        <cdt:cData ID="cAddDataInicio" runat="server" ValidationGroup="newTabela" />
                    </td>
                    <td >
                        <cdt:cData ID="cAddDataTermino" runat="server" ValidationGroup="newTabela" />
                    </td>
                    <td >
                        <asp:CheckBox ID="ckAddCumulativoMensalmente" runat="server" /></td>
                </tr>
                <%--<tr id="trNewFaixas" runat="server">
                    <td></td>
                    <td></td>
                    <td colspan="3">
                        <uc1:ControleFaixaTaxas ID="ControleFaixaTaxas1" runat="server" ValidationGroup="Add" />
                    </td>
                </tr>--%>
            </table>
        </EmptyDataTemplate>
        <Columns>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:ImageButton ID="ibEdit" ToolTip="editar" runat="server" ImageUrl="~/Styles/img/Edit.png" CausesValidation="false" CommandName="Edit" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:ImageButton ID="ibSave" ToolTip="salvar" runat="server" CssClass="add" ImageUrl="~/Styles/img/Save-32.png" CommandName="Update" CausesValidation="false" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:ImageButton ID="ibNew" ToolTip="inserir" runat="server" CssClass="add" ImageUrl="~/Styles/img/addImg.png" CommandName="Add" ValidationGroup="newTabela" />
                </FooterTemplate>
                <HeaderStyle HorizontalAlign="Left" Width="10px" Height="5px" />
                <ItemStyle HorizontalAlign="Left" />
                <FooterStyle BackColor="#cccccc" ForeColor="Black" />
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="false" CommandName="Delete" ImageUrl="~/Styles/img/delete_img.png" ToolTip="excluir" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:ImageButton ID="ibCancelEdit" ToolTip="voltar" runat="server" CssClass="add" ImageUrl="~/Styles/img/Back-round-16.PNG" CommandName="Cancel" CausesValidation="false" />
                </EditItemTemplate>
                <HeaderStyle Height="5px" HorizontalAlign="Left" Width="10px" />
                <ItemStyle HorizontalAlign="Left" />
                <FooterStyle BackColor="#cccccc" ForeColor="Black" />
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="false">
                <EditItemTemplate>
                    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="false" CommandName="Delete" ImageUrl="~/Styles/img/delete_img.png" ToolTip="excluir" />
                </EditItemTemplate>
                <HeaderStyle HorizontalAlign="Left" Width="10px" Height="5px" />
                <ItemStyle HorizontalAlign="Left" />
                <FooterStyle BackColor="#cccccc" ForeColor="Black" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="início">
                <EditItemTemplate>
                    <cdt:cData ID="cDataInicio" runat="server" Value='<%# Bind("data_ini") %>' />
                </EditItemTemplate>
                <FooterTemplate>
                    <cdt:cData ID="cAddDataInicio" runat="server" ValidationGroup="newTabela" />
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="LabelInicio" runat="server" Text='<%# Bind("data_ini", "{0:d}") %>' ></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" Height="5px" />
                <ItemStyle HorizontalAlign="Left" />
                <FooterStyle BackColor="#cccccc" ForeColor="Black" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="término">
                <EditItemTemplate>
                    <cdt:cData ID="cDataTermino" runat="server" Value='<%# Bind("data_fim") %>' />
                </EditItemTemplate>
                <FooterTemplate>
                    <cdt:cData ID="cAddDataTermino" runat="server" ValidationGroup="newTabela" />
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="LabelTermino" runat="server" Text='<%# Bind("data_fim", "{0:d}") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" Height="5px" />
                <ItemStyle HorizontalAlign="Left" />
                <FooterStyle BackColor="#cccccc" ForeColor="Black" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="mensalmente cumulativo">
                <EditItemTemplate>
                    <asp:CheckBox ID="ckCumulativoMensal" runat="server" Checked='<%# Bind("cumulativo_mensal") %>' />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:CheckBox ID="ckAddCumulativoMensalmente" runat="server" Checked='<%# Bind("cumulativo_mensal") %>' />
                </FooterTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="IckCumulativoMensal" runat="server" Checked='<%# Bind("cumulativo_mensal") %>' Enabled="false" />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" Height="5px" />
                <ItemStyle HorizontalAlign="Left" />
                <FooterStyle BackColor="#cccccc" ForeColor="Black" />
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Panel ID="pFaixas" runat="server" Width="100%" Visible="false">

                        <tr>
                            <td></td>
                            <td colspan='999'>
                                <table>
                                    <tr>
                                        <td valign="top">
                                            <uc1:ControleFaixaTaxas ID="ControleFaixaTaxas1" runat="server" ValidationGroup="edit" />
                                        </td>
                                        <td valign="top">
                                            <uc2:ControleBCDedutiveis ID="ControleBCDedutiveis1" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </asp:Panel>

                </ItemTemplate>
                <%--<FooterTemplate>
                    <asp:Panel ID="pFaixas" runat="server" Width="100%" Visible="true">

                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td colspan='999'>
                                <uc1:ControleFaixaTaxas ID="ControleFaixaTaxas1" runat="server" ValidationGroup="new" />
                            </td>
                        </tr>
                    </asp:Panel>
                </FooterTemplate>--%>
                <HeaderStyle Height="5px" />
                <FooterStyle BackColor="#cccccc" ForeColor="Black" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle HorizontalAlign="Left" />
        <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle" />
        <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle" />
        <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle" />
        <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle " />
    </asp:GridView>

</asp:Panel>


<asp:Label ID="txtCodigo" runat="server" Text="0" Visible="false"></asp:Label>


