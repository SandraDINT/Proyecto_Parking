using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Proyecto_Parking.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Parking
{
    class Estacionamiento :ObservableObject
    {
        private string _foto;
        public string Foto
        {
            get { return _foto; }
            set { SetProperty(ref _foto, value); }
        }
        private string _matricula;
        public string Matricula
        {
            get { return _matricula; }
            set { SetProperty(ref _matricula, value); }
        }

        private int _plazas;
        public int Plazas
        {
            get { return _plazas; }
            set { SetProperty(ref _plazas, value); }
        }

        private string _tipoVehiculo;
        public string TipoVehiculo
        {
            get { return _tipoVehiculo; }
            set { SetProperty(ref _tipoVehiculo, value); }
        }

        public Estacionamiento()
        {
            _plazas = Properties.Settings.Default.numPlazas;
            
        }
    }
}
