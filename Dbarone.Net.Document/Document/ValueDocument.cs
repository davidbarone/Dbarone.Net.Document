using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Text.Json;

namespace Dbarone.Net.Document
{
    /// <summary>
    /// Represent a simple value used in Document.
    /// </summary>
    public class ValueDocument : IComparable<ValueDocument>, IEquatable<ValueDocument>
    {
        public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Represents a Null type.
        /// </summary>
        public static ValueDocument Null = new ValueDocument(DocumentType.Null, null);

        /// <summary>
        /// Indicate DataType of this value.
        /// </summary>
        public DocumentType Type { get; }

        /// <summary>
        /// Get internal .NET value object.
        /// </summary>
        public virtual object? RawValue { get; }

        #region Constructors

        public ValueDocument()
        {
            this.Type = DocumentType.Null;
            this.RawValue = null;
        }

        public ValueDocument(Boolean value)
        {
            this.Type = DocumentType.Boolean;
            this.RawValue = value;
        }

        public ValueDocument(Byte value)
        {
            this.Type = DocumentType.Byte;
            this.RawValue = value;
        }

        public ValueDocument(SByte value)
        {
            this.Type = DocumentType.SByte;
            this.RawValue = value;
        }

        public ValueDocument(char value)
        {
            this.Type = DocumentType.Char;
            this.RawValue = value;
        }

        public ValueDocument(Int16 value)
        {
            this.Type = DocumentType.Int16;
            this.RawValue = value;
        }

        public ValueDocument(UInt16 value)
        {
            this.Type = DocumentType.UInt16;
            this.RawValue = value;
        }

        public ValueDocument(Int32 value)
        {
            this.Type = DocumentType.Int32;
            this.RawValue = value;
        }

        public ValueDocument(UInt32 value)
        {
            this.Type = DocumentType.UInt32;
            this.RawValue = value;
        }

        public ValueDocument(Single value)
        {
            this.Type = DocumentType.Single;
            this.RawValue = value;
        }

        public ValueDocument(Int64 value)
        {
            this.Type = DocumentType.Int64;
            this.RawValue = value;
        }

        public ValueDocument(UInt64 value)
        {
            this.Type = DocumentType.UInt64;
            this.RawValue = value;
        }

        public ValueDocument(DateTime value)
        {
            this.Type = DocumentType.DateTime;
            this.RawValue = value;
        }

        public ValueDocument(Double value)
        {
            this.Type = DocumentType.Double;
            this.RawValue = value;
        }

        public ValueDocument(Decimal value)
        {
            this.Type = DocumentType.Decimal;
            this.RawValue = value;
        }

        public ValueDocument(Guid value)
        {
            this.Type = DocumentType.Guid;
            this.RawValue = value;
        }

        public ValueDocument(String value)
        {
            this.Type = value == null ? DocumentType.Null : DocumentType.String;
            this.RawValue = value;
        }

        public ValueDocument(Byte[] value)
        {
            this.Type = value == null ? DocumentType.Null : DocumentType.Blob;
            this.RawValue = value;
        }

        protected ValueDocument(DocumentType type, object? rawValue)
        {
            this.Type = type;
            this.RawValue = rawValue;
        }

        public ValueDocument(object? value)
        {
            this.RawValue = value;

            if (value == null) this.Type = DocumentType.Null;
            else if (value is Boolean) this.Type = DocumentType.Boolean;
            else if (value is Byte) this.Type = DocumentType.Byte;
            else if (value is SByte) this.Type = DocumentType.SByte;
            else if (value is Char) this.Type = DocumentType.Char;
            else if (value is Int16) this.Type = DocumentType.Int16;
            else if (value is UInt16) this.Type = DocumentType.Int16;
            else if (value is Int32) this.Type = DocumentType.Int32;
            else if (value is UInt32) this.Type = DocumentType.Int32;
            else if (value is Single) this.Type = DocumentType.Single;
            else if (value is Int64) this.Type = DocumentType.Int64;
            else if (value is UInt64) this.Type = DocumentType.UInt64;
            else if (value is DateTime) this.Type = DocumentType.DateTime;
            else if (value is Double) this.Type = DocumentType.Double;
            else if (value is Decimal) this.Type = DocumentType.Decimal;
            else if (value is Guid) this.Type = DocumentType.Guid;
            else if (value is String) this.Type = DocumentType.String;
            else if (value is IDictionary<string, ValueDocument>) this.Type = DocumentType.Document;
            else if (value is IList<ValueDocument>) this.Type = DocumentType.Array;
            else if (value is Byte[]) this.Type = DocumentType.Blob;
            else if (value is ValueDocument)
            {
                var v = (ValueDocument)value;
                this.Type = v.Type;
                this.RawValue = v.RawValue;
            }
            else
            {
                // test for array or dictionary (document)
                var enumerable = value as System.Collections.IEnumerable;
                var dictionary = value as System.Collections.IDictionary;

                // test first for dictionary (because IDictionary implements IEnumerable)
                if (dictionary != null)
                {
                    var dict = new Dictionary<string, ValueDocument>();

                    foreach (var key in dictionary.Keys)
                    {
                        if (key != null && key.ToString() != null)
                        {
                            dict.Add(key.ToString()!, new ValueDocument((dictionary[key])));
                        }
                    }

                    this.Type = DocumentType.Document;
                    this.RawValue = dict;
                }
                else if (enumerable != null)
                {
                    var list = new List<ValueDocument>();

                    foreach (var x in enumerable)
                    {
                        list.Add(new ValueDocument(x));
                    }

                    this.Type = DocumentType.Array;
                    this.RawValue = list;
                }
                else
                {
                    throw new InvalidCastException("Value is not a valid document type. Use Mapper class for more complex types.");
                }
            }
        }

