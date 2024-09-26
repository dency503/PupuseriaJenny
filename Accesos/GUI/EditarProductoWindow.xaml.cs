using System;
using System.Data;
using System.Windows;
using DataLayer;

namespace Accesos.GUI
{
    /// <summary>
    /// Lógica de interacción para EditarProductoWindow.xaml
    /// </summary>
    public partial class EditarProductoWindow : Window
    {
        SesionManager.Sesion oSesion = SesionManager.Sesion.ObtenerInstancia();
        private bool _registroExitoso = false;
        public bool RegistroExitoso { get => _registroExitoso; private set => _registroExitoso = value; }

        private bool Validar()
        {
            bool valido = true;
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombreProducto.Text))
                {
                    // Notificador.SetError(txtNombreProducto, "Este campo no puede estar vacío");
                    valido = false;
                }
                if (string.IsNullOrWhiteSpace(txtCostoUnitario.Text))
                {
                    // Notificador.SetError(txtCostoUnitario, "Este campo no puede estar vacío");
                    valido = false;
                }
                if (string.IsNullOrWhiteSpace(txtPrecio.Text))
                {
                    // Notificador.SetError(txtPrecio, "Este campo no puede estar vacío");
                    valido = false;
                }
                if (string.IsNullOrWhiteSpace(txtCantidad.Text))
                {
                    // Notificador.SetError(txtCantidad, "Este campo no puede estar vacío");
                    valido = false;
                }
            }
            catch (Exception)
            {
                valido = false;
            }
            return valido;
        }

        public EditarProductoWindow()
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
            // Cargar el ComboBox de Categorías
            DataTable categorias = Consultas.CATEGORIAS(); // Asegúrate de tener esta consulta
            cmbCategoria.ItemsSource = categorias.DefaultView; // Establece el ItemsSource al DataView del DataTable
            cmbCategoria.DisplayMemberPath = "NombreCategoria"; // Nombre de la categoría que se mostrará
            cmbCategoria.SelectedValuePath = "IDCategoria"; // IDCategoria será el valor seleccionado

            // Cargar el ComboBox de Proveedores
            DataTable proveedores = Consultas.PROVEEDORES(); // Asegúrate de tener esta consulta
            cmbProveedor.ItemsSource = proveedores.DefaultView; // Establece el ItemsSource al DataView del DataTable
            cmbProveedor.DisplayMemberPath = "NombreProveedor"; // Nombre del proveedor que se mostrará
            cmbProveedor.SelectedValuePath = "IDProveedor"; // IDProveedor será el valor seleccionado
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Validar())
                {
                    // CREAR UN OBJETO A PARTIR DE LA CLASE ENTIDAD
                    CLS.Productos oProducto = new CLS.Productos();
                    // SINCRONIZAMOS EL OBJETO CON LA GUI
                    try
                    {
                        oProducto.IDProducto = Convert.ToInt32(txtIDProducto.Text);
                    }
                    catch (Exception)
                    {
                        //Console.WriteLine("No se pudo convertir ");
                        oProducto.IDProducto = 0;
                    }

                    oProducto.NombreProducto = txtNombreProducto.Text;
                    oProducto.CostoUnitario = Convert.ToDouble(txtCostoUnitario.Text);
                    oProducto.Precio = Convert.ToDouble(txtPrecio.Text);
                    oProducto.CantidadProducto = Convert.ToInt32(txtCantidad.Text);
                    oProducto.IDCategoria = Convert.ToInt32(cmbCategoria.SelectedValue);
                    oProducto.IDProveedor = Convert.ToInt32(cmbProveedor.SelectedValue);

                    //PROCEDER
                    if (txtIDProducto.Text.Trim().Length == 0)
                    {
                        //GUARDAR NUEVO REGISTRO
                        if (oProducto.Insertar())
                        {
                            MessageBox.Show("Registro realizado con éxito");
                            _registroExitoso = true;
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo registrar el producto");
                            _registroExitoso = false;
                        }
                    }
                    else
                    {
                        // ACTUALIZAR NUEVO REGISTRO
                        if (oProducto.Actualizar())
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
