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
        private const int TOTAL_PLAZAS_COCHE = 50;
        private const int TOTAL_PLAZAS_MOTO = 50;

        private BDService bdService;
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

        private int _plazasLibreCoche;
        public int PlazasLibreCoche
        {
            get { return _plazasLibreCoche; }
            set { SetProperty(ref _plazasLibreCoche, value); }
        }

        private int _totalPlazasMoto;
        public int TotalPlazasMoto
        {
            get { return _totalPlazasMoto; }
            set { SetProperty(ref _totalPlazasMoto, value); }
        }

        public MainWindowVM()
        {
            //Servicios
            azureService = new ServicioAzure();
            dialogosService = new ServicioDialogos();
            bdService = new BDService();

            //Variables
            Foto = "Assets/no_image_car.png";
           // _plazasLibreCoche = bdService.CuentaEstacionamientosNoFinalizadosCoche();

            //Comandos
            AbrirExaminarCommand = new RelayCommand(AbrirExaminar);
        }

        private string _foto;
        public string Foto
        {
            get { return _foto; }
            set { SetProperty(ref _foto, value); }
        }

        private void AbrirExaminar()
        {
            string rutaImagen = dialogosService.AbrirArchivoDialogo(filtrosRuta);
            string rutaAzure = azureService.GuardarImagen(rutaImagen);
            Foto = rutaAzure;
        }
    }
}
