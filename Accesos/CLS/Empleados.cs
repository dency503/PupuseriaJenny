using System.Text;
using DataLayer; // Assuming you have a DataLayer namespace with DBOperacion class

namespace Accesos.CLS
{
    public class Empleados
    {
        public int idEmpleados { get; set; }
        public string nombresEmpleado { get; set; }
        public string apellidosEmpleado { get; set; }
        public string telefono { get; set; }
        public string direccion { get; set; }
        public string email { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public int idCargo { get; set; }

        public bool Insertar()
        {
            bool resultado = false;
            DBOperacion operacion = new DBOperacion();
            StringBuilder sentencia = new StringBuilder();
            sentencia.Append("INSERT INTO Empleados(nombresEmpleado, apellidosEmpleado, telefono, direccion, email, fechaNacimiento, idCargo) VALUES(");
            sentencia.Append("'" + nombresEmpleado + "', '" + apellidosEmpleado + "', '" + telefono + "', '" + direccion + "', '" + email + "', '" + fechaNacimiento.ToString("yyyy-MM-dd") + "', " + idCargo + ");");
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
            sentencia.Append("UPDATE Empleados SET ");
            sentencia.Append("nombresEmpleado = '" + nombresEmpleado + "', " +
                             "apellidosEmpleado = '" + apellidosEmpleado + "', " +
                             "telefono = '" + telefono + "', " +
                             "direccion = '" + direccion + "', " +
                             "email = '" + email + "', " +
                             "fechaNacimiento = '" + fechaNacimiento.ToString("yyyy-MM-dd") + "', " +
                             "idCargo = " + idCargo);
            sentencia.Append(" WHERE idEmpleados = " + idEmpleados + ";");
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
            sentencia.Append("DELETE FROM Empleados ");
            sentencia.Append("WHERE idEmpleados = " + idEmpleados + ";");
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
