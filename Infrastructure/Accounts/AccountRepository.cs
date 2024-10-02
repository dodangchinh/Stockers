using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Chinh_QuanLyKho
{
    public class AccountRepository:IRepository<Account>
    {
        public List<Account> lstAccount { get; set; }

        public AccountRepository() 
        {
            lstAccount = new List<Account>();
            Load();
        }

        void Load()
        {
            DataProvider.pathData = "data/Account/Accounts.xml";
            DataProvider.Open();

            string xPath = string.Format("//Account");

            XmlNodeList listNode = DataProvider.getDsNode(xPath);

            foreach (XmlNode item in listNode)
            {
                Account account = new Account();
                account.Id = int.Parse(item.Attributes["Id"].Value);
                account.Name = item.Attributes["Name"].Value;
                account.Username = item.Attributes["Username"].Value;
                account.Password = item.Attributes["Password"].Value;
                lstAccount.Add(account);
                Parameter.nAccount++;
            }
            DataProvider.Close();
        }

        public void Add(Account item)
        {
            lstAccount.Add(item);

            // save item in file book2.xml
            DataProvider.pathData = "data/Account/Accounts.xml";
            DataProvider.Open();

            XmlNode newNode = DataProvider.createNode("Account");
            // XmlNode newNode = doc.CreateElement("Book");
            XmlAttribute attr1 = DataProvider.createAttr("Id");
            attr1.Value = item.Id.ToString();
            XmlAttribute attr2 = DataProvider.createAttr("Name");
            attr2.Value = item.Name;
            XmlAttribute attr3 = DataProvider.createAttr("Username");
            attr3.Value = item.Username;
            XmlAttribute attr4 = DataProvider.createAttr("Password");
            attr4.Value = item.Password;

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);
            newNode.Attributes.Append(attr3);
            newNode.Attributes.Append(attr4);

            string xPath = string.Format("//Accounts");
            XmlNode node = DataProvider.getNode(xPath);
            DataProvider.AppendNode(node, newNode);

            DataProvider.Close();
        }

        public void Update(Account item)
        {
            // save item in file book2.xml
            DataProvider.pathData = "data/Account/Accounts.xml";
            DataProvider.Open();
            string xPath = string.Format("//Account[@Id='{0}']", item.Id);
            XmlNode oldNode = DataProvider.getNode(xPath);

            XmlNode newNode = DataProvider.createNode("Account");
            // XmlNode newNode = doc.CreateElement("Book");
            XmlAttribute attr1 = DataProvider.createAttr("Id");
            attr1.Value = item.Id.ToString();
            XmlAttribute attr2 = DataProvider.createAttr("Name");
            attr2.Value = item.Name;
            XmlAttribute attr3 = DataProvider.createAttr("Username");
            attr3.Value = item.Username;
            XmlAttribute attr4 = DataProvider.createAttr("Password");
            attr4.Value = item.Password;

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);
            newNode.Attributes.Append(attr3);
            newNode.Attributes.Append(attr4);

            DataProvider.InsertNode(newNode, oldNode);
            DataProvider.RemoveNode(oldNode);

            DataProvider.Close();
        }

        public void Delete(Account item)
        {
            lstAccount.Remove(item);
            // save item in file book2.xml
            DataProvider.pathData = "data/Account/Accounts.xml";
            DataProvider.Open();

            string xPath = string.Format("//Account[@Id='{0}']", item.Id);

            XmlNode oldNode = DataProvider.getNode(xPath);
            DataProvider.RemoveNode(oldNode);

            DataProvider.Close();
        }

        public Account Get(string id)
        {
            int Id;
            foreach (var account in lstAccount)
                if (int.TryParse(id, out Id))
                    if (account.Id == Id)
                        return account;
            return null;
        }

        public List<Account> Gets()
        {
           return lstAccount;
        }
    }
}
