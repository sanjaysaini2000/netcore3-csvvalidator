using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using Microsoft.VisualBasic.FileIO;

namespace CsvValidator.DataLoader
{
    public class DataLoader : IDataLoader
    {
        public List<CvsValidation> CvsColumnsSchema { get; set; }
        public DataTable CsvRawData { get; set; }
        public string ValidatedCsvData { get; set; }
        IDataValidator _dataValidator;

        public DataLoader(IDataValidator dataValidator)
        {
            _dataValidator = dataValidator;
        }

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
        }

        public void LoadCsvValidation()
        {
            //todo
        }

    }
}
