using System.Threading.Tasks;
using System.Collections.Generic;
using dotnet_rpg.Models;
using dotnet_rpg.Services.ServiceResponse;

namespace dotnet_rpg.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<Character>>> GetAllCharacters();
        Task<ServiceResponse<Character>> GetCharacterById(int id);
        Task<ServiceResponse<List<Character>>> AddCharacter(Character newCharacter);

    }
}