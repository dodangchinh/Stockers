using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Chinh_QuanLyKho
{
    public class ExportDateRepository : IRepository<ExpDate>
    {
        public List<ExpDate> lstExportDate {  get; set; }
        public List<Product> lstProduct { get; set; }
        public ExportDateRepository(List<Product> lstProduct)
        {
            this.lstExportDate = new List<ExpDate>();
            this.lstProduct = lstProduct;
            Load();
        }

        void Load()
        {
            DataProvider.pathData = "data/Inventories/ExpDate.xml";
            DataProvider.Open();

            string xPath = string.Format("//Exports/ExpDateDetail");

            XmlNodeList listNode = DataProvider.getDsNode(xPath);

            foreach (XmlNode item in listNode)
            {
                ExpDate expDate = new ExpDate();
                expDate.Id = item.Attributes["Id"].Value;
                expDate.product = GetProduct(item.Attributes["IdProduct"].Value);
                expDate.IdReceipt = item.Attributes["IdReceipt"].Value;
                expDate.Quantity = int.Parse(item.Attributes["Quantity"].Value);
                expDate.QuantityUsed = int.Parse(item.Attributes["QuantityUsed"].Value);
                expDate.MfgDate = DateTime.Parse(item.Attributes["MfgDate"].Value);
                expDate.ExpDates = DateTime.Parse(item.Attributes["ExpDate"].Value);
                Parameter.nExportDate++;
                lstExportDate.Add(expDate);
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

        public void Add(ExpDate item)
        {
            lstExportDate.Add(item);

            // save item in file book2.xml
            DataProvider.pathData = "data/Inventories/ExpDate.xml";
            DataProvider.Open();

            XmlNode newNode = DataProvider.createNode("ExpDateDetail");
            // XmlNode newNode = doc.CreateElement("Book");
            XmlAttribute attr1 = DataProvider.createAttr("Id");
            attr1.Value = item.Id;
            XmlAttribute attr2 = DataProvider.createAttr("IdReceipt");
            attr2.Value = item.IdReceipt;
            XmlAttribute attr3 = DataProvider.createAttr("IdProduct");
            attr3.Value = item.product.Id;
            XmlAttribute attr4 = DataProvider.createAttr("NameProduct");
            attr4.Value = item.product.Name;
            XmlAttribute attr5 = DataProvider.createAttr("Quantity");
            attr5.Value = item.Quantity.ToString();
            XmlAttribute attr6 = DataProvider.createAttr("QuantityUsed");
            attr6.Value = item.QuantityUsed.ToString();
            XmlAttribute attr7 = DataProvider.createAttr("MfgDate");
            attr7.Value = item.MfgDate.ToString("yyyy-MM-dd HH:mm:ss");
            XmlAttribute attr8 = DataProvider.createAttr("ExpDate");
            attr8.Value = item.ExpDates.ToString("yyyy-MM-dd HH:mm:ss");

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);
            newNode.Attributes.Append(attr3);
            newNode.Attributes.Append(attr4);
            newNode.Attributes.Append(attr5);
            newNode.Attributes.Append(attr6);
            newNode.Attributes.Append(attr7);
            newNode.Attributes.Append(attr8);

            string xPath = string.Format("//Exports");
            XmlNode node = DataProvider.getNode(xPath);
            DataProvider.AppendNode(node, newNode);

            DataProvider.Close();
        }

        public void Delete(ExpDate item)
        {
            lstExportDate.Remove(item);
            // save item in file book2.xml
            DataProvider.pathData = "data/Inventories/ExpDate.xml";
            DataProvider.Open();
            string xPath = string.Format("//Exports/ExpDateDetail[@Id='{0}']", item.Id);
            XmlNode oldNode = DataProvider.getNode(xPath);

            DataProvider.RemoveNode(oldNode);
            DataProvider.Close();
        }

        public ExpDate Get(string Id)
        {
            throw new NotImplementedException();
        }

        public List<ExpDate> Gets()
        {
            return lstExportDate;
        }

        public void Update(ExpDate item)
        {
            // save item in file book2.xml
            DataProvider.pathData = "data/Inventories/ExpDate.xml";
            DataProvider.Open();
            string xPath = string.Format("//Exports/ExpDateDetail[@Id='{0}']", item.Id);
            XmlNode oldNode = DataProvider.getNode(xPath);

            XmlNode newNode = DataProvider.createNode("ExpDateDetail");
            // XmlNode newNode = doc.CreateElement("Book");
            XmlAttribute attr1 = DataProvider.createAttr("Id");
            attr1.Value = item.Id;
            XmlAttribute attr2 = DataProvider.createAttr("IdProduct");
            attr2.Value = item.product.Id;
            XmlAttribute attr3 = DataProvider.createAttr("IdReceipt");
            attr3.Value = item.IdReceipt;
            XmlAttribute attr4 = DataProvider.createAttr("NameProduct");
            attr4.Value = item.product.Name;
            XmlAttribute attr5 = DataProvider.createAttr("Quantity");
            attr5.Value = item.Quantity.ToString();
            XmlAttribute attr6 = DataProvider.createAttr("QuantityUsed");
            attr6.Value = item.QuantityUsed.ToString();
            XmlAttribute attr7 = DataProvider.createAttr("MfgDate");
            attr7.Value = item.MfgDate.ToString("yyyy-MM-dd HH:mm:ss");
            XmlAttribute attr8 = DataProvider.createAttr("ExpDate");
            attr8.Value = item.ExpDates.ToString("yyyy-MM-dd HH:mm:ss");

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);
            newNode.Attributes.Append(attr3);
            newNode.Attributes.Append(attr4);
            newNode.Attributes.Append(attr5);
            newNode.Attributes.Append(attr6);
            newNode.Attributes.Append(attr7);
            newNode.Attributes.Append(attr8);

            DataProvider.nodeRoot = DataProvider.getNode("//Exports");
            DataProvider.InsertNode(newNode, oldNode);
            DataProvider.RemoveNode(oldNode);

            DataProvider.Close();
        }
    }
}
