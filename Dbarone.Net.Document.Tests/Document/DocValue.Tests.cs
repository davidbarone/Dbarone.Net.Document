namespace Dbarone.Net.Document.Tests;
using Xunit;
using Dbarone.Net.Document;
using System;
using System.Security.Cryptography.X509Certificates;

public class DocTests
{
    #region Ctor tests

    [Fact]
    public void Test_DictionaryDocument_Ctor()
    {
        Dictionary<string, object> dict = new Dictionary<string, object>();
        dict["foo"] = 123;
        dict["bar"] = "abc";
        dict["baz"] = DateTime.Now;
        DocumentValue docValue = new DocumentValue(dict);
        var a = docValue.AsDocument;
        Assert.IsType<DictionaryDocument>(a);
        Assert.Equal(123, a["foo"].AsInt32);
    }



    public void Test_ArrayDictionaryDocument_Ctor()
    {
        List<Dictionary<string, object>> arr = new List<Dictionary<string, object>>();
        Dictionary<string, object> dict = new Dictionary<string, object>();
        dict["foo"] = 123;
        dict["bar"] = "abc";
        dict["baz"] = DateTime.Now;
        arr.Add(dict);
        arr.Add(dict);
        DocumentValue docValue = new DocumentValue(dict);
        var a = docValue.AsDocument;
        Assert.IsType<DictionaryDocument>(a);
        Assert.Equal(123, a["foo"].AsInt32);

    }

    [Fact]
    public void Test_DocumentValue_Ctor_Null()
    {
        DocumentValue DocumentValue = new DocumentValue();
        Assert.Equal(null, DocumentValue.RawValue);
        Assert.Equal(DocumentType.Null, DocumentValue.Type);
    }

    [Fact]
    public void Test_DocumentValue_Ctor_Boolean()
    {
        Boolean boolValue = true;
        DocumentValue DocumentValue = boolValue;
        Assert.Equal(boolValue, DocumentValue.RawValue);
        Assert.Equal(DocumentType.Boolean, DocumentValue.Type);
    }

    [Fact]
    public void Test_DocumentValue_Ctor_Byte()
    {
        Byte byteValue = Byte.MaxValue;
        DocumentValue DocumentValue = byteValue;
        Assert.Equal(byteValue, DocumentValue.RawValue);
        Assert.Equal(DocumentType.Byte, DocumentValue.Type);
    }

    [Fact]
    public void Test_DocumentValue_Ctor_SByte()
    {
        SByte sByteValue = 123;
        DocumentValue DocumentValue = sByteValue;
        Assert.Equal(sByteValue, DocumentValue.RawValue);
        Assert.Equal(DocumentType.SByte, DocumentValue.Type);
    }

    [Fact]
    public void Test_DocumentValue_Ctor_Char()
    {
        Char charValue = 'a';
        DocumentValue DocumentValue = charValue;
        Assert.Equal(charValue, DocumentValue.RawValue);
        Assert.Equal(DocumentType.Char, DocumentValue.Type);
    }

    [Fact]
    public void Test_DocumentValue_Ctor_Int16()
    {
        Int16 int16Value = -123;
        DocumentValue DocumentValue = int16Value;
        Assert.Equal(int16Value, DocumentValue.RawValue);
        Assert.Equal(DocumentType.Int16, DocumentValue.Type);
    }

    [Fact]
    public void Test_DocumentValue_Ctor_UInt16()
    {
        UInt16 uInt32Value = 123;
        DocumentValue DocumentValue = uInt32Value;
        Assert.Equal(uInt32Value, DocumentValue.RawValue);
        Assert.Equal(DocumentType.UInt16, DocumentValue.Type);
    }

    [Fact]
    public void Test_DocumentValue_Ctor_Int32()
    {
        Int32 int32Value = -123;
        DocumentValue DocumentValue = int32Value;
        Assert.Equal(int32Value, DocumentValue.RawValue);
        Assert.Equal(DocumentType.Int32, DocumentValue.Type);
    }

    [Fact]
    public void Test_DocumentValue_Ctor_UInt32()
    {
        UInt32 uInt32Value = 123;
        DocumentValue DocumentValue = uInt32Value;
        Assert.Equal(uInt32Value, DocumentValue.RawValue);
        Assert.Equal(DocumentType.UInt32, DocumentValue.Type);
    }

    [Fact]
    public void Test_DocumentValue_Ctor_Single()
    {
        Single singleValue = -123.45f;
        DocumentValue DocumentValue = singleValue;
        Assert.Equal(singleValue, DocumentValue.RawValue);
        Assert.Equal(DocumentType.Single, DocumentValue.Type);
    }

    [Fact]
    public void Test_DocumentValue_Ctor_Int64()
    {
        Int64 int64Value = Int64.MinValue;
        DocumentValue DocumentValue = int64Value;
        Assert.Equal(int64Value, DocumentValue.RawValue);
        Assert.Equal(DocumentType.Int64, DocumentValue.Type);
    }

    [Fact]
    public void Test_DocumentValue_Ctor_UInt64()
    {
        UInt64 uInt64Value = UInt64.MaxValue;
        DocumentValue DocumentValue = uInt64Value;
        Assert.Equal(uInt64Value, DocumentValue.RawValue);
        Assert.Equal(DocumentType.UInt64, DocumentValue.Type);
    }

