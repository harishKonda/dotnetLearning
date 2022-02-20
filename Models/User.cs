using System.Collections.Generic;

namespace dotnet_rpg.Models
{
    public class User
    {
        public int  Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { set; get; }
        public byte[] PasswordSalt { set; get; }
        
        public List<Character> Characters { get; set; }
    }
}