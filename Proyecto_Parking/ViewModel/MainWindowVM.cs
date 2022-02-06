using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Proyecto_Parking.clase;
using Proyecto_Parking.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Parking.ViewModel
{
    class MainWindowVM : ObservableObject
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

        private int _totalPlazasCoche;
        public int TotalPlazasCoche
        {
            get { return _totalPlazasCoche; }
            set { SetProperty(ref _totalPlazasCoche, value); }
        }
        public MainWindowVM()
        {
            _totalPlazasCoche = Properties.Settings.Default.numPlazasCoche;

            //Servicios
            azureService = new ServicioAzure();
            dialogosService = new ServicioDialogos();

           // EstacionamientoActual = new Estacionamiento();

            //Comandos
            AbrirExaminarCommand = new RelayCommand(AbrirExaminar);
        }

        private void AbrirExaminar()
        {
            string rutaImagen = dialogosService.AbrirArchivoDialogo(filtrosRuta);
            string rutaAzure = azureService.GuardarImagen(rutaImagen);
           // EstacionamientoActual.Foto = rutaAzure;
        }
    }
}
