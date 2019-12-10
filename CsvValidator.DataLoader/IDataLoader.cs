using System;
namespace CsvValidator.DataLoader
{
    /// <summary>
    /// This interface declares CSV data and CSV validation data loading functionality
    /// </summary>
    public interface IDataLoader
    {
        /// <summary>
        /// Load data from csv file into DataTable object
        /// </summary>
        void LoadCsvData();

        //void LoadColumnHeader();
        /// <summary>
        /// Load csv validations from the xml file
        /// </summary>
        void LoadCsvValidation();
    }
}