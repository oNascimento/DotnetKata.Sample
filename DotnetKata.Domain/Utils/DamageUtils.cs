using System;
using System.Linq;
using DotnetKata.Domain.Models;

namespace DotnetKata.Domain.Utils
{
    public static class DamageUtils
    {
        public static bool IsEnemyInRange(Character character, Character target)
        {
            var xDistance = Math.Pow(character.Position.Item1 - target.Position.Item1, 2);
            var yDistance = Math.Pow(character.Position.Item2 - target.Position.Item2, 2);

            var range = Math.Sqrt(xDistance + yDistance);
            Console.WriteLine($"Character has distance of {range}");
            
            switch(character.Type)
            {
                case Enums.ECharacterType.Melee:
                    return range < 2;
                case Enums.ECharacterType.Range:
                    return range < 20;
                default:
                    Console.WriteLine("Character must have a type");
                    return false;
            }
        }

        public static int ResolveCriticalDamage(Character character, Character target, int amount)
        {
            int result = amount;

            if((target.Level + 5) <= character.Level)
                result = (int) Math.Round(amount * 1.5);
            
            if((target.Level - 5) >= character.Level)
                result = (int) Math.Round(amount * 0.5);
            
            return result;
        }

        public static bool IsEnemyOnTheSameFaction(Character character, Character target)
        {
            foreach(var faction in character.Factions)
            {
                if(faction.Allies.Any(ally => ally == target))
                    return true;
            }
            return false;
        }
    }
}