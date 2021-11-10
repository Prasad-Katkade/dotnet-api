using api.Models;

namespace api.Dtos.Character
{
    public class UpdateCharacterDto
    {
        public int Id {get;set;}
        public string Name {get;set;}="default ";
        public int hp {get;set;}=100;
        public int strength {get;set;}=10;
        public int defence {get;set;}=10;
        public int intelligence {get;set;}=10;
        public RpgClass Class {get;set;}=RpgClass.knight;
    }
}