using Dbarone.Net.Document;
using Microsoft.VisualBasic;

public class DocumentSerializer : IDocumentSerializer
{
    public DocumentSerializer()
    {
    }

    private byte[] EncodeString(string value, TextEncoding textEncoding = TextEncoding.UTF8)
    {
        var buf = new GenericBuffer();
        // save string only - to get length in bytes
        var len = buf.Write(value);
        buf.Position = 0;
        var bytes = buf.ReadBytes(len);
        return bytes;
    }

    public DocumentValue Deserialize(GenericBuffer buf, TextEncoding textEncoding = TextEncoding.UTF8)
    {
        var varInt = buf.ReadVarInt();
        var serialType = new SerialType(varInt);
        switch (serialType.DocumentType)
        {
            case DocumentType.Null:
                return DocumentValue.Null;
            case DocumentType.Boolean:
                var boolValue = buf.ReadBool();
                return new DocumentValue(boolValue);
            case DocumentType.Byte:
                var byteValue = buf.ReadByte();
                return new DocumentValue(byteValue);
            case DocumentType.SByte:
                var sByteValue = buf.ReadByte();
                return new DocumentValue(sByteValue);
            case DocumentType.Char:
                var charValue = buf.ReadChar();
                return new DocumentValue(charValue);
            case DocumentType.DateTime:
                var dateTimeValue = buf.ReadDateTime();
                return new DocumentValue(dateTimeValue);
            case DocumentType.Decimal:
                var decimalValue = buf.ReadDecimal();
                return new DocumentValue(decimalValue);
            case DocumentType.Double:
                var doubleValue = buf.ReadDouble();
                return new DocumentValue(doubleValue);
            case DocumentType.Guid:
                var guidValue = buf.ReadGuid();
                return new DocumentValue(guidValue);
            case DocumentType.Int16:
                var int16Value = buf.ReadInt16();
                return new DocumentValue(int16Value);
            case DocumentType.Int32:
                var int32Value = buf.ReadInt32();
                return new DocumentValue(int32Value);
            case DocumentType.Int64:
                var int64Value = buf.ReadInt64();
                return new DocumentValue(int64Value);
            case DocumentType.UInt16:
                var uint16Value = buf.ReadUInt16();
                return new DocumentValue(uint16Value);
            case DocumentType.UInt32:
                var uint32Value = buf.ReadUInt32();
                return new DocumentValue(uint32Value);
            case DocumentType.UInt64:
                var uint64Value = buf.ReadUInt64();
                return new DocumentValue(uint64Value);
            case DocumentType.Single:
                var singleValue = buf.ReadSingle();
                return new DocumentValue(singleValue);
            case DocumentType.String:
                var stringValue = buf.ReadString(serialType.Length!.Value);
                return new DocumentValue(stringValue);
            case DocumentType.Array:
                List<DocumentValue> elements = new List<DocumentValue>();
                for (int i = 0; i < serialType.Length; i++)
                {
                    var docValue = this.Deserialize(buf, textEncoding);
                    elements.Add(docValue);
                }
                return new DocumentArray(elements);
            case DocumentType.Document:
                DictionaryDocument dict = new DictionaryDocument();
                for (int i = 0; i < serialType.Length; i++) {
                    // key
                    string key = this.Deserialize(buf, textEncoding);
                    // value
                    var value = this.Deserialize(buf, textEncoding);
                    dict[key] = value;
                }
                return dict;
            default:
                throw new NotImplementedException();
        }
    }

    public DocumentValue Deserialize(byte[] buffer, TextEncoding textEncoding = TextEncoding.UTF8)
    {
        var buf = new GenericBuffer(buffer);
        return this.Deserialize(buf, textEncoding);
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
                var bytes = EncodeString(docValue.AsString, textEncoding);
                serialType = new SerialType(DocumentType.String, bytes.Length);
                buf.Write(serialType.Value);
                buf.Write(bytes);
                break;
            case DocumentType.Array:
                var docArray = docValue as DocumentArray;
                if (docArray is null)
                {
                    throw new Exception("DocumentArray type expected!");
                }
                serialType = new SerialType(DocumentType.Array, docArray.Count);
                buf.Write(serialType.Value);
                foreach (var docElement in docArray)
                {
                    DocumentSerializer ser = new DocumentSerializer();
                    var elementBytes = ser.Serialize(docElement, textEncoding);
                    buf.Write(elementBytes);
                }
                break;
            case DocumentType.Document:
                var dictDoc = docValue as DictionaryDocument;
                if (dictDoc is null)
                {
                    throw new Exception("DictionaryDocument type expected!");
                }
                serialType = new SerialType(DocumentType.Document, dictDoc.Keys.Count);
                buf.Write(serialType.Value);
                foreach (var key in dictDoc.Keys)
                {
                    // key
                    var keyBytes = EncodeString(key, textEncoding);
                    SerialType serialTypeKey = new SerialType(DocumentType.String, keyBytes.Length);
                    buf.Write(serialTypeKey.Value);
                    buf.Write(keyBytes);

                    // value
                    DocumentSerializer ser = new DocumentSerializer();
                    var valueBytes = ser.Serialize(dictDoc[key], textEncoding);
                    buf.Write(valueBytes);
                }
                break;
            case DocumentType.Blob:
            case DocumentType.VarInt:
                serialType = new SerialType(DocumentType.VarInt);
                buf.Write(serialType.Value);
                buf.Write(docValue.AsVarInt);
                break;
            default:
                throw new NotImplementedException();
        }

        return buf.ToArray();
    }
}