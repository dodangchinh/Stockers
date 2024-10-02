using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_QuanLyKho
{
    public class ProductService
    { 
        IRepository<Product> _repository;   
        public ProductService()
        {
        }

        public void ChangeProduct(Product product)
        {
            switch (product.Category)
            {
                case "Electronic":
                    _repository = UnitOfWork.Instance.electronicRepository;
                    break;
                case "Porcelain":
                    _repository = UnitOfWork.Instance.porcelainRepository;
                    break;
                case "Food":
                    _repository = UnitOfWork.Instance.foodRepository;
                    break;
            }
        }

        public bool Add(Product product)
        {
            if(!isExit(product.Id, product.Name))
            {
                _repository.Add(product);
                return true;
            }
            return false;
        }

        public bool isExit(string id, string name)
        {
            foreach (var item in _repository.Gets())
                if (item.Name.ToLower().CompareTo(name.ToLower()) == 0
                    || item.Id.ToLower().CompareTo(id.ToLower()) == 0)
                    return true;
            return false;
        }

        public Product GetById(string id)
        {
            foreach (var item in _repository.Gets())
                if (item.Id.ToLower().CompareTo(id.ToLower()) == 0)
                    return item;
            return null;
        }

        public Product GetByName(string name)
        {
            foreach (var item in _repository.Gets())
                if (item.Name.ToLower().CompareTo(name.ToLower()) == 0)
                    return item;
            return null;
        }

        public void ChangePrice(Product item, double price)
        {
            item.PriceInput = price;
            item.PriceOutput = item.PriceInput + item.PriceInput * 0.1 + item.PriceInput * 0.3 + item.PriceInput * (Parameter.nAccount * 0.036);
            _repository.Update(item); 
        }

        public double GetRevenue(List<Product> products)
        {
            double price = 0;
            foreach (var item in products)
                price += (item.PriceOutput * item.QuantitySale);
            return price;
        }

        public double GetProfit(List<Product> products)
        {
            double price = 0;
            foreach (var item in products)
                price += ((item.PriceOutput * item.QuantitySale) - (item.PriceInput * item.QuantitySale));
            return price;
        }
        public Product GetById(List<Product> products, string id)
        {
            foreach (var item in products)
                if (item.Id.ToLower().CompareTo(id.ToLower()) == 0)
                    return item;
            return null;
        }

        public void UpdateQuantitySale(List<Product> products, List<SalesSlip> salesSlips)
        {
            Product product = null;
            foreach (var item in salesSlips)
            {
                if(item.CreateAt.Date == DateTime.Now.Date)
                    foreach (var  itemDetail in item.lstDetail)
                    {
                        product = GetById(products, itemDetail.product.Id);
                        product.QuantitySale += item.Quantity; 
                    }                    
            }
        }

        public List<Product> Gets()
        {
            return UnitOfWork.Instance.lstProduct;
        }

        public List<Product> GetElectronics()
        {
            return UnitOfWork.Instance.electronicRepository.Gets();
        }

        public List<Product> GetPorcelains()
        {
            return UnitOfWork.Instance.porcelainRepository.Gets();
        }

        public List<Product> GetFoods()
        {
            return UnitOfWork.Instance.foodRepository.Gets();
        }

        public Product SetElectronic(Product temp, int Warranty)
        {
            Product product;

            product = new Electronic(Warranty);
            product.Id = temp.Id;
            product.Name = temp.Name;
            product.Category = temp.Category;
            product.Producer = temp.Producer;
            product.PriceInput = temp.PriceInput;
            product.PriceOutput = temp.PriceOutput;

            return product;
        }

        public Product SetPorcelain(Product temp, string Material)
        {
            Product product;

            product = new Porcelain(Material);
            product.Id = temp.Id;
            product.Name = temp.Name;
            product.Category = temp.Category;
            product.Producer = temp.Producer;
            product.PriceInput = temp.PriceInput;
            product.PriceOutput = temp.PriceOutput;

            return product;
        }
    }
}
