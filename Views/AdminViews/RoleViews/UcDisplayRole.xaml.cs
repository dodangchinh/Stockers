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

namespace Chinh_QuanLyKho
{
    /// <summary>
    /// Interaction logic for UcDisplayAccount.xaml
    /// </summary>
    public partial class UcDisplayRole : UserControl
    {
        Account accountLogin;
        RoleService roleService;
        public Role roleSelected {  get; set; }   

        public ObservableCollection<Role> lstRole {  get; set; }
        public UcDisplayRole(Account accountLogin)
        {
            InitializeComponent();
            this.accountLogin = accountLogin;
            roleService = new RoleService();
            lstRole = new ObservableCollection<Role>(roleService.Gets());
            this.DataContext = this;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            frmEditRole frmEditRole = new frmEditRole(accountLogin);
            frmEditRole.myDelegate += GetRole;
            frmEditRole.ShowDialog();
            
        }

        private Role GetRole()
        {
            if (roleSelected == null)
                return null;

            return roleSelected;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            frmAddRole frmAddRole = new frmAddRole(lstRole);
            frmAddRole.DataContext = lstRole;
            frmAddRole.ShowDialog();
        }
    }
}
