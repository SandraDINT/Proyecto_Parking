using Microsoft.Data.Sqlite;
using Proyecto_Parking.clase;
using Proyecto_Parking.Clases;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Proyecto_Parking.Servicios
{
    class BDService
    {
        //Si no existe, lo creará
        public readonly SqliteConnection conexion = new SqliteConnection("Data Source = C:/bd_dint/parking.db");

        #region TABLA CLIENTES
        //select *
        public ObservableCollection<Cliente> RecorreClientes()
        {
            ObservableCollection<Cliente> clientes = new ObservableCollection<Cliente>();
            try
            {
                //abro conexion
                conexion.Open();
                SqliteCommand comando = conexion.CreateCommand();
                //Consulta de selección
                comando.CommandText = "SELECT * FROM clientes";
                SqliteDataReader lector = comando.ExecuteReader();

                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        long id = (long)lector["id_cliente"];
                        string nombre = (string)lector["nombre"];
                        string documento = (string)lector["documento"];
                        string foto = (string)lector["foto"];
                        long edad = (long)lector["edad"];
                        string genero = (string)lector["genero"];
                        string telefono = (string)lector["telefono"];
                        //añado cliente a la lista
                        clientes.Add(new Cliente((int)id, nombre, documento, foto, (int)edad, genero, telefono));
                    }
                }
                //cierro conexion
                conexion.Close();
            }
            catch (SqliteException)
            {
                Console.WriteLine("No se ha podido conectar");
            }
            catch (Exception)
            {
                Console.WriteLine("Error desconocido");
            }
            return clientes;
        }
        public Cliente BuscaClientePorId(int idBuscar)
        {
            Cliente clienteBuscado = null;
            try
            {
                //abro conexion
                conexion.Open();
                SqliteCommand comando = conexion.CreateCommand();
                //Consulta de selección
                comando.CommandText = "SELECT * FROM clientes WHERE id_cliente LIKE @idBuscar";
                comando.Parameters.Add("@idBuscar", SqliteType.Text);
                comando.Parameters["@idBuscar"].Value = idBuscar;
                SqliteDataReader lector = comando.ExecuteReader();
                if (lector.HasRows)
                {
                    //Lee el resultado
                    lector.Read();
                    long id = (long)lector["id_cliente"];
                    string nombre = (string)lector["nombre"];
                    string documento = (string)lector["documento"];
                    string foto = (string)lector["foto"];
                    long edad = (long)lector["edad"];
                    string genero = (string)lector["genero"];
                    string telefono = (string)lector["telefono"];
                    clienteBuscado = new Cliente((int)id, nombre, documento, foto, (int)edad, genero, telefono);
                }
                //cierro conexion
                conexion.Close();
            }
            catch (SqliteException)
            {
                Console.WriteLine("No se ha podido conectar");
            }
            catch (Exception)
            {
                Console.WriteLine("Error desconocido");
            }
            return clienteBuscado;
        }
        //select one
        public Cliente BuscaClientePorDocumento(string documentoBuscar)
        {
            Cliente clienteBuscado = null;
            try
            {
                //abro conexion
                conexion.Open();
                SqliteCommand comando = conexion.CreateCommand();
                //Consulta de selección
                comando.CommandText = "SELECT * FROM clientes WHERE documento LIKE @documentoBuscar";
                comando.Parameters.Add("@documentoBuscar", SqliteType.Text);
                comando.Parameters["@documentoBuscar"].Value = documentoBuscar;
                SqliteDataReader lector = comando.ExecuteReader();

                if (lector.HasRows)
                {
                    //Lee el resultado
                    lector.Read();
                    long id = (long)lector["id_cliente"];
                    string nombre = (string)lector["nombre"];
                    string documento = (string)lector["documento"];
                    string foto = (string)lector["foto"];
                    long edad = (long)lector["edad"];
                    string genero = (string)lector["genero"];
                    string telefono = (string)lector["telefono"];
                    clienteBuscado = new Cliente((int)id, nombre, documento, foto, (int)edad, genero, telefono);
                }
                //cierro conexion
                conexion.Close();
            }
            catch (SqliteException)
            {
                Console.WriteLine("No se ha podido conectar");
            }
            catch (Exception)
            {
                Console.WriteLine("Error desconocido");
            }
            return clienteBuscado;
        }

        public bool BuscaEstacionamientoPorMatricula(string matricula)
        {
            bool encuentra = false;
            try
            {
                //abro conexion
                conexion.Open();
                SqliteCommand comando = conexion.CreateCommand();
                //Consulta de selección
                comando.CommandText = "SELECT * FROM estacionamientos WHERE matricula LIKE @matricula";
                comando.Parameters.Add("@matricula", SqliteType.Text);
                comando.Parameters["@matricula"].Value = matricula;

                SqliteDataReader lector = comando.ExecuteReader();
                if (lector.HasRows)
                {
                    encuentra = true;
                }
                //cierro conexion
                conexion.Close();
            }
            catch (SqliteException)
            {
                Console.WriteLine("No se ha podido conectar");
            }
            catch (Exception)
            {
                Console.WriteLine("Error desconocido");
            }
            return encuentra;
        }

        #endregion

        #region TABLA ESTACIONAMIENTOS
        //insert
        public void InsertaEstacionamiento(Estacionamiento estacionamiento)
        {
            try
            {
                conexion.Open();
                SqliteCommand comando = conexion.CreateCommand();
                comando.CommandText = "INSERT INTO estacionamientos(id_vehiculo, matricula, entrada, salida, importe, tipo)" +
                    " VALUES (@id_vehiculo, @matricula, @entrada, @salida, @importe, @tipo)";
                comando.Parameters.Add("@id_vehiculo", SqliteType.Integer);
                comando.Parameters.Add("@matricula", SqliteType.Text);
                comando.Parameters.Add("@entrada", SqliteType.Text);
                comando.Parameters.Add("@salida", SqliteType.Text);
                comando.Parameters.Add("@importe", SqliteType.Real);
                comando.Parameters.Add("@tipo", SqliteType.Text);

                //asigno valores
                if (estacionamiento.IdVehiculo == -1)
                    comando.Parameters["@id_vehiculo"].Value = DBNull.Value;
                else
                    comando.Parameters["@id_vehiculo"].Value = estacionamiento.IdVehiculo;

                comando.Parameters["@matricula"].Value = estacionamiento.Matricula;
                comando.Parameters["@entrada"].Value = DateTime.Now.ToString(); //pruebas
                comando.Parameters["@salida"].Value = "";
                comando.Parameters["@importe"].Value = 0;
                comando.Parameters["@tipo"].Value = estacionamiento.Tipo;
                //ejecuta comando
                comando.ExecuteNonQuery();
                //cierra conexión
                conexion.Close();
            }
            catch (SqliteException)
            {
                Console.WriteLine("No se ha podido conectar");
            }
            catch (Exception)
            {
                Console.WriteLine("Error desconocido");
            }
        }
        //cuenta estacionamientos
        public int CuentaEstacionamientosNoFinalizadosCoche()
        {
            int resultado = -1;
            try
            {
                //abro conexion
                conexion.Open();
                SqliteCommand comando = conexion.CreateCommand();
                //Consulta de selección
                comando.CommandText = "SELECT COUNT(*) AS resultado FROM estacionamientos WHERE salida LIKE '' AND tipo LIKE 'Coche'";
                SqliteDataReader lector = comando.ExecuteReader();
                if (lector.HasRows)
                {
                    lector.Read();
                    resultado = Convert.ToInt32(lector["resultado"]); //daba error al castear así -> (int)lector["resultado]
                }
                //cierro conexion
                conexion.Close();
            }
            catch (SqliteException)
            {
                Console.WriteLine("No se ha podido conectar");
            }
            catch (Exception)
            {
                Console.WriteLine("Error desconocido");
            }
            return resultado;
        }
        public int CuentaEstacionamientosNoFinalizadosMoto()
        {
            int resultado = -1;
            try
            {
                //abro conexion
                conexion.Open();
                SqliteCommand comando = conexion.CreateCommand();
                //Consulta de selección
                comando.CommandText = "SELECT COUNT(*) AS resultado FROM estacionamientos WHERE salida LIKE '' AND tipo LIKE 'Motocicleta'";
                SqliteDataReader lector = comando.ExecuteReader();
                if (lector.HasRows)
                {
                    lector.Read();
                    resultado = Convert.ToInt32(lector["resultado"]);
                }
                //cierro conexion
                conexion.Close();
            }
            catch (SqliteException)
            {
                Console.WriteLine("No se ha podido conectar");
            }
            catch (Exception)
            {
                Console.WriteLine("Error desconocido");
            }
            return resultado;
        }
        //select estacionamientos no finalizados
        public ObservableCollection<Estacionamiento> RecorreEstacionamientosNoFinalizados()
        {
            ObservableCollection<Estacionamiento> estacionamiento = new ObservableCollection<Estacionamiento>();
            try
            {
                //abro conexion
                conexion.Open();
                SqliteCommand comando = conexion.CreateCommand();
                //Consulta de selección
                comando.CommandText = "SELECT * FROM estacionamientos WHERE salida LIKE ''";
                SqliteDataReader lector = comando.ExecuteReader();
                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        long idEstacionamiento = (long)lector["id_estacionamiento"];
                        long idVehiculo = (lector["id_vehiculo"] == DBNull.Value) ? -1 : (long)lector["id_vehiculo"];
                        string matricula = (string)lector["matricula"];
                        string entrada = (string)lector["entrada"];
                        string salida = (string)lector["salida"];
                        double importe = (double)lector["importe"];
                        string tipo = (string)lector["tipo"];
                        //añado cliente a la lista
                        estacionamiento.Add(new Estacionamiento((int)idEstacionamiento,
                            (int)idVehiculo, matricula, entrada, salida, importe, tipo));
                    }
                }
                //cierro conexion
                conexion.Close();
            }
            catch (SqliteException)
            {
                Console.WriteLine("No se ha podido conectar");
            }
            catch (Exception)
            {
                Console.WriteLine("Error desconocido");
            }
            return estacionamiento;
        }

        #endregion

        #region TABLA VEHICULOS

        //select *
        public ObservableCollection<Vehiculo> RecorreVehiculos()
        {
            ObservableCollection<Vehiculo> vehiculos = new ObservableCollection<Vehiculo>();
            try
            {
                //abro conexion
                conexion.Open();
                SqliteCommand comando = conexion.CreateCommand();
                //Consulta de selección
                comando.CommandText = "SELECT * FROM vehiculos";
                SqliteDataReader lector = comando.ExecuteReader();
                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        long idVehiculo = (long)lector["id_vehiculo"];
                        long idCliente = (long)lector["id_cliente"];
                        string matricula = (string)lector["matricula"];
                        long idMarca = (long)lector["id_marca"];
                        string modelo = (string)lector["modelo"];
                        string tipo = (string)lector["tipo"];
                        //añado cliente a la lista
                        vehiculos.Add(new Vehiculo((int)idVehiculo, (int)idCliente, matricula, (int)idMarca, modelo, tipo));
                    }
                }
                //cierro conexion
                conexion.Close();
            }
            catch (SqliteException)
            {
                Console.WriteLine("No se ha podido conectar");
            }
            catch (Exception)
            {
                Console.WriteLine("Error desconocido");
            }
            return vehiculos;
        }
        public int BuscaIDVehiculoPorMatricula(String matricula)
        {
            int id = -1;
            try
            {
                //abro conexion
                conexion.Open();
                SqliteCommand comando = conexion.CreateCommand();
                //Consulta de selección
                comando.CommandText = "SELECT * FROM vehiculos WHERE matricula LIKE @matricula";
                comando.Parameters.Add("@matricula", SqliteType.Text);
                comando.Parameters["@matricula"].Value = matricula;
                SqliteDataReader lector = comando.ExecuteReader();
                if (lector.HasRows)
                {
                    lector.Read();
                    id = Convert.ToInt32((long)lector["id_vehiculo"]);
                }
                //cierro conexion
                conexion.Close();
            }
            catch (SqliteException)
            {
                Console.WriteLine("No se ha podido conectar");
            }
            catch (Exception)
            {
                Console.WriteLine("Error desconocido");
            }
            return id;
        }
        //
        public bool BuscaVehiculosPorIdCliente(Cliente cliente)
        {
            bool encuentra = false;
            try
            {
                //abro conexion
                conexion.Open();
                SqliteCommand comando = conexion.CreateCommand();
                //Consulta de selección
                comando.CommandText = "SELECT * FROM vehiculos WHERE id_cliente LIKE @idCliente";
                comando.Parameters.Add("@idCliente", SqliteType.Integer);
                comando.Parameters["@idCliente"].Value = cliente.IdCliente;
                SqliteDataReader lector = comando.ExecuteReader();
                if (lector.HasRows)
                {
                    encuentra = true;
                }
                //cierro conexion
                conexion.Close();
            }
            catch (SqliteException)
            {
                Console.WriteLine("No se ha podido conectar");
            }
            catch (Exception)
            {
                Console.WriteLine("Error desconocido");
            }
            return encuentra;
        }

        public bool BuscaVehiculosPorMatricula(String matricula)
        {
            bool encuentra = false;
            try
            {
                //abro conexion
                conexion.Open();
                SqliteCommand comando = conexion.CreateCommand();
                //Consulta de selección
                comando.CommandText = "SELECT * FROM vehiculos WHERE matricula LIKE @matricula";
                comando.Parameters.Add("@matricula", SqliteType.Text);
                comando.Parameters["@matricula"].Value = matricula;
                SqliteDataReader lector = comando.ExecuteReader();
                if (lector.HasRows)
                {
                    encuentra = true;
                }
                //cierro conexion
                conexion.Close();
            }
            catch (SqliteException)
            {
                Console.WriteLine("No se ha podido conectar");
            }
            catch (Exception)
            {
                Console.WriteLine("Error desconocido");
            }
            return encuentra;
        }
        #endregion
    }
}
