namespace Dbarone.Net.Document;

/// A data frame is a specialised type of document. It is bound to a schema, and represents
/// a single array of values, each value being a simple data type (not a nested dictonary or array).
public class DataList {

    SchemaElement Schema { get; set; } = default!;
    DocumentValue Document { get; set; } = default!;

    /// <summary>
    /// Creates a new data list from a document and schema.
    /// </summary>
    /// <param name="document">The document.</param>
    /// <param name="schema">The schema.</param>
    public DataList(DocumentValue document, SchemaElement schema)
    {
        if (!schema.IsListSchema())
        {
            throw new Exception("The schema is not a tabular schema.");
        }

        if (schema.Validate(document))
        {
            this.Document = document;
            this.Schema = schema;
        }
    }

    /// <summary>
    /// Creates a new data frame from a document.
    /// </summary>
    /// <param name="document">The document to create the data frame from.</param>
    public DataList(DocumentValue document) : this(document, SchemaElement.Parse(document)) { }
}