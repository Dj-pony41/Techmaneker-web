<%@ Page Title="" Language="C#" MasterPageFile="~/EvaMaster.Master" AutoEventWireup="true" CodeBehind="Listado_evaluador_home.aspx.cs" Inherits="TechMakerWeb.Listado_evaluador_home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Css/Styles_Home_evaluator.css" />
    <script type="text/javascript">
        document.addEventListener("DOMContentLoaded", function() {
            var addTaskButton = document.getElementById("addTaskButton");
            addTaskButton.addEventListener("click", function() {
                window.location.href = "Lector_qr.aspx?type=P";
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="wrapper">
        <div class="screen-backdrop"></div>
        <div class="home-screen screen">
            <div class="head-wrapper">
                <div class="welcome">
                    <div class="content">
                        <h1 id="Evaluador" runat="server">Hola Evaluador</h1>
                        <p>Aun te quedan <span id="totalProyectos" runat="server">5</span> Proyectos por calificar</p>
                    </div>
                    <div class="img">
                        <div class="backdrop"></div>
                        <img src="Css/Images/images.png" alt="" />
                    </div>
                </div>
            </div>
            <div class="categories-wrapper">
                <div id="categorias" runat="server" class="categories"></div>
            </div>
            <div id="addTaskButton" class="add-task-btn">
                <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="w-6 h-6">
                    <path stroke-linecap="round" stroke-linejoin="round" d="M3 3h5v2H3zM16 3h5v2h-5zM3 19h5v2H3zM16 19h5v2h-5zM3 3v5h2V3zM19 3v5h2V3zM3 16v5h2v-5zM19 16v5h2v-5z"/>
                </svg>
            </div>
        </div>
    </div>
</asp:Content>

