<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListaEmails.ascx.cs" Inherits="Medusa.Controles.ListaEmails" %>


<cem:cEmail ID="cEmail1" runat="server" ValidationGroup="email" />
<asp:Button ID="btAdd" runat="server" onclick="btAdd_Click" Text="salvar" 
    ValidationGroup="email" />
<asp:Button ID="btExcluir" runat="server" onclick="btExcluir_Click" 
    Text="excluir"  ValidationGroup="email" />



<asp:Button ID="btCancel" runat="server" onclick="btCancel_Click" 
    Text="cancelar" CausesValidation="False" />

<asp:Label ID="txtId" runat="server" Font-Bold="False" 
    Text="-1" Visible="false"></asp:Label>



<p>
    <asp:GridView ID="gridEmails" runat="server" 
        ondatabinding="gridEmails_DataBinding" 
        onrowdeleting="gridEmails_RowDeleting" AutoGenerateColumns="False" 
        Width="500px">
        <Columns>
            <asp:TemplateField HeaderText="email">
              
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("value") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="450px" />
            </asp:TemplateField>
            <asp:CommandField ShowDeleteButton="True" DeleteText="selecionar" >
            <ItemStyle Width="50px" />
            </asp:CommandField>
        </Columns>
        <HeaderStyle HorizontalAlign="Left" />
    </asp:GridView>
</p>

