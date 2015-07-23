<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControleContratoAutonomo.ascx.cs" Inherits="Medusa.Sistemas.Folha.ControleContratoAutonomo" %>
<%@ Register src="../../Controles/DdlTipoContratos.ascx" tagname="DdlTipoContratos" tagprefix="uc1" %>
<%@ Register src="../../Controles/Contratos/ControleContratoPagamento.ascx" tagname="ControleContratoPagamento" tagprefix="cControleContratoPagamento" %>

  <script type="text/javascript">
     
          function openDialog() {
              var dlg = $("#modalPagamento").dialog({
                  width: 850,
                  height: 470,
                  position: 'top',
                  modal: true,
                  title: 'cadastro de pagamentos'
              });
              dlg.parent().appendTo(jQuery("form:first"));
              $("#modalPagamento").dialog("open");
          }

          $('#open').live("click", function () {
              openDialog();
          });
    </script>   
<div class="conteudo">

         <div id="modalPagamento" style="display: none">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    
                    <cControleContratoPagamento:ControleContratoPagamento ID="ControleContratoPagamento" 
                        runat="server" />
                    
                </ContentTemplate>
            </asp:UpdatePanel>
         </div>

               <asp:CheckBox ID="ckFilter" runat="server" AutoPostBack="True" 
                        oncheckedchanged="ckFilter_CheckedChanged" 
            Text="habilitar múltiplos filtros" />
        <asp:DataList ID="DataListFiltros" runat="server" RepeatColumns="6"
            ondatabinding="DataListFiltros_DataBinding" RepeatDirection="Horizontal" 
            OnDeleteCommand="DataListFiltros_DeleteCommand" Height="16px">
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
                    
                    <asp:Label ID="Label1" runat="server" Text="procurar"></asp:Label>
                    &nbsp;<asp:DropDownList ID="ddlOptions" runat="server" CausesValidation="True" 
                        AutoPostBack="True" onselectedindexchanged="ddlOptions_SelectedIndexChanged">
                        <asp:ListItem Value="Projeto.cod_def_projeto">projeto</asp:ListItem>
                        <asp:ListItem Value="TipoContrato.nome">tipo</asp:ListItem>
                        <asp:ListItem Value="inicio">descrição</asp:ListItem>
                        <asp:ListItem Value="termino">início</asp:ListItem>
                        <asp:ListItem>valor</asp:ListItem>
                        <asp:ListItem Value="descricao">término</asp:ListItem>
<asp:ListItem Value="observacao">observação</asp:ListItem>
                        <asp:ListItem>ativo</asp:ListItem>
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
                        AutoGenerateColumns="False" Caption="Lista de Contratos" 
                        CssClass="tableView" 
                        onpageindexchanging="grid_PageIndexChanging" onrowcreated="grid_RowCreated" 
                        onrowediting="grid_RowEditing" onsorting="grid_Sorting" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="projeto" 
                                SortExpression="Projeto.cod_def_projeto">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" 
                                        Text='<%# Bind("Projeto.cod_def_projeto") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" 
                                        Text='<%# Bind("Projeto.cod_def_projeto") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="inicio" HeaderText="início" 
                                SortExpression="inicio" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="termino" HeaderText="término" 
                                SortExpression="termino" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="valor" HeaderText="valor" SortExpression="valor" />
                            <asp:BoundField DataField="tipo" HeaderText="tipo" 
                                SortExpression="tipo" />
                            <asp:BoundField DataField="descricao" HeaderText="descrição" 
                                SortExpression="descricao" />
                            <asp:BoundField DataField="observacao" HeaderText="observação" 
                                SortExpression="observacao" />
                            <asp:CheckBoxField DataField="ativo" HeaderText="ativo" 
                                SortExpression="ativo" />
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
</div>


<asp:Panel ID="panelCadastro" runat="server">
    <table class="cadastro">
    <tr>
    <th colspan="1"  align="left">
                                cadastro de contratos</th>
                            <th colspan="1" >
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
            <td>
                tipo de contrato:</td>
            <td>
                <uc1:DdlTipoContratos ID="DdlTipoContratos" runat="server" />
            </td>
        </tr>
    <tr>
    <td>
        projeto:<asp:Label ID="txtCodigoContrato" runat="server" Text="0" 
            Visible="False"></asp:Label>
        </td>
    <td>
        <cddlProjeto:cDdlProjeto ID="cDdlProjeto" runat="server" />
    </td>
    </tr>
        <tr>
            <td>
                tipo:</td>
            <td>
                <asp:RadioButtonList ID="RbTipo" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem>contrato</asp:ListItem>
                    <asp:ListItem Value="cert">CERT</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>
                inicio do contrato:</td>
            <td>
                <cdt:cData ID="cDataInicio" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                término do contrato:</td>
            <td>
                <cdt:cData ID="cDataTermino" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                valor:</td>
            <td>
                <cvl:cValor ID="cValor" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                descrição:</td>
            <td>
                <ctx:cTexto ID="cTextoDescricao" runat="server" TextMode="MultiLine" 
                    Width="500px" Height="100px" />
            </td>
        </tr>
        <tr>
            <td>
                observação:</td>
            <td>
                <ctx:cTexto ID="cTextoObs" runat="server" TextMode="MultiLine" Width="500px" 
                    EnableValidator="False" Height="100px" />
            </td>
        </tr>
        <tr>
            <td>
                data de entrega do relatório:
            </td>
            <td>
                <cdt:cData ID="cDataRelatorio" runat="server" EnableValidator="False" />
            </td>
        </tr>
        <tr>
            <td>
                ativo:</td>
            <td>
                <asp:RadioButtonList ID="RbAtivo" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="True">sim</asp:ListItem>
                    <asp:ListItem Value="False">não</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
                            <th colspan="2">
                                <div id="dGravacao1" >
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
        <tr>
            <td colspan="1">
                 pagamentos
            </td>
            <td colspan="1">
                 <a id="open" href="#">novo pagamento</a>
            </td>
        </tr>
    </table>
</asp:Panel>

