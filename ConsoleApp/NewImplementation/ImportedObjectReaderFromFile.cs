using ConsoleApp.NewImplementation.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp.NewImplementation
{
    public class ImportedObjectReaderFromFile : IImportedObjectReader
    {
        private List<ImportedObject> importedObjects = new List<ImportedObject>();
        public IEnumerable<ImportedObject> ImportedObjects { get { return importedObjects.ToArray(); } }

        private String ColumnValueFormat(ImportedObjectColumn column, String value)
        {
            String ret = value;
            switch (column)
            {
                case ImportedObjectColumn.Type:
                    ret = ret.Trim().Replace(" ", "").Replace(Environment.NewLine, "").Replace("Name: ", "").ToUpper();
                    break;
                case ImportedObjectColumn.Name:
                case ImportedObjectColumn.Schema:
                case ImportedObjectColumn.ParentName:
                case ImportedObjectColumn.ParentType:
                    ret = ret.Trim().Replace(" ", "").Replace(Environment.NewLine, "");
                    break;
            }
            return ret;
        }

        private List<String> GetTypeDictionary()
        {
            return new List<String>() { "table", "database", "column" };
        }

        private List<String> GetParentTypeDictionary()
        {
            return new List<String>() { "", "table", "database", "column" };
        }

        private List<String> GetDataTypeDictionary()
        {
            return new List<String>() { "", "nvarchar", "int", "money", "uniqueidentifier", "xml", "date", "datetime", "decimal", "smallmoney", "bit" };
        }

        private bool ImportedObjectsValidation(ImportedObject obj)
        {
            return
                (this.GetTypeDictionary().Contains(obj.Type.ToLower()))
                && (this.GetParentTypeDictionary().Contains(obj.ParentType.ToLower()));
        }

        public ImportedObjectReaderFromFile(String fileToImport)
        {
            var streamReader = new StreamReader(fileToImport);
            var lineNo = 0;
            while (!streamReader.EndOfStream)
            {
                lineNo++;
                // read line
                var line = streamReader.ReadLine();
                if (lineNo.Equals(1))
                    continue;
                var values = line.Split(';');
                // skip empty lines
                if (values.Count().Equals(1))
                    continue;
                // throw exception if number of columns is not equals 7
                if (!values.Count().Equals(7))
                    throw new Exception($"Invalid number of columns at line {lineNo.ToString()} in file {Path.GetFileName(fileToImport)}");
                // add object with column formatted values
                var importedObject = new ImportedObject();
                importedObject.Type = this.ColumnValueFormat(ImportedObjectColumn.Type, values[0]);
                importedObject.Name = this.ColumnValueFormat(ImportedObjectColumn.Name, values[1]);
                importedObject.Schema = this.ColumnValueFormat(ImportedObjectColumn.Schema, values[2]);
                importedObject.ParentName = this.ColumnValueFormat(ImportedObjectColumn.ParentName, values[3]);
                importedObject.ParentType = this.ColumnValueFormat(ImportedObjectColumn.ParentType, values[4]);
                importedObject.DataType = this.ColumnValueFormat(ImportedObjectColumn.DataType, values[5]);
                importedObject.IsNullable = this.ColumnValueFormat(ImportedObjectColumn.IsNullable, values[6]);

                // validation
                if (!this.ImportedObjectsValidation(importedObject))
                    continue;

                importedObjects.Add(importedObject);
            }

            // assign number of children
            foreach (var item in importedObjects)
                item.NumberOfChildren = importedObjects
                    .Where(w =>
                        w.ParentName.ToLower().Equals(item.Name.ToLower())
                        && w.ParentType.ToLower().Equals(item.Type.ToLower())
                    ).Count();
        }
    }
}
