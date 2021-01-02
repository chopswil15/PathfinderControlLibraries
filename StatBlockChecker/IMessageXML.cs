using System.Xml;
using System.Xml.Linq;

namespace StatBlockChecker
{
    public interface IMessageXML
    {
        XDocument MessageXMLDocument { get; }

        void AddFail(string message);
        void AddFail(string message, string formula);
        void AddFail(string message, string formula, string calculation);
        void AddInfo(string message);
        void AddPass(string message);
        void AddPass(string message, string formula);
        XmlDocument GenerateMessageGrouping();
    }
}