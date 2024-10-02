using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Chinh_QuanLyKho
{
    public class ExportReceiptRepository : IRepository<ImportExportReceipt>
    {
        public List<ImportExportReceipt> lstExportReceipts { get; set; }
        public List<Product> lstProduct { get; set; }

        public ExportReceiptRepository(List<Product> lstProduct)
        {
            lstExportReceipts = new List<ImportExportReceipt>();
            this.lstProduct = lstProduct;
            Load();
        }

        void Load()
        {
            DataProvider.pathData = "data/Receipts/ExportReceipt.xml";
            DataProvider.Open();

            string xPath = "//Stocker";
            XmlNodeList listNode = DataProvider.getDsNode(xPath);

            foreach (XmlNode item in listNode)
            {
                ImportExportReceipt importReceipt = new ImportExportReceipt();
                importReceipt.Id = item.Attributes["Id"].Value;
                importReceipt.Name = item.Attributes["Name"].Value;
                importReceipt.Total = int.Parse(item.Attributes["Total"].Value);
                importReceipt.ReceiptDate = DateTime.Parse(item.Attributes["ReceiptDate"].Value);

                xPath = string.Format("//Stocker[@Id='{0}']/Detail", importReceipt.Id);
                XmlNodeList listNodeDetail = DataProvider.getDsNode(xPath);


                foreach (XmlNode itemDetail in listNodeDetail)
                {
                    Receipt receipt = new Receipt();
                    receipt.Quantity = int.Parse(itemDetail.Attributes["Quantity"].Value);
                    receipt.product = GetProduct(itemDetail.Attributes["IdProduct"].Value);

                    importReceipt.lstReceipts.Add(receipt);
                }

                lstExportReceipts.Add(importReceipt);
                Parameter.nExportReceipt++;
            }

            DataProvider.Close();
        }


        Product GetProduct(string id)
        {
            foreach (var item in lstProduct)
                if (item.Id.ToLower().Contains(id.ToLower()))
                    return item;
            return null;
        }
        public void Add(ImportExportReceipt item)
        {
            lstExportReceipts.Add(item);

            // save item in file book2.xml
            DataProvider.pathData = "data/Receipts/ExportReceipt.xml";
            DataProvider.Open();

            XmlNode newNode = DataProvider.createNode("Stocker");
            // XmlNode newNode = doc.CreateElement("Book");
            XmlAttribute attr1 = DataProvider.createAttr("Id");
            attr1.Value = item.Id;
            XmlAttribute attr2 = DataProvider.createAttr("Name");
            attr2.Value = item.Name;
            XmlAttribute attr3 = DataProvider.createAttr("Total");
            attr3.Value = item.Total.ToString();
            XmlAttribute attr4 = DataProvider.createAttr("ReceiptDate");
            attr4.Value = item.ReceiptDate.ToString("yyyy-MM-dd HH:mm:ss");

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);
            newNode.Attributes.Append(attr3);
            newNode.Attributes.Append(attr4);

            string xPath = string.Format("//ExportReceipts");
            XmlNode node = DataProvider.getNode(xPath);
            DataProvider.AppendNode(node, newNode);
            DataProvider.Close();

            // save item in file book2.xml
            DataProvider.pathData = "data/Receipts/ExportReceipt.xml";
            DataProvider.Open();
            int i = 0;
            foreach (var tempDetail in item.lstReceipts)
            {
                XmlNode newNode2 = DataProvider.createNode("Detail");
                // XmlNode newNode = doc.CreateElement("Book");
                XmlAttribute attr5 = DataProvider.createAttr("IdProduct");
                attr5.Value = tempDetail.product.Id;
                XmlAttribute attr6 = DataProvider.createAttr("Quantity");
                attr6.Value = tempDetail.Quantity.ToString();

                newNode2.Attributes.Append(attr5);
                newNode2.Attributes.Append(attr6);

                xPath = string.Format("//Stocker[@Id='{0}']", item.Id);
                XmlNode node1 = DataProvider.getNode(xPath);
                DataProvider.AppendNode(node1, newNode2);
                i++;
            }

            DataProvider.Close();
        }

        public void Delete(ImportExportReceipt item)
        {
            throw new NotImplementedException();
        }

        public ImportExportReceipt Get(string Id)
        {
            throw new NotImplementedException();
        }

        public List<ImportExportReceipt> Gets()
        {
            return lstExportReceipts;
        }

        public void Update(ImportExportReceipt item)
        {
            throw new NotImplementedException();
        }
    }
}
