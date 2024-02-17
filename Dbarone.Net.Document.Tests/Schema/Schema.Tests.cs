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

    [Fact]
    public void TestSchemaSerialization() {
        SchemaElement schema1 = new SchemaElement(DocumentType.Document, false, null, new List<SchemaAttribute>{
            new SchemaAttribute(1, "a", new SchemaElement(DocumentType.String, false)),
            new SchemaAttribute(2, "b", new SchemaElement(DocumentType.DateTime, false)),
            new SchemaAttribute(3, "c", new SchemaElement(DocumentType.Int32, false))
        });

        // Serialize
        var dd1 = schema1.ToDictionaryDocument();
        DocumentSerializer ser = new DocumentSerializer();
        var bytes1 = ser.Serialize(dd1);

        // Deserialize
        var dd2 = ser.Deserialize(bytes1).AsDocument;
        var bytes2 = ser.Serialize(dd2);
        
        var schema2 = new SchemaElement(dd2);

        Assert.Equal(DocumentType.Document, schema2.DocumentType);
        Assert.Equal(3, schema2.Attributes!.Count());
        Assert.Equal("c", schema2.Attributes!.First(a => a.AttributeId == 3).AttributeName);
    }

   [Fact]
    public void TestToDictionaryDocument() {
        SchemaElement schema1 = new SchemaElement(DocumentType.Document, false, null, new List<SchemaAttribute>{
            new SchemaAttribute(1, "a", new SchemaElement(DocumentType.String, false)),
            new SchemaAttribute(2, "b", new SchemaElement(DocumentType.DateTime, false)),
            new SchemaAttribute(3, "c", new SchemaElement(DocumentType.Int32, false))
        });

        var dd1 = schema1.ToDictionaryDocument();
        var schema2 = new SchemaElement(dd1);

        Assert.Equal(DocumentType.Document, schema2.DocumentType);
        Assert.Equal(3, schema2.Attributes!.Count());
        Assert.Equal("c", schema2.Attributes!.First(a => a.AttributeId == 3).AttributeName);
    }

}