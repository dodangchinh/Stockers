using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Chinh_QuanLyKho
{
    public class AccountRolesRepository:IRepository<AccountRole>
    {
        public List<AccountRole> lstAccountRole {  get; set; }  
        private List<Account> lstAccounts { get; set; }
        private List<Role> lstRole { get; set; }

        public AccountRolesRepository(List<Account> latAccount, List<Role> lstRole)
        {
            lstAccountRole = new List<AccountRole>();
            lstAccounts = latAccount;
            this.lstRole = lstRole;
            Load();            
        }

        public void Load()
        {
            DataProvider.pathData = "data/Account/AccountRoles.xml";
            DataProvider.Open();

            string xPath = string.Format("//AccountRole");

            XmlNodeList listNode = DataProvider.getDsNode(xPath);;
            foreach (XmlNode item in listNode)
            {
                AccountRole role = new AccountRole(GetAccount(int.Parse(item.Attributes["IdAccount"].Value)), GetRole(lstRole, item.Attributes["IdRole"].Value));
                lstAccountRole.Add(role);
                Parameter.nAccountRole++;
            }

            DataProvider.Close();
        }
        Role GetRole(List<Role> lstRoles, string id)
        {
            foreach (var item in lstRoles)
                if (item.Id.CompareTo(id) == 0)
                    return item;
            return null;
        }

        Account GetAccount(int id)
        {
            foreach (var item in lstAccounts) 
                if (item.Id == id)
                    return item;
            return null;
        }

        public void Add(AccountRole item)
        {
            lstAccountRole.Add(item);

            // save item in file book2.xml
            DataProvider.pathData = "data/Account/AccountRoles.xml";
            DataProvider.Open();

            XmlNode newNode = DataProvider.createNode("AccountRole");
            // XmlNode newNode = doc.CreateElement("Book");
            XmlAttribute attr1 = DataProvider.createAttr("IdAccount");
            attr1.Value = item.IdAccount.ToString();
            XmlAttribute attr2 = DataProvider.createAttr("IdRole");
            attr2.Value = item.role.Id;

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);

            string xPath = string.Format("//AccountRoles");
            XmlNode node = DataProvider.getNode(xPath);
            DataProvider.AppendNode(node, newNode);
            

            DataProvider.Close();
        }

        public void Update(AccountRole item)
        {
            // save item in file book2.xml
            DataProvider.pathData = "data/Account/AccountRoles.xml";
            DataProvider.Open();
            string xPath = string.Format("//AccountRoles/AccountRole[@IdAccount='{0}']", item.IdAccount);
            XmlNode oldNode = DataProvider.getNode(xPath);

            XmlNode newNode = DataProvider.createNode("AccountRole");
            // XmlNode newNode = doc.CreateElement("Book");
            XmlAttribute attr1 = DataProvider.createAttr("IdAccount");
            attr1.Value = item.IdAccount.ToString();
            XmlAttribute attr2 = DataProvider.createAttr("IdRole");
            attr2.Value = item.role.Id;

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);

            DataProvider.nodeRoot = DataProvider.getNode("//AccountRoles");
            DataProvider.InsertNode(newNode, oldNode);
            DataProvider.RemoveNode(oldNode);

            DataProvider.Close();
        }

        public void Delete(AccountRole item)
        {
            lstAccountRole.Remove(item);
            // save item in file book2.xml
            DataProvider.pathData = "data/Account/AccountRoles.xml";
            DataProvider.Open();

            string xPath = string.Format("//AccountRole[@IdAccount='{0}' and @IdRole='{1}']", item.IdAccount, item.role.Id);

            XmlNode oldNode = DataProvider.getNode(xPath);
            DataProvider.RemoveNode(oldNode);

            DataProvider.Close();
        }

        public AccountRole Get(string Id)
        {
            throw new NotImplementedException();
        }

        public List<AccountRole> Gets()
        {
            return lstAccountRole;
        }
    }
}
