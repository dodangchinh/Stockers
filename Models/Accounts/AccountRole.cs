using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_QuanLyKho
{
    public class AccountRole : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private Role _role;
        public Role role
        {
            get { return _role; }
            set
            {
                _role = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged();
            }
        }

        public int IdAccount {  get; set; }
        public string IdRole {  get; set; }

        private Account _account;
        public Account account
        { 
            get { return _account; }
            set
            {
                _account = value;
                OnPropertyChanged();
            }
        }

        public AccountRole() { }
        public AccountRole(Account account, Role role)
        {
            this.account = account;
            this.role = role;
            IdAccount = account.Id;
            
            if(role != null)
                this.IdRole = role.Id;
        }
    }
}
