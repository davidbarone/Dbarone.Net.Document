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
                buf.Write(docValue.AsChar);
                break;
            case DocumentType.DateTime:
                serialType = new SerialType(DocumentType.DateTime);
                buf.Write(serialType.Value);
                buf.Write(docValue.AsDateTime);
                break;
            case DocumentType.Decimal:
                serialType = new SerialType(DocumentType.Decimal);
                buf.Write(serialType.Value);
                buf.Write(docValue.AsDecimal);
                break;
            case DocumentType.Double:
                serialType = new SerialType(DocumentType.Double);
                buf.Write(serialType.Value);
                buf.Write(docValue.AsDouble);
                break;
            case DocumentType.Guid:
                serialType = new SerialType(DocumentType.Guid);
                buf.Write(serialType.Value);
                buf.Write(docValue.AsGuid);
                break;
            case DocumentType.Int16:
                serialType = new SerialType(DocumentType.Int16);
                buf.Write(serialType.Value);
                buf.Write(docValue.AsInt16);
                break;
            case DocumentType.Int32:
                serialType = new SerialType(DocumentType.Int32);
                buf.Write(serialType.Value);
                buf.Write(docValue.AsInt32);
                break;
            case DocumentType.Int64:
                serialType = new SerialType(DocumentType.Int64);
                buf.Write(serialType.Value);
                buf.Write(docValue.AsInt64);
                break;
            case DocumentType.UInt16:
                serialType = new SerialType(DocumentType.UInt16);
                buf.Write(serialType.Value);
                buf.Write(docValue.AsUInt16);
                break;
            case DocumentType.UInt32:
                serialType = new SerialType(DocumentType.UInt32);
                buf.Write(serialType.Value);
                buf.Write(docValue.AsUInt32);
                break;
            case DocumentType.UInt64:
                serialType = new SerialType(DocumentType.UInt64);
                buf.Write(serialType.Value);
                buf.Write(docValue.AsUInt64);
                break;
            case DocumentType.Single:
                serialType = new SerialType(DocumentType.Single);
                buf.Write(serialType.Value);
                buf.Write(docValue.AsSingle);
                break;
            case DocumentType.String:
                // save string only - to get length in bytes
                var len = buf.Write(docValue.AsString);
                buf.Position = 0;
                var bytes = buf.ReadBytes(len);
                serialType = new SerialType(DocumentType.String, len);
                buf.Write(bytes);
                break;
            case DocumentType.VarInt:
                serialType = new SerialType(DocumentType.VarInt);
                buf.Write(serialType.Value);
                buf.Write(docValue.);
                break;
            default:
                break;




        }
        throw new NotImplementedException();
    }
}