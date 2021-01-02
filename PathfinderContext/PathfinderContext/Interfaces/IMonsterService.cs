using PathfinderDomains;
using System.Collections.Generic;

namespace PathfinderContext.Services
{
    public interface IMonsterService
    {
        IEnumerable<string> AddMonster(monster newMonster);
        monster FindBy(int id);
        monster GetBestiaryMonsterByNamePathfinderDefault(string name);
        monster GetByNameSource(string indiv_name, string source, string altNameForm);
        monster GetMonsterByName(string name);
        IEnumerable<string> UpdateMonster(monster Monster);
    }
}