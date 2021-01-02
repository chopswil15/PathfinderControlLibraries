using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PathfinderDomains;

namespace PathfinderContext.Services
{
    public class MonsterService : PathfinderServiceBase, IMonsterService
    {
        public MonsterService(string connectionString) : base(connectionString) { }

        public IEnumerable<string> AddMonster(monster newMonster)
        {
            return base.Add<monster>(newMonster);
            //using (IRepository<monster> monsterRepository = CreateRepository<monster>())
            //{
            //    IEnumerable<string> rules;
            //    monsterRepository.Add(newMonster, out rules);
            //    return rules;
            //}
        }

        public IEnumerable<string> UpdateMonster(monster monster)
        {
            return base.Update<monster>(monster);
            //using (IRepository<monster> monsterRepository = CreateRepository<monster>())
            //{
            //    IEnumerable<string> rules;
            //    monsterRepository.Update(Monster, out rules);
            //    return rules;
            //}
        }
        public monster FindBy(int id)
        {
            return base.FindBy<monster>(id);
            //using (IRepository<monster> monsterRepository = CreateRepository<monster>())
            //{
            //    return monsterRepository.FindBy(id);
            //}
        }

        public monster GetMonsterByName(string name)
        {
            return base.FilterBy<monster>(c => c.name == name).FirstOrDefault();
            //using (IRepository<monster> monsterRepository = CreateRepository<monster>())
            //{
            //    return monsterRepository.FilterBy(c => c.name == name).FirstOrDefault();
            //}
        }

        public monster GetBestiaryMonsterByNamePathfinderDefault(string name)
        {
            //using (IRepository<monster> monsterRepository = CreateRepository<monster>())
            //{

                monster tempMonster = base.FilterBy<monster>(c => c.name == name && c.IsBestiary).FirstOrDefault(); // monsterRepository.FilterBy(c => c.name == name && c.IsBestiary).FirstOrDefault();
            if (tempMonster == null || !tempMonster.Source.Contains("Tome of Horrors")) return tempMonster;

                //try another, but it may be only one
                monster tempMonster2 = base.FilterBy<monster>(c => c.name == name && c.IsBestiary && !c.Source.Contains("Tome of Horrors")).FirstOrDefault(); ; //monsterRepository.FilterBy(c => c.name == name && c.IsBestiary && !c.Source.Contains("Tome of Horrors")).FirstOrDefault();

                if (tempMonster2 == null || tempMonster2.name == null) return tempMonster;

                return tempMonster2;
          //  }
        }

        public monster GetByNameSource(string indiv_name, string source, string altNameForm)
        {
            return base.FilterBy<monster>(c => c.name == indiv_name && c.Source == source && c.AlternateNameForm == altNameForm).FirstOrDefault();
            //using (IRepository<monster> monsterRepository = CreateRepository<monster>())
            //{
            //    return monsterRepository.FilterBy(c => c.name == indiv_name && c.Source == source && c.AlternateNameForm == altNameForm).FirstOrDefault();
            //}
        }
    }
}
