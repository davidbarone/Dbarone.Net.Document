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
    public class DocValue : IComparable<DocValue>, IEquatable<DocValue>
    {
        public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Represents a Null type.
        /// </summary>
        public static DocValue Null = new DocValue(DocType.Null, null);

        /// <summary>
        /// Indicate DataType of this value.
        /// </summary>
        public DocType Type { get; }

        /// <summary>
        /// Get internal .NET value object.
        /// </summary>
        public virtual object? RawValue { get; }

        #region Constructors

        public DocValue()
        {
            this.Type = DocType.Null;
            this.RawValue = null;
        }

        public DocValue(Boolean value)
        {
            this.Type = DocType.Boolean;
            this.RawValue = value;
        }

        public DocValue(Byte value)
        {
            this.Type = DocType.Byte;
            this.RawValue = value;
        }

        public DocValue(SByte value)
        {
            this.Type = DocType.SByte;
            this.RawValue = value;
        }

        public DocValue(char value)
        {
            this.Type = DocType.Char;
            this.RawValue = value;
        }

        public DocValue(Int16 value)
        {
            this.Type = DocType.Int16;
            this.RawValue = value;
        }

        public DocValue(UInt16 value)
        {
            this.Type = DocType.UInt16;
            this.RawValue = value;
        }

        public DocValue(Int32 value)
        {
            this.Type = DocType.Int32;
            this.RawValue = value;
        }

        public DocValue(UInt32 value)
        {
            this.Type = DocType.UInt32;
            this.RawValue = value;
        }

        public DocValue(Single value)
        {
            this.Type = DocType.Single;
            this.RawValue = value;
        }

        public DocValue(Int64 value)
        {
            this.Type = DocType.Int64;
            this.RawValue = value;
        }

        public DocValue(UInt64 value)
        {
            this.Type = DocType.UInt64;
            this.RawValue = value;
        }

        public DocValue(DateTime value)
        {
            this.Type = DocType.DateTime;
            this.RawValue = value;
        }

        public DocValue(Double value)
        {
            this.Type = DocType.Double;
            this.RawValue = value;
        }

        public DocValue(Decimal value)
        {
            this.Type = DocType.Decimal;
            this.RawValue = value;
        }

        public DocValue(Guid value)
        {
            this.Type = DocType.Guid;
            this.RawValue = value;
        }

        public DocValue(String value)
        {
            this.Type = value == null ? DocType.Null : DocType.String;
            this.RawValue = value;
        }

        public DocValue(Byte[] value)
        {
            this.Type = value == null ? DocType.Null : DocType.Blob;
            this.RawValue = value;
        }

        protected DocValue(DocType type, object? rawValue)
        {
            this.Type = type;
            this.RawValue = rawValue;
        }

        public DocValue(object? value)
        {
            this.RawValue = value;

            if (value == null) this.Type = DocType.Null;
            else if (value is Boolean) this.Type = DocType.Boolean;
            else if (value is Byte) this.Type = DocType.Byte;
            else if (value is SByte) this.Type = DocType.SByte;
            else if (value is Char) this.Type = DocType.Char;
            else if (value is Int16) this.Type = DocType.Int16;
            else if (value is UInt16) this.Type = DocType.Int16;
            else if (value is Int32) this.Type = DocType.Int32;
            else if (value is UInt32) this.Type = DocType.Int32;
            else if (value is Single) this.Type = DocType.Single;
            else if (value is Int64) this.Type = DocType.Int64;
            else if (value is UInt64) this.Type = DocType.UInt64;
            else if (value is DateTime) this.Type = DocType.DateTime;
            else if (value is Double) this.Type = DocType.Double;
            else if (value is Decimal) this.Type = DocType.Decimal;
            else if (value is Guid) this.Type = DocType.Guid;
            else if (value is String) this.Type = DocType.String;
            else if (value is IDictionary<string, DocValue>) this.Type = DocType.Document;
            else if (value is IList<DocValue>) this.Type = DocType.Array;
            else if (value is Byte[]) this.Type = DocType.Blob;
            else if (value is DocValue)
            {
                var v = (DocValue)value;
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
                    var dict = new Dictionary<string, DocValue>();

                    foreach (var key in dictionary.Keys)
                    {
                        if (key != null && key.ToString() != null)
                        {
                            dict.Add(key.ToString()!, new DocValue((dictionary[key])));
                        }
                    }

                    this.Type = DocType.Document;
                    this.RawValue = dict;
                }
                else if (enumerable != null)
                {
                    var list = new List<DocValue>();

                    foreach (var x in enumerable)
                    {
                        list.Add(new DocValue(x));
                    }

                    this.Type = DocType.Array;
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
        public virtual DocValue this[string name]
        {
            get => throw new InvalidOperationException($"Cannot access non-document type value on type: ${this.Type}.");
            set => throw new InvalidOperationException($"Cannot access non-document type value on type: ${this.Type}.");
        }

        /// <summary>
        /// Get / set value in array by position. Only permitted for DataType.Array
        /// </summary>
        public virtual DocValue this[int index]
        {
            get => throw new InvalidOperationException($"Cannot access non-array type value on type: ${this.Type}.");
            set => throw new InvalidOperationException($"Cannot access non-array type value on type: ${this.Type}.");
        }

        #endregion

        #region Convert types

        public DocArray AsArray => new DocArray(this.RawValue as IList<DocValue>);

        public DocDocument AsDocument => this as DocDocument;

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

        public bool IsNull => this.Type == DocType.Null;

        public bool IsBoolean => this.Type == DocType.Boolean;

        public bool IsByte => this.Type == DocType.Byte;

        public bool IsSByte => this.Type == DocType.SByte;

        public bool IsChar => this.Type == DocType.Char;

        public bool IsInt16 => this.Type == DocType.Int16;

        public bool IsUInt16 => this.Type == DocType.UInt16;

        public bool IsInt32 => this.Type == DocType.Int32;

        public bool IsUInt32 => this.Type == DocType.UInt32;

        public bool IsSingle => this.Type == DocType.Single;

        public bool IsInt64 => this.Type == DocType.Int64;

        public bool IsUInt64 => this.Type == DocType.UInt64;

        public bool IsDateTime => this.Type == DocType.DateTime;

        public bool IsDouble => this.Type == DocType.Double;

        public bool IsDecimal => this.Type == DocType.Decimal;

        public bool IsGuid => this.Type == DocType.Guid;

        public bool IsNumber => this.IsByte || this.IsSByte || this.IsInt16 || this.IsUInt16 || this.IsInt32 || this.IsUInt32 || this.IsInt64 || this.IsUInt64 || this.IsSingle || this.IsDouble || this.IsDecimal;

        public bool IsBlob => this.Type == DocType.Blob;

        public bool IsArray => this.Type == DocType.Array;

        public bool IsDocument => this.Type == DocType.Document;

        public bool IsString => this.Type == DocType.String;

        #endregion

        #region Implicit Ctor

        // Boolean
        public static implicit operator Boolean(DocValue value)
        {
            return (Boolean)value.RawValue;
        }

        // Boolean
        public static implicit operator DocValue(Boolean value)
        {
            return new DocValue(value);
        }

        // Byte
        public static implicit operator Byte(DocValue value)
        {
            return (Byte)value.RawValue;
        }

        // Byte
        public static implicit operator DocValue(Byte value)
        {
            return new DocValue(value);
        }

        // SByte
        public static implicit operator SByte(DocValue value)
        {
            return (SByte)value.RawValue;
        }

        // SByte
        public static implicit operator DocValue(SByte value)
        {
            return new DocValue(value);
        }

        // Char
        public static implicit operator Char(DocValue value)
        {
            return (Char)value.RawValue;
        }

        // Char
        public static implicit operator DocValue(Char value)
        {
            return new DocValue(value);
        }

        // Int16
        public static implicit operator Int16(DocValue value)
        {
            return (Int16)value.RawValue;
        }

        // Int16
        public static implicit operator DocValue(Int16 value)
        {
            return new DocValue(value);
        }

        // UInt16
        public static implicit operator UInt16(DocValue value)
        {
            return (UInt16)value.RawValue;
        }

        // UInt16
        public static implicit operator DocValue(UInt16 value)
        {
            return new DocValue(value);
        }

        // Int32
        public static implicit operator Int32(DocValue value)
        {
            return (Int32)value.RawValue;
        }

        // Int32
        public static implicit operator DocValue(Int32 value)
        {
            return new DocValue(value);
        }

        // UInt32
        public static implicit operator UInt32(DocValue value)
        {
            return (UInt32)value.RawValue;
        }

        // UInt32
        public static implicit operator DocValue(UInt32 value)
        {
            return new DocValue(value);
        }

        // Single
        public static implicit operator Single(DocValue value)
        {
            return (Single)value.RawValue;
        }

        // Single
        public static implicit operator DocValue(Single value)
        {
            return new DocValue(value);
        }

        // Int64
        public static implicit operator Int64(DocValue value)
        {
            return (Int64)value.RawValue;
        }

        // Int64
        public static implicit operator DocValue(Int64 value)
        {
            return new DocValue(value);
        }

        // UInt64
        public static implicit operator UInt64(DocValue value)
        {
            return (UInt64)value.RawValue;
        }

        // UInt64
        public static implicit operator DocValue(UInt64 value)
        {
            return new DocValue(value);
        }

        // DateTime
        public static implicit operator DateTime(DocValue value)
        {
            return (DateTime)value.RawValue;
        }

        // DateTime
        public static implicit operator DocValue(DateTime value)
        {
            return new DocValue(value);
        }

        // Double
        public static implicit operator Double(DocValue value)
        {
            return (Double)value.RawValue;
        }

        // Double
        public static implicit operator DocValue(Double value)
        {
            return new DocValue(value);
        }

        // Decimal
        public static implicit operator Decimal(DocValue value)
        {
            return (Decimal)value.RawValue;
        }

        // Decimal
        public static implicit operator DocValue(Decimal value)
        {
            return new DocValue(value);
        }

        // Guid
        public static implicit operator Guid(DocValue value)
        {
            return (Guid)value.RawValue;
        }

        // Guid
        public static implicit operator DocValue(Guid value)
        {
            return new DocValue(value);
        }

        // String
        public static implicit operator String(DocValue value)
        {
            return (String)value.RawValue;
        }

        // String
        public static implicit operator DocValue(String value)
        {
            return new DocValue(value);
        }

        // Binary
        public static implicit operator Byte[](DocValue value)
        {
            return (Byte[])value.RawValue;
        }

        // Binary
        public static implicit operator DocValue(Byte[] value)
        {
            return new DocValue(value);
        }

        // +
        public static DocValue operator +(DocValue left, DocValue right)
        {
            if (!left.IsNumber || !right.IsNumber) return DocValue.Null;

            if (left.IsInt32 && right.IsInt32) return left.AsInt32 + right.AsInt32;
            if (left.IsInt64 && right.IsInt64) return left.AsInt64 + right.AsInt64;
            if (left.IsDouble && right.IsDouble) return left.AsDouble + right.AsDouble;
            if (left.IsDecimal && right.IsDecimal) return left.AsDecimal + right.AsDecimal;

            var result = left.AsDecimal + right.AsDecimal;
            var type = (DocType)Math.Max((int)left.Type, (int)right.Type);

            return
                type == DocType.Int64 ? new DocValue((Int64)result) :
                type == DocType.Double ? new DocValue((Double)result) :
                new DocValue(result);
        }

        // -
        public static DocValue operator -(DocValue left, DocValue right)
        {
            if (!left.IsNumber || !right.IsNumber) return DocValue.Null;

            if (left.IsInt32 && right.IsInt32) return left.AsInt32 - right.AsInt32;
            if (left.IsInt64 && right.IsInt64) return left.AsInt64 - right.AsInt64;
            if (left.IsDouble && right.IsDouble) return left.AsDouble - right.AsDouble;
            if (left.IsDecimal && right.IsDecimal) return left.AsDecimal - right.AsDecimal;

            var result = left.AsDecimal - right.AsDecimal;
            var type = (DocType)Math.Max((int)left.Type, (int)right.Type);

            return
                type == DocType.Int64 ? new DocValue((Int64)result) :
                type == DocType.Double ? new DocValue((Double)result) :
                new DocValue(result);
        }

        // *
        public static DocValue operator *(DocValue left, DocValue right)
        {
            if (!left.IsNumber || !right.IsNumber) return DocValue.Null;

            if (left.IsInt32 && right.IsInt32) return left.AsInt32 * right.AsInt32;
            if (left.IsInt64 && right.IsInt64) return left.AsInt64 * right.AsInt64;
            if (left.IsDouble && right.IsDouble) return left.AsDouble * right.AsDouble;
            if (left.IsDecimal && right.IsDecimal) return left.AsDecimal * right.AsDecimal;

            var result = left.AsDecimal * right.AsDecimal;
            var type = (DocType)Math.Max((int)left.Type, (int)right.Type);

            return
                type == DocType.Int64 ? new DocValue((Int64)result) :
                type == DocType.Double ? new DocValue((Double)result) :
                new DocValue(result);
        }

        // /
        public static DocValue operator /(DocValue left, DocValue right)
        {
            if (!left.IsNumber || !right.IsNumber) return DocValue.Null;
            if (left.IsDecimal || right.IsDecimal) return left.AsDecimal / right.AsDecimal;

            return left.AsDouble / right.AsDouble;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        #endregion

        #region IComparable<BsonValue>, IEquatable<BsonValue>

        public virtual int CompareTo(DocValue other)
        {
            return this.CompareTo(other, Collation.Binary);
        }

        public virtual int CompareTo(DocValue other, Collation collation)
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
                case DocType.Null:
                    return 0;

                case DocType.Int32: return this.AsInt32.CompareTo(other.AsInt32);
                case DocType.Int64: return this.AsInt64.CompareTo(other.AsInt64);
                case DocType.Double: return this.AsDouble.CompareTo(other.AsDouble);
                case DocType.Decimal: return this.AsDecimal.CompareTo(other.AsDecimal);

                case DocType.String: return collation.Compare(this.AsString, other.AsString);

                case DocType.Document: return this.AsDocument.CompareTo(other);
                case DocType.Array: return this.AsArray.CompareTo(other);

                case DocType.Blob: return this.BinaryCompare(this.AsBinary, other.AsBinary);
                case DocType.Guid: return this.AsGuid.CompareTo(other.AsGuid);

                case DocType.Boolean: return this.AsBoolean.CompareTo(other.AsBoolean);
                case DocType.DateTime:
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

        public bool Equals(DocValue other)
        {
            return this.CompareTo(other) == 0;
        }

        #endregion

        #region Operators

        public static bool operator ==(DocValue lhs, DocValue rhs)
        {
            if (object.ReferenceEquals(lhs, null)) return object.ReferenceEquals(rhs, null);
            if (object.ReferenceEquals(rhs, null)) return false; // don't check type because sometimes different types can be ==

            return lhs.Equals(rhs);
        }

        public static bool operator !=(DocValue lhs, DocValue rhs)
        {
            return !(lhs == rhs);
        }

        public static bool operator >=(DocValue lhs, DocValue rhs)
        {
            return lhs.CompareTo(rhs) >= 0;
        }

        public static bool operator >(DocValue lhs, DocValue rhs)
        {
            return lhs.CompareTo(rhs) > 0;
        }

        public static bool operator <(DocValue lhs, DocValue rhs)
        {
            return lhs.CompareTo(rhs) < 0;
        }

        public static bool operator <=(DocValue lhs, DocValue rhs)
        {
            return lhs.CompareTo(rhs) <= 0;
        }

        public override bool Equals(object obj)
        {
            if (obj is DocValue other)
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
        internal virtual int GetBytesCount(bool recalc)
        {
            switch (this.Type)
            {
                case DocType.Null: return 0;

                case DocType.Boolean:
                case DocType.SByte:
                case DocType.Byte: return 1;

                case DocType.Char:
                case DocType.Int16:
                case DocType.UInt16: return 2;

                case DocType.Int32:
                case DocType.UInt32:
                case DocType.Single: return 4;

                case DocType.Int64:
                case DocType.UInt64:
                case DocType.DateTime:
                case DocType.Double: return 8;

                case DocType.Decimal:
                case DocType.Guid: return 16;

                case DocType.String: return Encoding.UTF8.GetByteCount(this.AsString);
                case DocType.Blob: return this.AsBinary.Length;
                case DocType.Document: return this.AsDocument.GetBytesCount(recalc);
                case DocType.Array: return this.AsArray.GetBytesCount(recalc);
            }

            throw new ArgumentException();
        }

        /// <summary>
        /// Get how many bytes one single element will used in BSON format
        /// </summary>
        protected int GetBytesCountElement(string key, DocValue value)
        {
            // check if data type is variant
            var variant = value.Type == DocType.String || value.Type == DocType.Blob || value.Type == DocType.Guid;

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
