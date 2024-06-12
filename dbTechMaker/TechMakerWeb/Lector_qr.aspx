<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Lector_qr.aspx.cs" Inherits="TechMakerWeb.Lector_qr" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" href="Css/Styles_qr_reader.css" />
    <script src="assets/plugins/qrCode.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <title></title>
</head>
<body>
    <div class="Container_camar">
        <h1 id="title_qr" runat="server">Lector qr</h1>
        <br/>
        <div id="scan_container" class="scan hidden">
        
            <div class="qrcode">
                <a id="btn-scan-qr" class="camara-container" href="#">
                    
                <a/>
            </div>
            
            <div class="border"></div>
        </div>
        <div class="camara">
            <canvas hidden="" id="qr-canvas" class="img-fluid"></canvas>
        </div>
        <div class="Container_botones">
            <button class="btn" onclick="encenderCamara()">Encender camara</button>
            <button class="btn apagar" onclick="cerrarCamara()">Detener camara</button>
        </div>
        
    </div>
    
    <audio id="audioScaner" src="assets/sonido.mp3"></audio>
    <script src="assets/js/index.js"></script>
</body>
</html>
