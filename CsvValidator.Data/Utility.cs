using System;
using System.Configuration;
using System.IO;

namespace CsvValidator.Data
{
    static public class Utility
    {

        /// <summary>
        /// Fetch validation file path from the app.config file.
        /// </summary>
        /// <returns>CSV file path</returns>
        public static String GetCsvFile()
        {
            return Path.GetFullPath(ConfigurationManager.AppSettings["csvfile"]);
        }

        /// <summary>
        /// Fetch validation file path from the app.config file.
        /// </summary>
        /// <returns>Validation XML file path</returns>
        public static String GetValidationFile()
        {
            return Path.GetFullPath(ConfigurationManager.AppSettings["validationfile"]);
        }

        /// <summary>
        /// It updates the column value by inserting the given character to the column value.
        /// </summary>
        /// <param name="colVlaue"></param>
        /// <param name="findChar"></param>
        /// <param name="insertChar"></param>
        /// <returns>Updated column value</returns>
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

        /// <summary>
        /// Creates file path for validated CSV file.
        /// </summary>
        /// <returns>Validated CSV file path</returns>
        public static string ValidatedCsvFileName()
        {
            string csvFilePath = Utility.GetCsvFile();
            string path = Path.GetDirectoryName(csvFilePath);
            string filename = Path.GetFileNameWithoutExtension(csvFilePath) + "_" + "validated.csv";
            return Path.Combine(path, filename);

        }

    }
}