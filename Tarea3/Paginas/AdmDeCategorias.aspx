<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdmDeCategorias.aspx.cs" Inherits="TareaCorta5PrograV.Paginas.AdmDeCategorias" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../CSS/AdmCategorias.css" rel="stylesheet" />

    <h1>Administración de Categorías</h1>

    <div class="body">
        <!-- Botones y opciones -->
        <div>
            <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modalCrearCategoria">Crear Categoría</button>
            <button type="button" class="btn btn-secondary" data-bs-toggle="modal" onclick="abrirModalEditar()">Editar Categoría</button>
            <asp:Button ID="btnEliminarCategoria" runat="server" Text="Eliminar Categoría" CssClass="btn btn-danger" OnClick="BtnEliminar_Click" OnClientClick="return confirmarEliminar();" />
        </div>

        <%-- TABLA --%>
        <div class="row mt-3">
            <asp:GridView ID="Datos" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-hover table-light">
                <Columns>
                    <asp:BoundField DataField="Nombre" HeaderText="Categoria" />
                    <asp:TemplateField HeaderText="Seleccionar">
                        <ItemTemplate>
                            <input type="radio" name="selectOption"
                                value='<%# Eval("Nombre") %>'
                                onclick="setSelectedName(this.value);" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>



        <!-- Modal Crear Categoría -->
        <div class="modal fade" id="modalCrearCategoria" tabindex="-1" aria-labelledby="ariaLabelCrearCategoria" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="ariaLabelCrearCategoria">Crear Categoría</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                    </div>
                    <div class="modal-body">
                        <!-- Formulario para crear categorías -->
                        <div class="row">
                            <div class="form mt-3">
                                <div class="form-group mb-3">
                                    <label for="categoria" class="form-label">Nombre de categoría</label>
                                    <asp:TextBox ID="TextBoxCategoriaCrear" class="form-control" aria-describedby="Categoria" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="ButtonCancelar" class="btn btn-secondary" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" data-bs-dismiss="modal" />
                        <asp:Button ID="ButtonAceptar" class="btn btn-primary" runat="server" Text="Aceptar" OnClick="BtnCrear_Click" OnClientClick="return confirmarCrear();" />
                    </div>
                </div>
            </div>
        </div>



        <!-- Modal Editar Categoría -->
        <div class="modal fade" id="modalEditarCategoria" tabindex="-1" aria-labelledby="ariaLabelEditarCategoria" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="ariaLabelEditarCategoria">Editar Categoría</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="form mt-3">
                                <div class="form-group mb-3">
                                    <label for="categoria" class="form-label">Nombre de categoría</label>
                                    <asp:TextBox ID="TextBoxEditarCategoria" class="form-control" aria-describedby="Categoria" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnEditarCategoriaCancelar" class="btn btn-secondary" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" data-bs-dismiss="modal" />
                        <asp:Button ID="btnEditarCategoriaEliminar" class="btn btn-primary" runat="server" Text="Aceptar" OnClick="BtnEditar_Click" OnClientClick="return confirmarTextoEditar();" />
                    </div>
                </div>
            </div>
        </div>





        <!-- Hidden field to store the selected ID -->
        <asp:HiddenField ID="hiddenSelectedName" runat="server" />
        <script type="text/javascript">
            function setSelectedName(value) {
                document.getElementById('<%= hiddenSelectedName.ClientID %>').value = value;
            }
        </script>

        <%-- SCRIPTS --%>
        <script type="text/javascript">



            function confirmarCrear() {
                // Obtener el valor
                var selectedName = document.getElementById('<%= TextBoxCategoriaCrear.ClientID %>').value;



                // Verificar si el campo esta vació
                if (selectedName === "") {
                    alert("El nombre no puede ser vacío.");
                    return false; // Evitar que se envíe el evento al servidor
                }



                return true;
            }

            function abrirModalEditar() {
                if (confirmarEditar()) {
                    // Abre el modal solo si la confirmación es exitosa
                    $('#modalEditarCategoria').modal('show');
                }
            }

            function confirmarEditar() {
                // Obtener el valor
                var selectedName = document.getElementById('<%= hiddenSelectedName.ClientID %>').value;



                // Verificar si el campo esta vació
                if (selectedName === "") {
                    alert("Por favor seleccione la categoría.");
                    return false; // Evitar que se envíe el evento al servidor
                }

                document.getElementById('<%= TextBoxEditarCategoria.ClientID %>').value = selectedName;
                return true;
            }



            function confirmarTextoEditar() {
                // Obtener el valor
                var selectedName = document.getElementById('<%= TextBoxEditarCategoria.ClientID %>').value;



                // Verificar si el campo esta vació
                if (selectedName === "") {
                    alert("El nombre no puede ser vacío.");
                    return false; // Evitar que se envíe el evento al servidor
                }



                return true;
            }



            function confirmarEliminar() {
                // Obtener el valor del campo oculto
                var selectedName = document.getElementById('<%= hiddenSelectedName.ClientID %>').value;



                // Verificar si el nombre esta vacío
                if (selectedName === "") {
                    alert("Por favor seleccione la categoría.");
                    return false; // Evitar que se envíe el evento al servidor
                }

                var resultado = window.confirm("¿Desea eliminar la categoría seleccionada?");
                if (resultado) {
                    return true;
                }


                return false;
            }
        </script>



        <!-- Mensaje de error -->
        <%--<asp:Label ID="lblMensaje" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>--%>
    </div>

</asp:Content>
