using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Quantum.Canon;

namespace NaiveBayes
{
    class NaiveBayes
    {
        Random random;
        int NummerCases;
        
        int[,] Pokemons;
        Dictionary<string, Tuple<int,int>> Cases;
        int[] UnknownPokemon;

        public NaiveBayes(int NummerOfCases=100)
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

            //Is it a pidgey or pidgeotto
            UnknownPokemon = new int[2] { 50, 50 };
    }

        public void MakeCases()
        {
            random = new Random();

            string Name = "";
            int index = 0;
            for (int N = 0; N < NummerCases; N++)
            {
                int Pok = random.Next(0, 2);

                if (Pok == 0) { Name = "Pidgey"; }
                else if (Pok == 1) { Name = "Pidgeotto"; }

                Thread.Sleep(1);
                random = new Random();

                Cases.Add(Name+index, 
                    new Tuple<int, int>(
                        random.Next(Pokemons[Pok, 0], Pokemons[Pok, 0]+31),
                        random.Next( Pokemons[Pok, 1], Pokemons[Pok, 1]+31)
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

            int N_PriorPidgeyHP = 0;
            int N_PriorPidgeyAttack = 0;

            int N_PriorPidgeottoHP = 0;
            int N_PriorPidgeottoAttack = 0;

            int PriorPidgeyHP = 0;
            int PriorPidgeyAttack = 0;

            int PriorPidgeottoHP = 0;
            int PriorPidgeottoAttack = 0;


            foreach (KeyValuePair<string, Tuple<int, int>> pok in Cases)
            {
                if (pok.Key[5] == 'y')
                {
                    N_Pidgey++;
                    if (pok.Value.Item1 >= UnknownPokemon[0] && pok.Value.Item1 <= UnknownPokemon[0]+10)
                    {
                        N_PriorPidgeyHP++;
                    }

                    else if (pok.Value.Item2 >= UnknownPokemon[1] && pok.Value.Item2 <= UnknownPokemon[1]+10)
                    {
                        N_PriorPidgeyAttack++;
                    }
                }
                else if (pok.Key[5] == 'o')
                {
                    N_Pidgeotto++;
                    if (pok.Value.Item1 >= UnknownPokemon[0] && pok.Value.Item1 <= UnknownPokemon[0] + 10)
                    {
                        N_PriorPidgeottoHP++;
                    }

                    else if (pok.Value.Item2 >= UnknownPokemon[1] && pok.Value.Item2 <= UnknownPokemon[1] + 10)
                    {
                        N_PriorPidgeottoAttack++;
                    }
                }
                N++;
            }

            PriorPidgeyHP = N_PriorPidgeyHP / N;
            PriorPidgeottoHP = N_PriorPidgeottoHP / N;

            PriorPidgeyAttack = N_PriorPidgeyAttack / N;
            PriorPidgeottoAttack = N_PriorPidgeottoAttack / N;


            int PosteriorPidgey = N_PriorPidgeyHP/N_PriorPidgeyHP;
            int PosteriorPidgeotto = N_PriorPidgeottoHP/N_PriorPidgeottoAttack;

            int ChancePidgey = PosteriorPidgey*(PriorPidgeyAttack/PriorPidgeyHP);
            int ChancePidgeotto = PosteriorPidgeotto * (PriorPidgeottoAttack / PriorPidgeottoHP);

            Console.WriteLine("Chance of unknown Pokemon being \nPidgey={0}\nPidgeotto", ChancePidgey, ChancePidgeotto);
        }

        public void Run()
        {
            Console.WriteLine("Unknown Pokemon stats are HP:50 and Attack:50");
            MakeCases();
            CalculateProbability();
        }

    }
}
