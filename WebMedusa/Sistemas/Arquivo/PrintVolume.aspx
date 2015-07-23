<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintVolume.aspx.cs" Inherits="Medusa.Sistemas.Arquivo.PrintVolume" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Panel ID="Panel1" runat="server" BorderStyle="Solid" 
            style="text-align: center" Width="530px" BorderWidth="3px">
            <table>
            <tr>
            <td>
            <table>
            <tr>
                            <td style="width:400px; text-align: left;" colspan="2">
            <asp:Label ID="txtNome" runat="server" Font-Bold="True" style="font-size: 13pt"></asp:Label>
            
            </td>
            </tr>
            <tr>
            <td style="text-align:left" colspan="2">
            <asp:Label ID="txtCoordenador" runat="server" Font-Bold="True" style="font-size: 13pt"></asp:Label>
            </td>
            </tr>
            <tr>
            <td style="text-align:left"><asp:Label ID="txtProjeto" runat="server" Font-Bold="True" style="font-size: 13pt"></asp:Label></td>
           
            <td style="text-align:right"><asp:Label ID="txtProjetoA" runat="server" Font-Bold="True" style="font-size: 13pt"></asp:Label></td>
            </tr>
            <tr>
          <td style="text-align:left" colspan="2">
            <asp:Label ID="txtDescricao" runat="server" Font-Size="13pt" Font-Bold="True" ForeColor="Red" 
                                    style="text-align: left; font-size: 7pt;"></asp:Label>
          </td>
            
           </tr>
            
            
            </table>
                            </td>
                            <td style="float:right">
                            <asp:Image ID="Image1" runat="server" />
                            <br />         
            <asp:Label ID="Id_volume" runat="server"></asp:Label>
                            </td>
                        </tr></table>
       </asp:Panel>
    
    </div>
    </form>
</body>
</html>
