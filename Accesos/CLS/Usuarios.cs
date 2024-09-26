using System.Data;
using System.Security.Cryptography;
using System.Text;
using DataLayer;

namespace Accesos.CLS
{
    public class Usuarios
    {
        private int _IDUsuario;
        private string _Usuario;
        private string _Contraseña;
        private int _IDEmpleado;
        private int _IDRol;

        public int IDUsuario { get => _IDUsuario; set => _IDUsuario = value; }
        public string Usuario { get => _Usuario; set => _Usuario = value; }
        public string Contraseña
        {
            get => _Contraseña;
            set => _Contraseña = ConvertirContrasenia(value); // Hashing al establecer la contraseña
        }
        public int IDEmpleado { get => _IDEmpleado; set => _IDEmpleado = value; }
        public int IDRol { get => _IDRol; set => _IDRol = value; }

        public bool Insertar()
        {
            var operacion = new DBOperacion();
            var sentencia = $"INSERT INTO Usuario(Usuario, Contrasenia, idEmpleado, idRol) VALUES('{Usuario}', '{Contraseña}', {IDEmpleado}, {IDRol});";
            return EjecutarSentencia(operacion, sentencia);
        }

        public bool Actualizar()
        {
            var operacion = new DBOperacion();
            var sentencia = $"UPDATE Usuario SET Usuario = '{Usuario}', Contrasenia = '{Contraseña}', idEmpleado = {IDEmpleado}, idRol = {IDRol} WHERE idUsuario = {IDUsuario};";
            return EjecutarSentencia(operacion, sentencia);
        }

        public bool Eliminar()
        {
            var operacion = new DBOperacion();
            var sentencia = $"DELETE FROM Usuario WHERE idUsuario = {IDUsuario};";
            return EjecutarSentencia(operacion, sentencia);
        }

        private bool EjecutarSentencia(DBOperacion operacion, string sentencia)
        {
            try
            {
                return operacion.EjecutarSentencia(sentencia) >= 0;
            }
            catch (Exception)
            {
                // Manejo de excepciones si es necesario
                return false;
            }
        }

        public static string ConvertirContrasenia(string contrasenia)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] convertirBytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(contrasenia));
                var convertirCadena = new StringBuilder();
                foreach (var byteValue in convertirBytes)
                {
                    convertirCadena.Append(byteValue.ToString("x2"));
                }
                return convertirCadena.ToString();
            }
        }

        public static bool UsuarioExiste(string oUsuario)
        {
            var operacion = new DBOperacion();
            string consulta = $"SELECT Usuario FROM Usuario WHERE Usuario = '{oUsuario}';";
            DataTable resultado;
            try
            {
                resultado = operacion.Consultar(consulta);
                return resultado.Rows.Count > 0;
            }
            catch (Exception)
            {
                // Manejo de excepciones si es necesario
                return false;
            }
        }
    }
}
