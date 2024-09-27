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
    /// Lógica de interacción para CategoriasView.xaml
    /// </summary>
    public partial class CategoriasView : UserControl, ITab
    {
        private CategoriasTab categoriasTab;
        private int _paginaActual = 1;
        private const int _tamanoPagina = 10;
        public int TotalRegistros { get; set; }

        public CategoriasView()
        {
            InitializeComponent();
            categoriasTab = new CategoriasTab();
            TabContent.Content = categoriasTab;

            Cargar();
        }

        private void ActualizarVista(string titulo)
        {
            lbRegistros.Text = categoriasTab.TotalRegistros.ToString() + $" {titulo} encontrado(s)";
            TotalRegistros = (int)Math.Ceiling((double)categoriasTab.TotalRegistros / _tamanoPagina);
            Titulo.Text = titulo;
        }

        private void Cargar()
        {
            ProcesarTabs();
        }

        private void AddCategoria_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EditarCategoriaWindow editarCategoriaWindow = new EditarCategoriaWindow();
                editarCategoriaWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir la ventana de edición: {ex.Message}");
            }
        }

        private void ActualizarVistaYBoton(string tipoTab, string buttonText)
        {
            // Actualizar la vista correspondiente y el texto del botón
            ActualizarVista(tipoTab);
            textButton.Text = buttonText;
        }

        private void FiltrarContenidoActual(string filter)
        {
            // Filtrar según el contenido activo en el TabControl
            FiltrarDatos(filter, categoriasTab.categoriasDataGrid, categoriasTab.Items, c => c.Categoria.ToLower().Contains(filter));
        }

        private void ProcesarTabs()
        {
            try
            {
                ActualizarVista("Categorías");
                addEmpleado.Visibility = Visibility.Visible;

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
            // Obtener la categoría seleccionada
            var button = sender as Button;
            var categoria = button.DataContext as Categorias; // Asegúrate de que esta clase exista

            if (categoria != null)
            {
                EditarCategoriaWindow editarCategoriaWindow = new EditarCategoriaWindow();
                editarCategoriaWindow.txtIDCategoria.Text = categoria.IDCategoria.ToString();
                editarCategoriaWindow.txtNombreCategoria.Text = categoria.Categoria.ToString();
                editarCategoriaWindow.ShowDialog();

                Cargar(); // Recargar datos
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var categoria = button.DataContext as Categorias; // Asegúrate de que esta clase exista

            if (categoria != null)
            {
                // Confirmar eliminación
                MessageBoxResult result = MessageBox.Show($"¿Estás seguro de que deseas eliminar la categoría {categoria.Categoria}?", "Confirmar eliminación", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    CLS.Categorias oCategoria = new CLS.Categorias();
                    oCategoria.IDCategoria = Convert.ToInt32(categoria.IDCategoria);
                    if (oCategoria.Eliminar())
                    {
                        categoriasTab.Items.Remove(categoria); // Eliminar de la colección ObservableCollection
                        MessageBox.Show($"Categoría {categoria.Categoria} eliminada.");
                    }
                    else
                    {
                        MessageBox.Show($"Error al eliminar la categoría {categoria.Categoria}.");
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
            FiltrarDatos(filter, categoriasTab.categoriasDataGrid, categoriasTab.Items, c => c.Categoria.ToLower().Contains(filter));
        }
    }
}
