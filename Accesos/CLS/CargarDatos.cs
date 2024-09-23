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

    public static class CargarDatosHelper
    {
        // Método para cargar roles
        public static void CargarRoles(int paginaActual, int tamanoPagina, DataGrid rolesDataGrid)
        {
            var cargarRoles = new CargarDatos<Roles>(paginaActual, tamanoPagina);
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
        }
        public static void CargarEmpleados(int paginaActual, int tamanoPagina, DataGrid rolesDataGrid)
        {
            var cargarRoles = new CargarDatos<Empleados>(paginaActual, tamanoPagina);
            cargarRoles.Cargar(
                DataLayer.Consultas.ContarRoles,
                DataLayer.Consultas.Empleados,
                (row, items) =>
                {
                    items.Add(new Empleados
                    {
                        IDEmpleado = Convert.ToInt32(row["IDEmpleado"]),
                        Nombre = row["Nombre"].ToString()
                    });

                },
                rolesDataGrid
            );
        }

        // Método para cargar usuarios
        public static void CargarUsuarios(int paginaActual, int tamanoPagina, DataGrid rolesDataGrid)
        {
            var cargarRoles = new CargarDatos<Usuarios>(paginaActual, tamanoPagina);
            cargarRoles.Cargar(
                DataLayer.Consultas.ContarUsuarios,
                DataLayer.Consultas.USUARIOS,
                (row, items) =>
                {
                    items.Add(new Usuarios
                    {
                        IDEmpleado = Convert.ToInt32(row["IDEmpleado"]),
                        IDUsuario = Convert.ToInt32(row["IDUsuario"]),
                        Usuario = row["Usuario"].ToString(),
                        Contraseña = row["Contrasena"].ToString()

                    });

                },
                rolesDataGrid
            );
        }
    }

    // Clase para Roles

    // Clase para Usuarios

}
