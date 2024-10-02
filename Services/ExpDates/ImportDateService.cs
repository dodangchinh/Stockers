using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_QuanLyKho
{
    public class ImportDateService
    {
        public ImportDateService()
        {
        }

        public void Add(ExpDate expDate)
        {
            UnitOfWork.Instance.importDateRepository.Add(expDate);
        }
        public void AddList(List<ExpDate> lstExpDate)
        {
            foreach (var item in lstExpDate)
                UnitOfWork.Instance.importDateRepository.Add(item);

        }
        public void ChangeImport(ExpDate importDate, int Quantity)
        {
            importDate.Quantity -= Quantity;
            importDate.QuantityUsed += Quantity;
        }
        public void Update(ExpDate expDate)
        {
            UnitOfWork.Instance.importDateRepository.Update(expDate);
        }
        public void UpdateList(List<ExpDate> lstExpDate)
        {
            foreach (var item in lstExpDate)
                UnitOfWork.Instance.importDateRepository.Update(item);
        }

        public void Delete(ExpDate expDate)
        {
            UnitOfWork.Instance.importDateRepository.Delete(expDate);
        }
        public ExpDate GetByQuantity(string IdProduct)
        {
            foreach (var item in UnitOfWork.Instance.importDateRepository.Gets())
                if(item.product.Id.ToLower().CompareTo(IdProduct.ToLower()) == 0 && item.Quantity != 0)
                    return item;
            return null;
        }
        public List<ExpDate> GetsByQuantity(string IdProduct, int quantity)
        {
            List<ExpDate> lstExpDate = new List<ExpDate>(); 
            foreach (var item in UnitOfWork.Instance.importDateRepository.Gets())
                if (item.product.Id.ToLower().CompareTo(IdProduct.ToLower()) == 0 && item.Quantity != 0 && item.Quantity >= quantity)
                    lstExpDate.Add(item);
            return lstExpDate;
        }

        public ExpDate GetById(string id)
        {
            foreach (var item in UnitOfWork.Instance.importDateRepository.Gets())
                if (item.Id.ToLower().CompareTo(id.ToLower()) == 0)
                    return item;          
            return null;
        }
        public void ChangeExpired(Product product, ref int Quantity)
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
            return UnitOfWork.Instance.importDateRepository.Gets();
        }

    }
}
