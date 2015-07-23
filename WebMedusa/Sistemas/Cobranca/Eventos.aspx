<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true" CodeBehind="Eventos.aspx.cs" Inherits="Medusa.Sistemas.Cobranca.Eventos" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="ControleBoletos.ascx" tagname="ControleBoletos" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(function ()
        {
            function openDialog()
            {
                var dlg = $("#modalSacados").dialog({
                    width: 800,
                    height: 470,
                    position: 'top',
                    modal: true,
                    title: 'escolher os sacados'
                });
                dlg.parent().appendTo(jQuery("form:first"));
                $("#modalSacados").dialog("open");
            }

            $('#open').live("click", function ()
            {
                openDialog();
            });
        });
        function openDialogBoletos()
        {
            var dlg = $("#modalBoletos").dialog({
                width: 900,
                height: 500,
                position: 'top',
                modal: true,
                title: 'boletos de cobrança'
            });
            dlg.parent().appendTo(jQuery("form:first"));
            $("#modalBoletos").dialog("open");
        }

        
    </script>
    <div class="conteudo">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <div id="modalSacados" style="display: none" >
            <asp:UpdatePanel ID="filtrarSacados" runat="server">
                <ContentTemplate>
                    <cPesqSacado:cPesqSacado ID="cPesqSacado1" runat="server" />
                
                            <asp:Button ID="btOk" runat="server" Text="selecionar os sacados" OnClientClick="$('#modalSacados').dialog('close');"
                            OnClick="btOk_Click" CausesValidation="false" />
                    <asp:Button ID="BtCancel" runat="server" Text="cancelar" OnClientClick="$('#modalSacados').dialog('close');" CausesValidation="false" />
                  
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

           <div id="modalBoletos" style="display: none">
                       <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>           
                                <uc1:ControleBoletos ID="ControleBoletos1" runat="server" />
                         
                            </ContentTemplate>
                       </asp:UpdatePanel>       
                                 
            </div>

    
    
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
                
              
                    <asp:Label ID="Label1" runat="server" Text="procurar"></asp:Label>
                    &nbsp;<asp:DropDownList ID="ddlOptions" runat="server" CausesValidation="True" 
                        AutoPostBack="True" onselectedindexchanged="ddlOptions_SelectedIndexChanged">
                        <asp:ListItem Value="id_evento">codigo</asp:ListItem>
                        <asp:ListItem Value="nome">nome</asp:ListItem>
                        <asp:ListItem Value="descricao">descrição</asp:ListItem>
                        <asp:ListItem Value="instrucao">instrução</asp:ListItem>
                        <asp:ListItem Value="Projeto.codigo">projeto</asp:ListItem>
                        <asp:ListItem Value="Conta.numero">conta</asp:ListItem>
                        <asp:ListItem>total</asp:ListItem>
                        <asp:ListItem Value="qtde_parcelas">parcelas</asp:ListItem>
                        <asp:ListItem Value="inicio_cobranca">inicio cobrança</asp:ListItem>
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
                        AutoGenerateColumns="False" Caption="Lista de Eventos" 
                        CssClass="tableView" 
                        onpageindexchanging="grid_PageIndexChanging" onrowcreated="grid_RowCreated" 
                        onrowediting="grid_RowEditing" onsorting="grid_Sorting" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="id_evento" HeaderText="código" 
                                SortExpression="id_evento">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nome" HeaderText="nome" 
                                SortExpression="nome">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="descricao" HeaderText="descrição" 
                                SortExpression="descricao" >
                            <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="instrucao" HeaderText="instrução" 
                                SortExpression="instrucao" />
                            <asp:TemplateField HeaderText="conta" SortExpression="Conta.numero">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Conta.numero") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Conta.numero") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="projeto" SortExpression="Projeto.codigo">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" 
                                        Text='<%# Bind("Projeto.HtmlPaginaProjeto") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" 
                                        Text='<%# Bind("Projeto.HtmlPaginaProjeto") %>'></asp:Label>
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
                                cadastro de eventos</th>
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
                                código:
                            </td>
                            <td class="direito">
                                <asp:Label ID="txtCodigo" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                nome:
                            </td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoNome" runat="server" EnableValidator="True" 
                                    MaxLength="50" Width="300" TextMode="SingleLine" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                descrição:</td>
                            <td class="direito">
                                <asp:TextBox ID="cTextoDescricao" runat="server" TextMode="MultiLine" 
                                    Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                instrução:
                            </td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoInstrucao" runat="server" EnableValidator="True" 
                                    Width="300" TextMode="MultiLine" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                Conta:</td>
                            <td class="direito">
                                <cDdlConta:cDdlConta ID="cTextoConta" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                projeto</td>
                            <td class="direito">
                                <cddlProjeto:cDdlProjeto ID="cDdlProjeto1" runat="server" />
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
                <asp:Panel ID="pSacados" runat="server">
                      <table class="cadastro">
                        <tr>
                            <th colspan="1">
                                sacados
                            </th>
                            <th colspan="1">
                                <a id="open" href="#">escolher os sacados</a>
                            </th>                         
                        </tr>
                        <tr>
                        <td colspan="2" class="esquerdo">
                            procurar sacado:
                            <asp:TextBox ID="txtProcurarSacado" runat="server" Width="300px"></asp:TextBox>
                            <asp:Button ID="btProcuraSacados" runat="server" 
                                OnClick="btProcuraSacados_Click" Text="procurar" />
                            <asp:ImageButton ID="btPrint" runat="server" ImageUrl="~/Styles/img/print.gif" 
                                onclick="btPrint_Click" ToolTip="imprimir todos os boletos" />
                            &nbsp;<asp:ImageButton ID="btEnviarEmail" runat="server" 
                                ImageUrl="~/Styles/img/icone_email.gif" onclick="btEnviarEmail_Click" 
                                style="height: 20px" ToolTip="enviar boleto por e-mail " Width="20px" />
                        </td>
                        
                        </tr>
                          <tr>
                              <td class="esquerdo">
                                  quantidade de parcelas:</td>
                              <td class="direito">
                                  <cint:cInteiro ID="cInteiroQtdade" runat="server" ValidationGroup="gerar" />
                              </td>
                          </tr>
                          <tr>
                              <td class="esquerdo">
                                  nº da primeira parcela:</td>
                              <td class="direito">
                                  <cint:cInteiro ID="cInteiroPrimeira" runat="server" ValidationGroup="gerar"/>
                              </td>

                          </tr>
                          <tr>
                              <td class="esquerdo">
                                  vencimento inicial:</td>
                              <td class="direito">
                                  <cdt:cData ID="cDataVenctoInicial" runat="server" ValidationGroup="gerar"/>
                              </td>
                          </tr>
                          <tr>
                              <td class="esquerdo">
                                  valor:</td>
                              <td class="direito">
                                  <cvl:cValor ID="cValorParcela" runat="server" ValidationGroup="gerar"/>
                              </td>
                          </tr>
                          <tr>
                               <td colspan="2" class="esquerdo">
                           
                                   <asp:Button ID="btGerarParcela" runat="server" Text="gerar parcelas" 
                                       ValidationGroup="gerar" onclick="btGerarParcela_Click" />
                               </td>
                          </tr>
                        <tr>
                        <td colspan="2">
                            <asp:GridView ID="gridSacadosEvento" runat="server" AutoGenerateColumns="False" 
                                Caption="Lista de Sacados" Width="100%" 
                                AllowPaging="True" 
                                onpageindexchanging="gridSacadosEvento_PageIndexChanging" 
                                onrowdeleting="gridSacadosEvento_RowDeleting" 
                                onrowediting="gridSacadosEvento_RowEditing">
                                <Columns>
                                    <asp:TemplateField HeaderText="nome" >
                                     
                                        <ItemTemplate>
                                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("Sacado.HtmlPaginaSacados") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="cpf" >
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" 
                                                Text='<%# Bind("Sacado.PessoaFisica.cpf.Value") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="rg">
                                      
                                        <ItemTemplate>
                                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("Sacado.PessoaFisica.rg") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ShowHeader="False">
                                     
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" OnClientClick="openDialogBoletos()"
                                                CommandName="Edit" Text="selecionar"></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" Width="70px" />
                                        <ItemStyle HorizontalAlign="Right" Width="70px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                                                CommandName="Delete" Text="excluir"></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle 
                                    HorizontalAlign="Left" />
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
                        
                     
                    </table>
            </asp:Panel>
            </ContentTemplate>            
        </asp:UpdatePanel>
           <ajaxToolkit:UpdatePanelAnimationExtender ID="upae" BehaviorID="animation" runat="server"
            TargetControlID="UpdatePanel1" Enabled="True">
            <Animations>
            <OnUpdating>
                <Parallel duration="0">                    
                    <FadeOut minimumOpacity=".5" />
                    <ScriptAction Script="waitingDialog({});" />  
                 </Parallel>
            </OnUpdating>
            <OnUpdated>
                <Parallel duration="0">
                   <FadeIn minimumOpacity=".5" />  
                    <ScriptAction Script="closeWaitingDialog()" /> 
                </Parallel> 
            </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>


      
</div>
</asp:Content>
