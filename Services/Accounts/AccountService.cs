using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Chinh_QuanLyKho
{
    public class AccountService
    {

        public AccountService() 
        {
        }

        public bool Add(Account account)
        {
            if (!isExist(account.Name))
            {
                UnitOfWork.Instance.accountRepository.Add(account);
                return true;
            }
            return false;
        }

        public bool Update(Account account, string name)
        {
            account.Name = name;
            if (!isExist(account.Name))
            {
                UnitOfWork.Instance.accountRepository.Update(account);
                return true;
            }
            return false;          
        }

        public Account getAccount(string username, string password)
        {
            foreach (var item in UnitOfWork.Instance.accountRepository.Gets())
            {
                if (item.Username == username && item.Password == password)
                    return new Account(item.Id, item.Name, item.Username, item.Password);
            }
            return null;
        }

        public Account SearchById(string  id)
        {
            return UnitOfWork.Instance.accountRepository.Get(id);
        }

        public Account SearchByName(string name)
        {
            foreach (var item in UnitOfWork.Instance.accountRepository.Gets())
                if (item.Name.CompareTo(name) == 0)
                    return item;
            return null;
        }

        public bool isExist(string name)
        {
            if (SearchByName(name) == null)
                return false;
            return true;
        }

        public List<Account> Gets()
        {
            return UnitOfWork.Instance.accountRepository.Gets();
        }

        public void Delete(Account account)
        {
            UnitOfWork.Instance.accountRepository.Delete(account);
        }
    }
}