namespace Dbarone.Net.Document.Tests;
using Xunit;
using Dbarone.Net.Document;
using System;

public class SerialTypeTests
{
    [Theory]
    [InlineData(DocType.Null, null, 0)]
    [InlineData(DocType.Boolean, null, 1)]
    [InlineData(DocType.Byte, null, 2)]
    [InlineData(DocType.SByte, null, 3)]
    [InlineData(DocType.Char, null, 4)]
    [InlineData(DocType.Decimal, null, 5)]
    [InlineData(DocType.Double, null, 6)]
    [InlineData(DocType.Single, null, 7)]
    [InlineData(DocType.Int16, null, 8)]
    [InlineData(DocType.UInt16, null, 9)]
    [InlineData(DocType.Int32, null, 10)]
    [InlineData(DocType.UInt32, null, 11)]
    [InlineData(DocType.Int64, null, 12)]
    [InlineData(DocType.UInt64, null, 13)]
    [InlineData(DocType.DateTime, null, 14)]
    [InlineData(DocType.Guid, null, 15)]
    [InlineData(DocType.Array, 10, 70)]
    [InlineData(DocType.Blob, 10, 71)]
    [InlineData(DocType.String, 10, 72)]
    [InlineData(DocType.Document, 10, 73)]
    [InlineData(DocType.VarInt, 10, 74)]
    public void TestVarInt(DocType docType, int? length, int expected)
    {
        VarInt expectedVarInt = (VarInt)expected;

        // Create VarInt - #1
        var varInt = new SerialType(docType, length);
        Assert.Equal((long)expectedVarInt.Value, (long)varInt.Value);

        // Create VarInt - #2
        varInt = new SerialType(expected);
        Assert.Equal(docType, varInt.DocType);
        Assert.Equal(length, varInt.Length);
    }
}