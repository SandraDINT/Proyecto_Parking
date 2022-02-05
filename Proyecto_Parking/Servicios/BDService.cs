using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Parking.Servicios
{
    class BDService
    {
        //Si no existe, lo creará
        SqliteConnection conexion = new SqliteConnection("Data Source = C:/bd_dint/parking.db");

        #region TABLA CLIENTES insert, update, delete, select *
        //insert
        public void InsertaCliente(Cliente cliente)
        {
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "INSERT INTO clientes(nombre,documento, foto, edad, genero,telefono) VALUES (@nombre,@documento,@foto,@edad,@genero,@telefono)";
            comando.Parameters.Add("@nombre", SqliteType.Text);
            comando.Parameters.Add("@documento", SqliteType.Text);
            comando.Parameters.Add("@foto", SqliteType.Text);
            comando.Parameters.Add("@edad", SqliteType.Integer);
            comando.Parameters.Add("@genero", SqliteType.Text);
            comando.Parameters.Add("@telefono", SqliteType.Text);
            //asigno valores
            comando.Parameters["@nombre"].Value = cliente.Nombre;
            comando.Parameters["@documento"].Value = cliente.Documento;
            comando.Parameters["@foto"].Value = cliente.Foto;
            comando.Parameters["@edad"].Value = cliente.Edad;
            comando.Parameters["@genero"].Value = cliente.Genero;
            comando.Parameters["@telefono"].Value = cliente.Telefono;
            //ejecuta comando
            comando.ExecuteNonQuery();
            //cierra conexión
            conexion.Close();
        }

        //delete
        public void EliminaCliente(Cliente cliente)
        {
            //abro conexion
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            //Consulta de selección
            comando.CommandText = "DELETE FROM clientes WHERE id_cliente = @id";
            comando.Parameters.Add("@id", SqliteType.Integer);
            comando.Parameters["@id"].Value = cliente.IdCliente;
            //ejecuta comando
            comando.ExecuteNonQuery();
            //cierro conexion
            conexion.Close();
        }
        //update
        public void EditaCliente(Cliente cliente, int id)
        {
            //abro conexion
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            //Consulta de selección
            comando.CommandText = "UPDATE clientes " +
                "SET nombre = @nombre, documento = @documento, " +
                "foto = @foto, edad = @edad, genero = @genero, " +
                "telefono = @telefono WHERE id_cliente = @id";

            comando.Parameters.Add("@id", SqliteType.Integer);
            comando.Parameters.Add("@nombre", SqliteType.Text);
            comando.Parameters.Add("@documento", SqliteType.Text);
            comando.Parameters.Add("@foto", SqliteType.Text);
            comando.Parameters.Add("@edad", SqliteType.Integer);
            comando.Parameters.Add("@genero", SqliteType.Text);
            comando.Parameters.Add("@telefono", SqliteType.Text);
            //asigno valores
            comando.Parameters["@id"].Value = id;
            comando.Parameters["@nombre"].Value = cliente.Nombre;
            comando.Parameters["@documento"].Value = cliente.Documento;
            comando.Parameters["@foto"].Value = cliente.Foto;
            comando.Parameters["@edad"].Value = cliente.Edad;
            comando.Parameters["@genero"].Value = cliente.Genero;
            comando.Parameters["@telefono"].Value = cliente.Telefono;
            //ejecuta comando
            int a = comando.ExecuteNonQuery();
            //cierro conexion
            conexion.Close();
        }
        //select *
        public ObservableCollection<Cliente> RecorreClientes()
        {
            //abro conexion
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            //Consulta de selección
            comando.CommandText = "SELECT * FROM clientes";
            SqliteDataReader lector = comando.ExecuteReader();
            ObservableCollection<Cliente> clientes = new ObservableCollection<Cliente>();
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
            return clientes;
        }
        public Cliente BuscaClientePorId(int idBuscar)
        {
            //abro conexion
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            //Consulta de selección
            comando.CommandText = "SELECT * FROM clientes WHERE id_cliente LIKE @idBuscar";
            comando.Parameters.Add("@idBuscar", SqliteType.Text);
            comando.Parameters["@idBuscar"].Value = idBuscar;
            SqliteDataReader lector = comando.ExecuteReader();
            Cliente clienteBuscado = null;
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
            return clienteBuscado;
        }
        //select one
        public Cliente BuscaClientePorDocumento(string documentoBuscar)
        {
            //abro conexion
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            //Consulta de selección
            comando.CommandText = "SELECT * FROM clientes WHERE documento LIKE @documentoBuscar";
            comando.Parameters.Add("@documentoBuscar", SqliteType.Text);
            comando.Parameters["@documentoBuscar"].Value = documentoBuscar;
            SqliteDataReader lector = comando.ExecuteReader();
            Cliente clienteBuscado = null;
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
            return clienteBuscado;
        }
        #endregion

        #region TABLA MARCAS insert, delete, update, select *
        //insert
        public void InsertaMarca(string marca)
        {
            //abre conexión
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "INSERT INTO marcas(marca) VALUES (@marca)";
            comando.Parameters.Add("@marca", SqliteType.Text);
            comando.Parameters["@marca"].Value = marca;
            comando.ExecuteNonQuery();
            //cierra conexión
            conexion.Close();
        }
        public String BuscaMarcaPorId(int idBuscar)
        {
            //abro conexion
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            //Consulta de selección
            string marca = null;
            comando.CommandText = "SELECT * FROM marcas WHERE id_marca LIKE @idBuscar";
            comando.Parameters.Add("@idBuscar", SqliteType.Integer);
            comando.Parameters["@idBuscar"].Value = idBuscar;
            SqliteDataReader lector = comando.ExecuteReader();
            if (lector.HasRows)
            {
                lector.Read();
                marca = (string)lector["marca"];
            }
            //cierro conexion
            conexion.Close();
            return marca;
        }
        public Marca BuscaMarca(string marcaBuscar)
        {
            //abro conexion
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            //Consulta de selección
            comando.CommandText = "SELECT * FROM marcas WHERE marca LIKE @marcaBuscar";
            comando.Parameters.Add("@marcaBuscar", SqliteType.Text);
            comando.Parameters["@marcaBuscar"].Value = marcaBuscar;
            SqliteDataReader lector = comando.ExecuteReader();
            Marca marcaObj = null;
            if (lector.HasRows)
            {
                lector.Read();
                long id = (long)lector["id_marca"];
                string marca = (string)lector["marca"];
                marcaObj = new Marca((int)id, marca);
            }
            //cierro conexion
            conexion.Close();
            return marcaObj;
        }
        public ObservableCollection<Marca> RecorreMarcas()
        {
            //abro conexion
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            //Consulta de selección
            comando.CommandText = "SELECT * FROM marcas";
            SqliteDataReader lector = comando.ExecuteReader();
            ObservableCollection<Marca> marcas = new ObservableCollection<Marca>();
            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    long id = (long)lector["id_marca"];
                    string marca = (string)lector["marca"];
                    marcas.Add(new Marca((int)id, marca));
                }
            }
            //cierro conexion
            conexion.Close();
            return marcas;
        }
        public ObservableCollection<String> RecorreMarcaCadena()
        {
            //abro conexion
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            //Consulta de selección
            comando.CommandText = "SELECT * FROM marcas";
            SqliteDataReader lector = comando.ExecuteReader();
            ObservableCollection<String> marcas = new ObservableCollection<String>();
            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    string marca = (string)lector["marca"];
                    marcas.Add(marca);
                }
            }
            //cierro conexion
            conexion.Close();
            return marcas;
        }
        //DELETE
        public void EliminaMarca(Marca marca)
        {
            //abre conexión
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "DELETE FROM marcas WHERE id_marca = @id";
            comando.Parameters.Add("@id", SqliteType.Integer);
            comando.Parameters["@id"].Value = marca.IdMarca;
            comando.ExecuteNonQuery();
            //cierra conexión
            conexion.Close();
        }
        //UPDATE
        public void EditaMarca(Marca marcaNueva, int idMarcaVieja)
        {
            //abre conexión
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "UPDATE marcas SET marca = @marca WHERE id_marca = @id";
            comando.Parameters.Add("@id", SqliteType.Integer);
            comando.Parameters.Add("@marca", SqliteType.Text);
            comando.Parameters["@id"].Value = idMarcaVieja;
            comando.Parameters["@marca"].Value = marcaNueva.MarcaCadena;
            comando.ExecuteNonQuery();
            //cierra conexión
            conexion.Close();
        }
        #endregion

        #region TABLA ESTACIONAMIENTOS insert, update, select *
        //insert
        public void InsertaEstacionamiento(Estacionamiento estacionamiento)
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
            comando.Parameters["@id_vehiculo"].Value = estacionamiento.IdVehiculo;
            comando.Parameters["@matricula"].Value = estacionamiento.Matricula;
            comando.Parameters["@entrada"].Value = estacionamiento.Entrada;
            comando.Parameters["@salida"].Value = "";
            comando.Parameters["@importe"].Value = 0;
            comando.Parameters["@tipo"].Value = estacionamiento.Tipo;
            //ejecuta comando
            comando.ExecuteNonQuery();
            //cierra conexión
            conexion.Close();
        }
        //select *
        public ObservableCollection<Estacionamiento> RecorreEstacionamientos()
        {
            //abro conexion
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            //Consulta de selección
            comando.CommandText = "SELECT * FROM estacionamientos";
            SqliteDataReader lector = comando.ExecuteReader();
            ObservableCollection<Estacionamiento> estacionamiento = new ObservableCollection<Estacionamiento>();
            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    long idEstacionamiento = (long)lector["id_estacionamiento"];
                    long idVehiculo = (long)lector["id_vehiculo"];
                    string matricula = (string)lector["matricula"];
                    string entrada = (string)lector["entrada"];
                    string salida = (string)lector["salida"];
                    double importe = (double)lector["importe"];
                    string tipo = (string)lector["tipo"];
                    //añado estacionamiento a la lista
                    estacionamiento.Add(new Estacionamiento((int)idEstacionamiento,
                        (int)idVehiculo, matricula, entrada, salida, importe, tipo));
                }
            }
            //cierro conexion
            conexion.Close();
            return estacionamiento;
        }
        //select estacionamientos no finalizados
        public ObservableCollection<Estacionamiento> RecorreEstacionamientosNoFinalizados()
        {
            //abro conexion
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            //Consulta de selección
            comando.CommandText = "SELECT * FROM estacionamientos WHERE salida LIKE ''";
            SqliteDataReader lector = comando.ExecuteReader();
            ObservableCollection<Estacionamiento> estacionamiento = new ObservableCollection<Estacionamiento>();
            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    long idEstacionamiento = (long)lector["id_estacionamiento"];
                    long idVehiculo = (long)lector["id_vehiculo"];
                    string matricula = (string)lector["matricula"];
                    string entrada = (string)lector["entrada"];
                    string salida = (string)lector["salida"];
                    double importe = (double)lector["importe"];
                    string tipo = (string)lector["tipo"];
                    //añado estacionamiento a la lista
                    estacionamiento.Add(new Estacionamiento((int)idEstacionamiento,
                        (int)idVehiculo, matricula, entrada, salida, importe, tipo));
                }
            }
            //cierro conexion
            conexion.Close();
            return estacionamiento;
        }
        //update en principio solo edita para poder finalizar el estacionamiento ???
        public void EditaEstacionamiento(Estacionamiento estacionamientoFinalizado)
        {
            //abro conexion
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            //Consulta de selección
            comando.CommandText = "UPDATE estacionamientos " +
                "SET salida = @salida, importe = @importe WHERE id_estacionamiento = @id";
            comando.Parameters.Add("@id", SqliteType.Integer);
            comando.Parameters.Add("@salida", SqliteType.Text);
            comando.Parameters.Add("@importe", SqliteType.Real);

            //asigno valores
            comando.Parameters["@id"].Value = estacionamientoFinalizado.IdEstacionamiento;
            comando.Parameters["@salida"].Value = estacionamientoFinalizado.Salida;
            comando.Parameters["@importe"].Value = estacionamientoFinalizado.Importe;

            //ejecuta comando
            comando.ExecuteNonQuery();
            //cierro conexion
            conexion.Close();
        }

        #endregion

        #region TABLA VEHICULOS insert, delete, update, select *
        //insert
        public void InsertaVehiculo(Vehiculo vehiculo)
        {
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "INSERT INTO vehiculos(id_cliente, matricula, id_marca, modelo, tipo) " +
                "VALUES (@id_cliente,@matricula,@id_marca,@modelo,@tipo)";
            comando.Parameters.Add("@id_cliente", SqliteType.Integer);
            comando.Parameters.Add("@matricula", SqliteType.Text);
            comando.Parameters.Add("@id_marca", SqliteType.Integer);
            comando.Parameters.Add("@modelo", SqliteType.Text);
            comando.Parameters.Add("@tipo", SqliteType.Text);
            //asigno valores
            comando.Parameters["@id_cliente"].Value = vehiculo.IdCliente;
            comando.Parameters["@matricula"].Value = vehiculo.Matricula;
            comando.Parameters["@id_marca"].Value = vehiculo.IdMarca;
            comando.Parameters["@modelo"].Value = vehiculo.Modelo;
            comando.Parameters["@tipo"].Value = vehiculo.Tipo;
            //ejecuta comando
            comando.ExecuteNonQuery();
            //cierra conexión
            conexion.Close();
        }

        //select *
        public ObservableCollection<Vehiculo> RecorreVehiculos()
        {
            //abro conexion
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            //Consulta de selección
            comando.CommandText = "SELECT * FROM vehiculos";
            SqliteDataReader lector = comando.ExecuteReader();
            ObservableCollection<Vehiculo> vehiculos = new ObservableCollection<Vehiculo>();
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
            return vehiculos;
        }
        //
        public bool BuscaVehiculosPorIdCliente(Cliente cliente)
        {
            //abro conexion
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            //Consulta de selección
            comando.CommandText = "SELECT * FROM vehiculos WHERE id_cliente LIKE @idCliente";
            comando.Parameters.Add("@idCliente", SqliteType.Integer);
            comando.Parameters["@idCliente"].Value = cliente.IdCliente;

            SqliteDataReader lector = comando.ExecuteReader();
            bool encuentra = false;
            if (lector.HasRows)
            {
                encuentra = true;
            }
            //cierro conexion
            conexion.Close();
            return encuentra;
        }

        //delete
        public void EliminaVehiculo(Vehiculo vehiculo)
        {
            //abro conexion
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            //Consulta de selección
            comando.CommandText = "DELETE FROM vehiculos WHERE id_vehiculo = @id";
            comando.Parameters.Add("@id", SqliteType.Integer);
            comando.Parameters["@id"].Value = vehiculo.IdVehiculo;
            comando.ExecuteNonQuery();
            //cierro conexion
            conexion.Close();
        }

        //update
        public void EditaVehiculo(Vehiculo vehiculo, int id)
        {
            //abro conexion
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            //Consulta de selección
            comando.CommandText = "UPDATE vehiculos " +
                "SET id_cliente = @id_cliente, matricula = @matricula, " +
                "id_marca = @id_marca, modelo = @modelo, tipo = @tipo " +
                "WHERE id_vehiculo = @id";

            comando.Parameters.Add("@id", SqliteType.Integer);
            comando.Parameters.Add("@id_cliente", SqliteType.Integer);
            comando.Parameters.Add("@matricula", SqliteType.Text);
            comando.Parameters.Add("@id_marca", SqliteType.Integer);
            comando.Parameters.Add("@modelo", SqliteType.Text);
            comando.Parameters.Add("@tipo", SqliteType.Text);
            //asigno valores
            comando.Parameters["@id"].Value = id;
            comando.Parameters["@id_cliente"].Value = vehiculo.IdCliente;
            comando.Parameters["@matricula"].Value = vehiculo.Matricula;
            comando.Parameters["@id_marca"].Value = vehiculo.IdMarca;
            comando.Parameters["@modelo"].Value = vehiculo.Modelo;
            comando.Parameters["@tipo"].Value = vehiculo.Tipo;
            //ejecuta comando
            comando.ExecuteNonQuery();
            //cierro conexion
            conexion.Close();
        }
        #endregion
    }
}
