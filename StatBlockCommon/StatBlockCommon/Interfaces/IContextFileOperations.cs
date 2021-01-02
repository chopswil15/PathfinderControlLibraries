using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StatBlockCommon.Interfaces
{
    interface IContextFileOperations
    {
        bool SaveAsHTML(string fileName, string FullText);
        bool SaveAsXmlFormat<T>(T value, string fileName);
        T LoadFromXmlFormat<T>(T value, string fileName);
        bool SaveListAsXmlFormat<T>(List<T> MagicItems, string fileName);
        List<T> LoadListFromXmlFormat<T>(T value, string fileName);
    }
}
