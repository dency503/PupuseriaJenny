using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Accesos.CLS;
using MahApps.Metro.IconPacks;

namespace Accesos.GUI
{
    /// <summary>
    /// Lógica de interacción para UsuarioContent.xaml
    /// </summary>
    public partial class UsuarioContent : UserControl, ITab
    {

        UsuariosTab usuariosTab = new UsuariosTab();
        EmpleadoTab empleadoTab = new EmpleadoTab();
        RolesTab rolesTab;
        private int _paginaActual = 1;
        private const int _tamanoPagina = 10;
        public int TotalRegistros { get; set; }
        public UsuarioContent()
        {
            InitializeComponent();
            TabContent.Content = usuariosTab;


            Cargar();
        }
        private void ActualizarVista(ITab tab, string titulo)
        {
            lbRegistros.Text = tab.TotalRegistros.ToString() + $" {titulo} encontrado(s)";
            TotalRegistros = (int)Math.Ceiling((double)tab.TotalRegistros / _tamanoPagina);
            Titulo.Text = titulo;
        }
        private void Cargar()
        {
            ProcesarTabs();
        }
        private void AddUsuario_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EditarUsuarioWindow editarUsuarioWindow = new EditarUsuarioWindow();
                editarUsuarioWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir la ventana de edición: {ex.Message}");
            }
        }

        private void AddRol_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EditarRolWindow editarRolWindow = new EditarRolWindow();
                editarRolWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir la ventana de edición: {ex.Message}");
            }
        }

        private void AddEmpleado_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EditarEmpleadoWindow editarEmpleadoWindow = new EditarEmpleadoWindow();
                editarEmpleadoWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir la ventana de edición: {ex.Message}");
            }
        }
        private void ActualizarVistaYBoton(string tipoTab, Action buttonClickAction, string buttonText, ITab tab)
        {
            // Actualizar la vista correspondiente y el texto del botón
            ActualizarVista(tab, tipoTab);
            textButton.Text = buttonText;



        }

        private void FiltrarContenidoActual(string filter)
        {
            // Filtrar según el contenido activo en el TabControl
            if (TabContent.Content == usuariosTab)
            {
                FiltrarDatos(filter, usuariosTab.usersDataGrid, usuariosTab.Items, u => u.Usuario.ToLower().Contains(filter));
            }
            else if (TabContent.Content == rolesTab)
            {
                FiltrarDatos(filter, rolesTab.rolesDataGrid, rolesTab.Items, u => u.Rol.ToLower().Contains(filter));
            }
            else if (TabContent.Content == empleadoTab)
            {
                FiltrarDatos(filter, empleadoTab.usersDataGrid, empleadoTab.Items, u => u.nombresEmpleado.ToLower().Contains(filter));
            }
        }

        private void ProcesarTabs()
        {
            try
            {
                if (TabContent.Content == usuariosTab)
                {
                    ActualizarVista(usuariosTab, "Usuarios");


                    // Remover manejadores anteriores
                    addUsuarios.Visibility = Visibility.Visible;
                    addRol.Visibility = Visibility.Collapsed;
                    addEmpleado.Visibility = Visibility.Collapsed;
                }
                else if (TabContent.Content == rolesTab)
                {
                    ActualizarVista(rolesTab, "Roles");


                    // Remover manejadores anteriores

                    addUsuarios.Visibility = Visibility.Collapsed;
                    addRol.Visibility = Visibility.Visible;
                    addEmpleado.Visibility = Visibility.Collapsed;
                }
                else if (TabContent.Content == empleadoTab)
                {
                    ActualizarVista(empleadoTab, "Empleados");

                    addUsuarios.Visibility = Visibility.Collapsed;
                    addRol.Visibility = Visibility.Collapsed;
                    addEmpleado.Visibility = Visibility.Visible;
                }



                // Generar botones de paginación
                GenerarBotonesPaginacion(TotalRegistros, _paginaActual);

                // Filtrar datos según el contenido activo
                string filter = txtFilter.Text.Trim().ToLower();
                FiltrarContenidoActual(filter);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}");
            }
        }

        public void FiltrarDatos<T>(string filtro, DataGrid dataGrid, ObservableCollection<T> items, Func<T, bool> filtroPredicado)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filtro))
                {
                    dataGrid.ItemsSource = items;
                }
                else
                {
                    dataGrid.ItemsSource = new ObservableCollection<T>(items.Where(filtroPredicado));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al filtrar datos: {ex.Message}");
            }
        }

        private void GenerarBotonesPaginacion(int totalPaginas, int paginaActual)
        {
            // Limpiar botones existentes
            paginacion.Children.Clear();

            // Agregar botón de "anterior"
            if (paginaActual > 1)
            {
                var btnAnterior = new Button
                {
                    Style = (Style)FindResource("pagingButton"),
                    Content = new PackIconMaterial { Kind = PackIconMaterialKind.ChevronLeft }
                };
                btnAnterior.Click += (s, e) => CambiarPagina(paginaActual - 1);
                paginacion.Children.Add(btnAnterior);
            }

            // Generar botones de páginas
            for (int i = 1; i <= totalPaginas; i++)
            {
                var btnPagina = new Button
                {
                    Style = (Style)FindResource("pagingButton"),
                    Content = i.ToString(),
                    Background = (i == paginaActual) ? new SolidColorBrush(Color.FromRgb(121, 80, 242)) : Brushes.Transparent,
                    Foreground = (i == paginaActual) ? Brushes.White : Brushes.Black
                };

                // Usar una variable local para el clic
                int pagina = i;
                btnPagina.Click += (s, e) => CambiarPagina(pagina);
                paginacion.Children.Add(btnPagina);
            }

            // Agregar botón de "siguiente"
            if (paginaActual < totalPaginas)
            {
                var btnSiguiente = new Button
                {
                    Style = (Style)FindResource("pagingButton"),
                    Content = new PackIconMaterial { Kind = PackIconMaterialKind.ChevronRight }
                };
                btnSiguiente.Click += (s, e) => CambiarPagina(paginaActual + 1);
                paginacion.Children.Add(btnSiguiente);
            }
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // Obtener el usuario seleccionado
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

                Cargar();
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
                        usuariosTab.Items.Remove(usuario); // Eliminar de la colección ObservableCollection
                        MessageBox.Show($"Usuario {usuario.Usuario} eliminado.");
                    }
                    else
                    {
                        MessageBox.Show($"Error al eliminar el usuario {usuario.Usuario}.");
                    }
                }
            }
        }


        private void CambiarPagina(int nuevaPagina)
        {
            if (nuevaPagina < 1 || nuevaPagina > TotalRegistros)
                return; // Evitar cambios fuera de rango

            _paginaActual = nuevaPagina;
            Cargar(); // Método que carga los datos según la página actual
            GenerarBotonesPaginacion(TotalRegistros, _paginaActual); // Regenerar botones
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filter = txtFilter.Text.Trim().ToLower();
            if (TabContent.Content == usuariosTab)
            {
                FiltrarDatos(filter, usuariosTab.usersDataGrid, usuariosTab.Items, u => u.Usuario.ToLower().Contains(filter));
            }
            if (TabContent.Content == rolesTab)
            {
                FiltrarDatos(filter, rolesTab.rolesDataGrid, rolesTab.Items, u => u.Rol.ToLower().Contains(filter));
            }
            if (TabContent.Content == empleadoTab)
            {
                FiltrarDatos(filter, empleadoTab.usersDataGrid, empleadoTab.Items, u => u.nombresEmpleado.ToLower().Contains(filter));

            }

        }

        private void rolesButton_Click(object sender, RoutedEventArgs e)
        {
            if (rolesTab == null)
            {
                rolesTab = new RolesTab();
            }
            TabContent.Content = rolesTab;

            Cargar();


        }

        private void UsuariosButton_Click(object sender, RoutedEventArgs e)
        {
            if (usuariosTab == null)
            {
                usuariosTab = new UsuariosTab();
            }
            TabContent.Content = usuariosTab;
            Cargar();
        }

        private void EmpleadosButton_Click(object sender, RoutedEventArgs e)
        {
            //empleadoTab = ;
            if (empleadoTab == null)
            {
                empleadoTab = new EmpleadoTab();
            }
            TabContent.Content = empleadoTab;
            Cargar();
        }
    }
}
