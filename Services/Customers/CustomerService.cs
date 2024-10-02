using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_QuanLyKho
{
    public class CustomerService
    {
        public CustomerService()
        {
        }

        public void Add(Customer customer)
        {
            UnitOfWork.Instance.customerRepository.Add(customer);
        }
        public void Update(Customer customer)
        {
            UnitOfWork.Instance.customerRepository.Update(customer);
        }

        public Customer GetByName(string name)
        {
            foreach (var item in UnitOfWork.Instance.customerRepository.Gets())
                if(item.Name.ToLower().CompareTo(name.ToLower()) == 0)  
                    return item;
            return null;
        }

        public double getTotalScore(Customer customer, double scoreUse)
        {
            double sum = 0;
            foreach (var item in customer.lstDetails)
                sum += item.Score;
            sum -= scoreUse;
            return sum;
        }

        public List<Customer> Gets()
        {
            return UnitOfWork.Instance.customerRepository.Gets();
        }
    }
}
