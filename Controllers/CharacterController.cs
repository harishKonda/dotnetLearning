using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using dotnet_rpg.Models;
namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {
        private static List<Character> characters = new List<Character>{
            new Character(),
            new Character {Name = "Harish"},
        };
        [HttpGet("getAll")]
        public IActionResult Get()
        {
            return Ok(characters);
        }

        public IActionResult getSingel(){
            return Ok(characters[0]);
        }
    }

}