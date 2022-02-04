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
    class LeerMatriculaService
    {
        public string LeerMatricula(string url, string tipoVehiculo)
        {

            //////////////////////////PRIMERA PARTE DEL SERVICIO ENVIA FOTO DEL VEHICULO

            var cliente = new RestClient(Properties.Settings.Default.EndpointCoputerVision + "/vision/v3.2/read/analyze/");

            var request = new RestRequest(Method.POST);

            request.AddHeader("Ocp-Apim-Subscription-Key", Properties.Settings.Default.SubscriptionKey);
            request.AddHeader("Content-Type", "application/json");

            request.AddJsonBody("{\u0022url\u0022:" + "\u0022" + url + "\u0022}");//Esto se ve feo pero lo que hace es un json            

            string resultadoOCR = cliente.Execute(request).Headers[0].ToString().Split('=')[1];
            Thread.Sleep(1500);



            //////////////////////////SEGUNDA PARTE DEL SERVICIO SOLICITAR RESULTADOS

            var clienteGET = new RestClient(resultadoOCR);

            var requestGET = new RestRequest(Method.GET);

            requestGET.AddHeader("Ocp-Apim-Subscription-Key", Properties.Settings.Default.SubscriptionKey);

            JToken jt = JToken.Parse(clienteGET.Execute(requestGET).Content).SelectToken("analyzeResult").SelectToken("readResults").First.SelectToken("lines").First.SelectToken("text");

            string matricul = jt.ToString();
            if (tipoVehiculo.Equals("Motocicleta"))
            {
                jt = JToken.Parse(clienteGET.Execute(requestGET).Content).SelectToken("analyzeResult").SelectToken("readResults").First.SelectToken("lines")[1].SelectToken("text");
                matricul += jt.ToString();
            }
            return matricul;
        }
    }
}
