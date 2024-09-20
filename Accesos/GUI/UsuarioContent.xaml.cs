using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Accesos.CLS;
using InventiSys.GUI;
using MahApps.Metro.IconPacks;

namespace Accesos.GUI
{
    /// <summary>
    /// Lógica de interacción para UsuarioContent.xaml
    /// </summary>
    public partial class UsuarioContent : UserControl
    {
        ObservableCollection<Usuarios> _usuarios = new ObservableCollection<Usuarios>();
        private int _paginaActual = 1;
        private const int _tamanoPagina = 10;
        int totalPaginas;
        public UsuarioContent()
        {
            InitializeComponent();
           
            Cargar();
           

        }
        private void Cargar()
        {
            try
            {
                // Obtener los datos y cargarlos en la colección ObservableCollection
                int totalUsuarios = DataLayer.Consultas.ContarUsuarios();
                lbRegistros.Text = totalUsuarios.ToString();

                // Calcular el total de páginas
                totalPaginas = (int)Math.Ceiling((double)totalUsuarios / _tamanoPagina);
               
                GenerarBotonesPaginacion(totalPaginas, _paginaActual);
                DataTable dt = DataLayer.Consultas.USUARIOS(_paginaActual, _tamanoPagina);
            
                FiltrarLocalmente();
                _usuarios.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    _usuarios.Add(new Usuarios
                    {
                        IDUsuario = Convert.ToInt32(row["IDUsuario"]),
                        Usuario = row["Usuario"].ToString(),
                        Contraseña = row["Contrasena"].ToString(),
                        IDEmpleado = Convert.ToInt32(row["IDEmpleado"]),
                        IDRol = Convert.ToInt32(row["IDRol"])

                    });
                }
                usersDataGrid.ItemsSource = _usuarios;
                lbRegistros.Text = totalUsuarios.ToString() + " Usuarios encontrado(s)";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}");
            }
        }
        private void FiltrarLocalmente()
        {
            try
            {
                string filter = txtFilter.Text.Trim().ToLower();
                if (string.IsNullOrWhiteSpace(filter))
                {
                    usersDataGrid.ItemsSource = _usuarios;
                }
                else
                {
                    usersDataGrid.ItemsSource = new ObservableCollection<Usuarios>(
                        _usuarios.Where(u => u.Usuario.ToLower().Contains(filter))
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al filtrar datos: {ex.Message}");
            }
        }
        private void GenerarBotonesPaginacion(int totalPaginas, int paginaActual)
        {
            // Limpiar botones existentes
            paginacion.Children.Clear();

            // Agregar botón de "anterior"
            if (paginaActual > 1)
            {
                var btnAnterior = new Button
                {
                    Style = (Style)FindResource("pagingButton"),
                    Content = new PackIconMaterial { Kind = PackIconMaterialKind.ChevronLeft }
                };
                btnAnterior.Click += (s, e) => CambiarPagina(paginaActual - 1);
                paginacion.Children.Add(btnAnterior);
            }

            // Generar botones de páginas
            for (int i = 1; i <= totalPaginas; i++)
            {
                var btnPagina = new Button
                {
                    Style = (Style)FindResource("pagingButton"),
                    Content = i.ToString(),
                    Background = (i == paginaActual) ? new SolidColorBrush(Color.FromRgb(121, 80, 242)) : Brushes.Transparent,
                    Foreground = (i == paginaActual) ? Brushes.White : Brushes.Black
                };

                // Usar una variable local para el clic
                int pagina = i;
                btnPagina.Click += (s, e) => CambiarPagina(pagina);
                paginacion.Children.Add(btnPagina);
            }

            // Agregar botón de "siguiente"
            if (paginaActual < totalPaginas)
            {
                var btnSiguiente = new Button
                {
                    Style = (Style)FindResource("pagingButton"),
                    Content = new PackIconMaterial { Kind = PackIconMaterialKind.ChevronRight }
                };
                btnSiguiente.Click += (s, e) => CambiarPagina(paginaActual + 1);
                paginacion.Children.Add(btnSiguiente);
            }
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // Obtener el usuario seleccionado
            var button = sender as Button;
            var usuario = button.DataContext as Usuarios;

            if (usuario != null)
            {
                EditarUsuarioWindow editarUsuarioWindow = new EditarUsuarioWindow();
                editarUsuarioWindow.txtIDUsuario.Text = usuario.IDUsuario.ToString();
                editarUsuarioWindow.txtUsuario.Text =usuario.Usuario.ToString();
                editarUsuarioWindow.cmbEmpleado.SelectedValue = usuario.IDEmpleado;
                editarUsuarioWindow.cmbRol.SelectedValue=usuario.IDRol;
                editarUsuarioWindow.txtContraseña.Password = usuario.Contraseña;
                editarUsuarioWindow.ShowDialog();
                
                Cargar();
                // Aquí puedes abrir una ventana de edición o realizar otras acciones
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var usuario = button.DataContext as Usuarios;

            if (usuario != null)
            {
                // Confirmar eliminación
                MessageBoxResult result = MessageBox.Show($"¿Estás seguro de que deseas eliminar a {usuario.Usuario}?", "Confirmar eliminación", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    
                    CLS.Usuarios oUsuario = new CLS.Usuarios();
                    oUsuario.IDUsuario = Convert.ToInt32(usuario.IDUsuario);
                    if (oUsuario.Eliminar())
                    {
                        _usuarios.Remove(usuario); // Eliminar de la colección ObservableCollection
                        MessageBox.Show($"Usuario {usuario.Usuario} eliminado.");
                    }
                    else
                    {
                        MessageBox.Show($"Error al eliminar el usuario {usuario.Usuario}.");
                    }
                }
            }
        }


        private void CambiarPagina(int nuevaPagina)
        {
            if (nuevaPagina < 1 || nuevaPagina > totalPaginas)
                return; // Evitar cambios fuera de rango

            _paginaActual = nuevaPagina;
            Cargar(); // Método que carga los datos según la página actual
            GenerarBotonesPaginacion(totalPaginas, _paginaActual); // Regenerar botones
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            FiltrarLocalmente();
        }
    }
}
