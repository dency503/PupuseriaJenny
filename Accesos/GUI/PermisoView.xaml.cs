﻿using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Accesos.CLS;
using MahApps.Metro.IconPacks;

namespace Accesos.GUI
{
    public partial class PermisoView : UserControl, ITab
    {
        PermisoTab permisoTab = new PermisoTab();
        private int _paginaActual = 1;
        private const int _tamanoPagina = 10;
        public int TotalRegistros { get; set; }

        public PermisoView()
        {
            InitializeComponent();
            TabContent.Content = permisoTab;
            Cargar();
        }

        private void ActualizarVista(ITab tab, string titulo)
        {
            lbRegistros.Text = tab.TotalRegistros.ToString() + $" {titulo} encontrado(s)";
            TotalRegistros = (int)Math.Ceiling((double)tab.TotalRegistros / _tamanoPagina);
            Titulo.Text = titulo;
        }

        private void Cargar()
        {
            ProcesarTabs();
        }

        private void AddPermiso_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EditarPermisoWindow editarPermisoWindow = new EditarPermisoWindow();
                editarPermisoWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir la ventana de edición: {ex.Message}");
            }
        }

        private void FiltrarContenidoActual(string filter)
        {
            if (TabContent.Content == permisoTab)
            {
                FiltrarDatos(filter, permisoTab.usersDataGrid, permisoTab.Items, p => p.IDPermiso.ToString().Contains(filter));
            }
        }

        private void ProcesarTabs()
        {
            try
            {
                if (TabContent.Content == permisoTab)
                {
                    ActualizarVista(permisoTab, "Permisos");
                    addPermiso.Visibility = Visibility.Visible;
                }

                GenerarBotonesPaginacion(TotalRegistros, _paginaActual);

                string filter = txtFilter.Text.Trim().ToLower();
                FiltrarContenidoActual(filter);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}");
            }
        }

        public void FiltrarDatos<T>(string filtro, DataGrid dataGrid, ObservableCollection<T> items, Func<T, bool> filtroPredicado)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filtro))
                {
                    dataGrid.ItemsSource = items;
                }
                else
                {
                    dataGrid.ItemsSource = new ObservableCollection<T>(items.Where(filtroPredicado));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al filtrar datos: {ex.Message}");
            }
        }

        private void GenerarBotonesPaginacion(int totalPaginas, int paginaActual)
        {
            paginacion.Children.Clear();

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

            for (int i = 1; i <= totalPaginas; i++)
            {
                var btnPagina = new Button
                {
                    Style = (Style)FindResource("pagingButton"),
                    Content = i.ToString(),
                    Background = (i == paginaActual) ? new SolidColorBrush(Color.FromRgb(121, 80, 242)) : Brushes.Transparent,
                    Foreground = (i == paginaActual) ? Brushes.White : Brushes.Black
                };

                int pagina = i;
                btnPagina.Click += (s, e) => CambiarPagina(pagina);
                paginacion.Children.Add(btnPagina);
            }

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

        private void CambiarPagina(int nuevaPagina)
        {
            if (nuevaPagina < 1 || nuevaPagina > TotalRegistros)
                return;

            _paginaActual = nuevaPagina;
            Cargar();
            GenerarBotonesPaginacion(TotalRegistros, _paginaActual);
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filter = txtFilter.Text.Trim().ToLower();
            FiltrarContenidoActual(filter);
        }
    }
}
