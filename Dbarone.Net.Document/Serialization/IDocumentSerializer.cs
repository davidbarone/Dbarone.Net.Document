namespace Dbarone.Net.Document;

/// <summary>
/// Interface for serialising and deserialising DocumentValue objects.
/// </summary>
public interface IDocumentSerializer {
    
    /// <summary>
    /// Serialise a <see cref="DocumentValue"/> instance. 
    /// </summary>
    /// <param name="docValue">The instance to serialise.</param>
    /// <param name="textEncoding">The optional text encoding to use.</param>
    /// <returns>Returns a byte array.</returns>
    byte[] Serialize(DocumentValue docValue, TextEncoding textEncoding = TextEncoding.UTF8);

    /// <summary>
    /// Deserialise an object to a <see cref="DocumentValue"/> instance.
    /// </summary>
    /// <param name="buffer">The buffer / byte array</param>
    /// <param name="textEncoding">Optional text encoding to use.</param>
    /// <returns>Returns a DocumentValue instance.</returns>
    DocumentValue Deserialize(byte[] buffer, TextEncoding textEncoding = TextEncoding.UTF8);
}