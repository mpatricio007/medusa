<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/SiteBasePagina.Master" AutoEventWireup="true" CodeBehind="ProcessoSeletivo.aspx.cs" Inherits="Medusa.Sistemas.SISPS.ProcessoSeletivo" %>
<%@ Register src="../../Controles/DdlProjetoA.ascx" tagname="DdlProjetoA" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadBaseMaster" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBaseMaster" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentBaseMaster" runat="server">
  <script type="text/javascript">
      function pageLoad() {
          $(function () {
              $(".multiOpenAccordion").accordion();
              $("#tabs").tabs();
              $(".ui-widget-content").css("background", "white");
//              $(".ui-tabs-panel").css("background", "#c0c0c0");
//              $(".ui-state-active").css("background", "#c0c0c0");
              
          });

      }

</script>
<style>
    #tabs {
	padding: 0px;
	background: none;
	border-width: 0px;
}
#tabs .ui-tabs-nav {
	padding-left: 0px;
	background: transparent;
	border-width: 0px 0px 1px 0px;
	border-radius: 0px;
	-moz-border-radius: 0px;
	-webkit-border-radius: 0px;
}
#tabs .ui-tabs-panel {
	background: url(images/ui-bg_highlight-hard_100_f5f3e5_1x100.png) repeat-x scroll 50% top #f5f3e5;
	border-width: 0px 1px 1px 1px;
}
</style>

<%--<div class="conteudo">--%>



<div class="conteudo">



        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <asp:Panel ID="panelCadastro" runat="server" >

                <div id="controleVagas" runat="server">
                   
                </div>
            
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <ajaxToolkit:UpdatePanelAnimationExtender ID="upae" BehaviorID="animation" runat="server"
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
        </ajaxToolkit:UpdatePanelAnimationExtender>
    </div>
       
    <%--</div>--%>
</asp:Content>
