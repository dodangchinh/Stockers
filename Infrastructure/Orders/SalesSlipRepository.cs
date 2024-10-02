using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Chinh_QuanLyKho
{
    public class SalesSlipRepository : IRepository<SalesSlip>
    {
        public List<SalesSlip> lstSaleSlip {  get; set; }
        private List<Product> lstProduct { get; set; }  

        public SalesSlipRepository(List<Product> lstProduct)
        {
            lstSaleSlip = new List<SalesSlip>();
            this.lstProduct = lstProduct;
            Load();
        }

        void Load()
        {
            DataProvider.pathData = "data/Orders/SalesSlip.xml";
            DataProvider.Open();

            string xPath = "//SalesSlip";
            XmlNodeList listNode = DataProvider.getDsNode(xPath);

            foreach (XmlNode item in listNode)
            {
                SalesSlip saleSlip = new SalesSlip();
                saleSlip.Id = item.Attributes["Id"].Value;
                saleSlip.UserName = item.Attributes["UserName"].Value;
                saleSlip.IdCard = int.Parse(item.Attributes["IdCard"].Value);
                saleSlip.NameCustomer = item.Attributes["Customer"].Value;
                saleSlip.PhoneNumber = item.Attributes["PhoneNumber"].Value;
                saleSlip.Address = item.Attributes["Address"].Value;
                saleSlip.Quantity = int.Parse(item.Attributes["Quantity"].Value);
                saleSlip.Total = double.Parse(item.Attributes["Total"].Value);
                saleSlip.Discount = double.Parse(item.Attributes["Discount"].Value);
                saleSlip.TotalPay = double.Parse(item.Attributes["TotalPay"].Value);
                saleSlip.CreateAt = DateTime.Parse(item.Attributes["CreateAt"].Value);

                xPath = string.Format("//SalesSlip[@Id='{0}']/SalesSlipDetail", saleSlip.Id);
                XmlNodeList listNodeDetail = DataProvider.getDsNode(xPath);

                foreach (XmlNode itemDetail in listNodeDetail)
                {
                    SalesSlipDetail saleSlipDetail = new SalesSlipDetail();
                    saleSlipDetail.Quantity = int.Parse(itemDetail.Attributes["Quantity"].Value);
                    saleSlipDetail.Discount = double.Parse(itemDetail.Attributes["Discount"].Value);
                    saleSlipDetail.product = GetProduct(itemDetail.Attributes["IdProduct"].Value);
                    saleSlip.lstDetail.Add(saleSlipDetail);
                }

                lstSaleSlip.Add(saleSlip);
                Parameter.nSaleSlip++;
            }

            DataProvider.Close();
        }

        Product GetProduct(string Id)
        {
            foreach (var item in lstProduct)
                if (item.Id.ToLower().CompareTo(Id.ToLower()) == 0)
                    return item;
            return null;
        }

        public void Add(SalesSlip item)
        {
            lstSaleSlip.Add(item);

            // save item in file book2.xml
            DataProvider.pathData = "data/Orders/SalesSlip.xml";
            DataProvider.Open();

            XmlNode newNode = DataProvider.createNode("SalesSlip");
            // XmlNode newNode = doc.CreateElement("Book");
            XmlAttribute attr1 = DataProvider.createAttr("Id");
            attr1.Value = item.Id;
            XmlAttribute attr2 = DataProvider.createAttr("UserName");
            attr2.Value = item.UserName;
            XmlAttribute attr3 = DataProvider.createAttr("Customer");
            attr3.Value = item.NameCustomer;
            XmlAttribute attr4 = DataProvider.createAttr("PhoneNumber");
            attr4.Value = item.PhoneNumber;
            XmlAttribute attr5 = DataProvider.createAttr("IdCard");
            attr5.Value = item.IdCard.ToString();
            XmlAttribute attr6 = DataProvider.createAttr("Address");
            attr6.Value = item.Address;
            XmlAttribute attr7 = DataProvider.createAttr("Quantity");
            attr7.Value = item.Quantity.ToString();
            XmlAttribute attr8 = DataProvider.createAttr("Total");
            attr8.Value = item.Total.ToString();
            XmlAttribute attr9 = DataProvider.createAttr("Discount");
            attr9.Value = item.Discount.ToString();
            XmlAttribute attr10 = DataProvider.createAttr("TotalPay");
            attr10.Value = item.TotalPay.ToString();
            XmlAttribute attr11 = DataProvider.createAttr("CreateAt");
            attr11.Value = item.CreateAt.ToString("dd/MM/yyy HH:mm:ss");

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);
            newNode.Attributes.Append(attr3);
            newNode.Attributes.Append(attr4);
            newNode.Attributes.Append(attr5);
            newNode.Attributes.Append(attr6);
            newNode.Attributes.Append(attr7);
            newNode.Attributes.Append(attr8);
            newNode.Attributes.Append(attr9);
            newNode.Attributes.Append(attr10);
            newNode.Attributes.Append(attr11);

            string xPath = string.Format("//SalesSlips");
            XmlNode node = DataProvider.getNode(xPath);
            DataProvider.AppendNode(node, newNode);
            DataProvider.Close();

            // save item in file book2.xml
            DataProvider.pathData = "data/Orders/SalesSlip.xml";
            DataProvider.Open();
            int i = 0;
            foreach (var tempDetail in item.lstDetail)
            {
                XmlNode newNode2 = DataProvider.createNode("SalesSlipDetail");
                // XmlNode newNode = doc.CreateElement("Book");
                XmlAttribute attr12 = DataProvider.createAttr("IdProduct");
                attr12.Value = tempDetail.product.Id;
                XmlAttribute attr13 = DataProvider.createAttr("Name");
                attr13.Value = tempDetail.product.Name;
                XmlAttribute attr14 = DataProvider.createAttr("Category");
                attr14.Value = tempDetail.product.Category;
                XmlAttribute attr15 = DataProvider.createAttr("PriceInput");
                attr15.Value = tempDetail.product.PriceInput.ToString();
                XmlAttribute attr16 = DataProvider.createAttr("PriceOutput");
                attr16.Value = tempDetail.product.PriceOutput.ToString();
                XmlAttribute attr17 = DataProvider.createAttr("Quantity");
                attr17.Value = tempDetail.Quantity.ToString();
                XmlAttribute attr18 = DataProvider.createAttr("Discount");
                attr18.Value = tempDetail.Discount.ToString();

                newNode2.Attributes.Append(attr12);
                newNode2.Attributes.Append(attr13);
                newNode2.Attributes.Append(attr14);
                newNode2.Attributes.Append(attr15);
                newNode2.Attributes.Append(attr16);
                newNode2.Attributes.Append(attr17); 
                newNode2.Attributes.Append(attr18);

                xPath = string.Format("//SalesSlip[@Id='{0}']", item.Id);
                XmlNode node1 = DataProvider.getNode(xPath);
                DataProvider.AppendNode(node1, newNode2);
                i++;
            }

            DataProvider.Close();
        }

        public void Delete(SalesSlip item)
        {
            throw new NotImplementedException();
        }

        public SalesSlip Get(string Id)
        {
            throw new NotImplementedException();
        }

        public List<SalesSlip> Gets()
        {
            return lstSaleSlip;
        }

        public void Update(SalesSlip item)
        {
            // save item in file book2.xml
            DataProvider.pathData = "data/Orders/SalesSlip.xml";
            DataProvider.Open();
            string xPath = string.Format("//SalesSlip[@Id='{0}']", item.Id);
            XmlNode oldNode = DataProvider.getNode(xPath);

            XmlNode newNode = DataProvider.createNode("SalesSlip");
            // XmlNode newNode = doc.CreateElement("Book");
            XmlAttribute attr1 = DataProvider.createAttr("Id");
            attr1.Value = item.Id;
            XmlAttribute attr2 = DataProvider.createAttr("UserName");
            attr2.Value = item.UserName;
            XmlAttribute attr3 = DataProvider.createAttr("Customer");
            attr3.Value = item.NameCustomer;
            XmlAttribute attr4 = DataProvider.createAttr("PhoneNumber");
            attr4.Value = item.PhoneNumber;
            XmlAttribute attr5 = DataProvider.createAttr("IdCard");
            attr5.Value = item.IdCard.ToString();
            XmlAttribute attr6 = DataProvider.createAttr("Address");
            attr6.Value = item.Address;
            XmlAttribute attr7 = DataProvider.createAttr("Quantity");
            attr7.Value = item.Quantity.ToString();
            XmlAttribute attr8 = DataProvider.createAttr("Total");
            attr8.Value = item.Total.ToString();
            XmlAttribute attr9 = DataProvider.createAttr("Discount");
            attr9.Value = item.Discount.ToString();
            XmlAttribute attr10 = DataProvider.createAttr("TotalPay");
            attr10.Value = item.TotalPay.ToString();
            XmlAttribute attr11 = DataProvider.createAttr("CreateAt");
            attr11.Value = item.CreateAt.ToString("dd/MM/yyy HH:mm:ss");

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);
            newNode.Attributes.Append(attr3);
            newNode.Attributes.Append(attr4);
            newNode.Attributes.Append(attr5);
            newNode.Attributes.Append(attr6);
            newNode.Attributes.Append(attr7);
            newNode.Attributes.Append(attr8);
            newNode.Attributes.Append(attr9);
            newNode.Attributes.Append(attr10);
            newNode.Attributes.Append(attr11);

            xPath = string.Format("//SalesSlips");
            XmlNode node = DataProvider.getNode(xPath);
            DataProvider.InsertNode(newNode, oldNode);
            DataProvider.RemoveNode(oldNode);

            // save item in file book2.xml
            DataProvider.pathData = "data/Orders/SalesSlip.xml";
            DataProvider.Open();
            int i = 0;
            foreach (var tempDetail in item.lstDetail)
            {
                XmlNode newNode2 = DataProvider.createNode("SalesSlipDetail");
                // XmlNode newNode = doc.CreateElement("Book");
                XmlAttribute attr12 = DataProvider.createAttr("IdProduct");
                attr12.Value = tempDetail.product.Id;
                XmlAttribute attr13 = DataProvider.createAttr("Name");
                attr13.Value = tempDetail.product.Name;
                XmlAttribute attr14 = DataProvider.createAttr("Category");
                attr14.Value = tempDetail.product.Category;
                XmlAttribute attr15 = DataProvider.createAttr("PriceInput");
                attr15.Value = tempDetail.product.PriceInput.ToString();
                XmlAttribute attr16 = DataProvider.createAttr("PriceOutput");
                attr16.Value = tempDetail.product.PriceOutput.ToString();
                XmlAttribute attr17 = DataProvider.createAttr("Quantity");
                attr17.Value = tempDetail.Quantity.ToString();
                XmlAttribute attr18 = DataProvider.createAttr("Discount");
                attr18.Value = tempDetail.Discount.ToString();

                newNode2.Attributes.Append(attr12);
                newNode2.Attributes.Append(attr13);
                newNode2.Attributes.Append(attr14);
                newNode2.Attributes.Append(attr15);
                newNode2.Attributes.Append(attr16);
                newNode2.Attributes.Append(attr17);
                newNode2.Attributes.Append(attr18);

                xPath = string.Format("//SalesSlip[@Id='{0}']", item.Id);
                XmlNode node1 = DataProvider.getNode(xPath);
                DataProvider.AppendNode(node1, newNode2);
                i++;
            }

            DataProvider.Close();
        }
    }
}
