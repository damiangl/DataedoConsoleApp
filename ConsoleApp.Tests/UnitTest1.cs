using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using ConsoleApp.NewImplementation;
using ConsoleApp.NewImplementation.Interfaces;

namespace ConsoleApp.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ValidRead1()
        {
            // arrange
            IEnumerable<ImportedObject> expected = new ImportedObject[]
            {
                new ImportedObject() { Type = "TABLE", Name = "SalesTaxRate", Schema = "Sales", ParentName = "AdventureWorks2016_EXT", ParentType = "Database", DataType = "NULL", IsNullable = "", NumberOfChildren = 7 },
                new ImportedObject() { Type = "TABLE", Name = "PersonCreditCard", Schema = "Sales", ParentName = "AdventureWorks2016_EXT", ParentType = "Database", DataType = "NULL", IsNullable = "", NumberOfChildren = 3 },
                new ImportedObject() { Type = "TABLE", Name = "PersonPhone", Schema = "Person", ParentName = "AdventureWorks2016_EXT", ParentType = "Database", DataType = "NULL", IsNullable = "",NumberOfChildren = 4 },
                new ImportedObject() { Type = "TABLE", Name = "seriespost", Schema = "dbo", ParentName = "BaseballData", ParentType = "Database", DataType = "NULL", IsNullable = "", NumberOfChildren = 9 },
                new ImportedObject() { Type = "COLUMN", Name = "ModifiedDate", Schema = "Sales", ParentName = "SalesTaxRate", ParentType = "TABLE", DataType = "datetime", IsNullable = "0" },
                new ImportedObject() { Type = "COLUMN", Name = "Name", Schema = "Sales", ParentName = "SalesTaxRate", ParentType = "TABLE", DataType = "Name: nvarchar", IsNullable = "0" },
                new ImportedObject() { Type = "COLUMN", Name = "rowguid", Schema = "Sales", ParentName = "SalesTaxRate", ParentType = "table", DataType = "uniqueidentifier", IsNullable = "0" },
                new ImportedObject() { Type = "COLUMN", Name = "SalesTaxRateID", Schema = "Sales", ParentName = "SalesTaxRate", ParentType = "TABLE", DataType = "int", IsNullable = "0" },
                new ImportedObject() { Type = "COLUMN", Name = "StateProvinceID", Schema = "Sales", ParentName = "SalesTaxRate", ParentType = "TABLE", DataType = "int", IsNullable = "0" },
                new ImportedObject() { Type = "COLUMN", Name = "TaxRate", Schema = "Sales", ParentName = "SalesTaxRate", ParentType = "TABLE", DataType = "smallmoney", IsNullable = "0" },
                new ImportedObject() { Type = "COLUMN", Name = "TaxType", Schema = "Sales", ParentName = "SalesTaxRate", ParentType = "table", DataType = "tinyint", IsNullable = "0" },
                new ImportedObject() { Type = "COLUMN", Name = "BusinessEntityID", Schema = "Sales", ParentName = "PersonCreditCard", ParentType = "TABLE", DataType = "int", IsNullable = "0" },
                new ImportedObject() { Type = "COLUMN", Name = "CreditCardID", Schema = "Sales", ParentName = "PersonCreditCard", ParentType = "TABLE", DataType = "int", IsNullable = "0" },
                new ImportedObject() { Type = "COLUMN", Name = "ModifiedDate", Schema = "Sales", ParentName = "PersonCreditCard", ParentType = "TABLE", DataType = "datetime", IsNullable = "0" },
                new ImportedObject() { Type = "COLUMN", Name = "BusinessEntityID", Schema = "Person", ParentName = "PersonPhone", ParentType = "TABLE", DataType = "int", IsNullable = "0" },
                new ImportedObject() { Type = "COLUMN", Name = "ModifiedDate", Schema = "Person", ParentName = "PersonPhone", ParentType = "TABLE", DataType = "datetime", IsNullable = "0" },
                new ImportedObject() { Type = "COLUMN", Name = "PhoneNumber", Schema = "Person", ParentName = "PersonPhone", ParentType = "TABLE", DataType = "Phone: nvarchar", IsNullable = "0" },
                new ImportedObject() { Type = "COLUMN", Name = "PhoneNumberTypeID", Schema = "Person", ParentName = "PersonPhone", ParentType = "TABLE", DataType = "int", IsNullable = "0" },
                new ImportedObject() { Type = "COLUMN", Name = "yearID", Schema = "dbo", ParentName = "seriespost", ParentType = "TABLE", DataType = "int", IsNullable = "0" },
                new ImportedObject() { Type = "COLUMN", Name = "round", Schema = "dbo", ParentName = "seriespost", ParentType = "TABLE", DataType = "varchar", IsNullable = "0" },
                new ImportedObject() { Type = "COLUMN", Name = "teamIDwinner", Schema = "dbo", ParentName = "seriespost", ParentType = "TABLE", DataType = "varchar", IsNullable = "1" },
                new ImportedObject() { Type = "COLUMN", Name = "lgIDwinner", Schema = "dbo", ParentName = "seriespost", ParentType = "TABLE", DataType = "varchar", IsNullable = "1" },
                new ImportedObject() { Type = "COLUMN", Name = "teamIDloser", Schema = "dbo", ParentName = "seriespost", ParentType = "TABLE", DataType = "varchar", IsNullable = "1" },
                new ImportedObject() { Type = "COLUMN", Name = "lgIDloser", Schema = "dbo", ParentName = "seriespost", ParentType = "TABLE", DataType = "varchar", IsNullable = "1" },
                new ImportedObject() { Type = "COLUMN", Name = "wins", Schema = "dbo", ParentName = "seriespost", ParentType = "TABLE", DataType = "int", IsNullable = "1" },
                new ImportedObject() { Type = "COLUMN", Name = "losses", Schema = "dbo", ParentName = "seriespost", ParentType = "TABLE", DataType = "int", IsNullable = "1" },
                new ImportedObject() { Type = "COLUMN", Name = "ties", Schema = "dbo", ParentName = "seriespost", ParentType = "TABLE", DataType = "int", IsNullable = "1" },
                new ImportedObject() { Type = "DATABASE", Name = "AdventureWorks2016_EXT", Schema = "", ParentName = "", ParentType = "", DataType = "", IsNullable = "", NumberOfChildren = 3 },
                new ImportedObject() { Type = "DATABASE", Name = "BaseballData", Schema = "", ParentName = "", ParentType = "", DataType = "", IsNullable = "", NumberOfChildren = 1 }
            };
            var expected_serialized = JsonConvert.SerializeObject(
                    expected
                        .OrderBy(o => o.Type)
                        .ThenBy(p => p.Name)
                        .ThenBy(r => r.Schema)
                        .ThenBy(s => s.ParentName)
                        .ThenBy(t => t.ParentType)
                        .ThenBy(u => u.DataType));

            // act
            IImportedObjectReader reader = new ImportedObjectReaderFromFile("TestFiles/test1.csv");
            var actual = reader.ImportedObjects;
            var actual_serialized = JsonConvert.SerializeObject(
                    actual
                        .OrderBy(o => o.Type)
                        .ThenBy(p => p.Name)
                        .ThenBy(r => r.Schema)
                        .ThenBy(s => s.ParentName)
                        .ThenBy(t => t.ParentType)
                        .ThenBy(u => u.DataType));

            // assert
            Assert.AreEqual<String>(actual_serialized, expected_serialized);
        }

        [TestMethod]
        public void ReadInvalid_Exception()
        {
            // arrange
            var expected = new Exception("Invalid number of columns at line 6 in file test2.csv");
            // act
            var actual = Assert.ThrowsException<Exception>(() => new ImportedObjectReaderFromFile("TestFiles/test2.csv"));
            // assert
            Assert.AreEqual(expected.GetType(), actual.GetType());
            Assert.AreEqual(expected.Message, actual.Message);
        }
    }
}
