using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Accesos.CLS;

namespace Accesos.GUI
{
    /// <summary>
    /// Lógica de interacción para UsuariosTab.xaml
    /// </summary>
    public partial class UsuariosTab : UserControl, ITab
    {
        public ObservableCollection<Usuarios> Items { get; set; } = new ObservableCollection<Usuarios>();
        public DataGrid DataGridControl => usersDataGrid;
        private int _paginaActual = 1;
        private const int _tamanoPagina = 10;
        int TotalPaginas;
        public int TotalRegistros { get; set; }
        public UsuariosTab()
        {
            InitializeComponent();
            CargarDatos();

        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var usuario = button.DataContext as Usuarios;

            if (usuario != null)
            {
                EditarUsuarioWindow editarUsuarioWindow = new EditarUsuarioWindow();
                editarUsuarioWindow.txtIDUsuario.Text = usuario.IDUsuario.ToString();
                editarUsuarioWindow.txtUsuario.Text = usuario.Usuario.ToString();
                editarUsuarioWindow.cmbEmpleado.SelectedValue = usuario.IDEmpleado;
                editarUsuarioWindow.cmbRol.SelectedValue = usuario.IDRol;
                editarUsuarioWindow.txtContraseña.Password = usuario.Contraseña;
                editarUsuarioWindow.ShowDialog();

                CargarDatos();
                // Aquí puedes abrir una ventana de edición o realizar otras acciones
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var usuario = button.DataContext as Usuarios;

            if (usuario != null)
            {
                // Confirmar eliminación
                MessageBoxResult result = MessageBox.Show($"¿Estás seguro de que deseas eliminar a {usuario.Usuario}?", "Confirmar eliminación", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {

                    CLS.Usuarios oUsuario = new CLS.Usuarios();
                    oUsuario.IDUsuario = Convert.ToInt32(usuario.IDUsuario);
                    if (oUsuario.Eliminar())
                    {
                        Items.Remove(usuario); // Eliminar de la colección ObservableCollection
                        MessageBox.Show($"Usuario {usuario.Usuario} eliminado.");
                    }
                    else
                    {
                        MessageBox.Show($"Error al eliminar el usuario {usuario.Usuario}.");
                    }
                }
            }
        }
        private void CargarDatos()
        {
            try
            {


                /*  CargarDatosHelper.CargarUsuarios(_paginaActual, _tamanoPagina
                      , usersDataGrid);*/
                var cargarDatos = new CargarDatos<Usuarios>(_paginaActual, _tamanoPagina);
                cargarDatos.Cargar(
                    DataLayer.Consultas.ContarUsuarios,
                    DataLayer.Consultas.USUARIOS,
                   (row, items) =>
                   {
                       items.Add(new Usuarios
                       {
                           IDEmpleado = Convert.ToInt32(row["idEmpleado"]), // Asegúrate de que las claves coincidan con las de la base de datos
                           IDUsuario = Convert.ToInt32(row["idUsuario"]),
                           Usuario = row["Usuario"].ToString(),
                           Contraseña = row["Contrasenia"].ToString(), // Considera no exponer la contraseña directamente
                           IDRol = Convert.ToInt32(row["idRol"])
                       });
                   },

                    usersDataGrid
                );
                
                TotalRegistros = cargarDatos.TotalRegistros;
           
                Items = cargarDatos.Items;








            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}");
            }
        }
    }
}
