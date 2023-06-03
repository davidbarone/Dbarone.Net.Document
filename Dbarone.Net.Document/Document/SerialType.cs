using Dbarone.Net.Document;

/// <summary>
/// Encodes the data type and size of a value.
/// 
/// When serialising all keys and values in a document, the payload is
/// prefixed with a SerialType value, which encodes the value's data type
/// and length.
/// 
/// The serial type is stored in the database as a VarInt. The values are as follows:
/// 
/// Serial Type     Data Type
/// ===========     =========
/// 0               NULL value
/// 1               Boolean
/// 2               Byte
/// 3               SByte
/// 4               Char
/// 5               Decimal
/// 6               Double
/// 7               Single
/// 8               VarInt
/// 9               Int16
/// 10              UInt16
/// 11              Int32
/// 12              UInt32
/// 13              Int64
/// 14              UInt64
/// 15              DateTime
/// 16              Guid
/// 
/// Variable Length data types
/// N>=20, N%4==0   Array
/// N>=20, N%4==1   Blob. Value is a byte array that is (N-18)/3 bytes long.
/// N>=20, N%4==2   String. Value is a string that is (N-19)/3 bytes long, stored in the text encoding of the database.
/// N>=20, N%4==3   Document. value is a document that is (N-20)/3 bytes long
/// </summary>
public class SerialType
{
    /// <summary>
    /// The DocType of the value.
    /// </summary>
    public DocType DocType { get; set; }

    /// <summary>
    /// Byte length of the data if string or blob.
    /// </summary>
    public int? Length { get; set; }

    public VarInt Value { get; init; }

    private const int VariableStart = 20;

    public SerialType(VarInt value)
    {
        if (value.Value < VariableStart)
        {
            // Fixed-width type
            this.DocType = (DocType)value.Value;
            this.Length = null;
        }
        else if (value.Value % 4 == 0)
        {
            // array
            this.DocType = DocType.Array;
            this.Length = (((int)value.Value % VariableStart) - VariableStart) / 4;
        }
        else if (value.Value % 4 == 1)
        {
            // blob
            this.DocType = DocType.Blob;
            this.Length = (((int)value.Value % VariableStart) - VariableStart) / 4;
        }
        else if (value.Value % 4 == 2)
        {
            // string
            this.DocType = DocType.String;
            this.Length = (((int)value.Value % VariableStart) - VariableStart) / 4;
        }
        else if (value.Value % 4 == 3)
        {
            // document
            this.DocType = DocType.Document;
            this.Length = (((int)value.Value % VariableStart) - VariableStart) / 4;
        }
    }

    public SerialType(DataType dataType, int? length = null)
    {
        this.DataType = dataType;
        this.Length = length;

        int variableTypeStart = (int)DataType.Blob;
        int evenStart = variableTypeStart % 2 == 1 ? variableTypeStart + 1 : variableTypeStart;
        int oddStart = evenStart + 1;

        if (dataType == DataType.Blob)
        {
            if (length == null)
            {
                throw new Exception("Length must be set.");
            }
            // Serial type for byte[] values is (N-variableTypeStart)/2 and even. 
            this.Value = new VarInt((length.Value * 2) + evenStart);
        }
        else if (dataType == DataType.String)
        {
            if (length == null)
            {
                throw new Exception("Length must be set.");
            }
            // Serial type for string values is (N-variableTypeStart)/2 and odd. 
            this.Value = new VarInt((length.Value * 2) + oddStart);
        }
        else
        {
            // For other types, return a VarInt of the DataType enum value.
            this.Value = new VarInt((int)dataType);
        }
    }
}