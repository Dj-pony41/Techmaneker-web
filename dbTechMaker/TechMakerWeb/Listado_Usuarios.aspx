<%@ Page Title="" Language="C#" MasterPageFile="~/AdmMaster.Master" AutoEventWireup="true" CodeBehind="Listado_Usuarios.aspx.cs" Inherits="TechMakerWeb.Listado_Usuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Css/Style_Listado_tabla.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main class="table">
        <section class="table__header">
            <h1 class="titleTable" >Listado Usuarios</h1>    
             <a href="Usuario.aspx" class="btnAprobar">Crear</a>
        </section>
        <section class="table__body">
            <table>
                <thead>
                    <tr>
                        <th>Nro.</th>
                        <th>Nombre Completo</th>
                        
                        <th>Editar</th>
                        <th>Eliminar</th>
                    </tr>
                </thead>
                <tbody id="tableBody" runat="server">
        
                </tbody>
            </table>
        </section>
    </main>
</asp:Content>
