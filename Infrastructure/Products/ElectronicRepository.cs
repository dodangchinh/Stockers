using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Chinh_QuanLyKho
{
    public class ElectronicRepository : IRepository<Product>
    {
        public List<Product> lstElectronic { get; set; }
        public ElectronicRepository()
        {
            lstElectronic = new List<Product>();
            Load();
        }

        void Load()
        {
            DataProvider.pathData = "data/Products/Electronics.xml";
            DataProvider.Open();

            string xPath = string.Format("//Product");

            XmlNodeList listNode = DataProvider.getDsNode(xPath);

            foreach (XmlNode item in listNode)
            {
                Electronic electronic = new Electronic();
                electronic.Id = item.Attributes["Id"].Value;
                electronic.Name = item.Attributes["Name"].Value;
                electronic.Category = item.Attributes["Category"].Value;
                electronic.Producer = item.Attributes["Producer"].Value;
                electronic.PriceInput = double.Parse(item.Attributes["PriceInput"].Value);
                electronic.PriceOutput = double.Parse(item.Attributes["PriceOutput"].Value);
                electronic.Warranty = int.Parse(item.Attributes["Warranty"].Value);
                lstElectronic.Add(electronic);
                Parameter.nElectronic++;
            }
            DataProvider.Close();
        }

        public void Add(Product item)
        {
            lstElectronic.Add(item);

            // save item in file book2.xml
            DataProvider.pathData = "data/Products/Electronics.xml";
            DataProvider.Open();

            XmlNode newNode = DataProvider.createNode("Product");
            // XmlNode newNode = doc.CreateElement("Book");
            XmlAttribute attr1 = DataProvider.createAttr("Id");
            attr1.Value = item.Id;
            XmlAttribute attr2 = DataProvider.createAttr("Name");
            attr2.Value = item.Name;
            XmlAttribute attr3 = DataProvider.createAttr("Category");
            attr3.Value = item.Category;
            XmlAttribute attr4 = DataProvider.createAttr("Producer");
            attr4.Value = item.Producer;
            XmlAttribute attr5 = DataProvider.createAttr("PriceInput");
            attr5.Value = item.PriceInput.ToString();
            XmlAttribute attr6 = DataProvider.createAttr("PriceOutput");
            attr6.Value = item.PriceOutput.ToString();
            XmlAttribute attr7 = DataProvider.createAttr("Warranty");
            attr7.Value = item.getWarranty().ToString();

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);
            newNode.Attributes.Append(attr3);
            newNode.Attributes.Append(attr4);
            newNode.Attributes.Append(attr5);
            newNode.Attributes.Append(attr6);
            newNode.Attributes.Append(attr7);

            string xPath = string.Format("//Products");
            XmlNode node = DataProvider.getNode(xPath);
            DataProvider.AppendNode(node, newNode);

            DataProvider.Close();
        }

        public void Delete(Product item)
        {
            throw new NotImplementedException();
        }

        public Product Get(string Id)
        {
            throw new NotImplementedException();
        }

        public List<Product> Gets()
        {
            return lstElectronic;
        }

        public void Update(Product item)
        {
            // save item in file book2.xml
            DataProvider.pathData = "data/Products/Electronics.xml";
            DataProvider.Open();
            string xPath = string.Format("//Product[@Id='{0}']", item.Id);
            XmlNode oldNode = DataProvider.getNode(xPath);

            XmlNode newNode = DataProvider.createNode("Product");
            // XmlNode newNode = doc.CreateElement("Book");
            XmlAttribute attr1 = DataProvider.createAttr("Id");
            attr1.Value = item.Id;
            XmlAttribute attr2 = DataProvider.createAttr("Name");
            attr2.Value = item.Name;
            XmlAttribute attr3 = DataProvider.createAttr("Category");
            attr3.Value = item.Category;
            XmlAttribute attr4 = DataProvider.createAttr("Producer");
            attr4.Value = item.Producer;
            XmlAttribute attr5 = DataProvider.createAttr("PriceInput");
            attr5.Value = item.PriceInput.ToString();
            XmlAttribute attr6 = DataProvider.createAttr("PriceOutput");
            attr6.Value = item.PriceOutput.ToString();
            XmlAttribute attr7 = DataProvider.createAttr("Warranty");
            attr7.Value = item.getWarranty().ToString();

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);
            newNode.Attributes.Append(attr3);
            newNode.Attributes.Append(attr4);
            newNode.Attributes.Append(attr5);
            newNode.Attributes.Append(attr6);
            newNode.Attributes.Append(attr7);

            DataProvider.InsertNode(newNode, oldNode);
            DataProvider.RemoveNode(oldNode);

            DataProvider.Close();
        }
    }
}
