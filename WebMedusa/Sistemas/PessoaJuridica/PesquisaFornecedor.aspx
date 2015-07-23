<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true" CodeBehind="PesquisaFornecedor.aspx.cs" Inherits="Medusa.Sistemas.PessoaJuridica.PesquisaFornecedor" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="ControleHistoricoFornecedor.ascx" tagname="ControleHistoricoFornecedor" tagprefix="uc1" %>

<%@ Register src="ControleHistoricoDocumento.ascx" tagname="ControleHistoricoDocumento" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
    <div class="conteudo">
    <tr>
        <td colspan = "2">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>  
               <asp:CheckBox ID="ckFilter" runat="server" AutoPostBack="True" 
                        oncheckedchanged="ckFilter_CheckedChanged" 
            Text="habilitar múltiplos filtros" />
                <br />
                <asp:CheckBoxList ID="ckProperties" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="ckProperties_SelectedIndexChanged" 
                    RepeatDirection="Horizontal" RepeatLayout="Flow">
                </asp:CheckBoxList>
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
                        AutoPostBack="True" 
                        onselectedindexchanged="ddlOptions_SelectedIndexChanged">
                        <asp:ListItem Value="StrSocios">sócio</asp:ListItem>
                        <asp:ListItem Value="cnpj.Value">cnpj</asp:ListItem>
                        <asp:ListItem Value="nome">nome</asp:ListItem>
                        <asp:ListItem Value="nome_fantasia">nome fantasia</asp:ListItem>
                        <asp:ListItem Value="grupo_economico">grupo econômico</asp:ListItem>
                        <asp:ListItem Value="inscr_estadual">inscrição estadual</asp:ListItem>
                        <asp:ListItem Value="inscr_municipal">inscrição municipal</asp:ListItem>
                        <asp:ListItem Value="RamoAtividade.nome">ramo de atividade</asp:ListItem>
                        <asp:ListItem Value="Categoria.nome">categoria</asp:ListItem>
                        <asp:ListItem Value="StatusFornecedor.nome">status</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtProcura" runat="server" Width="137px"></asp:TextBox>
                    &nbsp;<asp:DropDownList ID="ddlMode" runat="server">
                    </asp:DropDownList>
                    <asp:Label ID="Label2" runat="server" Text="mostrar"></asp:Label>
                    &nbsp;&nbsp;<asp:DropDownList ID="ddlSize" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlSize_SelectedIndexChanged">
                        <asp:ListItem Selected="True">10</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>50</asp:ListItem>
                        <asp:ListItem Value="0">todos</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btSearch" runat="server" CausesValidation="False" 
                        onclick="btProcurar_Click" Text="procurar" />
                    &nbsp;<asp:ImageButton ID="btExportToExcel" runat="server" ImageUrl="~/Styles/img/excel_icon.png" onclick="btExportToExcel_Click" ToolTip="exportar para excel" Width="20px" />
                </div>
                <asp:Panel ID="panelGrid" runat="server">
                 <table class = "cadastro">
                    <tr>
                    <td colspan="2">
                    <asp:GridView ID="grid" runat="server" AllowPaging="True" AllowSorting="True" 
                        AutoGenerateColumns="False" Caption="Lista de Fornecedores" 
                        CssClass="tableView" 
                        onpageindexchanging="grid_PageIndexChanging" onrowcreated="grid_RowCreated" 
                        onrowediting="grid_RowEditing" onsorting="grid_Sorting" Width="100%" 
                        >
                        <Columns>
                            <asp:CommandField EditText="selecionar" ShowEditButton="True">
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:CommandField>
                            <asp:BoundField DataField="validade" DataFormatString="{0:d}" HeaderText="validade" SortExpression="validade">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="cnpj" SortExpression="cnpj.Value">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("cnpj.Value") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("cnpj.Value") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="nome" HeaderText="nome" 
                                SortExpression="nome">
                            <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nome_fantasia" HeaderText="nome fantasia" 
                                SortExpression="nome_fantasia" />
                            <asp:BoundField DataField="grupo_economico" HeaderText="grupo econômico" 
                                SortExpression="grupo_economico" />
                            <asp:BoundField DataField="inscr_estadual" HeaderText="inscrição estadual" 
                                SortExpression="inscr_estadual" />
                            <asp:BoundField DataField="inscr_municipal" HeaderText="inscrição municipal" 
                                SortExpression="inscr_municipal" />
                         <asp:TemplateField HeaderText="ramo de atividade" 
                                SortExpression="RamoAtividade.nome">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox4" runat="server" 
                                        Text='<%# Bind("RamoAtividade.nome") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("RamoAtividade.nome") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           <%-- <asp:TemplateField HeaderText="categoria" SortExpression="Categoria.nome">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Categoria.nome") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Categoria.nome") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>--%>
                            
                            <asp:TemplateField HeaderText="status" SortExpression="StatusFornecedor.nome">
                               
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" 
                                        Text='<%# Bind("StatusFornecedor.nome") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
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
                </td>
                </tr>
                </table>                            
                </asp:Panel>
                <asp:Panel ID="panelCadastro" runat="server">
                    <table class="cadastro">
                        <tr>
                            <th colspan="1">
                                cadastro de fornecedores</th>
                            <th colspan="1">
                            <div id="dGravacao" runat="server" style="display:none">
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
                                validade do cadastro:&nbsp;</td>
                            <td class="direito">
                                <asp:Label ID="lbValidade" runat="server" Text="validade"></asp:Label>
                                </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                cnpj:</td>
                            <td class="direito">
                                <asp:Label ID="LbCnpj" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                razão social:<asp:Label ID="txtCodigo" runat="server" Text="0" Visible="False"></asp:Label>
                            </td>
                            <td class="direito">
                                <asp:Label ID="LbNome" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="esquerdo">
                                denominação comercial / fantasia:</td>
                            <td class="direito">
                                <asp:Label ID="LbNomeFantasia" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">usuário:</td>
                            <td class="direito">
                                <asp:Label ID="lbUsuario" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">e-mail:</td>
                            <td class="direito">
                                <asp:Label ID="lbUsuarioEmail" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                grupo econômico:</td>
                            <td class="direito">
                                <asp:Label ID="LbGrupoEconomico" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="esquerdo">
                                endereço:</td>
                            <td class="direito">
                                <asp:Label ID="LbEndereco" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                telefone:</td>
                            <td class="direito">
                                <asp:Label ID="LbTelefone" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                home-page:</td>
                            <td class="direito">
                                <asp:LinkButton ID="LbHomePage" runat="server" CausesValidation="False"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                e-mail:</td>
                            <td class="direito">
                                <asp:Label ID="LbEmail" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                inscrição estadual:</td>
                            <td class="direito">
                                <asp:Label ID="LbEstadual" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                inscrição municipal:</td>
                            <td class="direito">
                                <asp:Label ID="LbMunicipal" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                ramo de atividade:&nbsp;</td>
                            <td class="direito">
                                <asp:Label ID="lbRamoAtividade" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                registro na junta comercial ou cartório de registro pj nº:</td>
                            <td class="direito">
                                <asp:Label ID="LbRegistroNumero" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                data última alteração:</td>
                            <td class="direito">
                                <asp:Label ID="LbDataAlteracao" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                número:</td>
                            <td class="direito">
                                <asp:Label ID="LbNumero" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                capital social:</td>
                            <td class="direito">
                                <asp:Label ID="LbCapital" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                ano balanço patrimonial:</td>
                            <td class="direito">
                                <asp:Label ID="LbAno" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                categoria:</td>
                            <td class="direito">
                                <asp:Label ID="LbCategoria" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                        <td class="esquerdo"> representantes legais:</td>
                            <td class="direito">
                                <asp:GridView ID="gridRepresentante" runat="server" AutoGenerateColumns="False" 
                                    CssClass="tableView" onrowediting="grid_RowEditing" Width="100%" 
                                    EmptyDataText="não há representante legal cadastrado!">
                                    <Columns>
                                        <asp:BoundField DataField="nome" HeaderText="nome" SortExpression="nome" />
                                        <asp:BoundField DataField="cpf" HeaderText="cpf" SortExpression="cpf" />
                                        <asp:BoundField DataField="rg" HeaderText="rg" SortExpression="rg" />
                                        <asp:TemplateField HeaderText="e-mail" SortExpression="email.value">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("email.value") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("email.value") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle" />
                                    <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle" />
                                    <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle" />
                                    <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle " />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo"> sócios:</td>
                            <td class="direito">
                                <asp:GridView ID="gridSocio" runat="server" AutoGenerateColumns="False" 
                                    CssClass="tableView" onrowediting="grid_RowEditing" 
                                    Width="100%" EmptyDataText="não há sócio cadastrado">
                                    <Columns>
                                        <asp:BoundField DataField="nome" HeaderText="nome" SortExpression="nome" />
                                        <asp:BoundField DataField="cpf" HeaderText="cpf" SortExpression="cpf" />
                                        <asp:BoundField DataField="rg" HeaderText="rg" SortExpression="rg" />
                                        <asp:TemplateField HeaderText="e-mail" SortExpression="email.value">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("email.value") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("email.value") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="cota" HeaderText="cota" SortExpression="cota" />
                                    </Columns>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle" />
                                    <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle" />
                                    <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle" />
                                    <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle " />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                           <td class="esquerdo"> diretores:</td>
                            <td class="direito">
                                <asp:GridView ID="gridDiretor" runat="server" AutoGenerateColumns="False" CssClass="tableView" 
                                    onrowediting="grid_RowEditing" Width="100%" 
                                    EmptyDataText="não há diretor cadastrado">
                                    <Columns>
                                        <asp:BoundField DataField="nome" HeaderText="nome" SortExpression="nome" />
                                        <asp:BoundField DataField="cpf" HeaderText="cpf" SortExpression="cpf" />
                                        <asp:BoundField DataField="rg" HeaderText="rg" SortExpression="rg" />
                                        <asp:TemplateField HeaderText="e-mail" SortExpression="email.value">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("email.value") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label7" runat="server" Text='<%# Bind("email.value") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle" />
                                    <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle" />
                                    <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle" />
                                    <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle " />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                     <div id="dGravacao1" style="display:none">
                            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                                <asp:Button ID="btInserir0" runat="server" onclick="btInserir_Click" 
                                    Text="inserir" />
                                <asp:Button ID="btAlterar0" runat="server" onclick="btAlterar_Click" 
                                    Text="salvar" />
                                <asp:Button ID="btExcluir0" runat="server" CausesValidation="False" 
                                    onclick="btExcluir_Click" Text="excluir" />
                                <asp:Button ID="btCancelar0" runat="server" CausesValidation="False" 
                                    onclick="btCancelar_Click" Text="cancelar" />
                            </div>
                        </td>
                        </tr>
                        <tr>
                        <td colspan="2">
                                     <uc1:ControleHistoricoFornecedor ID="ControleHistoricoFornecedor1" 
                                         runat="server" />
                        </td>
                        </tr>
                            

                        <tr>
                        <td colspan="2">
                            <uc2:ControleHistoricoDocumento ID="ControleHistoricoDocumento2" 
                                runat="server" />
                        </td>
                        </tr>
                 
                     
                    </table>
                </asp:Panel>
          
            </ContentTemplate>
             <Triggers>
                   <asp:PostBackTrigger ControlID="btExportToExcel"  />
                   </Triggers>
        </asp:UpdatePanel>
        <AjaxToolKit:UpdatePanelAnimationExtender ID="upae" BehaviorID="animation" runat="server" TargetControlID="UpdatePanel1" Enabled="True">
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
        </AjaxToolKit:UpdatePanelAnimationExtender>
</div>
</asp:Content>
