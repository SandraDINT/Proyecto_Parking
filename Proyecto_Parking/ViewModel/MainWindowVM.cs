using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Proyecto_Parking.clase;
using Proyecto_Parking.Clases;
using Proyecto_Parking.Servicios;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Proyecto_Parking.ViewModel
{

    class MainWindowVM : ObservableObject
    {
        private const int TOTAL_PLAZAS_COCHE = 50;
        private const int TOTAL_PLAZAS_MOTO = 50;

        private readonly ReconocimientoVehiculoService reconocimientoService;
        private readonly LeerMatriculaService matriculaService;
        private readonly BDService bdService;
        private readonly ServicioDialogos dialogosService;
        private readonly ServicioAzure azureService;

        private static readonly string filtrosRuta = "JPG (*.jpg)|*.jpg|PNG (*.png*)|*.png*";
        public RelayCommand AbrirExaminarCommand { get; }
        public RelayCommand AbrirAceptarCommand { get; }

        private ObservableCollection<Estacionamiento> _listaEstacionamientos;
        public ObservableCollection<Estacionamiento> ListaEstacionamientos
        {
            get { return _listaEstacionamientos; }
            set { SetProperty(ref _listaEstacionamientos, value); }
        }

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

        private int _idVehiculo;
        public int IdVehiculo
        {
            get { return _idVehiculo; }
            set { SetProperty(ref _idVehiculo, value); }
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

        string rutaAzure;
        public MainWindowVM()
        {
            //Servicios
            azureService = new ServicioAzure();
            dialogosService = new ServicioDialogos();
            bdService = new BDService();
            matriculaService = new LeerMatriculaService();
            reconocimientoService = new ReconocimientoVehiculoService();

            //Variables
            Foto = "Assets/no_image_car.png";
            _plazasLibresCoche = SacarPlazasLibresCoche();
            _plazasLibresMoto = SacarPlazasLibresMoto();
            _listaEstacionamientos = new ObservableCollection<Estacionamiento>();
            EstacionamientoActual = new Estacionamiento();

            //Comandos
            AbrirExaminarCommand = new RelayCommand(AbrirExaminar);
            AbrirAceptarCommand = new RelayCommand(EntrarAlParking);
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
            try
            {
                string rutaImagen = dialogosService.AbrirArchivoDialogo(filtrosRuta);
                rutaAzure = azureService.GuardarImagen(rutaImagen);
                Foto = rutaAzure;
            }//System.AggregateException System.ArgumentOutOfRangeException
            catch (AggregateException)
            {
                dialogosService.MensajeError("ERROR", "Error al guardar la imagen");
            }
            catch (ArgumentException) { Console.WriteLine("Ha cerrado dialogo"); }
            catch (Azure.RequestFailedException)
            {
                dialogosService.MensajeError("ERROR", "Error con la imagen");
            }
            catch (Exception)
            {
                dialogosService.MensajeError("ERROR", "Error desconocido");
            }
        }

        private void EntrarAlParking()
        {
            try
            {
                Estacionamiento estacionamiento = new Estacionamiento();
                string tipo = reconocimientoService.ReconocerVehiculo(rutaAzure);
                string matricula = matriculaService.LeerMatricula(rutaAzure, tipo);
                if (InsertaVehiculo(matricula, tipo, estacionamiento))
                {
                    if (tipo == "Coche")
                    {
                        PlazasLibresCoche--;
                    }
                    else
                    {
                        PlazasLibresMoto--;
                    }
                }
                //cuando el vehículo entra en el parking, desaparece su imagen y se pone por defecto no_image_car
                Foto = "Assets/no_image_car.png";
            }
            catch (Exception)
            {
                dialogosService.MensajeError("ERROR", "Error al identificar vehículo");
            }
        }

        private bool InsertaVehiculo(string matricula, string tipo, Estacionamiento estacionamiento)
        {
            bool inserta = false;
            if (!bdService.BuscaEstacionamientoPorMatricula(matricula) &&
                SacarPlazasLibresCoche() > 0)
            {
                estacionamiento.Matricula = matricula;
                estacionamiento.Tipo = tipo;
                estacionamiento.Entrada = "";
                if (!bdService.BuscaVehiculosPorMatricula(matricula))
                {
                    estacionamiento.IdVehiculo = -1;
                }
                else
                {
                    IdVehiculo = bdService.BuscaIDVehiculoPorMatricula(matricula);
                    estacionamiento.IdVehiculo = IdVehiculo;
                }
                bdService.InsertaEstacionamiento(estacionamiento);
                inserta = true;
            }
            else
                dialogosService.MensajeError("ERROR", "Estacionamiento ya activo, imposible crear");
            return inserta;
        }
    }
}
