using System;
using DotnetKata.Domain.Enums;
using DotnetKata.Domain.Models;
using Xunit;

namespace DotnetKata.Tests
{
    public class CharacterTest
    {
        [Fact]
        public void ChararacterShouldDealDamageToOtherCharacter()
        {
            var hero = new Character();
            var enemy = new Character();
            const int damageDealt = 100;
            
            int expectedHealthRemaing = enemy.Health - damageDealt;

            hero.DealDamage(enemy, damageDealt);
            Assert.Equal(expectedHealthRemaing, enemy.Health);
        }

        [Fact]
        public void CharacterShouldBeDeadIfTakesLethalDamage()
        {
            var hero = new Character();
            var enemy = new Character();

            hero.DealDamage(enemy, 1000);
            Assert.False(enemy.IsAlive);
        }

        [Fact]
        public void CharacterShouldNotHealMoreThanOneHundred()
        {
            var hero = new Character();

            hero.Health = 100;
            hero.Heal(hero, 1000);

            Assert.Equal(1000, hero.Health);
        }

        [Fact]
        public void CharacterShouldNotDealDamageToHimself()
        {
            var hero = new Character();

            bool damageDone = hero.DealDamage(hero, 100);
            Assert.False(damageDone);
        }

        [Fact]
        public void CharacterShouldDealMoreDamageIfHasFiveLevelsMore()
        {
            var hero = new Character();
            var enemy = new Character();
            
            int damageDealt = 175;
            int expectedHealthRemaing = (int) Math.Round(enemy.Health - (damageDealt * 1.5));
            hero.Level = 6; // 5 levels more
            
            hero.DealDamage(enemy, damageDealt);
            Assert.Equal(expectedHealthRemaing, enemy.Health);
        }

        [Fact]
        public void CharacterShouldDealLessDamageIfHasFiveLevelsLess()
        {
            var hero = new Character();
            var enemy = new Character();

            int damageDealt = 175;
            int expectedHealthRemaing = (int) Math.Round(enemy.Health - (damageDealt * 0.5));
            enemy.Level = 6; // 5 levels more
            
            hero.DealDamage(enemy, damageDealt);
            Assert.Equal(expectedHealthRemaing, enemy.Health);
        }

        [Fact]
        public void EnemyShouldBeInRangeToTakeDamage()
        {
            var heroPosition = new Tuple<int, int>(0,0);
            var enemyPosition = new Tuple<int, int>(5,5);

            var hero = new Character(ECharacterType.Melee, heroPosition);
            var enemy = new Character(ECharacterType.Melee, enemyPosition);

            var damageDealtWithSword = hero.DealDamage(enemy, 100);

            //Hero switched weapon
            hero.Type = ECharacterType.Range;
            var damageDealtWithBow = hero.DealDamage(enemy, 50);

            Assert.True(damageDealtWithBow);
            Assert.False(damageDealtWithSword);
        }

        [Fact]
        public void CharacterCanJoinAndLeaveAFaction()
        {
            var horde = new Faction();
            var hero = new Character();

            hero.JoinFaction(horde);

            Assert.Single(horde.Allies);
            Assert.Single(hero.Factions);

            hero.LeaveFaction(horde);

            Assert.Empty(horde.Allies);
            Assert.Empty(hero.Factions);
        }

        [Fact]
        public void EnemyShouldBeOnOtherFactionToDealDamage()
        {
            var alliance = new Faction();
            var horde = new Faction();

            var hero = new Character();
            var enemy = new Character();

            hero.JoinFaction(horde);
            enemy.JoinFaction(horde);
            
            var damageDealtOnHorde = hero.DealDamage(enemy, 100);
            
            enemy.JoinFaction(horde);
            Assert.False(damageDealtOnHorde);
        }

        [Fact]
        public void EnemyShouldBeOnTheSameFactionToHeal()
        {
            var alliance = new Faction();
            var horde = new Faction();

            var hero = new Character();
            var enemy = new Character();

            hero.JoinFaction(horde);
            enemy.JoinFaction(alliance);
            
            var healApplied = hero.Heal(enemy, 100);
            Assert.False(healApplied);
        }
    }
}