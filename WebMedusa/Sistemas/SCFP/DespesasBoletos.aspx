<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true" CodeBehind="DespesasBoletos.aspx.cs" Inherits="Medusa.Sistemas.SCFP.DespesasBoletos" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Src="../../Controles/DdlCodigoPlanoConta.ascx" TagName="DdlCodigoPlanoConta" TagPrefix="uc1" %>
<%@ Register Src="../../Controles/DdlProjeto.ascx" TagName="cDdlProjeto" TagPrefix="cddlProjeto" %>
<%@ Register Src="../../Controles/Data.ascx" TagName="cData" TagPrefix="cdt" %>
<%@ Register Src="../../Controles/Valor.ascx" TagName="cValor" TagPrefix="cvl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

     <script type="text/javascript">
         function pageLoad() {
             $(function () {
                 $('.date').datepicker({
                     showOtherMonths: true,
                     changeMonth: true,
                     changeYear: true
                 });
                 $('.date').dateEntry({ spinnerImage: '' });
                 $('.cep').mask("99999-999");
                 $(".cpf").mask("999.999.999-99");
                 $(".telefone").mask("(99)99999999?9");
                 $(".cnpj").mask("99.999.999/9999-99");
                 $(".boleto").mask("99999.99999 99999.999999 99999.999999 9 99999999999999");
             });
         }

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
    <div class="conteudo">
        <div id="progressDialog" title="Processando" style="display: none;">
            <img src="../../Styles/img/loading2.gif" alt="Processing" />
            <p>
                Aguarde...
            </p>
        </div>    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:CheckBox ID="ckFilter" runat="server" AutoPostBack="True"
                    OnCheckedChanged="ckFilter_CheckedChanged"
                    Text="habilitar múltiplos filtros" />
                <asp:DataList ID="DataListFiltros" runat="server" RepeatColumns="6"
                    OnDataBinding="DataListFiltros_DataBinding" RepeatDirection="Horizontal"
                    OnDeleteCommand="DataListFiltros_DeleteCommand">
                    <ItemTemplate>

                        <div class="FilterName">

                            <%# Eval("property_name") %>&nbsp
                <%# Eval("mode_name")%>&nbsp
                <%# Eval("value")%> &nbsp 
                   <asp:ImageButton ID="btExcluiFiltro" runat="server"
                       ImageUrl="~/Styles/img/bt_delete.jpg" Width="15px" Height="15px" CommandName="delete" />
                        </div>
                    </ItemTemplate>
                </asp:DataList>
                <div class="pesquisar">
                    <div id="updateProgressDiv" style="display: none; position: absolute;">
                        <div style="margin-left: 780px; float: left">
                            <img src="../../Styles/img/loading.gif" />
                            <span style="margin: 3px">Carregando ...</span>
                        </div>
                    </div>
                    <asp:Label ID="Label1" runat="server" Text="procurar"></asp:Label>
                    &nbsp;<asp:DropDownList ID="ddlOptions" runat="server" CausesValidation="True"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlOptions_SelectedIndexChanged">
                        <asp:ListItem>cedente</asp:ListItem>
                        <asp:ListItem Value="Projeto.codigo">projeto</asp:ListItem>
                        <asp:ListItem Value="dataVencto">data de vencimento</asp:ListItem>
                        <asp:ListItem Value="valor">valor</asp:ListItem>
                        <asp:ListItem Value="rp">rd/rp</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtProcura" runat="server" ></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlMode" runat="server">
                    </asp:DropDownList>
                    <asp:Label ID="Label2" runat="server" Text="mostrar"></asp:Label>
                    &nbsp;
                    <asp:DropDownList ID="ddlSize" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlSize_SelectedIndexChanged">
                        <asp:ListItem Selected="True">10</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>50</asp:ListItem>
                        <asp:ListItem Value="0">todos</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btSearch" runat="server" CausesValidation="False"
                        OnClick="btProcurar_Click" Text="procurar" />
                    <asp:Button ID="btCriar" runat="server" CausesValidation="False"
                        OnClick="btCriar_Click" Text="novo" Width="80px" />
                </div>
                <asp:Panel ID="panelGrid" runat="server">





                    <asp:GridView ID="grid" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" Caption="Lista de Despesas Boletos" CellPadding="4"
                        CssClass="tableView" ForeColor="#333333" GridLines="None"
                        OnPageIndexChanging="grid_PageIndexChanging" OnRowCreated="grid_RowCreated"
                        OnRowEditing="grid_RowEditing" OnSorting="grid_Sorting" Width="100%">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                               <asp:TemplateField HeaderText="projeto" 
                            SortExpression="Projeto.codigo.Value">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" 
                             Text='<%# Bind("Projeto.HtmlPaginaProjeto") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:BoundField DataField="dataVencto" DataFormatString="{0:d}"
                                HeaderText="dt. vencto." SortExpression="dataVencto">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="data_pagto" DataFormatString="{0:d}"
                                HeaderText="dt. pagto." SortExpression="data_pagto">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cedente" HeaderText="cedente" SortExpression="cedente" />


                            <asp:BoundField DataField="valor" HeaderText="valor"
                                SortExpression="valor" DataFormatString="{0:c}">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                              <asp:BoundField DataField="rp" HeaderText="rp/rd" SortExpression="rp" />
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
                            <th colspan="1">cadastro de&nbsp; despesas de boletos</th>
                            <th colspan="1">
                                <div id="dGravacao" runat="server">
                                    <asp:Button ID="btInserir" runat="server" OnClick="btInserir_Click"
                                        Text="inserir" />
                                    <asp:Button ID="btAlterar" runat="server" OnClick="btAlterar_Click"
                                        Text="salvar" />
                                    <asp:Button ID="btExcluir" runat="server" CausesValidation="False"
                                        OnClick="btExcluir_Click" Text="excluir" />
                                    <asp:Button ID="btCancelar" runat="server" CausesValidation="False"
                                        OnClick="btCancelar_Click" Text="cancelar" />
                                </div>
                            </th>
                        </tr>
                        <tr>
                            <td class="esquerdo">boleto:<asp:Label ID="txtCodigo" runat="server" Text="0" Visible="false"></asp:Label></td>
                            
                            <td class="direito">
                                <%--<cint:cInteiro ID="cIntCodigo" runat="server" ontextchanged="txtInteiro_TextChanged" AutoPostBack="true" />--%>
                                <asp:TextBox ID="txtBoleto" runat="server" AutoPostBack="True" CssClass="boleto" MaxLength="55" OnTextChanged="txtBoleto_TextChanged" Width="500px" ValidationGroup="boleto"></asp:TextBox>
                                <asp:CustomValidator ID="cvBoleto" runat="server" ControlToValidate="txtBoleto" ErrorMessage="inválido" 
                                    ForeColor="Red" OnServerValidate="cvBoleto_ServerValidate" ValidateEmptyText="True" ValidationGroup="boleto"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">cedente:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoCedente" runat="server" Width="500" MaxLength="30" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">descrição:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoDescricao" runat="server" Width="500" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">projeto:</td>
                            <td class="direito">
                                <cddlProjeto:cDdlProjeto ID="cDdlProjeto1" runat="server" OnSelectedIndexChanged="cDdlProjeto1_SelectedIndexChanged" AutoPostBack="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">coordenador(es):</td>
                            <td class="direito">
                                <asp:GridView ID="GridCoordenadores" runat="server"
                                    AutoGenerateColumns="False" ShowHeader="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="coordenador(es)"
                                            SortExpression="Coordenador.PessoaFisica.nome">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server"
                                                    Text='<%# Bind("Coordenador.PessoaFisica.nome") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server"
                                                    Text='<%# Bind("Coordenador.PessoaFisica.nome") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>

                        <tr>
                            <td class="esquerdo">patrocinador(es):</td>
                            <td class="direito">
                                <asp:GridView ID="GridPatrocinadores" runat="server"
                                    AutoGenerateColumns="False" ShowHeader="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="patrocinador(es)"
                                            SortExpression="Financiador.nome">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox4" runat="server"
                                                    Text='<%# Bind("Financiador.nome") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label4" runat="server"
                                                    Text='<%# Bind("Financiador.nome") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>

                        <tr>
                            <td class="esquerdo">título do projeto:</td>
                            <td class="direito">
                                <asp:Label ID="lblTitulo" runat="server" Text="titulo"></asp:Label>
                            </td>
                        </tr>
                                <tr>
                            <td class="esquerdo">rp:</td>
                            <td class="direito">
                                                 <ctx:cTexto ID="cTextoRp" runat="server" MaxLength="50" EnableValidator="False" />
                            </td>
                        </tr>

                        <tr>
                            <td class="esquerdo">valor:</td>
                            <td class="direito">
                                <cvl:cValor ID="cValor1" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">data vencimento:</td>
                            <td class="direito">
                                <cdt:cData ID="cData1" runat="server" />
                            </td>
                        </tr>
                            <tr>
                            <td class="esquerdo">data pagto:</td>
                            <td class="direito">
                                <cdt:cData ID="cDataPagto" runat="server" />
                            </td>
                        </tr>

                        <tr>
                            <td class="esquerdo">lançamentos:</td>
                            <td class="direito">
                                <h2>
                                    <asp:Button ID="btCalcular0" runat="server" OnClick="btCalcular_Click" Text="calcular" />
                                </h2>
                                <h2>beneficiário</h2>
                                <asp:GridView ID="GridView1" CssClass="tableView" runat="server" AutoGenerateColumns="False" OnDataBound="GridView1_DataBound" ShowFooter="True">
                                    <Columns>
                                        <asp:BoundField DataField="codigo" HeaderText="código" >
                                        <ItemStyle Width="20px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="descricao" HeaderText="descrição" >
                                        <ItemStyle Width="300px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="credito" DataFormatString="{0:N2}" HeaderText="crédito" >
                                        <FooterStyle HorizontalAlign="Right" Width="80px" />
                                        <HeaderStyle HorizontalAlign="Right" Width="80px" />
                                        <ItemStyle HorizontalAlign="Right" Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="debito" DataFormatString="{0:N2}" HeaderText="débito" >
                                        <FooterStyle HorizontalAlign="Right" Width="80px" />
                                        <HeaderStyle HorizontalAlign="Right" Width="80px" />
                                        <ItemStyle HorizontalAlign="Right" Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="valorDeducao" DataFormatString="{0:N2}" HeaderText="dependentes" />
                                    </Columns>
                                </asp:GridView>
                                <h2>projeto</h2>
                                <asp:GridView ID="GridView2" CssClass="tableView" runat="server" AutoGenerateColumns="False" OnDataBound="GridView2_DataBound" ShowFooter="True">
                                    <Columns>
                                        <asp:BoundField DataField="codigo" HeaderText="código" >
                                        <ItemStyle Width="20px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="descricao" HeaderText="descrição" >
                                        <ItemStyle Width="300px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="creditoProjeto" DataFormatString="{0:N2}" HeaderText="crédito" >
                                        <FooterStyle HorizontalAlign="Right" Width="80px" />
                                        <HeaderStyle HorizontalAlign="Right" Width="80px" />
                                        <ItemStyle HorizontalAlign="Right" Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="debitoProjeto" DataFormatString="{0:N2}" HeaderText="débito" >
                                        <FooterStyle HorizontalAlign="Right" Width="80px" />
                                        <HeaderStyle HorizontalAlign="Right" Width="80px" />
                                        <ItemStyle HorizontalAlign="Right" Width="80px" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="projeto">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("projeto.codigo") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("projeto.codigo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <h2>encargos a recolher</h2>
                                <asp:GridView ID="GridView3" CssClass="tableView" runat="server" AutoGenerateColumns="False" OnDataBound="GridView3_DataBound" ShowFooter="True">
                                    <Columns>
                                        <asp:BoundField DataField="codigo" HeaderText="código" >
                                        <ItemStyle Width="20px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="descricao" HeaderText="descrição" >
                                        <ItemStyle Width="300px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="creditoProjeto" DataFormatString="{0:N2}" HeaderText="crédito" >
                                        <FooterStyle HorizontalAlign="Right" Width="80px" />
                                        <HeaderStyle HorizontalAlign="Right" Width="80px" />
                                        <ItemStyle HorizontalAlign="Right" Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="debitoProjeto" DataFormatString="{0:N2}" HeaderText="débito" >
                                        <FooterStyle HorizontalAlign="Right" Width="80px" />
                                        <HeaderStyle HorizontalAlign="Right" Width="80px" />
                                        <ItemStyle HorizontalAlign="Right" Width="80px" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="projeto">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("projeto.codigo") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("projeto.codigo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
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
                                    <asp:Button ID="btInserir0" runat="server" OnClick="btInserir_Click"
                                        Text="inserir" />
                                    <asp:Button ID="btAlterar0" runat="server"
                                        Text="salvar" OnClick="btAlterar_Click" />
                                    <asp:Button ID="btExcluir0" runat="server" CausesValidation="False"
                                        OnClick="btExcluir_Click" Text="excluir" />
                                    <asp:Button ID="btCancelar0" runat="server" CausesValidation="False"
                                        OnClick="btCancelar_Click" Text="cancelar" />
                                </div>
                            </th>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
