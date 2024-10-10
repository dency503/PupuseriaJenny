using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Accesos.CLS
{
    public class CargarDatos<T>
    {
        // Propiedades
        public int PaginaActual { get; set; }
        public int TamanoPagina { get; set; }
        public int TotalRegistros { get; private set; }
        public int TotalPaginas { get; private set; }
        public ObservableCollection<T> Items { get; private set; } = new ObservableCollection<T>();

        // Constructor para inicializar la clase
        public CargarDatos(int paginaActual, int tamanoPagina)
        {
            PaginaActual = paginaActual;
            TamanoPagina = tamanoPagina;
        }

        // Método genérico para cargar datos
        public void Cargar(Func<int> contarRegistros, Func<int, int, DataTable> obtenerDatos, Action<DataRow, ObservableCollection<T>> agregarItem, DataGrid dataGrid)
        {
            try
            {
                // Obtener el total de registros
                TotalRegistros = contarRegistros();

                // Calcular el total de páginas
                TotalPaginas = (int)Math.Ceiling((double)TotalRegistros / TamanoPagina);

                // Obtener los datos desde la base de datos
                DataTable dt = obtenerDatos(PaginaActual, TamanoPagina);

                // Limpiar la colección actual
                Items.Clear();

                // Agregar los nuevos datos
                foreach (DataRow row in dt.Rows)
                {
                    agregarItem(row, Items);

                }

                // Asignar la fuente de datos al DataGrid
                dataGrid.ItemsSource = Items;


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}");
            }
        }
    }

   


}
