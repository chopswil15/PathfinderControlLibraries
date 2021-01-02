using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace StatBlockChecker
{
    public class MessageXML : IMessageXML
    {
        private XDocument doc;

        private string ROOT = "messages";
        private string NODE = "message";
        private string NODE2 = "Group";
        private string TYPE = "type";
        private string INFO_TEXT = "info_text";
        private string FORMULA = "formula";
        private string CALCULATION = "calculation";
        private const string PASS = "Pass";
        private const string INFO = "Info";
        private const string FAIL = "Fail";
        private const string NAME = "Name";

        public MessageXML()
        {
            doc = new XDocument(
                 new XDeclaration("1.0", "utf-8", "yes"),
                 new XElement(ROOT));
        }

        public XDocument MessageXMLDocument
        {
            get { return doc; }
        }

        public void AddPass(string message, string formula)
        {
            XElement node = new XElement(NODE,
                new XAttribute(TYPE,PASS),
                new XElement(INFO_TEXT,message),
                 new XElement(FORMULA,formula) );
            doc.Element(ROOT).Add(node);
        }

        public void AddPass(string message)
        {
            XElement node = new XElement(NODE,
                new XAttribute(TYPE, PASS),
                new XElement(INFO_TEXT, message));
            doc.Element(ROOT).Add(node);
        }

        public void AddInfo(string message)
        {
            XElement node = new XElement(NODE,
                new XAttribute(TYPE,INFO),
                new XElement(INFO_TEXT,message));
            doc.Element(ROOT).Add(node);
        }

        public void AddFail(string message, string formula, string calculation)
        {
            XElement node = new XElement(NODE,
                new XAttribute(TYPE, FAIL),
                new XElement(INFO_TEXT, message),
                new XElement(FORMULA,formula)                ,
                new XElement(CALCULATION, calculation));
            doc.Element(ROOT).Add(node);
        }

        public void AddFail(string message, string formula)
        {
            XElement node = new XElement(NODE,
                new XAttribute(TYPE, FAIL),
                new XElement(INFO_TEXT, message),
                new XElement(FORMULA, formula));
            doc.Element(ROOT).Add(node);
        }

        public void AddFail(string message)
        {
            XElement node = new XElement(NODE,
                new XAttribute(TYPE, FAIL),
                new XElement(INFO_TEXT, message));
            doc.Element(ROOT).Add(node);
        }

        public XmlDocument GenerateMessageGrouping()
        {
            XDocument newDoc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                 new XElement(ROOT));

            var query = from c in doc.Descendants(NODE)
                        select c;

            List<XElement> PassList = new List<XElement>();
            List<XElement> FailList = new List<XElement>();
            List<XElement> InfoList = new List<XElement>();

            foreach (XElement xe in query)
            {
                switch (xe.Attributes(TYPE).First().Value)
                {
                    case PASS:
                        PassList.Add(xe);
                        break;
                    case FAIL:
                        FailList.Add(xe);
                        break;
                    case INFO:
                        InfoList.Add(xe);
                        break;
                }
            }


            XElement newnode = new XElement(NODE2,
                new XAttribute(NAME, PASS));
            newDoc.Element(ROOT).Add(newnode);

            XElement parentXElement = newDoc.XPathSelectElement(ROOT + "/" + NODE2 + "[@" + NAME + "='" + PASS + "']");
            foreach (XElement element in PassList)
            {
                parentXElement.Add(element);
            }

            newnode = new XElement(NODE2,
                new XAttribute(NAME, FAIL));
            newDoc.Element(ROOT).Add(newnode);

            parentXElement = newDoc.XPathSelectElement(ROOT + "/" + NODE2 + "[@" + NAME + "='" + FAIL + "']");
            foreach (XElement element in FailList)
            {
                parentXElement.Add(element);
            }

            newnode = new XElement(NODE2,
                new XAttribute(NAME, INFO));
            newDoc.Element(ROOT).Add(newnode);

            parentXElement = newDoc.XPathSelectElement(ROOT + "/" + NODE2 + "[@" + NAME + "='" + INFO + "']");
            foreach (XElement element in InfoList)
            {
                parentXElement.Add(element);
            }

            var xmlDocument = new XmlDocument();
            using (var xmlReader = newDoc.CreateReader())
            {
                xmlDocument.Load(xmlReader);
            }
            return xmlDocument;
           // return newDoc;
        }
    }
}
