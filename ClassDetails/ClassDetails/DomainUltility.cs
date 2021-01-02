using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClericDomains;
using PathfinderGlobals;

namespace ClassDetails
{
    public static class DomainUltility
    {
        public static List<IDomain> ProcessDomains(string DomainsString)
        {
            List<IDomain>  Domains = new List<IDomain>();
            List<string> DomainList = DomainsString.Split(',').ToList();
            foreach (string domain in DomainList)
            {                
                string temp = domain.Trim();
                if (!temp.Contains("inquisition"))  //clerics can take inquisitions
                {
                    temp = temp.Replace(PathfinderConstants.SPACE, string.Empty);
                    temp = temp.Replace("*", string.Empty).Trim();
                    IDomain domainClass = DomainReflectionWrapper.AddDomainClass(temp);
                    if (domainClass == null && temp.Length > 0) throw new Exception("Domain not defined - " + temp);
                    Domains.Add(domainClass);
                }
            }
            Domains.Remove(null);

            return Domains;
        }

        public static int GetDomainSpellsPerLevel(int ClassLevel, List<IDomain> Domains)
        {
            int Count = 0;
            foreach (IDomain domain in Domains)
            {
                Dictionary<string, int> DomainSpells = domain.DomainSpells;
                foreach (KeyValuePair<string, int> pair in DomainSpells)
                {
                    if (pair.Value <= ClassLevel)
                    {
                        Count++;
                    }
                }
            }

            return Count;
        }
    }
}
