namespace CsvValidator.Data
{
    /// <summary>
    /// Stores validation data for column from the validation xml.
    /// </summary>
    public class CvsValidation
    {
        public string Name { get; set; }
        public string IsUnique { get; set; }
        public string IsNull { get; set; }
        public string DefaultValue { get; set; }
    }
}