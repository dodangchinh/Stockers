using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Chinh_QuanLyKho
{
    public class RemainingProductRepository : IRepository<RemainingProduct>
    {
        public List<RemainingProduct> lstRemainingProducts { get; set; }
        public List<Product> lstProducts { get; set; }
        public RemainingProductRepository(List<Product> lstProducts)
        {
            lstRemainingProducts = new List<RemainingProduct>();
            this.lstProducts = lstProducts;
            Load();
        }
        void Load()
        {
            DataProvider.pathData = "data/Inventories/Inventory.xml";
            DataProvider.Open();

            string xPath = string.Format("//RemainingProduct");

            XmlNodeList listNode = DataProvider.getDsNode(xPath);

            foreach (XmlNode item in listNode)
            {
                RemainingProduct remainingProduct = new RemainingProduct();
                remainingProduct.product = GetProduct(item.Attributes["IdProduct"].Value);
                remainingProduct.Quantity = int.Parse(item.Attributes["Quantity"].Value);
                remainingProduct.QuantityExpDate = int.Parse(item.Attributes["QuantityExpDate"].Value);
                remainingProduct.QuantityRemain = int.Parse(item.Attributes["QuantityRemain"].Value);

                lstRemainingProducts.Add(remainingProduct);
            }
            DataProvider.Close();
        }
        Product GetProduct(string id)
        {
            foreach (var item in lstProducts)
                if (item.Id.ToLower().CompareTo(id.ToLower()) == 0)
                    return item;
            return null;
        }
        public void Add(RemainingProduct item)
        {
            lstRemainingProducts.Add(item);

            // save item in file book2.xml
            DataProvider.pathData = "data/Inventories/Inventory.xml";
            DataProvider.Open();

            XmlNode newNode = DataProvider.createNode("RemainingProduct");
            // XmlNode newNode = doc.CreateElement("Book");
            XmlAttribute attr1 = DataProvider.createAttr("IdProduct");
            attr1.Value = item.product.Id;
            XmlAttribute attr2 = DataProvider.createAttr("Quantity");
            attr2.Value = item.Quantity.ToString();
            XmlAttribute attr3 = DataProvider.createAttr("QuantityExpDate");
            attr3.Value = item.QuantityExpDate.ToString();
            XmlAttribute attr4 = DataProvider.createAttr("QuantityRemain");
            attr4.Value = item.QuantityRemain.ToString();

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);
            newNode.Attributes.Append(attr3);
            newNode.Attributes.Append(attr4);

            string xPath = string.Format("//RemainingProducts");
            XmlNode node = DataProvider.getNode(xPath);
            DataProvider.AppendNode(node, newNode);

            DataProvider.Close();
        }


        public void Delete(RemainingProduct item)
        {
            throw new NotImplementedException();
        }

        public RemainingProduct Get(string Id)
        {
            throw new NotImplementedException();
        }

        public List<RemainingProduct> Gets()
        {
            return lstRemainingProducts;
        }

        public void Update(RemainingProduct item)
        {
            // save item in file book2.xml
            DataProvider.pathData = "data/Inventories/Inventory.xml";
            DataProvider.Open();
            string xPath = string.Format("//RemainingProduct[@IdProduct='{0}']", item.product.Id);
            XmlNode oldNode = DataProvider.getNode(xPath);

            XmlNode newNode = DataProvider.createNode("RemainingProduct");
            // XmlNode newNode = doc.CreateElement("Book");
            XmlAttribute attr1 = DataProvider.createAttr("IdProduct");
            attr1.Value = item.product.Id;
            XmlAttribute attr2 = DataProvider.createAttr("Quantity");
            attr2.Value = item.Quantity.ToString();
            XmlAttribute attr3 = DataProvider.createAttr("QuantityExpDate");
            attr3.Value = item.QuantityExpDate.ToString();
            XmlAttribute attr4 = DataProvider.createAttr("QuantityRemain");
            attr4.Value = item.QuantityRemain.ToString();

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);
            newNode.Attributes.Append(attr3);
            newNode.Attributes.Append(attr4);

            DataProvider.nodeRoot = DataProvider.getNode("//RemainingProducts");
            DataProvider.InsertNode(newNode, oldNode);
            DataProvider.RemoveNode(oldNode);

            DataProvider.Close();
        }
    }
}