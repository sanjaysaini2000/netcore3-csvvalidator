using System;
namespace CsvValidator.Data
{
    /// <summary>
    /// This interface declares CSV data and CSV validation data loading functionality.
    /// </summary>
    public interface IDataLoader
    {
        void LoadCsvData();
        void LoadCsvValidation();
    }
}