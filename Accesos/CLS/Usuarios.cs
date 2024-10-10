using System;
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
            set => _Contraseña = value; // Hashing al establecer la contraseña
        }
        public int IDEmpleado { get => _IDEmpleado; set => _IDEmpleado = value; }
        public int IDRol { get => _IDRol; set => _IDRol = value; }

       public bool Insertar()
{
    var operacion = new DBOperacion();
    var sentencia = "INSERT INTO `Usuario` (`Usuario`, `Contrasenia`, `idRol`, `idEmpleado`) VALUES (@Usuario, @Contrasenia, @idEmpleado, @idRol);";
    
    var parametros = new Dictionary<string, object>
    {
        { "@Usuario", Usuario },
        { "@Contrasenia", Contraseña },  // Se debe almacenar la contraseña hasheada
        { "@idEmpleado", IDEmpleado },
        { "@idRol", IDRol }
    };

    try
    {
        return EjecutarSentencia(operacion, sentencia, parametros);
    }
    catch (Exception ex)
    {
        // Manejo de excepciones si es necesario
        Console.WriteLine($"Error al insertar el usuario: {ex.Message}");
        return false;
    }
}


        public bool Actualizar()
        {
            var operacion = new DBOperacion();
            string sentencia;

            // Si la contraseña está vacía, no se actualiza
            if (string.IsNullOrWhiteSpace(Contraseña))
            {
                sentencia = "UPDATE `usuario` SET `Usuario` = @Usuario, idEmpleado = @idEmpleado, idRol = @idRol WHERE (idUsuario = @idUsuario);";
            }
            else
            {
                sentencia = "UPDATE `usuario` SET `Usuario` = @Usuario, Contrasenia = @Contrasenia, idEmpleado = @idEmpleado, idRol = @idRol WHERE (idUsuario = @idUsuario);";
            }

            var parametros = new Dictionary<string, object>
    {
        { "@Usuario", Usuario },
        { "@idEmpleado", IDEmpleado },
        { "@idRol", IDRol },
        { "@idUsuario", IDUsuario }
    };

            // Agregar la contraseña solo si no está vacía
            if (!string.IsNullOrWhiteSpace(Contraseña))
            {
                parametros.Add("@Contrasenia", Contraseña);
            }

            return EjecutarSentencia(operacion, sentencia, parametros);
        }


        public bool Eliminar()
        {
            var operacion = new DBOperacion();
            var sentencia = "DELETE FROM Usuario WHERE idUsuario = @idUsuario;";
            var parametros = new Dictionary<string, object>
            {
                { "@idUsuario", IDUsuario }
            };
            return EjecutarSentencia(operacion, sentencia, parametros);
        }

        private bool EjecutarSentencia(DBOperacion operacion, string sentencia, Dictionary<string, object> parametros)
        {
            try
            {
                return operacion.EjecutarSentencia(sentencia, parametros) >= 0;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones si es necesario
                Console.WriteLine(ex.ToString());
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
            string consulta = "SELECT Usuario FROM Usuario WHERE Usuario = @Usuario;";
            var parametros = new Dictionary<string, object>
            {
                { "@Usuario", oUsuario }
            };
            DataTable resultado;
            try
            {
                resultado = operacion.Consultar(consulta, parametros);
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
