using System;
using System.Configuration;
using System.IO;

namespace CsvValidator.DataLoader
{
    static public class Utility
    {

        public static String GetCsvFile()
        {

            return ConfigurationManager.AppSettings["csvfile"];
        }

        public static String GetSchemaFile()
        {
            return ConfigurationManager.AppSettings["validationfile"];
        }

        public static string ValidatedCsvFileName()
        {
            string csvFilePath = Utility.GetCsvFile();
            string path = Path.GetDirectoryName(csvFilePath);
            string filename = Path.GetFileNameWithoutExtension(csvFilePath) + "_" + "validated.csv";
            return Path.Combine(path, filename);

        }

    }
}