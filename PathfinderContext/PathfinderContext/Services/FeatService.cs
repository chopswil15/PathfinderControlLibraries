using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PathfinderDomains;

namespace PathfinderContext.Services
{
    public class FeatService : PathfinderServiceBase, IFeatService
    {
        public FeatService(string connectionString) : base(connectionString) { }

        public IEnumerable<string> AddFeat(feats newFeat)
        {
            return base.Add<feats>(newFeat);
            //using (IRepository<feats> featRepository = CreateRepository<feats>())
            //{
            //    IEnumerable<string> rules;
            //    featRepository.Add(newFeat, out rules);
            //    return rules;
            //}
        }

        public IEnumerable<string> UpdateFeat(feats feat)
        {
            return base.Update<feats>(feat);
            //using (IRepository<feats> featRepository = CreateRepository<feats>())
            //{
            //    IEnumerable<string> rules;
            //    featRepository.Update(Feat, out rules);
            //    return rules;
            //}
        }

        public feats FindBy(int id)
        {
            return base.FindBy<feats>(id);
            //using (IRepository<feats> featRepository = CreateRepository<feats>())
            //{
            //    return featRepository.FindBy(id);
            //}
        }

        public feats GetFeatByName(string name)
        {
           return base.FilterBy<feats>(c => c.name == name && !c.mythic).FirstOrDefault(); 
            //using (IRepository<feats> featRepository = CreateRepository<feats>())
            //{
            //    return featRepository.FilterBy(c => c.name == name && !c.mythic).FirstOrDefault();
            //}
        }

        public feats GetMythicFeatByName(string name)
        {
            return base.FilterBy<feats>(c => c.name == name && c.mythic).FirstOrDefault();
            //using (IRepository<feats> featRepository = CreateRepository<feats>())
            //{
            //    return featRepository.FilterBy(c => c.name == name && c.mythic).FirstOrDefault();
            //}
        }

        public feats GetFeatByNameSource(string name, string source)
        {
            return base.FilterBy<feats>(c => c.name == name && c.source == source).FirstOrDefault();
            //using (IRepository<feats> featRepository = CreateRepository<feats>())
            //{
            //    return featRepository.FilterBy(c => c.name == name && c.source == source).FirstOrDefault();
            //}
        }
    }
}
