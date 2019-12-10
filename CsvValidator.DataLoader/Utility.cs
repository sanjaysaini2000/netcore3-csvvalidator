using System;
using System.Configuration;

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
            return ConfigurationManager.AppSettings["schemafile"];
        }
    }
}