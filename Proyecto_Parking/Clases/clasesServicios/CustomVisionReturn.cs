using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Parking.clase.clasesServicios
{
    class CustomVisionReturn : ObservableObject
    {


        private string id;

        public string Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }


        private string project;

        public string Project
        {
            get { return project; }
            set { SetProperty(ref project, value); }
        }



        private string iteration;

        public string Iteration
        {
            get { return iteration; }
            set { SetProperty(ref iteration, value); }
        }


        private DateTime created;

        public DateTime Created
        {
            get { return created; }
            set { SetProperty(ref created, value); }
        }


        private List<Prediction> predictions;

        public List<Prediction> Predictions
        {
            get { return predictions; }
            set { SetProperty(ref predictions, value); }
        }

    }
}
