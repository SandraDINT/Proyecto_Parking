using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Parking.clase
{
    class Estacionamiento : ObservableObject
    {
        private int idEstacionamiento;

        public int IdEstacionamiento
        {
            get { return idEstacionamiento; }
            set { SetProperty(ref idEstacionamiento, value); }
        }
        private int idVehiculo;

        public int IdVehiculo
        {
            get { return idVehiculo; }
            set { SetProperty(ref idVehiculo, value); }
        }
        private string matricula;

        public string Matricula
        {
            get { return matricula; }
            set { SetProperty(ref matricula, value); }
        }
        private string entrada;

        public string Entrada
        {
            get { return entrada; }
            set { SetProperty(ref entrada, value); }
        }
        private string salida;

        public string Salida
        {
            get { return salida; }
            set { SetProperty(ref salida, value); }
        }

        //campo calculado ??
        private double importe;

        public double Importe
        {
            get { return importe; }
            set { SetProperty(ref importe, value); }
        }
        private string tipo;

        public string Tipo
        {
            get { return tipo; }
            set { SetProperty(ref tipo, value); }
        }
        public Estacionamiento()
        {

        }

        public Estacionamiento(int idEstacionamiento,
            int idVehiculo, string matricula,
            string entrada, string salida,
            double importe, string tipo)
        {
            IdEstacionamiento = idEstacionamiento;
            IdVehiculo = idVehiculo;
            Matricula = matricula;
            Entrada = entrada;
            //esto soluciona-> Unable to cast object of type 'System.DBNull' to type 'System.String'
            if (salida == null)
                Salida = String.Empty;
            else
                Salida = salida;
            Importe = importe;
            Tipo = tipo;
        }

        public Estacionamiento(int idVehiculo,
            string matricula, string entrada,
            string salida, double importe, string tipo)
        {
            IdVehiculo = idVehiculo;
            Matricula = matricula;
            Entrada = entrada;
            if (salida == null)
                Salida = String.Empty;
            else
                Salida = salida;
            Importe = importe;
            Tipo = tipo;
        }
    }
}
