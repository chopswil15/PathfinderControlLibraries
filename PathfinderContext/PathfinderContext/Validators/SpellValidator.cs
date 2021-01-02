using System.Collections.Generic;
using System.Linq;
using DatabaseInterfaces;
using PathfinderDomains;

namespace PathfinderContext
{
    public class SpellValidator : IValidator<spell>
    {
        public bool IsValid(spell entity)
        {
            return BrokenRules(entity).Count() == 0;
        }

        public IEnumerable<string> BrokenRules(spell entity)
        {
            return new List<string>();
        }
    }
}
