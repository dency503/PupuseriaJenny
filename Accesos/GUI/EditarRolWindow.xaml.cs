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

namespace Accesos.GUI
{
    /// <summary>
    /// Lógica de interacción para EditarRolWindow.xaml
    /// </summary>
    public partial class EditarRolWindow : Window
    {
        public EditarRolWindow()
        {
            InitializeComponent();
        }
        private Boolean Validar()
        {
            Boolean valido = true;
            try
            {
                if (txtRol.Text.Trim().Length == 0)
                {
                    //Notificador.SetError(tbRol, "Este campo no puede estar vacio");
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
            try
            {
                if (Validar())
                {
                    // CREAR UN OBJETO A PARTIR DE LA CLASE ENTIDAD
                    CLS.Roles oRol = new CLS.Roles();
                    // SINCRONIZAMOS EL OBJETO CON LA GUI
                    try
                    {
                        oRol.IDRol = Convert.ToInt32(txtIDRol.Text);
                    }
                    catch (Exception)
                    {
                        //Console.WriteLine("No se puedo convertir ");
                        oRol.IDRol = 0;
                    }

                    oRol.Rol = txtRol.Text;
                    //PROCEDER
                    if (txtIDRol.Text.Trim().Length == 0)
                    {
                        //GUARDAR NUEVO REGISTRO
                        if (oRol.Insertar())
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
                        // ACTUALIZAR NUEVO REGISTRO
                        if (oRol.Actualizar())
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
                throw;
            }
        }

        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
