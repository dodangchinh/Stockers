using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Chinh_QuanLyKho
{
    public class ExportInventoryRepository : IRepository<ImportExport>
    {
        public List<ImportExport> lstExportInventories { get; set; }
        private List<Product> lstProduct { get; set; }
        public ExportInventoryRepository(List<Product> lstProduct)
        {
            lstExportInventories = new List<ImportExport>();
            this.lstProduct = lstProduct;
            Load();
        }
        void Load()
        {
            DataProvider.pathData = "data/Inventories/Inventory.xml";
            DataProvider.Open();

            string xPath = string.Format("//ExportInventory");

            XmlNodeList listNode = DataProvider.getDsNode(xPath);

            foreach (XmlNode item in listNode)
            {
                ImportExport exportInventory = new ImportExport();
                exportInventory.product = GetProduct(item.Attributes["IdProduct"].Value);
                exportInventory.Previous = int.Parse(item.Attributes["Previous"].Value);
                exportInventory.AmountPre = double.Parse(item.Attributes["AmountPre"].Value);
                exportInventory.Recent = int.Parse(item.Attributes["Recent"].Value);
                exportInventory.AmountRecent = double.Parse(item.Attributes["AmountRecent"].Value);
                exportInventory.ReceiptDate = DateTime.Parse(item.Attributes["InvoiceDate"].Value);
                exportInventory.Quantity = int.Parse(item.Attributes["Quantity"].Value);
                exportInventory.Total = double.Parse(item.Attributes["Total"].Value);
                lstExportInventories.Add(exportInventory);
            }
            DataProvider.Close();
        }

        Product GetProduct(string id)
        {
            foreach (var item in lstProduct)
                if (item.Id.ToLower().CompareTo(id.ToLower()) == 0)
                    return item;
            return null;
        }
        public void Add(ImportExport item)
        {
            lstExportInventories.Add(item);

            // save item in file book2.xml
            DataProvider.pathData = "data/Inventories/Inventory.xml";
            DataProvider.Open();

            XmlNode newNode = DataProvider.createNode("ExportInventory");
            // XmlNode newNode = doc.CreateElement("Book");
            XmlAttribute attr1 = DataProvider.createAttr("IdProduct");
            attr1.Value = item.product.Id;
            XmlAttribute attr2 = DataProvider.createAttr("Previous");
            attr2.Value = item.Previous.ToString();
            XmlAttribute attr3 = DataProvider.createAttr("AmountPre");
            attr3.Value = item.AmountPre.ToString();
            XmlAttribute attr4 = DataProvider.createAttr("Recent");
            attr4.Value = item.Recent.ToString();
            XmlAttribute attr5 = DataProvider.createAttr("AmountRecent");
            attr5.Value = item.AmountRecent.ToString();
            XmlAttribute attr6 = DataProvider.createAttr("InvoiceDate");
            attr6.Value = item.ReceiptDate.ToString("yyyy-MM-dd HH:mm:ss");
            XmlAttribute attr7 = DataProvider.createAttr("Quantity");
            attr7.Value = item.Quantity.ToString();
            XmlAttribute attr8 = DataProvider.createAttr("Total");
            attr8.Value = item.Total.ToString();

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);
            newNode.Attributes.Append(attr3);
            newNode.Attributes.Append(attr4);
            newNode.Attributes.Append(attr5);
            newNode.Attributes.Append(attr6);
            newNode.Attributes.Append(attr7);
            newNode.Attributes.Append(attr8);

            string xPath = string.Format("//ExportInventorys");
            XmlNode node = DataProvider.getNode(xPath);
            DataProvider.AppendNode(node, newNode);

            DataProvider.Close();
        }

        public void Delete(ImportExport item)
        {
            throw new NotImplementedException();
        }

        public ImportExport Get(string Id)
        {
            throw new NotImplementedException();
        }

        public List<ImportExport> Gets()
        {
            return lstExportInventories;
        }

        public void Update(ImportExport item)
        {
            // save item in file book2.xml
            DataProvider.pathData = "data/Inventories/Inventory.xml";
            DataProvider.Open();
            string xPath = string.Format("//ExportInventory[@IdProduct='{0}']", item.product.Id);
            XmlNode oldNode = DataProvider.getNode(xPath);

            XmlNode newNode = DataProvider.createNode("ExportInventory");
            // XmlNode newNode = doc.CreateElement("Book");
            XmlAttribute attr1 = DataProvider.createAttr("IdProduct");
            attr1.Value = item.product.Id;
            XmlAttribute attr2 = DataProvider.createAttr("Previous");
            attr2.Value = item.Previous.ToString();
            XmlAttribute attr3 = DataProvider.createAttr("AmountPre");
            attr3.Value = item.AmountPre.ToString();
            XmlAttribute attr4 = DataProvider.createAttr("Recent");
            attr4.Value = item.Recent.ToString();
            XmlAttribute attr5 = DataProvider.createAttr("AmountRecent");
            attr5.Value = item.AmountRecent.ToString();
            XmlAttribute attr6 = DataProvider.createAttr("InvoiceDate");
            attr6.Value = item.ReceiptDate.ToString("yyyy-MM-dd HH:mm:ss");
            XmlAttribute attr7 = DataProvider.createAttr("Quantity");
            attr7.Value = item.Quantity.ToString();
            XmlAttribute attr8 = DataProvider.createAttr("Total");
            attr8.Value = item.Total.ToString();

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);
            newNode.Attributes.Append(attr3);
            newNode.Attributes.Append(attr4);
            newNode.Attributes.Append(attr5);
            newNode.Attributes.Append(attr6);
            newNode.Attributes.Append(attr7);
            newNode.Attributes.Append(attr8);

            DataProvider.nodeRoot = DataProvider.getNode("//ExportInventorys");
            DataProvider.InsertNode(newNode, oldNode);
            DataProvider.RemoveNode(oldNode);

            DataProvider.Close();
        }
    }
}
