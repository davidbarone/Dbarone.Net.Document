namespace Dbarone.Net.Document.Tests;
using Xunit;
using Dbarone.Net.Document;
using System;

public class DocTests
{
    [Fact]
    public void Test_DocValue_Ctor_Null()
    {
        DocValue docValue = new DocValue();
        Assert.Equal(null, docValue.RawValue);
        Assert.Equal(DocType.Null, docValue.Type);
    }

    [Fact]
    public void Test_DocValue_Ctor_Boolean()
    {
        Boolean boolValue = true;
        DocValue docValue = boolValue;
        Assert.Equal(boolValue, docValue.RawValue);
        Assert.Equal(DocType.Boolean, docValue.Type);
    }

    [Fact]
    public void Test_DocValue_Ctor_Byte()
    {
        Byte byteValue = Byte.MaxValue;
        DocValue docValue = byteValue;
        Assert.Equal(byteValue, docValue.RawValue);
        Assert.Equal(DocType.Byte, docValue.Type);
    }

    [Fact]
    public void Test_DocValue_Ctor_SByte()
    {
        SByte sByteValue = 123;
        DocValue docValue = sByteValue;
        Assert.Equal(sByteValue, docValue.RawValue);
        Assert.Equal(DocType.SByte, docValue.Type);
    }

    [Fact]
    public void Test_DocValue_Ctor_Char()
    {
        Char charValue = 'a';
        DocValue docValue = charValue;
        Assert.Equal(charValue, docValue.RawValue);
        Assert.Equal(DocType.Char, docValue.Type);
    }

    [Fact]
    public void Test_DocValue_Ctor_Int16()
    {
        Int16 int16Value = -123;
        DocValue docValue = int16Value;
        Assert.Equal(int16Value, docValue.RawValue);
        Assert.Equal(DocType.Int16, docValue.Type);
    }

    [Fact]
    public void Test_DocValue_Ctor_UInt16()
    {
        UInt16 uInt32Value = 123;
        DocValue docValue = uInt32Value;
        Assert.Equal(uInt32Value, docValue.RawValue);
        Assert.Equal(DocType.UInt16, docValue.Type);
    }

    [Fact]
    public void Test_DocValue_Ctor_Int32()
    {
        Int32 int32Value = -123;
        DocValue docValue = int32Value;
        Assert.Equal(int32Value, docValue.RawValue);
        Assert.Equal(DocType.Int32, docValue.Type);
    }

    [Fact]
    public void Test_DocValue_Ctor_UInt32()
    {
        UInt32 uInt32Value = 123;
        DocValue docValue = uInt32Value;
        Assert.Equal(uInt32Value, docValue.RawValue);
        Assert.Equal(DocType.UInt32, docValue.Type);
    }

    [Fact]
    public void Test_DocValue_Ctor_Single()
    {
        Single singleValue = -123.45f;
        DocValue docValue = singleValue;
        Assert.Equal(singleValue, docValue.RawValue);
        Assert.Equal(DocType.Single, docValue.Type);
    }

    [Fact]
    public void Test_DocValue_Ctor_Int64()
    {
        Int64 int64Value = Int64.MinValue;
        DocValue docValue = int64Value;
        Assert.Equal(int64Value, docValue.RawValue);
        Assert.Equal(DocType.Int64, docValue.Type);
    }

    [Fact]
    public void Test_DocValue_Ctor_UInt64()
    {
        UInt64 uInt64Value = UInt64.MaxValue;
        DocValue docValue = uInt64Value;
        Assert.Equal(uInt64Value, docValue.RawValue);
        Assert.Equal(DocType.UInt64, docValue.Type);
    }

    [Fact]
    public void Test_DocValue_Ctor_DateTime()
    {
        DateTime datetimeValue = DateTime.Now;
        DocValue docValue = datetimeValue;
        Assert.Equal(datetimeValue, docValue.RawValue);
        Assert.Equal(DocType.DateTime, docValue.Type);
    }

    [Fact]
    public void Test_DocValue_Ctor_Double()
    {
        Double doubleValue = Double.MinValue;
        DocValue docValue = doubleValue;
        Assert.Equal(doubleValue, docValue.RawValue);
        Assert.Equal(DocType.Double, docValue.Type);
    }

    [Fact]
    public void Test_DocValue_Ctor_Decimal()
    {
        Decimal decimalValue = Decimal.MinValue;
        DocValue docValue = decimalValue;
        Assert.Equal(decimalValue, docValue.RawValue);
        Assert.Equal(DocType.Decimal, docValue.Type);
    }

    [Fact]
    public void Test_DocValue_Ctor_Guid()
    {
        Guid guidValue = Guid.NewGuid();
        DocValue docValue = guidValue;
        Assert.Equal(guidValue, docValue.RawValue);
        Assert.Equal(DocType.Guid, docValue.Type);
    }

    [Fact]
    public void Test_DocValue_Ctor_String()
    {
        string stringValue = "this is a string";
        DocValue docValue = stringValue;
        Assert.Equal(stringValue, docValue.RawValue);
        Assert.Equal(DocType.String, docValue.Type);
    }

    [Fact]
    public void Test_DocValue_Ctor_Array()
    {
        string[] arrayValue = new string[] { "this is a string", "another string" };
        DocValue docValue = new DocValue(arrayValue);
        Assert.Equal(arrayValue[0], docValue.AsArray[0]);
        Assert.Equal(arrayValue[1], docValue.AsArray[1]);
        Assert.Equal(DocType.Array, docValue.Type);
    }

    [Fact]
    public void Test_DocValue_Ctor_Blob()
    {
        byte[] arrayValue = new byte[] { 100, 101, 102, 103, 104, 105 };
        DocValue docValue = arrayValue;
        Assert.Equal(arrayValue, docValue.RawValue);
        Assert.Equal(DocType.Blob, docValue.Type);
    }

}