using System;
using System.Configuration;
using System.IO;

namespace CsvValidator.Data
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

        public static string UpdateColumnValue(string colVlaue, char findChar, char insertChar)
        {
            int startIndex = 0;
            int colIndex = colVlaue.IndexOf(findChar, startIndex);
            while (colIndex != -1)
            {
                colVlaue = colVlaue.Insert(colIndex, insertChar.ToString());
                startIndex = colIndex + 2;
                colIndex = colVlaue.IndexOf(findChar, startIndex);
            }
            return colVlaue;
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