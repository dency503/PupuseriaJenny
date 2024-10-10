using System;
using System.Collections.Generic; // Para usar el diccionario
using System.Data.SqlClient; // Asegúrate de tener la referencia adecuada
using System.Text;
using DataLayer; // Asumiendo que tienes un espacio de nombres DataLayer con la clase DBOperacion

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
            sentencia.Append("INSERT INTO Empleados(nombresEmpleado, apellidosEmpleado, telefono, direccion, email, fechaNacimiento, idCargo) VALUES(@nombresEmpleado, @apellidosEmpleado, @telefono, @direccion, @email, @fechaNacimiento, @idCargo);");

            try
            {
                var parametros = new Dictionary<string, object>
                {
                    { "@nombresEmpleado", nombresEmpleado },
                    { "@apellidosEmpleado", apellidosEmpleado },
                    { "@telefono", telefono },
                    { "@direccion", direccion },
                    { "@email", email },
                    { "@fechaNacimiento", fechaNacimiento.ToString("yyyy-MM-dd") },
                    { "@idCargo", idCargo }
                };

                if (operacion.EjecutarSentencia(sentencia.ToString(), parametros) >= 0)
                {
                    resultado = true;
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
            sentencia.Append("nombresEmpleado = @nombresEmpleado, " +
                             "apellidosEmpleado = @apellidosEmpleado, " +
                             "telefono = @telefono, " +
                             "direccion = @direccion, " +
                             "email = @email, " +
                             "fechaNacimiento = @fechaNacimiento, " +
                             "idCargo = @idCargo ");
            sentencia.Append("WHERE idEmpleados = @idEmpleados;");

            try
            {
                var parametros = new Dictionary<string, object>
                {
                    { "@nombresEmpleado", nombresEmpleado },
                    { "@apellidosEmpleado", apellidosEmpleado },
                    { "@telefono", telefono },
                    { "@direccion", direccion },
                    { "@email", email },
                    { "@fechaNacimiento", fechaNacimiento.ToString("yyyy-MM-dd") },
                    { "@idCargo", idCargo },
                    { "@idEmpleados", idEmpleados }
                };

                if (operacion.EjecutarSentencia(sentencia.ToString(), parametros) >= 0)
                {
                    resultado = true;
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
            sentencia.Append("DELETE FROM Empleados WHERE idEmpleados = @idEmpleados;");

            try
            {
                var parametros = new Dictionary<string, object>
                {
                    { "@idEmpleados", idEmpleados }
                };

                if (operacion.EjecutarSentencia(sentencia.ToString(), parametros) >= 0)
                {
                    resultado = true;
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