        #endregion

        #region Index "this" property

        /// <summary>
        /// Get / set a field for document. Fields are case sensitive. Only permitted for DataType.Document.
        /// </summary>
        public virtual ValueDocument this[string name]
        {
            get => throw new InvalidOperationException($"Cannot access non-document type value on type: ${this.Type}.");
            set => throw new InvalidOperationException($"Cannot access non-document type value on type: ${this.Type}.");
        }

        /// <summary>
        /// Get / set value in array by position. Only permitted for DataType.Array
        /// </summary>
        public virtual ValueDocument this[int index]
        {
            get => throw new InvalidOperationException($"Cannot access non-array type value on type: ${this.Type}.");
            set => throw new InvalidOperationException($"Cannot access non-array type value on type: ${this.Type}.");
        }

        #endregion

        #region Convert types

        public ArrayDocument AsArray => new ArrayDocument(this.RawValue as IList<ValueDocument>);

        public DictionaryDocument AsDocument => this as DictionaryDocument;

        public Byte[] AsBinary => this.RawValue as Byte[];

        public string AsString => (string)this.RawValue;

        public bool AsBoolean => (bool)this.RawValue;

        public Byte AsByte => (Byte)this.RawValue;

        public SByte AsSByte => (SByte)this.RawValue;

        public int AsInt16 => Convert.ToInt16(this.RawValue);

        public uint AsUInt16 => Convert.ToUInt16(this.RawValue);

        public int AsInt32 => Convert.ToInt32(this.RawValue);

        public uint AsUInt32 => Convert.ToUInt32(this.RawValue);

        public long AsInt64 => Convert.ToInt64(this.RawValue);

        public ulong AsUInt64 => Convert.ToUInt64(this.RawValue);

        public DateTime AsDateTime => (DateTime)this.RawValue;
        
        public double AsDouble => Convert.ToDouble(this.RawValue);

        public decimal AsDecimal => Convert.ToDecimal(this.RawValue);

        public Guid AsGuid => (Guid)this.RawValue;

        #endregion

        #region IsTypes

        public bool IsNull => this.Type == DocumentType.Null;

        public bool IsBoolean => this.Type == DocumentType.Boolean;

        public bool IsByte => this.Type == DocumentType.Byte;

        public bool IsSByte => this.Type == DocumentType.SByte;

        public bool IsChar => this.Type == DocumentType.Char;

        public bool IsInt16 => this.Type == DocumentType.Int16;

        public bool IsUInt16 => this.Type == DocumentType.UInt16;

        public bool IsInt32 => this.Type == DocumentType.Int32;

        public bool IsUInt32 => this.Type == DocumentType.UInt32;

        public bool IsSingle => this.Type == DocumentType.Single;

        public bool IsInt64 => this.Type == DocumentType.Int64;

        public bool IsUInt64 => this.Type == DocumentType.UInt64;

        public bool IsDateTime => this.Type == DocumentType.DateTime;

        public bool IsDouble => this.Type == DocumentType.Double;

        public bool IsDecimal => this.Type == DocumentType.Decimal;

        public bool IsGuid => this.Type == DocumentType.Guid;

        public bool IsNumber => this.IsByte || this.IsSByte || this.IsInt16 || this.IsUInt16 || this.IsInt32 || this.IsUInt32 || this.IsInt64 || this.IsUInt64 || this.IsSingle || this.IsDouble || this.IsDecimal;

