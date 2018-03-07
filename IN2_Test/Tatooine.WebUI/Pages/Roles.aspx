<%@ Page Title="Management Roles" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Roles.aspx.cs" Inherits="Tatooine.WebUI.Pages.Roles"
    EnableEventValidation="false" %>

<asp:Content ID="contentCitizens" ContentPlaceHolderID="MainContent" runat="server">
    <div id="title">
        <h2>Management Roles</h2>
    </div>
    <div>
        <asp:Panel ID="pnlMessage" runat="server" Visible="false">
            <asp:Image ID="imgMessage" runat="server" Width="35px" Height="35px" />
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </asp:Panel>
    </div>
    <hr />
    <div>
        <h3>Role Information</h3>
        <div class="form-group">
            <label for="txtDescription">Description:</label>
            <asp:TextBox ID="txtDescription" runat="server" ClientIDMode="Static" MaxLength="150" Width="300" TabIndex="1"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ControlToValidate="txtDescription" Display="Dynamic">
                The Description is required.
            </asp:RequiredFieldValidator>
        </div>

        <br />
        <asp:Button ID="btnSaveRole" runat="server" Text="Save" CausesValidation="true" OnClick="btnSaveRole_Click" CssClass="btn btn-default" />
    </div>
    <hr />
    <div>
        <h3>Users in Roles list</h3>
        <asp:GridView ID="gvRoles" runat="server" EmptyDataText="There is no Users in Roles to show." AutoGenerateColumns="false" DataKeyNames="ID" GridLines="None"
            CssClass="table table-hover table-striped">
            <Columns>
                <asp:TemplateField HeaderText="User Name">
                    <ItemTemplate>
                        <%# Eval("Name") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Role ID">
                    <ItemTemplate>
                        <%# Eval("Role.ID") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Role Description">
                    <ItemTemplate>
                        <%# Eval("Role.Description") %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
