
<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/SiteBase.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Medusa.Home" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="aspCt" %>





<asp:Content ID="Content1" ContentPlaceHolderID="HeadBaseMaster" runat="server">  

</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBaseMaster" runat="server">

    
    <aspCt:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </aspCt:ToolkitScriptManager>


    
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

    <div class="loginFields">
  
  Nome de Usuário
  <asp:TextBox ID="txtLogin" runat="server" Width="125px" MaxLength="50"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" 
        ErrorMessage="? " ControlToValidate="txtLogin" 
        SetFocusOnError="True" SkinID="naotem" ForeColor="Red"></asp:RequiredFieldValidator>   
      Senha
  <asp:TextBox ID="txtSenha" runat="server" Width="125px" MaxLength="20" 
           TextMode="Password" SkinID="naotem"></asp:TextBox>

    <asp:RequiredFieldValidator ID="rfvSenha" runat="server" ControlToValidate="txtSenha"
      ErrorMessage="? " SetFocusOnError="True" SkinID="reqfield_sem_msg" 
           ForeColor="Red" ></asp:RequiredFieldValidator>
    <asp:Button ID="btEntrar" runat="server" Text="entrar" BackColor="#404040" BorderStyle="Double" ForeColor="White" OnClick="btEntrar_Click" />
  
    <asp:HyperLink ID="hlSenha" runat="server" Font-Underline="True" NavigateUrl="EsqueceuSenha.aspx" ForeColor="White" >esqueceu sua senha?</asp:HyperLink>&nbsp;&nbsp;
    <asp:LinkButton ID="lkNew" runat="server" 
                            PostBackUrl="~/Sistemas/Admin/UsuariosExternos.aspx" 
                            CausesValidation="False" ForeColor="White">Não possui cadastro? Cadastre-se!</asp:LinkButton>

    <asp:Label ID="lblMsg" runat="server"></asp:Label>


</div>

 

</ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentBaseMaster" runat="server">
                                    
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
    <div id="master_page">
        <div id="master_menu">
            <%--         <div class="sidebar">
                <div class="sidebarheader">Avisos</div>
                <div id="master_sidebarSamples" class="sidebarcontent">
                </div>
            </div>--%>
            <div class="sidebar">
                <div class="sidebarheader">Links</div>
                            
                <div id="master_sidebarLinks" class="sidebarcontent">
                    <asp:HyperLink ID="ConhecaFUSP" runat="server" Target="_blank" Text="Site da FUSP" NavigateUrl="http://www.fusp.org.br" EnableViewState="false" /><br />
                    <asp:HyperLink ID="PortalRH" runat="server"  Target="_blank" Text="Portal RH" NavigateUrl="http://portalrh.fusp.org.br" EnableViewState="false" /><br />
                    <asp:HyperLink ID="Demonstrativo" runat="server" Target="_blank" Text="Demonstrativo" NavigateUrl="http://demonstrativo.fusp.org.br" EnableViewState="false" /><br />
                    <asp:HyperLink ID="CadastroPJ" runat="server" Target="_blank" Text="Cadastro de Fornecedores" NavigateUrl="~/HomePJ.aspx" EnableViewState="false" />
                    
                </div>
                <br />
                <div class="sidebarheader">AVISO</div>
                            
                <div  class="sidebarcontent">
                    <p>Este é o novo portal dos sistemas da FUSP.</p>
                    <p>Futuramente todos os sistemas estarão integrados neste portal. Inicialmente estão 
                        no portal o sistema de cartas, conciliação bancária e arquivo de pastas. Nem 
                        todos tem acesso aos sistemas, caso necessite entre em contato com o Setor de 
                        Informática.</p>
                    <p>
                    Para acessar basta usar seu login e senha do antigo sistema de cartas.</p></div>
            </div>
        </div>
        
        
        
        </div>
<div id="master_content">
<div id="master_contentheader">
   
