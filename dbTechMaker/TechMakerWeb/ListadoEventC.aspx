<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListadoEventC.aspx.cs" Inherits="TechMakerWeb.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Eventos</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f1f1f1;
            margin: 0;
            padding: 0;
        }

        h1 {
            color: #003366;
            margin-bottom: 20px;
        }

        .table {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 20px;
        }

        .table th,
        .table td {
            border: 1px solid #ddd;
            padding: 8px;
            text-align: center;
        }

        .table th {
            background-color: #003366;
            color: #ffffff;
        }

        .table tr:nth-child(even) {
            background-color: #f2f2f2;
        }

        .table-striped tbody tr:nth-child(odd) {
            background-color: #f2f2f2;
        }

        .table-hover tbody tr:hover {
            background-color: #cceeff;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Eventos</h1>
            <asp:GridView ID="gridData" runat="server" CssClass="table table-bordered table-striped table-hover"></asp:GridView>
        </div>
    </form>
</body>
</html>
