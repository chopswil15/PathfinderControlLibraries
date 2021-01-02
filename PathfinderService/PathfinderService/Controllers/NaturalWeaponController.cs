using PathfinderContext.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;

namespace PathfinderService.Controllers
{
    public class NaturalWeaponController : ApiController
    {
        private INaturalWeaponService _naturalWeaponService;

        [Route("naturalWeapon/name/{name}")]
        [HttpGet]
        public IHttpActionResult GetArmorByName(string name)
        {
            _naturalWeaponService = GetNaturalWeaponServiceervice();
            var naturalWeapon = _naturalWeaponService.GetNaturalWeaponByName(name);
            if (naturalWeapon == null) return NotFound();
            return Ok(naturalWeapon);
        }

        private NaturalWeaponService GetNaturalWeaponServiceervice()
        {
            return new NaturalWeaponService(WebConfigurationManager.AppSettings["PathfinderConnectionString"]);
        }
    }
}
