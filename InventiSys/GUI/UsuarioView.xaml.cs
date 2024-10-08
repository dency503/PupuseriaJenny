﻿using Accesos.GUI;
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

namespace InventiSys.GUI
{
    /// <summary>
    /// Lógica de interacción para UsuarioView.xaml
    /// </summary>
    public partial class UsuarioView : Window
    {
         
        public UsuarioView()
        {
            InitializeComponent();
            
            MainContentControl.Content = new UsuarioContent();
        }

        private void Acessos_Click(object sender, RoutedEventArgs e)
        {
            MainContentControl.Content = new UsuarioContent();
        }

        private void Permisos_Click(object sender, RoutedEventArgs e)
        {
            MainContentControl.Content = new PermisoView();
        }

        private void Productos_Click(object sender, RoutedEventArgs e)
        {
            MainContentControl.Content = new ProductosView();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainContentControl.Content = new CategoriasView();
        }
    }
}
