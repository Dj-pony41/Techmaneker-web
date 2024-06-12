<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TechMakerWeb.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    
    <link rel="stylesheet" href="Css/Style_Login.css" />
    <title>Iniciar Sesión</title>
</head>
<body>
    
    <div class="container" id="container">
        <form id="formLogin" runat="server">
            <div class="form-container sign-up">
                <div class="form-container-session">
                    <h1>Recuperar Contraseña</h1>
               
                    <span>Usa tu correo para la recuperacion</span>
                    <asp:TextBox ID="txtMail" runat="server" placeholder="Correo"></asp:TextBox>
                    <asp:TextBox ID="txtUserName" runat="server" placeholder="Usuario"></asp:TextBox>
                    <a class="hidden btn2" id="login2">Iniciar</a>
                    <asp:Button ID="btnMandar" runat="server" class="input-button" Text="Mandar cambio de contraseña" OnClick="btnMandar_OnClick" />
                </div>
            </div>
            <div class="form-container sign-in">
                <div class="form-container-session">
                    <h1>Iniciar Sesión</h1>
                
                    <span>Usa tu usuario y contraseña</span>
                    <asp:TextBox ID="txtUsuario" placeholder="Usuario" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtContrasena" placeholder="Contraseña" runat="server" TextMode="Password"></asp:TextBox>
                    <a class="hidden btn2" id="register2">Olvido su Contraseña?</a>
                    <asp:Button ID="btnIniciarSesion" runat="server" class="input-button" Text="Iniciar Sesión" OnClick="btnLogin" />
                </div>
            </div>
            <div class="toggle-container">
                <div class="toggle">
                    <div class="toggle-panel toggle-left">
                        <h1>TechMaker</h1>
                        <p>No compartas tus credenciales con nadie</p>
                        <a class="hidden" id="login">Iniciar</a>
                    </div>
                    <div class="toggle-panel toggle-right">
                        <h1>TechMaker</h1>
                        <p>No compartas tus credenciales con nadie</p>
                        <a class="hidden" id="register">Olvido su Contraseña?</a>
                    </div>
                </div>
            </div>
            <asp:Label Text="" ID="lblInfo" runat="server" CssClass="info-message" />
            <asp:Label Text="" ID="lblError" runat="server" CssClass="error-message" />
        </form>
    </div>
        
        
    
    <script src="Scripts/Script_Login.js"></script>
</body>

    <script type="text/javascript">
    document.addEventListener("DOMContentLoaded", function () {
        var txtUsuario = document.getElementById('<%= txtUsuario.ClientID %>');
        var txtContrasena = document.getElementById('<%= txtContrasena.ClientID %>');
        var btnIniciarSesion = document.getElementById('<%= btnIniciarSesion.ClientID %>');

        txtUsuario.addEventListener("keypress", function (e) {
            if (e.key === "Enter") {
                e.preventDefault(); // Evita el envío del formulario al presionar Enter
                btnIniciarSesion.click(); // Simula el clic en el botón "Iniciar Sesión"
            }
        });

        txtContrasena.addEventListener("keypress", function (e) {
            if (e.key === "Enter") {
                e.preventDefault(); // Evita el envío del formulario al presionar Enter
                btnIniciarSesion.click(); // Simula el clic en el botón "Iniciar Sesión"
            }
        });
    });

        window.onload = function () {
            history.pushState(null, null, document.URL);
            window.addEventListener('popstate', function () {
                history.pushState(null, null, document.URL);
            });
        }

    </script>

</html>
