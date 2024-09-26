using System.Data;
using System.Windows;
using System.Windows.Controls;
using Accesos.CLS;
using DataLayer;

namespace Accesos.GUI
{
    /// <summary>
    /// Lógica de interacción para EditarPermisoWindow.xaml
    /// </summary>
    public partial class EditarPermisoWindow : Window
    {
        public EditarPermisoWindow()
        {
            InitializeComponent();
        }
        private void CargarDatos()
        {
            DataTable roles = Consultas.ROLES();
            cmbRol.ItemsSource = roles.DefaultView; // Establece el ItemsSource al DataView del DataTable
            cmbRol.DisplayMemberPath = "Rol"; // Rol que se mostrará
            cmbRol.SelectedValuePath = "IDRol"; // IDRol será el valor seleccionado
        }

        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private Boolean Validar()
        {
            Boolean valido = true;
            try
            {
                if (cmbRol.Text.Trim().Length == 0)
                {
                    //Notificador.SetError(cbIDRol, "Este campo no puede estar vacio");
                    valido = false;
                }
            }
            catch (Exception)
            {
                valido = false;
            }
            return valido;
        }
        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (Validar())
            {
                Permisos oPermiso = new Permisos();

                // Obtener el valor de cmbPermiso, convertirlo a entero o usar 0 si hay error
                try
                {
                    oPermiso.IDPermiso = Convert.ToInt32(txtIDPermiso.Text);
                }
                catch (Exception)
                {
                    oPermiso.IDPermiso = 0;
                }

                // Obtener el valor del combo cmbRol
                oPermiso.IDRol = Convert.ToInt32(cmbRol.SelectedValue);

                // Iterar sobre los ítems seleccionados en cmbOpciones (usamos ListBox en lugar de CheckedListBox)
                foreach (var item in cmbOpcion.SelectedItems)
                {
                    var listBoxItem = item as ListBoxItem;
                    if (listBoxItem != null)
                    {
                        oPermiso.IDOpcion = Convert.ToInt32(listBoxItem.Tag);  // Usar Tag o un valor correspondiente
                    }
                }

                // Si el texto del ComboBox cmbPermiso está vacío, insertamos un nuevo registro
                if (string.IsNullOrWhiteSpace(txtIDPermiso.Text))
                {
                    if (oPermiso.Insertar())
                    {
                        MessageBox.Show("Registro guardado");
                        Close(); // Cerrar la ventana si se guarda correctamente
                    }
                    else
                    {
                        MessageBox.Show("El registro no pudo ser almacenado");
                    }
                }
                else
                {
                    // Si el registro ya existe, lo actualizamos
                    if (oPermiso.Actualizar())
                    {
                        MessageBox.Show("Registro actualizado");
                        Close(); // Cerrar la ventana si se actualiza correctamente
                    }
                    else
                    {
                        MessageBox.Show("El registro no pudo ser actualizado");
                    }
                }
            }
        }
    }
}
