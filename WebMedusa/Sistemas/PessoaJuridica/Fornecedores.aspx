<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/SitePJ.Master" AutoEventWireup="true" CodeBehind="Fornecedores.aspx.cs" Inherits="Medusa.Sistemas.PessoaJuridica.Fornecedores" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>


<%@ Register src="ControleHistoricoFornecedor.ascx" tagname="ControleHistoricoFornecedor" tagprefix="uc1" %>


<%@ Register src="ControleSocios.ascx" tagname="ControleSocios" tagprefix="uc2" %>


<%@ Register src="ControleRepresentanteLegal.ascx" tagname="ControleRepresentanteLegal" tagprefix="uc3" %>
<%@ Register src="ControleDiretor.ascx" tagname="ControleDiretor" tagprefix="uc4" %>


<%@ Register src="ControleReferenciaBancaria.ascx" tagname="ControleReferenciaBancaria" tagprefix="uc5" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
   
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="conteudo">
       
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Path="../../Scripts/FixFocus.js" />   
        </Scripts>
        </asp:ScriptManager>
       
       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
               <asp:CheckBox ID="ckFilter" runat="server" AutoPostBack="True" 
                        oncheckedchanged="ckFilter_CheckedChanged" 
            Text="habilitar múltiplos filtros" />
                <br />
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
                        <asp:ListItem Value="id_fornecedor">código</asp:ListItem>
                        <asp:ListItem Value="cnpj.Value">cnpj</asp:ListItem>
                        <asp:ListItem Value="nome">nome</asp:ListItem>
                        <asp:ListItem Value="nome_fantasia">nome fantasia</asp:ListItem>
                        <asp:ListItem Value="inscr_estadual">inscrição estadual</asp:ListItem>
                        <asp:ListItem Value="inscr_municipal">inscrição municipal</asp:ListItem>
                        <asp:ListItem Value="RamoAtividade.nome">ramo de atividade</asp:ListItem>
                        <asp:ListItem Value="Categoria.nome">categoria</asp:ListItem>
                        <asp:ListItem Value="StatusFornecedor.nome">status</asp:ListItem>
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
                        onclick="btCriar_Click" Text="novo" Width="80px" />
                    &nbsp;</div>
                <asp:Panel ID="panelGrid" runat="server">
                                             
                    <asp:GridView ID="grid" runat="server" AllowPaging="True" AllowSorting="True" 
                        AutoGenerateColumns="False" Caption="Lista de Fornecedores" 
                        CssClass="tableView" 
                        onpageindexchanging="grid_PageIndexChanging" onrowcreated="grid_RowCreated" 
                        onrowediting="grid_RowEditing" onsorting="grid_Sorting" Width="100%" 
                        >
                        <Columns>
                           <%-- <asp:TemplateField HeaderText="categoria" SortExpression="Categoria.nome">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Categoria.nome") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Categoria.nome") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>--%>
                            
                            <asp:BoundField DataField="id_fornecedor" HeaderText="código" 
                                SortExpression="id_fornecedor" >
                            <HeaderStyle HorizontalAlign="Left" />
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
                                SortExpression="nome_fantasia" >
                            <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="inscr_estadual" HeaderText="inscrição estadual" 
                                SortExpression="inscr_estadual" >
                            <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="inscr_municipal" HeaderText="inscrição municipal" 
                                SortExpression="inscr_municipal" >
                            <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                         <asp:TemplateField HeaderText="ramo de atividade" 
                                SortExpression="RamoAtividade.nome">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox4" runat="server" 
                                        Text='<%# Bind("RamoAtividade.nome") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("RamoAtividade.nome") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            
                            <asp:BoundField DataField="validade" DataFormatString="{0:d}" 
                                HeaderText="validade" SortExpression="validade">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            
                            <asp:TemplateField HeaderText="status" SortExpression="StatusFornecedor.nome">
                               
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" 
                                        Text='<%# Bind("StatusFornecedor.nome") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:CommandField EditText="selecionar" ShowEditButton="True">
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Center" />
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
        CADASTRO DE FORNECEDOR
                   
                    <table class="cadastro">
                        <tr>
                            <th colspan="1">
                                informações básicas</th>
                            <th colspan="1">
                            <div id="dGravacao" runat="server">
                                <asp:Button ID="btInserir" runat="server" onclick="btInserir_Click" 
                                    Text="inserir" Visible="False" />
                                <asp:Button ID="btAlterar" runat="server" onclick="btAlterar_Click" 
                                    Text="salvar" Visible="False" CausesValidation="False" />
                                <asp:Button ID="btCancelar" runat="server" 
                                    onclick="btCancelar_Click" Text="cancelar" CausesValidation="False" />
                            </div>
                            </th>
                        </tr>

                        <tr>
                            <td class="esquerdo">
                                código:</td>
                            <td class="direito" >
                                <asp:Label ID="txtCodigo" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>

                        <tr>
                            <td class="esquerdo">
                                cnpj:</td>
                            <td class="direito">
                                <cCnpj:cCNPJ ID="cCNPJ" runat="server" />
                            </td>
                        </tr>

                        <tr>
                            <td class="esquerdo" style="height: 34px">
                                razão social:</td>
                            <td class="direito" style="height: 34px">
                                <ctx:cTexto ID="cTextoNome" runat="server" Width="500px" MaxLength="100" 
                                   />
                            </td>
                        </tr>

                        <tr>
                            <td class="esquerdo">
                                denominação comercial / fantasia:</td>
                        
                            <td class="direito">
                                <ctx:cTexto ID="cTextoFantasia" runat="server" Width="500px" 
                                    />
                            </td>
                        </tr>

                        <tr>
                            <td class="esquerdo">
                                grupo econômico:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoGrupoEconomico" runat="server" Width="500px" 
                                     />
                            </td>
                        </tr>
                        </table>

