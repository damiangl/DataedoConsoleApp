namespace ConsoleApp
{
    using ConsoleApp.NewImplementation;
    using ConsoleApp.NewImplementation.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class Program
    {
        static void Main(string[] args)
        {
            // old implementation
            //var reader = new DataReader();
            //reader.ImportAndPrintData("test1.csv");

            // new implementation
            IImportedObjectReader reader_new = new ImportedObjectReaderFromFile("data.csv");
            IImportedObjectPublish publisher = new ImportedObjectPublishToConsole(reader_new);
            publisher.Publish();

            Console.ReadLine();
        }
    }
}
