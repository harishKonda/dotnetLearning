
using System.Threading.Tasks;
using dotnet_rpg.Dtos.Weapon;
using dotnet_rpg.Services.WeaponService;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeaponController: ControllerBase
    {
        private readonly IWeaponService _weaponService;

        public WeaponController(IWeaponService weaponService)
        {
            _weaponService = weaponService;
        }
        [HttpPost]
        public async Task<IActionResult> AddWeapon(AddWeaponDto newWeapon)
        {
            var weapon = await _weaponService.AddWeapon(newWeapon);
            if (weapon.Success)
            {
                return BadRequest(weapon);
            }
            return Ok(weapon);
        }
    }
}