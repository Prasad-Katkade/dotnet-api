using System.Collections.Generic;
using System.Threading.Tasks;
using api.Dtos.Character;
using api.Models;

namespace api.Services.CharacterService
{
    public interface ICharacterService
    {
        Task <ServiceResponse<List<GetCharacterDto>>> GetAllCharacters(int userID);
        Task <ServiceResponse<GetCharacterDto>> GetCharacterById(int id);
        Task <ServiceResponse<List<GetCharacterDto>>> AddNewCharacter(AddCharacterDto newCharacter);
        Task <ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter);
        Task <ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id);
        
    }
}