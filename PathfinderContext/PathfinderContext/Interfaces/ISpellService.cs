using PathfinderDomains;
using System.Collections.Generic;

namespace PathfinderContext.Services
{
    public interface ISpellService
    {
        IEnumerable<string> AddSpell(spell newSpell);
        spell FindBy(int id);
        spell GetSpellByName(string name);
        IEnumerable<string> UpdateSpell(spell Spell);
    }
}