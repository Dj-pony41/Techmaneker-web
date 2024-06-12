<%@ Page Language="C#" MasterPageFile="~/DocMaster.Master" AutoEventWireup="true" CodeBehind="NotificacionProyecto.aspx.cs" Inherits="TechMakerWeb.WebForm6"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Aquí se pueden agregar etiquetas meta, enlaces a hojas de estilo, scripts, etc. -->
      <link rel="stylesheet" href="Css/StylesNotification.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Contenido específico de la página NotificacionProyecto.aspx -->
    <main class="notifications">
        <h1 class="titleTable">Notificaciones</h1>
        <ul id="notificationList" runat="server" class="notification-list">
            <%-- Aquí se generarán dinámicamente las notificaciones --%>
        </ul>
    </main>
</asp:Content>
