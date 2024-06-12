<%@ Page Title="Mis Proyectos" Language="C#" MasterPageFile="~/DocMaster.Master" AutoEventWireup="true" CodeBehind="Listado_Proyectos_Propuestos.aspx.cs" Inherits="TechMakerWeb.Listado_Proyectos_Propuestos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Css/Style_Listado_tabla.css" />
    <link rel="stylesheet" href="Css/Style_PopUp_descripcion.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="modal_container" class="modal-container">
        <div class="modal">
            <h1>Descripcion del Proyecto</h1>
            <div class="modal-content">
                <p id="modal-contenido-texto"></p>
            </div>
            <button id="close" class="btn-class-style">Cerrar</button>
        </div>
    </div>
    <div id="modal_container_metricas" class="modal-container">
        <div class="modal">
            <h1>Observaciones</h1>
            <div class="modal-content">
                <p id="modal-contenido-texto-metricas"></p>
            </div>
            <button id="close_metricas" class="btn-class-style">Cerrar</button>
        </div>
    </div>
    <main class="table">
        <section class="table__header">
            <h1 class="titleTable" >Listado Proyectos</h1>    
             <a href="Proyecto.aspx" class="btnAprobar">Crear</a>
        </section>
        <section class="table__body">
            <table>
                <thead>
                    <tr>
                        <th>Nro.</th>
                        <th>Nombre Proyecto</th>
                        <th>Descripcion </th>
                        <th>Observaciones </th>
                        <th>Estado</th>
                        <th>Editar</th>
                        <th>Eliminar</th>
                    </tr>
                </thead>
                <tbody id="tableBody" runat="server">
        
                </tbody>
            </table>
        </section>
    </main>
    <script src="Scripts/Script_descripcion_evento.js"></script>
</asp:Content>
