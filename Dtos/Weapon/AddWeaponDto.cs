namespace dotnet_rpg.Dtos.Weapon
{
    public class AddWeaponDto
    {
        public string Name { set; get; }
        public int Damage { get; set; }
        public int CharacterId { get; set; }
    
    }
}