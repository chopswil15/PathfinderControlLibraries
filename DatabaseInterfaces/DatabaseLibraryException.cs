using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseInterfaces
{
    [Serializable]
    public class DatabaseLibraryException : Exception
    {
        public DatabaseLibraryException() { }

        public DatabaseLibraryException(string message, string libraryBaseName, Exception ex) 
            : base($"{ libraryBaseName }: {message}", ex) { }
             
       
    }
}
