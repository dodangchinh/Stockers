using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Chinh_QuanLyKho
{
    public class OutOfStockRepository : IRepository<OutOfStock>
    {
        public List<OutOfStock> lstOutOfStocks {  get; set; }
        public List<Product> lstProducts { get; set; }
        public OutOfStockRepository(List<Product> lstProducts)
        {
            lstOutOfStocks = new List<OutOfStock>();
            this.lstProducts = lstProducts;
            Load();
        }
        public void Load()
        {
            DataProvider.pathData = "data/Inventories/OutOfStock.xml";
            DataProvider.Open();

            string xPath = string.Format("//OutOfStock");

            XmlNodeList listNode = DataProvider.getDsNode(xPath);

            foreach (XmlNode item in listNode)
            {
                OutOfStock outOfStock = new OutOfStock();
                outOfStock.product = GetProduct(item.Attributes["IdProduct"].Value);
                outOfStock.Remaining = int.Parse(item.Attributes["Remaining"].Value);
                lstOutOfStocks.Add(outOfStock);
            }
            DataProvider.Close();
        }

        Product GetProduct(string id)
        {
            foreach (var item in lstProducts)
                if(item.Id.ToLower().CompareTo(id.ToLower()) == 0)
                    return item;
            return null;
        }

        public void Add(OutOfStock item)
        {
            lstOutOfStocks.Add(item);

            // save item in file book2.xml
            DataProvider.pathData = "data/Inventories/OutOfStock.xml";
            DataProvider.Open();

            XmlNode newNode = DataProvider.createNode("OutOfStock");
            // XmlNode newNode = doc.CreateElement("Book");
            XmlAttribute attr1 = DataProvider.createAttr("IdProduct");
            attr1.Value = item.product.Id;
            XmlAttribute attr2 = DataProvider.createAttr("NameProduct");
            attr2.Value = item.product.Name;
            XmlAttribute attr3 = DataProvider.createAttr("Remaining");
            attr3.Value = item.Remaining.ToString();

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);
            newNode.Attributes.Append(attr3);

            string xPath = string.Format("//OutOfStocks");
            XmlNode node = DataProvider.getNode(xPath);
            DataProvider.AppendNode(node, newNode);

            DataProvider.Close();
        }

        public void Delete(OutOfStock item)
        {
            lstOutOfStocks.Remove(item);
            // save item in file book2.xml
            DataProvider.pathData = "data/Inventories/OutOfStock.xml";
            DataProvider.Open();
            string xPath = string.Format("//OutOfStock[@IdProduct='{0}']", item.product.Id);
            XmlNode oldNode = DataProvider.getNode(xPath);

            DataProvider.RemoveNode(oldNode);
            DataProvider.Close();
        }

        public OutOfStock Get(string Id)
        {
            throw new NotImplementedException();
        }

        public List<OutOfStock> Gets()
        {
            return lstOutOfStocks;
        }

        public void Update(OutOfStock item)
        {
            // save item in file book2.xml
            DataProvider.pathData = "data/Inventories/OutOfStock.xml";
            DataProvider.Open();
            string xPath = string.Format("//OutOfStock[@IdProduct='{0}']", item.product.Id);
            XmlNode oldNode = DataProvider.getNode(xPath);

            XmlNode newNode = DataProvider.createNode("OutOfStock");
            // XmlNode newNode = doc.CreateElement("Book");
            XmlAttribute attr1 = DataProvider.createAttr("IdProduct");
            attr1.Value = item.product.Id;
            XmlAttribute attr2 = DataProvider.createAttr("NameProduct");
            attr2.Value = item.product.Name;
            XmlAttribute attr3 = DataProvider.createAttr("Remaining");
            attr3.Value = item.Remaining.ToString();

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);
            newNode.Attributes.Append(attr3);

            DataProvider.InsertNode(newNode, oldNode);
            DataProvider.RemoveNode(oldNode);

            DataProvider.Close();
        }
    }
}
