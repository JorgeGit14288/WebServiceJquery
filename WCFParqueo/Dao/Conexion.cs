using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WCFParqueo.Dao
{
    public class Conexion
    {
       protected string cadena_conexion { get; set; }
       public SqlConnection connection { get; set; }
        public Conexion()
        {
            cadena_conexion= System.Configuration.ConfigurationManager.ConnectionStrings["ParqueoDb_connection"].ConnectionString;
            connection = new SqlConnection(cadena_conexion);

        }
        public void Conectar()
        {
            connection.Open();
        }
        public void Desconectar()
        {
            connection.Close();
        }
        public void Dispose()
        {
            connection.Dispose();
        }
        public string test()
        {
            try
            {
                this.Conectar();
                return "Se ha conectado a la base de datos";
            }
            catch(Exception ex)
            {
                return "No se ha podido conectar a la base de datos " + ex.ToString();
            }
        }
    }
}