<div id="master_contentplaceholder">

    
        <asp:Panel ID="Panel2" runat="server" CssClass="collapsePanelHeader" Height="30px"> 
            <div style="padding:5px; cursor: pointer; vertical-align: middle;">
                <div style="float: left;">ramais por nome</div>
                <div style="float: left; margin-left: 20px;">
               </div>
                <div style="float: right; vertical-align: middle;">
                    <asp:ImageButton ID="Image1" runat="server" 
                        ImageUrl="Styles/img/expand_blue.jpg" 
                        AlternateText="(mostrar detalhes...)"  
                        CausesValidation="False"/>
                </div>
            </div>
        </asp:Panel>
            <asp:Panel ID="Panel1" runat="server" CssClass="collapsePanel" Height="0">
               <div style="height: 120px; overflow: auto; overflow-y: scroll; border: solid 1px #CCCCCC;">
                <asp:BulletedList ID="BulletedList1" runat="server" DataTextField="exibir" 
                    DataValueField="id_pessoa">
                </asp:BulletedList>
               </div>
        </asp:Panel>
        <asp:Panel ID="Panel4" runat="server" CssClass="collapsePanelHeader" Height="30px"> 
            <div style="padding:5px; cursor: pointer; vertical-align: middle;">
                <div style="float: left;">ramais por setor</div>
                <div style="float: left; margin-left: 20px;">
               </div>
                <div style="float: right; vertical-align: middle;">
                    <asp:ImageButton ID="ImageButton1" runat="server" 
                        ImageUrl="Styles/img/expand_blue.jpg" 
                        AlternateText="(mostrar detalhes...)"  
                        CausesValidation="False"/>
                </div>
            </div>
        </asp:Panel>
            <asp:Panel ID="Panel3" runat="server" CssClass="collapsePanel" Height="0">
               <div style="height: 120px; overflow: auto; overflow-y: scroll; border: solid 1px #CCCCCC;">
                <asp:BulletedList ID="BulletedList2" runat="server" DataTextField="exibirSetor" 
                    DataValueField="id_pessoa">
                </asp:BulletedList>
               </div>
        </asp:Panel>

    <aspCt:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server"
        TargetControlID="Panel1"
        ExpandControlID="Panel2"
        CollapseControlID="Panel2" 
        Collapsed="False"
        ImageControlID="Image1"    
        ExpandedText="(esconder detalhes...)"
        CollapsedText="(mostrar detalhes...)"
        ExpandedImage="Styles/img/collapse_blue.jpg"
        CollapsedImage="Styles/img/expand_blue.jpg"
        SuppressPostBack="false"
        SkinID="CollapsiblePanelDemo"/>
    <aspCt:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="server"
        TargetControlID="Panel3"
        ExpandControlID="Panel4"
        CollapseControlID="Panel4" 
        Collapsed="True"
        ImageControlID="ImageButton1"     
        ExpandedText="(esconder detalhes...)"
        CollapsedText="(mostrar detalhes...)"
        ExpandedImage="Styles/img/collapse_blue.jpg"
        CollapsedImage="Styles/img/expand_blue.jpg"
        SuppressPostBack="false"
        SkinID="CollapsiblePanelDemo"/>
    
    <aspCt:PagingBulletedListExtender ID="PagingBulletedListExtender1"  SelectIndexCssClass="selectIndex"
    UnselectIndexCssClass="unSelectIndex" TargetControlID="BulletedList1" runat="server">
    </aspCt:PagingBulletedListExtender>
    <aspCt:PagingBulletedListExtender ID="PagingBulletedListExtender2"  SelectIndexCssClass="selectIndex"
    UnselectIndexCssClass="unSelectIndex" IndexSize="3" TargetControlID="BulletedList2" runat="server">
    </aspCt:PagingBulletedListExtender>

  </div>
</div>    
    </ContentTemplate>
    </asp:UpdatePanel>
  
   
</asp:Content>
