namespace ConsoleApp.NewImplementation
{
    public enum ImportedObjectColumn
    {
        Name,
        Type,
        Schema,
        ParentName,
        ParentType,
        DataType,
        IsNullable,
        NumberOfChildren
    }

    public class ImportedObject : ImportedObjectBaseClass
    {
        public string Schema { get; set; }
        public string ParentName { get; set; }
        public string ParentType { get; set; }
        public string DataType { get; set; }
        public string IsNullable { get; set; }
        public double NumberOfChildren { get; set; }
    }

    public class ImportedObjectBaseClass
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
