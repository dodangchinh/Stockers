using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_QuanLyKho
{
    public abstract class Product: INotifyPropertyChanged
    {
        private string _Id;
        public string Id
        {
            get { return _Id; }
            set
            {
                _Id = value;
                OnPropertyChanged();
            }
        }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                OnPropertyChanged();
            }
        }

        public string Category { get; set; }
        public string Producer { get; set; }

        private double _PriceInput;
        public double PriceInput
        {
            get { return _PriceInput; }
            set
            {
                _PriceInput = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private double _PriceOutput;
        public double PriceOutput
        {
            get { return _PriceOutput; }
            set 
            { 
                _PriceOutput = PriceInput + PriceInput * 0.1 + PriceInput * 0.3 + PriceInput * (Parameter.nAccount * 0.036);
                OnPropertyChanged();
            }
        }
        public int QuantitySale {  get; set; }

        public Product(string id, string name, string category, string producer, double priceInput, double priceOutput, int quantitySale)
        {
            Id = id;
            Name = name;
            Category = category;
            Producer = producer;
            PriceInput = priceInput;
            PriceOutput = _PriceOutput;
            QuantitySale = quantitySale;
        }

        public Product() { }

        public abstract string getName();
        public abstract void Add(Product item);
        public abstract Product Clone();
        public abstract double GetPriceDiscount();

        public virtual int getWarranty()
        {
            return 0;
        }

        public virtual string getMaterial()
        {
            return null;
        }
    }
}
