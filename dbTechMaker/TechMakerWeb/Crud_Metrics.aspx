<%@ Page Title="" Language="C#" MasterPageFile="~/AdmMaster.Master" AutoEventWireup="true" CodeBehind="Crud_Metrics.aspx.cs" Inherits="TechMakerWeb.Crud_Metrics" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com">
    <link href="https://fonts.googleapis.com/css2?family=Prompt:wght@400;600&display=swap" rel="stylesheet">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css">
    <link rel="stylesheet" href="Css/Styles_Metrics.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="formEvento" runat="server">   
        <div class="container">
            <div class="perfil">
                <h1>Preguntas para la calificación</h1>
                <span>Escribe qué se tomará en cuenta para la evaluación</span>
            </div>

            <div class="agregar-tarea">
                <asp:TextBox ID="txt_Pregunta" CssClass="form-control" placeholder="Agregar una pregunta" runat="server" />
                <asp:Button ID="btnAgregar" runat="server" OnClick="btnAgregarPregunta_Click" CssClass="btn-submit" Text="+" />
            </div>

            <!-- Nuevo botón -->
            <asp:Button ID="btnConcatenar" runat="server" OnClick="btnConcatenarPreguntas_Click" CssClass="btn-submit-enviar" Text="Enviar" />

            <div class="seccion-tarea">
                <h3>Estas son las preguntas para los jurados</h3>
                <ul id="lista">
                    <asp:PlaceHolder ID="phPreguntas" runat="server"></asp:PlaceHolder>
                </ul>
            </div>
        </div>
    </form>
</asp:Content>
