<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true" CodeBehind="Fornecedores2.aspx.cs" Inherits="Medusa.Sistemas.PessoaJuridica.Fornecedores2" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>


<%@ Register src="ControleHistoricoFornecedor.ascx" tagname="ControleHistoricoFornecedor" tagprefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
   <script type="text/javascript">
       $(function () {
           $('#wizard').smartWizard(
            {
                transitionEffect: 'slideleft',
                onLeaveStep: leaveAStepCallback
            });

           //           $('#wizard').smartWizard({
           //               transitionEffect: 'slideleft',
           //               onLeaveStep: leaveAStepCallback,
           //               onFinish: onFinishCallback
           //           });

           function leaveAStepCallback(obj) {
               var step_num = obj.attr('rel'); // get the current step number
               return Page_ClientValidate(step_num); // validateSteps(step_num); // return false to stay on step and true to continue navigation 
           }

           function onFinishCallback() {
               if (validateAllSteps()) {
                   $('form').submit();
               }
           }

           // Your Step validation logic
           function validateSteps(stepnumber) {
               var isStepValid = true;
               // validate step 1
               if (stepnumber == 1) {
                   // Your step validation logic
                   // set isStepValid = false if has errors
               }
               // ...      
           }
           function validateAllSteps() {
               var isStepValid = true;
               // all step validation logic     
               return isStepValid;
           }
       });
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
               // create the loading window and set autoOpen to false
               
           });
       }

   </script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="conteudo">
        <br />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
            
             	<div id="wizard" class="swMain">
  			<ul>
  				<li><a href="#step-1">
                <label class="stepNumber">1</label>
                <span class="stepDesc">
                   Dados básicos<br />
                   <small>Dados básicos</small>
                </span>
            </a></li>
  				
  				<li><a href="#step-2">
                <label class="stepNumber">2</label>
                <span class="stepDesc">
                   Contatos<br />
                   <small>Contatos</small>
                </span>                   
             </a></li>
  				<li><a href="#step-3">
                <label class="stepNumber">3</label>
                <span class="stepDesc">
                   Outras informações<br />
                   <small>Outras informações</small>
                </span>                   
            </a></li>
            
  			</ul>
  			<div id="step-1">	
            <h2 class="StepTitle">Dados básicos</h2>
             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
        <table class="cadastro">
                        <tr>
                            <td class="esquerdo">
                                cnpj:</td>
                            <td class="direito" >
                                <cCnpj:cCNPJ ID="cCNPJ" runat="server" ValidationGroup="1" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                razão social:<asp:Label ID="txtCodigo" runat="server" Text="0" Visible="False"></asp:Label>
                            </td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoNome" runat="server" Width="500px" MaxLength="100" 
                                    ValidationGroup="1" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                denominação comercial / fantasia:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoFantasia" runat="server" Width="500px" 
                                    ValidationGroup="1" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                grupo econômico:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoGrupoEconomico" runat="server" Width="500px" ValidationGroup="1" 
                                     />
                            </td>
                        </tr>
                    </table>
                     <cender:cEnder ID="cEnder1" runat="server" ValidationGroup="1" />                   
               			  </ContentTemplate>
                         </asp:UpdatePanel>  
        </div>
  		              
  			<div id="step-2">
            <h2 class="StepTitle">Contatos</h2>	
            <table class="cadastro">
                <tr>
                            <td class="esquerdo">
                                telefone:</td>
                            <td class="direito">
                                <ctel:cTelefone ID="cTelefone1" runat="server" ValidationGroup="2" />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                home-page:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoHomePage" runat="server" Width="350px" ValidationGroup="2" 
                                   />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo" style="height: 34px">
                                e-mail:</td>
                            <td class="direito" style="height: 34px">
                                <cem:cEmail ID="cEmail1" runat="server" ValidationGroup="2" />
                            </td>
                        </tr>
            </table>                				          
        </div>
  			<div id="step-3">
            <h2 class="StepTitle">Outras Informações</h2>	

               <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                  		<table class="cadastro">
                         <tr>
                            <td class="esquerdo">
                                inscrição estadual:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoEstadual" runat="server" ValidationGroup="3"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                inscrição municipal:</td>
                            <td class="direito">
                                <ctx:cTexto ID="cTextoMunicipal" runat="server" ValidationGroup="3"/>
                            </td>
                        </tr>

                        <tr>
                            <td class="esquerdo">
                                ramo de atividade:</td>
                            <td class="direito">
                            
                            <asp:DropDownList ID="ListaRamoAtividade" runat="server" DataTextField="nome" 
                                DataValueField="id_ramo_atividade" AppendDataBoundItems="True" Width="500px" 
                                    ValidationGroup="3">
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
                                registro na junta comercial ou cartório de registro pj nº:</td>
                            <td class="direito">
                                <cint:cInteiro ID="cIntRegistro" runat="server" Width="200px" ValidationGroup="3" 
                                     />
                                data última alteração:&nbsp;
                                <cdt:cData ID="cDataAlteracao" runat="server" ValidationGroup="3" />
                                &nbsp;número:
                                <cint:cInteiro ID="cIntNumero" runat="server" Width="50px" ValidationGroup="3" 
                                    />
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                capital social:</td>
                            <td class="direito">
                                <cvl:cValor ID="cValorCapital" runat="server" ValidationGroup="3"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="esquerdo">
                                ano balanço patrimonial:</td>
                            <td class="direito">
                                <cDdlAno:cDdlAno ID="cDdlAno1" runat="server" ValidationGroup="3" />
                            </td>
                        </tr>

                               <tr>
                            <td class="esquerdo">
                                cadastro para participar de processo de :</td>
                            <td class="direito">
                                
                                <asp:DropDownList ID="listaCategoria" runat="server" 
                                    AppendDataBoundItems="True" AutoPostBack="True" DataTextField="nome" 
                                    DataValueField="id_categoria" 
                                    onselectedindexchanged="listaCategoria_SelectedIndexChanged" Width="500px" 
                                    ValidationGroup="3">
                                </asp:DropDownList>
                                <asp:CompareValidator ID="cv" runat="server" ControlToValidate="listaCategoria" 
                                    ErrorMessage="selecione uma categoria..." ForeColor="Red" Operator="NotEqual" 
                                    ValueToCompare="0"></asp:CompareValidator>
                            </td>
                        </tr>

                          
                        <tr>
                            <td class="esquerdo">
                                Documentos para serem encaminhados para a FUSP:</td>
                            <td class="direito">
                                <asp:ListBox ID="listDocumentos" runat="server" DataTextField="nome" 
                                    Width="100%" ValidationGroup="3"></asp:ListBox>
                            </td>
                        </tr>
                        </table>	
                        </ContentTemplate>
        </asp:UpdatePanel>
        </div>
       
  		</div>
                   
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