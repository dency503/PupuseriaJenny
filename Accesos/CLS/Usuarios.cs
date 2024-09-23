using System.Data;
using System.Security.Cryptography;
using System.Text;
using DataLayer;

namespace Accesos.CLS
{
    public class Usuarios
    {
        Int32 _IDUsuario;
        string _Usuario;
        string _Contraseña;
        Int32 _IDEmpleado;
        Int32 _IDRol;

        public int IDUsuario { get => _IDUsuario; set => _IDUsuario = value; }
        public string Usuario { get => _Usuario; set => _Usuario = value; }
        public string Contraseña { get => _Contraseña; set => _Contraseña = value; }
        public int IDEmpleado { get => _IDEmpleado; set => _IDEmpleado = value; }
        public int IDRol { get => _IDRol; set => _IDRol = value; }

        public Boolean Insertar()
        {
            Boolean Resultado = false;
            DataLayer.DBOperacion Operacion = new DataLayer.DBOperacion();
            StringBuilder Sentencia = new StringBuilder(); // objeto para construir cadenas complejas
            Sentencia.Append("INSERT INTO usuarios(Usuario, Contraseña, IDEmpleado, IDRol) VALUES(");
            Sentencia.Append("'" + Usuario + "', ");
            Sentencia.Append("'" + Usuarios.ConvertirContraseña(Contraseña) + "', ");
            Sentencia.Append("'" + IDEmpleado + "', ");
            Sentencia.Append("'" + IDRol + "');");
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
        public Boolean Actualizar()
        {
            Boolean Resultado = false;
            DataLayer.DBOperacion Operacion = new DataLayer.DBOperacion();
            StringBuilder Sentencia = new StringBuilder(); // objeto para construir cadenas complejas
            Sentencia.Append("UPDATE usuarios SET ");
            Sentencia.Append("Usuario = '" + _Usuario + "', ");
            Sentencia.Append("Contraseña = '" + Usuarios.ConvertirContraseña(_Contraseña) + "', ");
            Sentencia.Append("IDEmpleado = '" + _IDEmpleado + "', ");
            Sentencia.Append("IDRol = '" + _IDRol + "' ");
            Sentencia.Append("WHERE IDUsuario = " + _IDUsuario + "; ");
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
            DataLayer.DBOperacion Operacion = new DataLayer.DBOperacion(); // Agregar referencias= Referencias-Agregar referencia
            StringBuilder Sentencia = new StringBuilder(); // Objeto para construir cadenas complejas
            Sentencia.Append("DELETE FROM usuarios ");
            Sentencia.Append("WHERE IDUsuario = " + _IDUsuario + ";");
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
        public static string ConvertirContraseña(string cContraseña)
        {
            using (SHA256 sha256Hash = SHA256.Create()) // 'using' asegura que el objeto SHA256 se libere. SHA256.Create() crea una instancia.
            {
                byte[] ConvertirBytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(cContraseña)); // Convierte la cadena en un arreglo de bytes. Procesa esos bytes y devuelve un arreglo de bytes.

                StringBuilder convertirCadena = new StringBuilder();
                for (int i = 0; i < ConvertirBytes.Length; i++)
                {
                    convertirCadena.Append(ConvertirBytes[i].ToString("x2")); // Convierte cada byte a una cadena hexadecimal usando "x2" que representa los bytes en formato hexadecimal de dos caracteres.
                }
                return convertirCadena.ToString();
            }
        }

        public static bool UsuarioExiste(string oUsuario)
        {
            DataTable Resultado = new DataTable();
            String Consulta = @"SELECT Usuario FROM Usuarios WHERE Usuario = '" + oUsuario + "';";
            DBOperacion operacion = new DBOperacion();
            try
            {
                Resultado = operacion.Consultar(Consulta);
            }
            catch (Exception)
            {

            }
            return Resultado.Rows.Count > 0;
        }
    }
}
