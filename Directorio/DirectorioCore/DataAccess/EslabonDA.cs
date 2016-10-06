using DirectorioCore.Entities;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectorioCore.DataAccess
{
    public class EslabonDA
    {
        private static string ConnectionString = @"Driver={SQL Server};Server=192.168.1.220; Database=eslabonstd; Uid=eslabonstd; Pwd=eslabonstd;";
        /// <summary>
        /// Método que permite obtener la lista de nombres de los empleados
        /// </summary>
        /// <returns></returns>
        public static List<Catalogo> CatalogoEmpleados()
        {
            List<Catalogo> Empleados = new List<Catalogo>();
            try
            {
                string QueryString = @" select distinct r.id,  r.nombre + ' ' + r.paterno, em.Puesto
                                          from vwEmpleados em
                                    inner join recursos r on r.id = em.idEmpleado
                                        ";
                System.Data.Odbc.OdbcCommand command = new System.Data.Odbc.OdbcCommand(QueryString);
                using (System.Data.Odbc.OdbcConnection connection = new System.Data.Odbc.OdbcConnection(ConnectionString))
                {
                    command.Connection = connection;
                    connection.Open();
                    using (OdbcDataReader reader = command.ExecuteReader())
                    {
                        Catalogo Empleado;
                        while (reader.Read())
                        {
                            Empleado = new Catalogo();
                            Empleado.id = reader.GetInt32(0);
                            Empleado.descripcion = reader.GetString(1);
                            Empleado.codigo = reader.GetString(2);
                            Empleados.Add(Empleado);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BusinessLogic.Log.EscribeLog("Error: ElsabonDA.CatalogoEmpleados - " + ex.Message);
            }
            return Empleados;
        }
    }
}
