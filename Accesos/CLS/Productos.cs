using System.Text;

namespace Accesos.CLS
{
    public class Productos
    {
        // Propiedades que coinciden con las columnas de la tabla
        Int32 _IDProducto;
        string _NombreProducto;
        double _CostoUnitario;
        double _Precio;
        Int32 _CantidadProducto;
        Int32 _IDCategoria;
        Int32 _IDProveedor;

        // Getters y Setters
        public int IDProducto { get => _IDProducto; set => _IDProducto = value; }
        public string NombreProducto { get => _NombreProducto; set => _NombreProducto = value; }
        public double CostoUnitario { get => _CostoUnitario; set => _CostoUnitario = value; }
        public double Precio { get => _Precio; set => _Precio = value; }
        public int CantidadProducto { get => _CantidadProducto; set => _CantidadProducto = value; }
        public int IDCategoria { get => _IDCategoria; set => _IDCategoria = value; }
        public int IDProveedor { get => _IDProveedor; set => _IDProveedor = value; }

        // Método para insertar un nuevo producto
        public Boolean Insertar()
        {
            Boolean Resultado = false;
            DataLayer.DBOperacion Operacion = new DataLayer.DBOperacion();
            StringBuilder Sentencia = new StringBuilder();
            Sentencia.Append("INSERT INTO productos(nombreProducto, costoUnitario, precio, cantidadProducto, idCategoria, idProveedor) VALUES(");
            Sentencia.Append("'" + NombreProducto + "', '" + CostoUnitario + "', '" + Precio + "', '" + CantidadProducto + "', '" + IDCategoria + "', '" + IDProveedor + "');");
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

        // Método para actualizar un producto existente
        public Boolean Actualizar()
        {
            Boolean Resultado = false;
            DataLayer.DBOperacion Operacion = new DataLayer.DBOperacion();
            StringBuilder Sentencia = new StringBuilder();
            Sentencia.Append("UPDATE productos SET ");
            Sentencia.Append("nombreProducto = '" + NombreProducto + "', " +
                             "costoUnitario = '" + CostoUnitario + "', " +
                             "precio = '" + Precio + "', " +
                             "cantidadProducto = '" + CantidadProducto + "', " +
                             "idCategoria = '" + IDCategoria + "', " +
                             "idProveedor = '" + IDProveedor + "' ");
            Sentencia.Append("WHERE idProducto = " + IDProducto + ";");
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

        // Método para eliminar un producto
        public Boolean Eliminar()
        {
            Boolean Resultado = false;
            DataLayer.DBOperacion Operacion = new DataLayer.DBOperacion();
            StringBuilder Sentencia = new StringBuilder();
            Sentencia.Append("DELETE FROM productos ");
            Sentencia.Append("WHERE idProducto = " + IDProducto + ";");
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
