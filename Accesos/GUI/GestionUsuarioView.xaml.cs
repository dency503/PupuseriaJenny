using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Accesos.CLS;
using InventiSys.CLS;
using InventiSys.GUI;
namespace Accesos.GUI
{
    public partial class GestionUsuarioView : UserControl
    {
        private ObservableCollection<Usuarios> _usuarios = new ObservableCollection<Usuarios>();
        private int _paginaActual = 1;
        private const int _tamanoPagina = 10;
        int totalPaginas;
        public GestionUsuarioView()
        {
            InitializeComponent();
            // Configurar el timer para actualizar datos periódicamente


            Cargar();
        }

        private void Cargar()
        {

        }

        private void FiltrarLocalmente()
        {
            try
            {
                string filter = tbFiltro.Text.Trim().ToLower();
                if (string.IsNullOrWhiteSpace(filter))
                {
                    dgUsuarios.ItemsSource = _usuarios;
                }
                else
                {
                    dgUsuarios.ItemsSource = new ObservableCollection<Usuarios>(
                        _usuarios.Where(u => u.Usuario.ToLower().Contains(filter))
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al filtrar datos: {ex.Message}");
            }
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            UsuarioEdicionView usuarioEdicionView = new UsuarioEdicionView();

            NavigationManager.Navigate(usuarioEdicionView, this.Parent as ContentControl);
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Desea editar el registro seleccionado?", "Pregunta", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    UsuarioEdicionView oUsuario = new UsuarioEdicionView();


                    var usuarioEdicion = new UsuarioEdicionView
                    {
                        // Suponiendo que tienes controles similares en UsuariosEdicion
                        txtID = { Text = (dgUsuarios.SelectedItem as DataRowView)?["IDUsuario"].ToString() },
                        txtUsuario = { Text = (dgUsuarios.SelectedItem as DataRowView)?["Usuario"].ToString() },
                        txtContraseña = { Password = (dgUsuarios.SelectedItem as DataRowView)?["Contraseña"].ToString() },

                    };
                    Cargar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Obtener el elemento seleccionado en el DataGrid
                var selectedItem = dgUsuarios.SelectedItem as DataRowView;

                if (selectedItem != null)
                {
                    // Obtener el ID del usuario a eliminar
                    int idUsuario = Convert.ToInt32(selectedItem["IDUsuario"]);

                    // Confirmar la eliminación
                    if (MessageBox.Show($"¿Desea eliminar el registro seleccionado con ID {idUsuario}?", "Pregunta", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        // Crear una instancia del usuario para eliminar
                        var usuario = new CLS.Usuarios
                        {
                            IDUsuario = idUsuario
                        };

                        // Intentar eliminar el usuario
                        if (usuario.Eliminar())
                        {
                            MessageBox.Show("Registro eliminado");

                            // Recargar los datos del DataGrid
                            Cargar();
                        }
                        else
                        {
                            MessageBox.Show("El registro no fue eliminado");
                        }
                    }
                }
                else
                {
                    // Mostrar un mensaje si no se ha seleccionado ningún registro
                    MessageBox.Show("Por favor, selecciona un registro.");
                }
            }
            catch (Exception ex)
            {
                // Mostrar mensaje de error en caso de excepción
                MessageBox.Show($"Error al eliminar usuario: {ex.Message}");
            }
        }


        private void TxtFiltro_TextChanged(object sender, TextChangedEventArgs e)
        {
            FiltrarLocalmente();
        }

        private void BtnEmpleados_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnRoles_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Cronometro_Tick(object sender, EventArgs e)
        {
            try
            {
                Cargar();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar datos: {ex.Message}");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    // Define a class to represent Usuario for binding

}
