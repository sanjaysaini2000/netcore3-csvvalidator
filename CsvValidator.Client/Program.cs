using System;
using CsvValidator.Data;

namespace CsvValidator.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("CSV Validator is starting....");
            try
            {

                IDataValidator dataValidator = new DataValidator();
                DataLoader dataLoader = new DataLoader(dataValidator);
                dataLoader.LoadCsvData();
                dataLoader.LoadCsvValidation();
                dataLoader.ValidateData();
                dataLoader.GenerateValidatedCsvDataFile();

                Console.WriteLine("CSV Validator is completed successfully...");
                Console.WriteLine("Press any key...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(string.Format("CSV Validator is failed with Error {0}...", ex.Message));
            }
        }
    }
}
