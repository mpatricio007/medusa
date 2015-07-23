<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true" CodeBehind="Autonomos.aspx.cs" Inherits="Medusa.Sistemas.Folha.Autonomos" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="ControleContratoAutonomo.ascx" tagname="ControleContratoAutonomo" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    
    <div class="conteudo">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

     
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
                        <asp:ListItem Value="PessoaFisica.cpf.Value">cpf</asp:ListItem>
                        <asp:ListItem Value="PessoaFisica.nome">nome</asp:ListItem>
                        <asp:ListItem Value="PessoaFisica.rg">rg</asp:ListItem>
                        <asp:ListItem Value="profissao">profissão</asp:ListItem>
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
                        AutoGenerateColumns="False" Caption="Lista de Autônomos" 
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
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="nome" SortExpression="PessoaFisica.nome">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" 
                                        Text='<%# Bind("PessoaFisica.nome") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("PessoaFisica.nome") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="rg" SortExpression="PessoaFisica.rg">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("PessoaFisica.rg") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("PessoaFisica.rg") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="profissao" HeaderText="Profissão" 
                                SortExpression="profissao" />
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
                                cadastro de autônomo</th>
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
                            <td class="esquerdo">
                                cpf:<asp:Label ID="txtCodigo" runat="server" Text="0" Visible="False"></asp:Label>
                                <asp:Label ID="txtId_pessoa" runat="server" Text="0" Visible="False"></asp:Label>
                            </td>
                            <td class="direito">
                                <cCpf:cCPF ID="cCPF" runat="server" OnTextChanged="cCPF_OnTextChanged" AutoPostBack="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                nome:
                            </td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoNome" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                sexo:</td>
                            <td class="direito">
                                <cddlSexo:cDdlSexo ID="cDdlSexo" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                rg:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoRg" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                data de nascimento:</td>
                            <td class="direito">
                                <cdt:cData ID="cDataNascimento" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                profissão:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoProfissao" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                inscrição inss:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoInscrInss" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                validade iss:</td>
                            <td class="direito">
                                <cdt:cData ID="cDataValidadeIss" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                tipo iss:</td>
                            <td class="direito">
                                <asp:RadioButtonList ID="RbTipoIss" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem>anual</asp:ListItem>
                                    <asp:ListItem>mensal</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                cbo:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoCbo" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                ccm:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoCcm" runat="server" EnableValidator="False" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo" style="height: 38px">
                                isento ir:</td>
                            <td class="direito" style="height: 38px">
                                <asp:RadioButtonList ID="RbIsentoIr" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="RbIsentoIr_SelectedIndexChanged" 
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem Value="True" Selected="True">sim</asp:ListItem>
                                    <asp:ListItem Value="False">não</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                motivo isenção:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoMotivoIsencao" runat="server" EnableValidator="True" 
                                    Width="300" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                classificação:</td>
                            <td class="direito">
                                <asp:RadioButtonList ID="RbClassificacao" runat="server" 
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem>Docente USP</asp:ListItem>
                                    <asp:ListItem>Funcionário USP</asp:ListItem>
                                    <asp:ListItem>Nenhum</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                regime usp:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoRegimeUsp" runat="server" EnableValidator="False" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                número cert:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoNumeroCert" runat="server" EnableValidator="False" />
                                &nbsp; Validade:&nbsp;
                                <cdt:cData ID="cDataValidade" runat="server" EnableValidator="False" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                horas semanais:</td>
                            <td class="direito">
                                <cint:cInteiro ID="cInteiroHoras" runat="server" />
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
                        </tr>
                        <tr>
                        <th colspan="2" class="style1">
                            contatos</th>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                telefones:</td>
                            <td class="direito">
                                <cListaPessoaTelefones:cListaPessoaTelefones ID="cListaPessoaTelefones" 
                                    runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                e-mails: </td>
                            <td class="direito">
                                <cListaPessoaEmails:cListaPessoaEmails ID="cListaPessoaEmails" 
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
                            <div id="dGravacao1">
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
                    </table>
                </asp:Panel>

                <asp:Panel ID="pContratos" runat="server">
                    <uc1:ControleContratoAutonomo ID="ControleContratoAutonomo" runat="server" />
                </asp:Panel>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
              

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
        
        <%# Eval("property_name") %>
</div>
</asp:Content>
