using ConsoleApp.NewImplementation.Interfaces;
using System;
using System.Linq;

namespace ConsoleApp.NewImplementation
{
    public class ImportedObjectPublishToConsole : IImportedObjectPublish
    {
        private readonly IImportedObjectReader reader;

        public ImportedObjectPublishToConsole(IImportedObjectReader reader)
        {
            this.reader = reader;
        }

        public void Publish()
        {
            // print all databases
            foreach (var database in reader.ImportedObjects.Where(w => w.Type.ToLower().Equals("database")))
            {
                Console.WriteLine($"Database '{database.Name}' ({database.NumberOfChildren} tables)");
                // print all tables of database
                foreach (var table in reader.ImportedObjects.Where(w =>
                    w.ParentType.ToLower().Equals(database.Type.ToLower())
                    && w.ParentName.ToLower().Equals(database.Name.ToLower())))
                {
                    Console.WriteLine($"\tTable '{table.Schema}.{table.Name}' ({table.NumberOfChildren} columns)");
                    // print all columns of table
                    foreach (var column in reader.ImportedObjects.Where(w =>
                        w.ParentType.ToLower().Equals(table.Type.ToLower())
                        && w.ParentName.ToLower().Equals(table.Name.ToLower())))
                        Console.WriteLine($"\t\tColumn '{column.Name}' with {column.DataType} data type {(column.IsNullable == "1" ? "accepts nulls" : "with no nulls")}");
                }
            }
        }
    }
}
