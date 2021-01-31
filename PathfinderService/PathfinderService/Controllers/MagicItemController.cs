using PathfinderContext.Services;
using PathfinderDomains;
using StatBlockCommon.MagicItem_SB;
using StatBlockParsing;
using System.Collections.Generic;
using System.Web.Configuration;
using System.Web.Http;

namespace PathfinderService.Controllers
{
    public class MagicItemController : ApiController
    {
        private IMagicItemService _magicItemService;

        [Route("magicItem/{id}")]
        [HttpGet]
        public IHttpActionResult GetMagicItemByID(int id)
        {
            _magicItemService = GetMagicItemService();
            var magicItem = _magicItemService.FindBy(id);
            return Ok(magicItem);
        }

        [Route("magicItem/name/{name}")]
        [HttpGet]
        public IHttpActionResult GetMagicItemByName(string name)
        {
            _magicItemService = GetMagicItemService();
            var magicItem = _magicItemService.GetMagicItemByName(name);
            return Ok(magicItem);
        }

        [Route("magicItem/name/{name}/source/{source}")]
        [HttpGet]
        public IHttpActionResult GetMagicItemByNameSource(string name, string source)
        {
            _magicItemService = GetMagicItemService();
            var magicItem = _magicItemService.GetMagicItemByNameSource(name, source);
            return Ok(magicItem);
        }

        [Route("magicItem/parse")]
        [HttpPost]
        public IHttpActionResult ParseMagicItem([FromBody]MagicItemParseInfo info)
        {
            MagicItemStatBlock_Parse magic_itemParse = new MagicItemStatBlock_Parse();
            magic_itemParse.BoldPhrases = info.BoldWords;
            magic_itemParse.ItalicPhrases = info.ItalicWords;
            string ErrorMessage = string.Empty;
            MagicItemParseReturn SBReturn = new MagicItemParseReturn();

            SBReturn.MagicItemStatBlock = magic_itemParse.Parse(info.rawMagicItemString, ref ErrorMessage);
            SBReturn.Error = ErrorMessage;

            return Ok(SBReturn);
        }

        [Route("magicItem")]
        [HttpPut]
        public IHttpActionResult UpdateMagicItem([FromBody]magic_item magic_item)
        {
            if (magic_item == null)  return Ok("Magic Item Empty");
            if (string.IsNullOrEmpty(magic_item.Group)) return Ok("Group Empty");

            _magicItemService = GetMagicItemService();
            IEnumerable<string> Error = _magicItemService.UpdateMagicItem(magic_item);
            return Ok(Error);
        }

        [Route("magicItem")]
        [HttpPost]
        public IHttpActionResult AddMagicItem([FromBody]magic_item magic_item)
        {
            if (magic_item == null) return Ok("Magic Item Empty");

            if (string.IsNullOrEmpty(magic_item.Group)) return Ok("Group Empty");
            if (string.IsNullOrEmpty(magic_item.name))  return Ok("Name Empty");

            _magicItemService = GetMagicItemService();
            IEnumerable<string> Error = _magicItemService.AddMagicItem(magic_item);
            return Ok(Error);
        }

        private MagicItemService GetMagicItemService()
        {
            return new MagicItemService(WebConfigurationManager.AppSettings["PathfinderConnectionString"]);
        }
    }

    public class MagicItemParseInfo
    {
        public List<string> BoldWords { get; set; }
        public List<string> ItalicWords { get; set; }
        public string rawMagicItemString { get; set; }
    }

    public class MagicItemParseReturn
    {
        public MagicItemStatBlock MagicItemStatBlock { get; set; }
        public string Error { get; set; }
    }
}