using System;
using System.Data;
using System.Windows;
using DataLayer;

namespace Accesos.GUI
{
    /// <summary>
    /// Lógica de interacción para EditarUsuarioWindow.xaml
    /// </summary>
    public partial class EditarUsuarioWindow : Window
    {
        
        private bool _registroExitoso = false;
        public bool RegistroExitoso { get => _registroExitoso; private set => _registroExitoso = value; }

        public EditarUsuarioWindow()
        {
            InitializeComponent();
            Cargar();
        }

        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Cargar()
        {
            // Cargar el ComboBox de Empleados
            DataTable empleados = Consultas.Empleados();
            cmbEmpleado.ItemsSource = empleados.DefaultView;
            cmbEmpleado.DisplayMemberPath = "nombresEmpleado";
            cmbEmpleado.SelectedValuePath = "idEmpleados";

            // Cargar el ComboBox de Roles
            DataTable roles = Consultas.ROLES();
            cmbRol.ItemsSource = roles.DefaultView;
            cmbRol.DisplayMemberPath = "rol";
            cmbRol.SelectedValuePath = "idRol";
        }

        private bool Validar()
        {
            bool valido = true;
            try
            {
                if (string.IsNullOrWhiteSpace(txtUsuario.Text))
                {
                    // Notificador.SetError(tbUsuario, "Este campo no puede estar vacío");
                    MessageBox.Show("El campo Usuario no puede estar vacío.");
                    valido = false;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la validación: " + ex.Message);
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
                    // Crear un objeto a partir de la clase entidad
                    CLS.Usuarios oUsuario = new CLS.Usuarios();

                    // Sincronizamos el objeto con la GUI
                    oUsuario.IDUsuario = string.IsNullOrWhiteSpace(txtIDUsuario.Text) ? 0 : Convert.ToInt32(txtIDUsuario.Text);
                    oUsuario.Usuario = txtUsuario.Text;
                    oUsuario.Contraseña = txtContraseña.Password;
                    oUsuario.IDEmpleado = Convert.ToInt32(cmbEmpleado.SelectedValue);
                    oUsuario.IDRol = Convert.ToInt32(cmbRol.SelectedValue);
                   

                    // Proceder según si es un nuevo registro o una actualización
                    if (oUsuario.IDUsuario == 0)
                    {
                        // Guardar nuevo registro
                        if (oUsuario.Insertar())
                        {
                            MessageBox.Show("Registro realizado con éxito");
                            _registroExitoso = true;
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo registrar el Usuario");
                            _registroExitoso = false;
                        }
                    }
                    else
                    {
                        // Actualizar registro existente
                        if (oUsuario.Actualizar())
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
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
        }
    }
}
