using Dbarone.Net.Document;

/// <summary>
/// Column-oriented document
/// </summary>
public class ColumnStoreDocument {

    Dictionary<DocumentType, SortedList<object, int>> dictionaryValues = new Dictionary<DocumentType, SortedList<object, int>>();

    /// <summary>
    /// Creates a new column store object based on a schema-bound document array
    /// </summary>
    /// <param name="documentArray"></param>
    /// <param name="schema"></param>
    public ColumnStoreDocument(DocumentArray documentArray, SchemaElement schema) {
        if (schema is null){
            throw new Exception("Schema required to support column store encoding.");
        }
        if (!schema.IsTabularSchema()) {
            throw new Exception("Schema does not support column store encoding.");
        }
        

    }
}