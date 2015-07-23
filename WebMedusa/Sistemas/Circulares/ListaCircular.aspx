<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/SiteBasePagina.Master" AutoEventWireup="true"
    CodeBehind="ListaCircular.aspx.cs" Inherits="Medusa.Sistemas.Circulares.ListaCircular" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadBaseMaster" runat="server">
    <style type="text/css">
        .style1
        {
            font-size: xx-large;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBaseMaster" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentBaseMaster" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Path="../../Scripts/FixFocus.js" />
        </Scripts>
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            
            <H2>CIRCULAR NÃO ENCONTRADA!</H2>
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
