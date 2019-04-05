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
                try
                {
                    AssignPointsToCentroids();
                    Output();
                    UpdateCentroids();
                }

                catch
                {
                    Console.WriteLine("\nCan't Update Centroids NO MO\n");
                    break;
                }
            }
        }

        #region Output
        public void Output()
        {
            //Print Output
            string s = "";
            for (int i = 0; i < AmountCentroids; i++)
            {
                s+= "\n";
                s += "Centroid: " + i+"|";
                for (int j = 0; j < Columns; j++)
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
        #endregion
    }




}
