<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucMessage.ascx.cs" Inherits="Tatooine.WebUI.UserControls.ucMessage" %>
<asp:Panel ID="pnlMessage" runat="server" Height="100%" Width="100%" Visible="false">
    <table style="width: 100%; height: 100%; border: 2px; border-collapse: collapse; border-spacing: 0px;">
        <tr>
            <td>
                <asp:Image ID="imgMessage" runat="server" />
            </td>
            <td>
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblDescription" runat="server" Visible="false"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Panel>
