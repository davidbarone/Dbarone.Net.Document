using Dbarone.Net.Document;

public class SchemaTests
{

    [Theory]
    [InlineData(DocumentType.Int32, false, (int)123)]
    [InlineData(DocumentType.Int32, true, null)]
    public void TestValidationSuccess(DocumentType schemaType, bool allowNull, object? value)
    {
        DocumentValue document = new DocumentValue(value);
        SchemaElement schema = new SchemaElement(schemaType, allowNull);
        Assert.True(schema.Validate(document));
    }

    [Theory]
    [InlineData(DocumentType.Int32, false, null)]
    [InlineData(DocumentType.Int32, false, "foobar")]
    public void TestValidationFailure(DocumentType schemaType, bool allowNull, object? value)
    {
        DocumentValue document = new DocumentValue(value);
        SchemaElement schema = new SchemaElement(schemaType, allowNull);
        Assert.Throws<Exception>(() => schema.Validate(document));
    }

    [Fact]
    public void TestDocumentValidationSuccess()
    {
        SchemaElement schema = new SchemaElement(DocumentType.Document, false, null, new List<SchemaAttribute>{
            new SchemaAttribute(1, "a", new SchemaElement(DocumentType.String, false)),
            new SchemaAttribute(2, "b", new SchemaElement(DocumentType.DateTime, false)),
            new SchemaAttribute(3, "c", new SchemaElement(DocumentType.Int32, false))
        });

        DictionaryDocument dd = new DictionaryDocument();
        dd["a"] = new DocumentValue("foobar");
        dd["b"] = new DocumentValue(DateTime.Now);
        dd["c"] = new DocumentValue((int)123);

        Assert.True(schema.Validate(dd));
    }

    [Fact]
    public void TestDocumentValidationFailure()
    {
        SchemaElement schema = new SchemaElement(DocumentType.Document, false, null, new List<SchemaAttribute>{
            new SchemaAttribute(1, "a", new SchemaElement(DocumentType.String, false)),
            new SchemaAttribute(2, "b", new SchemaElement(DocumentType.DateTime, false)),
            new SchemaAttribute(3, "c", new SchemaElement(DocumentType.Int32, false))
        });

        DictionaryDocument dd = new DictionaryDocument();
        dd["a"] = new DocumentValue("foobar");
        dd["b"] = new DocumentValue(DateTime.Now);
        dd["c"] = new DocumentValue((int)123);
        // This column is not in the schema
        dd["d"] = new DocumentValue((int)123);

        Assert.Throws<Exception>(() => schema.Validate(dd));
    }

}