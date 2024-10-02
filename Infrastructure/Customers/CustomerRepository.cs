using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Chinh_QuanLyKho
{
    public class CustomerRepository : IRepository<Customer>
    {
        public List<Customer> lstCustomer { get; set; }
        public List<Cards> lstCards { get; set; }
        public List<SalesSlip> lstSalesSlips { get; set; }

        public CustomerRepository( List<Cards> lstCards, List<SalesSlip> lstSalesSlips)
        {
            lstCustomer = new List<Customer>();
            this.lstCards = lstCards;
            this.lstSalesSlips = lstSalesSlips;
            Load();
         
        }

        void Load()
        {
            DataProvider.pathData = "data/Customers/Customers.xml";
            DataProvider.Open();

            string xPath = "//Customer";
            XmlNodeList listNode = DataProvider.getDsNode(xPath);

            foreach (XmlNode item in listNode)
            {
                Customer customer = new Customer();
                customer.No = int.Parse(item.Attributes["No"].Value);
                customer.Name = item.Attributes["Name"].Value;
                customer.PhoneNumber = item.Attributes["PhoneNumber"].Value;
                customer.Address = item.Attributes["Address"].Value;
                customer.TotalScore = double.Parse(item.Attributes["TotalScore"].Value);
                customer.Cards = Get(int.Parse(item.Attributes["IdCard"].Value));

                xPath = string.Format("//Customer[@No='{0}']/CustomerDetail", customer.No);
                XmlNodeList listNodeDetail = DataProvider.getDsNode(xPath);

                foreach (XmlNode itemDetail in listNodeDetail)
                {
                    CustomerDetail customerDetail = new CustomerDetail();
                    customerDetail.IdSaleSlip = itemDetail.Attributes["IdSaleSlip"].Value;
                    customerDetail.SalesSlip = GetSaleSlip(customerDetail.IdSaleSlip);
                    customerDetail.Quantity = int.Parse(itemDetail.Attributes["Quantity"].Value);
                    customerDetail.Total = double.Parse(itemDetail.Attributes["Total"].Value);
                    customerDetail.Score = double.Parse(itemDetail.Attributes["Score"].Value);
                    customerDetail.CreateAt = DateTime.Parse(itemDetail.Attributes["CreateAt"].Value);
                    customer.lstDetails.Add(customerDetail);    
                }

                lstCustomer.Add(customer);
                Parameter.nCustomer++;
            }

            DataProvider.Close();
        }

        Cards Get(int id)
        {
            foreach (var item in lstCards)
                if (item.Id == id)
                    return item;
            return null;
        }

        SalesSlip GetSaleSlip(string id)
        {
            foreach (var item in lstSalesSlips)
                if (item.Id.ToLower().CompareTo(id.ToLower()) == 0)
                    return item;
            return null;
        }

        public void Add(Customer item)
        {
            lstCustomer.Add(item);

            // save item in file book2.xml
            DataProvider.pathData = "data/Customers/Customers.xml";
            DataProvider.Open();

            XmlNode newNode = DataProvider.createNode("Customer");
            // XmlNode newNode = doc.CreateElement("Book");
            XmlAttribute attr1 = DataProvider.createAttr("No");
            attr1.Value = item.No.ToString();
            XmlAttribute attr2 = DataProvider.createAttr("Name");
            attr2.Value = item.Name;
            XmlAttribute attr3 = DataProvider.createAttr("PhoneNumber");
            attr3.Value = item.PhoneNumber.ToString();
            XmlAttribute attr4 = DataProvider.createAttr("IdCard");
            attr4.Value = item.Cards.Id.ToString();
            XmlAttribute attr5 = DataProvider.createAttr("Address");
            attr5.Value = item.Address;
            XmlAttribute attr6 = DataProvider.createAttr("TotalScore");
            attr6.Value = item.TotalScore.ToString("F2");

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);
            newNode.Attributes.Append(attr3);
            newNode.Attributes.Append(attr4);
            newNode.Attributes.Append(attr5);
            newNode.Attributes.Append(attr6);

            string xPath = string.Format("//Customers");
            XmlNode node = DataProvider.getNode(xPath);
            DataProvider.AppendNode(node, newNode);
            DataProvider.Close();

            // save item in file book2.xml
            DataProvider.pathData = "data/Customers/Customers.xml";
            DataProvider.Open();
            int i = 0;
            foreach (var tempDetail in item.lstDetails)
            {
                XmlNode newNode2 = DataProvider.createNode("CustomerDetail");
                // XmlNode newNode = doc.CreateElement("Book");
                XmlAttribute attr8 = DataProvider.createAttr("IdSaleSlip");
                attr8.Value = tempDetail.IdSaleSlip;
                XmlAttribute attr9 = DataProvider.createAttr("Quantity");
                attr9.Value = tempDetail.Quantity.ToString();
                XmlAttribute attr10 = DataProvider.createAttr("Total");
                attr10.Value = tempDetail.Total.ToString();
                XmlAttribute attr11 = DataProvider.createAttr("Score");
                attr11.Value = tempDetail.Score.ToString("F2");
                XmlAttribute attr12 = DataProvider.createAttr("CreateAt");
                attr12.Value = tempDetail.CreateAt.ToString("dd/MM/yyy HH:mm:ss");

                newNode2.Attributes.Append(attr8);
                newNode2.Attributes.Append(attr9);
                newNode2.Attributes.Append(attr10);
                newNode2.Attributes.Append(attr11);
                newNode2.Attributes.Append(attr12);

                xPath = string.Format("//Customer[@No='{0}']/CustomerDetail", item.No);
                XmlNode node1 = DataProvider.getNode(xPath);
                DataProvider.AppendNode(node1, newNode2);
                i++;
            }

            DataProvider.Close();
        }

        public void Delete(Customer item)
        {
            throw new NotImplementedException();
        }

        public Customer Get(string Id)
        {
            throw new NotImplementedException();
        }

        public List<Customer> Gets()
        {
            return lstCustomer;
        }

        public void Update(Customer item)
        {
            // save item in file book2.xml
            DataProvider.pathData = "data/Customers/Customers.xml";
            DataProvider.Open();
            string xPath = string.Format("//Customer[@No='{0}']", item.No);
            XmlNode oldNode = DataProvider.getNode(xPath);

            XmlNode newNode = DataProvider.createNode("Customer");
            // XmlNode newNode = doc.CreateElement("Book");
            XmlAttribute attr1 = DataProvider.createAttr("No");
            attr1.Value = item.No.ToString();
            XmlAttribute attr2 = DataProvider.createAttr("Name");
            attr2.Value = item.Name;
            XmlAttribute attr3 = DataProvider.createAttr("PhoneNumber");
            attr3.Value = item.PhoneNumber.ToString();
            XmlAttribute attr4 = DataProvider.createAttr("IdCard");
            attr4.Value = item.Cards.Id.ToString();
            XmlAttribute attr5 = DataProvider.createAttr("Address");
            attr5.Value = item.Address;
            XmlAttribute attr6 = DataProvider.createAttr("TotalScore");
            attr6.Value = item.TotalScore.ToString("F2");

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);
            newNode.Attributes.Append(attr3);
            newNode.Attributes.Append(attr4);
            newNode.Attributes.Append(attr5);
            newNode.Attributes.Append(attr6);

            xPath = string.Format("//Customers");
            XmlNode node = DataProvider.getNode(xPath);
            DataProvider.InsertNode(newNode, oldNode);
            DataProvider.RemoveNode(oldNode);

            DataProvider.Close();

            // save item in file book2.xml
            DataProvider.pathData = "data/Customers/Customers.xml";
            DataProvider.Open();
            int i = 0;
            foreach (var tempDetail in item.lstDetails)
            {
                XmlNode newNode2 = DataProvider.createNode("CustomerDetail");
                // XmlNode newNode = doc.CreateElement("Book");
                XmlAttribute attr8 = DataProvider.createAttr("IdSaleSlip");
                attr8.Value = tempDetail.IdSaleSlip;
                XmlAttribute attr9 = DataProvider.createAttr("Quantity");
                attr9.Value = tempDetail.Quantity.ToString();
                XmlAttribute attr10 = DataProvider.createAttr("Total");
                attr10.Value = tempDetail.Total.ToString();
                XmlAttribute attr11 = DataProvider.createAttr("Score");
                attr11.Value = tempDetail.Score.ToString("F2");
                XmlAttribute attr12 = DataProvider.createAttr("CreateAt");
                attr12.Value = tempDetail.CreateAt.ToString("dd/MM/yyy HH:mm:ss");

                newNode2.Attributes.Append(attr8);
                newNode2.Attributes.Append(attr9);
                newNode2.Attributes.Append(attr10);
                newNode2.Attributes.Append(attr11);
                newNode2.Attributes.Append(attr12);

                xPath = string.Format("//Customer[@No='{0}']", item.No);
                XmlNode node1 = DataProvider.getNode(xPath);
                DataProvider.AppendNode(node1, newNode2);
                i++;
            }
        
            DataProvider.Close();
        }
    }
}