        public bool IsBlob => this.Type == DocumentType.Blob;

        public bool IsArray => this.Type == DocumentType.Array;

        public bool IsDocument => this.Type == DocumentType.Document;

        public bool IsString => this.Type == DocumentType.String;

        #endregion

        #region Implicit Ctor

        // Boolean
        public static implicit operator Boolean(ValueDocument value)
        {
            return (Boolean)value.RawValue;
        }

        // Boolean
        public static implicit operator ValueDocument(Boolean value)
        {
            return new ValueDocument(value);
        }

        // Byte
        public static implicit operator Byte(ValueDocument value)
        {
            return (Byte)value.RawValue;
        }

        // Byte
        public static implicit operator ValueDocument(Byte value)
        {
            return new ValueDocument(value);
        }

        // SByte
        public static implicit operator SByte(ValueDocument value)
        {
            return (SByte)value.RawValue;
        }

        // SByte
        public static implicit operator ValueDocument(SByte value)
        {
            return new ValueDocument(value);
        }

        // Char
        public static implicit operator Char(ValueDocument value)
        {
            return (Char)value.RawValue;
        }

        // Char
        public static implicit operator ValueDocument(Char value)
        {
            return new ValueDocument(value);
        }

        // Int16
        public static implicit operator Int16(ValueDocument value)
        {
            return (Int16)value.RawValue;
        }

        // Int16
        public static implicit operator ValueDocument(Int16 value)
        {
            return new ValueDocument(value);
        }

        // UInt16
        public static implicit operator UInt16(ValueDocument value)
        {
            return (UInt16)value.RawValue;
        }

        // UInt16
        public static implicit operator ValueDocument(UInt16 value)
        {
            return new ValueDocument(value);
        }

        // Int32
        public static implicit operator Int32(ValueDocument value)
        {
            return (Int32)value.RawValue;
        }

        // Int32
        public static implicit operator ValueDocument(Int32 value)
        {
            return new ValueDocument(value);
        }

        // UInt32
        public static implicit operator UInt32(ValueDocument value)
        {
            return (UInt32)value.RawValue;
        }

        // UInt32
        public static implicit operator ValueDocument(UInt32 value)
        {
            return new ValueDocument(value);
        }

        // Single
        public static implicit operator Single(ValueDocument value)
        {
            return (Single)value.RawValue;
        }

        // Single
        public static implicit operator ValueDocument(Single value)
        {
            return new ValueDocument(value);
        }

        // Int64
        public static implicit operator Int64(ValueDocument value)
        {
            return (Int64)value.RawValue;
        }

        // Int64
        public static implicit operator ValueDocument(Int64 value)
        {
            return new ValueDocument(value);
        }

        // UInt64
        public static implicit operator UInt64(ValueDocument value)
        {
            return (UInt64)value.RawValue;
        }

        // UInt64
        public static implicit operator ValueDocument(UInt64 value)
        {
            return new ValueDocument(value);
        }

        // DateTime
        public static implicit operator DateTime(ValueDocument value)
        {
            return (DateTime)value.RawValue;
        }

        // DateTime
        public static implicit operator ValueDocument(DateTime value)
        {
            return new ValueDocument(value);
        }

        // Double
        public static implicit operator Double(ValueDocument value)
        {
            return (Double)value.RawValue;
        }

        // Double
        public static implicit operator ValueDocument(Double value)
        {
            return new ValueDocument(value);
        }

        // Decimal
        public static implicit operator Decimal(ValueDocument value)
        {
            return (Decimal)value.RawValue;
        }

        // Decimal
        public static implicit operator ValueDocument(Decimal value)
        {
            return new ValueDocument(value);
        }

        // Guid
        public static implicit operator Guid(ValueDocument value)
        {
            return (Guid)value.RawValue;
        }

        // Guid
        public static implicit operator ValueDocument(Guid value)
        {
            return new ValueDocument(value);
        }

        // String
        public static implicit operator String(ValueDocument value)
        {
            return (String)value.RawValue;
        }

        // String
        public static implicit operator ValueDocument(String value)
        {
            return new ValueDocument(value);
        }

        // Binary
        public static implicit operator Byte[](ValueDocument value)
        {
            return (Byte[])value.RawValue;
        }

        // Binary
        public static implicit operator ValueDocument(Byte[] value)
        {
            return new ValueDocument(value);
        }

