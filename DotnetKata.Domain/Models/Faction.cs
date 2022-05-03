using System.Collections.Generic;

namespace DotnetKata.Domain.Models
{
    public class Faction
    {
        public Faction() =>  Allies = new List<Character>();
        public List<Character> Allies { get; set; }
    }
}