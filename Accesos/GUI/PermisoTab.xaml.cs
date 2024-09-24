using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Accesos.GUI
{
    /// <summary>
    /// Lógica de interacción para PermisoTab.xaml
    /// </summary>
    public partial class PermisoTab : UserControl, ITab
    {
        public ObservableCollection<Permisos> Items { get; set; } = new ObservableCollection<Permisos>();
        public DataGrid DataGridControl => usersDataGrid;
        private int _paginaActual = 1;
        private const int _tamanoPagina = 10;
        int TotalPaginas;
        public int TotalRegistros { get; set; }
    
        public PermisoTab()
        {
            InitializeComponent();
        }
        private void CargarDatos()
        {
            try
            {


                /*  CargarDatosHelper.CargarUsuarios(_paginaActual, _tamanoPagina
                      , usersDataGrid);*/
                var cargarRoles = new CargarDatos<Permisos>(_paginaActual, _tamanoPagina);
                cargarRoles.Cargar(
                    DataLayer.Consultas.ContarPermisos,
                    DataLayer.Consultas.Permisos,
                    (row, items) =>
                    {
                        items.Add(new Permisos
                        {
                            IDOpcion = Convert.ToInt32(row["IDOpcion"]),
                            IDPermiso = Convert.ToInt32(row["IDPermiso"])
                            

                        });

                    },
                    usersDataGrid
                );
                TotalRegistros = cargarRoles.TotalRegistros;
                Items = cargarRoles.Items;








            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}");
            }
        }

    }
}
