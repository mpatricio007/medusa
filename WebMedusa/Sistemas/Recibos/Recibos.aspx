<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/SiteBasePagina.Master" AutoEventWireup="true" CodeBehind="Recibos.aspx.cs" Inherits="Medusa.Sistemas.Recibos.Recibos" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<%@ Register src="../../Controles/DdlReciboCursos.ascx" tagname="DdlReciboCursos" tagprefix="uc1" %>

<%@ Register src="ControleReciboCheques.ascx" tagname="ControleReciboCheques" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadBaseMaster" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentBaseMaster" runat="server">
    <div class="conteudo">
        &nbsp;&nbsp;&nbsp;
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        &nbsp;&nbsp;&nbsp;
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
                <%# Eval("displayValue")%> &nbsp 
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
                        <asp:ListItem Value="curso">curso</asp:ListItem>                        
                        <asp:ListItem Value="id_recibo">recibo nº</asp:ListItem>
                        <asp:ListItem Value="data">data</asp:ListItem>
                        <asp:ListItem Value="valor">valor</asp:ListItem>
                        <asp:ListItem Value="nome">recebemos de</asp:ListItem>
                        <asp:ListItem Value="descricao">referente a</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtProcura" runat="server" Width="137px"></asp:TextBox>
                    &nbsp;<asp:DropDownList ID="listaCurso" runat="server" 
                        AppendDataBoundItems="True" DataTextField="nome" 
                        DataValueField="id_recibo_curso" Style="margin-bottom: 0px" Width="400px">
                    </asp:DropDownList>
                    <ajaxToolkit:ListSearchExtender ID="listaCurso_ListSearchExtender" 
                        runat="server" Enabled="True" PromptCssClass="ListSearchExtenderPrompt" 
                        PromptText="digite para procurar" QueryPattern="Contains" QueryTimeout="2000" 
                        TargetControlID="listaCurso">
                    </ajaxToolkit:ListSearchExtender>
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
                        AutoGenerateColumns="False" Caption="Lista de Recibos" 
                        CssClass="tableView" 
                        onpageindexchanging="grid_PageIndexChanging" onrowcreated="grid_RowCreated" 
                        onrowediting="grid_RowEditing" onsorting="grid_Sorting" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="id_recibo" HeaderText="nº" 
                                SortExpression="id_recibo">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                           
                                                       <asp:BoundField DataField="data" HeaderText="data" 
                                SortExpression="data" DataFormatString="{0:d}">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="curso" SortExpression="ReciboCurso.nome">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" 
                                        Text='<%# Bind("ReciboCurso.nome") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("ReciboCurso.nome") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="valor" HeaderText="valor" SortExpression="valor" 
                                DataFormatString="{0:c}" >
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nome" HeaderText="recebemos de" 
                                SortExpression="nome" >
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="descricao" HeaderText="referente a" 
                                SortExpression="descricao">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="strDocumentos" HeaderText="cpf/cnpj" 
                                >
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="usuário" 
                                SortExpression="Usuario.nome">
                               
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" 
                                        Text='<%# Bind("Usuario.nome") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:CheckBoxField DataField="status_recibo" HeaderText="status" 
                                SortExpression="status_recibo">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:CheckBoxField>
                            <asp:CommandField EditText="selecionar" ShowEditButton="True">
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:CommandField>
                        </Columns>
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
                                cadastro de recibos</th>
                            <th colspan="1">
                            <div id="dGravacao" runat="server">
                                <asp:Button ID="btInserir" runat="server" onclick="btInserir_Click" 
                                    Text="inserir" ValidationGroup="recibo" />
                                <asp:Button ID="btExcluir" runat="server" 
                                    onclick="btExcluir_Click" Text="cancelar recibo" 
                                    ValidationGroup="cancelar" />
                                <asp:Button ID="btCancelar" runat="server" CausesValidation="False" 
                                    onclick="btCancelar_Click" Text="limpar campos" ValidationGroup="recibo" />
                                <asp:ImageButton ID="btImprimirRecibo" runat="server" 
                                    ImageUrl="~/Styles/img/print.gif" onclick="btImprimirRecibo_Click" 
                                    style="width: 16px; height: 16px;" ToolTip="imprimir" />
                            </div>
                            </th>
                        </tr>
                        <asp:Panel ID="panelCampos" runat="server">
                         <tr>
                            <td class="esquerdo">
                                Recibo Nº:
                            </td>
                            <td class="direito">
                                <asp:TextBox ID="txtCodigo" runat="server" Enabled="False" 
                                    ValidationGroup="recibo" Width="80px">0</asp:TextBox>
                            </td>
                        </tr>
                                <tr>
                            <td class="esquerdo">
                                data:</td>
                            <td class="direito">
                                <cdt:cData ID="cData" runat="server" ValidationGroup="recibo" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                usuário:</td>
                            <td class="direito">
                                <asp:Label ID="txtUsuario" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                tipo inscrição:</td>
                            <td class="direito">
                                <asp:RadioButtonList ID="rbDocumento" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="rbDocumento_SelectedIndexChanged" 
                                    RepeatDirection="Horizontal" ValidationGroup="recibo">
                                </asp:RadioButtonList>
                            </td>
                        </tr>

                        <asp:Panel ID="divCnpj" runat="server">
                      <tr>
                        <td class="esquerdo">cnpj:</td>
                        <td class="direito">
                            <cCnpj:cCNPJ ID="cCNPJ2" runat="server" ValidationGroup="recibo" />
                          </td>
                      </tr>
                      </asp:Panel>
                      <asp:Panel ID="divCpf" runat="server">
                      <tr>
                        <td class="esquerdo">cpf:</td>
                        
                        <td class="direito">
                            <cCpf:cCPF ID="cCPF" runat="server" ValidationGroup="recibo" />
                          </td>
                      </tr>

                      </asp:Panel>

                    
                
                        <tr>
                            <td class="esquerdo">
                                recebemos de:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoNome" runat="server" EnableValidator="True" 
                                    MaxLength="100" Width="400" ValidationGroup="recibo" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                referente a:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoDescricao" runat="server" Height="100" MaxLength="200" 
                                    TextMode="MultiLine" Width="400" ValidationGroup="recibo" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                do curso/evento de:</td>
                            <td class="direito">
                                  <asp:DropDownList ID="lista" runat="server" DataTextField="nome" 
                    DataValueField="id_recibo_curso" AppendDataBoundItems="True" AutoPostBack="True" Width="400px"
                                      onselectedindexchanged="lista_SelectedIndexChanged">
                </asp:DropDownList>


