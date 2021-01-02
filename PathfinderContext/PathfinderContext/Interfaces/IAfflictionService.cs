using PathfinderDomains;
using System.Collections.Generic;

namespace PathfinderContext.Services
{
    public interface IAfflictionService
    {
        IEnumerable<string> AddAffliction(affliction newAffliction);
        affliction FindBy(int id);
        affliction GetAfflictionByName(string name);
        IEnumerable<string> UpdateAffliction(affliction Affliction);
    }
}