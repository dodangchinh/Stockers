using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Chinh_QuanLyKho
{
    /// <summary>
    /// Interaction logic for frmEditAccount.xaml
    /// </summary>
    public partial class frmEditRole : Window, INotifyPropertyChanged
    {
        Account accountLogin;

        private Role _roleSelected;
        public Role roleSelected
        {
            get {return _roleSelected;}
            set
            {
                _roleSelected = value;
                OnPropertyChanged();
            }
        }

        RoleService roleService;

        // create delegate 
        public delegate Role MyDelegate();
        public event MyDelegate myDelegate;

        public event PropertyChangedEventHandler PropertyChanged;
        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (myDelegate != null)
            {
                roleSelected = myDelegate();
                txtName.Text = roleSelected.Name;
            }
        }

        public frmEditRole(Account accountLogin)
        {
            InitializeComponent();
            this.accountLogin = accountLogin;
            roleService = new RoleService();
            this.DataContext = this;
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            Role role = roleSelected.Clone();
            
            CheckUpdateRole(role);
        }

        void CheckUpdateRole(Role role)
        {
            if (roleSelected.Name.CompareTo(txtName.Text) == 0)
                return;
            if (roleService.Update(role, txtName.Text))
            {
                roleSelected.Name = txtName.Text;
                MessageBox.Show("Successful!");
                this.Close();
            }
            else
                MessageBox.Show($"This {roleSelected.Name} is Exist!");
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
