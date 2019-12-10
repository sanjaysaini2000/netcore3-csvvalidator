using System.Data;

namespace CsvValidator.DataLoader
{
    public interface IDataValidator
    {
        DataTable ValidateUniqueDataColumn(DataTable CsvRawData, string columnName);
        DataTable ValidateMissingDataColumn(DataTable CsvRawData, string columnName);
        DataTable ValidateDefaultValueDataColumn(DataTable CsvRawData, string columnName, string defaultValue);
    }

}