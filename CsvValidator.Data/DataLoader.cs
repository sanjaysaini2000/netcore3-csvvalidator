using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using Microsoft.VisualBasic.FileIO;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace CsvValidator.Data
{
    public class DataLoader : IDataLoader
    {
        /// <summary>
        /// Stores collection of CSV validations from validation xml file.
        /// </summary>
        public List<CvsValidation> CvsValidations { get; set; }

        /// <summary>
        /// Stores CSV data from the CSV file.
        /// </summary>
        public DataTable CsvRawData { get; set; }

        /// <summary>
        /// Stores validated CSV formatted data.
        /// </summary>
        public string ValidatedCsvData { get; set; }

        /// <summary>
        /// Holds the reference of DataValidation class.
        /// </summary>
        IDataValidator dataValidator;

        public DataLoader(IDataValidator dv)
        {
            dataValidator = dv;
        }

        /// <summary>
        /// Loads data from CSV file into DataTable object.
        /// </summary>
        public void LoadCsvData()
        {
            string csvFile = Utility.GetCsvFile();

            DataTable csvData = new DataTable();
            using (TextFieldParser parseData = new TextFieldParser(csvFile))
            {
                parseData.SetDelimiters(new string[] { "," });
                parseData.HasFieldsEnclosedInQuotes = true;
                foreach (var item in parseData.ReadFields())
                {
                    csvData.Columns.Add(item);
                }
                while (!parseData.EndOfData)
                {
                    string[] dataFields = parseData.ReadFields();
                    DataRow csvRow = csvData.NewRow();
                    for (int i = 0; i < dataFields.Length; i++)
                    {
                        csvRow[i] = dataFields[i];
                    }
                    csvData.Rows.Add(csvRow);
                }
            }
            CsvRawData = csvData;
            Console.WriteLine("CSV file data is loaded.");
            Console.WriteLine(string.Format("{0} rows loaded from the file.", CsvRawData.Rows.Count));
        }

        /// <summary>
        /// Loads CSV validations from the xml file.
        /// </summary>
        public void LoadCsvValidation()
        {
            string schemaFile = Utility.GetValidationFile();
            List<CvsValidation> validations = new List<CvsValidation>();
            var table = XElement.Load(schemaFile).Elements("table").ToList();
            foreach (var column in table.Elements("column"))
            {
                CvsValidation validation = new CvsValidation();
                validation.Name = column.Attribute("name").Value;
                validation.IsUnique = column.Attribute("isunique").Value;
                validation.IsNull = column.Attribute("isnull").Value;
                validation.DefaultValue = column.Attribute("default").Value;
                validations.Add(validation);
            }
            CvsValidations = validations;
            Console.WriteLine("CSV validation data loaded.");
        }

        /// <summary>
        /// Performs CSV validations on the loaded CSV data.
        /// </summary>
        public void ValidateData()
        {
            foreach (var item in CvsValidations)
            {
                if (Convert.ToBoolean(item.IsUnique))
                    CsvRawData = dataValidator.ValidateUniqueDataColumn(CsvRawData, item.Name);
                if (!Convert.ToBoolean(item.IsNull))
                    CsvRawData = dataValidator.ValidateMissingDataColumn(CsvRawData, item.Name);
                if (!string.IsNullOrEmpty(item.DefaultValue))
                    CsvRawData = dataValidator.ValidateDefaultValueDataColumn(CsvRawData, item.Name, item.DefaultValue);
            }
            Console.WriteLine("CSV Data validation completed.");
        }

        /// <summary>
        /// Reads validated CSV DataTable object and creates a CSV formatted string.
        /// </summary>
        public void RenderValidatedCsvData()
        {
            var columnsHeader = CsvRawData.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToList();
            StringBuilder csvData = new StringBuilder();
            csvData.AppendLine(string.Join(",", columnsHeader));
            foreach (DataRow row in CsvRawData.Rows)
            {
                var columns = row.ItemArray.Select(x => x.ToString()).ToList();
                var i = columns.FindIndex(x => x.Contains(","));
                if (i != -1)
                {
                    var col = columns.ElementAt(i);
                    col = Utility.UpdateColumnValue(col, '"', '"');
                    columns[i] = "\"" + col + "\"";
                }
                csvData.AppendLine(string.Join(",", columns));
            }
            ValidatedCsvData = csvData.ToString().Trim('{', '}');
            Console.WriteLine(string.Format("{0} rows successfully validated.", CsvRawData.Rows.Count));
        }

        /// <summary>
        /// Reads valdated CSV data from CSV formatted string and writes into CSV file on the disk.
        /// </summary>
        public void GenerateValidatedCsvDataFile()
        {
            RenderValidatedCsvData();
            using (StreamWriter sw = new StreamWriter(Utility.ValidatedCsvFileName(), false))
            {
                sw.Write(ValidatedCsvData);
                sw.Flush();
            }
            Console.WriteLine("Validated CSV file is generated.");
        }
    }
}
