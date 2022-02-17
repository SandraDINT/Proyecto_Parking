using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Proyecto_Parking
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string KEY = "NTY2OTI3QDMxMzkyZTM0MmUzMG85Q0toWFI2U2dGcVIwdjlXRHBDVGlHanpxZFlWVEJLcnZWUWtuSE5EekU9";

        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(KEY);
        }

    }
}
