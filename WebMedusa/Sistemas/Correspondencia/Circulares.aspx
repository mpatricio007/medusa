<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true"
    CodeBehind="Circulares.aspx.cs" Inherits="Medusa.Sistemas.Correspondencia.Circulares" %>



<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 16px;
            height: 16px;
        }
 
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
    <script type="text/javascript">
        $(function ()
        {
           
            function openDialog()
            {
                var dlg = $("#modalDestEmail").dialog({
                    width: 750,
                    height: 470,
                    position: 'top',
                    modal: true,
                    title: 'escolher os destinatários'
                });
                dlg.parent().appendTo(jQuery("form:first"));
                $("#modalDestEmail").dialog("open");
            }

            $('#open').live("click", function ()
            {
                openDialog();
            });
        });  
        
//        function pageLoad(){
//            $(function(){
//                waitingDialog({});
//                setTimeout(closeWaitingDialog, 100);
//                });
//        }      
    </script>
    <div class="conteudo">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
       
        
        <div id="modalDestEmail" style="display: none">
            <asp:UpdatePanel ID="filtrarEmail" runat="server">
                <ContentTemplate>
                
                    <cPesqOrigemEmail:cPesqOrigemEmail ID="cPesqOrigemEmail1" runat="server" />
                    <asp:Button ID="btOk" runat="server" Text="selecionar os destinatários" OnClientClick="$('#modalDestEmail').dialog('close');"
                        OnClick="btOk_Click" />
                    <asp:Button ID="BtCancel" runat="server" Text="cancelar" OnClientClick="$('#modalDestEmail').dialog('close');"
                       />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:CheckBox ID="ckFilter" runat="server" AutoPostBack="True" OnCheckedChanged="ckFilter_CheckedChanged"
                    Text="habilitar múltiplos filtros" />
                <asp:DataList ID="DataListFiltros" runat="server" RepeatColumns="6" OnDataBinding="DataListFiltros_DataBinding"
                    RepeatDirection="Horizontal" OnDeleteCommand="DataListFiltros_DeleteCommand">
                    <ItemTemplate>
                        <div class="FilterName">
                            <%# Eval("property_name") %>&nbsp
                            <%# Eval("mode_name")%>&nbsp
                            <%# Eval("value")%>
                            &nbsp
                            <asp:ImageButton ID="btExcluiFiltro" runat="server" ImageUrl="~/Styles/img/bt_delete.jpg"
                                Width="15px" Height="15px" CommandName="delete" />
                        </div>
                    </ItemTemplate>
                </asp:DataList>
                <div class="pesquisar">
                    ano
                    <asp:DropDownList ID="ddlAno" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAno_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;
                    
                    <asp:Label ID="Label1" runat="server" Text="procurar"></asp:Label>
                    &nbsp;<asp:DropDownList ID="ddlOptions" runat="server" CausesValidation="True" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlOptions_SelectedIndexChanged">
                        <asp:ListItem Value="num" Selected="True">número</asp:ListItem>
                        <asp:ListItem>projeto</asp:ListItem>
                        <asp:ListItem>data</asp:ListItem>
                        <asp:ListItem Value="destinatario">destinatário</asp:ListItem>
                        <asp:ListItem Value="descricao">descrição</asp:ListItem>
                        <asp:ListItem Value="Usuario.PessoaFisica.nome">usuário</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtProcura" runat="server" Width="137px"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlMode" runat="server">
                    </asp:DropDownList>
                    &nbsp;<asp:Label ID="Label2" runat="server" Text="mostrar"></asp:Label>
                    &nbsp;
                    <asp:DropDownList ID="ddlSize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSize_SelectedIndexChanged">
                        <asp:ListItem Selected="True">10</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>50</asp:ListItem>
                        <asp:ListItem Value="0">todos</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btSearch" runat="server" CausesValidation="False" OnClick="btProcurar_Click"
                        Text="procurar" />
                    <asp:Button ID="btCriar" runat="server" CausesValidation="False" OnClick="btCriar_Click"
                        Text="novo" Width="80px" />
                </div>
                <asp:Panel ID="panelGrid" runat="server">
                    <asp:GridView ID="grid" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                        Caption="Lista de Circulares" CellPadding="4" CssClass="tableView" ForeColor="#333333"
                        GridLines="None" OnPageIndexChanging="grid_PageIndexChanging" OnRowCreated="grid_RowCreated"
                        OnRowEditing="grid_RowEditing" OnSorting="grid_Sorting" Width="100%">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="num" HeaderText="nº" SortExpression="num">
                                <ItemStyle Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="projeto" HeaderText="projeto" SortExpression="projeto">
                                <ItemStyle Width="70px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="data" DataFormatString="{0:d}" HeaderText="data" SortExpression="data">
                                <ItemStyle Width="70px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="destinatario" HeaderText="destinatário" SortExpression="destinatario">
                                <ItemStyle Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="descricao" HeaderText="descrição" SortExpression="descricao">
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="arquivo" SortExpression="arquivo">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("arquivo") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("arquivo") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="70px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="usuário" SortExpression="Usuario.nome">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Usuario.login") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="100px" />
                            </asp:TemplateField>
                            <asp:CheckBoxField DataField="ativa" HeaderText="Ativa" />
                            <asp:CommandField EditText="selecionar" ShowEditButton="True">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:CommandField>
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" CssClass="SortedAscendingCellStyle" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" CssClass="SortedAscendingHeaderStyle" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" CssClass="SortedDescendingCellStyle" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" CssClass="SortedDescendingHeaderStyle " />
                    </asp:GridView>
                </asp:Panel>
                <asp:Panel ID="panelCadastro" runat="server">
                    <table class="cadastro">
                        <tr>
                            <th colspan="1">
                                cadastro de circulares
                            </th>
                            <th colspan="1">
                                <div id="dGravacao" runat="server">
                                    <asp:Button ID="btInserir" runat="server" OnClick="btInserir_Click" Text="inserir" />
                                    <asp:Button ID="btAlterar" runat="server" OnClick="btAlterar_Click" Text="salvar" />
                                    <asp:Button ID="btExcluir" runat="server" CausesValidation="False" OnClick="btExcluir_Click"
                                        Text="excluir" />
                                    <asp:Button ID="btCancelar" runat="server" CausesValidation="False" OnClick="btCancelar_Click"
                                        Text="cancelar" />
                                </div>
                            </th>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                nº:
                            </td>
                            <td class="direito">
                                <asp:Label ID="lbNum" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                data:
                                <asp:TextBox ID="txtCodigo" runat="server" Enabled="False" Visible="False" Width="43px">0</asp:TextBox>
                            </td>
                            <td class="direito">
                                <cdt:cData ID="cData1" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                projeto:
                            </td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoProjeto" runat="server" MaxLength="50" Width="200px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                destinatário:
                            </td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoDestinatario" runat="server" MaxLength="50" Width="500px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                descrição:
                            </td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoDescricao" runat="server" Height="50px" TextMode="MultiLine"
                                    Width="500" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                palavras chave:
                            </td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoPalavrasChave" runat="server" Height="50px" TextMode="MultiLine"
                                    Width="500px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo" style="height: 27px">
                                arquivo:
                            </td>
                            <td class="direito" style="height: 27px">
                                <asp:Label ID="lbArquivo" runat="server"></asp:Label>
                                &nbsp;<asp:LinkButton ID="lkAddFile" runat="server" OnClick="lkAddFile_Click" CausesValidation="False">adicionar arquivo</asp:LinkButton>
                                <ajaxToolkit:AsyncFileUpload ID="AsyncFileUpload1" runat="server" UploadingBackColor="Yellow"
                                    OnUploadedComplete="ProcessUpload" ThrobberID="spanUploading" UploaderStyle="Modern"
                                    Width="300px" Visible="False" />
                                <span id="spanUploading" runat="server" visible="false">
                                    <img class="style1" src="../../Styles/img/loading2.gif" alt="carregando" /></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                ativa?
                            </td>
                            <td class="direito">
                                <asp:CheckBox ID="chkAtiva" runat="server" Text="Está em vigor" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                usuário:
                            </td>
                            <td class="direito">
                                <asp:Label ID="lbUsuario" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th colspan="2">
                                <div id="dGravacao1" runat="server">
                                    <asp:Button ID="btInserir0" runat="server" OnClick="btInserir_Click" Style="height: 26px"
                                        Text="inserir" />
                                    <asp:Button ID="btAlterar0" runat="server" OnClick="btAlterar_Click" Text="salvar" />
                                    <asp:Button ID="btExcluir0" runat="server" CausesValidation="False" OnClick="btExcluir_Click"
                                        Text="excluir" />
                                    <asp:Button ID="btCancelar0" runat="server" CausesValidation="False" OnClick="btCancelar_Click"
                                        Text="cancelar" />
                                </div>
                            </th>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="PanelEmail" runat="server">
                    <asp:Panel ID="PanelGridEmail" runat="server">
                        <asp:GridView ID="gridEnvEmail" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            Caption="envio da circular por e-mail" CellPadding="4" CssClass="tableView" ForeColor="#333333"
                            GridLines="None" Width="100%" OnDataBinding="gridEnvEmail_DataBinding" OnRowEditing="gridEnvEmail_RowEditing">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField HeaderText="data" DataField="data" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField HeaderText="assunto" DataField="assunto" />
                                <asp:BoundField DataField="enviadoEm" DataFormatString="{0:dd/MM/yyyy}" HeaderText="enviado em" />
                                <asp:CommandField EditText="selecionar" ShowEditButton="True">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:CommandField>
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" CssClass="SortedAscendingCellStyle" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" CssClass="SortedAscendingHeaderStyle" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" CssClass="SortedDescendingCellStyle" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" CssClass="SortedDescendingHeaderStyle " />
                        </asp:GridView>
                    </asp:Panel>
                    <asp:Panel ID="PanelCadEmail" runat="server">
                        <table class="cadastro">
                            <tr>
                                <th colspan="1">
                                    envio da circular por e-mail
                                </th>
                                <th colspan="1">
                                </th>
                            </tr>
                            <tr>
                                <td class="esquerdo">
                                    data:
                                    <asp:TextBox ID="txt_id_correspEmail" runat="server" Enabled="False" Visible="False"
                                        Width="43px">0</asp:TextBox>
                                </td>
                                <td class="direito">
                                    <cdt:cData ID="cDataCorrespEmail" runat="server" Enabled="False" />
                                </td>
                            </tr>
                            <tr>
                                <td class="esquerdo">
                                    assunto:
                                </td>
                                <td class="direito">
                                    <ctx:cTexto ID="cTextoAssunto" runat="server" MaxLength="500" Width="500px" />
                                </td>
                            </tr>
                            <tr>
                                <td class="esquerdo">
                                    corpo da mensagem:
                                </td>
                                <td class="direito">
                                    <ctx:cTexto ID="cTextoCorpo" runat="server" Height="100px" TextMode="MultiLine" Width="500" />
                                </td>
                            </tr>
                            <tr>
                                <td class="esquerdo">
                                    enviado em:
                                </td>
                                <td class="direito">
                                    <cdt:cData ID="cDataEnviado" runat="server" Enabled="False" EnableValidator="False" />
                                </td>
                            </tr>
                            <tr>
                                <th colspan="2">
                                    <div id="dGravacao2" runat="server">
                                        <asp:Button ID="btSalvarEmail" runat="server" OnClick="btSalvarEmail_Click" Style="height: 26px"
                                            Text="inserir" />
                                        <asp:Button ID="btExcluirEmail" runat="server" CausesValidation="False" OnClick=" btExcluirEmail_Click"
                                            Text="excluir" />
                                    </div>
                                </th>
                            </tr>
                        </table>
                        <asp:Panel ID="PanelEscolherDestinatarios" runat="server" Visible="False">
                            destinatários: <a id="open" href="#">escolher os destinatarios</a>
                             
                            <asp:Button ID="btEnviarEmail" runat="server" Text="enviar" OnClick="btEnviarEmail_Click" />  
                        </asp:Panel>
                        <asp:Panel ID="PanelDestinatariosEmail" runat="server" Visible="False">
                            <asp:Button ID="btReencaminharEmail" runat="server" CausesValidation="False" Text="criar novo e-mail para os que não leram"
                                OnClick="btReencaminharEmail_Click" />
                            <table class="cadastro">
                                <tr>
                                    <td class="esquerdo" colspan="2">
                                        procurar destinatário:
                                        <asp:TextBox ID="txtProcuraDestinatario" runat="server" Width="300px"></asp:TextBox>
                                        <asp:Button ID="btProcuraDestinatario" runat="server" OnClick="btProcuraDestinatario_Click"
                                            Text="procurar" />
                                        &nbsp;&nbsp;
                                        <asp:Label ID="lblLidos" runat="server" Text="lidos"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="direito" colspan="2">
                                        <asp:GridView ID="GridDestinatariosDoEmail" runat="server" 
                                            AutoGenerateColumns="False" Caption="destinatários do e-mail" CellPadding="4" 
                                            CssClass="tableView" ForeColor="#333333" GridLines="None" 
                                            OnRowEditing="GridDestinatariosDoEmail_RowEditing" Width="100%" 
                                            AllowPaging="True" 
                                            onpageindexchanging="GridDestinatariosDoEmail_PageIndexChanging">
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            <Columns>
                                                <asp:BoundField DataField="nome_destinatario" HeaderText="destinatário" />
                                                <asp:BoundField DataField="email_value" HeaderText="e-mail" />
                                                <asp:BoundField DataField="enviado_em" HeaderText="enviado em" />
                                                <asp:BoundField DataField="confirmacao_leitura" HeaderText="lido em" />
                                                <asp:CommandField EditText="excluir" ShowEditButton="True">
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
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </asp:Panel>
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
