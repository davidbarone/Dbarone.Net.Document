namespace Dbarone.Net.Document;

using System.Text;

/// <summary>
/// Represents a generic memory buffer.
/// </summary>
public class GenericBuffer : IBuffer
{
    protected MemoryStream Stream;

    /// <summary>
    /// Returns true if the current buffer can grow.
    /// </summary>
    public bool Resizeable { get; private set; }

    /// <summary>
    /// Gets / sets the position in the buffer.
    /// </summary>
    public long Position
    {
        get { return this.Stream.Position; }
        set { this.Stream.Position = value; }
    }

    /// <summary>
    /// Returns the length of the underlying buffer in bytes.
    /// </summary>
    public long Length
    {
        get { return this.Stream.Length; }
    }

    /// <summary>
    /// Creates a non-resizeable buffer.
    /// </summary>
    /// <param name="buffer"></param>
    public GenericBuffer(byte[] buffer)
    {
        // MemoryStream with fixed capacity buffer
        this.Stream = new MemoryStream(buffer);
        this.Resizeable = false;
    }

    /// <summary>
    /// Creates an expandable buffer.
    /// </summary>
    public GenericBuffer()
    {
        this.Stream = new MemoryStream();
        this.Resizeable = true;
    }

    /// <summary>
    /// The internal byte array used for read and write operations. For resizeable buffers
    /// the buffer returned is the underlying MemoryStream buffer, and may return more
    /// bytes than actually populated. You will need to use the MemoryStream.Length property
    /// to get the actual size of the buffer.
    /// </summary>
    protected virtual byte[] InternalBuffer
    {
        get
        {
            return this.Stream.GetBuffer();
        }
    }

    public virtual byte this[int index]
    {
        get => this.InternalBuffer[index];
        set => this.InternalBuffer[index] = value;
    }

    public void Clear(int index, int length)
    {
        System.Array.Clear(InternalBuffer, index, length);
    }

    public void Fill(int index, int length, byte value)
    {
        for (var i = 0; i < length; i++)
        {
            InternalBuffer[index + i] = value;
        }
    }

    public virtual byte[] ToArray()
    {
        // copy existing buffer
        var buffer = new byte[InternalBuffer.Length];
        Buffer.BlockCopy(InternalBuffer, 0, buffer, 0, InternalBuffer.Length);
        return buffer;
    }

    public virtual byte[] Slice(int index, int length)
    {
        // copy existing buffer
        var buffer = new byte[length];
        Buffer.BlockCopy(InternalBuffer, index, buffer, 0, length);
        return buffer;
    }

    #region Read methods

    public bool ReadBool()
    {
        var index = (int)this.Stream.Position;
        return InternalBuffer[index] != 0;
    }

    public byte ReadByte()
    {
        var index = (int)this.Stream.Position;
        return InternalBuffer[index];
    }

    public sbyte ReadSByte()
    {
        var index = (int)this.Stream.Position;
        return (sbyte)InternalBuffer[index];
    }

    public char ReadChar()
    {
        var index = (int)this.Stream.Position;
        return BitConverter.ToChar(InternalBuffer, index);
    }

    public VarInt ReadVarInt()
    {
        var index = (int)this.Stream.Position;
        int i = 0;
        byte[] bytes = new byte[4];
        Byte b;
        do
        {
            b = InternalBuffer[index + i];
            bytes[i] = b;
            i++;
        } while ((b & 128) != 0);
        return new VarInt(bytes[0..i]);
    }

    public Int16 ReadInt16()
    {
        var index = (int)this.Stream.Position;
        return BitConverter.ToInt16(InternalBuffer, index);
    }

    public UInt16 ReadUInt16()
    {
        var index = (int)this.Stream.Position;
        return BitConverter.ToUInt16(InternalBuffer, index);
    }

    public Int32 ReadInt32()
    {
        var index = (int)this.Stream.Position;
        return BitConverter.ToInt32(InternalBuffer, index);
    }

    public UInt32 ReadUInt32()
    {
        var index = (int)this.Stream.Position;
        return BitConverter.ToUInt32(InternalBuffer, index);
    }

    public Int64 ReadInt64()
    {
        var index = (int)this.Stream.Position;
        return BitConverter.ToInt64(InternalBuffer, index);
    }

    public UInt64 ReadUInt64()
    {
        var index = (int)this.Stream.Position;
        return BitConverter.ToUInt64(InternalBuffer, index);
    }

