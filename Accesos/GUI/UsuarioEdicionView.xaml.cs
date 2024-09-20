using System.Windows.Controls;
using InventiSys.CLS;

namespace InventiSys.GUI
{
    /// <summary>
    /// Lógica de interacción para UsuarioEdicionView.xaml
    /// </summary>
    public partial class UsuarioEdicionView : UserControl
    {
        public UsuarioEdicionView()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationManager.GoBack(this.Parent as ContentControl);

        }
    }
}
