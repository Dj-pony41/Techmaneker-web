<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Observation.aspx.cs" Inherits="TechMakerWeb.Observation" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Listado de Scores</title>
    <link rel="stylesheet" href="Css/Style_Login.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Observaciones</h2>
            <asp:TextBox ID="txtObservation" runat="server" TextMode="MultiLine" Rows="5" Columns="50"></asp:TextBox>
            <br />
            <asp:Button ID="btnSubmit" runat="server" Text="Enviar" OnClick="btnSubmit_Click" />
            <asp:Button ID="btnBack" runat="server" Text="Volver" OnClick="btnBack_Click" />
        </div>
    </form>
</body>
</html>


