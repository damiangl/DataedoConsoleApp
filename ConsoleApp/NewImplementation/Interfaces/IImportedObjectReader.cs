using ConsoleApp.NewImplementation;
using System.Collections.Generic;

namespace ConsoleApp.NewImplementation.Interfaces
{
    public interface IImportedObjectReader
    {
        IEnumerable<ImportedObject> ImportedObjects { get; }
    }
}