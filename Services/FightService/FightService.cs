using System;
using System.Threading.Tasks;
using dotnet_rpg.Data;
using dotnet_rpg.Dtos.Fight;
using dotnet_rpg.Models;
using dotnet_rpg.Services.CharacterService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services.FightService
{
    public class FightService : IFightService
    {
        private readonly DataContext _context;

        public FightService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackerDto request)
        {
            var response = new ServiceResponse<AttackResultDto>();
            try
            {
                var attacker = await _context.Characters
                    .Include(c => c.Weapon)
                    .FirstOrDefaultAsync(c => c.Id == request.AttackerId);
                var opponent = await _context.Characters
                    .FirstOrDefaultAsync(c => c.Id == request.OpponentId);
                var damage = attacker.Weapon.Damage + (new Random().Next(attacker.Strength));
                damage -= new Random().Next(opponent.Defence);
                if (damage > 0)
                    opponent.HitPoints -= damage;
                if (opponent.HitPoints <= 0)
                    response.Message = $"{opponent.Name} has been defeated";
                _context.Characters.Update(opponent);
                await _context.SaveChangesAsync();
                response.Data = new AttackResultDto()
                {
                    Attacker = attacker.Name,
                    Opponent = opponent.Name,
                    Damage = damage,
                    AttackerHp = attacker.HitPoints,
                    OpponentHp = opponent.HitPoints
                };
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