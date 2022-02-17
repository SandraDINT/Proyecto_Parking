using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;

namespace Proyecto_Parking.Servicios
{
    /// <summary>
    /// El servicio ServicioAzure nos permite guardar una imagen en un contenedor de Azure.
    /// Creado por Sr.J.
    /// </summary>
    class ServicioAzure
    {
        /// <summary>
        /// El metodo GuardarImagen nos permite guardar una imagen en azure.
        /// </summary>
        /// <param name="rutaImagen">Le deberemos de pasar como parametro la ruta local de la foto que queremos guardar en Azure.</param>
        /// <returns>Nos debuelve un string el cual nos indica la ruta de la imagen en el contenedor de Azure.</returns>
        public string GuardarImagen(string rutaImagen)
        {

            //Obtenemos el cliente del contenedor
            var clienteBlobService = new BlobServiceClient(Properties.Settings.Default.cadenaConexion);
            var clienteContenedor = clienteBlobService.GetBlobContainerClient(Properties.Settings.Default.nombreContenedorBlobs);

            //Leemos la imagen
            Stream streamImagen = File.OpenRead(rutaImagen);
            string nombreImagen = Path.GetFileName(rutaImagen);

            //Eliminamos si existe
            BlobServiceClient blobServiceClient = new BlobServiceClient(Properties.Settings.Default.cadenaConexion);
            BlobContainerClient cont = blobServiceClient.GetBlobContainerClient(Properties.Settings.Default.nombreContenedorBlobs);
            cont.GetBlobClient(nombreImagen).DeleteIfExists();

            //Subimos
            clienteContenedor.UploadBlob(nombreImagen, streamImagen);

            //Una vez subida, obtenemos la URL para referenciarla
            var clienteBlobImagen = clienteContenedor.GetBlobClient(nombreImagen);
            return clienteBlobImagen.Uri.AbsoluteUri;
        }
    }
}
