using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Chinh_QuanLyKho
{
    public class InventorySaleRepository : IRepository<InventorySale>
    {
        public List<InventorySale> lstInventorySales {  get; set; }
        private List<ImportExport> lstExports { get; set; }
        public List<Product> lstProducts { get; set; }
        public InventorySaleRepository(List<ImportExport> lstExports, List<Product> lstProducts)
        {
            lstInventorySales = new List<InventorySale>();
            this.lstExports = lstExports;
            this.lstProducts = lstProducts;
            Load();          
        }

        void Load()
        {
            DataProvider.pathData = "data/Orders/InventorySale.xml";
            DataProvider.Open();

            string xPath = string.Format("//InventorySale");

            XmlNodeList listNode = DataProvider.getDsNode(xPath);

            foreach (XmlNode item in listNode)
            {
                int quantitySold = int.Parse(item.Attributes["QuantitySold"].Value);
                int remaining = int.Parse(item.Attributes["Remaining"].Value);
                InventorySale inventorySale = new InventorySale(quantitySold, remaining, GetByProduct(item.Attributes["IdProduct"].Value), GetProduct(item.Attributes["IdProduct"].Value));
                lstInventorySales.Add(inventorySale);
            }
            DataProvider.Close();
        }

        Product GetProduct(string idProduct)
        {
            foreach (var item in lstProducts)
                if (item.Id.ToLower().CompareTo(idProduct.ToLower()) == 0)
                    return item;
            return null;
        }

        ImportExport GetByProduct(string idProduct)
        {
            foreach (var item in lstExports)
                if (item.product.Id.ToLower().CompareTo(idProduct.ToLower()) == 0)
                    return item;
            return null;
        }

        public void Add(InventorySale item)
        {
            lstInventorySales.Add(item);

            // save item in file book2.xml
            DataProvider.pathData = "data/Orders/InventorySale.xml";
            DataProvider.Open();

            XmlNode newNode = DataProvider.createNode("InventorySale");
            // XmlNode newNode = doc.CreateElement("Book");
            XmlAttribute attr1 = DataProvider.createAttr("IdProduct");
            attr1.Value = item.IdProduct;
            XmlAttribute attr2 = DataProvider.createAttr("QuantityInvoice");
            attr2.Value = item.QuantityInvoice.ToString();
            XmlAttribute attr3 = DataProvider.createAttr("PriceOutput");
            attr3.Value = item.PriceOutput.ToString();
            XmlAttribute attr4 = DataProvider.createAttr("QuantitySold");
            attr4.Value = item.QuantitySold.ToString();
            XmlAttribute attr5 = DataProvider.createAttr("Remaining");
            attr5.Value = item.Remaining.ToString();

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);
            newNode.Attributes.Append(attr3);
            newNode.Attributes.Append(attr4);
            newNode.Attributes.Append(attr5);

            string xPath = string.Format("//InventorySales");
            XmlNode node = DataProvider.getNode(xPath);
            DataProvider.AppendNode(node, newNode);

            DataProvider.Close();
        }

        public void Delete(InventorySale item)
        {
            throw new NotImplementedException();
        }

        public InventorySale Get(string Id)
        {
            throw new NotImplementedException();
        }

        public List<InventorySale> Gets()
        {
            return lstInventorySales;
        }

        public void Update(InventorySale item)
        {
            // save item in file book2.xml
            DataProvider.pathData = "data/Orders/InventorySale.xml";
            DataProvider.Open();
            string xPath = string.Format("//InventorySale[@IdProduct='{0}']", item.IdProduct);
            XmlNode oldNode = DataProvider.getNode(xPath);

            XmlNode newNode = DataProvider.createNode("InventorySale");
            // XmlNode newNode = doc.CreateElement("Book");
            XmlAttribute attr1 = DataProvider.createAttr("IdProduct");
            attr1.Value = item.IdProduct;
            XmlAttribute attr2 = DataProvider.createAttr("QuantityInvoice");
            attr2.Value = item.QuantityInvoice.ToString();
            XmlAttribute attr3 = DataProvider.createAttr("PriceOutput");
            attr3.Value = item.PriceOutput.ToString();
            XmlAttribute attr4 = DataProvider.createAttr("QuantitySold");
            attr4.Value = item.QuantitySold.ToString();
            XmlAttribute attr5 = DataProvider.createAttr("Remaining");
            attr5.Value = item.Remaining.ToString();

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);
            newNode.Attributes.Append(attr3);
            newNode.Attributes.Append(attr4);
            newNode.Attributes.Append(attr5);

            DataProvider.InsertNode(newNode, oldNode);
            DataProvider.RemoveNode(oldNode);

            DataProvider.Close();
        }
    }
}
