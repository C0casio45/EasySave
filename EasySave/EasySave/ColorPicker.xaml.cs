using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EasySave
{
    /// <summary>
    /// Logique d'interaction pour ColorPicker.xaml
    /// </summary>
    public partial class ColorPicker : Window
    {
        public string ColorPicked
        {
            get;
            private set;
        }

        public ColorPicker()
        {
            InitializeComponent();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            ColorPicked = btn.Background.ToString();
            DialogResult = true;
            this.Close();
        }
    }
}
