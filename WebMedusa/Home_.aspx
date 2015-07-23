
<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/MasterPage.Master" AutoEventWireup="true" CodeBehind="Home_.aspx.cs" Inherits="Medusa.Home_" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="aspCt" %>





<asp:Content ID="Content1" ContentPlaceHolderID="HeadBaseMaster" runat="server">  
        <a type="button" class="btn btn-navbar" data-toggle="collapse" data-target=".nav-collapse">
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
          </a>

          <a class="brand" href="#"><img src="Styles/img/fusp_topo.gif"  width="120" height="30"  /></a>
          <div class="nav-collapse collapse" >
         
               <ul class="nav">
             <li class="dropdown">
             <a href="#" class="dropdown-toggle" data-toggle="dropdown">
              Links Úteis
             <b class="caret"></b>
             </a>
                 
            <ul class="dropdown-menu">            
             <li><a href="http://www.fusp.org.br/">Site da FUSP</a></li>
           <li><a href="http://portalrh.fusp.org.br/">Portal RH</a></li>
            <li><a href="../../HomePJ.aspx">Cadastro de Fornecedores</a></li>
            <li><a href="http://pac.fusp.org.br/account/login.aspx">PAC</a></li>

            </ul>
          </li>
                     <li class="divider-vertical"></li>
           </ul>
         
                
                    <ul class="nav pull-right">
                         <li class="divider-vertical"></li> <li> 
                            <asp:Label ID="Label1" runat="server" Text="Usuário" CssClass="navbar-text"></asp:Label>
  <asp:TextBox ID="txtLogin" runat="server"  MaxLength="50"  Width="70px"  ></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" 
        ErrorMessage="? " ControlToValidate="txtLogin" 
        SetFocusOnError="True" SkinID="naotem" ForeColor="Red"></asp:RequiredFieldValidator>   </li>
           
      <li>
      <asp:Label ID="Label2" runat="server" Text="Senha" CssClass="navbar-text"></asp:Label>
  <asp:TextBox ID="txtSenha" runat="server"  MaxLength="20" Width="70px"
           TextMode="Password" SkinID="naotem"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvSenha" runat="server" ControlToValidate="txtSenha"
      ErrorMessage="? " SetFocusOnError="True" SkinID="reqfield_sem_msg" 
           ForeColor="Red" ></asp:RequiredFieldValidator>
         <asp:Label ID="lblMsg" runat="server" CssClass="label label-important"></asp:Label>&nbsp
                    </li>  <li style="background-color: #4682B4">
                         <asp:LinkButton ID="btEntrar" runat="server" Text="entrar"  OnClick="btEntrar_Click" TabIndex="0" EnableTheming="false"    /> 
                 </li>  
                        
            <li >
             <asp:LinkButton ID="Button1" runat="server" Text="esqueceu sua senha?" PostBackUrl="~/EsqueceuSenha.aspx" OnClick="btEntrar_Click" EnableTheming="false"  />
                </li>
             <li class="divider-vertical"></li>
              </ul>
          
          
           
              
         
 
 </ul>
   
            </div>
          </div><!--/.nav-collapse -->
     
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBaseMaster" runat="server">



    <aspCt:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </aspCt:ToolkitScriptManager>


    
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

  

</ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentBaseMaster" runat="server">
                                    
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
          
             
           <div class="row-fluid">
            <div class="span12">
                <div class="hero-unit">
                    <div id="myCarousel" class="carousel slide">
                      
                        <div class="carousel-inner">
                            <div class="item active">
                                <img src="Styles/img/fotobanner2.jpg" />
                            </div>
                          
                            
                        </div>
                      
                    </div>
                </div>
            </div>
        </div>
                <div class="row-fluid">
            <div class="span4">
                <h4>
                    AVISOS
                </h4>
                 <p>Este é o novo portal dos sistemas da FUSP.</p>
                    <p>Futuramente todos os sistemas estarão integrados neste portal. Inicialmente estão 
                        no portal o sistema de cartas, conciliação bancária e arquivo de pastas. Nem 
                        todos tem acesso aos sistemas, caso necessite entre em contato com o Setor de 
                        Informática.</p>
                    <p>
                    Para acessar basta usar seu login e senha do antigo sistema de cartas.</p>
              
            </div>
            <div class="span4">
                <h4>
                    RAMAIS POR NOME
                </h4>
            
                <asp:BulletedList ID="BulletedList1" runat="server" DataTextField="exibir" CssClass=""
                    DataValueField="id_pessoa">
                </asp:BulletedList>
                  
                
            
               
            </div>
            <div class="span4">
                <h4>
                    RAMAIS POR SETOR
                </h4>
               
           <asp:BulletedList ID="BulletedList2" runat="server" DataTextField="exibirSetor" 
                    DataValueField="id_pessoa">
                </asp:BulletedList>
            
                
            </div>
        </div>
                     <aspCt:PagingBulletedListExtender ID="PagingBulletedListExtender1"  SelectIndexCssClass="selectIndex"
    UnselectIndexCssClass="unSelectIndex" TargetControlID="BulletedList1" runat="server">
    </aspCt:PagingBulletedListExtender>
    <aspCt:PagingBulletedListExtender ID="PagingBulletedListExtender2"  SelectIndexCssClass="selectIndex"
    UnselectIndexCssClass="unSelectIndex" IndexSize="3" TargetControlID="BulletedList2" runat="server">
    </aspCt:PagingBulletedListExtender>
    </ContentTemplate>
    </asp:UpdatePanel>
  
   
</asp:Content>
