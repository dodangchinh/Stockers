using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_QuanLyKho
{
    public class AccountRoleService
    {

        public AccountRoleService()
        {
        }

        public void Add(AccountRole accountRole)
        {
            UnitOfWork.Instance.accountRolesRepository.Add(accountRole);
            Parameter.nAccountRole++;
        }

        public void Delete(AccountRole accountRole)
        {
            UnitOfWork.Instance.accountRolesRepository.Delete(accountRole);
            Parameter.nAccountRole--;
        }

        public AccountRole GetByIdAccount(int id)
        {
            foreach (var item in UnitOfWork.Instance.accountRolesRepository.Gets())
                if(item.IdAccount == id)
                    return item;
            return null;
        }

        public List<AccountRole> Gets()
        {
            return UnitOfWork.Instance.accountRolesRepository.Gets();
        }

        public void Update(AccountRole accountRole)
        {
            UnitOfWork.Instance.accountRolesRepository.Update(accountRole);
        }
    }
}
