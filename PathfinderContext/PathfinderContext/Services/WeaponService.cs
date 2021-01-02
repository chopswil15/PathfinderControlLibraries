using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PathfinderDomains;

namespace PathfinderContext.Services
{
    public class WeaponService : PathfinderServiceBase, IWeaponService
    {
        public WeaponService(string connectionString) : base(connectionString) { }

        public weapon GetWeaponByName(string name)
        {
            //using (IRepository<weapon> weaponRepository = CreateRepository<weapon>())
            //{
                weapon temp = base.FilterBy<weapon>(c => c.name == name).FirstOrDefault(); //weaponRepository.FilterBy(c => c.name == name).FirstOrDefault();
            if (temp == null)
                {
                    temp = base.FilterBy<weapon>(c => c.search_name == name).FirstOrDefault();// weaponRepository.FilterBy(c => c.search_name == name).FirstOrDefault();
                }
                return temp;
           // }
        }
    }
}
