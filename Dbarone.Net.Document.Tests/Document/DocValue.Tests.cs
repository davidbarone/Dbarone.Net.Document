namespace Dbarone.Net.Document.Tests;
using Xunit;
using Dbarone.Net.Document;
using System;

public class DocTests
{
    #region Ctor tests

    [Fact]
    public void Test_ValueDocument_Ctor_Null()
    {
        ValueDocument ValueDocument = new ValueDocument();
        Assert.Equal(null, ValueDocument.RawValue);
        Assert.Equal(DocumentType.Null, ValueDocument.Type);
    }

    [Fact]
    public void Test_ValueDocument_Ctor_Boolean()
    {
        Boolean boolValue = true;
        ValueDocument ValueDocument = boolValue;
        Assert.Equal(boolValue, ValueDocument.RawValue);
        Assert.Equal(DocumentType.Boolean, ValueDocument.Type);
    }

    [Fact]
    public void Test_ValueDocument_Ctor_Byte()
    {
        Byte byteValue = Byte.MaxValue;
        ValueDocument ValueDocument = byteValue;
        Assert.Equal(byteValue, ValueDocument.RawValue);
        Assert.Equal(DocumentType.Byte, ValueDocument.Type);
    }

    [Fact]
    public void Test_ValueDocument_Ctor_SByte()
    {
        SByte sByteValue = 123;
        ValueDocument ValueDocument = sByteValue;
        Assert.Equal(sByteValue, ValueDocument.RawValue);
        Assert.Equal(DocumentType.SByte, ValueDocument.Type);
    }

    [Fact]
    public void Test_ValueDocument_Ctor_Char()
    {
        Char charValue = 'a';
        ValueDocument ValueDocument = charValue;
        Assert.Equal(charValue, ValueDocument.RawValue);
        Assert.Equal(DocumentType.Char, ValueDocument.Type);
    }

    [Fact]
    public void Test_ValueDocument_Ctor_Int16()
    {
        Int16 int16Value = -123;
        ValueDocument ValueDocument = int16Value;
        Assert.Equal(int16Value, ValueDocument.RawValue);
        Assert.Equal(DocumentType.Int16, ValueDocument.Type);
    }

    [Fact]
    public void Test_ValueDocument_Ctor_UInt16()
    {
        UInt16 uInt32Value = 123;
        ValueDocument ValueDocument = uInt32Value;
        Assert.Equal(uInt32Value, ValueDocument.RawValue);
        Assert.Equal(DocumentType.UInt16, ValueDocument.Type);
    }

    [Fact]
    public void Test_ValueDocument_Ctor_Int32()
    {
        Int32 int32Value = -123;
        ValueDocument ValueDocument = int32Value;
        Assert.Equal(int32Value, ValueDocument.RawValue);
        Assert.Equal(DocumentType.Int32, ValueDocument.Type);
    }

    [Fact]
    public void Test_ValueDocument_Ctor_UInt32()
    {
        UInt32 uInt32Value = 123;
        ValueDocument ValueDocument = uInt32Value;
        Assert.Equal(uInt32Value, ValueDocument.RawValue);
        Assert.Equal(DocumentType.UInt32, ValueDocument.Type);
    }

    [Fact]
    public void Test_ValueDocument_Ctor_Single()
    {
        Single singleValue = -123.45f;
        ValueDocument ValueDocument = singleValue;
        Assert.Equal(singleValue, ValueDocument.RawValue);
        Assert.Equal(DocumentType.Single, ValueDocument.Type);
    }

    [Fact]
    public void Test_ValueDocument_Ctor_Int64()
    {
        Int64 int64Value = Int64.MinValue;
        ValueDocument ValueDocument = int64Value;
        Assert.Equal(int64Value, ValueDocument.RawValue);
        Assert.Equal(DocumentType.Int64, ValueDocument.Type);
    }

    [Fact]
    public void Test_ValueDocument_Ctor_UInt64()
    {
        UInt64 uInt64Value = UInt64.MaxValue;
        ValueDocument ValueDocument = uInt64Value;
        Assert.Equal(uInt64Value, ValueDocument.RawValue);
        Assert.Equal(DocumentType.UInt64, ValueDocument.Type);
    }

    [Fact]
    public void Test_ValueDocument_Ctor_DateTime()
    {
        DateTime datetimeValue = DateTime.Now;
        ValueDocument ValueDocument = datetimeValue;
        Assert.Equal(datetimeValue, ValueDocument.RawValue);
        Assert.Equal(DocumentType.DateTime, ValueDocument.Type);
    }

    [Fact]
    public void Test_ValueDocument_Ctor_Double()
    {
        Double doubleValue = Double.MinValue;
        ValueDocument ValueDocument = doubleValue;
        Assert.Equal(doubleValue, ValueDocument.RawValue);
        Assert.Equal(DocumentType.Double, ValueDocument.Type);
    }

