﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proyecto_Parking.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.10.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("DefaultEndpointsProtocol=https;AccountName=dintactividadjuego;AccountKey=lXb3S+5V" +
            "M1XHwy19G3f44mrkanj4apLcSu8uSX9pP/Tn89kpIRJGhtyO2tQuqlNmQwKz5DobVSR6cIe1JkmXDg==" +
            ";EndpointSuffix=core.windows.net")]
        public string cadenaConexion {
            get {
                return ((string)(this["cadenaConexion"]));
            }
            set {
                this["cadenaConexion"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("imagenes")]
        public string nombreContenedorBlobs {
            get {
                return ((string)(this["nombreContenedorBlobs"]));
            }
            set {
                this["nombreContenedorBlobs"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("50")]
        public int numPlazas {
            get {
                return ((int)(this["numPlazas"]));
            }
            set {
                this["numPlazas"] = value;
            }
        }
    }
}
