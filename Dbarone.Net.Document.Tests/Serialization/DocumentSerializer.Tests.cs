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
}