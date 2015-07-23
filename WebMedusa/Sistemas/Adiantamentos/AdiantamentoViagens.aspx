<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true" CodeBehind="AdiantamentoViagens.aspx.cs" Inherits="Medusa.Sistemas.Adiantamentos.AdiantamentoViagens" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="ControleHistoricoAdiantamento.ascx" tagname="ControleHistoricoAdiantamento" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <script type="text/javascript">
         function openDialog() {
             Page_ClientValidate('');
             if (Page_IsValid) {
                 var dlg = $("#dEmail").dialog({
                     width: 900,
                     height: 1000,
                     position: 'top',
                     modal: true,
                     resizable: false,
                     draggable: false,
                     title: 'envio de email'
                 });

                 dlg.parent().appendTo(jQuery("form:first"));
                 $("#dEmail").dialog("open");
             }
         }

         function closeDialog() {
             $("#dEmail").dialog("close");
         }
    </script>
    <div class="conteudo">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="dEmail" style="display: none">
            <asp:UpdatePanel ID="upEmail" runat="server">
                <ContentTemplate>
                    <table class="cadastro">
                        <tr>
                            <td class="esquerdo">
                                padrão de email:
                            </td>
                            <td class="direito">
                                <asp:DropDownList ID="listaEmailpadrao" runat="server" OnSelectedIndexChanged="lista_SelectedIndexChanged"
                                    AutoPostBack="True" DataTextField="nome" DataValueField="id_email_padrao" Width="100" CausesValidation="false" />
                            </td>
                        </tr>
                        <tr>
                            <th colspan="2">
                                destinatarios
                            </th>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                coordenador(es):
                            </td>
                            <td class="direito">
                                <asp:GridView ID="gridEmailCoord" runat="server" AutoGenerateColumns="false" EmptyDataText="projeto sem coordenador cadastrado">
                                    <Columns>
                                        <asp:TemplateField HeaderText="coordenador" SortExpression="Coordenador.PessoaFisica.nome">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBoxx" runat="server" Text='<%# Bind("Coordenador.PessoaFisica.nome") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Labell" runat="server" Text='<%# Bind("Coordenador.PessoaFisica.nome") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="emails" SortExpression="Coordenador.PessoaFisica.strListEmails">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Coordenador.PessoaFisica.strListEmails") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("Coordenador.PessoaFisica.strListEmails") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                adicionar destinatários:
                            </td>
                            <td class="direito">
                                <cListaPessoaEmails:cListaPessoaEmails ID="cListaPessoaEmailsDest" runat="server" ValidationGroup="listDest" />
                            </td>
                        </tr>
                        <tr>
                            <th colspan="2">
                                cópias
                            </th>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                contatos:
                            </td>
                            <td class="direito">
                                <asp:GridView ID="gridCopy" runat="server" AutoGenerateColumns="false" EmptyDataText="projeto sem contatos para este tipo de notificação">
                                    <Columns>
                                        <asp:TemplateField HeaderText="contato" SortExpression="Contato.PessoaFisica.nome">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBoxx" runat="server" Text='<%# Bind("Contato.PessoaFisica.nome") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Labell" runat="server" Text='<%# Bind("Contato.PessoaFisica.nome") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="emails" SortExpression="Contato.PessoaFisica.strListEmails">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Contato.PessoaFisica.strListEmails") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("Contato.PessoaFisica.strListEmails") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                cópias opcionais:
                            </td>
                            <td class="direito">
                                <cListaPessoaEmails:cListaPessoaEmails ID="cListaPessoaEmailsCopy" ValidationGroup="listCopy"
                                    runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <th colspan="2">
                                conteúdo do email
                            </th>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                assunto:
                            </td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoAssunto" runat="server" ValidationGroup="send" MaxLength="50" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                texto:
                            </td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoEmail" runat="server" Height="300" TextMode="MultiLine" Width="500" ValidationGroup="send" />
                            </td>
                        </tr>
                        <tr>
                            <th colspan="2">
                                <asp:Button ID="btSendEmail" runat="server" Text="enviar" OnClick="btSendEmail_Click"
                                    ValidationGroup="send" /><asp:Label ID="lbSaida" runat="server" Text=""></asp:Label>
                            </th>
                        </tr>
                    </table>
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
                    <div id="updateProgressDiv" style="display:none; position:absolute;">
                        <div style=" margin-left:780px;  float:left">
                        <img src="../../Styles/img/loading.gif" />
                        <span style="margin:3px">Carregando ...</span></div>
                    </div>
                    <asp:Label ID="Label1" runat="server" Text="procurar"></asp:Label>
                    &nbsp;<asp:DropDownList ID="ddlOptions" runat="server" CausesValidation="True" 
                        AutoPostBack="True" onselectedindexchanged="ddlOptions_SelectedIndexChanged">
                        <asp:ListItem Value="Projeto.codigo">projeto</asp:ListItem>
                        <asp:ListItem Value="Beneficiario.PessoaFisica.nome">beneficiário</asp:ListItem>
                        <asp:ListItem Value="Beneficiario.PessoaFisica.cpf.Value">cpf</asp:ListItem>
                        <asp:ListItem Value="data">data</asp:ListItem>
                        <asp:ListItem Value="data_partida">data de partida</asp:ListItem>
                        <asp:ListItem Value="data_vencimento">data de vencimento</asp:ListItem>
                        <asp:ListItem>valor</asp:ListItem>
                        <asp:ListItem Value="descricao">descrição</asp:ListItem>
                        <asp:ListItem>rp</asp:ListItem>
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
                        AutoGenerateColumns="False" Caption="Lista de Adiantamentos de Diárias" CellPadding="4" 
                        CssClass="tableView" ForeColor="#333333" GridLines="None" 
                        onpageindexchanging="grid_PageIndexChanging" onrowcreated="grid_RowCreated" 
                        onrowediting="grid_RowEditing" onsorting="grid_Sorting" Width="100%">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="projeto" SortExpression="Projeto.codigo">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" 
                                        Text='<%# Bind("Projeto.HtmlPaginaProjeto") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" 
                                        Text='<%# Bind("Projeto.HtmlPaginaProjeto") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="beneficiário" 
                                SortExpression="Beneficiario.PessoaFisica.nome">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox3" runat="server" 
                                        Text='<%# Bind("Beneficiario.HtmlPaginaBeneficiario") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" 
                                        Text='<%# Bind("Beneficiario.HtmlPaginaBeneficiario") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="data_retorno" HeaderText="data de retorno" SortExpression="data_retorno" 
                                DataFormatString="{0:d}" >
                            </asp:BoundField>
                            <asp:BoundField DataField="data_pagamento" DataFormatString="{0:d}" 
                                HeaderText="data de pagamento" SortExpression="data_pagamento" />
                            <asp:BoundField DataField="data_partida" HeaderText="data de partida" 
                                SortExpression="data_partida" DataFormatString="{0:d}" >
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="data_vencimento" HeaderText="data de vencimento" 
                                SortExpression="data_vencimento" DataFormatString="{0:d}">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valor" DataFormatString="{0:N2}" HeaderText="valor" 
                                SortExpression="valor">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="rp" HeaderText="rp" SortExpression="rp" />
                            <asp:BoundField DataField="descricao" HeaderText="descrição" 
                                SortExpression="descricao">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="status" SortExpression="StatusAdiantamento.nome">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox4" runat="server" 
                                        Text='<%# Bind("StatusAdiantamento.nome") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" 
                                        Text='<%# Bind("StatusAdiantamento.nome") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
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
                            <th colspan="1">
                                cadastro de adiantamentos de diárias</th>
                            <th colspan="1">
                            <div id="dGravacao" runat="server">
                                <asp:Button ID="btInserir" runat="server" onclick="btInserir_Click" 
                                    Text="inserir" />
                                <asp:Button ID="btAlterar" runat="server" onclick="btAlterar_Click" 
                                    Text="salvar" />
                                <asp:Button ID="btCancelar" runat="server" CausesValidation="False" 
                                    onclick="btCancelar_Click" Text="cancelar" />
                                <asp:Button ID="btEnviarEmail" runat="server" OnClick="btEnviarEmail_Click" Text="enviar email"
                                                        CausesValidation="false" />
                            </div>
                            </th>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                beneficiário:</td>
                            <td class="direito">
                                <cDdlBeneficiario:cDdlBeneficiario ID="cDdlBeneficiario1" runat="server" />
                            </td>
                        </tr>
                            <tr>
                            <td class="esquerdo">
                                projeto:
                                <asp:Label ID="txtCodigo" runat="server" Text="0" Visible="False"></asp:Label>
                            </td>
                            <td class="direito">
                                <cddlProjeto:cDdlProjeto ID="cDdlProjeto1" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="cDdlProjeto1_SelectedIndexChanged"  />
                            </td>
                        </tr>
                          <tr>
                            <td class="esquerdo">
                                opções do projeto:</td>
                            <td class="direito">
                                <asp:GridView ID="gridOpcao" runat="server" AllowPaging="True" 
                                    AllowSorting="True" AlternatingRowStyle-CssClass="alt" 
                                    AutoGenerateColumns="False" CssClass="mGrid" 
                                    EmptyDataText="não há opções de adiantamentos cadastrados" EnableTheming="False" 
                                    OnPageIndexChanging="grid_PageIndexChanging" OnRowCreated="grid_RowCreated" 
                                    OnRowEditing="grid_RowEditing" OnSorting="grid_Sorting" 
                                    PagerStyle-CssClass="pgr" Width="100%">
                                    <AlternatingRowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="opção" SortExpression="TiposAdiantamento.nome">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox8" runat="server" 
                                                    Text='<%# Bind("TiposAdiantamento.nome") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label8" runat="server" 
                                                    Text='<%# Bind("TiposAdiantamento.nome") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="data" DataFormatString="{0:d}" HeaderText="data" 
                                            SortExpression="data" />
                                        <asp:BoundField DataField="obs" HeaderText="observação" SortExpression="obs" />
                                        <asp:TemplateField HeaderText="conferente" 
                                            SortExpression="Usuario.PessoaFisica.nome">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox9" runat="server" 
                                                    Text='<%# Bind("Usuario.PessoaFisica.nome") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label9" runat="server" 
                                                    Text='<%# Bind("Usuario.PessoaFisica.nome") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle />
                                    <PagerStyle CssClass="pgr" />
                                    <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle" />
                                    <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle" />
                                    <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle" />
                                    <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle " />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                n° rp:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoRp" runat="server" MaxLength="10" 
                                    ValidationGroup="false" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                data:</td>
                            <td class="direito">
                                <cdt:cData ID="cData" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                valor:</td>
                            <td class="direito">
                                <cvl:cValor ID="cValorViagem" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                descrição:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoDescricao" runat="server" Height="100px" 
                                    TextMode="MultiLine" Width="500px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                observação:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoObs" runat="server" MaxLength="200" 
                                    TextMode="MultiLine" Width="500px" Height="100px" 
                                    ValidationGroup="false" />
                            </td>
                        </tr>
                    </table>
                    <div id="dPrazo" runat="server">
                        <table class="cadastro">
                            <tr>
                                <th colspan="2">
                                    prazo
                                </th>
                            </tr>
                            <tr>
                                <td class="esquerdo">
                                    data partida:</td>
                                <td class="direito">
                                    <cdt:cData ID="cDataPartida" runat="server" EnableValidator="False" />
                                </td>
                            </tr>
                            <tr>
                                <td class="esquerdo">
                                    data retorno:</td>
                                <td class="direito">
                                    <cdt:cData ID="cDataRetorno" runat="server" EnableValidator="False" />
                                </td>
                            </tr>
                            <tr>
                                <td class="esquerdo">
                                    data pagamento:</td>
                                <td class="direito">
                                    <cdt:cData ID="cDataPagamento" runat="server" ValidationGroup="calcular" />
                                </td>
                            </tr>
                            <tr>
                                <td class="esquerdo">
                                    data prestação de contas:</td>
                                <td class="direito">
                                    <cdt:cData ID="cDataVencto" runat="server" Enabled="false" 
                                        ValidationGroup="false" />
                                    <asp:Button ID="btCalcular" runat="server" onclick="btCalcular_Click" 
                                        Text="calcular(opcional)" ValidationGroup="calcular" />
                                </td>
                            </tr>
                            <tr>
                                <th colspan="2">
                                    prestação de contas
                                </th>
                            </tr>
                            <tr>
                                <td class="esquerdo">
                                    descrição:</td>
                                <td class="direito">
                                    <ctx:cTexto ID="cTextoDescRd" runat="server" EnableValidator="False" 
                                        Height="100px" TextMode="MultiLine" Visible="true" Width="500px" 
                                        ValidationGroup="rd" />
                                </td>
                            </tr>
                            <tr>
                                <td class="esquerdo">
                                    valor total:</td>
                                <td class="direito">
                                    <cvl:cValor ID="cValorTotal" runat="server" ValidationGroup="rd" />
                                </td>
                            </tr>
                            <tr>
                                <td class="esquerdo">
                                    data da rd:</td>
                                <td class="direito">
                                    <cdt:cData ID="cDataRd" runat="server" ValidationGroup="rd" />
                                </td>
                            </tr>
                            <tr>
                                <td class="esquerdo">
                                    valor dev/rec:</td>
                                <td class="direito">
                                    <asp:Label ID="lbTotalDevRec" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <table class="cadastro">
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th colspan="2">
                                <table>
                                    <tr>
                                        <th colspan="2">
                                            <div id="dGravacao1" runat="server">
                                                <asp:Button ID="btInserir0" runat="server" onclick="btInserir_Click" 
                                                    Text="inserir" />
                                                <asp:Button ID="btAlterar0" runat="server" onclick="btAlterar_Click" 
                                                    Text="salvar" />
                                                <asp:Button ID="btCancelar0" runat="server" onclick="btCancelar_Click" 
                                                    Text="cancelar" />
                                                <asp:Button ID="btRelatorioViagem" runat="server" 
                                                    onclick="btRelatorioViagem_Click" Text="imprimir solicitação" 
                                                CausesValidation="False" />
                                            </div>
                                        </th>
                                    </tr>
                                </table>
                            </th>
                        </tr>
                        <tr>
                        <div>
                            <td colspan="2">
                                
                                <uc1:ControleHistoricoAdiantamento ID="ControleHistoricoAdiantamento1" 
                                    runat="server" />
                                
                            </td>
                        </div>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="gridHistEmail" runat="server" AutoGenerateColumns="False" 
                                    Caption="Historico de Envio Emails" Width="100%" CssClass="tableView">
                                    <Columns>
                                        <asp:BoundField DataField="assunto" HeaderText="assunto" 
                                            SortExpression="assunto">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="data" DataFormatString="{0:d}" HeaderText="data" 
                                            SortExpression="data" >
                                        <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="usuário" 
                                            SortExpression="Usuario.PessoaFisica.nome">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox2" runat="server" 
                                                    Text='<%# Bind("Usuario.PessoaFisica.nome") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" 
                                                    Text='<%# Bind("Usuario.PessoaFisica.nome") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>


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
