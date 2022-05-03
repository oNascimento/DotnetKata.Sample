using System;
using System.Collections.Generic;
using DotnetKata.Domain.Enums;
using DotnetKata.Domain.Utils;

namespace DotnetKata.Domain.Models
{
    public class Character : Element
    {
        public Character()
        {
            Health = 1000;
            Level = 1;
            Position = new Tuple<int, int>(0,0);
            Factions = new List<Faction>();
        }

        public Character(ECharacterType type, Tuple<int, int> position) : this()
        {
            Type = type;
            Position = position;
        }

        private const int maxLifeAmount = 1000;
        public int Level { get; set; }
        public ECharacterType Type { get; set; }
        public Tuple<int, int> Position { get; set; }
        public List<Faction> Factions { get; set; }
        public bool IsAlive => Health > 0;
        public bool DealDamage(Element target, int amount)
        {
            if(target is Character)
            {
                Character enemy = (Character) target;
                if(this == enemy)
                {
                    Console.WriteLine($"Chararacter is damaging himself");
                    return false;
                }

                if(DamageUtils.IsEnemyOnTheSameFaction(this, enemy))
                {
                    Console.WriteLine($"Chararacter of type {this.Type} is on the same faction");
                    return false;
                }

                if(!DamageUtils.IsEnemyInRange(this, enemy))
                {
                    Console.WriteLine($"Chararacter of type {this.Type} is not in range");
                    return false;
                }
                
                amount = DamageUtils.ResolveCriticalDamage(this, enemy, amount);
            }

            target.Health -= amount;
            
            Console.WriteLine($"{nameof(target)} take {amount} damage");
            return true;
        }

        public bool Heal(Element target, int amount)
        {
            if(target is Character)
            {
                Character ally = (Character) target;
                if(!DamageUtils.IsEnemyOnTheSameFaction(this, ally) && ally != this)
                {
                    Console.WriteLine($"Chararacter of type {this.Type} is on the same faction");
                    return false;
                }
                
                if(!IsAlive && ally.IsAlive)
                {
                    Console.WriteLine($"Chararacter is Dead");
                    return false;
                }
            }

            Console.WriteLine($"Heal {amount} damage");
            target.Health += amount;
            if(target.Health > maxLifeAmount) target.Health = maxLifeAmount;
            return true;
        }

        public void JoinFaction(Faction faction)
        {
            faction.Allies.Add(this);
            Factions.Add(faction);
        }

        public void LeaveFaction(Faction faction)
        {
            Factions.Remove(faction);
            faction.Allies.Remove(this);
        }
    }
}