using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using dotnet_rpg.Models;
using dotnet_rpg.Services.CharacterService;
using System.Threading.Tasks;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {
        // private static List<Character> characters = new List<Character>{
        //     new Character(),
        //     new Character {Name = "Harish", Id = 1,},
        //     new Character {Name = "Sai kiran", Id = 2, Intelligence = 101,},
        // };
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _characterService.GetAllCharacters());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getSingle(int id)
        {
            return Ok(await _characterService.GetCharacterById(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddCharcter(Character newCharacter)
        {
            // characters.Add(newCharacter);
            return Ok(await _characterService.AddCharacter(newCharacter));
        }
    }

}