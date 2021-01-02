using PathfinderDomains;
using System.Collections.Generic;

namespace PathfinderContext.Services
{
    public interface IFindReplaceTextService
    {
        void ExecuteFindReplaceOnText(ref string text);
        List<FindReplaceText> GetAllFindReplaceTexts();
    }
}