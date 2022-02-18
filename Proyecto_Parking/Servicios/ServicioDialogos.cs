using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Proyecto_Parking.Servicios
{
    /// <summary>
    /// El servicio ServicioDiaologos nos permite usar los dialogos mas necesitados en nuestro programación.
    /// </summary>
    class ServicioDialogos
    {
        /// <summary>
        /// El metodo AbrirArchivoDialogo nos ayudara a saber que archivo elige el usuario.
        /// </summary>
        /// <param name="filtro">Este parametro lo utilizamos para indicarle que tipos de archivos puede elegir el usuario.</param>
        /// <returns>Nos retorna la ruta del archivo elegido por el usuario.</returns>
        public string AbrirArchivoDialogo(string filtro)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = filtro
            };
            openFileDialog.ShowDialog();

            return openFileDialog.FileName;
        }
        /// <summary>
        /// El metodo MensajeError nos ayudara a indicarle al usuario de una forma simple un error
        /// </summary>
        /// <param name="tituloMessageBox">Este parametro es donde le indicamos que titulo tendra la ventana emergente</param>
        /// <param name="mensajeError">Este parametro es donde le indicaremos que error es el que sucede</param>
        public void MensajeError(string tituloMessageBox, string mensajeError)
        {
            MessageBox.Show(mensajeError, tituloMessageBox, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
