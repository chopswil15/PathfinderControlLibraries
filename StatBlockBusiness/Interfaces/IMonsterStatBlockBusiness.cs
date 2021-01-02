using StatBlockCommon.Monster_SB;
using System.Collections.Generic;

namespace StatBlockBusiness
{
    public interface IMonsterStatBlockBusiness
    {
        bool AddMonster(MonsterStatBlock SB, ref IEnumerable<string> Error);
        MonsterStatBlock GetBestiaryMonsterByNamePathfinderDefault(string name);
        MonsterStatBlock GetMonsterByName(string name);
        bool UpdateMonster(MonsterStatBlock SB, ref IEnumerable<string> Error);
    }
}