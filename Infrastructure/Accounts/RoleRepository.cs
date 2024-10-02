using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Chinh_QuanLyKho
{
    public class RoleRepository : IRepository<Role>
    {
        public List<Role> lstRole {  get; set; }

        public RoleRepository() 
        { 
            lstRole = new List<Role>();
            Load();
        }

        public void Load()
        {
            DataProvider.pathData = "data/Account/Roles.xml";
            DataProvider.Open();

            string xPath = string.Format("//Role");

            XmlNodeList listNode = DataProvider.getDsNode(xPath);

            foreach (XmlNode item in listNode)
            {
                Role role = new Role();
                role.Id = item.Attributes["Id"].Value;
                role.Name = item.Attributes["Name"].Value;
                lstRole.Add(role);
                Parameter.nRole++;
            }
            DataProvider.Close();
        }

        public void Add(Role item)
        {
            lstRole.Add(item);

            // save item in file book2.xml
            DataProvider.pathData = "data/Account/Roles.xml";
            DataProvider.Open();

            XmlNode newNode = DataProvider.createNode("Role");
            // XmlNode newNode = doc.CreateElement("Book");
            XmlAttribute attr1 = DataProvider.createAttr("Id");
            attr1.Value = item.Id.ToString().ToUpper();
            XmlAttribute attr2 = DataProvider.createAttr("Name");
            attr2.Value = item.Name.ToUpper();

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);

            string xPath = string.Format("//Roles");
            XmlNode node = DataProvider.getNode(xPath);
            DataProvider.AppendNode(node, newNode);

            DataProvider.Close();
        }

        public void Delete(Role item)
        {
            throw new NotImplementedException();
        }

        public Role Get(string Id)
        {
            foreach (var item in lstRole)
                if(item.Id.CompareTo(Id) == 0)
                    return item;
            return null;
        }

        public List<Role> Gets()
        {
            return lstRole;
        }

        public void Update(Role item)
        {
            // save item in file book2.xml
            DataProvider.pathData = "data/Account/Roles.xml";
            DataProvider.Open();
            string xPath = string.Format("//Role[@Id='{0}']", item.Id);
            XmlNode oldNode = DataProvider.getNode(xPath);

            XmlNode newNode = DataProvider.createNode("Role");
            // XmlNode newNode = doc.CreateElement("Book");
            XmlAttribute attr1 = DataProvider.createAttr("Id");
            attr1.Value = item.Id.ToString().ToUpper();
            XmlAttribute attr2 = DataProvider.createAttr("Name");
            attr2.Value = item.Name.ToUpper();

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);

            DataProvider.InsertNode(newNode, oldNode);
            DataProvider.RemoveNode(oldNode);

            DataProvider.Close();
        }
    }
}
