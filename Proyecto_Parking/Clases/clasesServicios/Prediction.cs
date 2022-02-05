using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Parking.clase.clasesServicios
{
    public class Prediction : ObservableObject
    {

        private double probability;

        public double Probability
        {
            get { return probability; }
            set { SetProperty(ref probability, value); }
        }


        private string tagId;

        public string TagId
        {
            get { return tagId; }
            set { SetProperty(ref tagId, value); }
        }


        private string tagName;

        public string TagName
        {
            get { return tagName; }
            set { SetProperty(ref tagName, value); }
        }

    }
}
