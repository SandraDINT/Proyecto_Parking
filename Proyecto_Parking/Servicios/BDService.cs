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
    /// <summary>
    /// La clase BDService se encargará de conectar con la base de datos parking para recoger, insertar, actualizar o eliminar los datos solicitados.
    /// </summary>
    class BDService
    {
        //Si no existe, lo creará
        public readonly SqliteConnection conexion = new SqliteConnection("Data Source = " + Properties.Settings.Default.EnlaceBD);

        #region TABLA CLIENTES
        /// <summary>
        /// El método RecorreClientes busca todos los registros que hay en la tabla clientes y los inserta en una lista.
        /// </summary>
        /// <returns>Una lista con los clientes.</returns>
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
        /// <summary>
        /// El método BuscaClientePorId busca el cliente según una id proporcionada.
        /// </summary>
        /// <param name="idBuscar">Este parámetro es la id del cliente a buscar</param>
        /// <returns>Un objeto cliente con el cliente si lo encuentra, y si no, devuelve null</returns>
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
        /// <summary>
        /// Este método busca cliente por documento proporcionado
        /// </summary>
        /// <param name="documentoBuscar">Este parámetro es el documento del cliente a buscar</param>
        /// <returns>Un objeto cliente con el cliente si encuentra coincidencia, y si no, devuelve null</returns>
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

        /// <summary>
        /// Este método busca estacionamiento no finalizado por matricula proporcionada
        /// </summary>
        /// <param name="matricula">Este parámetro es la matricula del coche estacionado a buscar</param>
        /// <returns>True si encuentra coincidencia, false si no</returns>
        public bool BuscaEstacionamientoPorMatricula(string matricula)
        {
            bool encuentra = false;
            try
            {
                //abro conexion
                conexion.Open();
                SqliteCommand comando = conexion.CreateCommand();
                //Consulta de selección
                comando.CommandText = "SELECT * FROM estacionamientos WHERE salida = '' AND matricula LIKE @matricula";
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
        /// <summary>
        /// Este método busca estacionamiento por matricula proporcionada
        /// </summary>
        /// <param name="estacionamiento">Este parámetro es el estacionamiento a insertar</param>
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
        /// <summary>
        /// Este método cuenta los estacionamientos no finalizados de los vehículos que sean de tipo coche
        /// </summary>
        /// <returns>Un entero con la cantidad de estacionamientos no finalizados</returns>
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
        /// <summary>
        /// Este método cuenta los estacionamientos no finalizados de los vehículos que sean de tipo motocicleta
        /// </summary>
        /// <returns>Un entero con la cantidad de estacionamientos no finalizados</returns>
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
        /// <summary>
        /// Este método busca estacionamientos no finalizados y los recoge en una lista
        /// </summary>
        /// <returns>Una lista de Estacionamientos con los estacionamientos no finalizados</returns>
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

        /// <summary>
        /// Este método recorre los vehículos de la tabla vehículos y los recoge en una lista
        /// </summary>
        /// <returns>Una lista con los vehículos registrados</returns>
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
        /// <summary>
        /// Este método busca la id del vehículo según la matrícula proporcionada
        /// </summary>
        /// <param name="matricula">Este parámetro es la matrícula del vehiculo a buscar</param>
        /// <returns>La id del vehículo si hay coincidencia, si no, devuelve -1</returns>
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
        /// <summary>
        /// Este método busca vehículos por la id del cliente
        /// </summary>
        /// <param name="cliente">Este parámetro es el cliente con el cual buscaremos vehiculos suyos registrados</param>
        /// <returns>Si ese cliente tiene vehículos dedolverá true, si no, false</returns>
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
        /// <summary>
        /// Este método busca vehículos con una matrícula proporcionada
        /// </summary>
        /// <param name="matricula">Este parámetro es la matrícula con la que buscaremos el vehículo</param>
        /// <returns>Si encuentra un vehículo devuelve true, si no, false</returns>
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
