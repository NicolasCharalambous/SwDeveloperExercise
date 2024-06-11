using SwDeveloperExercise.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SwDeveloperExercise
{
    class Program
    {
        const string delimiter = "##";
        static void Main(string[] args)
        {
            string strReadFile = @"C:\Users\Nicolas\Source\Repos\SwDeveloperExercise\SwDeveloperExercise\SwDeveloperExercise\Input\sales.txt";
            var fileLines = File.ReadAllLines(strReadFile).ToList();
            List<SalesItem> sales = new List<SalesItem>();
            try
            {
                foreach (string line in fileLines)
                {
                    SalesItem sale = new SalesItem();
                    sale.Date = DateTime.Parse(line.Split(new string[] { delimiter }, StringSplitOptions.None)[0]);
                    sale.Amount = Decimal.Parse(line.Split(new string[] { delimiter }, StringSplitOptions.None)[1]);
                    sales.Add(sale);
                }

                Console.WriteLine("Enter from year for Average:");
                string fromYearAvg = Console.ReadLine();
                Console.WriteLine("Enter to year for Average:");
                string toYearAvg = Console.ReadLine();

                Program p = new Program();
                p.AverageCalculation(sales, fromYearAvg, toYearAvg);

                Console.WriteLine("Enter specific year for Standard Deviation:");
                string specYear = Console.ReadLine();

                p.StandardDeviationCalculation(sales, specYear);

                Console.WriteLine("Enter from year for Standard Deviation:");
                string fromYearSD = Console.ReadLine();
                Console.WriteLine("Enter to year for Standard Deviation:");
                string toYearSD = Console.ReadLine();

                p.StandardDeviationCalculation(sales, fromYearSD, toYearSD);

                Console.ReadKey();
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public void AverageCalculation(List<SalesItem> sales, string fromYear, string toYear)
        {
            decimal avg = sales.Where(x => x.Date.Year >= int.Parse(fromYear) && x.Date.Year <= int.Parse(toYear))
                .Select(x => x.Amount).DefaultIfEmpty(0).Average();
            Console.WriteLine($"Average ({fromYear}-{toYear}): {avg}");
        }

        public void StandardDeviationCalculation(List<SalesItem> sales, string year)
        {
            IEnumerable<double> amounts = sales.Where(x => x.Date.Year == int.Parse(year))
                .Select(x => (double)x.Amount);
            double avg = amounts.Average();

            double sumOfSquaresOfDifferences = amounts.Select(val => (val - avg) * (val - avg)).Sum();
            double sd = Math.Sqrt(sumOfSquaresOfDifferences / amounts.Count());
            Console.WriteLine($"Standard Deviation ({year}): {sd}");
        }

        public void StandardDeviationCalculation(List<SalesItem> sales, string fromYear, string toYear)
        {
            IEnumerable<double> amounts = sales.Where(x => x.Date.Year >= int.Parse(fromYear) && x.Date.Year <= int.Parse(toYear))
                .Select(x => (double)x.Amount);
            double avg = amounts.Average();

            double sumOfSquaresOfDifferences = amounts.Select(val => (val - avg) * (val - avg)).Sum();
            double sd = Math.Sqrt(sumOfSquaresOfDifferences / amounts.Count());
            Console.WriteLine($"Standard Deviation ({fromYear}-{toYear}): {sd}");
        }
    }
}
