using BotInterface.Bot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBotUpload
{

    class NeuroEvolveBot : IBot2
    {
        public string Name { get; }

        private double currentDrawStreak;
        private double MyWinsLeft;
        private double EnemyWinsLeft;
        private double DynamiteLeft;
        private double EnemyDynamiteLeft;
        private CustomNeuralNet network;
        private Random rand;


        public NeuroEvolveBot(CustomNeuralNet net, int seed = 3, string name = "NeuroEvolve")
        {
            rand = new Random();
            rand.Next();
            network = net;
            Name = name;
        }

        public Weapon GetNextWeaponChoice()
        {
            if (currentDrawStreak == 0)
            {
                return (Weapon)rand.Next(3);
            }
            var outputs = network.Forward(new double[1, 5] { { currentDrawStreak / 5, MyWinsLeft / 1000, EnemyWinsLeft / 1000, DynamiteLeft / 100, EnemyDynamiteLeft / 100 } });
            var probs = new probVector(outputs[0, 0], outputs[0, 1], outputs[0, 2]);
            probs = probs.Normalise();
            var weaponChoice = rand.NextDouble();
            if (weaponChoice < probs.watProb)
            {
                if (EnemyDynamiteLeft > 0)
                {
                    return Weapon.WaterBallon;
                }
            }
            if (weaponChoice < probs.watProb + probs.dynProb)
            {
                if (DynamiteLeft > 0)
                {
                    return Weapon.Dynamite;
                }
            }

            return (Weapon)rand.Next(3);// returns rock, paper, scissors randomly.
        }


        public void HandleBattleResult(BattleResult result, Weapon yourWeapon, Weapon enemiesWeapon)
        {
            if (result == BattleResult.Draw)
            {
                currentDrawStreak++;
            }
            else
            {
                currentDrawStreak = 0;
            }
            if (result == BattleResult.Win)
            {
                MyWinsLeft -= currentDrawStreak + 1;
            }
            if (result == BattleResult.Lose)
            {
                EnemyWinsLeft -= currentDrawStreak + 1;
            }

            if (enemiesWeapon == Weapon.Dynamite)
            {
                EnemyDynamiteLeft--;
            }
            if (yourWeapon == Weapon.Dynamite)
            {
                DynamiteLeft--;
            }
        }

        public void HandleFinalResult(bool isWin)
        {
            throw new NotImplementedException();
        }

        public void NewGame(string enemyBotName)
        {
            currentDrawStreak = 0;
            MyWinsLeft = 1000;
            EnemyWinsLeft = 1000;
            DynamiteLeft = 100;
            EnemyDynamiteLeft = 100;
        }
    }

    public class CustomNeuralNet
    {
        private double[] chromosome = {4.09127315487594,
            1.03509032260818,
            1.59088312522279,
            2.84938177798884,
            1.2540769773385,
            0.0786729685801587,
            3.62986019953569,
            10.2041014676762,
            0.560911974621894,
            -1.67067006940757,
            -6.39510443838845,
            21.7372019475097,
            27.2136276900884,
            -17.9941990534444,
            63.4055503627202,
            -33.2334766865179,
            -31.9003848741661,
            -29.4510418869476,
            -2.5612143654882,
            5.04453299700761,
            172.253450363779,
            -272.367159509375,
            302.114317873506,
            -41.4845624638135,
            -18.5153096244246,
            -0.580445775627334,
            -29.1840046564128,
            -11.6024117890832,
            9.22990357622967,
            -0.700906189340301,
            -0.851516428897239,
            0.175879979953249
        };

        
            private int inputLayerSize = 5;
            private int outputLayerSize = 3;
            private int hiddenLayerSize = 4;

            public double[,] W1;
            public double[,] W2;

            private double[,] dJdW1;
            private double[,] dJdW2;

            public CustomNeuralNet()
            {
                W1 = new double[inputLayerSize, hiddenLayerSize];
                W2 = new double[hiddenLayerSize, outputLayerSize];
                var r = new Random();

                ApplyFuncToEveryElement(W1, x => r.NextDouble());
                ApplyFuncToEveryElement(W2, x => r.NextDouble());

            int geneNumber = 0;
            for (int i = 0; i < W1.GetLength(0); i++)
            {
                for (int j = 0; j < W1.GetLength(1); j++)
                {
                    W1[i, j] = chromosome[geneNumber];
                    geneNumber++;
                }
            }

            for (int i = 0; i < W2.GetLength(0); i++)
            {
                for (int j = 0; j < W2.GetLength(1); j++)
                {
                    W2[i, j] = chromosome[geneNumber];
                    geneNumber++;
                }
            }

        }

            /*private static double costFunction(double[,] y, double[,] ry)
            {
                var subtracted = Elementwise.Subtract(y, ry);
                var differnce = ApplyFuncToEveryElement(subtracted, x => 0.5 * Math.Pow(x, 2));
                return Matrix.Sum(differnce);
            }*/

            public static double Sigmoid(double z)
            {
                return 1 / (1 + Math.Exp(-z));
            }

            public static double SigmoidPrime(double z)
            {
                return Math.Exp(-z) / Math.Pow((1 + Math.Exp(-z)), 2);
            }

            public static double[,] sigmoid(double[,] z)
            {
                for (int i = 0; i < z.GetLength(0); i++)
                {
                    for (int j = 0; j < z.GetLength(1); j++)
                    {
                        z[i, j] = Sigmoid(z[i, j]);
                    }
                }
                return z;
            }

            private static double[,] ApplyFuncToEveryElement(double[,] z, Func<double, double> func)
            {
                for (int i = 0; i < z.GetLength(0); i++)
                {
                    for (int j = 0; j < z.GetLength(1); j++)
                    {
                        z[i, j] = func(z[i, j]);
                    }
                }
                return z;
            }

        public double[,] MultiplyMatrix(double[,] a, double[,] b)
        {
            
                var c = new double[a.GetLength(0), b.GetLength(1)];
                for (int i = 0; i < c.GetLength(0); i++)
                {
                    for (int j = 0; j < c.GetLength(1); j++)
                    {
                        c[i, j] = 0;
                        for (int k = 0; k < a.GetLength(1); k++) // OR k<b.GetLength(0)
                            c[i, j] = c[i, j] + a[i, k] * b[k, j];
                    }
                }

            return c;
            
        }




        public double[,] Forward(double[,] inputs)
            {
                var x = MultiplyMatrix(inputs, W1);
                var a = sigmoid(x);
                var y = MultiplyMatrix(x, W2);
                var b = sigmoid(y);
                return b;
            }
            

            private void input(int[] x)
            {

            }
        }




    




}
