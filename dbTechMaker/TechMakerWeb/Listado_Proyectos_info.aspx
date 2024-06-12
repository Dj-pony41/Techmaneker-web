<%@ Page Title="Listado Proyectos" Language="C#" MasterPageFile="~/DirMaster.Master" AutoEventWireup="true" CodeBehind="Listado_Proyectos_info.aspx.cs" Inherits="TechMakerWeb.Listado_Proyectos_info" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Css/Style_Listado_tabla.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main class="table">
         <section class="table__header">
      <a href="Listado_eventosDirector.aspx" class="btnAprobar">Volver</a>
 </section>
            <section class="table__header">
                <h1 class="titleTable" >Listado Proyectos</h1>                
            </section>
            <section class="table__body">
                <table>
                    <thead>
                        
                    </thead>
                    <tbody id="tableBody" runat="server">
                
                    </tbody>
                </table>
            </section>
        </main>
</asp:Content>