<asp:ListSearchExtender ID="lista_ListSearchExtender" runat="server" 
    Enabled="True" PromptCssClass="ListSearchExtenderPrompt" 
    PromptText="digite para procurar" QueryPattern="Contains" QueryTimeout="2000" 
    TargetControlID="lista">
</asp:ListSearchExtender>


<asp:CompareValidator ID="cv" runat="server" 
    ErrorMessage="selecione um curso..." ForeColor="Red" Operator="NotEqual" 
    ValueToCompare="0" ControlToValidate="lista"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                pagamento através de:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoTipoPagto" runat="server" MaxLength="100" Visible="True" 
                                    Width="400" ValidationGroup="recibo" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                a importância supra de:</td>
                            <td class="direito">
                                <cvl:cValor ID="cValor" runat="server" ValidationGroup="recibo" />
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="esquerdo">
                                observação:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoObs" runat="server" Height="70" TextMode="MultiLine" 
                                    Width="400" MaxLength="200" EnableValidator="False" />
                            </td>
                        </tr>
                        </asp:Panel>
                        <tr>
                            <td colspan="2">
                                <div id="dMotivo" runat="server">
                                    <table width="100%">
                                        <tr>
                                            <td class="esquerdo">
                                                motivo do cancelamento:
                                            </td>
                                            <td class="direito">
                                                <ctx:cTexto ID="cTextoMotivo" runat="server" Height="70" TextMode="MultiLine" 
                                                    ValidationGroup="cancelar" Width="400" MaxLength="100" Enable="True" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th colspan="2">
                            <div id="dGravacao1" runat = "server">
                                <asp:Button ID="btInserir0" runat="server" onclick="btInserir_Click" 
                                    Text="inserir" ValidationGroup="recibo" />
                                <asp:Button ID="btExcluir0" runat="server" CausesValidation="True" 
                                    onclick="btExcluir_Click" Text="cancelar recibo" 
                                    ValidationGroup="cancelar" />
                                <asp:Button ID="btCancelar0" runat="server" CausesValidation="False" 
                                    onclick="btCancelar_Click" Text="limpar campos" ValidationGroup="recibo" />
                                &nbsp;<asp:ImageButton ID="btImprimirRecibo0" runat="server" 
                                    ImageUrl="~/Styles/img/print.gif" onclick="btImprimirRecibo_Click" 
                                    style="width: 16px; height: 16px;" ToolTip="imprimir" />
                            </div>
                         </th>
                        </tr>
                        
                        </table>
                    </asp:Panel>
                    <tr>
                    <td colspan="2">
                    
                    </td>
                    </tr>
                    
                    <tr>
                        <td colspan="2">
                               
                        </td>
                        </tr>
                        <asp:Panel ID="panelCheques" runat="server">
                                    <tr>
                                        <th colspan="2">
                                            cadastro de cheques
                                        </th>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div>
                                                <uc2:ControleReciboCheques ID="ControleReciboCheques1" runat="server" />
                                            </div>
                                        </td>
                                    </tr>

                        </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
</div>
</asp:Content>
