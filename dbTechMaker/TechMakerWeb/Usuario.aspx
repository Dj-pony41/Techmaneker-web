<%@ Page Title="" Language="C#" MasterPageFile="~/AdmMaster.Master" AutoEventWireup="true" CodeBehind="Usuario.aspx.cs" Inherits="TechMakerWeb.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Css/Style_Formularios.css" />
    <script type="text/javascript">
        function validateForm() {
            var name = document.getElementById('<%= txtName.ClientID %>').value.trim();
            var lastName = document.getElementById('<%= txtLastName.ClientID %>').value.trim();
            var secondLastName = document.getElementById('<%= txtSecondLastName.ClientID %>').value.trim();
            var email = document.getElementById('<%= txtMail.ClientID %>').value.trim();
            var role = document.getElementById('<%= ddlRole.ClientID %>').value.trim();
            var carrera = document.getElementById('<%= Carrera.ClientID %>').value.trim();

            if (name === "") {
                alert("El campo 'Nombre' es obligatorio.");
                return false;
            }
            if (lastName === "") {
                alert("El campo 'Apellido Paterno' es obligatorio.");
                return false;
            }
            if (secondLastName === "") {
                alert("El campo 'Apellido Materno' es obligatorio.");
                return false;
            }
            if (email === "") {
                alert("El campo 'Correo Electrónico' es obligatorio.");
                return false;
            }
            if (!validateEmail(email)) {
                alert("Por favor ingrese un correo electrónico válido.");
                return false;
            }
            if (role === "") {
                alert("El campo 'Rol' es obligatorio.");
                return false;
            }
            if (carrera === "") {
                alert("El campo 'Carrera' es obligatorio.");
                return false;
            }
            return true;
        }

        function validateEmail(email) {
            var re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
            return re.test(email);
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="formUsuario" runat="server" onsubmit="return validateForm();">
        <div class="box-form">
            <h2>Registro de Usuario</h2>
            <br />
            <div class="input-group">
                <asp:TextBox ID="txtName" placeholder="Nombre" runat="server" />
                <asp:TextBox ID="txtLastName" placeholder="Apellido Paterno" runat="server" />
                <asp:TextBox ID="txtSecondLastName" placeholder="Apellido Materno" runat="server" />
                <asp:TextBox ID="txtMail" placeholder="Correo Electrónico" runat="server" />
                <div class="select">
                    <asp:DropDownList ID="ddlRole" CssClass="form-control" runat="server">
                        <asp:ListItem Text="Administrador" Value="Administrador" />
                        <asp:ListItem Text="Usuario" Value="Usuario" />
                        <asp:ListItem Text="Director" Value="Director" />
                    </asp:DropDownList>
                </div>
                <div class="select">
                    <asp:DropDownList ID="Carrera" CssClass="form-control" runat="server"></asp:DropDownList>
                </div>
                <asp:Button ID="btnRegistrar" class="btn" runat="server" Text="Registrar" OnClick="btnRegistrar_Click" />
                <asp:Button Visible="false" ID="btnUpdate" class="btn" runat="server" Text="Actualizar" OnClick="btnUpdate_Click" />
            </div>
        </div>
    </form>
</asp:Content>
