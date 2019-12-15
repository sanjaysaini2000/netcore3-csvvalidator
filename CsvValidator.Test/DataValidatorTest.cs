using NUnit.Framework;
using System.Data;
using System;
using CsvValidator.Data;
using System.Linq;

namespace CsvValidator.Test
{
    public class DataValidatorTests
    {
        DataTable cvsData;

        [SetUp]
        public void Setup()
        {
            cvsData = new DataTable();
            PopulateTestData();
        }

        void PopulateTestData()
        {
            for (int i = 1; i < 5; i++)
            {
                string col = "Col" + i.ToString();
                cvsData.Columns.Add(col);

            }

            Random random = new Random();
            for (int i = 0; i < 5; i++)
            {
                DataRow dr = cvsData.NewRow();
                string colVal = string.Empty;
                for (int j = 0; j < 4; j++)
                {
                    if (j == 0)
                    {
                        colVal = "test-value";
                    }
                    else if (j == 1 || j == 2)
                    {
                        colVal = string.Empty;
                    }
                    else
                    {
                        int r = random.Next(100, 1000);
                        colVal = "value" + r.ToString();
                    }
                    dr[j] = colVal;
                }
                cvsData.Rows.Add(dr);
            }
        }

        [TearDown]
        public void TearDown()
        {
            cvsData = null;
        }

        [Test]
        public void Test_WhenDuplicateValueExistInTheColumn()
        {
            IDataValidator dv = new DataValidator();
            DataTable valaidatedData = dv.ValidateUniqueDataColumn(cvsData, "Col1");
            Assert.AreEqual(0, valaidatedData.Rows.Count);
        }

        [Test]
        public void Test_WhenDuplicateValueNotExistInTheColumn()
        {
            IDataValidator dv = new DataValidator();
            DataTable valaidatedData = dv.ValidateUniqueDataColumn(cvsData, "Col4");
            Assert.AreEqual(5, valaidatedData.Rows.Count);
        }

        [Test]
        public void Test_WhenEmptyValueExistInTheColumn()
        {
            IDataValidator dv = new DataValidator();
            DataTable valaidatedData = dv.ValidateMissingDataColumn(cvsData, "Col2");
            Assert.AreEqual(0, valaidatedData.Rows.Count);
        }

        [Test]
        public void Test_WhenEmptyValueNotExistInTheColumn()
        {
            IDataValidator dv = new DataValidator();
            DataTable valaidatedData = dv.ValidateUniqueDataColumn(cvsData, "Col4");
            Assert.AreEqual(5, valaidatedData.Rows.Count);
        }

        [Test]
        public void Test_WhenEmptyValueExistInTheColumnHasDefaultValue()
        {
            IDataValidator dv = new DataValidator();
            DataTable valaidatedData = dv.ValidateDefaultValueDataColumn(cvsData, "Col3", "default");
            var colValues = valaidatedData.AsEnumerable().Select(row => row.Field<string>("Col3")).ToList();
            Assert.AreEqual(5, (colValues.Where(c => string.Equals(c, "default")).Count()));
        }

        [Test]
        public void Test_WhenEmptyValueNotExistInTheColumnHasDefaultValue()
        {
            IDataValidator dv = new DataValidator();
            DataTable valaidatedData = dv.ValidateDefaultValueDataColumn(cvsData, "Col4", "default");
            var colValues = valaidatedData.AsEnumerable().Select(row => row.Field<string>("Col3")).ToList();
            Assert.AreEqual(0, (colValues.Where(c => string.Equals(c, "default")).Count()));
        }

    }
}