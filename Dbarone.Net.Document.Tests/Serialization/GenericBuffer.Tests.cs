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

}