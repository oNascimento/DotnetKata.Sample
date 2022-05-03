using System;
using DotnetKata.Domain.Enums;
using DotnetKata.Domain.Models;

namespace DotnetKata.Domain
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var character = new Character(Enums.ECharacterType.Melee, new Tuple<int, int>(1,1));

            Console.WriteLine(character.Health);

            var heroPosition = new Tuple<int, int>(0,0);
            var enemyPosition = new Tuple<int, int>(5,5);
            
            var hero = new Character(ECharacterType.Melee, heroPosition);
            var enemy = new Character(ECharacterType.Melee, heroPosition);

            hero.DealDamage(enemy, 100);
        }
    }
}
