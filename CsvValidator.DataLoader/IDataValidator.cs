using System.Data;

namespace CsvValidator.DataLoader
{
    /// <summary>
    /// This interface declares validation functionality on CSV Data using CSV validation data.
    /// </summary>
    public interface IDataValidator
    {
        DataTable ValidateUniqueDataColumn(DataTable CsvRawData, string columnName);
        DataTable ValidateMissingDataColumn(DataTable CsvRawData, string columnName);
        DataTable ValidateDefaultValueDataColumn(DataTable CsvRawData, string columnName, string defaultValue);
    }

}