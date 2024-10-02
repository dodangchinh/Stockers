using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
public partial class frmEditAccount : Window,INotifyPropertyChanged
{
        Account accountLogin;
        private AccountRole _accountRoleSelected;
        public AccountRole accountRoleSelected
        { 
            get { return _accountRoleSelected; }
            set
            {
                _accountRoleSelected = value;
                OnPropertyChanged();
            }
        }
        public Role roleSelected {  get; set; }    

        RoleService roleService;
        AccountService accountService;
        AccountRoleService accountRoleService;

        public ObservableCollection<Role> Roles { get; set; }

        // create delegate 
        public delegate AccountRole MyDelegate();
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
                accountRoleSelected = myDelegate();
                txtName.Text = accountRoleSelected.account.Name.ToString();
            }
        }
        public frmEditAccount(Account accountLogin)
        {
            InitializeComponent();
            InitVarible();

            this.accountLogin = accountLogin;
            this.accountRoleSelected = accountRoleSelected;
            this.DataContext = this;
        }

        private void InitVarible()
        {
            accountService = new AccountService();
            accountRoleService = new AccountRoleService();
            roleService = new RoleService();
            Roles = new ObservableCollection<Role>(roleService.Gets());
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (myDelegate != null)
            {
                accountRoleSelected = myDelegate();

                Account account = accountRoleSelected.account.Clone();
                if (account.Name.CompareTo(accountLogin.Name) == 0)
                {
                    MessageBox.Show("Cannot edited the MANAGER role from the currently logged-in account!!");
                    return;
                }
                CheckUpdateAccount(account);
                CheckUpdateRoleAccount(account);
            }        
        }

        void CheckUpdateAccount(Account account)
        {
            if (account.Name.CompareTo(txtName.Text) == 0)
                return;

            if (accountService.Update(account, txtName.Text))
            {
                accountRoleSelected.account.Name = txtName.Text;
                MessageBox.Show("Successful!");
                this.Close();
            }
            else
                MessageBox.Show($"This {accountRoleSelected.account.Name} is Exist!");
        }

        void CheckUpdateRoleAccount(Account account)
        {
            
            if (roleSelected == null || roleSelected.Id.CompareTo(accountRoleSelected.role.Id) == 0)
                return;
            accountRoleSelected.role = roleSelected;
            accountRoleSelected.IdRole = roleSelected.Id;
            accountRoleService.Update(accountRoleSelected);

            MessageBox.Show("Successful!");
            this.Close();

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}