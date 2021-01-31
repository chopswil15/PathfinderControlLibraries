using PathfinderContext.Services;
using System.Web.Configuration;
using System.Web.Http;

namespace PathfinderService.Controllers
{
    public class ArmorController : ApiController
    {
        private IArmorService _armorService;


        [Route("armor/{id}")]
        [HttpGet]
        public IHttpActionResult GetArmorByID(int id)
        {
            _armorService = GetArmorService();
            var feat = _armorService.FindBy(id);
            if (feat == null) return NotFound();
            return Ok(feat);
        }

        [Route("armor/name/{name}")]
        [HttpGet]
        public IHttpActionResult GetArmorByName(string name)
        {
            _armorService = GetArmorService();
            var feat = _armorService.GetArmorByName(name);
            if (feat == null)  return NotFound();
            return Ok(feat);
        }

        private ArmorService GetArmorService()
        {
            return new ArmorService(WebConfigurationManager.AppSettings["PathfinderConnectionString"]);
        }
    }    
}
