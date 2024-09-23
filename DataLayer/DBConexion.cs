using MySql.Data.MySqlClient;

namespace DataLayer
{
    public class DBConexion
    {
        protected MySqlConnection _CONEXION;
        private const string ServerIP = "localhost"; // IP del servidor MySQL
        private const string Database = "GestionRestauranteDB";
        private const string UserId = "root";
        private const string Password = "root";

        public bool Conectar()
        {
            try
            {
                var connectionString = $"Server={ServerIP};Port=3306;Database={Database};Uid={UserId};Pwd={Password};SSLMode=None";
                _CONEXION = new MySqlConnection(connectionString);
                _CONEXION.Open();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar: {ex.Message}");
                return false;
            }
        }

        public void Desconectar()
        {
            try
            {
                if (_CONEXION != null && _CONEXION.State == System.Data.ConnectionState.Open)
                {
                    _CONEXION.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al desconectar: {ex.Message}");
            }
        }
    }
}
