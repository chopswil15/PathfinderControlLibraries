using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PathfinderDomains;

namespace PathfinderContext.Services
{
    public class NaturalWeaponService : PathfinderServiceBase, INaturalWeaponService
    {
        public NaturalWeaponService(string connectionString) : base(connectionString) { }

        public natural_weapon GetNaturalWeaponByName(string name)
        {
            return base.FilterBy<natural_weapon>(c => c.name == name).FirstOrDefault();
            //using (IRepository<natural_weapon> naturalWeaponsRepository = CreateRepository<natural_weapon>())
            //{
            //    return naturalWeaponsRepository.FilterBy(c => c.name == name).FirstOrDefault();
            //}
        }
    }
}
