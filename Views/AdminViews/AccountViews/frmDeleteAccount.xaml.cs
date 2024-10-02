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
using System.Windows.Shapes;

namespace Chinh_QuanLyKho
{
    /// <summary>
    /// Interaction logic for frmDeleteAccount.xaml
    /// </summary>
    public partial class frmDeleteAccount : Window
    {
        Account accountLogin;
        Account account;
        AccountRole accountRole;
        AccountService accountService;
        AccountRoleService accountRoleService;
        ObservableCollection<AccountRole> lstAccountRole;

        public frmDeleteAccount(ObservableCollection<AccountRole> lstAccountRole, AccountRole accountRoleSelected, Account accountLogin)
        {
            InitializeComponent();

            accountRoleService = new AccountRoleService();
            accountService = new AccountService();
            this.lstAccountRole = lstAccountRole;
            this.accountLogin = accountLogin;
            accountRole = accountRoleSelected;
            account = accountRoleSelected.account;

            txtDelete.Text += $"Do you want to delete account '{account.Name}'";
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            if(account.Name.CompareTo(accountLogin.Name) == 0)
            {
                MessageBox.Show("Cannot remove the MANAGER role from the currently logged-in account!!");
                return;
            }
            lstAccountRole.Remove(accountRole);
            accountRoleService.Delete(accountRole);
            accountService.Delete(account);
            Parameter.nAccount--;
            MessageBox.Show("Successful!");
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}