using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBotUpload
{
    public enum Weapon { Rock, Paper, Scissors, Dynamite, WaterBallon }

    public enum BattleResult { Win, Lose, Draw };

    public class MemoryForStreak
    {
        public double dynValue = 0.333;
        public double watValue = 0.333;
        public double rpsValue = 0.333;


        public void AddMove(Weapon enemyWeapon)
        {


            if (enemyWeapon == Weapon.Dynamite)
            {
                watValue += 0.25;
            }
            else if (enemyWeapon == Weapon.WaterBallon)
            {
                rpsValue += 0.25;
            }
            else
            {
                dynValue += 0.25;
            }

            double totalValue = dynValue + watValue + rpsValue;

            dynValue = dynValue / totalValue;
            watValue = watValue / totalValue;
            rpsValue = rpsValue / totalValue;

        }


    }

    public class Memory
    {
        MemoryForStreak highMemory = new MemoryForStreak();
        MemoryForStreak medMemory = new MemoryForStreak();
        MemoryForStreak lowMemory = new MemoryForStreak();

        public void addEnemyMove(int drawStreak, double dynamiteValue, Weapon enemyWeapon)
        {
            if (drawStreak > dynamiteValue + 0.5)
            {
                highMemory.AddMove(enemyWeapon);
            }
            else if (drawStreak < dynamiteValue - 0.5)
            {
                lowMemory.AddMove(enemyWeapon);
            }
            else
            {
                medMemory.AddMove(enemyWeapon);
            }
        }

        public MemoryForStreak GetMemory(int drawStreak, double dynamiteValue)
        {
            if (drawStreak > dynamiteValue + 0.5)
            {
                return highMemory;
            }
            else if (drawStreak < dynamiteValue - 0.5)
            {
                return lowMemory;
            }
            else
            {
                return medMemory;
            }
        }
    }

    public interface IBot2
    {
        string Name { get; }

        void NewGame(string enemyBotName);

        void HandleBattleResult(BattleResult result, Weapon yourWeapon, Weapon enemiesWeapon);

        Weapon GetNextWeaponChoice();

        void HandleFinalResult(bool isWin);

    }


    public class Battle
    {
        public BattleResult P1BattleResult { get; }
        public BattleResult P2BattleResult { get; }
        public Weapon P1Weapon { get; }
        public Weapon P2Weapon { get; }

        public Battle(Weapon p1Weapon, Weapon p2Weapon)
        {
            P1Weapon = p1Weapon;
            P2Weapon = p2Weapon;
            P1BattleResult = BattleResultForFirstArg(p1Weapon, p2Weapon);
            P2BattleResult = BattleResultForFirstArg(p2Weapon, p1Weapon);
        }

        private BattleResult BattleResultForFirstArg(Weapon w1, Weapon w2)
        {
            if (w1 == w2)
            {
                return BattleResult.Draw;
            }

            if (
                (w1 == Weapon.Dynamite && w2 != Weapon.WaterBallon)
                || (w1 == Weapon.WaterBallon && w2 == Weapon.Dynamite)
                || (w1 != Weapon.Dynamite && w2 == Weapon.WaterBallon)
                || (w1 == Weapon.Rock && w2 == Weapon.Scissors)
                || (w1 == Weapon.Scissors && w2 == Weapon.Paper)
                || (w1 == Weapon.Paper && w2 == Weapon.Rock)
                )
            {
                return BattleResult.Win;
            }

            return BattleResult.Lose;
        }

        public override string ToString()
        {
            return "Player 1 " + P1BattleResult.ToString() + ", " + P1Weapon.ToString() + ", " + P2Weapon.ToString();
        }
    }
}
