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

        private readonly BDService bdService;
        private readonly ServicioDialogos dialogosService;
        private readonly ServicioAzure azureService;

        private static string filtrosRuta = "JPG (*.jpg)|*.jpg|PNG (*.png*)|*.png*";
        public RelayCommand AbrirExaminarCommand { get; }

        private Estacionamiento _estacionamientoActual;
        public Estacionamiento EstacionamientoActual
        {
            get { return _estacionamientoActual; }
            set { SetProperty(ref _estacionamientoActual, value); }
        }

        private int _plazasOcupadasCoche;
        public int PlazasOcupadasCoche
        {
            get { return _plazasOcupadasCoche; }
            set { SetProperty(ref _plazasOcupadasCoche, value); }
        }

        private int _plazasOcupadasMoto;
        public int PlazasOcupadasMoto
        {
            get { return _plazasOcupadasMoto; }
            set { SetProperty(ref _plazasOcupadasMoto, value); }
        }

        private int _plazasLibresCoche;
        public int PlazasLibresCoche
        {
            get { return _plazasLibresCoche; }
            set { SetProperty(ref _plazasLibresCoche, value); }
        }

        private int _plazasLibresMoto;
        public int PlazasLibresMoto
        {
            get { return _plazasLibresMoto; }
            set { SetProperty(ref _plazasLibresMoto, value); }
        }


        public MainWindowVM()
        {
            //Servicios
            azureService = new ServicioAzure();
            dialogosService = new ServicioDialogos();
            bdService = new BDService();

            //Variables
            Foto = "Assets/no_image_car.png";
            _plazasLibresCoche = SacarPlazasLibresCoche();
            _plazasLibresMoto = SacarPlazasLibresMoto();

            //Comandos
            AbrirExaminarCommand = new RelayCommand(AbrirExaminar);
        }
        private int SacarPlazasLibresCoche()
        {
            int plazasLibres = 0;
            _plazasOcupadasCoche = bdService.CuentaEstacionamientosNoFinalizadosCoche();
            plazasLibres = _plazasLibresCoche = TOTAL_PLAZAS_COCHE - _plazasOcupadasCoche;
            return plazasLibres;
        }
        private int SacarPlazasLibresMoto()
        {
            int plazasLibres = 0;
            _plazasOcupadasMoto = bdService.CuentaEstacionamientosNoFinalizadosMoto();

            plazasLibres = _plazasLibresMoto = TOTAL_PLAZAS_MOTO - _plazasOcupadasMoto;
            return plazasLibres;
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
            Estacionamiento estacionamiento = new Estacionamiento();
            bdService.InsertaEstacionamiento(estacionamiento);
        }
    }
}