    public Double ReadDouble()
    {
        var index = (int)this.Stream.Position;
        return BitConverter.ToDouble(InternalBuffer, index);
    }

    public Single ReadSingle()
    {
        var index = (int)this.Stream.Position;
        return BitConverter.ToSingle(InternalBuffer, index);
    }

    public Decimal ReadDecimal()
    {
        var index = (int)this.Stream.Position;
        var a = this.ReadInt32();
        var b = this.ReadInt32();
        var c = this.ReadInt32();
        var d = this.ReadInt32();
        return new Decimal(new int[] { a, b, c, d });
    }

    public Guid ReadGuid()
    {
        var index = (int)this.Stream.Position;
        return new Guid(this.ReadBytes(16));
    }

    public byte[] ReadBytes(int length)
    {
        var index = (int)this.Stream.Position;
        var bytes = new byte[length];
        Buffer.BlockCopy(InternalBuffer, index, bytes, 0, length);
        return bytes;
    }

    public DateTime ReadDateTime()
    {
        var index = (int)this.Stream.Position;
        return DateTime.FromBinary(this.ReadInt64());
    }

    public string ReadString(int length, TextEncoding textEncoding = TextEncoding.UTF8)
    {
        var index = (int)this.Stream.Position;
        if (textEncoding == TextEncoding.UTF8)
        {
            return Encoding.UTF8.GetString(InternalBuffer, index, length);
        }
        else if (textEncoding == TextEncoding.Latin1)
        {
            return Encoding.Latin1.GetString(InternalBuffer, index, length);
        }
        throw new Exception("Unable to read string encoding.");
    }

    public object Read(DocumentType dataType, int? length = null, TextEncoding textEncoding = TextEncoding.UTF8)
    {
        switch (dataType)
        {
            case DocumentType.Boolean:
                return ReadBool();
            case DocumentType.Byte:
                return ReadByte();
            case DocumentType.SByte:
                return ReadSByte();
            case DocumentType.Char:
                return ReadChar();
            case DocumentType.Decimal:
                return ReadDecimal();
            case DocumentType.Double:
                return ReadDouble();
            case DocumentType.Single:
                return ReadSingle();
            case DocumentType.VarInt:
                return ReadVarInt();
            case DocumentType.Int16:
                return ReadInt16();
            case DocumentType.UInt16:
                return ReadUInt16();
            case DocumentType.Int32:
                return ReadInt32();
            case DocumentType.UInt32:
                return ReadUInt32();
            case DocumentType.Int64:
                return ReadInt64();
            case DocumentType.UInt64:
                return ReadUInt64();
            case DocumentType.DateTime:
                return ReadDateTime();
            case DocumentType.String:
                if (length == null) { throw new Exception("Length required (1)."); }
                return ReadString(length.Value, textEncoding);
            case DocumentType.Guid:
                return ReadGuid();
            case DocumentType.Blob:
                if (length == null) { throw new Exception("Length required (2)."); }
                return ReadBytes(length.Value);
        }
        throw new Exception($"Invalid data type.");
    }

    #endregion

    #region Write methods

    public void Write(bool value, int index)
    {
        var bytes = BitConverter.GetBytes(value);
        this.Stream.Position = index;
        this.Stream.Write(bytes, 0, bytes.Length);
    }

    public void Write(byte value, int index)
    {
        // BitConverter.GetBytes(Byte) treats the byte value as a ushort, so returns 2 bytes.
        // Section 6.1.2 of the C# language spec
        // Take 1st byte only.
        var bytes = BitConverter.GetBytes(value).Take(1).ToArray();
        this.Stream.Position = index;
        this.Stream.Write(bytes, 0, bytes.Length);
    }

    public void Write(sbyte value, int index)
    {
        var bytes = BitConverter.GetBytes(value);
        this.Stream.Position = index;
        this.Stream.Write(bytes, 0, bytes.Length);
    }

    public void Write(char value, int index)
    {
        var bytes = BitConverter.GetBytes(value);
        this.Stream.Position = index;
        this.Stream.Write(bytes, 0, bytes.Length);
    }

    public void Write(VarInt value, int index)
    {
        this.Stream.Position = index;
        this.Stream.Write(value.Bytes, 0, value.Size);
    }

    public void Write(Int16 value, int index)
    {
        var bytes = BitConverter.GetBytes(value);
        this.Stream.Position = index;
        this.Stream.Write(bytes, 0, bytes.Length);
    }

