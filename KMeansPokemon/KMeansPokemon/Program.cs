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
            Algorithm a = new Algorithm(9);
            a.Run();
        }
    }

    class Algorithm: Algo
    {
        public Algorithm(int amountPokemons, int centroids = 3,int Columns=6) : base(amountPokemons, centroids,Columns)
        { }

        public void Run()
        {
            for(int iteration = 0; iteration < 5; iteration++)
            {
                try
                {
                    AssignPointsToCentroids();
                    Output();
                    UpdateCentroids();
                }

                catch(Exception ex)
                {
                    Console.WriteLine("\nCan't Update Centroids NO MO\n");
                    break;
                }
            }
        }

        #region Output
        public void Output()
        {
            //Print Centroids
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

            //Print stats
            
            string GetStats(int pnumber)
            {
                string Stats = "";
                int pointindex = 0;
                for (int i = 4; i < 4 + Columns; i++)
                {
                    //Console.WriteLine(Pokemon + "  " + Convert.ToInt32(Data.Rows[Pokemon][i].ToString()));
                    Stats += "|";
                    Stats += " " + Convert.ToInt32(csv.dt.Rows[pnumber][i].ToString()) + " ";
                    pointindex++;
                }
                return Stats;
            }
            string AltString = "";
            int pok = 0;
            for(int k = 0; k < AmountCentroids; k++)
            {
                foreach (int AP in AssignedPoints)
                {
                    // Console.WriteLine("Point/pokemon: {0} Closest to Centroid: {1}",csv.dt.Rows[pok][1].ToString(), AP);
                    if (k == AP)
                    {
                        AltString += string.Format("Centroid {0} Pokemon: {1} {2}\n", k, csv.dt.Rows[pok][1].ToString(),GetStats(pok));
                        pok++;

                    }
                }
            }
            
            Console.WriteLine(AltString);
        }
        #endregion
    }




}
