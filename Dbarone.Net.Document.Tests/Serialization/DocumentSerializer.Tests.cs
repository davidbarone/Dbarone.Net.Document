using Dbarone.Net.Document;
using Xunit;

namespace Dbarone.Net.Document.Tests;

public class DocumentSerializerTests {

    [Fact]
    public void SerializeInt16DocumentValue() {
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
    public void SerializeDocumentArray() {
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
}