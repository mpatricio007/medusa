<%@ Page Title="About Us" Language="C#" MasterPageFile="~/Styles/SiteBase.Master"
    AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="Medusa.About" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadBaseMaster">

</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="MainContentBaseMaster">
    <h2>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    </h2>
    <h2>
        SOBRE
   
        </h2>
      <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
          &nbsp;<p>
                  Licensed to: Fundação de Apoio à Universidade de São Paulo.</p>
     
              
     </ContentTemplate></asp:UpdatePanel>
    
  
</asp:Content>
