using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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

namespace Chinh_QuanLyKho
{
    /// <summary>
    /// Interaction logic for frmAccept.xaml
    /// </summary>
    public partial class frmAccept : Window
    {
        public event EventHandler Accept;
        public event EventHandler Cancel;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Get the screen's working area dimensions
            var screenWidth = SystemParameters.WorkArea.Width;
            var screenHeight = SystemParameters.WorkArea.Height;

            // Calculate the left position to center the window horizontally
            var left = (screenWidth - this.Width) / 2;

            // Position the window at the bottom of the screen
            var top = screenHeight - this.Height;

            // Set the window's position
            this.Left = left;
            this.Top = top;
        }
        public frmAccept(string service)
        {
            InitializeComponent();
            txtConten.Text = service;
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Accept?.Invoke(this, EventArgs.Empty);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Cancel?.Invoke(this, EventArgs.Empty);
            this.Close();
        }
    }
}