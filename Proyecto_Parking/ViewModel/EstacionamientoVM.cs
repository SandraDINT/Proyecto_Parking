using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Proyecto_Parking.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Parking.ViewModel
{
    class EstacionamientoVM : ObservableObject
    {
        private ServicioDialogos dialogosService;
        private ServicioAzure azureService;

        private static string filtrosRuta = "JPG (*.jpg)|*.jpg|PNG (*.png*)|*.png*";
        public RelayCommand AbrirExaminarCommand { get; }

        private Estacionamiento _estacionamientoActual;
        public Estacionamiento EstacionamientoActual
        {
            get { return _estacionamientoActual; }
            set { SetProperty(ref _estacionamientoActual, value); }
        }

        public EstacionamientoVM()
        {
            //Servicios
            azureService = new ServicioAzure();
            dialogosService = new ServicioDialogos();

            EstacionamientoActual = new Estacionamiento();

            //Comandos
            AbrirExaminarCommand = new RelayCommand(AbrirExaminar);
        }

        private void AbrirExaminar()
        {
            string rutaImagen = dialogosService.AbrirArchivoDialogo(filtrosRuta);
            string rutaAzure = azureService.GuardarImagen(rutaImagen);
            EstacionamientoActual.Foto = rutaAzure;
        }
    }
}
