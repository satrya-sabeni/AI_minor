using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace KMeansPokemonWindowsForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Algorithm a = new Algorithm(9,chart1);
            a.Run();
        }

        private void chart1_Click(object sender, EventArgs e)
        {
        }
    }

    class Algorithm : Algo
    {
        public Algorithm(int amountPokemons, Chart chart, int centroids = 3, int Columns = 6) : base(amountPokemons, chart,centroids, Columns)
        { }

        public void Run()
        {
            for (int iteration = 0; iteration < 5; iteration++)
            {
                //try
                //{
                    AssignPointsToCentroids();
                    Output();
                    UpdateCentroids();
                //}

                //catch (Exception ex)
                //{
                //    Console.WriteLine("\nCan't Update Centroids NO MO\n");
                //    Console.WriteLine(ex);
                //    break;
                //}
            }
        }

        #region Output
        public void Output()
        {
            //Print Centroids
            string s = "";
            for (int i = 0; i < AmountCentroids; i++)
            {
                s += "\n";
                s += "Centroid: " + i + "|";
                for (int j = 0; j < Columns; j++)
                {
                    s += string.Format(" {0} ", Centroids[i, j]);
                }
                s += "\n";
            }
            Console.WriteLine(s);

            //Print stats

            string GetStats(string PName)
            {
                string Stats = "";
                for (int Pokemon = 0; Pokemon < AmountPokemons; Pokemon++)
                {
                    //Console.WriteLine(Pokemon + "  " + Convert.ToInt32(Data.Rows[Pokemon][i].ToString()));
                    if (csv.dt.Rows[Pokemon][1].ToString() == PName)
                    {
                        for (int i = 4; i < 4 + Columns; i++)
                        {
                            Stats += "|";
                            Stats += " " + Convert.ToInt32(csv.dt.Rows[Pokemon][i].ToString()) + " ";
                        }
                        
                    }
                    
                }
                return Stats;
            }
            string AltString = "";
            int pok = 0;
            for (int k = 0; k < AmountCentroids; k++)
            {
                foreach (KeyValuePair<string, int> kvp in AssignedPoints)
                {
                    // Console.WriteLine("Point/pokemon: {0} Closest to Centroid: {1}",csv.dt.Rows[pok][1].ToString(), AP);
                    if (k == kvp.Value)
                    {
                        AltString += string.Format("Centroid {0} Pokemon: {1} {2}\n", k, kvp.Key, GetStats(kvp.Key));
                        pok++;

                    }
                }
            }

            Console.WriteLine(AltString);
        }

        #endregion
    }
}