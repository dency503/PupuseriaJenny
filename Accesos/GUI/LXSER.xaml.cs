using System.Windows;
using System.Windows.Controls;
using Accesos.CLS;

namespace Accesos.GUI
{
    /// <summary>
    /// Lógica de interacción para UsuarioTab.xaml
    /// </summary>
    public partial class LXSER : UserControl
    {
        private int _paginaActual = 1;
        private const int _tamanoPagina = 10;
        int totalPaginas;
        public LXSER()
        {
            InitializeComponent();
           
            CargarDatosHelper.CargarEmpleados(_paginaActual, _tamanoPagina
                 , usersDataGrid);

        }
    }
}
