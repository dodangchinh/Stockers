using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Chinh_QuanLyKho
{
    public static class DataProvider
    {
        public static string pathData { get; set; }
        static XmlDocument doc = null;
        public static XmlNode nodeRoot = null;

        public static void Open()
        {
            if (doc == null)
                doc = new XmlDocument();
            doc.Load(pathData);
            nodeRoot = doc.DocumentElement;
        }

        public static void Close()
        {
            if (doc != null)
                doc.Save(pathData);
        }

        public static XmlNode getNode(string xpath)
        {
            return nodeRoot.SelectSingleNode(xpath);
        }

        public static XmlNode getNode(string xpath, int index)
        {
            XmlNode temp = doc.SelectSingleNode(xpath);
            for (int i = 0; i < index; i++)
            {
                temp = temp.NextSibling;
            }
            return temp;
        }

        public static XmlNodeList getDsNode(string xpath)
        {
            return nodeRoot.SelectNodes(xpath);
        }

        public static XmlNode createNode(string tagName)
        {
            XmlNode node = doc.CreateElement(tagName);
            return node;
        }

        public static XmlAttribute createAttr(string name)
        {
            XmlAttribute attr = doc.CreateAttribute(name);
            return attr;
        }

        // Thêm 1 nút con tại vị trí cuối trong nút gốc (nút cha)
        public static void AppendNode(XmlNode node, XmlNode newNode)
        {
            node.AppendChild(newNode);
        }

        public static void PrependNode(XmlNode node, XmlNode newNode)
        {
            node.PrependChild(newNode);
        }

        public static void InsertNode(XmlNode childNode, XmlNode refNode)
        {
            nodeRoot.InsertAfter(childNode, refNode);
        }

        public static void RemoveNode(XmlNode refNode)
        {
            XmlNode parent = refNode.ParentNode;
            parent.RemoveChild(refNode);
        }

        public static void ReplaceNode(XmlNode oldNode, XmlNode newNode)
        {
            doc.ReplaceChild(newNode, oldNode);
        }
    }
}
