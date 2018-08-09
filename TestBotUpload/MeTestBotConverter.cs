using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotInterface.Bot;
using BotInterface.Game;

namespace TestBotUpload
{

public class ExampleBot : IBot
{
public Move MakeMove(Gamestate gamestate)
{
   return Move.P;
}
}
    /*
    public class ExampleBot : IBot
    {
        //public IBot2 botToConvert;

        public MeTestBotConverter()
        {
            //botToConvert = new NeuroEvolveBot(new CustomNeuralNet());
            //botToConvert = new WaveBot();
            //botToConvert.NewGame("");
        }

        public Move MakeMove(Gamestate gamestate)
        {
            return Move.P;
            /*
            int numberOfRounds = gamestate.GetRounds().Length;

            if (numberOfRounds == 0)
            {
                return WeaponConvert.Convert(botToConvert.GetNextWeaponChoice());
            }


            var mostRecentRound = gamestate.GetRounds()[numberOfRounds - 1];
            var GetMyMostRecentMove = mostRecentRound.GetP1();
            var GetMyEnemiesMostRecentMove = mostRecentRound.GetP2();

            var mostRecentWeaponChoice = WeaponConvert.Convert(GetMyMostRecentMove);
            var enemyMostRecentWeaponChoice = WeaponConvert.Convert(GetMyEnemiesMostRecentMove);

            var battleResult = new Battle(mostRecentWeaponChoice, enemyMostRecentWeaponChoice).P1BattleResult;

            botToConvert.HandleBattleResult(battleResult, mostRecentWeaponChoice, enemyMostRecentWeaponChoice);

            var WeaponChoice = botToConvert.GetNextWeaponChoice();

            return WeaponConvert.Convert(WeaponChoice);
        }

        public static class WeaponConvert
        {



            public static Move Convert(Weapon weapon)
            {
                switch (weapon)
                {
                    case Weapon.Rock:
                        return Move.R;
                    case Weapon.Scissors:
                        return Move.S;
                    case Weapon.Paper:
                        return Move.P;
                    case Weapon.Dynamite:
                        return Move.D;
                    case Weapon.WaterBallon:
                        return Move.W;
                }

                throw new Exception();

            }

            public static Weapon Convert(Move weapon)
            {
                switch (weapon)
                {
                    case Move.R:
                        return Weapon.Rock;
                    case Move.S:
                        return Weapon.Scissors;
                    case Move.P:
                        return Weapon.Paper;
                    case Move.D:
                        return Weapon.Dynamite;
                    case Move.W:
                        return Weapon.WaterBallon;
                }

                throw new Exception();

            }
        }
    }*/

}
