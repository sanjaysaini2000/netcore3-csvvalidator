using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using Microsoft.VisualBasic.FileIO;
using System.Text;
using System.IO;

namespace CsvValidator.Data
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
        }

        public void GenerateValidatedCsvDataFile()
        {
            RenderValidatedCsvData();
            using (StreamWriter sw = new StreamWriter(Utility.ValidatedCsvFileName(), false))
            {
                sw.Write(ValidatedCsvData);
                sw.Flush();
            }
        }

    }
}
