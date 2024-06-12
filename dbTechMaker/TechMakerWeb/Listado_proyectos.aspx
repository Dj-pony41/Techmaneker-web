<%@ Page Title="" Language="C#" MasterPageFile="~/AdmMaster.Master" AutoEventWireup="true" CodeBehind="Listado_proyectos.aspx.cs" Inherits="TechMakerWeb.Listado_proyectos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Css/Style_Listado_tabla.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <main class="table">
            <section class="table__header">
                <h1 class="titleTable" >Listado Proyectos</h1> 
            </section>
            <a href="Crud_Eventos.aspx" class="btnAprobar">Crear</a>
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
                            <th>Aprovar</th>
                            <th>Rechazar</th>
                        </tr>
                    </thead>
                    <tbody id="tableBody" runat="server">
                
                    </tbody>
                </table>
            </section>
        </main>

</asp:Content>