<asp:Panel ID="panelCadastro2" runat="server" Visible="false">
                      
                                <cender:cEnder ID="cEnder1" runat="server"  />
                                                     
<table class="cadastro">
                       
                        <tr>
                        
                        <td colspan="2">
                            <uc3:ControleRepresentanteLegal ID="ControleRepresentanteLegal1" 
                                runat="server" />
                            </td>
                        </tr>
                        
                        <tr>
                            <td colspan="2">
                                <uc2:ControleSocios ID="ControleSocios1" runat="server" />
                            </td>
                        </tr>
                        
                        <tr>
                            <td colspan="2">
                                <uc4:ControleDiretor ID="ControleDiretor1" runat="server" />
                            </td>
                        </tr>
                        
                        <tr>
                        <th colspan="2">
                            contatos</th>
                        </tr>

                        <tr>
                            <td class="esquerdo">
                                telefone:</td>
                            <td class="direito">
                                <ctel:cTelefone ID="cTelefone1" runat="server" 
                                     />
                            </td>
                        </tr>

                        <tr>
                            <td class="esquerdo">
                                home-page:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoHomePage" runat="server" Width="350px" 
                                    ErrorMsg="digite a home page" EnableTheming="True" MaxLength="50" ValidationGroup="contatos" 
                                   />
                            </td>
                        </tr>

                        <tr>
                            <td class="esquerdo" >
                                e-mail:</td>
                            <td class="direito" >
                                <cem:cEmail ID="cEmail1" runat="server"  />                            </td>
                        </tr>
                        <tr>
                        <th colspan="2">outras informações</th>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                inscrição estadual:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoEstadual" runat="server" Width="165" MaxLength="50" 
                                    ErrorMsg="digite a inscrição estadual"/>
                            </td>
                        </tr>

                        <tr>
                            <td class="esquerdo">
                                inscrição municipal:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoMunicipal" runat="server" Width="165" MaxLength="50" 
                                    ErrorMsg="digite a inscrição municipal"/>
                            </td>
                        </tr>

                        <tr>
                            <td class="esquerdo">
                                ramo de atividade:</td>
                            <td class="direito">
                            
                            <asp:DropDownList ID="ListaRamoAtividade" runat="server" DataTextField="strRamoAtividade" 
                                DataValueField="id_ramo_atividade" AppendDataBoundItems="True" Width="500px">
                            </asp:DropDownList>
                            <AjaxToolKit:ListSearchExtender ID="lista_ListSearchExtender" runat="server" 
                                Enabled="True" PromptCssClass="ListSearchExtenderPrompt" 
                                PromptText="digite para procurar" QueryPattern="Contains" QueryTimeout="2000" 
                                TargetControlID="ListaRamoAtividade">
                            </AjaxToolKit:ListSearchExtender>
                            <asp:CompareValidator ID="cvRamoAtividade" runat="server" 
                                ErrorMessage="selecione um ramo de atividade..." ForeColor="Red" Operator="NotEqual" 
                                ValueToCompare="0" ControlToValidate="ListaRamoAtividade"></asp:CompareValidator>    
                            </td>
                        </tr>

                        <tr>
                            <td class="esquerdo">
                                registro na junta comercial ou<br /> cartório de registro pj nº:</td>
                            <td class="direito">
                                 <ctx:cTexto ID="cTextoRegistro" runat="server" Width="165px" MaxLength="30"
                                    EnableViewState="False" ErrorMsg="registro na junta comercial obrigatório" 
                                     />
                                data última alteração:&nbsp;
                                <cdt:cData ID="cDataAlteracao" runat="server" EnableTheming="False" ErrorMsg="data ultima alteração obrigatória" 
                                     />
                                &nbsp;número:
                               <ctx:cTexto ID="cTextoNumero" runat="server" Width="50px" ErrorMsg="número de alterações obrigatório"  MaxLength="30"
                                    />
                            </td>
                        </tr>

                        <tr>
                            <td class="esquerdo">
                                capital social:</td>
                            <td class="direito">
                                <asp:TextBox ID="txtValorCapitalSocial" runat="server" 
                                    onkeyup="formataValor(this,event);"></asp:TextBox>
                                &nbsp;<asp:RequiredFieldValidator ID="rfvValorCapitalSocial" runat="server" 
                                    ControlToValidate="txtValorCapitalSocial" 
                                    ErrorMessage="capital social obrigatório" EnableTheming="False" 
                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                &nbsp;<%--<asp:CompareValidator ID="cvCapitalSocial" runat="server" 
                                    ControlToValidate="txtValorCapitalSocial" ErrorMessage="maior que zero" 
                                    ForeColor="Red" Operator="GreaterThan" Type="Double" ValueToCompare="0"></asp:CompareValidator>--%></td>
                        </tr>

                        <tr>
                            <td class="esquerdo">
                                ano balanço patrimonial:</td>
                            <td class="direito">
                                <cDdlAno:cDdlAno ID="cDdlAno1" runat="server" />
                            </td>
                        </tr>
                  

                        </table>
                          <uc5:ControleReferenciaBancaria ID="ControleReferenciaBancaria2" 
                                                runat="server" />
                                                