        // +
        public static ValueDocument operator +(ValueDocument left, ValueDocument right)
        {
            if (!left.IsNumber || !right.IsNumber) return ValueDocument.Null;

            if (left.IsInt32 && right.IsInt32) return left.AsInt32 + right.AsInt32;
            if (left.IsInt64 && right.IsInt64) return left.AsInt64 + right.AsInt64;
            if (left.IsDouble && right.IsDouble) return left.AsDouble + right.AsDouble;
            if (left.IsDecimal && right.IsDecimal) return left.AsDecimal + right.AsDecimal;

            var result = left.AsDecimal + right.AsDecimal;
            var type = (DocumentType)Math.Max((int)left.Type, (int)right.Type);

            return
                type == DocumentType.Int64 ? new ValueDocument((Int64)result) :
                type == DocumentType.Double ? new ValueDocument((Double)result) :
                new ValueDocument(result);
        }

        // -
        public static ValueDocument operator -(ValueDocument left, ValueDocument right)
        {
            if (!left.IsNumber || !right.IsNumber) return ValueDocument.Null;

            if (left.IsInt32 && right.IsInt32) return left.AsInt32 - right.AsInt32;
            if (left.IsInt64 && right.IsInt64) return left.AsInt64 - right.AsInt64;
            if (left.IsDouble && right.IsDouble) return left.AsDouble - right.AsDouble;
            if (left.IsDecimal && right.IsDecimal) return left.AsDecimal - right.AsDecimal;

            var result = left.AsDecimal - right.AsDecimal;
            var type = (DocumentType)Math.Max((int)left.Type, (int)right.Type);

            return
                type == DocumentType.Int64 ? new ValueDocument((Int64)result) :
                type == DocumentType.Double ? new ValueDocument((Double)result) :
                new ValueDocument(result);
        }

        // *
        public static ValueDocument operator *(ValueDocument left, ValueDocument right)
        {
            if (!left.IsNumber || !right.IsNumber) return ValueDocument.Null;

            if (left.IsInt32 && right.IsInt32) return left.AsInt32 * right.AsInt32;
            if (left.IsInt64 && right.IsInt64) return left.AsInt64 * right.AsInt64;
            if (left.IsDouble && right.IsDouble) return left.AsDouble * right.AsDouble;
            if (left.IsDecimal && right.IsDecimal) return left.AsDecimal * right.AsDecimal;

            var result = left.AsDecimal * right.AsDecimal;
            var type = (DocumentType)Math.Max((int)left.Type, (int)right.Type);

            return
                type == DocumentType.Int64 ? new ValueDocument((Int64)result) :
                type == DocumentType.Double ? new ValueDocument((Double)result) :
                new ValueDocument(result);
        }

