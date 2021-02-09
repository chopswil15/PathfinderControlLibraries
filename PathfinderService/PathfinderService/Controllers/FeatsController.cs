using PathfinderContext.Services;
using PathfinderDomains;
using StatBlockCommon.Feat_SB;
using StatBlockParsing;
using System.Collections.Generic;
using System.Web.Configuration;
using System.Web.Http;

namespace PathfinderService.Controllers
{
    [Route("[controller]")]
    public class FeatsController : ApiController
    {
        private IFeatService _featService;

        [Route("GetFeatByID")]
        [HttpGet]
        public IHttpActionResult GetFeatByID(int id)
        {
            _featService = GetFeatService();
            var feat = _featService.FindBy(id);
            if (feat == null) return NotFound();
            return Ok(feat);
        }

        [Route("feats/name/{name}")]
        [HttpGet]
        public IHttpActionResult GetFeatByName(string name)
        {
            _featService = GetFeatService();
            var feat = _featService.GetFeatByName(name);
            if (feat == null) return NotFound();
            return Ok(feat);
        }

        [Route("feats/name/mythic/{nameMythic}")]
        [HttpGet]
        public IHttpActionResult GetMythicFeatByName(string nameMythic)
        {
            _featService = GetFeatService();
            var feat = _featService.GetMythicFeatByName(nameMythic);
            if (feat == null) return NotFound();
            return Ok(feat);
        }

        [Route("feats/name/{name}/source/{source}")]
        [HttpGet]
        public IHttpActionResult GetFeatByNameSource(string name, string source)
        {
            _featService = GetFeatService();
            var feat = _featService.GetFeatByNameSource(name, source);
            return Ok(feat);
        }

        [Route("feats/parse")]
        [HttpPost]
        public IHttpActionResult ParseFeat([FromBody]FeatParseInfo info)
        {
            FeatStatBlock_Parse featParse = new FeatStatBlock_Parse();
            featParse.BoldPhrases = info.BoldWords;
            featParse.ItalicPhrases = info.ItalicWords;
            string ErrorMessage = string.Empty;
            FeatParseReturn SBReturn = new FeatParseReturn();

            SBReturn.FeatStatBlock = featParse.Parse(info.rawFeatString, ref ErrorMessage);
            SBReturn.Error = ErrorMessage;

            return Ok(SBReturn);
        }


        [Route("feats")]
        [HttpPut]
        public IHttpActionResult UpdateFeat([FromBody]feats feat)
        {           
            if (feat == null)  return Ok("No Feat Data");

            _featService = GetFeatService();
            IEnumerable<string> Error = _featService.UpdateFeat(feat);           
            return Ok(Error);
        }


        [Route("feats")]
        [HttpPost]
        public IHttpActionResult AddFeat([FromBody]feats feat)
        {
            if (feat == null) return Ok("No Feat Data");

            _featService = GetFeatService();
            IEnumerable<string> Error = _featService.AddFeat(feat);
            return Ok(Error);
        }


        private FeatService GetFeatService()
        {
            return new FeatService(WebConfigurationManager.AppSettings["PathfinderConnectionString"]);
        }
    }


    public class FeatParseInfo
    {
        public List<string> BoldWords { get; set; }
        public List<string> ItalicWords { get; set; }
        public string rawFeatString { get; set; }
    }

    public class FeatParseReturn
    {
        public FeatStatBlock FeatStatBlock { get; set; }
        public string Error { get; set; }
    }
}