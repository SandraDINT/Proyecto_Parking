using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proyecto_Parking.Servicios
{
    /// <summary>
    /// La clase LeerMatriculaService nos ayudara con el reconocimiento del vehiculo
    /// con una IA para leer texto
    /// </summary>
    class LeerMatriculaService
    {

        /// <summary>
        /// El metodo LeerMatricula nos ayudara a reconocer las letras de la matricula del vehiculo
        /// </summary>
        /// <param name="url">Este parametro nos sirve para decirle la imagen del vehiculo que necesitemos</param>
        /// <param name="tipoVehiculo">Este parametro es muy importante ya que le tendremos que indicar si una Motocicleta y que asi lea las dos lineas de la matricula, si no es una Motocicleta leera la primera linea de la matricula</param>
        /// <returns>Nos retorna una cadena la cual contiene la matricula reconocida</returns>
        public string LeerMatricula(string url, string tipoVehiculo)
        {

            //Envio la foto del vehiculo con un POST

            var cliente = new RestClient(Properties.Settings.Default.EndpointCoputerVision + "/vision/v3.2/read/analyze/");

            var request = new RestRequest(Method.POST);

            request.AddHeader("Ocp-Apim-Subscription-Key", Properties.Settings.Default.SubscriptionKey);
            request.AddHeader("Content-Type", "application/json");

            request.AddJsonBody("{\u0022url\u0022:" + "\u0022" + url + "\u0022}");//Esto se ve feo pero lo que hace es un json            

            string resultadoOCR = cliente.Execute(request).Headers[0].ToString().Split('=')[1];
            Thread.Sleep(1500);


            //Solicitamos los resultados con un GET

            var clienteGET = new RestClient(resultadoOCR);

            var requestGET = new RestRequest(Method.GET);

            requestGET.AddHeader("Ocp-Apim-Subscription-Key", Properties.Settings.Default.SubscriptionKey);

            JToken jt = JToken.Parse(clienteGET.Execute(requestGET).Content).SelectToken("analyzeResult").SelectToken("readResults").First.SelectToken("lines").First.SelectToken("text");

            string matricula = jt.ToString();
            if (tipoVehiculo.Equals("Motocicleta"))
            {
                jt = JToken.Parse(clienteGET.Execute(requestGET).Content).SelectToken("analyzeResult").SelectToken("readResults").First.SelectToken("lines")[1].SelectToken("text");
                matricula += jt.ToString();
            }
            return matricula;
        }
    }
}
