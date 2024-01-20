namespace Dbarone.Net.Document.Tests;
using Xunit;
using Dbarone.Net.Document;
using System;

public class SerialTypeTests
{
    [Theory]
    [InlineData(DocumentType.Null, null, 0)]
    [InlineData(DocumentType.Boolean, null, 1)]
    [InlineData(DocumentType.Byte, null, 2)]
    [InlineData(DocumentType.SByte, null, 3)]
    [InlineData(DocumentType.Char, null, 4)]
    [InlineData(DocumentType.Decimal, null, 5)]
    [InlineData(DocumentType.Double, null, 6)]
    [InlineData(DocumentType.Single, null, 7)]
    [InlineData(DocumentType.Int16, null, 8)]
    [InlineData(DocumentType.UInt16, null, 9)]
    [InlineData(DocumentType.Int32, null, 10)]
    [InlineData(DocumentType.UInt32, null, 11)]
    [InlineData(DocumentType.Int64, null, 12)]
    [InlineData(DocumentType.UInt64, null, 13)]
    [InlineData(DocumentType.DateTime, null, 14)]
    [InlineData(DocumentType.Guid, null, 15)]
    [InlineData(DocumentType.Array, 10, 70)]
    [InlineData(DocumentType.Blob, 10, 71)]
    [InlineData(DocumentType.String, 10, 72)]
    [InlineData(DocumentType.Document, 10, 73)]
    [InlineData(DocumentType.VarInt, 10, 74)]
    public void TestVarInt(DocumentType DocumentType, int? length, int expected)
    {
        VarInt expectedVarInt = (VarInt)expected;

        // Create VarInt - #1
        var varInt = new SerialType(DocumentType, length);
        Assert.Equal((long)expectedVarInt.Value, (long)varInt.Value);

        // Create VarInt - #2
        varInt = new SerialType(expected);
        Assert.Equal(DocumentType, varInt.DocumentType);
        Assert.Equal(length, varInt.Length);
    }
}