using PathfinderDomains;
using System.Collections.Generic;

namespace PathfinderContext.Services
{
    public interface IMagicItemService
    {
        IEnumerable<string> AddMagicItem(magic_item newMagicItem);
        magic_item FindBy(int id);
        magic_item GetMagicItemByName(string name);
        magic_item GetMagicItemByNameSource(string name, string source);
        IEnumerable<string> UpdateMagicItem(magic_item MagicItem);
    }
}