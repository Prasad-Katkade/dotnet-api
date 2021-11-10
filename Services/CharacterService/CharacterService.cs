


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Character;
using api.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace api.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        // Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;
        private static List<Character> characters = new List<Character>{
            new Character(),
            new Character {Id=1,Name="FIRST"}
        };
        private readonly IMapper _mapper;
        public DataContext _context { get; set; }

        public CharacterService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;

        }
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddNewCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Character character = _mapper.Map<Character>(newCharacter);
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            serviceResponse.Data =await _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                Character character = await _context.Characters.FirstAsync(c => c.Id == id);
                _context.Characters.Remove(character);
                await _context.SaveChangesAsync();
                serviceResponse.Data =await _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            }
            catch (Exception e)
            {
                serviceResponse.success = false;
                serviceResponse.message = e.Message.ToString();
            }
            return serviceResponse;

        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters(int userId)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters=await _context.Characters.Where(c=>c.User.Id==userId).ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacter=await _context.Characters.FirstOrDefaultAsync(c=>c.Id==id);
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
            return serviceResponse;

        }


        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                Character character =await _context.Characters.FirstOrDefaultAsync(c => c.Id == updateCharacter.Id);
                character.Name = updateCharacter.Name;
                character.hp = updateCharacter.hp;
                character.strength = updateCharacter.strength;
                character.defence = updateCharacter.defence;
                character.intelligence = updateCharacter.intelligence;
                character.Class = updateCharacter.Class;
                serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                serviceResponse.success = false;
                serviceResponse.message = ex.Message.ToString();
            }
            return serviceResponse;
        }
    }
}