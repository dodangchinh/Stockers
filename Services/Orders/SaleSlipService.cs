using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_QuanLyKho
{
    public class SaleSlipService
    {
        public SaleSlipService()
        {
        }

        public double getDiscount(SalesSlip salesSlip)
        {
            double total = 0;
            foreach (var item in salesSlip.lstDetail)
                total += item.product.GetPriceDiscount();
            return total;
        }

        public void Add(SalesSlip salesSlip)
        {
            UnitOfWork.Instance.saleSlipRepository.Add(salesSlip);
        }

        public void Update(SalesSlip salesSlip)
        {
            UnitOfWork.Instance.saleSlipRepository.Update(salesSlip);
        }
        public SalesSlip Get(string id)
        {
            foreach (var item in UnitOfWork.Instance.saleSlipRepository.Gets())
                if(item.Id.ToLower().CompareTo(id.ToLower()) == 0)
                    return item;
            return null;
        }

        public double GetTotal(List<SalesSlipDetail> lstSalesSlipDetail)
        {
            double sum = 0;
            foreach (var item in lstSalesSlipDetail)
                sum += (item.product.PriceOutput * item.Quantity) - item.Discount;
            return sum;
        }

        public List<SalesSlip> Gets()
        {
            return UnitOfWork.Instance.saleSlipRepository.Gets();
        }
    }
}
