using System;
using DotnetKata.Domain.Enums;
using DotnetKata.Domain.Utils;

namespace DotnetKata.Domain.Models
{
    public class Character
    {
        public Character()
        {
            Health = 1000;
            Level = 1;
            Position = new Tuple<int, int>(0,0);
        }

        public Character(ECharacterType type, Tuple<int, int> position) : this()
        {
            Type = type;
            Position = position;
        }

        private const int maxLifeAmount = 1000;
        public int Health { get; set; }
        public int Level { get; set; }
        public ECharacterType Type { get; set; }
        public Tuple<int, int> Position { get; set; }
        public bool IsAlive => Health > 0;

        public bool DealDamage(Character enemy, int amount)
        {
            if(this == enemy)
            {
                Console.WriteLine($"Chararacter is damaging himself");
                return false;
            }

            if(!DamageUtils.IsEnemyInRange(this, enemy))
            {
                Console.WriteLine($"Chararacter of type {this.Type} is not in range");
                return false;
            }

            amount = DamageUtils.ResolveCriticalDamage(this, enemy, amount);
            Console.WriteLine($"{enemy.Level} Take {amount} damage");
            enemy.Health -= amount;
            return true;
        }

        public void Heal(int amount)
        {
            Console.WriteLine($"Heal {amount} damage");
            Health += amount;
            if(Health > maxLifeAmount) Health = maxLifeAmount;
        }
    }
}