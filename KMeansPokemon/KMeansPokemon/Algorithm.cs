﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KMeansPokemon
{
    class Algo
    {
        //Step 1: Make Points out of the pokemon data
        //Step 2: Load Centroids
        //Step 3: Assign points to centroids
        //Step 4: Recalculate centroids

        public CSV csv;

        public int[,] Points;
        public int[,] Centroids;
        public int AmountPokemons;
        public int AmountCentroids;

        public int Columns;

        public int[] AssignedPoints;

        Random random = new Random();

        public Algo(int amountPokemons, int centroids = 3, int Columns = 6)
        {
            this.csv = new CSV();
            this.AmountPokemons = amountPokemons;
            this.AmountCentroids = centroids;
            this.Columns = Columns;

            MakePoints(csv.dt);
            MakeCentroids();
        }

        #region MakePoints/LoadPoints
        private void MakePoints(DataTable Data)
        {
            //index 4 to 11 is HP,Attack,Defense,Sp. Atk,Sp. Def,Speed,Generation
            Points = new int[AmountPokemons, Columns];

            for (int Pokemon = 0; Pokemon < AmountPokemons; Pokemon++)
            {
                int pointindex = 0;
                for (int i = 4; i < 4 + Columns; i++)
                {
                    //Console.WriteLine(Pokemon + "  " + Convert.ToInt32(Data.Rows[Pokemon][i].ToString()));
                    Points[Pokemon, pointindex] = Convert.ToInt32(Data.Rows[Pokemon][i].ToString());

                    pointindex++;
                }
            }

        }
        #endregion

        #region MakeCentroids
        public void MakeCentroids()
        {
            int[,] PokemonTypes =
            {
                {39,52,43,60,50,65},
                {58,64,58,80,65,80},
                {78,84,78,109,85,100}
            };

            Centroids = new int[AmountCentroids, Columns];


            for (int C = 0; C < AmountCentroids; C++)
            {
                int RP = random.Next(AmountPokemons);
                for (int item = 0; item < Columns; item++)
                {
                    Thread.Sleep(30);
                    Centroids[C, item] = Convert.ToInt32(csv.dt.Rows[RP][4+item].ToString());
                    //Centroids[C, item] = PokemonTypes[C, item];
                }
                Console.WriteLine("Centroid Pokemon {0}", csv.dt.Rows[RP][1].ToString());
            }

            //Fire = { 69, 81, 67, 90, 73, 76 };
            //Water = { 70, 71, 72, 72, 69, 64 };
            //Grass = { 66, 31, 73, 72, 71, 59 }

            //First evolution = {39,52,43,60,50,65}
            //Second evolution = {58,64,58,80,65,80}
            //Third evolution = {78,84,78,109,85,100}


        }
        #endregion

        #region AssignPointsToCentroids
        public void AssignPointsToCentroids()
        {
            AssignedPoints = new int[AmountPokemons];


            for (int p = 0; p < AmountPokemons; p++)
            {
                double tempDist = 0;
                double Dist = 0;
                int ClosestCentroid = 0;

                for (int C = 0; C < AmountCentroids; C++)
                {
                    int PokemonStat = 0;
                    int CentroidStat = 0;
                    for (int item = 0; item < Columns; item++)
                    {
                        PokemonStat += Points[p, item];
                        CentroidStat += Centroids[C, item];
                    };

                    int res = CentroidStat - PokemonStat;
                    tempDist = Math.Pow(res, 2);
                    //Console.WriteLine(res);
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
        #endregion

        #region UpdateCentroids
        public void UpdateCentroids()
        {

            for (int centroid = 0; centroid < AmountCentroids; centroid++)
            {
                int[] sum = new int[Columns];
                int sumindex = 0;
                for (int point = 0; point < AmountPokemons; point++)
                {
                    if (AssignedPoints[point] == centroid)
                    {
                        for (int item = 0; item < Columns; item++)
                        {
                            sum[item] += Points[point, item];
                        }
                        sumindex++;
                    }
                }

                for (int SummedItem = 0; SummedItem < Columns; SummedItem++)
                {
                    Centroids[centroid, SummedItem] = (sum[SummedItem] / sumindex);
                }
            }

            Console.WriteLine("\nUpdated Centroids!!!\n");
        }
        #endregion


    }
}
