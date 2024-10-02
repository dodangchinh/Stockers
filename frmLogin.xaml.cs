using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
using static System.Net.WebRequestMethods;

namespace Chinh_QuanLyKho
{
    /// <summary>
    /// Interaction logic for frmLogin.xaml
    /// </summary>
    public partial class frmLogin : Window
    {
        AccountService accountService;
        AccountRoleService accountRoleService;
        public frmLogin()
        {
            InitializeComponent();
            accountService = new AccountService();
            accountRoleService = new AccountRoleService();
            Validator.CheckAll();
            this.Show();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Account account = new Account();
            account = accountService.getAccount(txtUsername.Text, txtPassword.Password);
            AccountRole accountRole = new AccountRole();

            if (account != null)
            {
                accountRole = accountRoleService.GetByIdAccount(account.Id);
                if (accountRole == null)
                    MessageBox.Show($"Your account '{account.Name}' does not have permission to access this application!");
                else
                {
                    switch (accountRole.role.Name)
                    {
                        case "MANAGER":
                            frmAdmin frmAdmin = new frmAdmin(account);
                            this.Hide();
                            frmAdmin.ShowDialog();
                            break;
                        case "STOCKER":
                            frmStocker frmStocker = new frmStocker(account);
                            this.Hide();
                            frmStocker.ShowDialog();
                            break;
                        case "CASHIER":
                            frmCashier frmCashier = new frmCashier(account);
                            this.Hide();
                            frmCashier.ShowDialog();
                            break;
                    }
                    this.Show();
                }            
            }
            else
                MessageBox.Show("Incorrect account!");

            txtUsername.Clear();
            txtPassword.Clear();
        }
    }
}
