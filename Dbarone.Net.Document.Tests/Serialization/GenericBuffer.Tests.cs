namespace Dbarone.Net.Document;

public class GenericBufferTests
{

    [Fact]
    public void TestCreateResizableBuffer()
    {
        var buf = new GenericBuffer();
        Assert.Equal(0, buf.Position);
        Assert.Equal(0, buf.Length);
    }

    [Fact]
    public void TestResizableBuffer_Write()
    {
        var buf = new GenericBuffer();
        buf.Write((int)123);
        Assert.Equal(sizeof(Int32), buf.Position);
        Assert.Equal(sizeof(Int32), buf.Length);
    }

    [Fact]
    public void TestResizableBuffer_SetPosition()
    {
        var buf = new GenericBuffer();
        buf.Write((int)123);
        buf.Position = 100;
        Assert.Equal(100, buf.Position);
        buf.Write((int)123);
        Assert.Equal(100 + sizeof(Int32), buf.Position);
        Assert.Equal(100 + sizeof(Int32), buf.Length);
    }

    [Fact]
    public void TestResizableBuffer_WriteRead()
    {
        var buf = new GenericBuffer();
        buf.Write((int)123);
        Assert.Equal(sizeof(Int32), buf.Position);
        Assert.Equal(sizeof(Int32), buf.Length);
        
        // reset position + read int
        buf.Position = 0;
        var actual = buf.ReadInt32();

        Assert.Equal(123, actual);
    }

    [Theory]
    [InlineData(DocumentType.Boolean, true)]
    [InlineData(DocumentType.Byte, (Byte)123)]
    [InlineData(DocumentType.SByte, (SByte)123)]
    [InlineData(DocumentType.Char, 'a')]
    [InlineData(DocumentType.Int16, Int16.MaxValue)]
    [InlineData(DocumentType.UInt16, UInt16.MaxValue)]
    [InlineData(DocumentType.Int32, Int32.MaxValue)]
    [InlineData(DocumentType.UInt32, UInt32.MaxValue)]
    [InlineData(DocumentType.Int64, Int64.MaxValue)]
    [InlineData(DocumentType.UInt64, UInt64.MaxValue)]
    [InlineData(DocumentType.Single, Single.MaxValue)]
    [InlineData(DocumentType.Double, Double.MaxValue)]
    public void WriteTests(DocumentType docType, object expectedValue){
       var buf = new GenericBuffer();
        buf.Write(expectedValue);
        buf.Position = 0;
        var actualValue = buf.Read(docType);
        Assert.Equal(expectedValue, actualValue);
    }

    [Fact]
    public void WriteDecimalTest()
    {
        // Arrange
        var value = (decimal)123.45;
        var buf = new GenericBuffer();
        buf.Write(value);

        // Act
        buf.Position = 0;
        var actual = buf.Read(DocumentType.Decimal);

        // Assert
        Assert.Equal(value, actual);
    }

    [Fact]
    public void WriteDateTimeTest()
    {
        // Arrange
        var value = DateTime.Now;
        var buf = new GenericBuffer();
        buf.Write(value);

        // Act
        buf.Position = 0;
        var actual = buf.Read(DocumentType.DateTime);

        // Assert
        Assert.Equal(value, actual);
    }

    [Fact]
    public void TestBuffer_WriteGuid()
    {
        // Arrange
        var value = Guid.NewGuid();
        var buf = new GenericBuffer();
        buf.Write(value);

        // Act
        buf.Position = 0;
        var actual = buf.Read(DocumentType.Guid);

        // Assert
        Assert.Equal(value, actual);
    }

    [Fact]
    public void TestBuffer_WriteString()
    {
        // Arrange
        var value = "foo bar";
        var buf = new GenericBuffer();
        var length = buf.Write(value);

        // Act
        buf.Position = 0;
        var actual = buf.Read(DocumentType.String, length);

        // Assert
        Assert.Equal(value, actual);
    }

    [Fact]
    public void TestBuffer_WriteStringUnicode()
    {
        // Arrange
        var value = "Þð©á";
        var buf = new GenericBuffer();
        var length = buf.Write(value);

        // Act
        buf.Position = 0;
        var actual = buf.Read(DocumentType.String, length);

        // Assert
        Assert.Equal(value, actual);
    }

    [Fact]
    public void TestBuffer_WriteBlob()
    {
        // Arrange
        var value = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
        var buf = new GenericBuffer();
        buf.Write(value);

        // Act
        buf.Position = 0;
        var actual = buf.Read(DocumentType.Blob, 8);

        // Assert
        Assert.Equal(value, actual);
    }
}