using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Accesos.CLS;

namespace Accesos.GUI
{
    /// <summary>
    /// Lógica de interacción para EmpleadoTab.xaml
    /// </summary>
    public partial class EmpleadoTab : UserControl, ITab
    {
        public ObservableCollection<Empleados> Items { get; set; } = new ObservableCollection<Empleados>();
        public int TotalRegistros { get; set; }
        private int _paginaActual = 1;
        private const int _tamanoPagina = 10;

        public EmpleadoTab()
        {
            InitializeComponent();
            Cargar();
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // Obtener el usuario seleccionado
            var button = sender as Button;
            var empleado = button.DataContext as Empleados;

            if (empleado != null)
            {
                EditarEmpleadoWindow editarEmpleadoWindow = new EditarEmpleadoWindow();
                editarEmpleadoWindow.txtIDEmpleado.Text = empleado.idEmpleados.ToString();
                editarEmpleadoWindow.txtNombre.Text = empleado.nombresEmpleado.ToString();
                editarEmpleadoWindow.txtCargo.Text = empleado.idCargo.ToString();
                editarEmpleadoWindow.txtTelefono.Text = empleado.telefono.ToString();
                editarEmpleadoWindow.txtEmail.Text = empleado.email.ToString();
                editarEmpleadoWindow.ShowDialog();

                Cargar();
                // Aquí puedes abrir una ventana de edición o realizar otras acciones
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var empleado = button.DataContext as Empleados;
            if (empleado != null)
            {
                // Confirmar eliminación
                MessageBoxResult result = MessageBox.Show($"¿Estás seguro de que deseas eliminar a {empleado.nombresEmpleado}?", "Confirmar eliminación", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {

                    CLS.Empleados oUsuario = new CLS.Empleados();
                    oUsuario.idEmpleados = Convert.ToInt32(empleado.idEmpleados);
                    if (oUsuario.Eliminar())
                    {
                        Items.Remove(empleado); // Eliminar de la colección ObservableCollection
                        MessageBox.Show($"Empleado {empleado.nombresEmpleado} eliminado.");
                    }
                    else
                    {
                        MessageBox.Show($"Error al eliminar el empleado {empleado.idEmpleados}.");
                    }
                }
            }

        }
        private void Cargar()
        {
            var cargarRoles = new CargarDatos<Empleados>(_paginaActual, _tamanoPagina);
            cargarRoles.Cargar(
                DataLayer.Consultas.ContarEmpleados,
                DataLayer.Consultas.Empleados,
                (row, items) =>
                {
                    items.Add(new Empleados
                    {
                        idEmpleados = Convert.ToInt32(row["idEmpleados"]),
                        nombresEmpleado = row["nombresEmpleado"].ToString(),
                        apellidosEmpleado = row["apellidosEmpleado"].ToString(),
                        email = row["email"].ToString(),
                        telefono = row["telefono"].ToString(),
                        direccion = row["direccion"].ToString(),
                        fechaNacimiento = DateTime.TryParse(row["fechaNacimiento"].ToString(), out DateTime fecha) ? fecha : DateTime.MinValue,
                        idCargo = Convert.ToInt32(row["idCargo"].ToString())

                    });

                },
                usersDataGrid
            );

            TotalRegistros = cargarRoles.TotalRegistros;
            Items = cargarRoles.Items;
        }
    }
}
