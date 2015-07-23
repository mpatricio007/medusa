<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true" CodeBehind="Bolsistas.aspx.cs" Inherits="Medusa.Sistemas.Bolsa.Bolsistas" %>

<%@ Register src="ControleContratoBolsa.ascx" tagname="ControleContratoBolsa" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    

    <div class="conteudo">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

               <asp:CheckBox ID="ckFilter" runat="server" AutoPostBack="True" 
                        oncheckedchanged="ckFilter_CheckedChanged" 
            Text="habilitar múltiplos filtros" />
        <asp:DataList ID="DataListFiltros" runat="server" RepeatColumns="6"
            ondatabinding="DataListFiltros_DataBinding" RepeatDirection="Horizontal" 
            OnDeleteCommand="DataListFiltros_DeleteCommand">
            <ItemTemplate>

               <div class="FilterName">

                <%# Eval("property_name") %>&nbsp
                <%# Eval("mode_name")%>&nbsp
                <%# Eval("value")%> &nbsp 
                   <asp:ImageButton ID="btExcluiFiltro" runat="server" 
                       ImageUrl="~/Styles/img/bt_delete.jpg" Width="15px" Height="15px" CommandName="delete"
                        />
                </div>
            </ItemTemplate>
        </asp:DataList>
                <div class="pesquisar">
                    <div id="updateProgressDiv" style="display:none; position:absolute;">
                        <div style=" margin-left:780px;  float:left">
                        <img src="../../Styles/img/loading.gif" />
                        <span style="margin:3px">Carregando ...</span></div>
                    </div>
                    <asp:Label ID="Label1" runat="server" Text="procurar"></asp:Label>
                    &nbsp;<asp:DropDownList ID="ddlOptions" runat="server" CausesValidation="True" 
                        AutoPostBack="True" onselectedindexchanged="ddlOptions_SelectedIndexChanged">
                        <asp:ListItem Value="PessoaFisica.nome">nome</asp:ListItem>
                        <asp:ListItem Value="PessoaFisica.cpf.Value">cpf</asp:ListItem>
                        <asp:ListItem Value="PessoaFisica.rg">rg</asp:ListItem>
                        <asp:ListItem Value="PessoaFisica.dtNascto">data de nascimento</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtProcura" runat="server" Width="137px"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlMode" runat="server">
                    </asp:DropDownList>
                    <asp:Label ID="Label2" runat="server" Text="mostrar"></asp:Label>
                    &nbsp;
                    <asp:DropDownList ID="ddlSize" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlSize_SelectedIndexChanged">
                        <asp:ListItem Selected="True">10</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>50</asp:ListItem>
                        <asp:ListItem Value="0">todos</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btSearch" runat="server" CausesValidation="False" 
                        onclick="btProcurar_Click" Text="procurar" />
                    <asp:Button ID="btCriar" runat="server" CausesValidation="False" 
                        onclick="btCriar_Click"  Text="novo" Width="80px" />
                </div>
                <asp:Panel ID="panelGrid" runat="server">
                          
                            
                    <asp:GridView ID="grid" runat="server" AllowPaging="True" AllowSorting="True" 
                        AutoGenerateColumns="False" Caption="Lista de Bolsistas" 
                        CssClass="tableView" 
                        onpageindexchanging="grid_PageIndexChanging" onrowcreated="grid_RowCreated" 
                        onrowediting="grid_RowEditing" onsorting="grid_Sorting" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="cpf" SortExpression="PessoaFisica.cpf.Value">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" 
                                        Text='<%# Bind("PessoaFisica.cpf.Value") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" 
                                        Text='<%# Bind("PessoaFisica.cpf.Value") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="nome" SortExpression="PessoaFisica.nome">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" 
                                        Text='<%# Bind("PessoaFisica.nome") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("PessoaFisica.nome") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="rg" SortExpression="PessoaFisica.rg">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("PessoaFisica.rg") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("PessoaFisica.rg") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="data de nascimento" 
                                SortExpression="PessoaFisica.dtNascto">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox4" runat="server" 
                                        Text='<%# Bind("PessoaFisica.dtNascto") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" 
                                        Text='<%# Bind("PessoaFisica.dtNascto", "{0:d}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField EditText="selecionar" ShowEditButton="True">
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:CommandField>
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
                </asp:Panel>
                <asp:Panel ID="panelCadastro" runat="server">
                    <table class="cadastro">
                        <tr>
                            <th colspan="1">
                                cadastro de bolsistas</th>
                            <th colspan="1">
                            <div id="dGravacao" runat="server">
                                <asp:Button ID="btInserir" runat="server" onclick="btInserir_Click" 
                                    Text="inserir" />
                                <asp:Button ID="btAlterar" runat="server" onclick="btAlterar_Click" 
                                    Text="salvar" />
                                <asp:Button ID="btExcluir" runat="server" CausesValidation="False" 
                                    onclick="btExcluir_Click" Text="excluir" />
                                <asp:Button ID="btCancelar" runat="server" CausesValidation="False" 
                                    onclick="btCancelar_Click" Text="cancelar" />
                            </div>
                            </th>
                        </tr>
                        <tr>
                            <td class="esquerdo" align="left" style="width: 11%">
                                cpf:<asp:Label ID="txtCodigo" runat="server" Text="0" Visible="False"></asp:Label>
                                <asp:Label ID="txtId_pessoa" runat="server" Text="0" Visible="False"></asp:Label>
                            </td>
                            <td class="direito">
                                <cCpf:cCPF ID="cCPFBolsista" runat="server" OnTextChanged="cCPFBolsista_OnTextChanged" AutoPostBack="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo" style="width: 11%">
                                nome: </td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoNome" runat="server" EnableValidator="True" 
                                    MaxLength="50" Width="300" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo" style="width: 11%" >
                                sexo:</td>
                            <td class="direito">
                                <cddlSexo:cDdlSexo ID="cDdlSexo" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo" style="width: 11%">
                                rg:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoRg" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo" style="width: 11%" >
                                data de nascimento:</td>
                            <td class="direito">
                                <cdt:cData ID="cDataNascimento" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">

                                <cContaPessoa:cControleContaPessoa ID="cControleContaPessoa1" runat="server" />

                            </td>
                            </tr>
                        <tr>
                          <td colspan="2">
                                <cender:cEnder ID="cEndereco" runat="server" />
                            </td>
                            <tr>
                                <th colspan="2">
                                    contatos</th>
                            </tr>
                            <tr>
                            <td class="esquerdo" style="0%: ;">
                                Telefones:</td>
                            <td class="direito">
                                
                                <cListaPessoaTelefones:cListaPessoaTelefones ID="cListaPessoaTelefones1" 
                                    runat="server" />
                                
                            </td>
                        </tr>
                            <tr>
                                <td class="esquerdo" style="0%: ;">
                                    E-mails</td>
                                <td class="direito">
                                    <cListaPessoaEmails:cListaPessoaEmails ID="cListaPessoaEmails1" 
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th colspan="2">
                                    <div ID="dGravacao1">
                                        <asp:Button ID="btInserir0" runat="server" onclick="btInserir_Click" 
                                            Text="inserir" />
                                        <asp:Button ID="btAlterar0" runat="server" onclick="btAlterar_Click" 
                                            Text="salvar" />
                                        <asp:Button ID="btExcluir0" runat="server" CausesValidation="False" 
                                            onclick="btExcluir_Click" Text="excluir" />
                                        <asp:Button ID="btCancelar0" runat="server" CausesValidation="False" 
                                            onclick="btCancelar_Click" Text="cancelar" />
                                    </div>
                                </th>
                            </tr>
                        </tr>
                             </table>
                             </asp:Panel>

                <asp:Panel ID="pContratosBolsista" runat="server">

                    <uc1:ControleContratoBolsa ID="ControleContratoBolsa1" runat="server" />

                </asp:Panel>

        </ContentTemplate>
        </asp:UpdatePanel>

        <ajaxToolKit:UpdatePanelAnimationExtender ID="upae" BehaviorID="animation" runat="server" TargetControlID="UpdatePanel1" Enabled="True">
        <Animations>
            <OnUpdating>
                <Parallel duration="0">
                    <FadeOut minimumOpacity=".5" />
                    <ScriptAction Script="onUpdating();" />  
                 </Parallel>
            </OnUpdating>
            <OnUpdated>
                <Parallel duration="0">
                   <FadeIn minimumOpacity=".5" />  
                    <ScriptAction Script="onUpdated();" /> 
                </Parallel> 
            </OnUpdated>
        </Animations>
        </ajaxToolKit:UpdatePanelAnimationExtender>
</div>
</asp:Content>
