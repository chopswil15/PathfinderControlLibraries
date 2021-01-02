using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;
using PathfinderContext.Services;

namespace PathfinderService.Controllers
{
    public class WeaponController : ApiController
    {
        private IWeaponService _weaponService;

        [Route("weapon/name/{name}")]
        [HttpGet]
        public IHttpActionResult GetWeaponByName(string name)
        {
            _weaponService = GetWeaponService();
            var weapon = _weaponService.GetWeaponByName(name);
           // if (weapon == null)  return NotFound();
            return Ok(weapon);
        }

        private WeaponService GetWeaponService()
        {
            return new WeaponService(WebConfigurationManager.AppSettings["PathfinderConnectionString"]);
        }
    }
}