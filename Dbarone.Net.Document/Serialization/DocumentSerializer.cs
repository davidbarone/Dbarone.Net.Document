using Dbarone.Net.Document;

public class DocumentSerializer : IDocumentSerializer
{
    public DocumentSerializer()
    {
    }

    public DocumentValue Deserialize(byte[] buffer, TextEncoding textEncoding = TextEncoding.UTF8)
    {
        throw new NotImplementedException();
    }

    public byte[] Serialize(DocumentValue docValue, TextEncoding textEncoding = TextEncoding.UTF8)
    {
        var buf = new GenericBuffer();
        switch (docValue.Type)
        {
            case DocumentType.Null:
                var serialType = new SerialType(DocumentType.Null);
                buf.Write(serialType.Value);
                break;
            case DocumentType.Boolean:
                serialType = new SerialType(DocumentType.Boolean);
                buf.Write(serialType.Value);
                buf.Write(docValue.AsBoolean);
                break;
            case DocumentType.Byte:
                serialType = new SerialType(DocumentType.Byte);
                buf.Write(serialType.Value);
                buf.Write(docValue.AsByte);
                break;
            case DocumentType.SByte:
                serialType = new SerialType(DocumentType.SByte);
                buf.Write(serialType.Value);
                buf.Write(docValue.AsSByte);
                break;
            case DocumentType.Char:
                serialType = new SerialType(DocumentType.Char);
                buf.Write(serialType.Value);
                buf.Write(docValue.AsByte);
                break;

            default:
                break;




        }
        throw new NotImplementedException();
    }
}