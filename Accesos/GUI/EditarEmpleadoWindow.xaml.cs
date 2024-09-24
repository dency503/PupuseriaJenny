using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Accesos.CLS;

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
                    // Crear un objeto de la clase Empleados
                    Empleados oEmpleado = new Empleados();

                    try
                    {
                        oEmpleado.idEmpleados = Convert.ToInt32(txtIDEmpleado.Text);
                    }
                    catch (Exception)
                    {
                        oEmpleado.idEmpleados = 0;
                    }

                    oEmpleado.nombresEmpleado = txtNombre.Text;
                    oEmpleado.idCargo = Convert.ToInt32(txtCargo.Text);
                    oEmpleado.telefono = txtTelefono.Text;
                    oEmpleado.email = txtEmail.Text;

                    if (txtIDEmpleado.Text.Trim().Length == 0)
                    {
                        // Guardar un nuevo registro
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
                    else
                    {
                        // Actualizar un registro existente
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
            catch (Exception)
            {
                // Manejar la excepción apropiadamente
            }
        }

        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
