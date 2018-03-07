<%@ Page Title="Management Citizens" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Citizens.aspx.cs" Inherits="Tatooine.WebUI.Pages.Citizens"
    EnableEventValidation="false" %>

<asp:Content ID="contentCitizens" ContentPlaceHolderID="MainContent" runat="server">
    <div id="title">
        <h2>Management Citizens</h2>
    </div>
    <div>
        <asp:Panel ID="pnlMessage" runat="server" Visible="false">
            <asp:Image ID="imgMessage" runat="server" Width="35px" Height="35px" />
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </asp:Panel>
    </div>
    <hr />
    <div>
        <div class="form-group">
            <label for="txtName">Name:</label>
            <asp:TextBox ID="txtName" runat="server" ClientIDMode="Static" MaxLength="100" Width="300" TabIndex="1"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName" Display="Dynamic">
                The Name is required.
            </asp:RequiredFieldValidator>
        </div>

        <div class="form-group">
            <label for="txtSpecieType">Specie Type:</label>
            <asp:TextBox ID="txtSpecieType" runat="server" ClientIDMode="Static" MaxLength="100" Width="300" TabIndex="2"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvSpecieType" runat="server" ControlToValidate="txtSpecieType" Display="Dynamic">
                The Specie Type is required.
            </asp:RequiredFieldValidator>
        </div>

        <div class="form-group">
            <label for="ddlRole">Role:</label>
            <asp:DropDownList ID="ddlRole" runat="server" ClientIDMode="Static" TabIndex="3"></asp:DropDownList>
        </div>
        <div class="form-group">
            <label for="ddlCitizenStatus">Citizen Status:</label>
            <asp:DropDownList ID="ddlCitizenStatus" runat="server" ClientIDMode="Static" TabIndex="4"></asp:DropDownList>
        </div>
        <br />
        <asp:HiddenField ID="hfCitizenID" runat="server" />
        <asp:Button ID="btnSaveCitizen" runat="server" Text="Save" CausesValidation="true" OnClick="btnSaveCitizen_Click" CssClass="btn btn-default" />
    </div>
    <hr />
    <div>
        <asp:GridView ID="gvCitizens" runat="server" EmptyDataText="There is no registered Citizens." AutoGenerateColumns="false" DataKeyNames="ID" GridLines="None"
            OnRowCommand="gvCitizens_RowCommand" CssClass="table table-hover table-striped">
            <Columns>
                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <%# Eval("Name") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Specie Type">
                    <ItemTemplate>
                        <%# Eval("SpecieType") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Role">
                    <ItemTemplate>
                        <%# Eval("Role.Description") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Citizen Status">
                    <ItemTemplate>
                        <%# Eval("CitizenStatus.Description") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Button ID="btnUserToRole" runat="server" CommandName="_userRole" CommandArgument='<%# Eval("ID") %>' Text="User To Role" ToolTip="Change User Role" CausesValidation="false" CssClass="btn btn-default" />
                        <asp:Button ID="btnUpdateUser" runat="server" CommandName="_updateUser" CommandArgument='<%# Eval("ID") %>' Text="Update" ToolTip="Update User Information" CausesValidation="false" CssClass="btn btn-default" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <script src="../Scripts/jquery.blockUI.js"></script>
    <script src="../Scripts/jquery.cookie.js"></script>
    <script src="../Scripts/Citizen.js"></script>
</asp:Content>
