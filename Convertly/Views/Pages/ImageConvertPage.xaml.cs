﻿using Convertly.ViewModels;
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

namespace Convertly.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для ImageConvertPage.xaml
    /// </summary>
    public partial class ImageConvertPage : Page
    {
        public ImageConvertPage()
        {
            InitializeComponent();

            DataContext = new ImageConvertPageViewModel();
        }
    }
}
