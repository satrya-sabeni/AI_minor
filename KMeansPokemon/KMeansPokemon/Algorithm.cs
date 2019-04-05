using System;
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

        CSV csv;

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
            Centroids = new int[AmountCentroids, Columns];

            for (int C = 0; C < AmountCentroids; C++)
            {
                for (int item = 0; item < Columns; item++)
                {
                    Thread.Sleep(3);
                    Centroids[C, item] = random.Next(1, 100);
                }
            }
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
                    for (int item = 0; item < Columns; item++)
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
