namespace Dbarone.Net.Document;

/// <summary>
/// Describes operations that can be performed on a buffer.
/// </summary>
public interface IBuffer
{
    /// <summary>
    /// Clears bytes in the buffer
    /// </summary>
    /// <param name="index"></param>
    /// <param name="length"></param>
    public void Clear(int index, int length);

    /// <summary>
    /// Fills the buffer with a byte.
    /// </summary>
    /// <param name="index"></param>
    /// <param name="length"></param>
    /// <param name="value"></param>
    public void Fill(int index, int length, byte value);

    /// <summary>
    /// Returns a byte array representation of the buffer.
    /// </summary>
    /// <returns></returns>
    public byte[] ToArray();

    /// <summary>
    /// Returns a slice of the byte array.
    /// </summary>
    /// <param name="index">The start of the byte array to return.</param>
    /// <param name="length">The length of the byte array to return.</param>
    /// <returns></returns>
    public byte[] Slice(int index, int length);

    /// <summary>
    /// Gets the size of the buffer.
    /// </summary>
    public long Length { get; }

    #region Read methods

    public bool ReadBool();
    public Byte ReadByte();
    public SByte ReadSByte();
    public char ReadChar();
    public VarInt ReadVarInt();
    public Int16 ReadInt16();
    public UInt16 ReadUInt16();
    public Int32 ReadInt32();
    public UInt32 ReadUInt32();
    public Int64 ReadInt64();
    public UInt64 ReadUInt64();
    public Double ReadDouble();
    public Decimal ReadDecimal();
    public Single ReadSingle();
    public Guid ReadGuid();
    public byte[] ReadBytes(int length);
    public DateTime ReadDateTime();
    public string ReadString(int length, TextEncoding textEncoding = TextEncoding.UTF8);
    public object Read(DocumentType dataType, int? length = null, TextEncoding textEncoding = TextEncoding.UTF8);

    #endregion

    #region Write methods

    public void Write(bool value);
    public void Write(byte value);
    public void Write(sbyte value);
    public void Write(char value);
    public void Write(VarInt value);
    public void Write(Int16 value);
    public void Write(UInt16 value);
    public void Write(Int32 value);
    public void Write(UInt32 value);
    public void Write(Int64 value);
    public void Write(UInt64 value);
    public void Write(Double value);
    public void Write(Single value);
    public void Write(Decimal value);
    public void Write(Guid value);
    public void Write(byte[] value);
    public void Write(DateTime  value);
    public void Write(string value, TextEncoding textEncoding = TextEncoding.UTF8);
    public void Write(object value, TextEncoding textEncoding = TextEncoding.UTF8);

    #endregion

}