using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Parking.Servicios
{
    class ServicioDialogos
    {
        public string AbrirArchivoDialogo(string filtro)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = filtro;
            openFileDialog.ShowDialog();

            return openFileDialog.FileName;
        }
    }
}
