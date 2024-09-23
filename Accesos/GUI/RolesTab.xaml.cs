using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Accesos.CLS;

namespace Accesos.GUI
{
    /// <summary>
    /// Lógica de interacción para RolesTab.xaml
    /// </summary>
    public partial class RolesTab : UserControl, ITab
    {

        public ObservableCollection<Roles> Items { get; set; } = new ObservableCollection<Roles>();
        public DataGrid DataGridControl => rolesDataGrid;
        private int _paginaActual = 1;
        private const int _tamanoPagina = 10;
        int TotalPaginas;
        public int TotalRegistros { get; set; }
        public RolesTab()
        {
            InitializeComponent();
            Cargar();
        }




        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // Obtener el usuario seleccionado
            var button = sender as Button;
            var rol = button.DataContext as Roles;

            if (rol != null)
            {
                EditarRolWindow editarRolWindow = new EditarRolWindow();
                editarRolWindow.txtIDRol.Text = rol.IDRol.ToString();
                editarRolWindow.txtRol.Text = rol.Rol.ToString();
                editarRolWindow.ShowDialog();

                Cargar();
                // Aquí puedes abrir una ventana de edición o realizar otras acciones
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var rol = button.DataContext as Roles;
            if (rol != null)
            {
                // Confirmar eliminación
                MessageBoxResult result = MessageBox.Show($"¿Estás seguro de que deseas eliminar a {rol.Rol}?", "Confirmar eliminación", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {

                    CLS.Roles oUsuario = new CLS.Roles();
                    oUsuario.IDRol  = Convert.ToInt32(rol.IDRol);
                    if (oUsuario.Eliminar())
                    {
                        Items.Remove(rol); // Eliminar de la colección ObservableCollection
                        MessageBox.Show($"Empleado {rol.Rol} eliminado.");
                    }
                    else
                    {
                        MessageBox.Show($"Error al eliminar el usuario {rol.IDRol}.");
                    }
                }
            }

        }

        private void Cargar()
        {
            try
            {
                //_baseTab.CargarDatos(rolesDataGrid, Items);
                // Obtener los datos y cargarlos en la colección ObservableCollection
                /*TotalRegistros = DataLayer.Consultas.ContarRoles();


                // Calcular el total de páginas
                TotalPaginas = (int)Math.Ceiling((double)TotalRegistros / _tamanoPagina);


                DataTable dt = DataLayer.Consultas.ROLES(_paginaActual, _tamanoPagina);


                Items.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    Items.Add(new Roles
                    {
                        IDRol = Convert.ToInt32(row["IDRol"]),
                        Rol = row["Rol"].ToString()


                        
                    });
                    
                }
                rolesDataGrid.ItemsSource = Items;*/
                var cargarRoles = new CargarDatos<Roles>(_paginaActual, _tamanoPagina);
                cargarRoles.Cargar(
                    DataLayer.Consultas.ContarRoles,
                    DataLayer.Consultas.ROLES,
                    (row, items) =>
                    {
                        items.Add(new Roles
                        {
                            IDRol = Convert.ToInt32(row["IDRol"]),
                            Rol = row["Rol"].ToString()
                        });

                    },
                    rolesDataGrid
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
