using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WCFParqueo.Models;
//importamos las librerias para conexion
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data.SqlTypes;
using System.Data;
//

namespace WCFParqueo.Dao
{
    public class VehiculosDao : IVehiculosDao
    {
        private SqlCommand command;
        private SqlDataReader dataReader;
        private SqlDataAdapter dataAdapter;
       
        Conexion conexion;
        
        public VehiculosDao()
        {
            //iniciamos las operaciones a usar
            conexion = new Conexion();
        }
        public string Actualizar(Vehiculos v)
        {
            try
            {
                //iniciamos la conexion
                conexion.Conectar();
                //inicimamos el comando
                command = new SqlCommand("sp_Vehiculos_Actualizar", conexion.connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                //agregarmos los parametros para usar el procedimiento almacenado
                command.Parameters.AddWithValue("@noPlaca", v.noPlaca);
                command.Parameters.AddWithValue("@tipo", v.tipo);
                command.Parameters.AddWithValue("@color", v.color);
                command.Parameters.AddWithValue("@propietario", v.propietario);
                command.Parameters.AddWithValue("@direccion", v.direccion);
                command.Parameters.AddWithValue("@telefono", v.telefono);

                //agregamos el parametro de salida
                command.Parameters.Add("@resultado",SqlDbType.VarChar,100).Direction= ParameterDirection.Output;
                //ejecutamos la transaccion
                int filasAfectadas = command.ExecuteNonQuery();
                conexion.Desconectar();
                if (filasAfectadas>0)
                {
                    return command.Parameters["@resultado"].Value.ToString();
                }
                else
                {
                    return command.Parameters["@resultado"].Value.ToString();
                } 
            }
            catch (Exception ex)
            {
                return "No se ha podido actualizar el registro " + ex.ToString();
            }
        }

        public Vehiculos BuscarPlaca(string noPlaca)
        {
            Vehiculos vehiculo = new Vehiculos();
            try
            {
                //iniciamos la conexion
                conexion.Conectar();
                //iniciamos el comando
                command = new SqlCommand("sp_Vehiculos_BuscarPlaca", conexion.connection);
                command.CommandType = CommandType.StoredProcedure;
                //agregamos el parametro de entrada
                command.Parameters.Add("@noPlaca", SqlDbType.VarChar, 25).Value = noPlaca;
                //ejecutamos el reader
                dataReader = command.ExecuteReader();
                //verificamos si el reader tiene algo
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        vehiculo.noPlaca = dataReader["noPlaca"].ToString();
                        vehiculo.tipo = dataReader["tipo"].ToString();
                        vehiculo.color = dataReader["color"].ToString();
                        vehiculo.propietario = dataReader["propietario"].ToString();
                        vehiculo.direccion = dataReader["direccion"].ToString();
                        vehiculo.telefono = dataReader["telefono"].ToString();
                        if (!String.IsNullOrEmpty(dataReader["imagen"].ToString()))
                        {
                            vehiculo.imagen = (byte[])dataReader["imagen"];
                        }
                    }
                    conexion.Desconectar();
                    
                }
                return vehiculo;

            }
            catch (Exception ex)
            {
                return vehiculo;
            }
            }

        public List<Vehiculos> BuscarPropietario(string propietario)
        {
            List<Vehiculos> lista = new List<Vehiculos>();
            try
            {
                //iniciamos la conexion
                conexion.Conectar();
                //inicmiamos el comando
                command = new SqlCommand("sp_Vehiculos_Listar", conexion.connection);
                //ejecutamos la lectura
                dataReader = command.ExecuteReader();
                //verificamos si el datareader tiene resultados
                if(dataReader.HasRows)
                {
                    while(dataReader.Read())
                    {
                        Vehiculos vehiculo = new Vehiculos();
                        vehiculo.noPlaca = dataReader["noPlaca"].ToString();
                        vehiculo.tipo = dataReader["tipo"].ToString();
                        vehiculo.color = dataReader["color"].ToString();
                        vehiculo.propietario = dataReader["propietario"].ToString();
                        vehiculo.direccion = dataReader["direccion"].ToString();
                        vehiculo.telefono = dataReader["telefono"].ToString();
                        if (!String.IsNullOrEmpty(dataReader["imagen"].ToString()))
                        {
                            vehiculo.imagen = (byte[])dataReader["imagen"];
                        }
                        lista.Add(vehiculo);
                       
                    }
                    dataReader.NextResult();
                }

                //desconectamos
                conexion.Desconectar();
                return lista;
            }
            catch (Exception ex)
            {
                return lista;
            }
        }

        public string Crear(Vehiculos v)
        {
            try
            {
                //iniciamos la conexion
                conexion.Conectar();
                //inicimamos el comando
                command = new SqlCommand("sp_Vehiculos_Insertar", conexion.connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                //agregarmos los parametros para usar el procedimiento almacenado
                command.Parameters.AddWithValue("@noPlaca", v.noPlaca);
                command.Parameters.AddWithValue("@tipo", v.tipo);
                command.Parameters.AddWithValue("@color", v.color);
                command.Parameters.AddWithValue("@propietario", v.propietario);
                command.Parameters.AddWithValue("@direccion", v.direccion);
                command.Parameters.AddWithValue("@telefono", v.telefono);

                //agregamos el parametro de salida
                command.Parameters.Add("@resultado", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                //ejecutamos la transaccion
                int filasAfectadas = command.ExecuteNonQuery();
                conexion.Desconectar();
                if (filasAfectadas > 0)
                {
                    return command.Parameters["@resultado"].Value.ToString();
                }
                else
                {
                    return command.Parameters["@resultado"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                return "No se ha podido Crear el registro " + ex.ToString();
            }
        }

        public string Eliminar(string noPlaca)
        {
            try
            {
                //abrimos la conexion
                conexion.Conectar();
                //creamos el comando
                command = new SqlCommand("sp_Vehiculos_Eliminar", conexion.connection);
                command.CommandType = CommandType.StoredProcedure;
                //agregamos el parametro de entrada
                command.Parameters.AddWithValue("@noPlaca", noPlaca);
                //agregamos el parametro de salida
                command.Parameters.Add("@resultado", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

                // ejecutamos el comando y obtenemos el numero de filas afectadas
                int filasAfectadas = command.ExecuteNonQuery();
                //desconectamos la bd
                conexion.Desconectar();
                //valuamos si afecto a las filas
                if (filasAfectadas>0)
                {
                    return command.Parameters["@resultado"].ToString();
                }
                else
                {
                    return command.Parameters["@resultado"].ToString();
                }
              
            }
            catch(Exception ex)
            {
                return "No se pudo conectar a la base de datos " + ex.ToString() ;
            }
        }

        public List<Vehiculos> Listar()
        {
            throw new NotImplementedException();
        }

        public string test()
        {
            return conexion.test();
        }
    }
}