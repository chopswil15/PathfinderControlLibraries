using System.Collections.Generic;
using PathfinderDomains;
using System.Linq;

namespace PathfinderContext.Services
{
    public class SpellService : PathfinderServiceBase, ISpellService
    {
        public SpellService(string connectionString) : base(connectionString) { }

        public IEnumerable<string> AddSpell(spell newSpell)
        {
            return base.Add<spell>(newSpell);
            //using (IRepository<spell> spellRepository = CreateRepository<spell>())
            //{
            //    IEnumerable<string> rules;
            //    spellRepository.Add(newSpell, out rules);
            //    return rules;
            //}
        }

        public IEnumerable<string> UpdateSpell(spell spell)
        {
            return base.Update<spell>(spell);
            //using (IRepository<spell> spellRepository = CreateRepository<spell>())
            //{
            //    IEnumerable<string> rules;
            //    spellRepository.Update(Spell, out rules);
            //    return rules;
            //}
        }

        public spell FindBy(int id)
        {
            return base.FindBy<spell>(id);
            //using (IRepository<spell> spellRepository = CreateRepository<spell>())
            //{
            //    return spellRepository.FindBy(id);
            //}
        }

        public spell GetSpellByName(string name)
        {
            return base.FilterBy<spell>(c => c.name == name).FirstOrDefault();
            //using (IRepository<spell> spellRepository = CreateRepository<spell>())
            //{
            //    return spellRepository.FilterBy(c => c.name == name).FirstOrDefault();
            //}
        }
    }
}
