using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_QuanLyKho
{
    public class Account : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public int Id { get; set; }
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged();
            }
        }
        public string Username { get; set; }
        public string Password { get; set; }

        public Account(int id, string name, string username, string password)
        {
            Id = id;
            Name = name;
            Username = username;
            Password = password;
        }

        public Account()
        {
            Name = "Undefined";
        }

        public Account Clone()
        {
            return new Account(Id, Name, Username, Password);
        }
    }
}