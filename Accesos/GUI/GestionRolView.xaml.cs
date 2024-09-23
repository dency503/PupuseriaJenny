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
    /// Lógica de interacción para GestionRolView.xaml
    /// </summary>
    public partial class GestionRolView : UserControl
    {
        private ObservableCollection<Roles> _roles = new ObservableCollection<Roles>();
        private int _paginaActual = 1;
        private const int _tamanoPagina = 10;
        int totalPaginas;
        public GestionRolView()
        {
            InitializeComponent();
            CargarDatos();
        }
        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            FiltrarLocalmente();
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // Obtener el usuario seleccionado
            var button = sender as Button;
            var usuario = button.DataContext as Usuarios;


        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var usuario = button.DataContext as Usuarios;


        }

        private void CargarDatos()
        {
            try
            {
                // Obtener los datos y cargarlos en la colección ObservableCollection
                int totalUsuarios = DataLayer.Consultas.ContarRoles();


                // Calcular el total de páginas
                totalPaginas = (int)Math.Ceiling((double)totalUsuarios / _tamanoPagina);

                GenerarBotonesPaginacion(totalPaginas, _paginaActual);
                DataTable dt = DataLayer.Consultas.ROLES(_paginaActual, _tamanoPagina);

                FiltrarLocalmente();
                _roles.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    _roles.Add(new Roles
                    {
                        IDRol = Convert.ToInt32(row["IDRol"]),
                        Rol = row["Rol"].ToString()



                    });

                }
                rolesDataGrid.ItemsSource = _roles;
                lbRegistros.Text = totalUsuarios.ToString() + " Roles encontrado(s)";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}");
            }
        }
        private void CambiarPagina(int nuevaPagina)
        {
            if (nuevaPagina < 1 || nuevaPagina > totalPaginas)
                return; // Evitar cambios fuera de rango

            _paginaActual = nuevaPagina;
            CargarDatos(); // Método que carga los datos según la página actual
            GenerarBotonesPaginacion(totalPaginas, _paginaActual); // Regenerar botones
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
        private void FiltrarLocalmente()
        {
            try
            {
                string filter = txtFilter.Text.Trim().ToLower();
                if (string.IsNullOrWhiteSpace(filter))
                {
                    rolesDataGrid.ItemsSource = _roles;
                }
                else
                {
                    rolesDataGrid.ItemsSource = new ObservableCollection<Roles>(
                        _roles.Where(u => u.Rol.ToLower().Contains(filter))
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al filtrar datos: {ex.Message}");
            }
        }

    }
}