<table class="cadastro">
<tr>
<td></td>
</tr>
                                     <tr>
                   <th colspan="2">categoria</th>
                   </tr>
                        <tr>
                            <td class="esquerdo">
                                cadastro para participar de processo de :</td>
                            <td class="direito">
                                
                                <asp:DropDownList ID="listaCategoria" runat="server" 
                                    AppendDataBoundItems="True" AutoPostBack="True" DataTextField="nome" 
                                    DataValueField="id_categoria" 
                                    onselectedindexchanged="listaCategoria_SelectedIndexChanged" Width="500px">
                                </asp:DropDownList>
                                <asp:CompareValidator ID="cv" runat="server" ControlToValidate="listaCategoria" 
                                    ErrorMessage="selecione uma categoria..." ForeColor="Red" Operator="NotEqual" 
                                    ValueToCompare="0"></asp:CompareValidator>
                            </td>
                        </tr>

                        <tr>
                            <td class="esquerdo">
                                documentos para serem encaminhados para a FUSP:</td>
                            <td class="direito">
                                <asp:ListBox ID="listDocumentos" runat="server" DataTextField="nome" 
                                    Width="100%"></asp:ListBox>
                            </td>
                        </tr>
                                </table>

<table class="cadastro">
                        <tr>
                            <td>
                              <div id="dFocus">
                                <asp:ValidationSummary ID="vsErros" runat="server" ForeColor="Red" />
                                 </div>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                       
                    
                                
                                </table>
