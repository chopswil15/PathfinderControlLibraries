using PathfinderContext.Services;
using PathfinderDomains;
using StatBlockBusiness;
using StatBlockChecker;
using StatBlockCommon.Individual_SB;
using StatBlockCommon.Monster_SB;
using StatBlockParsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Configuration;
using System.Web.Http;
using System.Xml;

namespace PathfinderService.Controllers
{
    public class MonsterController : ApiController
    {
        private IMonsterService _monsterService;

        [Route("monster/{id}")]
        [HttpGet]
        public IHttpActionResult GetMonstertByID(int id)
        {
            _monsterService = GetMonsterService();
            var monster = _monsterService.FindBy(id);
          
            return Ok(monster);
        }    

        [Route("monster/search")]
        [HttpPost]
        public IHttpActionResult GetMonterByNameSource([FromBody]MonsterSearchCriteria criteria)
        {
            _monsterService = GetMonsterService();
            if (criteria.altname == null) criteria.altname = string.Empty;
            var monster = _monsterService.GetByNameSource(criteria.name, criteria.source, criteria.altname);

            return Ok(monster);
        }

        [Route("monster/validate")]
        [HttpPost]
        public IHttpActionResult ValidateMonster([FromBody]monster monster)
        {
            string ErrorMessage = string.Empty;
            MonsterValidateReturn validateReturn = new MonsterValidateReturn();

            try
            {                
                try
                {
                    MonsterStatBlock MonSB = MonsterStatBlockBusiness.MapThisToMonsterStatBlockObject(monster);
                    SBChecker checker = new SBChecker(MonSB);
               
                    if (monster.IsBestiary)
                    {
                        checker.CheckBestiaryStatBlock();
                    }
                    else
                    {
                        checker.CheckStatBlock();
                    }       
                
                    validateReturn.MessageXML = checker.MessageXML;
                    validateReturn.MessageJson = checker.MessageInfos.OrderBy(x=>x.MessageType).ToList();
                    validateReturn.SkillCalculations = checker.SkillCalculations;
                    }
                catch (Exception ex)
                {
                    validateReturn.Error = ex.Message;
                    return Ok(validateReturn);
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }

            return Ok(validateReturn);
        }

        [Route("monster/parse")]
        [HttpPost]
        public IHttpActionResult ParseMonster([FromBody]MonsterParseInfo info)
        {
            string ErrorMessage = string.Empty;
            try
            {                
                info.BoldWords.RemoveAll(x => x == string.Empty);
                info.ItalicWords.RemoveAll(x => x == string.Empty);

                for (int a = 0; a <= info.ItalicWords.Count - 1; a++)
                {
                    info.ItalicWords[a] = info.ItalicWords[a].Replace("fl ", "fl");
                    info.ItalicWords[a] = info.ItalicWords[a].Replace("fi ", "fi");
                }

                if((info.SpecialAbilitiesBoldWords == null || !info.SpecialAbilitiesBoldWords.Any()) 
                    && info.BoldWords.Contains("Special Abilities Stop"))
                {
                    int startIndex = info.BoldWords.IndexOf("SPECIAL ABILITIES") + 1;
                    int stoptIndex = info.BoldWords.IndexOf("Special Abilities Stop");
                    try
                    {
                        var range = info.BoldWords.GetRange(startIndex, stoptIndex - startIndex);
                        info.SpecialAbilitiesBoldWords.AddRange(range);
                    }
                    catch(Exception ex)
                    {
                        MonsterParseReturn SBReturn = new MonsterParseReturn();
                        SBReturn.MonsterStatBlock = new MonsterStatBlock();
                        SBReturn.Error = "SpecialAbilitiesBoldWords Issue";

                        return Ok(SBReturn);
                    }
                }

                if (info.IsBestiary)
                {
                    MonsterStatBlock_Parse monsterParse = new MonsterStatBlock_Parse();
                    monsterParse.BoldPhrases = info.BoldWords;
                    monsterParse.ItalicPhrases = info.ItalicWords;
                    monsterParse.BoldPhrasesSpecialAbilities = info.SpecialAbilitiesBoldWords;
                    monsterParse.GroupInName = info.IsGroupInName;
                    MonsterParseReturn SBReturn = new MonsterParseReturn();
                    SBReturn.MonsterStatBlock = monsterParse.Parse(info.rawMonsterString, info.Source, info.IsFrogGods, ref ErrorMessage);
                    SBReturn.Error = ErrorMessage;

                    return Ok(SBReturn);
                }
                else
                {
                    IndividualStatBlock_Parse indivParse = new IndividualStatBlock_Parse();
                    indivParse.BoldPhrases = info.BoldWords;
                    indivParse.ItalicPhrases = info.ItalicWords;
                    indivParse.BoldPhrasesSpecialAbilities = info.SpecialAbilitiesBoldWords;
                    IndividualParseReturn SBReturn = new IndividualParseReturn();
                    SBReturn.IndividualStatBlock = indivParse.Parse(info.rawMonsterString, info.Source, info.IsFrogGods, ref ErrorMessage);
                    SBReturn.Error = ErrorMessage;

                    return Ok(SBReturn);
                }
            }
            catch(Exception ex)
            {
                MonsterParseReturn SBReturn = new MonsterParseReturn();
                SBReturn.Error = ex.Message;
                return Ok(SBReturn);
            }
        }

        [Route("monster")]
        [HttpPut]
        public IHttpActionResult UpdateMonster([FromBody]monster monster)
        {
            if (monster == null) return Ok("Monster Empty");

            _monsterService = GetMonsterService();
            IEnumerable<string> Error = _monsterService.UpdateMonster(monster);
            return Ok(Error);
        }


        [Route("monster")]
        [HttpPost]
        public IHttpActionResult AddMonster([FromBody]monster monster)
        {
            if (monster == null)  return Ok("Monster Empty");

            _monsterService = GetMonsterService();
            IEnumerable<string> Error = _monsterService.AddMonster(monster);
            return Ok(Error);
        }

        private MonsterService GetMonsterService()
        {
            return new MonsterService(WebConfigurationManager.AppSettings["PathfinderConnectionString"]);
        }
    }

    public class MonsterSearchCriteria
    {
        public string name {get; set;}
        public string source {get; set;}
        public string altname {get; set;}
    }

    public class MonsterParseInfo
    {
        public List<string> BoldWords { get; set; }
        public List<string> ItalicWords { get; set; }
        public List<string> SpecialAbilitiesBoldWords { get; set; }
        public string rawMonsterString { get; set; }
        public string Source { get; set; }
        public bool IsBestiary { get; set; }
        public bool IsGroupInName { get; set; }
        public bool IsFrogGods { get; set; }
    }

    public class MonsterParseReturn
    {
        public MonsterStatBlock MonsterStatBlock { get; set; }
        public string Error { get; set; }
    }   

    public class IndividualParseReturn
    {
        public IndividualStatBlock IndividualStatBlock { get; set; }
        public string Error { get; set; }
    }

    public class MonsterValidateInfo
    {
        public monster MonsterData { get; set; }
        public bool IsBestiary { get; set; }
    }

    public class MonsterValidateReturn
    {
        public XmlDocument MessageXML { get; set; }
        public List<SkillCalculation> SkillCalculations { get; set; }
        public List<MessageInfo> MessageJson { get; set; }
        public string Error { get; set; }
    }
}