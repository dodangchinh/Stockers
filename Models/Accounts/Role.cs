using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_QuanLyKho
{
    public class Role : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public string Id { get; set; }
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
        public bool Status = true;

        public Role() { }
        public Role(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public Role Clone()
        {
            return new Role(Id, Name);
        }
    }
}
