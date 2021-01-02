using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatabaseInterfaces;
using PathfinderDomains;

namespace PathfinderContext.Services
{
    public class MagicItemService : PathfinderServiceBase, IMagicItemService
    {
        public MagicItemService(string connectionString) : base(connectionString) { }

        public IEnumerable<string> AddMagicItem(magic_item newMagicItem)
        {
            return base.Add<magic_item>(newMagicItem);
            //using (IRepository<magic_item> magicItemRepository = CreateRepository<magic_item>())
            //{
            //    IEnumerable<string> rules;
            //    magicItemRepository.Add(newMagicItem, out rules);
            //    return rules;
            //}
        }

        public IEnumerable<string> UpdateMagicItem(magic_item magicItem)
        {
            return base.Update<magic_item>(magicItem);
            //using (IRepository<magic_item> magicItemRepository = CreateRepository<magic_item>())
            //{
            //    IEnumerable<string> rules;
            //    magicItemRepository.Update(MagicItem, out rules);
            //    return rules;
            //}
        }

        public magic_item FindBy(int id)
        {
            return base.FindBy<magic_item>(id);
            //using (IRepository<magic_item> magicItemRepository = CreateRepository<magic_item>())
            //{
            //    return magicItemRepository.FindBy(id);
            //}
        }

        public magic_item GetMagicItemByName(string name)
        {
            return base.FilterBy<magic_item>(c => c.name == name).FirstOrDefault();
            //using (IRepository<magic_item> magicItemRepository = CreateRepository<magic_item>())
            //{
            //    return magicItemRepository.FilterBy(c => c.name == name).FirstOrDefault();
            //}
        }

        public magic_item GetMagicItemByNameSource(string name, string source)
        {
            return base.FilterBy<magic_item>(c => c.name == name && c.Source == source).FirstOrDefault();
            //using (IRepository<magic_item> magicItemRepository = CreateRepository<magic_item>())
            //{
            //    return magicItemRepository.FilterBy(c => c.name == name && c.Source == source).FirstOrDefault();
            //}
        }
    }
}
