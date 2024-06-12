<%@ Page Title="Listado Proyectos" Language="C#"  MasterPageFile="~/DirMaster.Master" AutoEventWireup="true" CodeBehind="Listado_proyecto.aspx.cs" Inherits="TechMakerWeb.Listado_proyectos" Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <link rel="stylesheet" href="Css/Style_Listado_tabla.css" />
      <link rel="stylesheet" href="Css/Style_PopUp_descripcion.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                <h1 class="titleTable" >Listado Proyectos</h1>                
            </section>
            <section class="table__body">
                <table>
                    <thead>
                        <tr>
                            <th>Nro.</th>
                            <th>Nombre Proyecto <%--<span class="icon-filer-column"><img src="Css/Images/Vector.png"/></span>--%></th>
                            <th>Carrera </th>
                            <th>Descripcion </th>
                            <th>Evento </th>
                            <th>Fecha de registro </th>
                            <th>Estado </th>
                            <th>Ver Qr</th>
                            <th>Aprobar</th>
                            <th>Rechazar</th>
                        </tr>
                    </thead>
                    <tbody id="tableBody" runat="server">
                
                    </tbody>
                </table>
            </section>
        </main>
    <script src="Scripts/Script_Qr_Proyect.js"></script>
</asp:Content>
