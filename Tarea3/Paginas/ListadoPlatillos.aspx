<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListadoPlatillos.aspx.cs" Inherits="TareaCorta5PrograV.Paginas.ListadoPlatillos" Async="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<link href="../CSS/ListadoPlatillos.css" rel="stylesheet" />

        <h1>Lista de Platillos</h1>

    <div class="row">
            <div class ="col-md-10 col-md-offset-1">
                <div class="table-responsive">
                    <asp:GridView ID="Datos" Width="100%" runat="server" AutoGenerateColumns="False" 
                        CellPadding="4" ForeColor="#333333" GridLines="None" Height="292px" OnSelectedIndexChanged="Datos_SelectedIndexChanged" 
                        CssClass="table table-bordered tab-condensed table-responsive table-hover">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <HeaderStyle BackColor="#6b696b" Font-Bold="true" Font-Size="Larger" ForeColor="White"/>
                        <RowStyle BackColor="#f5f5f5"/>
                        <SelectedRowStyle BackColor="#669999" Font-Bold="true" ForeColor="White"/>
                        <Columns>
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre del Platillo" />
                            <asp:BoundField DataField="Categoria" HeaderText="Categoría" />
                            <asp:BoundField DataField="Estado" HeaderText="Estado" />
                            <asp:BoundField DataField="Costo" HeaderText="Costo" />
                            <asp:CommandField ShowSelectButton="True" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div> 
    



    <!-- Botones y opciones -->
    <div id="opciones">

        <asp:Button ID="btnCrear" runat="server" Text="Crear Platillo"  CssClass="btn btn-primary" OnClick="btnCrear_Click"/>
        <asp:Button ID="btnEditar" runat="server" Text="Editar Platillo"  CssClass="btn btn-primary" OnClick="btnEditar_Click"/>
        <asp:Button ID="btnEliminar" runat="server" Text="Borrar Platillo"  CssClass="btn btn-primary" OnClick="btnEliminar_Click"/>
    
        <asp:Button ID="btnActivar" runat="server" Text="Activar Platillo"  CssClass="btn btn-primary" OnClick="btnActivar_Click"/>
        <asp:Button ID="btnInactivar" runat="server" Text="Inactivar Platillo"  CssClass="btn btn-primary" OnClick="btnInactivar_Click1"/>
        
        <asp:Button ID="TxtAdmCategoria" runat="server" Text="Administrar Categorías"  CssClass="btn btn-primary" OnClick="TxtAdmCategoria_Click"/>
    </div>

</asp:Content>



