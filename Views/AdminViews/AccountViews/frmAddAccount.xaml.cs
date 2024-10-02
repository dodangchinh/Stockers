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
    public partial class frmAddAccount : Window
    {
        int nAccount;
        AccountService accountService;
        AccountRoleService accountRoleService;
        RoleService roleService;
        ObservableCollection<AccountRole> lstAccountRole;
        public frmAddAccount(ObservableCollection<AccountRole> lstAccountRole)
        {
            InitializeComponent();
            InitVarible();

            this.lstAccountRole = lstAccountRole;
            nAccount = Parameter.nAccount + 1;
            txtId.Text = nAccount.ToString();
        }

        private void InitVarible()
        {
            accountService = new AccountService();
            accountRoleService = new AccountRoleService();
            roleService = new RoleService();
        }

        private void TxtDuration_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text.Length <= 0 || txtUsername.Text.Length <= 0 || txtPaassword.Text.Length <= 0)
                return;
            Account account = new Account(Parameter.nAccount, txtName.Text, txtUsername.Text, txtPaassword.Text);

            if(AddAccount(account))
                AddAccountRole(account);

            this.Close();
        }

        bool AddAccount(Account account)
        {     
            if (!accountService.Add(account))
            {
                MessageBox.Show($"This {txtName.Text} is Exist!");
                return false;
            }
            MessageBox.Show("Successful!");
            Parameter.nAccount++;
            return true;
        }

        void AddAccountRole(Account account)
        {
            AccountRole accountRole = new AccountRole(account, roleService.Gets()[0]);
            accountRoleService.Add(accountRole);
            lstAccountRole.Add(accountRole);
        }
        
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}