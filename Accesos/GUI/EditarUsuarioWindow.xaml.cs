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
        SesionManager.Sesion oSesion = SesionManager.Sesion.ObtenerInstancia();
        private Boolean _RegistroExitoso = false;
        public bool RegistroExitoso { get => _RegistroExitoso; private set => _RegistroExitoso = value; }

        private int _paginaActual = 1;
        private const int _tamanoPagina = 10;
        int totalPaginas;
        private Boolean Validar()
        {
            Boolean valido = true;
            try
            {
                if (txtUsuario.Text.Trim().Length == 0)
                {
                    // Notificador.SetError(tbUsuario, "Este campo no puede estar vacío");
                    valido = false;
                }
                if (txtContraseña.Password.Trim().Length == 0)
                {
                    //  Notificador.SetError(tbContraseña, "Este campo no puede estar vacío");
                    valido = false;
                }

            }
            catch (Exception)
            {
                valido = false;
            }
            return valido;
        }
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
            cmbEmpleado.ItemsSource = empleados.DefaultView; // Establece el ItemsSource al DataView del DataTable
            cmbEmpleado.DisplayMemberPath = "Nombre"; // Nombre del empleado que se mostrará
            cmbEmpleado.SelectedValuePath = "IDEmpleado"; // IDEmpleado será el valor seleccionado

            // Cargar el ComboBox de Roles
            DataTable roles = Consultas.ROLES();
            cmbRol.ItemsSource = roles.DefaultView; // Establece el ItemsSource al DataView del DataTable
            cmbRol.DisplayMemberPath = "Rol"; // Rol que se mostrará
            cmbRol.SelectedValuePath = "IDRol"; // IDRol será el valor seleccionado
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Validar())
                {
                    // CREAR UN OBJETO A PARTIR DE LA CLASE ENTIDAD
                    CLS.Usuarios oUsuario = new CLS.Usuarios();
                    // SINCRONIZAMOS EL OBJETO CON LA GUI
                    try
                    {
                        oUsuario.IDUsuario = Convert.ToInt32(txtIDUsuario.Text);
                    }
                    catch (Exception)
                    {
                        //Console.WriteLine("No se puedo convertir ");
                        oUsuario.IDUsuario = 0;
                    }

                    oUsuario.Usuario = txtUsuario.Text;
                    oUsuario.Contraseña = txtContraseña.Password;
                    oUsuario.IDEmpleado = Convert.ToInt32(cmbEmpleado.SelectedValue);
                    oUsuario.IDRol = Convert.ToInt32(cmbRol.SelectedValue);
                    //PROCEDER
                    if (txtIDUsuario.Text.Trim().Length == 0)
                    {
                        //GUARDAR NUEVO REGISTRO
                        if (oUsuario.Insertar())
                        {
                            MessageBox.Show("Resgistro realizado con éxito");
                            _RegistroExitoso = true;
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo registrar el Usuario");
                            _RegistroExitoso = false;
                        }
                    }
                    else
                    {
                        // ACTUALIZAR NUEVO REGISTRO
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

                MessageBox.Show(ex.Message);
            }
        }
    }

}
