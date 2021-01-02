using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PathfinderDomains;

namespace PathfinderContext.Services
{
    public class AfflictionService : PathfinderServiceBase, IAfflictionService
    {
        public AfflictionService(string connectionString) : base(connectionString) { }

        public IEnumerable<string> AddAffliction(affliction newAffliction)
        {
            return base.Add<affliction>(newAffliction);
            //using (IRepository<affliction> afflictionRepository = CreateRepository<affliction>())
            //{
            //    IEnumerable<string> rules;
            //    afflictionRepository.Add(newAffliction, out rules);
            //    return rules;
            //}
        }

        public IEnumerable<string> UpdateAffliction(affliction affliction)
        {
            return base.Update<affliction>(affliction);
            //using (IRepository<affliction> afflictionRepository = CreateRepository<affliction>())
            //{
            //    IEnumerable<string> rules;
            //    afflictionRepository.Update(Affliction, out rules);
            //    return rules;
            //}
        }

        public affliction FindBy(int id)
        {
            return base.FindBy<affliction>(id);
            //using (IRepository<affliction> afflictionRepository = CreateRepository<affliction>())
            //{
            //    return afflictionRepository.FindBy(id);
            //}
        }

        public affliction GetAfflictionByName(string name)
        {
            return base.FilterBy<affliction>(c => c.name == name).FirstOrDefault();
            //using (IRepository<affliction> afflictionRepository = CreateRepository<affliction>())
            //{
            //    return afflictionRepository.FilterBy(c => c.name == name).FirstOrDefault();
            //}
        }
    }
}
