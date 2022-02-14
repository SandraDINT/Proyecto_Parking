using Newtonsoft.Json;
using Proyecto_Parking.clase.clasesServicios;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Parking.Servicios
{
    /// <summary>
    /// El servicio ReconocimientoVehiculoService es usado para reconocer el vehiculo, atravez de una IA de azure.
    /// La IA de azure esta entrenada para reconocer Motocicletas o Coches, pero si se le entrenaa para que reconozca otro vehiculo tambien funcionaria
    /// devolviendote el nombre asignado en azure
    /// </summary>
    class ReconocimientoVehiculoService
    {
        /// <summary>
        /// El metodo reconocerVehiculo sirve para reconocer un vehiculo atraves de una imagen.
        /// </summary>
        /// <param name="url">El parametro url es el que tendra la imagen tienen que estar en un servido accesible publicamente</param>
        /// <returns>Este metodo devuelve un String indicando el tipo de vehiculo</returns>
        public string reconocerVehiculo(string url)
        {

            var cliente = new RestClient(Properties.Settings.Default.EnlaceIAReconocimientoVehiculo + "/customvision/v3.0/Prediction/" + Properties.Settings.Default.ProjectIAReconocimientoVehiculo_id + "/classify/iterations/" + Properties.Settings.Default.PublishedIAReconocimientoVehiculo_name + "/url");

            var request = new RestRequest(Method.POST);

            request.AddHeader("Prediction-Key", Properties.Settings.Default.Prediction_key);
            request.AddHeader("Content-Type", "application/json");

            request.AddJsonBody("{\u0022url\u0022:" + "\u0022" + url + "\u0022}");//Esto se ve feo pero lo que hace es un json, igual que en el otro servicio


            CustomVisionReturn customVisionReturn = JsonConvert.DeserializeObject<CustomVisionReturn>(cliente.Execute(request).Content);


            string vehiculo = "";
            double porcentaje = 0.0;
            foreach (var item in customVisionReturn.Predictions)
            {
                if (item.Probability > porcentaje)
                {
                    porcentaje = item.Probability;
                    vehiculo = item.TagName;
                }
            }

            return vehiculo;

        }

    }
}
