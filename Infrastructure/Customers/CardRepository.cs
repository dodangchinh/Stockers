using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Chinh_QuanLyKho
{
    public class CardRepository : IRepository<Cards>
    {
        public List<Cards> lstCard {  get; set; }
        public CardRepository()
        {
            lstCard = new List<Cards>();
            Load();
        }

        void Load()
        {
            DataProvider.pathData = "data/Customers/Cards.xml";
            DataProvider.Open();

            string xPath = string.Format("//Card");

            XmlNodeList listNode = DataProvider.getDsNode(xPath);

            foreach (XmlNode item in listNode)
            {
                Cards card = new Cards();
                card.Id = int.Parse(item.Attributes["Id"].Value);
                card.NameCustomer = item.Attributes["Customer"].Value;
                card.Score = double.Parse(item.Attributes["Score"].Value);
                card.CreateAt = DateTime.Parse(item.Attributes["CreateAt"].Value);
                lstCard.Add(card);
            }
            DataProvider.Close();
        }

        public void Add(Cards item)
        {
            lstCard.Add(item);

            // save item in file book2.xml
            DataProvider.pathData = "data/Customers/Cards.xml";
            DataProvider.Open();

            XmlNode newNode = DataProvider.createNode("Card");
            // XmlNode newNode = doc.CreateElement("Book");
            XmlAttribute attr1 = DataProvider.createAttr("Id");
            attr1.Value = item.Id.ToString();
            XmlAttribute attr2 = DataProvider.createAttr("Customer");
            attr2.Value = item.NameCustomer;
            XmlAttribute attr3 = DataProvider.createAttr("Score");
            attr3.Value = item.Score.ToString("F2");
            XmlAttribute attr4 = DataProvider.createAttr("CreateAt");
            attr4.Value = item.CreateAt.ToString("dd/MM/yyy HH:mm:ss");

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);
            newNode.Attributes.Append(attr3);
            newNode.Attributes.Append(attr4);

            string xPath = string.Format("//Cards");
            XmlNode node = DataProvider.getNode(xPath);
            DataProvider.AppendNode(node, newNode);

            DataProvider.Close();
        }

        public void Delete(Cards item)
        {
            throw new NotImplementedException();
        }

        public Cards Get(string Id)
        {
            throw new NotImplementedException();
        }

        public List<Cards> Gets()
        {
            return lstCard;
        }

        public void Update(Cards item)
        {
            // save item in file book2.xml
            DataProvider.pathData = "data/Customers/Cards.xml";
            DataProvider.Open();
            string xPath = string.Format("//Card[@Id='{0}']", item.Id);
            XmlNode oldNode = DataProvider.getNode(xPath);

            XmlNode newNode = DataProvider.createNode("Card");
            // XmlNode newNode = doc.CreateElement("Book");
            XmlAttribute attr1 = DataProvider.createAttr("Id");
            attr1.Value = item.Id.ToString();
            XmlAttribute attr2 = DataProvider.createAttr("Customer");
            attr2.Value = item.NameCustomer;
            XmlAttribute attr3 = DataProvider.createAttr("Score");
            attr3.Value = item.Score.ToString("F2");
            XmlAttribute attr4 = DataProvider.createAttr("CreateAt");
            attr4.Value = item.CreateAt.ToString("dd/MM/yyy HH:mm:ss");

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);
            newNode.Attributes.Append(attr3);
            newNode.Attributes.Append(attr4);

            DataProvider.InsertNode(newNode, oldNode);
            DataProvider.RemoveNode(oldNode);

            DataProvider.Close();
        }
    }
}
