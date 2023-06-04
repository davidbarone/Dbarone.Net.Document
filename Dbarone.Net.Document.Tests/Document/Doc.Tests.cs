namespace Dbarone.Net.Document.Tests;
using Xunit;
using Dbarone.Net.Document;
using System;

public class DocTests
{
    [Fact]
    public void Test_DocValue_Ctor_Null()
    {
        // Int32
        DocValue docValue = new DocValue();
        Assert.Equal(null, docValue.RawValue);
        Assert.Equal(DocType.Null, docValue.Type);
    }

    [Fact]
    public void Test_DocValue_Ctor_Int32()
    {
        // Int32
        var int32Value = -123;
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
        Assert.Equal(DocType.Int32, docValue.Type);
    }

}