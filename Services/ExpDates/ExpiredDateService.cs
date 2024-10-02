using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_QuanLyKho
{
    public class ExpiredDateService
    {
        public ExpiredDateService()
        {
        }

        public void Add(ExpDate expDate)
        {
            UnitOfWork.Instance.expiredDateRepository.Add(expDate);
        }
        public void AddList(List<ExpDate> lstExpDate)
        {
            foreach (var item in lstExpDate)
                UnitOfWork.Instance.expiredDateRepository.Add(item);

        }
        public void ChangeImport(ExpDate importDate, int Quantity)
        {
            importDate.Quantity -= Quantity;
            importDate.QuantityUsed += Quantity;
        }
        public void Update(ExpDate expDate)
        {
            UnitOfWork.Instance.expiredDateRepository.Update(expDate);
        }
        public void UpdateList(List<ExpDate> lstExpDate)
        {
            foreach (var item in lstExpDate)
                UnitOfWork.Instance.expiredDateRepository.Update(item);
        }

        public void Delete(ExpDate expDate)
        {
            UnitOfWork.Instance.expiredDateRepository.Delete(expDate);
        }
        public ExpDate GetByQuantity(string IdProduct)
        {
            foreach (var item in UnitOfWork.Instance.expiredDateRepository.Gets())
                if(item.product.Id.ToLower().CompareTo(IdProduct.ToLower()) == 0 && item.QuantityUsed != 0)
                    return item;
            return null;
        }

        public ExpDate GetById(string id)
        {
            foreach (var item in UnitOfWork.Instance.expiredDateRepository.Gets())
                if (item.Id.ToLower().CompareTo(id.ToLower()) == 0)
                    return item;          
            return null;
        }
        public void ChangeExpired(Product product, int Quantity)
        {
            ExpDate expiredDate = GetByQuantity(product.Id);
            if (Quantity > expiredDate.Quantity)
            {
                int tempQuantity = Quantity - expiredDate.Quantity;
                Quantity -= tempQuantity;
                expiredDate.Quantity -= Quantity;
                expiredDate.QuantityUsed += Quantity;
                Quantity = tempQuantity;
            }
            else
            {
                expiredDate.Quantity -= Quantity;
                expiredDate.QuantityUsed += Quantity;
                Quantity = 0;
            }
        }

        public List<ExpDate> Gets()
        {
            return UnitOfWork.Instance.expiredDateRepository.Gets();
        }

        public List<ExpDate> GetListImport()
        {
            List<ExpDate> lstImport = new List<ExpDate>();

            foreach (var item in UnitOfWork.Instance.expiredDateRepository.Gets())
                if (item.Id[0].CompareTo('I') == 0)
                    lstImport.Add(item);
            return lstImport;
        }

        public List<ExpDate> GetListExport()
        {
            List<ExpDate> lstExport = new List<ExpDate>();

            foreach (var item in UnitOfWork.Instance.expiredDateRepository.Gets())
                if (item.Id[0].CompareTo('E') == 0)
                    lstExport.Add(item);
            return lstExport;
        }
    }
}
