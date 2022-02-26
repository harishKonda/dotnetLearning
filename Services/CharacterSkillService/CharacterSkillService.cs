using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Data;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Dtos.CharacterSkill;
using dotnet_rpg.Models;
using dotnet_rpg.Services.CharacterService;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services.CharacterSkillService
{
    public class CharacterSkillService : ICharacterSkillService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CharacterSkillService(DataContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            try
            {
                var character = await _context.Characters.
                    Include(c => c.Weapon).
                    Include(c => c.CharacterSkills).
                    ThenInclude(cs => cs.Skill).
                    FirstOrDefaultAsync(c => c.Id==  newCharacterSkill.CharacterId &&
                                              c.User.Id == int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(
                                                  ClaimTypes.NameIdentifier)));
                if (character == null)
                {
                    response.Success = false;
                    response.Message = "Character not found";
                    return response;
                }

                var skill = await _context.Skill.FirstOrDefaultAsync(s => s.Id == newCharacterSkill.SkillId);
                if (skill == null)
                {
                    response.Message = "Skill not found";
                    response.Success = false;
                }

                var characterSkill = new CharacterSkill
                {
                    Character = character,  
                    Skill = skill,
                };
                await _context.CharacterSkills.AddAsync(characterSkill);
                await _context.SaveChangesAsync();
                response.Success = true;
                response.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
            }

            return response;
        }
    }
}