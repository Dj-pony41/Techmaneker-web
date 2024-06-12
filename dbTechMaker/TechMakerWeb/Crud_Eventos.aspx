<%@ Page Title="" Language="C#" MasterPageFile="~/AdmMaster.Master" AutoEventWireup="true" CodeBehind="Crud_Eventos.aspx.cs" Inherits="TechMakerWeb.Crud_Eventos" Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Css/Styles_Crud_v2.css" />
    <script type="text/javascript">
        function validateForm() {
            var nombreEvento = document.getElementById('<%= txtNombreEvento.ClientID %>').value.trim();
            var descripcionEvento = document.getElementById('<%= txtDescripcionEvento.ClientID %>').value.trim();
            var gestionEvento = document.getElementById('<%= txtGestionEvento.ClientID %>').value.trim();
            var fechaInicio = document.getElementById('<%= txtFechaInicio.ClientID %>').value.trim();
            var fechaFin = document.getElementById('<%= txtFechaFin.ClientID %>').value.trim();

            var today = new Date();
            today.setHours(0, 0, 0, 0); // Establecer la hora a 00:00:00 para comparar solo la fecha

            if (nombreEvento === "") {
                alert("El campo 'Nombre del Evento' es obligatorio.");
                return false;
            }
            if (descripcionEvento === "") {
                alert("El campo 'Descripción del Evento' es obligatorio.");
                return false;
            }
            if (gestionEvento === "") {
                alert("El campo 'Gestión del Evento' es obligatorio.");
                return false;
            }
            if (fechaInicio === "") {
                alert("El campo 'Fecha de Inicio del Evento' es obligatorio.");
                return false;
            }
            if (fechaFin === "") {
                alert("El campo 'Fecha Fin del Evento' es obligatorio.");
                return false;
            }

            // Convertir las fechas de texto a objetos Date
            var startDate = new Date(fechaInicio);
            var endDate = new Date(fechaFin);

            // Validar que las fechas no sean anteriores a hoy
            if (startDate < today) {
                alert("La 'Fecha de Inicio del Evento' no puede ser anterior a hoy.");
                return false;
            }
            if (endDate < today) {
                alert("La 'Fecha Fin del Evento' no puede ser anterior a hoy.");
                return false;
            }

            return true;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="formEvento" runat="server" onsubmit="return validateForm();">
        <div class="box-form">
            <h2>Formulario de Evento</h2>
            <div class="input-group">
                <asp:Label ID="lblNombreEvento" runat="server" AssociatedControlID="txtNombreEvento" Text="Nombre del Evento:" CssClass="lbl"></asp:Label>
                <asp:TextBox ID="txtNombreEvento" CssClass="form-control" placeholder="Nombre del Evento" runat="server" />

                <asp:Label ID="lblDescripcionEvento" runat="server" AssociatedControlID="txtDescripcionEvento" Text="Descripción del Evento:" CssClass="lbl"></asp:Label>
                <asp:TextBox ID="txtDescripcionEvento" CssClass="form-control" TextMode="MultiLine" Rows="4" placeholder="Descripción del Evento" runat="server" />

                <asp:Label ID="lblGestionEvento" runat="server" AssociatedControlID="txtGestionEvento" Text="Gestión del Evento:" CssClass="lbl"></asp:Label>
                <asp:TextBox ID="txtGestionEvento" CssClass="form-control" placeholder="Ej. 1 - 2024" runat="server" />

                <asp:Label ID="lblFechaInicio" runat="server" AssociatedControlID="txtFechaInicio" Text="Fecha de Inicio del Evento:" CssClass="lbl"></asp:Label>
                <asp:TextBox ID="txtFechaInicio" CssClass="form-control" TextMode="DateTimeLocal" runat="server" />

                <asp:Label ID="lblFechaFin" runat="server" AssociatedControlID="txtFechaFin" Text="Fecha Fin del Evento:" CssClass="lbl"></asp:Label>
                <asp:TextBox ID="txtFechaFin" CssClass="form-control" TextMode="DateTimeLocal" runat="server" />

                <asp:Button ID="btnEnviar" runat="server" Text="Enviar" OnClick="btnEnviar_Click" CssClass="btn" />
            </div>
        </div>
    </form>
</asp:Content>