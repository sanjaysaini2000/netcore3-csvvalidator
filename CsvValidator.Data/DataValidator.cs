using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CsvValidator.Data
{
    public class DataValidator : IDataValidator
    {

        public DataTable ValidateUniqueDataColumn(DataTable CsvRawData, string columnName)
        {
            //todo
            return new DataTable();

        }

        public DataTable ValidateMissingDataColumn(DataTable CsvRawData, string columnName)
        {
            //todo
            return new DataTable();
        }

        public DataTable ValidateDefaultValueDataColumn(DataTable CsvRawData, string columnName, string defaultValue)
        {
            //todo
            return new DataTable();
        }
    }
}
