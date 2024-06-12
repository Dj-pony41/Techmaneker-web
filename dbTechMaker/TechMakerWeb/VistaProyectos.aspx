<%@ Page Language="C#" MasterPageFile="~/DirMaster.Master" AutoEventWireup="true" CodeBehind="VistaProyectos.aspx.cs" Inherits="TechMakerWeb.WebForm9"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Css/Styles_Crud2.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <div class="form-group">
            <asp:Label ID="lblEvento" runat="server" AssociatedControlID="Evento" Text="Evento:"></asp:Label>
            <asp:DropDownList ID="Evento" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Evento_SelectedIndexChanged"></asp:DropDownList>
        </div>

        <main class="table">
            <section class="table__header">
                <h1 class="titleTable">Listado Proyectos</h1>
            </section>
            <section class="table__body">
                <table>
                    <thead>
                        <tr>
                            <th>Nro</th>
                            <th>Nombre</th>
                            <th>Descripcion</th>
                            <th>Estado</th>
                            <th>Creador</th>
                            <th>Fecha de Registro</th>
                            <th>Revisado por</th>
                            
                        </tr>
                    </thead>
                    <tbody id="tableBody" runat="server">
                    </tbody>
                </table>
            </section>
        </main>
    </form>

   
</asp:Content>
