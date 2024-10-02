using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Chinh_QuanLyKho
{
    public class Electronic:Product
    {
        public int Warranty {  get; set; }

        public Electronic(string id, string name, string category, string producer, double priceInput, double priceOutput, int warranty, int quantitySale) :base(id, name, category, producer, priceInput, priceOutput, quantitySale)
        {
            Warranty = warranty;
        }

        public Electronic(int warranty):base()
        {
            Warranty = warranty;
        }

        public Electronic(string id, string name, string category, string producer, double priceInput, double priceOutput, int warranty)
        {
            this.Id = id;
            this.Name = name;
            this.Category = category;
            this.Producer = producer;
            this.PriceInput = priceInput;
            this.PriceOutput = priceOutput;
            this.Warranty = warranty;
        }

        public Electronic() { }

        public override Product Clone()
        {
            return new Electronic(Id, Name, Category, Producer, PriceInput, PriceOutput, Warranty, QuantitySale);
        }

        public override string getName()
        {
            return "Electronic";
        }

        public override void Add(Product item)
        {
            UnitOfWork.Instance.electronicRepository.Add(item);
        }

        public override int getWarranty()
        {
            return Warranty;
        }

        public override double GetPriceDiscount()
        {
            return PriceOutput * Parameter.electronicDiscount;
        }
    }
}
