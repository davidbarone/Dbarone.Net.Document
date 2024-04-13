namespace Dbarone.Net.Document;

/// <summary>
/// A data frame is a specialised type of document. It is bound to a schema, and represents
/// a 2-dimensional tabular dataset where each row consists of 2 or more values, each being
/// a simple data type (not a nested dictonary or array).
/// </summary>
public class DataFrame
{
    SchemaElement Schema { get; set; }
    DocumentValue Document { get; set; }

    /// <summary>
    /// Creates a new data frame from a document and schema.
    /// </summary>
    /// <param name="document">The document.</param>
    /// <param name="schema">The schema.</param>
    public DataFrame(DocumentValue document, SchemaElement schema)
    {
        if (!schema.IsTabularSchema())
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
    public DataFrame(DocumentValue document) : this(document, SchemaElement.Parse(document)) { }

}