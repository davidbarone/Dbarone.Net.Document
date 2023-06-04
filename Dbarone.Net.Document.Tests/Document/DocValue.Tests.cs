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
        // Int32
        UInt32 uInt32Value = 123;
        DocValue docValue = uInt32Value;
        Assert.Equal(uInt32Value, docValue.RawValue);
        Assert.Equal(DocType.UInt32, docValue.Type);
    }

}