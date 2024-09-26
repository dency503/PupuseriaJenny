using DataLayer;
using System;
using System.Text;

namespace Accesos.CLS
{
    public class Categorias
    {
        Int32 _IDCategoria;
        string _Categoria;

        // Propiedades
        public int IDCategoria { get => _IDCategoria; set => _IDCategoria = value; }
        public string Categoria { get => _Categoria; set => _Categoria = value; }

        // Método para insertar una nueva categoría
        public Boolean Insertar()
        {
            Boolean Resultado = false;
            DBOperacion Operacion = new DBOperacion();
            StringBuilder Sentencia = new StringBuilder();
            Sentencia.Append("INSERT INTO categorias(categoria) VALUES(");
            Sentencia.Append("'" + Categoria + "');");
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

        // Método para actualizar una categoría existente
        public Boolean Actualizar()
        {
            Boolean Resultado = false;
            DBOperacion Operacion = new DBOperacion();
            StringBuilder Sentencia = new StringBuilder();
            Sentencia.Append("UPDATE categorias SET ");
            Sentencia.Append("categoria = '" + Categoria + "' ");
            Sentencia.Append("WHERE idCategoria = " + IDCategoria + ";");
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

        // Método para eliminar una categoría
        public Boolean Eliminar()
        {
            Boolean Resultado = false;
            DBOperacion Operacion = new DBOperacion();
            StringBuilder Sentencia = new StringBuilder();
            Sentencia.Append("DELETE FROM categorias ");
            Sentencia.Append("WHERE idCategoria = " + IDCategoria + ";");
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
