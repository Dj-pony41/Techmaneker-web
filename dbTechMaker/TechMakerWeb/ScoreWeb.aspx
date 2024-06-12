<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScoreWeb.aspx.cs" Inherits="TechMakerWeb.Score2" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Listado de Scores</title>
    <link rel="stylesheet" href="Css/Style_Listado_tabla.css" />
</head>
<body>
    <form id="form1" runat="server">
        <main class="table">
            <section class="table__header">
                <h1 class="titleTable">EVALUACION PROYECTOS</h1>
                <asp:Button ID="btnRegistrar" Text="Registrar" OnClick="btnRegistrar_Click" runat="server" class="btnAprobar"/>
            </section>
            <section class="table__body">
                <table>
                    <thead>
                        <tr>
                            <th>Nro.</th>
                            <th>Descripción</th>
                            <th>Calificación</th>
                        </tr>
                    </thead>
                    <tbody id="tableBody" runat="server">
                        <!-- Aquí se agregarán las filas de datos dinámicamente desde el código C# -->
                    </tbody>
                </table>
            </section>
        </main>
        <footer>
            <p>
                <asp:Label ID="resultadoLabel" runat="server" CssClass="resultadoLabel" Text="Sumatoria:"></asp:Label>
            </p>
            <p style="display:none">Sumatoria: 
                <asp:TextBox ID="resultadoTextBox" runat="server" CssClass="resultadoTextBox" Enabled="true"></asp:TextBox>
            </p>
        </footer>
        
        <asp:HiddenField ID="resultadoHidden" runat="server" />
    </form>

    <script type="text/javascript">
        function recalcularResultado() {
            var sum = 0;
            var inputs = document.querySelectorAll('input[type="number"]');
            var totalQuestions = inputs.length;

            inputs.forEach(function (input) {
                var valor = parseInt(input.value);
                if (!isNaN(valor)) {
                    sum += valor;
                }
            });

            // Calcular el nuevo resultado basado en la lógica proporcionada
            var valorPorPregunta = 100 / totalQuestions;
            var nuevoResultado = sum * (valorPorPregunta / 100);

            // Actualizar el elemento que muestra la sumatoria (TextBox)
            var resultadoElement = document.getElementById('<%= resultadoTextBox.ClientID %>');
            resultadoElement.value = nuevoResultado.toFixed(0);

            // Actualizar el elemento que muestra la sumatoria (Label)
            var resultadoLabel = document.getElementById('<%= resultadoLabel.ClientID %>');
            resultadoLabel.innerText = "Sumatoria: " + nuevoResultado.toFixed(0);
        }

        recalcularResultado();

        function validarCalificacion(input) {
            var valor = parseInt(input.value);
            if (isNaN(valor) || valor < 0) {
                input.value = "0";
            } else if (valor > 100) {
                input.value = "100";
            }
            recalcularResultado();
        }

        function validarEntrada(event) {
            var codigo = event.keyCode;
            if ((codigo >= 48 && codigo <= 57) || (codigo >= 96 && codigo <= 105) || codigo == 8 || codigo == 37 || codigo == 39 || codigo == 46) {
                return true;
            } else {
                return false;
            }
        }

        function guardarCalificaciones() {
            recalcularResultado();
            var resultadoElement = document.getElementById('<%= resultadoTextBox.ClientID %>');
            var resultadoHidden = document.getElementById('<%= resultadoHidden.ClientID %>');
            resultadoHidden.value = resultadoElement.value;
        }

        document.getElementById('<%= btnRegistrar.ClientID %>').addEventListener('click', guardarCalificaciones);
    </script>
</body>
</html>
