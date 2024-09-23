using System.Text;
using DataLayer; // Assuming you have a DataLayer namespace with DBOperacion class

namespace Accesos.CLS
{
    public class Empleados
    {
        public int IDEmpleado { get; set; }
        public string Nombre { get; set; }
        public string Cargo { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }

        public bool Insertar()
        {
            bool resultado = false;
            DBOperacion operacion = new DBOperacion();
            StringBuilder sentencia = new StringBuilder();
            sentencia.Append("INSERT INTO empleados(Nombre, Cargo, Telefono, Email) VALUES(");
            sentencia.Append("'" + Nombre + "','" + Cargo + "','" + Telefono + "','" + Email + "');");
            try
            {
                if (operacion.EjecutarSentencia(sentencia.ToString()) >= 0)
                {
                    resultado = true;
                }
                else
                {
                    resultado = false;
                }
            }
            catch (Exception)
            {
                resultado = false;
            }
            return resultado;
        }

        public bool Actualizar()
        {
            bool resultado = false;
            DBOperacion operacion = new DBOperacion();
            StringBuilder sentencia = new StringBuilder();
            sentencia.Append("UPDATE empleados SET ");
            sentencia.Append("Nombre = '" + Nombre + "'," +
                             "Cargo = '" + Cargo + "'," +
                             "Telefono = '" + Telefono + "'," +
                             "Email = '" + Email + "'");
            sentencia.Append("WHERE IDEmpleado = " + IDEmpleado + "; ");
            try
            {
                if (operacion.EjecutarSentencia(sentencia.ToString()) >= 0)
                {
                    resultado = true;
                }
                else
                {
                    resultado = false;
                }
            }
            catch (Exception)
            {
                resultado = false;
            }
            return resultado;
        }

        public bool Eliminar()
        {
            bool resultado = false;
            DBOperacion operacion = new DBOperacion();
            StringBuilder sentencia = new StringBuilder();
            sentencia.Append("DELETE FROM empleados ");
            sentencia.Append("WHERE IDEmpleado = " + IDEmpleado + ";");
            try
            {
                if (operacion.EjecutarSentencia(sentencia.ToString()) >= 0)
                {
                    resultado = true;
                }
                else
                {
                    resultado = false;
                }
            }
            catch (Exception)
            {
                resultado = false;
            }
            return resultado;
        }
    }
}