        // /
        public static ValueDocument operator /(ValueDocument left, ValueDocument right)
        {
            if (!left.IsNumber || !right.IsNumber) return ValueDocument.Null;
            if (left.IsDecimal || right.IsDecimal) return left.AsDecimal / right.AsDecimal;

            return left.AsDouble / right.AsDouble;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        #endregion

        #region IComparable<BsonValue>, IEquatable<BsonValue>

        public virtual int CompareTo(ValueDocument other)
        {
            return this.CompareTo(other, Collation.Binary);
        }

        public virtual int CompareTo(ValueDocument other, Collation collation)
        {
            // first, test if types are different
            if (this.Type != other.Type)
            {
                // if both values are number, convert them to Decimal (128 bits) to compare
                // it's the slowest way, but more secure
                if (this.IsNumber && other.IsNumber)
                {
                    return Convert.ToDecimal(this.RawValue).CompareTo(Convert.ToDecimal(other.RawValue));
                }
                // if not, order by sort type order
                else
                {
                    var result = this.Type.CompareTo(other.Type);
                    return result < 0 ? -1 : result > 0 ? +1 : 0;
                }
            }

            // for both values with same data type just compare
            switch (this.Type)
            {
                case DocumentType.Null:
                    return 0;

                case DocumentType.Int32: return this.AsInt32.CompareTo(other.AsInt32);
                case DocumentType.Int64: return this.AsInt64.CompareTo(other.AsInt64);
                case DocumentType.Double: return this.AsDouble.CompareTo(other.AsDouble);
                case DocumentType.Decimal: return this.AsDecimal.CompareTo(other.AsDecimal);

                case DocumentType.String: return collation.Compare(this.AsString, other.AsString);

                case DocumentType.Document: return this.AsDocument.CompareTo(other);
                case DocumentType.Array: return this.AsArray.CompareTo(other);

                case DocumentType.Blob: return this.BinaryCompare(this.AsBinary, other.AsBinary);
                case DocumentType.Guid: return this.AsGuid.CompareTo(other.AsGuid);

                case DocumentType.Boolean: return this.AsBoolean.CompareTo(other.AsBoolean);
                case DocumentType.DateTime:
                    var d0 = this.AsDateTime;
                    var d1 = other.AsDateTime;
                    if (d0.Kind != DateTimeKind.Utc) d0 = d0.ToUniversalTime();
                    if (d1.Kind != DateTimeKind.Utc) d1 = d1.ToUniversalTime();
                    return d0.CompareTo(d1);

                default: throw new NotImplementedException();
            }
        }

        private int BinaryCompare(byte[] lh, byte[] rh)
        {
            if (lh == null) return rh == null ? 0 : -1;
            if (rh == null) return 1;

            var result = 0;
            var i = 0;
            var stop = Math.Min(lh.Length, rh.Length);

            for (; 0 == result && i < stop; i++)
                result = lh[i].CompareTo(rh[i]);

            if (result != 0) return result < 0 ? -1 : 1;
            if (i == lh.Length) return i == rh.Length ? 0 : -1;
            return 1;
        }

        public bool Equals(ValueDocument other)
        {
            return this.CompareTo(other) == 0;
        }

        #endregion

        #region Operators

        public static bool operator ==(ValueDocument lhs, ValueDocument rhs)
        {
            if (object.ReferenceEquals(lhs, null)) return object.ReferenceEquals(rhs, null);
            if (object.ReferenceEquals(rhs, null)) return false; // don't check type because sometimes different types can be ==

            return lhs.Equals(rhs);
        }

        public static bool operator !=(ValueDocument lhs, ValueDocument rhs)
        {
            return !(lhs == rhs);
        }

        public static bool operator >=(ValueDocument lhs, ValueDocument rhs)
        {
            return lhs.CompareTo(rhs) >= 0;
        }

        public static bool operator >(ValueDocument lhs, ValueDocument rhs)
        {
            return lhs.CompareTo(rhs) > 0;
        }

        public static bool operator <(ValueDocument lhs, ValueDocument rhs)
        {
            return lhs.CompareTo(rhs) < 0;
        }

        public static bool operator <=(ValueDocument lhs, ValueDocument rhs)
        {
            return lhs.CompareTo(rhs) <= 0;
        }

        public override bool Equals(object obj)
        {
            if (obj is ValueDocument other)
            {
                return this.Equals(other);
            }

            return false;
        }

        public override int GetHashCode()
        {
            var hash = 17;
            hash = 37 * hash + this.Type.GetHashCode();
            hash = 37 * hash + (this.RawValue?.GetHashCode() ?? 0);
            return hash;
        }

        #endregion

        #region GetBytesCount()

        /// <summary>
        /// Returns how many bytes this BsonValue will consume when converted into binary BSON
        /// If recalc = false, use cached length value (from Array/Document only)
        /// </summary>
        /// <param name="recalc">Set to true to force recalculation.</param>
        /// <returns>Returns the document length.</returns>
        /// <exception cref="ArgumentException">Throws an exception if an invalid document type.</exception>
        internal virtual int GetBytesCount(bool recalc)
        {
            switch (this.Type)
            {
                case DocumentType.Null: return 0;

                case DocumentType.Boolean:
                case DocumentType.SByte:
                case DocumentType.Byte: return 1;

                case DocumentType.Char:
                case DocumentType.Int16:
                case DocumentType.UInt16: return 2;

                case DocumentType.Int32:
                case DocumentType.UInt32:
                case DocumentType.Single: return 4;

                case DocumentType.Int64:
                case DocumentType.UInt64:
                case DocumentType.DateTime:
                case DocumentType.Double: return 8;

                case DocumentType.Decimal:
                case DocumentType.Guid: return 16;

                case DocumentType.String: return Encoding.UTF8.GetByteCount(this.AsString);
                case DocumentType.Blob: return this.AsBinary.Length;
                case DocumentType.Document: return this.AsDocument.GetBytesCount(recalc);
                case DocumentType.Array: return this.AsArray.GetBytesCount(recalc);
            }

            throw new ArgumentException();
        }

        /// <summary>
        /// Get how many bytes one single element will used in BSON format
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>Returns the byte count for an element.</returns>
        protected int GetBytesCountElement(string key, ValueDocument value)
        {
            // check if data type is variant
            var variant = value.Type == DocumentType.String || value.Type == DocumentType.Blob || value.Type == DocumentType.Guid;

            return
                1 + // element type
                Encoding.UTF8.GetByteCount(key) + // CString
                1 + // CString \0
                value.GetBytesCount(true) +
                (variant ? 5 : 0); // bytes.Length + 0x??
        }

        #endregion
    }
}
