using System;
using System.Data;
using System.Windows;
using DataLayer;

namespace Accesos.GUI
{
    /// <summary>
    /// Lógica de interacción para EditarCategoriaWindow.xaml
    /// </summary>
    public partial class EditarCategoriaWindow : Window
    {
        private bool _registroExitoso = false;
        public bool RegistroExitoso { get => _registroExitoso; private set => _registroExitoso = value; }

        private bool Validar()
        {
            bool valido = true;
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombreCategoria.Text))
                {
                    // Notificador.SetError(txtNombreCategoria, "Este campo no puede estar vacío");
                    valido = false;
                }
            }
            catch (Exception)
            {
                valido = false;
            }
            return valido;
        }

        public EditarCategoriaWindow()
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
            // Aquí podrías cargar datos adicionales si es necesario
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Validar())
                {
                    // CREAR UN OBJETO A PARTIR DE LA CLASE ENTIDAD
                    CLS.Categorias oCategoria = new CLS.Categorias();
                    // SINCRONIZAMOS EL OBJETO CON LA GUI
                    try
                    {
                        oCategoria.IDCategoria = Convert.ToInt32(txtIDCategoria.Text);
                    }
                    catch (Exception)
                    {
                        // Console.WriteLine("No se pudo convertir ");
                        oCategoria.IDCategoria = 0;
                    }

                    oCategoria.Categoria = txtNombreCategoria.Text;

                    // PROCEDER
                    if (txtIDCategoria.Text.Trim().Length == 0)
                    {
                        // GUARDAR NUEVO REGISTRO
                        if (oCategoria.Insertar())
                        {
                            MessageBox.Show("Registro realizado con éxito");
                            _registroExitoso = true;
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo registrar la categoría");
                            _registroExitoso = false;
                        }
                    }
                    else
                    {
                        // ACTUALIZAR REGISTRO EXISTENTE
                        if (oCategoria.Actualizar())
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
