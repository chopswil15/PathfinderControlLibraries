using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using StatBlockCommon.Interfaces;

namespace StatBlockCommon
{
    internal class ContextFileOperations : IContextFileOperations
    {
        public bool SaveAsHTML(string fileName, string FullText)
        {
            using (StreamWriter objReader = new StreamWriter(fileName, false))
            {
                objReader.Write(FullText);
                objReader.Close();
                return true;
            }
        }

        public bool SaveAsXmlFormat<T>(T value, string fileName)
        {
            XmlSerializer xmlFormat = new XmlSerializer(typeof(T));

            using (Stream fStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                xmlFormat.Serialize(fStream, value);
                return true;
            }
        }

        public T LoadFromXmlFormat<T>(T value, string fileName)
        {
            XmlSerializer xmlFormat = new XmlSerializer(typeof(T));

            using (Stream fStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                try
                {
                    T temp = (T)xmlFormat.Deserialize(fStream);
                    return temp;
                }
                catch
                {
                    return (T)Activator.CreateInstance(typeof(T));
                }
            }
        }

        public bool SaveListAsXmlFormat<T>(List<T> MagicItems, string fileName)
        {
            XmlSerializer xmlFormat = new XmlSerializer(typeof(List<T>));

            using (Stream fStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                xmlFormat.Serialize(fStream, MagicItems);
                return true;
            }
        }

        public List<T> LoadListFromXmlFormat<T>(T value, string fileName)
        {
            XmlSerializer xmlFormat = new XmlSerializer(typeof(List<T>));

            using (Stream fStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                return (List<T>)xmlFormat.Deserialize(fStream);
            }
        }
    }
}
