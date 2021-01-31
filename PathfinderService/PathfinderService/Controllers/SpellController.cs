using PathfinderContext.Services;
using PathfinderDomains;
using StatBlockCommon.Spell_SB;
using StatBlockParsing;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Configuration;
using System.Web.Http;

namespace PathfinderService.Controllers
{
    public class SpellController : ApiController
    {
        private ISpellService _spellService;

        [Route("spell/{id}")]
        [HttpGet]
        public IHttpActionResult GetSpellByID(int id)
        {
            _spellService = GetSpellService();
            var spell = _spellService.FindBy(id);
            return Ok(spell);
        }

        [Route("spell/name/{name}")]
        [HttpGet]
        public IHttpActionResult GetSpellByName(string name)
        {
            _spellService = GetSpellService();
            var spell = _spellService.GetSpellByName(name);
            return Ok(spell);
        }

        [Route("spell/parse")]
        [HttpPost]
        public IHttpActionResult ParseSpell([FromBody]SpellParseInfo info)
        {
            try
            {
                SpellStatBlock_Parse spellParse = new SpellStatBlock_Parse();
                spellParse.BoldPhrases = info.BoldWords;
                spellParse.ItalicPhrases = info.ItalicWords;
                string ErrorMessage = string.Empty;
                SpellParseReturn SBReturn = new SpellParseReturn();

                SBReturn.SpellStatBlock = spellParse.Parse(info.rawSpellString, ref ErrorMessage);
                SBReturn.Error = ErrorMessage;

                return Ok(SBReturn);
            }
            catch(Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex);
            }           
        }


        [Route("spell")]
        [HttpPut]
        public IHttpActionResult UpdateSpell([FromBody]spell spell)
        {
            if (spell == null)  return Ok("Spell Empty");

            _spellService = GetSpellService();
            IEnumerable<string> Error = _spellService.UpdateSpell(spell);
            return Ok(Error);
        }

        [Route("spell")]
        [HttpPost]
        public IHttpActionResult AddSpell([FromBody]spell spell)
        {
            if (spell == null)return Ok("Spell Empty");

            _spellService = GetSpellService();
            IEnumerable<string> Error = _spellService.AddSpell(spell);
            return Ok(Error);
        }


        private SpellService GetSpellService()
        {
            return new SpellService(WebConfigurationManager.AppSettings["PathfinderConnectionString"]);
        }
    }

    public class SpellParseInfo
    {
        public List<string> BoldWords { get; set; }
        public List<string> ItalicWords { get; set; }
        public string rawSpellString { get; set; }
    }

    public class SpellParseReturn
    {
        public SpellStatBlock SpellStatBlock { get; set; }
        public string Error { get; set; }
    }
}