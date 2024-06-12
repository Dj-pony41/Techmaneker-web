<%@ Page Title="Listado Eventos" Language="C#" MasterPageFile="~/AdmMaster.Master" AutoEventWireup="true" CodeBehind="Listado_eventos.aspx.cs" Inherits="TechMakerWeb.Listado_eventos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Css/Style_Listado_tabla.css" />
    <link rel="stylesheet" href="Css/Style_PopUp_descripcion.css" />
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="modal_container_metricas" class="modal-container">
        <div class="modal">
            <h1>Metricas del evento</h1>
            <div class="modal-content">
                <p id="modal-contenido-texto-metricas"></p>
            </div>
            <button id="close_metricas" class="btn-class-style">Cerrar</button>
        </div>
    </div>

    <div id="modal_container" class="modal-container">
        <div class="modal">
            <h1>Descripcion del Proyecto</h1>
            <div class="modal-content">
                <p id="modal-contenido-texto"></p>
            </div>
            <button id="close" class="btn-class-style">Cerrar</button>
        </div>
    </div>

    <div id="modal_container_Qr" class="modal-container">
         <div class="modal">
             <h1>Descripcion del Proyecto</h1>
             <div class="modal-content">
                 <img src="Css/Images/QR_Code01.png" id="modal-contenido-img" class="img_qr_contendor"/>
             </div>
             <button id="close_image" class="btn-class-style">Cerrar</button>
             <button id="save_image" class="btn-class-style">Guardar</button>
         </div>
     </div>

    <main class="table">
        <section class="table__header">
            <h1 class="titleTable" >Listado Eventos</h1>    
             <a href="Crud_Eventos.aspx" class="btnAprobar">Crear</a>
        </section>
        <section class="table__body">
            <table>
                <thead>
                    <tr>
                        <th>Nro.</th>
                        <th>Nombre Evento</th>
                        <th>Descripcion </th>
                        <th>Gestion </th>
                        <th>Fecha de inicio </th>
                        <th>Fecha de fin </th>
                        <th>Metricas</th>
                        <th>Ver Qr</th>
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
