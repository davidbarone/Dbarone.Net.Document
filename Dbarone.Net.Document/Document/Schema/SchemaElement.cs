using System.Runtime.InteropServices;
using System.Xml.Schema;
using Dbarone.Net.Document;

/// <summary>
/// Represents an element in a document schema definition.
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
    public SchemaElement(DocumentType documentType, bool allowNull, SchemaElement? element = null, IEnumerable<SchemaAttribute>? attributes = null)
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
        IEnumerable<SchemaAttribute>? attributes = null;
        if (dictionaryDocument["Attributes"] is not null)
        {
            attributes = dictionaryDocument["Attributes"].AsArray.Select(a => new SchemaAttribute(a.AsDocument));
        }
        this.DocumentType = (DocumentType)dictionaryDocument["DocumentType"].AsInt32;
        this.AllowNull = dictionaryDocument["AllowNull"].AsBoolean;
        this.Element = new SchemaElement(dictionaryDocument["Element"].AsDocument);
        this.Attributes = attributes;
    }

    /// <summary>
    /// Converts the current schema element to a <see cref="DictionaryDocument"/> instance.
    /// </summary>
    /// <returns></returns>
    public DictionaryDocument ToDictionaryDocument()
    {
        if (this.DocumentType == DocumentType.Array)
        {
            if (Element is null)
            {
                throw new Exception("Element cannot be null for DocumentType.Array.");
            }
            DictionaryDocument dd = new DictionaryDocument();
            dd["DocumentType"] = new DocumentValue(DocumentType);
            dd["AllowNull"] = new DocumentValue(AllowNull);
            dd["Element"] = Element.ToDictionaryDocument();
            return dd;
        }
        else if (this.DocumentType == DocumentType.Document)
        {
            if (Attributes is null)
            {
                throw new Exception("Attributes cannot be null for DocumentType.Document");
            }
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

    /// <summary>
    /// Validates a document using the current schema.
    /// </summary>
    /// <param name="document"></param>
    /// <returns></returns>
    public bool Validate(DocumentValue document)
    {
        if (!(this.DocumentType == document.Type || (this.AllowNull && document.IsNull)))
        {
            throw new Exception("Validation error: Invalid type.");
        }

        // Array?
        if (this.DocumentType == DocumentType.Array)
        {
            if (this.Element == null)
            {
                throw new Exception("Array validation: Element is null.");
            }
            foreach (var item in document.AsArray)
            {
                this.Element.Validate(item);
            }
        }

        // Document?
        if (this.DocumentType == DocumentType.Document)
        {
            if (this.Attributes == null)
            {
                throw new Exception("Document validation: Attributes are null.");
            }

            var schemaAttributes = this.Attributes.Select(a => a.AttributeName);
            var dd = document.AsDocument;
            var documentAttributes = dd.Keys;
            
            // attributes can be missing in document if the schema AllowNull is set.
            var attributesMissingInDocument = schemaAttributes
                .Except(documentAttributes)
                .Where(
                        a => this.Attributes.First(f => f.AttributeName.Equals(a, StringComparison.Ordinal)).Element.AllowNull
                );
            var attributesMissingInSchema = documentAttributes.Except(schemaAttributes);

            if (attributesMissingInDocument.Any()){
                throw new Exception($"Attribute {attributesMissingInDocument.First()} is not defined in the document.");
            }

            if (attributesMissingInSchema.Any()){
                throw new Exception($"Attribute {attributesMissingInSchema.First()} is not defined in the schema.");
            }

            var validAttributes = schemaAttributes.Intersect(documentAttributes);

            // validate attributes
            foreach (var attribute in validAttributes) {
                this.Attributes.First(a => a.AttributeName.Equals(attribute, StringComparison.Ordinal)).Element.Validate(dd[attribute]);
            }
        }

        // If get here, then all good.
        return true;
    }
}
