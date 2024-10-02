using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_QuanLyKho
{
    public class CardService
    {
        public CardService()
        {
        }

        public void Add(Cards card)
        {
            UnitOfWork.Instance.cardRepository.Add(card);
        }

        public void Update(Cards card)
        {
            UnitOfWork.Instance.cardRepository.Update(card);
        }
        public Cards GetById(int id)
        {
            foreach (var item in UnitOfWork.Instance.cardRepository.Gets())
                if(item.Id == id)
                    return item;
            return null;
        }
    }
}
