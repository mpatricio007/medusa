<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true" CodeBehind="ArquivosRetornos.aspx.cs" Inherits="Medusa.Sistemas.RemessaBancaria.ArquivosRetornos" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="conteudo">
       <script type="text/javascript">
           function showProgressDialog() {

               $("#progressDialog").dialog({
                   autoOpen: false,
                   modal: true,
                   bgiframe: true
               });

               $('#progressDialog').dialog('open');

           }

           function hideProgressDialog() {
               if ($('#progressDialog').dialog('isOpen')) {
                   $('#progressDialog').dialog('close');
               }
           }
          
           function onProcessar() {
               showProgressDialog();               
               
           }
    </script>
    <div id="progressDialog" title="Processando" style="display: none;">
    <img src="../../Styles/img/loading2.gif" alt="Processing"/>
    <p>
        Aguarde...
    </p>
</div>


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
                        <asp:ListItem Value="id_arquivo">código</asp:ListItem>
                        <asp:ListItem Value="data_processado">data processado</asp:ListItem>
                        <asp:ListItem Value="codigo_banco">código banco</asp:ListItem>
                        <asp:ListItem Value="data_criacao">data de criação</asp:ListItem>
                        <asp:ListItem Value="TipoConciliacao.nome">tipo de conciliação</asp:ListItem>
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
                    <asp:Button ID="btConciliar" runat="server" onclick="btConciliar_Click" 
                        Text="conciliar arquivos de retorno" />
                </div>
                <asp:Panel ID="panelGrid" runat="server">
                          
                            
                    <asp:GridView ID="grid" runat="server" AllowPaging="True" AllowSorting="True" 
                        AutoGenerateColumns="False" Caption="Lista de Arquivos de Retornos" CellPadding="4" 
                        CssClass="tableView" ForeColor="#333333" GridLines="None" 
                        onpageindexchanging="grid_PageIndexChanging" onrowcreated="grid_RowCreated" 
                        onrowediting="grid_RowEditing" onsorting="grid_Sorting" Width="100%" 
                        HorizontalAlign="Left">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="id_arquivo" HeaderText="código" 
                                SortExpression="id_arquivo" >
                            <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="data_processado" HeaderText="data processado" 
                                SortExpression="data_processado" DataFormatString="{0:d}">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="codigo_banco" HeaderText="código banco" 
                                SortExpression="codigo_banco">
                            <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="data_criacao" HeaderText="data criação" 
                                SortExpression="data_criacao">
                            <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="tipo conciliação" 
                                SortExpression="TipoConciliacao.nome">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" 
                                        Text='<%# Bind("TipoConciliacao.nome") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" 
                                        Text='<%# Bind("TipoConciliacao.nome") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="tipo arquivo" SortExpression="TipoArquivo.nome">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" 
                                        Text='<%# Bind("TipoArquivo.nome") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("TipoArquivo.nome") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="usuário" 
                                SortExpression="Usuario.PessoaFisica.nome">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox3" runat="server" 
                                        Text='<%# Bind("Usuario.PessoaFisica.nome") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" 
                                        Text='<%# Bind("Usuario.PessoaFisica.nome") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="log_importacao" HeaderText="log" 
                                SortExpression="log_importacao">
                            <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:CommandField EditText="selecionar" ShowEditButton="True">
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:CommandField>
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" 
                            HorizontalAlign="Left" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" 
                            CssClass="SortedAscendingCellStyle" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" 
                            CssClass="SortedAscendingHeaderStyle" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" 
                            CssClass="SortedDescendingCellStyle" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" 
                            CssClass="SortedDescendingHeaderStyle " />
                    </asp:GridView>
                </asp:Panel>
                <asp:Panel ID="panelCadastro" runat="server">
                    <table class="cadastro">
                        <tr>
                            <th>
                                arquivo de retorno
                            </th>
                            <th>
                                <asp:Button ID="btCancelar" Text="voltar" runat="server" CausesValidation="false"
                                    onclick="btCancelar_Click" />
                            </th>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                código:</td>
                            <td class="direito">
                                <asp:Label ID="txtCodigo" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                data de processamento:</td>
                            <td class="direito">
                                <asp:Label ID="lblDataProcessado" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                código banco:</td>
                            <td class="direito">
                                <asp:Label ID="lblCodBanco" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                data de criação:</td>
                            <td class="direito">
                                <asp:Label ID="lblDataCriacao" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                tipo de conciliação:</td>
                            <td class="direito">
                                <asp:Label ID="lblTipoConc" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                tipo de arquivo:</td>
                            <td class="direito">
                                <asp:Label ID="lblTipoArquivo" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                usuário:</td>
                            <td class="direito">
                                <asp:Label ID="lblUsuario" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo" style="height: 25px">
                                log:</td>
                            <td class="direito" style="height: 25px">
                                <asp:Label ID="lblLog" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo" style="height: 25px">
                                observação:</td>
                            <td class="direito" style="height: 25px">
                                <ctx:cTexto ID="cTextoObs" runat="server" Height="50px" TextMode="MultiLine" 
                                    Width="500px" />
                            </td>
                        </tr>
                        <tr>
                            <th colspan="2">
                                <asp:Button ID="btCancelProcessamento" runat="server" OnClientClick="onProcessar();"
                                    Text="cancelar processamento" onclick="btCancelProcessamento_Click" />
                                <asp:Button ID="btCancelar0" runat="server" onclick="btCancelar_Click" CausesValidation="false"
                                    Text="voltar" />
                            </th>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="panelConciliacao" runat="server">                   
                    <asp:GridView ID="gridNaoProcessados" runat="server"
                        AutoGenerateColumns="False" Caption="Lista de Arquivos de Retornos Não Processados" 
                        CellPadding="4" CssClass="tableView" ForeColor="#333333" GridLines="None" 
                        HorizontalAlign="Left"  Width="100%">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                           
                            <asp:BoundField DataField="codigo_banco" HeaderText="código banco" 
                                SortExpression="codigo_banco">
                            <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="data_criacao" HeaderText="data criação" 
                                SortExpression="data_criacao">
                            <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="tipo conciliação" 
                                SortExpression="TipoConciliacao.nome">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox3" runat="server" 
                                        Text='<%# Bind("TipoConciliacao.nome") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" 
                                        Text='<%# Bind("TipoConciliacao.nome") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="tipo arquivo" SortExpression="TipoArquivo.nome">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox4" runat="server" 
                                        Text='<%# Bind("TipoArquivo.nome") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("TipoArquivo.nome") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" 
                            HorizontalAlign="Left" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" 
                            CssClass="SortedAscendingCellStyle" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" 
                            CssClass="SortedAscendingHeaderStyle" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" 
                            CssClass="SortedDescendingCellStyle" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" 
                            CssClass="SortedDescendingHeaderStyle " />
                    </asp:GridView>
                    <asp:Button ID="btProcessar" runat="server" onclick="btProcessar_Click" OnClientClick="onProcessar();" 
                        Text="processar arquivos" />
                    <asp:Button ID="btCancelar1" runat="server" onclick="btCancelar_Click" 
                        Text="cancelar" />
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanelAnimationExtender ID="upae" BehaviorID="animation" runat="server"
            TargetControlID="UpdatePanel1" Enabled="True">
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
        </asp:UpdatePanelAnimationExtender>
</div>
</asp:Content>
