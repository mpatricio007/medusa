<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true" CodeBehind="ContasLancto.aspx.cs" Inherits="Medusa.Sistemas.Conciliacao.ContasLancto" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            height: 25px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <div class="conteudo">
        &nbsp;<asp:ScriptManager ID="ScriptManager1" runat="server">
          <Scripts>
            <asp:ScriptReference Path="../../Scripts/FixFocus.js" />       
               
        </Scripts>
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
                        <img src="../../Styles/img/loading.gif" alt=""/>
                        <span style="margin:3px">Carregando ...</span></div>
                    </div>
                    <asp:Label ID="Label1" runat="server" Text="procurar"></asp:Label>
                    &nbsp;<asp:DropDownList ID="ddlOptions" runat="server" CausesValidation="True" 
                        AutoPostBack="True" onselectedindexchanged="ddlOptions_SelectedIndexChanged">
                        <asp:ListItem Value="numero">nº da conta</asp:ListItem>
                        <asp:ListItem Value="id_conta">código</asp:ListItem>
                        <asp:ListItem Value="Projeto.codigo">projeto</asp:ListItem>
                        <asp:ListItem Value="BancoAgencia.Banco.nome">banco</asp:ListItem>
                        <asp:ListItem Value="BancoAgencia.nome">nº da agência</asp:ListItem>
                        <asp:ListItem Value="ContaTipo.descricao">tipo da conta</asp:ListItem>
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
                </div>
                <asp:Panel ID="panelGrid" runat="server">
                          
                  <table class="cadastro">
                 <tr>
                 <th>
                            
                          saldo na data:
                                <cdt:cData ID="cDataTermino" runat="server" ValidationGroup="procurar" />
                          <asp:Button ID="btSearch0" runat="server" onclick="btProcurar_Click" 
                              Text="atualizar saldo" ValidationGroup="procurar" />
                   </th>         
                   </tr>
                   </table>
                        <asp:GridView ID="grid" runat="server" AllowPaging="True" AllowSorting="True" 
                        AutoGenerateColumns="False" Caption="Lançamentos em Conta Corrente" 
                        CellPadding="4" CssClass="tableView" ForeColor="#333333" GridLines="None" 
                        onpageindexchanging="grid_PageIndexChanging" onrowcreated="grid_RowCreated" 
                        onrowediting="grid_RowEditing" onsorting="grid_Sorting" Width="100%">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                             <asp:TemplateField HeaderText="nº da conta"
                                SortExpression="numero">                                
                                <ItemTemplate>
                              
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("numero") + "-" +Eval("digito")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                 <ItemStyle Width="100px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="banco" HeaderText="banco" SortExpression="banco" >
                             <HeaderStyle HorizontalAlign="Left" />
                             </asp:BoundField>
                            <asp:BoundField DataField="agencia" HeaderText="agência" 
                                SortExpression="agencia" >
                             <HeaderStyle HorizontalAlign="Left" />
                             </asp:BoundField>
                            <asp:BoundField DataField="cod_def_projeto" HeaderText="projeto" 
                                SortExpression="cod_def_projeto" >
                             <HeaderStyle HorizontalAlign="Left" />
                             </asp:BoundField>
                             <asp:BoundField DataField="saldo_anterior" DataFormatString="{0:N2}" 
                                 HeaderText="saldo anterior">
                             <HeaderStyle HorizontalAlign="Right" />
                             <ItemStyle HorizontalAlign="Right" />
                             </asp:BoundField>
                            <asp:BoundField DataField="creditos" HeaderText="créditos" 
                                DataFormatString="{0:N2}">
                             <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="debitos" DataFormatString="{0:N2}" 
                                HeaderText="débitos">
                             <HeaderStyle HorizontalAlign="Right" />
                             <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="saldo_final" DataFormatString="{0:N2}" 
                                HeaderText="saldo final">
                             <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle Width="120px" HorizontalAlign="Right" />
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
                            <th colspan="2">
                                lançamentos de conta corrente</th>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                conta corrente:
                                <asp:Label ID="txtCodigo" runat="server" Text="0" Visible="False"></asp:Label>
                            </td>
                            <td class="direito">
                              <asp:Label ID="txtConta" runat="server"></asp:Label>
                            </td>
                        </tr>
                       
                    </table>
                </asp:Panel>
                <asp:Panel ID="panelLanctos" runat="server" Visible="False">
                    <table class="cadastro">
                       <tr>
                            <td class="esquerdo">
                                lançamento:
                                </td>
                            <td class="direito">
                                <asp:Label ID="txtId_lcto_conta" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo" style="height: 34px">projeto:</td>
                            <td class="direito" style="height: 34px">
                                <ctx:cTexto ID="cTextoProjeto" runat="server" EnableValidator="False" MaxLength="30" Width="300" ValidationGroup="contaslancto" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                data:
                                </td>
                            <td class="direito">
                                <cdt:cData ID="cDataLancto" runat="server" EnableValidator="True" ValidationGroup="contaslancto" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                tipo lançamento:</td>
                            <td class="direito">
                                <cDdlTipoLancto:cDdlTipoLancto ID="cDdlTipoLancto1" runat="server" ValidationGroup="contaslancto" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                descrição:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoDescricao" runat="server" EnableValidator="True" 
                                    MaxLength="40" Width="300" ValidationGroup="contaslancto" TextMode="SingleLine" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                valor:</td>
                            <td class="direito">
                                <cvl:cValor ID="cValorLancto" runat="server" EnableValidator="True" ValidationGroup="contaslancto" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                nota fiscal nº:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTxtNumDocumento" runat="server" EnableValidator="False" 
                                    MaxLength="30" Width="300px" ValidationGroup="contaslancto" />
                            </td>
                        </tr>
                         <tr>
                             <td class="esquerdo">
                                 observação:</td>
                             <td class="direito">
                                 <asp:TextBox ID="txtObs" runat="server" Height="50px" TextMode="MultiLine" 
                                     Width="500px" ValidationGroup="contaslancto"></asp:TextBox>
                             </td>
                        </tr>
                         <tr>
                            <td colspan="2">
                                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                            </td>
                        </tr> 
                        <tr>
                            <th colspan="1">
                            <div id="dGravacao" runat="server">
                                <asp:Button ID="btInsereLancto" runat="server" 
                                    Text="inserir" onclick="btInserePag_Click" ValidationGroup="contaslancto" />
                                <asp:Button ID="btAlterarLancto" runat="server" Text="alterar" 
                                    onclick="btSalvaPag_Click" ValidationGroup="contaslancto" />
                                <asp:Button ID="btExcluiLancto" runat="server" CausesValidation="False" 
                                    Text="excluir" onclick="btExcluiPag_Click" ValidationGroup="contaslancto" />
                                    
                                <asp:Button ID="btCancelar" runat="server" onclick="btCancelar_Click" 
                                    Text="cancelar" CausesValidation="False" ValidationGroup="contaslancto" />
                                      </div>
                            </th>
                            <th>
                            
                                inicial:
                                <cdt:cData ID="cData1" runat="server" ValidationGroup="filtrar" />
                                &nbsp;final:
                                <cdt:cData ID="cData2" runat="server" ValidationGroup="filtrar" />
                                &nbsp;valor:
                                <cvl:cValor ID="cValor1" runat="server" EnableValidator="False" Width="80" />
                                <asp:Button ID="btFiltrar" runat="server" onclick="btFiltrar_Click" 
                                    Text="filtrar" ValidationGroup="filtrar" />                           
                            </th>
                        </tr>
              
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblMsgPag" runat="server"></asp:Label>
                            </td>
                        </tr>
                      </table>

                       <asp:GridView ID="gridLancto" runat="server"  AllowSorting="True" CssClass="mGrid" 
                                    AutoGenerateColumns="False" Caption="Lançamentos na Conta Corrente" CellPadding="4" 
                                     ForeColor="#333333" GridLines="None" Width="100%" 
                                    onrowediting="gridPag_RowEditing" ondatabinding="gridPag_DataBinding" 
                                  
                                    onselectedindexchanging="gridLancto_SelectedIndexChanging">
                                    <AlternatingRowStyle BackColor="White" ForeColor="Black" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgAdd" runat="server" CssClass="add" ImageUrl="../../Shared/images/plus.png" CommandName="Select" CausesValidation="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="proj_num" HeaderText="projeto" SortExpression="proj_num">
                                            <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="num_documento" HeaderText="nota fiscal" SortExpression="num_documento">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="200px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="data" HeaderText="data"
                                            DataFormatString="{0:dd/MM/yyyy}" SortExpression="data">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="valor" HeaderText="valor" DataFormatString="{0:N2}" SortExpression="valor">
                                        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="D/C" SortExpression="dc">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelDC" runat="server" Text='<%# Bind("dc") %>' CssClass='<%# Bind("dc") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="30px" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="30px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="true" HeaderText="descrição" SortExpression="descricao">
                                            <ItemTemplate>
                                              <asp:Label ID="LabelDescricao" runat="server" Text='<%# Bind("descricao") %>'></asp:Label>                                              
                                            </ItemTemplate>
                                            <ItemStyle Width="500px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="saldo" HeaderText="saldo" DataFormatString="{0:N2}" SortExpression="saldo" >
                                        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:CommandField EditText="selecionar" ShowEditButton="True" >
                                            <ItemStyle VerticalAlign="Top" />
                                        </asp:CommandField>
                                         <asp:TemplateField>
                                            <ItemTemplate>
                                                  <asp:Panel ID="pRemessas" runat="server" Width="100%" Visible="false">

                                                 <tr><td></td><td colspan = '999'>
                                                    <asp:GridView ID="gridRemessas" runat="server" AllowSorting="True" Width="100%"
                                                        AutoGenerateColumns="False" BorderStyle="Solid" CellPadding="4" 
                                                        CssClass="mGrid" EnableTheming="False" GridLines="None">
                                                        <RowStyle BackColor="#dedede" ForeColor="Black" />
                                                        <AlternatingRowStyle BackColor="White" ForeColor="Black" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="projeto">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label50" runat="server" Text='<%# Bind("Projeto.codigo") %>'></asp:Label>
                                                                </ItemTemplate>                                                              
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle VerticalAlign="Top" Width="70px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="lote">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox1" runat="server" 
                                                                        Text='<%# Bind("LoteRemessa.id_lote") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("LoteRemessa.id_lote") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" Width="70px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="nome_fav_cedente" HeaderText="beneficiário/cedente">
                                                          
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                          
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>

                                                            <asp:BoundField DataField="descricao" HeaderText="descrição">
                                                          
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" Width="230px" />
                                                          
                                                            </asp:BoundField>

                                                              <asp:BoundField DataField="valor" HeaderText="valor" 
                                                                DataFormatString="{0:N2}">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" Width="500px" />
                                                            <HeaderStyle HorizontalAlign="Left" Width="160px" />
                                                            <ItemStyle HorizontalAlign="Left" Width="160px" />
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" Width="203px" />
                                                            </asp:BoundField>

                                                            <asp:TemplateField HeaderText="status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("TipoRetorno.codigo") %>' ToolTip='<%# Bind("TipoRetorno.descricao") %>'></asp:Label>
                                                                </ItemTemplate>                                                               
                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="aut_bancaria" HeaderText="aut. bancaria">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                    </asp:GridView>
                                                     </td>
                                                </tr>
                                                </asp:Panel>
                                               
                                             </ItemTemplate>
                                        </asp:TemplateField>
                                         
                                            
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
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

            </ContentTemplate>
        </asp:UpdatePanel>
        <ajaxtoolkit:UpdatePanelAnimationExtender ID="upae" BehaviorID="animation" runat="server" TargetControlID="UpdatePanel1" Enabled="True">
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
        </ajaxtoolkit:UpdatePanelAnimationExtender>
</div>
</asp:Content>
