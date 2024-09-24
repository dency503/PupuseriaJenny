using System.Data;
using System.Security.Cryptography;
using System.Text;
using DataLayer;

namespace Accesos.CLS
{
    public class Usuarios
    {
        int _IDUsuario;
        string _Usuario;
        string _Contraseña;
        int _IDEmpleado;
        int _IDRol;

        public int IDUsuario { get => _IDUsuario; set => _IDUsuario = value; }
        public string Usuario { get => _Usuario; set => _Usuario = value; }
        public string Contraseña { get => _Contraseña; set => _Contraseña = value; }
        public int IDEmpleado { get => _IDEmpleado; set => _IDEmpleado = value; }
        public int IDRol { get => _IDRol; set => _IDRol = value; }

        public bool Insertar()
        {
            bool resultado = false;
            DBOperacion operacion = new DBOperacion();
            StringBuilder sentencia = new StringBuilder();
            sentencia.Append("INSERT INTO Usuario(Usuario, Contrasenia, idEmpleado, idRol) VALUES(");
            sentencia.Append("'" + Usuario + "', ");
            sentencia.Append("'" + Usuarios.ConvertirContrasenia(Contraseña) + "', ");
            sentencia.Append("'" + IDEmpleado + "', ");
            sentencia.Append("'" + IDRol + "');");
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
            sentencia.Append("UPDATE Usuario SET ");
            sentencia.Append("Usuario = '" + _Usuario + "', ");
            sentencia.Append("Contrasenia = '" + Usuarios.ConvertirContrasenia(_Contraseña) + "', ");
            sentencia.Append("idEmpleado = '" + _IDEmpleado + "', ");
            sentencia.Append("idRol = '" + _IDRol + "' ");
            sentencia.Append("WHERE idUsuario = " + _IDUsuario + ";");
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
            sentencia.Append("DELETE FROM Usuario ");
            sentencia.Append("WHERE idUsuario = " + _IDUsuario + ";");
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

        public static string ConvertirContrasenia(string contrasenia)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] convertirBytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(contrasenia));

                StringBuilder convertirCadena = new StringBuilder();
                for (int i = 0; i < convertirBytes.Length; i++)
                {
                    convertirCadena.Append(convertirBytes[i].ToString("x2"));
                }
                return convertirCadena.ToString();
            }
        }

        public static bool UsuarioExiste(string oUsuario)
        {
            DataTable resultado = new DataTable();
            string consulta = @"SELECT Usuario FROM Usuario WHERE Usuario = '" + oUsuario + "';";
            DBOperacion operacion = new DBOperacion();
            try
            {
                resultado = operacion.Consultar(consulta);
            }
            catch (Exception)
            {
                // Manejo de excepciones si es necesario
            }
            return resultado.Rows.Count > 0;
        }
    }
}
