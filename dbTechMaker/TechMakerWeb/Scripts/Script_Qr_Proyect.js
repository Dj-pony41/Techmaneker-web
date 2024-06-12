document.addEventListener('DOMContentLoaded', () => {
    // Variables para el tercer modal (Imagen)
    const modal_container_Qr = document.getElementById('modal_container_Qr');
    const Qr_img = document.getElementById('modal-contenido-img');
    const close_Qr = document.getElementById('close_image');
    const save_image = document.getElementById('save_image');
    const botonesVerQr = document.querySelectorAll('.btn-imagen');


    // Función para abrir el modal de imagen
    botonesVerQr.forEach(boton => {
        boton.addEventListener('click', function () {
            const img = this.nextElementSibling;  // Asumiendo que la imagen es el siguiente elemento después del botón
            const imgSrc = img.getAttribute('src');
            const imgId = img.getAttribute('id');  // Obtener el id de la imagen

            Qr_img.setAttribute('src', imgSrc);
            Qr_img.setAttribute('data-id', imgId);  // Guardar el id de la imagen en un atributo data-id en el modal

            modal_container_Qr.classList.add('show');
        });
    });

    // Función para cerrar el modal de imagen
    close_Qr.addEventListener('click', () => {
        modal_container_Qr.classList.remove('show');
    });

    // Función para guardar la imagen
    save_image.addEventListener('click', () => {
        const imgSrc = Qr_img.getAttribute('src');
        const imgId = Qr_img.getAttribute('data-id');  // Obtener el id de la imagen desde el atributo data-id

        // Fetch the image and create a Blob
        fetch(imgSrc)
            .then(response => response.blob())
            .then(blob => {
                const url = URL.createObjectURL(blob);
                const link = document.createElement('a');
                link.href = url;
                link.download = imgId + '.png';  // Usar el id de la imagen como nombre del archivo

                // Forzar el cuadro de diálogo de descarga
                link.click();

                // Limpiar el enlace y el objeto URL
                URL.revokeObjectURL(url);
            })
            .catch(error => {
                console.error('Error al descargar la imagen:', error);
            });
    });
});
