using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Chinh_QuanLyKho
{
    public class ImportInventoryRepository : IRepository<ImportExport>
    {
        public List<ImportExport> lstImportInventories { get; set; }
        private List<Product> lstProduct {  get; set; } 

        public ImportInventoryRepository(List<Product> lstProduct)
        {
            lstImportInventories = new List<ImportExport>();
            this.lstProduct = lstProduct;
            Load();
        }

        void Load()
        {
            DataProvider.pathData = "data/Inventories/Inventory.xml";
            DataProvider.Open();

            string xPath = string.Format("//ImportInventory");

            XmlNodeList listNode = DataProvider.getDsNode(xPath);

            foreach (XmlNode item in listNode)
            {
                ImportExport importInventory = new ImportExport();
                importInventory.product = GetProduct(item.Attributes["IdProduct"].Value);
                importInventory.Previous = int.Parse(item.Attributes["Previous"].Value);
                importInventory.AmountPre = double.Parse(item.Attributes["AmountPre"].Value);
                importInventory.Recent = int.Parse(item.Attributes["Recent"].Value);
                importInventory.AmountRecent = double.Parse(item.Attributes["AmountRecent"].Value);
                importInventory.ReceiptDate = DateTime.Parse(item.Attributes["ReceiptDate"].Value);
                importInventory.Quantity = int.Parse(item.Attributes["Quantity"].Value);
                importInventory.Total = double.Parse(item.Attributes["Total"].Value);
                lstImportInventories.Add(importInventory);
            }
            DataProvider.Close();
        }

        Product GetProduct(string id)
        {
            foreach (var item in lstProduct)
                if(item.Id.ToLower().CompareTo(id.ToLower()) == 0)
                    return item;
            return null;
        }

        public void Add(ImportExport item)
        {
            lstImportInventories.Add(item);

            // save item in file book2.xml
            DataProvider.pathData = "data/Inventories/Inventory.xml";
            DataProvider.Open();

            XmlNode newNode = DataProvider.createNode("ImportInventory");
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
            XmlAttribute attr6 = DataProvider.createAttr("ReceiptDate");
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

            string xPath = string.Format("//ImportInventorys");
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
            return lstImportInventories;
        }

        public void Update(ImportExport item)
        {
            // save item in file book2.xml
            DataProvider.pathData = "data/Inventories/Inventory.xml";
            DataProvider.Open();
            string xPath = string.Format("//ImportInventory[@IdProduct='{0}']", item.product.Id);
            XmlNode oldNode = DataProvider.getNode(xPath);

            XmlNode newNode = DataProvider.createNode("ImportInventory");
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
            XmlAttribute attr6 = DataProvider.createAttr("ReceiptDate");
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

            DataProvider.nodeRoot = DataProvider.getNode("//ImportInventorys");
            DataProvider.InsertNode(newNode, oldNode);
            DataProvider.RemoveNode(oldNode);

            DataProvider.Close();
        }
    }
}
