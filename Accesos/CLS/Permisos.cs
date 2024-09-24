using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accesos.CLS
{
    public class Permisos
    {

        Int32 _IDPermiso;
        Int32 _IDRol;
        Int32 _IDOpcion;

        public int IDPermiso { get => _IDPermiso; set => _IDPermiso = value; }
        public int IDRol { get => _IDRol; set => _IDRol = value; }
        public int IDOpcion { get => _IDOpcion; set => _IDOpcion = value; }

        public Boolean Insertar()
        {
            Boolean Resultado = false;
            DBOperacion Operacion = new DBOperacion();
            StringBuilder Sentencia;

            try
            {

                Sentencia = new StringBuilder();
                Sentencia.Append("INSERT INTO permisos (IDRol, IDOpcion) VALUES(");
                Sentencia.Append("'" + IDRol + "','" + _IDOpcion + "');");

                if (Operacion.EjecutarSentencia(Sentencia.ToString()) > 0)
                {
                    Resultado = true;
                }
                else
                {
                    Resultado = false;

                }

            }
            catch (Exception ex)
            {
                // Manejar la excepción según sea necesario (por ejemplo, loguear el error)
                throw new Exception("Error al insertar permisos: " + ex.Message);
            }

            return Resultado;
        }


        public Boolean Actualizar()
        {
            Boolean Resultado = false;
            DBOperacion Operacion = new DBOperacion();
            StringBuilder Sentencia = new StringBuilder();

            Sentencia.Append("UPDATE permisos SET ");
            Sentencia.Append("IDRol = '" + _IDRol + "'," +
                             "IDOpcion = '" + _IDOpcion + "' ");
            Sentencia.Append("WHERE IDPermiso = " + _IDPermiso + "; ");
            try
            {
                if (Operacion.EjecutarSentencia(Sentencia.ToString()) >= 0)
                {
                    Resultado = true;
                }
                else
                {
                    Resultado = false;
                }
            }
            catch (Exception)
            {
                Resultado = false;
            }




            return Resultado;
        }

        public Boolean Eliminar()
        {
            Boolean Resultado = false;
            DBOperacion Operacion = new DBOperacion();
            StringBuilder Sentencia = new StringBuilder();
            Sentencia.Append("DELETE FROM permisos ");
            Sentencia.Append("WHERE IDPermiso = " + IDPermiso + ";");
            try
            {
                if (Operacion.EjecutarSentencia(Sentencia.ToString()) >= 0)
                {
                    Resultado = true;
                }
                else
                {
                    Resultado = false;
                }
            }
            catch (Exception)
            {
                Resultado = false;
            }
            return Resultado;
        }
    }
}
