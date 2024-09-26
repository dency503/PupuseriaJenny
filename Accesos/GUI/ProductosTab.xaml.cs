using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Accesos.CLS;

namespace Accesos.GUI
{
    /// <summary>
    /// Lógica de interacción para ProductosTab.xaml
    /// </summary>
    public partial class ProductosTab : UserControl, ITab
    {
        public ObservableCollection<Productos> Items { get; set; } = new ObservableCollection<Productos>();
       // public DataGrid DataGridControl => productosDataGrid;
        private int _paginaActual = 1;
        private const int _tamanoPagina = 10;
        int TotalPaginas;
        public int TotalRegistros { get; set; }

        public ProductosTab()
        {
            InitializeComponent();
            CargarDatos();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var producto = button.DataContext as Productos;

            if (producto != null)
            {
                EditarProductoWindow editarProductoWindow = new EditarProductoWindow();
                editarProductoWindow.txtIDProducto.Text = producto.IDProducto.ToString();
                editarProductoWindow.txtNombreProducto.Text = producto.NombreProducto;
                editarProductoWindow.txtCostoUnitario.Text = producto.CostoUnitario.ToString();
                editarProductoWindow.txtPrecio.Text = producto.Precio.ToString();
                editarProductoWindow.txtCantidad.Text = producto.CantidadProducto.ToString();
                editarProductoWindow.cmbCategoria.SelectedValue = producto.IDCategoria;
                editarProductoWindow.cmbProveedor.SelectedValue = producto.IDProveedor;
                editarProductoWindow.ShowDialog();

                CargarDatos();
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var producto = button.DataContext as Productos;

            if (producto != null)
            {
                // Confirmar eliminación
                MessageBoxResult result = MessageBox.Show($"¿Estás seguro de que deseas eliminar {producto.NombreProducto}?", "Confirmar eliminación", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    CLS.Productos oProducto = new CLS.Productos();
                    oProducto.IDProducto = Convert.ToInt32(producto.IDProducto);
                    if (oProducto.Eliminar())
                    {
                        Items.Remove(producto); // Eliminar de la colección ObservableCollection
                        MessageBox.Show($"Producto {producto.NombreProducto} eliminado.");
                    }
                    else
                    {
                        MessageBox.Show($"Error al eliminar el producto {producto.NombreProducto}.");
                    }
                }
            }
        }

        private void CargarDatos()
        {
            try
            {
                var cargarProductos = new CargarDatos<Productos>(_paginaActual, _tamanoPagina);
                cargarProductos.Cargar(
                    DataLayer.Consultas.ContarProductos,
                    DataLayer.Consultas.PRODUCTOS,
                    (row, items) =>
                    {
                        items.Add(new Productos
                        {
                            IDProducto = Convert.ToInt32(row["idProducto"]), // Asegúrate de que el nombre de la columna sea correcto
                            NombreProducto = row["nombreProducto"].ToString(), // Asegúrate de que el nombre de la columna sea correcto
                            CostoUnitario = Convert.ToDouble(row["costoUnitario"]), // Usar Decimal si es necesario
                            Precio = Convert.ToDouble(row["precio"]), // Usar Decimal si es necesario
                            CantidadProducto = Convert.ToInt32(row["cantidadProducto"]), // Asegúrate de que el nombre de la columna sea correcto
                            IDCategoria = Convert.ToInt32(row["idCategoria"]), // Asegúrate de que el nombre de la columna sea correcto
                            IDProveedor = Convert.ToInt32(row["idProveedor"]) // Manejo de valores nulos
                        });
                        MessageBox.Show(row["nombreProducto"].ToString());
                    },
                    usersDataGrid
                );

                TotalRegistros = cargarProductos.TotalRegistros;
                Items = cargarProductos.Items;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}");
            }
        }
    }
}
