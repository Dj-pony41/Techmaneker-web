<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Evaluador.aspx.cs" Inherits="TechMakerWeb.WebForm5" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Registro de Usuario</title>
    <link rel="stylesheet" href="Css/Styles_Crud_v3.css" />
</head>
<body>
    <form id="formRegistro" runat="server">
        <div class="box-form">
            <h2>Registro de Usuario</h2>
            <hr />
            <div class="input-group">
                <label for="txtUserName" class="lbl">Nombre de Usuario:</label>
                <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control"/>
            
                <label for="txtPassword" class="lbl">Contraseña:</label>
                <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" CssClass="form-control"/>
            
                <label for="txtName" class="lbl">Nombre:</label>
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control"/>
           
                <label for="txtLastName" class="lbl">Apellido Paterno:</label>
                <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"/>
            
                <label for="txtSecondLastName" class="lbl">Apellido Materno:</label>
                <asp:TextBox ID="txtSecondLastName" runat="server" CssClass="form-control"/>
            
                <label for="txtMail" class="lbl">Correo Electrónico:</label>
                <asp:TextBox ID="txtMail" runat="server" CssClass="form-control"/>
            
                <div class="hidden">
                    <asp:DropDownList ID="ddlRole" runat="server">
                        <asp:ListItem Text="Evaluador" Value="Evaluador" />
    
                    </asp:DropDownList>
                </div>
                
            
           <div class="hidden">
                <div class="box-body">
                    <div class="form-group">
                        <asp:Label Text="Carrera:" runat="server" />
                        <asp:DropDownList ID="Carrera" runat="server"></asp:DropDownList>
                    </div>
                </div>
            </div>

                <asp:Button ID="btnRegistrar" Text="Registrar" OnClick="btnRegistrar_Click" runat="server" CssClass="btn"/>
                <asp:Button class="hidden" ID="btnUpdate" Text="Actualizar" OnClick="btnUpdate_Click" runat="server" />
          </div> 
        </div>
       
       
    </form>
</body>

    <script type="text/javascript">
        function validateForm() {
            var userName = document.getElementById('<%= txtUserName.ClientID %>').value;
            var password = document.getElementById('<%= txtPassword.ClientID %>').value;
            var name = document.getElementById('<%= txtName.ClientID %>').value;
        var lastName = document.getElementById('<%= txtLastName.ClientID %>').value;
        var secondLastName = document.getElementById('<%= txtSecondLastName.ClientID %>').value;
        var email = document.getElementById('<%= txtMail.ClientID %>').value;

            // Check if any field is empty
            if (userName === '' || password === '' || name === '' || lastName === '' || secondLastName === '' || email === '') {
                alert('Por favor, completa todos los campos.');
                return false;
            }

            // Validate email format
            var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
            if (!emailRegex.test(email)) {
                alert('Por favor, introduce un correo electrónico válido.');
                return false;
            }

            return true;
        }
    </script>

</html>
