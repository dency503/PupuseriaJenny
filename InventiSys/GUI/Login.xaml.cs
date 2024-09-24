using System.Data;
using System.Windows;
using Modelos;

namespace InventiSys.GUI
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private bool _autorizado;
        public bool Autorizado
        {
            get => _autorizado;
            private set => _autorizado = value;
        }

        public Login()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {

            Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string usuario = tbUsuario.Text.Trim();
            string contraseña = tbContraseña.Password.Trim();

            try
            {
                DataTable dt = AutenticarUsuario(usuario, contraseña);

                if (dt.Rows.Count == 1)
                {
                    ConfigurarSesion(dt.Rows[0]);
                    Autorizado = true;

                    Close();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.", "Error de Autenticación", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al intentar iniciar sesión: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private DataTable AutenticarUsuario(string usuario, string contraseña)
        {
            string query = @"
                SELECT 
                    u.IDUsuario, u.Usuario, 
                    e.IDEmpleado, e.Nombre, e.Cargo, e.Telefono, e.Email
                FROM 
                    usuarios u
                INNER JOIN 
                    empleados e ON u.IDEmpleado = e.IDEmpleado
                WHERE 
                    u.Usuario = @Usuario AND 
                    u.Contraseña = @Contraseña;";

            var oOperacion = new DataLayer.DBOperacion();
            var parameters = new Dictionary<string, object>
    {
        { "@Usuario", usuario },
        { "@Contraseña", contraseña }
    };

            try
            {
                // Llamar al método Consultar y obtener el DataTable
                return oOperacion.Consultar(query, parameters);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones: mostrar un mensaje de error o loguear el error
                MessageBox.Show($"Error al consultar la base de datos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new DataTable(); // Retornar un DataTable vacío en caso de error
            }
        }

        private void ConfigurarSesion(DataRow row)
        {
            SesionManager.Sesion oSesion = SesionManager.Sesion.ObtenerInstancia();
            oSesion.Usuario = tbUsuario.Text;
            oSesion.Contraseña = tbContraseña.Password;
            oSesion.empleado = new Empleado
            {
                IDEmpleado = Convert.ToInt32(row["IDEmpleado"]),
                Nombre = row["Nombre"].ToString(),
                Cargo = row["Cargo"].ToString(),
                Telefono = row["Telefono"].ToString(),
                Email = row["Email"].ToString()
            };
        }
    }
}