    public void Write(UInt16 value, int index)
    {
        var bytes = BitConverter.GetBytes(value);
        this.Stream.Position = index;
        this.Stream.Write(bytes, 0, bytes.Length);
    }

    public void Write(Int32 value)
    {
        var bytes = BitConverter.GetBytes(value);
        this.Stream.Write(bytes, 0, bytes.Length);
    }

    public void Write(UInt32 value, int index)
    {
        var bytes = BitConverter.GetBytes(value);
        this.Stream.Position = index;
        this.Stream.Write(bytes, 0, bytes.Length);
    }

    public void Write(Int64 value, int index)
    {
        var bytes = BitConverter.GetBytes(value);
        this.Stream.Position = index;
        this.Stream.Write(bytes, 0, bytes.Length);
    }

    public void Write(UInt64 value, int index)
    {
        var bytes = BitConverter.GetBytes(value);
        this.Stream.Position = index;
        this.Stream.Write(bytes, 0, bytes.Length);
    }

    public void Write(Double value, int index)
    {
        var bytes = BitConverter.GetBytes(value);
        this.Stream.Position = index;
        this.Stream.Write(bytes, 0, bytes.Length);
    }

    public void Write(Single value, int index)
    {
        var bytes = BitConverter.GetBytes(value);
        this.Stream.Position = index;
        this.Stream.Write(bytes, 0, bytes.Length);
    }

    public void Write(Decimal value, int index)
    {
        // Split Decimal into 4 ints
        var bits = Decimal.GetBits(value);
        this.Write(bits[0]);
        this.Write(bits[1]);
        this.Write(bits[2]);
        this.Write(bits[3]);
    }

    public void Write(Guid value, int index)
    {
        this.Write(value.ToByteArray(), index);
    }

    public void Write(byte[] value, int index)
    {
        Buffer.BlockCopy(value, 0, this.InternalBuffer, index, value.Length);
    }

    public void Write(DateTime value, int index)
    {
        this.Write(value.ToBinary(), index);
    }

    public void Write(string value, int index, TextEncoding textEncoding = TextEncoding.UTF8)
    {
        if (textEncoding == TextEncoding.UTF8)
        {
            // GetBytes writes directly to the buffer.
            var bytes = Encoding.UTF8.GetBytes(value, 0, value.Length, this.InternalBuffer, index);
        }
        else if (textEncoding == TextEncoding.Latin1)
        {
            var bytes = Encoding.Latin1.GetBytes(value, 0, value.Length, this.InternalBuffer, index);
        }
        else
        {
            throw new Exception("Unable to write string encoding.");
        }
    }

    public void Write(object value, int index, TextEncoding textEncoding = TextEncoding.UTF8)
    {
        var type = value.GetType();
        if (type.IsEnum)
        {
            type = Enum.GetUnderlyingType(type);
        }
        if (type == typeof(bool))
        {
            Write((bool)value, index);
        }
        else if (type == typeof(byte))
        {
            Write((byte)value, index);
        }
        else if (type == typeof(sbyte))
        {
            Write((sbyte)value, index);
        }
        else if (type == typeof(char))
        {
            Write((char)value, index);
        }
        else if (type == typeof(decimal))
        {
            Write((decimal)value, index);
        }
        else if (type == typeof(double))
        {
            Write((double)value, index);
        }
        else if (type == typeof(Single))
        {
            Write((Single)value, index);
        }
        else if (type == typeof(VarInt))
        {
            Write((VarInt)value, index);
        }
        else if (type == typeof(Int16))
        {
            Write((Int16)value, index);
        }
        else if (type == typeof(UInt16))
        {
            Write((UInt16)value, index);
        }
        else if (type == typeof(Int32))
        {
            Write((Int32)value);
        }
        else if (type == typeof(UInt32))
        {
            Write((UInt32)value, index);
        }
        else if (type == typeof(Int64))
        {
            Write((Int64)value, index);
        }
        else if (type == typeof(UInt64))
        {
            Write((UInt64)value, index);
        }
        else if (type == typeof(DateTime))
        {
            Write((DateTime)value, index);
        }
        else if (type == typeof(string))
        {
            Write((string)value, index, textEncoding);
        }
        else if (type == typeof(Guid))
        {
            Write((Guid)value, index);
        }
        else if (type == typeof(byte[]))
        {
            Write((byte[])value, index);
        }
        else if (type == typeof(DateTime))
        {
            Write((DateTime)value, index);
        }
    }

    #endregion
}