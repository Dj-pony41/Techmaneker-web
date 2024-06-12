<%@ Page Language="C#" MasterPageFile="~/DocMaster.Master" AutoEventWireup="true" CodeBehind="Proyecto.aspx.cs" Inherits="TechMakerWeb.WebForm4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Css/Style_Formularios.css" />
    <script type="text/javascript">
        function validateForm() {
            var nombreProyecto = document.getElementById('<%= txtNombreProyecto.ClientID %>').value.trim();
            var descripcion = document.getElementById('<%= txtDescripcion.ClientID %>').value.trim();
            var evento = document.getElementById('<%= Evento.ClientID %>').value.trim();
            
            if (nombreProyecto === "") {
                alert("El campo 'Nombre del Proyecto' es obligatorio.");
                return false;
            }
            if (evento === "") {
                alert("El campo 'Evento' es obligatorio.");
                return false;
            }
            if (descripcion === "") {
                alert("El campo 'Descripcion' es obligatorio.");
                return false;
            }
            return true;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="formRegistro" runat="server" class="form-container" onsubmit="return validateForm();">
        <div class="box-form">
            <h2>Registro de Proyecto</h2>
            <br />
            <div class="input-group">
                <asp:TextBox ID="txtNombreProyecto" runat="server" CssClass="form-control" placeholder="Nombre del Proyecto" />
                <div class="select">
                    <asp:DropDownList ID="Evento" runat="server"></asp:DropDownList>
                </div>
                
                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" cols="30" Rows="5" placeholder="Descripcion" />
            </div>
            <asp:Button ID="btnRegistrarProyecto" Text="Registrar Proyecto" OnClick="btnRegistrarProyecto_Click" runat="server" CssClass="btn" />
            <asp:Button Visible="false" ID="btnActualizarProyecto" Text="Actualizar Proyecto" OnClick="btnActualizarProyecto_Click" runat="server" CssClass="btn" />
        </div>
    </form>
</asp:Content>
