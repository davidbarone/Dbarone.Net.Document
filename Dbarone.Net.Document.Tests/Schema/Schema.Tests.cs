using Dbarone.Net.Document;

public class SchemaTests {

    [Theory]
    [InlineData(DocumentType.Int32, (int)123)]
    public void TestValidation(DocumentType schemaType, object value) {
        DocumentValue document = new DocumentValue(value);
        SchemaElement schema = new SchemaElement(schemaType, false);

        Assert.True(schema.Validate(document));
    }
}