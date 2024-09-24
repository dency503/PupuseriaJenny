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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Accesos.GUI
{
    /// <summary>
    /// Lógica de interacción para PermisoView.xaml
    /// </summary>
    public partial class PermisoView : UserControl
    {
        public PermisoView()
        {
            InitializeComponent();
            TabContent.Content = new PermisoTab();
        }

        private void UsuariosButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
        private void AddPermiso_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
