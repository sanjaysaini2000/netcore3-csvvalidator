using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CsvValidator.Data
{
    public class DataValidator : IDataValidator
    {
        /// <summary>
        /// It removes the rows containing the duplicate value of the given column.
        /// </summary>
        /// <param name="CsvRawData"></param>
        /// <param name="columnName"></param>
        /// <returns>Validated CSV data</returns>
        public DataTable ValidateUniqueDataColumn(DataTable CsvRawData, string columnName)
        {
            var colValues = CsvRawData.AsEnumerable().Select(r => r.Field<string>(columnName)).ToList();
            var duplicateColValues = colValues.GroupBy(x => x)
              .Where(y => y.Count() > 1)
              .Select(z => z.Key)
              .ToList();

            if (duplicateColValues.Count > 0)
            {
                foreach (var item in duplicateColValues)
                {
                    List<DataRow> rowsToDelete = new List<DataRow>();
                    foreach (DataRow dr in CsvRawData.Rows)
                    {
                        if (dr[columnName].ToString() == item)
                            rowsToDelete.Add(dr);
                    }
                    foreach (var dr in rowsToDelete)
                    {
                        CsvRawData.Rows.Remove(dr);
                    }
                }
                CsvRawData.AcceptChanges();
            }
            return CsvRawData;
        }

        /// <summary>
        /// It removes the rows that are missing value for the given column. 
        /// </summary>
        /// <param name="CsvRawData"></param>
        /// <param name="columnName"></param>
        /// <returns>Validated CSV data</returns>
        public DataTable ValidateMissingDataColumn(DataTable CsvRawData, string columnName)
        {
            List<DataRow> rowsToDelete = new List<DataRow>();
            foreach (DataRow dr in CsvRawData.Rows)
            {
                if (string.IsNullOrEmpty(dr[columnName].ToString()))
                    rowsToDelete.Add(dr);
            }
            foreach (var dr in rowsToDelete)
            {
                CsvRawData.Rows.Remove(dr);
            }
            CsvRawData.AcceptChanges();
            return CsvRawData;
        }

        /// <summary>
        /// It adds default value in the given column that is missing value.
        /// </summary>
        /// <param name="CsvRawData"></param>
        /// <param name="columnName"></param>
        /// <param name="defaultValue"></param>
        /// <returns>Validated CSV data</returns>
        public DataTable ValidateDefaultValueDataColumn(DataTable CsvRawData, string columnName, string defaultValue)
        {
            foreach (DataRow dr in CsvRawData.Rows)
            {
                if (string.IsNullOrEmpty(dr[columnName].ToString()))
                    dr[columnName] = defaultValue;
            }
            CsvRawData.AcceptChanges();
            return CsvRawData;
        }
    }
}