    [Fact]
    public void Test_DocumentValue_Ctor_DateTime()
    {
        DateTime datetimeValue = DateTime.Now;
        DocumentValue DocumentValue = datetimeValue;
        Assert.Equal(datetimeValue, DocumentValue.RawValue);
        Assert.Equal(DocumentType.DateTime, DocumentValue.Type);
    }

    [Fact]
    public void Test_DocumentValue_Ctor_Double()
    {
        Double doubleValue = Double.MinValue;
        DocumentValue DocumentValue = doubleValue;
        Assert.Equal(doubleValue, DocumentValue.RawValue);
        Assert.Equal(DocumentType.Double, DocumentValue.Type);
    }

    [Fact]
    public void Test_DocumentValue_Ctor_Decimal()
    {
        Decimal decimalValue = Decimal.MinValue;
        DocumentValue DocumentValue = decimalValue;
        Assert.Equal(decimalValue, DocumentValue.RawValue);
        Assert.Equal(DocumentType.Decimal, DocumentValue.Type);
    }

    [Fact]
    public void Test_DocumentValue_Ctor_Guid()
    {
        Guid guidValue = Guid.NewGuid();
        DocumentValue DocumentValue = guidValue;
        Assert.Equal(guidValue, DocumentValue.RawValue);
        Assert.Equal(DocumentType.Guid, DocumentValue.Type);
    }

    [Fact]
    public void Test_DocumentValue_Ctor_String()
    {
        string stringValue = "this is a string";
        DocumentValue DocumentValue = stringValue;
        Assert.Equal(stringValue, DocumentValue.RawValue);
        Assert.Equal(DocumentType.String, DocumentValue.Type);
    }

    [Fact]
    public void Test_DocumentValue_Ctor_Array()
    {
        string[] arrayValue = new string[] { "this is a string", "another string" };
        DocumentValue DocumentValue = new DocumentValue(arrayValue);
        Assert.Equal(arrayValue[0], DocumentValue.AsArray[0]);
        Assert.Equal(arrayValue[1], DocumentValue.AsArray[1]);
        Assert.Equal(DocumentType.Array, DocumentValue.Type);
    }

    [Fact]
    public void Test_DocumentValue_Ctor_Blob()
    {
        byte[] arrayValue = new byte[] { 100, 101, 102, 103, 104, 105 };
        DocumentValue DocumentValue = arrayValue;
        Assert.Equal(arrayValue, DocumentValue.RawValue);
        Assert.Equal(DocumentType.Blob, DocumentValue.Type);
    }

    #endregion

    #region Binary Operations

    [Fact]
    public void Test_DocumentValue_Operator_Add()
    {
        DocumentValue a = (int)4;
        DocumentValue b = (int)3;
        DocumentValue c = a + b;
        Assert.Equal(7, (int)c);
    }

    [Fact]
    public void Test_DocumentValue_Operator_Subtract()
    {
        DocumentValue a = (int)4;
        DocumentValue b = (int)3;
        DocumentValue c = a - b;
        Assert.Equal(1, (int)c);
    }

    [Fact]
    public void Test_DocumentValue_Operator_Multiply()
    {
        DocumentValue a = (int)4;
        DocumentValue b = (int)3;
        DocumentValue c = a * b;
        Assert.Equal(12, (int)c);
    }

    [Fact]
    public void Test_DocumentValue_Operator_Divide()
    {
        DocumentValue a = (int)12;
        DocumentValue b = (int)3;
        DocumentValue c = a / b;
        Assert.Equal(4, (double)c);
    }

    #endregion

    #region Comparison Operators

    [Fact]
    public void Test_DocumentValue_Operator_Equals()
    {
        DocumentValue a = (int)4;
        DocumentValue b = (int)4;
        Assert.True(a == b);
    }

    [Fact]
    public void Test_DocumentValue_Operator_NotEquals()
    {
        DocumentValue a = (int)4;
        DocumentValue b = (int)3;
        Assert.True(a != b);
    }

    [Fact]
    public void Test_DocumentValue_Operator_Greater()
    {
        DocumentValue a = (int)4;
        DocumentValue b = (int)3;
        Assert.True(a > b);
    }

    [Fact]
    public void Test_DocumentValue_Operator_GreaterEquals()
    {
        DocumentValue a = (int)3;
        DocumentValue b = (int)3;
        Assert.True(a >= b);
    }

    [Fact]
    public void Test_DocumentValue_Operator_Less()
    {
        DocumentValue a = (int)4;
        DocumentValue b = (int)3;
        Assert.True(b < a);
    }

    [Fact]
    public void Test_DocumentValue_Operator_LessEquals()
    {
        DocumentValue a = (int)3;
        DocumentValue b = (int)3;
        Assert.True(b <= a);
    }

    #endregion

    #region Implicit conversions

    [Fact]
    public void Test_Int_To_DocumentValue()
    {
        int a = 123;
        DocumentValue doc = a;

        Assert.NotNull(doc);
        if (doc is not null)
        {
            Assert.Equal(a, doc.AsInt32);
        }
    }

    [Fact]
    public void Test_DocumentValue_To_Int()
    {
        DocumentValue doc = new DocumentValue(123);
        int a = doc;

        Assert.Equal(doc.AsInt32, a);
    }

    #endregion
}