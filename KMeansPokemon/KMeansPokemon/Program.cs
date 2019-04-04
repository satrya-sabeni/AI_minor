using System;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Threading;

namespace KMeansPokemon
{
    class Program
    {
        static void Main(string[] args)
        {

           // csv.PrintDataTable();


            Algorithm a = new Algorithm(50);
            a.Run();
            
        }
    }

    class Algorithm: Algo
    {
        public Algorithm(int amountPokemons, int centroids = 3) : base(amountPokemons, centroids)
        { }

        public void Run()
        {
            for(int iteration = 0; iteration < 2; iteration++)
            {
                AssignPointsToCentroids();
                Output();
                UpdateCentroids();

            }

        }

        public void Output()
        {
            //Print Output
            string s = "";
            for (int i = 0; i < AmountCentroids; i++)
            {
                s += "\n";
                s += "Centroid: " + i+"|";
                for (int j = 0; j < 7; j++)
                {
                    s += string.Format(" {0} ", Centroids[i, j]);
                }
                s += "\n";
            }
            Console.WriteLine(s);

            int pok = 0;
            foreach (int AP in AssignedPoints)
            {
                Console.WriteLine("Point/pokemon: {0} Closest to Centroid: {1}", pok, AP);
                pok++;
            }
        }
    }

    class Algo
    {
        //Step 1: Make Points out of the pokemon data
        //Step 2: Load Centroids
        //Step 3: Assign points to centroids
        //Step 4: Recalculate centroids

        CSV csv;

        public int[,] Points;
        public int[,] Centroids;
        public int AmountPokemons;
        public int AmountCentroids;

        public int[] AssignedPoints;

        Random random = new Random();

        public Algo(int amountPokemons, int centroids=3)
        {
            this.csv = new CSV();
            this.AmountPokemons = amountPokemons;
            this.AmountCentroids = centroids;

            MakePoints(csv.dt);
            MakeCentroids();
        }

        private void MakePoints(DataTable Data)
        {
            //index 4 to 11 is HP,Attack,Defense,Sp. Atk,Sp. Def,Speed,Generation
            Points = new int[AmountPokemons, 7];

            for(int Pokemon= 0; Pokemon<AmountPokemons; Pokemon++)
            {
                int pointindex = 0;
                for (int i = 4; i < 11; i++)
                {
                    //Console.WriteLine(Pokemon + "  " + Convert.ToInt32(Data.Rows[Pokemon][i].ToString()));
                    Points[Pokemon, pointindex] = Convert.ToInt32(Data.Rows[Pokemon][i].ToString());

                    pointindex++;
                }
            }
            
        }

        public void MakeCentroids()
        {
            Centroids = new int[AmountCentroids, 7];

            for(int C = 0; C < AmountCentroids; C++)
            {
                for(int item = 0; item < 7; item++)
                {
                    Thread.Sleep(3);
                    Centroids[C, item] = random.Next(1, 100); 
                }
            }
        }

        public void AssignPointsToCentroids()
        {
            AssignedPoints = new int[AmountPokemons];


            for(int p= 0; p < AmountPokemons; p++)
            {
                double tempDist = 0;
                double Dist = 0;
                int ClosestCentroid = 0;

                for (int C = 0; C < AmountCentroids; C++)
                {
                    for (int item = 0; item < 7; item++)
                    {
                        tempDist += Math.Pow(Centroids[C, item] - Points[p, item], 2);
                        
                    }

                    tempDist = Math.Sqrt(tempDist);

                    if (tempDist > Dist)
                    {
                        Dist = tempDist;
                        ClosestCentroid = C;

                        //Console.WriteLine(Dist);
                    }
                }
                AssignedPoints[p] = ClosestCentroid;
                
            }

            
        }

        public void UpdateCentroids()
        {
            
            for (int centroid = 0; centroid < AmountCentroids; centroid++)
            {
                int[] sum = new int[7];
                int sumindex = 0;
                for(int point = 0; point < AmountPokemons; point++)
                {
                    if (AssignedPoints[point] == centroid)
                    {
                        for(int item = 0; item < 7; item++)
                        {
                            sum[item] += Points[point, item];
                        }
                        sumindex++;
                    }
                }

                for(int SummedItem=0;SummedItem<7;SummedItem++)
                {
                    Centroids[centroid, SummedItem] = (sum[SummedItem] / sumindex);
                }
            }
        }

        
    }

    
}