</asp:Panel>

<table class="cadastro">
                                
                               
                                <tr>
                                    <th>
                                        <div ID="dGravacao1" runat="server">
                                            <asp:Button ID="btInserir0" runat="server" onclick="btInserir_Click" 
                                                Text="inserir" Visible="False" />
                                            <asp:Button ID="btAlterar0" runat="server" CausesValidation="False" 
                                                onclick="btAlterar_Click" Text="salvar" Visible="False" />
                                            <asp:Button ID="btCancelar0" runat="server" CausesValidation="False" 
                                                onclick="btCancelar_Click" Text="cancelar" />
                                        </div>
                                        <asp:Button ID="btEnviar" runat="server" CausesValidation="False" 
                                            EnableTheming="True" onclick="btEnviar_Click" Text="enviar para fusp" />
                                        <asp:Button ID="btImprimir" runat="server" CausesValidation="False" 
                                            onclick="btImprimir_Click" Text="imprimir" Visible="False" />
                                        <asp:Button ID="btImprimirCRC" runat="server" CausesValidation="False" 
                                            onclick="btImprimirCRC_Click" Text="imprimir CRC" Visible="False" />
                                    </th>
                                </tr>
</table>

 

                            <asp:Panel ID="panelHistorico" runat="server" Visible="true">
                   
                                <table class="cadastro">
                                <tr>
                                <th colspan="2">
                                validade do cadastro
                                </th>
                                </tr>
                                 <tr>
                                    <td class="esquerdo">
                                        validade:</td>
                                    <td class="direito">
                                        <asp:Label ID="lbValidade" runat="server" Text="lbValidade"></asp:Label>
                                    </td>
                                </tr>

                                    <tr>
                                        <td colspan="2">
                                  
                                            <asp:GridView ID="gridHistorico" runat="server" AutoGenerateColumns="False" 
                                                Caption="Histórico" CssClass="tableView" Width="100%">
                                                <Columns>
                                                    <asp:BoundField DataField="data" HeaderText="data" SortExpression="data">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="observacao" HeaderText="observação" 
                                                        SortExpression="observacao">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="status" SortExpression="StatusFornecedor.nome">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox2" runat="server" 
                                                                Text='<%# Bind("StatusFornecedor.nome") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label2" runat="server" 
                                                                Text='<%# Bind("StatusFornecedor.nome") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="usuário" 
                                                        SortExpression="Usuario.PessoaFisica.nome">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox1" runat="server" 
                                                                Text='<%# Bind("Usuario.PessoaFisica.nome") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" 
                                                                Text='<%# Bind("Usuario.PessoaFisica.nome") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
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
                                 <asp:GridView ID="gridDocumentos" runat="server" 
                            AutoGenerateColumns="False" CssClass="tableView" Width="100%" 
                                Caption="Documentos" >
                                <Columns>
                                    <asp:BoundField DataField="data" HeaderText="data" SortExpression="data" />
                                    <asp:TemplateField HeaderText="documento" 
                                        SortExpression="documentocategoria.nome">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" 
                                                Text='<%# Bind("documentocategoria.nome") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" 
                                                Text='<%# Bind("documentocategoria.nome") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="numero" HeaderText="número" 
                                        SortExpression="numero" />
                                    <asp:BoundField DataField="validade" DataFormatString="{0:d}" 
                                        HeaderText="validade" SortExpression="validade" />
                                </Columns>
                                <HeaderStyle HorizontalAlign="Left" />
                                <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle" />
                                <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle" />
                                <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle" />
                                <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle " />
                            </asp:GridView>      
                                      </td>    
                                    
                                    </tr>
                                </table>
                            </asp:Panel>
</asp:Panel>

            </ContentTemplate>
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
