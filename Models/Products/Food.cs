using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Chinh_QuanLyKho
{

    public class Food : Product
    {
        public Food(string id, string name, string category, string producer, double priceInput, double priceOutput, int quantitySale) : base(id, name, category, producer, priceInput, priceOutput, quantitySale)
        {
        }
        public Food(string id, string name, string category, string producer, double priceInput, double priceOutput)
        {
            this.Id = id;
            this.Name = name;
            this.Category = category;
            this.Producer = producer;
            this.PriceInput = priceInput;
            this.PriceOutput = priceOutput;
        }

        public Food() { }

        public override Product Clone()
        {
            return new Food(Id, Name, Category, Producer, PriceInput, PriceOutput, QuantitySale);
        }

        public override string getName()
        {
            return "Food";
        }

        public override void Add(Product item)
        {
            UnitOfWork.Instance.foodRepository.Add(item);
        }

        public override double GetPriceDiscount()
        {
            return PriceOutput * Parameter.foodDiscount;
        }
    }
}
