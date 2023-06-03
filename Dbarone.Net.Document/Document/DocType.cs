namespace Dbarone.Net.Document;

/// <summary>
/// Represents data types allowed in Dbarone.Net.Document.
/// </summary>
public enum DocType : int
{
    /// <summary>
    /// Null value object.
    /// </summary>
    Null = 0,

    /// <summary>
    /// A boolean value.
    /// </summary>
    Boolean = 1,

    /// <summary>
    /// A single-byte value (0 to 255).
    /// </summary>
    Byte,

    /// <summary>
    /// A signed single byte value (-128 to 127).
    /// </summary>
    SByte,

    /// <summary>
    /// A Unicode UTF-16 character.
    /// </summary>
    Char,

    /// <summary>
    /// A 16-byte floating point numeric type.
    /// </summary>
    Decimal,

    /// <summary>
    /// An 8-byte floating point numeric type.
    /// </summary>
    Double,

    /// <summary>
    /// A 4-byte floating point numeric type.
    /// </summary>
    Single,

    /// <summary>
    /// A signed 16-bit integer.
    /// </summary>
    Int16,

    /// <summary>
    /// An unsigned 16-bit integer.
    /// </summary>
    UInt16,

    /// <summary>
    /// A signed 32-bit integer.
    /// </summary>
    Int32,

    /// <summary>
    /// An unsigned 32-bit integer.
    /// </summary>
    UInt32,

    /// <summary>
    /// A signed 64-bit integer.
    /// </summary>
    Int64,

    /// <summary>
    /// An unsigned 64-bit integer.
    /// </summary>
    UInt64,

    /// <summary>
    /// A date/time structure.
    /// </summary>
    DateTime,

    /// <summary>
    /// Represents a globally unique identifier (GUID).
    /// </summary>
    Guid,

    /// <summary>
    /// An array or collection of values.
    /// </summary>
    Array = 20,

    /// <summary>
    /// A variable-length byte-array.
    /// </summary>
    Blob,

    /// <summary>
    /// A variable-length string.
    /// </summary>
    String,

    /// <summary>
    /// A document with key/value pairs.
    /// </summary>
    Document,

    /// <summary>
    /// A variable-length integer.
    /// </summary>
    VarInt,

}