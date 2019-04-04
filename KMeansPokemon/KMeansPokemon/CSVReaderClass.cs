using System;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace KMeansPokemon
{
    public class CSV
    {
        StreamReader sr;
        public DataTable dt { get; set; }

        public CSV()
        {
            sr = new StreamReader(File.OpenRead(@"..\..\Data\pokemon.csv"));

            LoadDataTable();
        }

        private void LoadDataTable()
        {
            string[] headers = sr.ReadLine().Split(',');
            dt = new DataTable();
            foreach (string header in headers)
            {
                dt.Columns.Add(header);
            }
            while (!sr.EndOfStream)
            {
                string[] rows = Regex.Split(sr.ReadLine(), ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                DataRow dr = dt.NewRow();
                for (int i = 0; i < headers.Length; i++)
                {
                    dr[i] = rows[i];
                }
                dt.Rows.Add(dr);
            }
        }

        public void PrintDataTable()
        {
            DataTable workTable = dt;


            DataRow[] currentRows = workTable.Select(
                null, null, DataViewRowState.CurrentRows);

            foreach (DataColumn column in workTable.Columns)
                Console.Write("\t{0}", column.ColumnName);

            Console.WriteLine("\tRowState");

            foreach (DataRow row in currentRows)
            {
                foreach (DataColumn column in workTable.Columns)
                    Console.Write("\t{0}", row[column]);

                Console.WriteLine("\t" + row.RowState);
            }
        }
    }
}
