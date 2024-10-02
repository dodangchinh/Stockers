using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Chinh_QuanLyKho
{
    public class Porcelain : Product
    {
        public string Material {  get; set; }

        public Porcelain(string id, string name, string category, string producer, double priceInput, double priceOutput, string material, int quantitySale) : base(id, name, category, producer, priceInput, priceOutput, quantitySale)
        {
            Material = material;
        }
        public Porcelain(string id, string name, string category, string producer, double priceInput, double priceOutput, string material)
        {
            this.Id = id;
            this.Name = name;
            this.Category = category;
            this.Producer = producer;
            this.PriceInput = priceInput;
            this.PriceOutput = priceOutput;
            Material = material;
        }

        public Porcelain(string material):base()
        {
            Material = material;
        }

        public Porcelain() { }

        public override Product Clone()
        {
            return new Porcelain(Id, Name, Category, Producer, PriceInput, PriceOutput, Material, QuantitySale);
        }

        public override string getName()
        {
            return "Food";
        }

        public override string getMaterial()
        {
            return Material;
        }

        public override void Add(Product item)
        {
            UnitOfWork.Instance.porcelainRepository.Add(item);
        }

        public override double GetPriceDiscount()
        {
            return 0;
        }
    }
}
