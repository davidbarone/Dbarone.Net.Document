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
///
/// Fixed Width DocTypes
/// 
/// 0               NULL value
/// 1               Boolean
/// 2               Byte
/// 3               SByte
/// 4               Char
/// 5               Decimal
/// 6               Double
/// 7               Single

/// 8               Int16
/// 9              UInt16
/// 10              Int32
/// 11              UInt32
/// 12              Int64
/// 13              UInt64
/// 14              DateTime
/// 15              Guid
/// 
/// Variable Length data types
/// 
/// N>=20, N%4==0   Array. Value is a byte array that is (N-20)/5 bytes long
/// N>=20, N%4==1   Blob. Value is a byte array that is (N-21)/5 bytes long.
/// N>=20, N%4==2   String. Value is a string that is (N-22)/5 bytes long, stored in the text encoding of the database.
/// N>=20, N%4==3   Document. value is a document that is (N-23)/5 bytes long
/// N>=20, N%4==4   VarInt. value is (N-24)/5 bytes long
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
        else if (value.Value % 5 == 0)
        {
            // array
            this.DocType = DocType.Array;
            this.Length = ((int)value.Value - ((int)value.Value % 5) - VariableStart) / 5;
        }
        else if (value.Value % 5 == 1)
        {
            // blob
            this.DocType = DocType.Blob;
            this.Length = ((int)value.Value - ((int)value.Value % 5) - VariableStart) / 5;
        }
        else if (value.Value % 5 == 2)
        {
            // string
            this.DocType = DocType.String;
            this.Length = ((int)value.Value - ((int)value.Value % 5) - VariableStart) / 5;
        }
        else if (value.Value % 5 == 3)
        {
            // document
            this.DocType = DocType.Document;
            this.Length = ((int)value.Value - ((int)value.Value % 5) - VariableStart) / 5;
        }
        else if (value.Value % 5 == 4)
        {
            // VarInt
            this.DocType = DocType.VarInt;
            this.Length = ((int)value.Value - ((int)value.Value % 5) - VariableStart) / 5;
        }
    }

    public SerialType(DocType docType, int? length = null)
    {
        this.DocType = docType;
        this.Length = length;

        if (docType == DocType.Array)
        {
            if (length == null)
            {
                throw new Exception("Length must be set.");
            }
            this.Value = VariableStart + (length.Value * 5) + 0;
        }
        else if (docType == DocType.Blob)
        {
            if (length == null)
            {
                throw new Exception("Length must be set.");
            }
            this.Value = VariableStart + (length.Value * 5) + 1;
        }
        else if (docType == DocType.String)
        {
            if (length == null)
            {
                throw new Exception("Length must be set.");
            }
            this.Value = VariableStart + (length.Value * 5) + 2;
        }
        else if (docType == DocType.Document)
        {
            if (length == null)
            {
                throw new Exception("Length must be set.");
            }
            this.Value = VariableStart + (length.Value * 5) + 3;
        }
        else if (docType == DocType.VarInt)
        {
            if (length == null)
            {
                throw new Exception("Length must be set.");
            }
            this.Value = VariableStart + (length.Value * 5) + 4;
        }
        else if ((int)docType < VariableStart)
        {
            this.Value = (int)docType;
        }
        else
        {
            throw new Exception("Should not get here!");
        }
    }
}