using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Accesos.CLS;

namespace Accesos.GUI
{
    /// <summary>
    /// Lógica de interacción para CategoriasTab.xaml
    /// </summary>
    public partial class CategoriasTab : UserControl, ITab
    {
        public ObservableCollection<Categorias> Items { get; set; } = new ObservableCollection<Categorias>();
        public DataGrid DataGridControl => categoriasDataGrid;
        private int _paginaActual = 1;
        private const int _tamanoPagina = 10;
        int TotalPaginas;
        public int TotalRegistros { get; set; }

        public CategoriasTab()
        {
            InitializeComponent();
            CargarDatos();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var categoria = button.DataContext as Categorias;

            if (categoria != null)
            {
                EditarCategoriaWindow editarCategoriaWindow = new EditarCategoriaWindow();
                editarCategoriaWindow.txtIDCategoria.Text = categoria.IDCategoria.ToString();
                editarCategoriaWindow.txtNombreCategoria.Text = categoria.Categoria.ToString();
                editarCategoriaWindow.ShowDialog();

                CargarDatos();
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var categoria = button.DataContext as Categorias;

            if (categoria != null)
            {
                MessageBoxResult result = MessageBox.Show($"¿Estás seguro de que deseas eliminar la categoría {categoria.Categoria}?", "Confirmar eliminación", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    CLS.Categorias oCategoria = new CLS.Categorias();
                    oCategoria.IDCategoria = Convert.ToInt32(categoria.IDCategoria);
                    if (oCategoria.Eliminar())
                    {
                        Items.Remove(categoria); // Eliminar de la colección
                        MessageBox.Show($"Categoría {categoria.Categoria} eliminada.");
                    }
                    else
                    {
                        MessageBox.Show($"Error al eliminar la categoría {categoria.Categoria}.");
                    }
                }
            }
        }

        private void CargarDatos()
        {
            try
            {
                var cargarCategorias = new CargarDatos<Categorias>(_paginaActual, _tamanoPagina);
                cargarCategorias.Cargar(
                    DataLayer.Consultas.ContarCategorias,
                    DataLayer.Consultas.CATEGORIAS,
                    (row, items) =>
                    {
                        items.Add(new Categorias
                        {
                            IDCategoria = Convert.ToInt32(row["IDCategoria"]),
                            Categoria = row["Categoria"].ToString()
                        });
                    },
                    categoriasDataGrid
                );
                TotalRegistros = cargarCategorias.TotalRegistros;
                Items = cargarCategorias.Items;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}");
            }
        }
    }
}
