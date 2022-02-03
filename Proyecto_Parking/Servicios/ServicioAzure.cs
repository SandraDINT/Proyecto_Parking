using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;

namespace Proyecto_Parking.Servicios
{
    class ServicioAzure
    {
        public string GuardarImagen(string rutaImagen)
        {

            //Obtenemos el cliente del contenedor
            var clienteBlobService = new BlobServiceClient(Properties.Settings.Default.cadenaConexion);
            var clienteContenedor = clienteBlobService.GetBlobContainerClient(Properties.Settings.Default.nombreContenedorBlobs);

            //Leemos la imagen y la subimos al contenedor
            Stream streamImagen = File.OpenRead(rutaImagen);
            string nombreImagen = Path.GetFileName(rutaImagen);
            clienteContenedor.UploadBlob(nombreImagen, streamImagen);

            //Una vez subida, obtenemos la URL para referenciarla
            var clienteBlobImagen = clienteContenedor.GetBlobClient(nombreImagen);
            return clienteBlobImagen.Uri.AbsoluteUri;
        }
    }
}
