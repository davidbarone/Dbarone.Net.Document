using System.Runtime.InteropServices;
using System.Xml.Schema;
using Dbarone.Net.Document;

/// <summary>
/// Represents an element in a schema definition.
/// </summary>
public class SchemaElement
{
    /// <summary>
    /// Specifies the document type allowed. Only 1 type is allowed.
    /// </summary>
    public DocumentType DocumentType { get; set; }

    /// <summary>
    /// If set to true, then a Document.Null value is permitted.
    /// </summary>
    public bool AllowNull { get; set; } = false;

    /// <summary>
    /// For <see cref="DocumentArray"/> types, specified the child element type. 
    /// </summary>
    public SchemaElement? Element { get; set; } = null;

    /// <summary>
    /// For <see cref="DictionaryDocument" /> types, specify the attributes of the dictionary.
    /// </summary>
    public IEnumerable<SchemaAttribute>? Attributes { get; set; } = null;

    /// <summary>
    /// Creates a new SchemaElement instance.
    /// </summary>
    /// <param name="documentType">The document type.</param>
    /// <param name="allowNull">Set to true if DocumentValue.Null is allowed.</param>
    /// <param name="element">The inner element for DocumentValue.Array types.</param>
    /// <param name="attributes">The inner attributes for DocumentValue.Document types.</param>
    public SchemaElement(DocumentType documentType, bool allowNull, SchemaElement element = null, IEnumerable<SchemaAttribute>? attributes = null)
    {
        this.DocumentType = documentType;
        this.AllowNull = allowNull;
        this.Element = element;
        this.Attributes = attributes;
    }

    /// <summary>
    /// Creates a new SchemaElement instance from a <see cref="DictionaryDocument"/> instance.
    /// </summary>
    /// <param name="dictionaryDocument">The DictionaryDocument instance to create the SchemaElement from.</param>
    public SchemaElement(DictionaryDocument dictionaryDocument)
    {
        IEnumerable<SchemaAttribute> attributes = null;
        if (dictionaryDocument["Attributes"] != null)
        {
            attributes = dictionaryDocument["Attributes"].AsArray.Select(a => new SchemaAttribute(a.AsDocument));
        }
        this.DocumentType = (DocumentType)dictionaryDocument["DocumentType"].AsInt32;
        this.AllowNull = dictionaryDocument["AllowNull"].AsBoolean;
        this.Element = new SchemaElement(dictionaryDocument["Element"].AsDocument);
        this.Attributes = attributes;
    }

    public DictionaryDocument ToDictionaryDocument()
    {
        if (this.DocumentType == DocumentType.Array)
        {
            DictionaryDocument dd = new DictionaryDocument();
            dd["DocumentType"] = new DocumentValue(DocumentType);
            dd["AllowNull"] = new DocumentValue(AllowNull);
            dd["Element"] = Element.ToDictionaryDocument();
            return dd;
        }
        else if (this.DocumentType == DocumentType.Document)
        {
            DictionaryDocument dd = new DictionaryDocument();
            dd["DocumentType"] = new DocumentValue(DocumentType);
            dd["AllowNull"] = new DocumentValue(AllowNull);
            dd["Attributes"] = new DocumentArray(Attributes.Select(a => a.ToDictionaryDocument()));
            return dd;
        }
        else
        {
            return new DictionaryDocument(new Dictionary<string, DocumentValue> {
                {"DocumentType", new DocumentValue(DocumentType)},
                {"AllowNull", new DocumentValue(AllowNull)}
            });
        }
    }
}

public class SchemaAttribute
{
    public int AttributeId { get; set; }
    public string AttributeName { get; set; }
    public SchemaElement Element { get; set; }

    public SchemaAttribute(DictionaryDocument dictionaryDocument)
    {
        this.AttributeId = dictionaryDocument["AttributeId"].AsInt32;
        this.AttributeName = dictionaryDocument["AttributeName"].AsString;
        this.Element = new SchemaElement(dictionaryDocument["Element"].AsDocument);
    }
    public DictionaryDocument ToDictionaryDocument()
    {
        DictionaryDocument dd = new DictionaryDocument();
        dd["AttributeId"] = new DocumentValue(AttributeId);
        dd["AttributeName"] = new DocumentValue(AttributeName);
        dd["Element"] = Element.ToDictionaryDocument();
        return dd;
    }
}