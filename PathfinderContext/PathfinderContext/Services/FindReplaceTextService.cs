using PathfinderDomains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderContext.Services
{
    public class FindReplaceTextService : PathfinderServiceBase, IFindReplaceTextService
    {
        public FindReplaceTextService(string connectionString) : base(connectionString) { }

        public List<FindReplaceText> GetAllFindReplaceTexts()
        {
            return base.FilterBy<FindReplaceText>(x => x.Id > 0).ToList();
            //using (IRepository<FindReplaceText> findReplaceTextRepository = CreateRepository<FindReplaceText>())
            //{
            //    return findReplaceTextRepository.FilterBy(x => x.Id > 0).ToList();
            //}
        }
        public void ExecuteFindReplaceOnText(ref string text)
        {
            foreach (var findReplace in GetAllFindReplaceTexts())
            {
                text = text.Replace(findReplace.FindText, findReplace.ReplaceText);
            }
        }
    }
}
