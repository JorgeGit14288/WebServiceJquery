using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFParqueo.Models;

namespace WCFParqueo.Dao
{
    interface IVehiculosDao
    {
        string Crear(Vehiculos v);
        string Actualizar(Vehiculos v);
        string Eliminar(string noPlaca);
        Vehiculos BuscarPlaca(string noPlaca);
        List<Vehiculos> BuscarPropietario(string propietario);
        List<Vehiculos> Listar();
        string test();
    }
}
