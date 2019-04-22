using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1);
                NaiveBayes a = new NaiveBayes();
                a.Run();
            }
            
        }
    }

    class NaiveBayes
    {
        Random random;
        int NummerCases;

        int[,] Pokemons;
        Dictionary<string, Tuple<int, int>> Cases;
        int[] UnknownPokemon;

        public NaiveBayes(int NummerOfCases = 100)
        {
            NummerCases = NummerOfCases;
            Cases = new Dictionary<string, Tuple<int, int>>();
            //Data
            //Pidgey HP = 40, attack= 45
            //pidgeotto HP = 63 attack = 60

            Pokemons = new int[2, 2];
            int[] Pidgey = new int[2] { 40, 45 };
            int[] Pidgeotto = new int[2] { 63, 60 };

            Pokemons[0, 0] = Pidgey[0];
            Pokemons[0, 1] = Pidgey[1];

            Pokemons[1, 0] = Pidgeotto[0];
            Pokemons[1, 1] = Pidgeotto[1];

            
        }

        public void MakeCases()
        {
            random = new Random();
            int rStat = random.Next(0, 31);
            //Is it a pidgey or pidgeotto
            UnknownPokemon = new int[2] { random.Next(Pokemons[0,0],Pokemons[0,0]+rStat), random.Next(Pokemons[0, 1], Pokemons[0, 1] + rStat) };
            Console.WriteLine("\nPidgey stats| hp:{0} | attack:{1}", Pokemons[0, 0], Pokemons[0, 1]);
            Console.WriteLine("Pidgeotto stats| hp:{0} | attack:{1}", Pokemons[1, 0], Pokemons[1, 1]);

            Console.WriteLine("Unknown Pokemon stats are HP:{0} and Attack:{1}", UnknownPokemon[0], UnknownPokemon[1]);

            string Name = "";
            int index = 0;
            for (int N = 0; N < NummerCases; N++)
            {
                //Thread.Sleep(1);
                random = new Random();

                int Pok = random.Next(0, 2);

                if (Pok == 0) { Name = "Pidgey"; }
                else if (Pok == 1) { Name = "Pidgeotto"; }

                int randomstat = random.Next(0, 31);
                int randomhp = random.Next(Pokemons[Pok, 0] +randomstat, Pokemons[Pok, 0] + randomstat );
                randomstat= random.Next(0, 31);
                int randomattack = random.Next(Pokemons[Pok, 1] + randomstat , Pokemons[Pok, 1] + randomstat);
                //31 is the range for a stat to differ
                Cases.Add(Name + index,
                    new Tuple<int, int>(
                        randomhp,
                        randomattack
                        ));
                index++;

            }

        }

        public void CalculateProbability()
        {
            //Calculate Prior
            int N_Pidgey = 0;
            int N_Pidgeotto = 0;
            int N = 0;

            double N_PriorPidgeyHP = 0;
            double N_PriorPidgeyAttack = 0;

            double N_PriorPidgeottoHP = 0;
            double N_PriorPidgeottoAttack = 0;

            double PriorPidgeyHP = 0;
            double PriorPidgeyAttack = 0;

            double PriorPidgeottoHP = 0;
            double PriorPidgeottoAttack = 0;


            foreach (KeyValuePair<string, Tuple<int, int>> pok in Cases)
            {
                //Console.WriteLine("{0} | hp:{1} | attack:{2}", pok.Key, pok.Value.Item1, pok.Value.Item2);
                if (pok.Key[5] == 'y')
                {
                    N_Pidgey++;
                    if (pok.Value.Item1 >= UnknownPokemon[0] && pok.Value.Item1 <= UnknownPokemon[0] + 10)
                    {
                        N_PriorPidgeyHP++;
                    }

                    if (pok.Value.Item2 >= UnknownPokemon[1] && pok.Value.Item2 <= UnknownPokemon[1] + 10)
                    {
                        N_PriorPidgeyAttack++;
                    }
                }
                if (pok.Key[5] == 'o')
                {
                    N_Pidgeotto++;

                    if (pok.Value.Item1 >= UnknownPokemon[0] - 10 && pok.Value.Item1 <= UnknownPokemon[0] + 10)
                    {
                        N_PriorPidgeottoHP++;
                    }

                    if (pok.Value.Item2 >= UnknownPokemon[1]-10 && pok.Value.Item2 <= UnknownPokemon[1] + 10)
                    {
                        N_PriorPidgeottoAttack++;

                    }
                }
                N++;
            }

            PriorPidgeyHP = N_PriorPidgeyHP / N;
            PriorPidgeyAttack = N_PriorPidgeyAttack / N;

            PriorPidgeottoHP = N_PriorPidgeottoHP / N;
            PriorPidgeottoAttack = N_PriorPidgeottoAttack / N;


            double PosteriorPidgey = N_PriorPidgeyHP / N_PriorPidgeyHP;
            double PosteriorPidgeotto = N_PriorPidgeottoHP / N_PriorPidgeottoAttack;

            double ChancePidgey = PosteriorPidgey * (PriorPidgeyAttack / PriorPidgeyHP);
            double ChancePidgeotto = PosteriorPidgeotto * (PriorPidgeottoAttack / PriorPidgeottoHP);
            Console.WriteLine("\n{0} Cases/data", NummerCases);
            Console.WriteLine("Chance of unknown Pokemon being \nPidgey={0}\nPidgeotto={1}\n(NaN=0)", ChancePidgey, ChancePidgeotto);
            Console.WriteLine("-----------------------------------------------------------------------------");
        }

        public void Run()
        {
            
            MakeCases();
            CalculateProbability();

        }

    }
}
