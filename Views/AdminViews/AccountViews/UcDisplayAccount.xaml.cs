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
    public partial class UcDisplayAccount : UserControl
    {
        Account accountLogin;
        AccountService accountService;
        AccountRoleService accountRoleService;
        public AccountRole accountRoleSelected {  get; set; }   
        public ObservableCollection<AccountRole> lstAccountRole {  get; set; }
        public UcDisplayAccount(Account accountLogin)
        {
            InitializeComponent();
            this.accountLogin = accountLogin;
            accountRoleService = new AccountRoleService();
            lstAccountRole = new ObservableCollection<AccountRole>(accountRoleService.Gets());
            this.DataContext = this;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            frmEditAccount frmEditAccount = new frmEditAccount(accountLogin);
            frmEditAccount.myDelegate += GetAccountRole;
            frmEditAccount.ShowDialog();           
        }

        private AccountRole GetAccountRole()
        {
            if (accountRoleSelected == null)
                return null;
            return accountRoleSelected;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            frmAddAccount frmAddAccount = new frmAddAccount(lstAccountRole);
            frmAddAccount.DataContext = lstAccountRole;
            frmAddAccount.ShowDialog();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            frmDeleteAccount frmDeleteAccount = new frmDeleteAccount(lstAccountRole, accountRoleSelected, accountLogin);
            frmDeleteAccount.DataContext = accountRoleSelected;
            frmDeleteAccount.ShowDialog();  
        }
    }
}