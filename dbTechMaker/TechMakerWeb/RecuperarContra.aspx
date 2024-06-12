<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecuperarContra.aspx.cs" Inherits="TechMakerWeb.RecuperarContra" %>

<!DOCTYPE html>
<link rel="stylesheet" href="Css/Style_Formularios.css" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="formRecuperar" runat="server">
        <h2>Recuperar Contraseña</h2>
        <div>
            <label for="txtMail">Ingrese su Email</label>
            <asp:TextBox ID="txtMail" runat="server"></asp:TextBox>
        </div>
        <div>
            <label for="txtUserName">Ingrese su Username</label>
            <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Button ID="btnMandar" runat="server" Text="Mandar cambio de contraseña" OnClick="btnMandar_OnClick" />
            <asp:Label Text="" ID="lblInfo" runat="server" />
        </div>
    </form>
</body>
</html>
