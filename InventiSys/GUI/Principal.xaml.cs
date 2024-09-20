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
using Accesos.GUI;

namespace InventiSys.GUI
{
    public partial class Principal : Window
    {
        public Principal()
        {
            InitializeComponent();
            LoadInicioView();
        }

        private void LoadInicioView()
        {
           // MainContent.Content = new InicioView(); // Asegúrate de que "InicioView" esté correctamente definido.
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) {
                this.DragMove();
            }
        }
        private bool IsMaximized = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
               
                if (IsMaximized)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;
                    IsMaximized = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;

                    IsMaximized = true;
                }
            }
        }
        private void menuAcceso_Click(object sender, RoutedEventArgs e)
        {
            //MainContent.Content = new GestionUsuarioView(); // Asegúrate de que "GestionUsuarioView" esté correctamente definido.
        }
    }
}
