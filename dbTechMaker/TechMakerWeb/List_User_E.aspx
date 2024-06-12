<%@ Page Title="" Language="C#" MasterPageFile="~/DirMaster.Master" AutoEventWireup="true" CodeBehind="List_User_E.aspx.cs" Inherits="TechMakerWeb.List_User_E" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link rel="stylesheet" href="Css/Style_Listado_tabla.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <main class="table">
         <section class="table__header">
             <h1 class="titleTable" >Listado Proyectos</h1>                
         </section>
         <section class="table__body">
             <table>
                 <thead>
                     <tr>
                         <th>Nro.</th>
                         <th>Usuario</th>
                         <th>Nombre</th>
                         <th>Apellido </th>
                         <th>Mail </th>
                         <th>Estado </th>
                         <th>Evaluador</th>
                     </tr>
                 </thead>
                 <tbody id="tableBody" runat="server">
         
                 </tbody>
             </table>
         </section>
     </main>
</asp:Content>
