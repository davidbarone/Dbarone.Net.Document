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
}