    [Fact]
    public void Test_ValueDocument_Ctor_Decimal()
    {
        Decimal decimalValue = Decimal.MinValue;
        ValueDocument ValueDocument = decimalValue;
        Assert.Equal(decimalValue, ValueDocument.RawValue);
        Assert.Equal(DocumentType.Decimal, ValueDocument.Type);
    }

    [Fact]
    public void Test_ValueDocument_Ctor_Guid()
    {
        Guid guidValue = Guid.NewGuid();
        ValueDocument ValueDocument = guidValue;
        Assert.Equal(guidValue, ValueDocument.RawValue);
        Assert.Equal(DocumentType.Guid, ValueDocument.Type);
    }

    [Fact]
    public void Test_ValueDocument_Ctor_String()
    {
        string stringValue = "this is a string";
        ValueDocument ValueDocument = stringValue;
        Assert.Equal(stringValue, ValueDocument.RawValue);
        Assert.Equal(DocumentType.String, ValueDocument.Type);
    }

    [Fact]
    public void Test_ValueDocument_Ctor_Array()
    {
        string[] arrayValue = new string[] { "this is a string", "another string" };
        ValueDocument ValueDocument = new ValueDocument(arrayValue);
        Assert.Equal(arrayValue[0], ValueDocument.AsArray[0]);
        Assert.Equal(arrayValue[1], ValueDocument.AsArray[1]);
        Assert.Equal(DocumentType.Array, ValueDocument.Type);
    }

    [Fact]
    public void Test_ValueDocument_Ctor_Blob()
    {
        byte[] arrayValue = new byte[] { 100, 101, 102, 103, 104, 105 };
        ValueDocument ValueDocument = arrayValue;
        Assert.Equal(arrayValue, ValueDocument.RawValue);
        Assert.Equal(DocumentType.Blob, ValueDocument.Type);
    }

    #endregion

    #region Binary Operations

    [Fact]
    public void Test_ValueDocument_Operator_Add()
    {
        ValueDocument a = (int)4;
        ValueDocument b = (int)3;
        ValueDocument c = a + b;
        Assert.Equal(7, (int)c);
    }

    [Fact]
    public void Test_ValueDocument_Operator_Subtract()
    {
        ValueDocument a = (int)4;
        ValueDocument b = (int)3;
        ValueDocument c = a - b;
        Assert.Equal(1, (int)c);
    }

    [Fact]
    public void Test_ValueDocument_Operator_Multiply()
    {
        ValueDocument a = (int)4;
        ValueDocument b = (int)3;
        ValueDocument c = a * b;
        Assert.Equal(12, (int)c);
    }

    [Fact]
    public void Test_ValueDocument_Operator_Divide()
    {
        ValueDocument a = (int)12;
        ValueDocument b = (int)3;
        ValueDocument c = a / b;
        Assert.Equal(4, (double)c);
    }

    #endregion

    #region Comparison Operators

    [Fact]
    public void Test_ValueDocument_Operator_Equals()
    {
        ValueDocument a = (int)4;
        ValueDocument b = (int)4;
        Assert.True(a == b);
    }

    [Fact]
    public void Test_ValueDocument_Operator_NotEquals()
    {
        ValueDocument a = (int)4;
        ValueDocument b = (int)3;
        Assert.True(a != b);
    }

    [Fact]
    public void Test_ValueDocument_Operator_Greater()
    {
        ValueDocument a = (int)4;
        ValueDocument b = (int)3;
        Assert.True(a > b);
    }

    [Fact]
    public void Test_ValueDocument_Operator_GreaterEquals()
    {
        ValueDocument a = (int)3;
        ValueDocument b = (int)3;
        Assert.True(a >= b);
    }

    [Fact]
    public void Test_ValueDocument_Operator_Less()
    {
        ValueDocument a = (int)4;
        ValueDocument b = (int)3;
        Assert.True(b < a);
    }

    [Fact]
    public void Test_ValueDocument_Operator_LessEquals()
    {
        ValueDocument a = (int)3;
        ValueDocument b = (int)3;
        Assert.True(b <= a);
    }

    #endregion

    #region Implicit conversions

    [Fact]
    public void Test_Int_To_ValueDocument(){
        int a = 123;
        ValueDocument doc = a;

        Assert.NotNull(doc);
        if (doc is not null)
        {
            Assert.Equal(a, doc.AsInt32);
        }
    }

    [Fact]
    public void Test_ValueDocument_To_Int(){
        ValueDocument doc = new ValueDocument(123);
        int a = doc;

        Assert.Equal(doc.AsInt32, a);
    }

    #endregion
}