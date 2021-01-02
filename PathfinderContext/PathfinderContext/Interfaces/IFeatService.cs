using PathfinderDomains;
using System.Collections.Generic;

namespace PathfinderContext.Services
{
    public interface IFeatService
    {
        IEnumerable<string> AddFeat(feats newFeat);
        feats FindBy(int id);
        feats GetFeatByName(string name);
        feats GetFeatByNameSource(string name, string source);
        feats GetMythicFeatByName(string name);
        IEnumerable<string> UpdateFeat(feats Feat);
    }
}