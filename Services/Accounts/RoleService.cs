using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_QuanLyKho
{
    public class RoleService
    {

        public RoleService()
        {
        }

        public bool Add(Role role)
        {
            if (!isExist(role.Name))
            {
                UnitOfWork.Instance.roleRepository.Add(role);
                return true;
            }
            return false;
        }

        public bool Update(Role role, string name)
        {
            role.Name = name;   
            if (!isExist(role.Name))
            {
                UnitOfWork.Instance.roleRepository.Update(role);
                return true;
            }
            return false;
        }

        public Role SearchByName(string name)
        {
            foreach (var item in UnitOfWork.Instance.roleRepository.Gets())
                if (item.Name.ToUpper().CompareTo(name.ToUpper()) == 0)
                    return item;
            return null;
        }

        public bool isExist(string name)
        {
            if (SearchByName(name) == null)
                return false;
            return true;
        }

        public Role SearchById(string id)
        {
            foreach (var item in UnitOfWork.Instance.roleRepository.Gets())
                if(item.Id.ToUpper().CompareTo(id.ToUpper()) == 0)
                    return item;
            return null;
        }

        public List<Role> Gets()
        {
            return UnitOfWork.Instance.roleRepository.Gets();
        }
    }
}
