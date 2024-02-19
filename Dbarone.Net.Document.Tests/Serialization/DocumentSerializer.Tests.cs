using Dbarone.Net.Document;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;
using Xunit;

namespace Dbarone.Net.Document.Tests;

public class DocumentSerializerTests
{

    [Fact]
    public void SerializeInt16DocumentValue()
    {
        IDocumentSerializer ser = new DocumentSerializer();
        DocumentValue doc1 = (int)123;

        // Serialize
        var bytes = ser.Serialize(doc1);

        // Deserialize
        var doc2 = ser.Deserialize(bytes);

        // Assert doc1 == doc2
        Assert.Equal(doc1, doc2);
    }

    [Fact]
    public void SerializeDocumentArraySimple()
    {
        var element1 = new DocumentValue(1);
        var element2 = new DocumentValue(2);
        var element3 = new DocumentValue(3);
        var array1 = new DocumentArray(element1, element2, element3);
        IDocumentSerializer ser = new DocumentSerializer();

        // Serialize
        var bytes = ser.Serialize(array1);

        // Deserialize
        var array2 = ser.Deserialize(bytes);

        // Assert doc1 == doc2
        Assert.Equal(array1, array2);
        Assert.IsType<DocumentArray>(array2);
        Assert.Equal(3, (array2 as DocumentArray)!.Count);
    }

    [Fact]
    public void SerializeDictionaryDocument()
    {
        DictionaryDocument dd1 = new DictionaryDocument();
        dd1["Name"] = new DocumentValue("FooBar");
        dd1["Age"] = new DocumentValue(123);
        dd1["DoB"] = new DocumentValue(DateTime.Now);

        IDocumentSerializer ser = new DocumentSerializer();

        // Serialize
        var bytes = ser.Serialize(dd1);

        // Deserialize
        var dd2 = ser.Deserialize(bytes);

        Assert.Equal(dd1, dd2);
        Assert.IsType<DictionaryDocument>(dd2);
        Assert.Equal(3, (dd2 as DictionaryDocument)!.Count);
    }

    [Fact]
    public void SerializeDictionaryDocumentWithSchema()
    {
        SchemaElement schema = new SchemaElement(DocumentType.Document, false, null, new List<SchemaAttribute>(){
            new SchemaAttribute(1, "Name", new SchemaElement(DocumentType.String, false, null, null)),
            new SchemaAttribute(2, "Age", new SchemaElement(DocumentType.Int32, false, null, null)),
            new SchemaAttribute(3, "DoB", new SchemaElement(DocumentType.DateTime, false, null, null)),
        });

        DictionaryDocument dd1 = new DictionaryDocument();
        dd1["Name"] = new DocumentValue("FooBar");
        dd1["Age"] = new DocumentValue((Int32)123);
        dd1["DoB"] = new DocumentValue(DateTime.Now);

        IDocumentSerializer ser = new DocumentSerializer();

        // Serialize
        var bytes = ser.Serialize(dd1, schema);

        // Deserialize
        var dd2 = ser.Deserialize(bytes);

        Assert.Equal(dd1, dd2);
        Assert.IsType<DictionaryDocument>(dd2);
        Assert.Equal(3, (dd2 as DictionaryDocument)!.Count);
    }

    [Fact]
    public void SerializeDocumentArray()
    {
        DictionaryDocument dd1 = new DictionaryDocument();
        dd1["Name"] = new DocumentValue("FooBar");
        dd1["Age"] = new DocumentValue(123);
        dd1["DoB"] = new DocumentValue(DateTime.Now);

        DocumentArray da1 = new DocumentArray();
        for (int i = 0; i < 100; i++)
        {
            da1.Add(dd1);
        }

        IDocumentSerializer ser = new DocumentSerializer();

        // Serialize
        var bytes = ser.Serialize(da1);

        // Deserialize
        var da2 = (DocumentArray)ser.Deserialize(bytes);

        Assert.Equal(da1.Count, da2.Count);
        Assert.IsType<DocumentArray>(da2);
    }

    [Fact]
    public void SerializeDocumentArrayWithSchema()
    {
        SchemaElement schema = new SchemaElement(DocumentType.Array, false,
         new SchemaElement(DocumentType.Document, false, null, new List<SchemaAttribute>(){
            new SchemaAttribute(1, "Name", new SchemaElement(DocumentType.String, false, null, null)),
            new SchemaAttribute(2, "Age", new SchemaElement(DocumentType.Int32, false, null, null)),
            new SchemaAttribute(3, "DoB", new SchemaElement(DocumentType.DateTime, false, null, null)),
        }), null);

        DictionaryDocument dd1 = new DictionaryDocument();
        dd1["Name"] = new DocumentValue("FooBar");
        dd1["Age"] = new DocumentValue((Int32)123);
        dd1["DoB"] = new DocumentValue(DateTime.Now);

        DocumentArray da1 = new DocumentArray();
        for (int i = 0; i < 100; i++)
        {
            da1.Add(dd1);
        }

        IDocumentSerializer ser = new DocumentSerializer();

        // Serialize
        var bytes = ser.Serialize(da1, schema);

        // Deserialize
        var da2 = (DocumentArray)ser.Deserialize(bytes);

        Assert.Equal(da1.Count, da2.Count);
        Assert.IsType<DocumentArray>(da2);

    }
}