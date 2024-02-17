namespace Dbarone.Net.Document;


/// <summary>
/// Represents an attribute node in a document schema definition.
/// </summary>
public class SchemaAttribute
{
    /// <summary>
    /// The attribute id. Attribute ids must NOT be changed on a type, as they are used to encode objects during the serialisation process.
    /// </summary>
    public int AttributeId { get; set; } = default!;

    /// <summary>
    /// The attribute name.
    /// </summary>
    public string AttributeName { get; set; } = default!;

    /// <summary>
    /// The definition of the attribute.
    /// </summary>
    public SchemaElement Element { get; set; } = default!;

    public SchemaAttribute(int attributeId, string attributeName, SchemaElement element)
    {
        this.AttributeId = attributeId;
        this.AttributeName = attributeName;
        this.Element = element;
    }
    /// <summary>
    /// Creates a new SchemaAttribute from a <see cref="DictionaryDocument"/> instance.
    /// </summary>
    /// <param name="dictionaryDocument"></param>
    public SchemaAttribute(DictionaryDocument dictionaryDocument)
    {
        this.AttributeId = dictionaryDocument["AttributeId"].AsInt32;
        this.AttributeName = dictionaryDocument["AttributeName"].AsString;
        this.Element = new SchemaElement(dictionaryDocument["Element"].AsDocument);
    }

    /// <summary>
    /// Converts the current SchemaAttribute to a <see cref="DictionaryDocument"/> instance.
    /// </summary>
    /// <returns></returns>
    public DictionaryDocument ToDictionaryDocument()
    {
        DictionaryDocument dd = new DictionaryDocument();
        dd["AttributeId"] = new DocumentValue(AttributeId);
        dd["AttributeName"] = new DocumentValue(AttributeName);
        dd["Element"] = (DocumentValue)Element.ToDictionaryDocument();
        return dd;
    }
}