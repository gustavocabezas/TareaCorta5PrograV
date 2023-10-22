<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditarPlatillo.aspx.cs" Inherits="TareaCorta5PrograV.Paginas.EditarPlatillo" Async="true"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="../CSS/prueba.css" rel="stylesheet" />

    <h1 class="h1">Editar Platillo</h1>

 

    <div class="contenedor">
<br />
<label for="txtCategoriaPlatillo">Categoria del Platillo:</label>
<br />
<asp:DropDownList ID="ddlCategorias" runat="server" CssClass="custom-dropdown" Width="132px" OnSelectedIndexChanged="ddlCategorias_SelectedIndexChanged"></asp:DropDownList>
<br />
        <br />
<label for="txtCategoriaPlatillo">Estado del Platillo:</label>
<br />
<asp:DropDownList ID="ddlEstados" runat="server" Width="132px" CssClass="custom-dropdown" OnSelectedIndexChanged="ddlCategorias_SelectedIndexChanged"></asp:DropDownList>
<br />
        <br />
<label for="txtNombrePlatillo">Nombre del Platillo:</label>
        <br />
<asp:TextBox ID="txtNombrePlatillo" runat="server" CssClass="custom-textbox" MaxLength="100"></asp:TextBox>
        <br />
<asp:Label ID="lblMensajeNombre" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>
<br />
<label for="txtCostoPlatillo">Costo del Platillo:</label>
        <br />
<asp:TextBox ID="txtCostoPlatillo" runat="server" CssClass="custom-textbox"></asp:TextBox>
        <br />
<asp:Label ID="lblMensajeCosto" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>
        <br />
<asp:Button ID="btnAceptar" runat="server" Text="Aceptar"
            CssClass="btn btn-primary" ValidationGroup="CrearCategoriaValidation" OnClick="btnAceptar_Click1"/>
        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar"
            CssClass="btn btn-primary" ValidationGroup="CrearCategoriaValidation" />
        <br />
        <br />
    </div>
</asp:Content>
