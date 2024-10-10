using System.Data;
using System.Windows;
using Accesos.CLS;
using DataLayer;

namespace Accesos.GUI
{
    /// <summary>
    /// Lógica de interacción para EditarNombreWindow.xaml
    /// </summary>
    public partial class EditarEmpleadoWindow : Window
    {
        public EditarEmpleadoWindow()
        {
            InitializeComponent();
            DataTable cargos = Consultas.Cargos();
            cmbCargo.ItemsSource = cargos.DefaultView;
            cmbCargo.DisplayMemberPath = "cargo";
            cmbCargo.SelectedValuePath = "idCargo";
           
        }
        private Boolean Validar()
        {
            Boolean valido = true;
            try
            {
                if (txtNombre.Text.Trim().Length == 0)
                {
                    // Notificador.SetError(tbNombre, "Este campo no puede estar vacío");
                    valido = false;
                }
                // Agregar más validaciones si es necesario para otros campos
            }
            catch (Exception)
            {
                valido = false;
            }
            return valido;
        }
        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Validar())
                {
                    Empleados oEmpleado = new Empleados
                    {
                        idEmpleados = string.IsNullOrWhiteSpace(txtIDEmpleado.Text) ? 0 : Convert.ToInt32(txtIDEmpleado.Text),
                        nombresEmpleado = txtNombre.Text,
                        apellidosEmpleado = txtApellido.Text,
                        idCargo = Convert.ToInt32(cmbCargo.SelectedValue), // Asumiendo que el ComboBox está vinculado adecuadamente
                        telefono = txtTelefono.Text,
                        email = txtEmail.Text,
                        direccion = txtDireccion.Text,
                        fechaNacimiento = dpFechaNacimiento.SelectedDate.Value // Asegúrate de manejar el caso en que no haya una fecha seleccionada
                    };

                    if (oEmpleado.idEmpleados == 0) // Nuevo registro
                    {
                        if (oEmpleado.Insertar())
                        {
                            MessageBox.Show("Registro guardado");
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("El registro no pudo ser almacenado");
                        }
                    }
                    else // Actualizar registro existente
                    {
                        if (oEmpleado.Actualizar())
                        {
                            MessageBox.Show("Registro actualizado");
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("El registro no pudo ser actualizado");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}");
            }
        }

        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}