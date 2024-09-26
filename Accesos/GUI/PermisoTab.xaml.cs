using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Accesos.CLS;

namespace Accesos.GUI
{
    /// <summary>
    /// Lógica de interacción para PermisoTab.xaml
    /// </summary>
    public partial class PermisoTab : UserControl, ITab
    {
        public ObservableCollection<Permisos> Items { get; set; } = new ObservableCollection<Permisos>();
        public DataGrid DataGridControl => usersDataGrid;
        private int _paginaActual = 1;
        private const int _tamanoPagina = 10;
        int TotalPaginas;
        public int TotalRegistros { get; set; }

        public PermisoTab()
        {
            InitializeComponent();
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var opciones = button.DataContext as Permisos;

            if (opciones != null)
            {
                EditarPermisoWindow editarPermisoWindow = new EditarPermisoWindow();
                editarPermisoWindow.txtIDPermiso.Text = opciones.IDPermiso.ToString();
                editarPermisoWindow.cmbOpcion.SelectedValue = opciones.IDOpcion.ToString();
                editarPermisoWindow.cmbRol.SelectedValue = opciones.IDRol.ToString();

                editarPermisoWindow.ShowDialog();

                CargarDatos();
                // Aquí puedes abrir una ventana de edición o realizar otras acciones
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var permiso = button.DataContext as Permisos;

            if (permiso != null)
            {
                // Confirmar eliminación
                MessageBoxResult result = MessageBox.Show($"¿Estás seguro de que deseas eliminar a {permiso.IDPermiso}?", "Confirmar eliminación", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {

                    CLS.Permisos oPermiso = new CLS.Permisos();
                    oPermiso.IDPermiso = Convert.ToInt32(permiso.IDPermiso);
                    if (oPermiso.Eliminar())
                    {
                        Items.Remove(permiso); // Eliminar de la colección ObservableCollection
                        MessageBox.Show($"Usuario {permiso.IDPermiso} eliminado.");
                    }
                    else
                    {
                        MessageBox.Show($"Error al eliminar el usuario {permiso.IDPermiso}.");
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
                var cargarRoles = new CargarDatos<Permisos>(_paginaActual, _tamanoPagina);
                cargarRoles.Cargar(
                    DataLayer.Consultas.ContarPermisos,
                    DataLayer.Consultas.Permisos,
                    (row, items) =>
                    {
                        items.Add(new Permisos
                        {
                            IDOpcion = Convert.ToInt32(row["IDOpcion"]),
                            IDPermiso = Convert.ToInt32(row["IDPermiso"])


                        });

                    },
                    usersDataGrid
                );
                TotalRegistros = cargarRoles.TotalRegistros;
                Items = cargarRoles.Items;








            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}");
            }
        }

    }
}
