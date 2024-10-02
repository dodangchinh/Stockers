using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for frmAddMovie.xaml
    /// </summary>
    public partial class frmAddRole : Window
    {
        int nRole;
        RoleService roleService;
        ObservableCollection<Role> lstRole;
        public frmAddRole(ObservableCollection<Role> lstRole)
        {
            InitializeComponent();
            this.lstRole = lstRole;

            roleService = new RoleService();

            nRole = Parameter.nRole + 1;
            txtId.Text = $"R{nRole}";
        }

        private void TxtDuration_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text.Length <= 0)
                return;

            Role role = new Role(txtId.Text, txtName.Text);

            AddRole(role);

            this.Close();
        }

        void AddRole(Role role)
        {     
            if (!roleService.Add(role))
                MessageBox.Show($"This {txtName.Text} is Exist!");
            lstRole.Add(role);
            Parameter.nRole++;
            MessageBox.Show("Successful!");
        }
        
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
