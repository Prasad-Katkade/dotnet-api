

using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Dtos.Character;
using api.Models;
using api.Services.CharacterService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {


        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;

        }

       // [AllowAnonymous]
        [HttpGet("GetAll")]

        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get()
        {
            int id=int.Parse(User.Claims.FirstOrDefault(c=>c.Type==ClaimTypes.NameIdentifier).Value);
            return Ok(await _characterService.GetAllCharacters(id));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetSingle(int id)
        {
            return Ok(await _characterService.GetCharacterById(id));
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> addCharacter(AddCharacterDto newCharacter)
        {
            return Ok(await _characterService.AddNewCharacter(newCharacter));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> updateCharacter(UpdateCharacterDto updateCharacter){
            var resp=await _characterService.UpdateCharacter(updateCharacter);
            if(resp.Data==null){
                return NotFound(resp);
            }else{
                return Ok(resp);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> deleteCharacter(int id){
            var resp=await _characterService.DeleteCharacter(id);
            if(resp.Data==null){
                return NotFound(resp);
            }else{
                return Ok(resp);
            }
        }


    }
}