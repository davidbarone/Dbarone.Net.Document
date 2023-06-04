namespace Dbarone.Net.Document.Tests;
using Xunit;
using Dbarone.Net.Document;
using System;

public class DocTests
{
    [Fact]
    public void Test_DocValueCtor()
    {
        // Int32
        var int32Value = 123;
        DocValue docValue = int32Value;
        Assert.Equal(int32Value, docValue.RawValue);
        Assert.Equal(DocType.Int32, docValue.Type);
    }
}