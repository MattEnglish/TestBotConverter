using BotInterface.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBotUpload;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = new ExampleBot();
            var gamestate = new Gamestate();
            gamestate.SetRounds(new Round[0]);
            Round r = new Round();
            r.SetP1(Move.R);
            r.SetP2(Move.R);
            gamestate.SetRounds(new Round[1] { r });
            var y = x.MakeMove(gamestate );
            gamestate.SetRounds(new Round[2] { r, r });
            var z = x.MakeMove(gamestate);
        }
    }
}
