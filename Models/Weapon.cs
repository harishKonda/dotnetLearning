namespace dotnet_rpg.Models
{
    public class Weapon
    {
        public int Id { get; set; }
        public string Name { set; get; }
        public Character Character { get; set; }
        public int Damage { get; set; }
        public int CharacterId { get; set; }
    }
}