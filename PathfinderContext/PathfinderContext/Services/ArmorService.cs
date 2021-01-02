using System;
using System.Collections.Generic;
using System.Linq;
using PathfinderDomains;

namespace PathfinderContext.Services
{
    public class ArmorService : PathfinderServiceBase, IArmorService
    {
        public ArmorService(string connectionString) : base(connectionString) { }

        public armor GetArmorByName(string name)
        {
            return base.FilterBy<armor>(c => c.name == name).FirstOrDefault();
            //using (IRepository<armor> armorRepository = CreateRepository<armor>())
            //{
            //    return armorRepository.FilterBy(c => c.name == name).FirstOrDefault();
            //}
        }

        public armor FindBy(int id)
        {
            return base.FindBy<armor>(id);
            //using (IRepository<armor> armorRepository = CreateRepository<armor>())
            //{
            //    return armorRepository.FindBy(id);
            //}
        }
    }
}
