# Dbarone.Net.Document
A .NET document library offering the following services:
1. A document model based on the document model in [LiteDB](https://github.com/mbdavid/LiteDB/tree/master/LiteDB)
2. A serialisation / data interchange format, including automatic compression of the serialised data
3. A query language to manipulate documents

This library is being used for another of my projects, [Dbarone.Net.Database](https://github.com/davidbarone/Dbarone.Net.Database), and has been heavily influenced by the [LiteDB](https://github.com/mbdavid/LiteDB/tree/master/LiteDB) project.

## Library Reference
For a full API reference of this library, please refer to the [documentation](https://github.com/davidbarone/Dbarone.Net.Document/blob/main/Documentation.md).

## Document Model
At the core of this library is the DocumentValue type. This represents a value. Values can be simple / native types, or complex types:

### Simple / Native Types
The following native types are supported:

| DocType  | Description                                    |
| -------- | ---------------------------------------------- |
| Null     | Special type representing null values          |
| Boolean  | Boolean type                                   |
| Byte     | Single byte                                    |
| SByte    | Signed byte                                    |
| Char     | Single unicode UTF-16 code point               |
| Decimal  | A 16-byte floating point numeric type          |
| Double   | An 8-byte floating point numeric type          |
| Single   | A 4-byte floating point numeric type           |
| VarInt   | A variable-length integer                      |
| Int16    | A signed 16-bit integer                        |
| UInt16   | An unsigned 16-bit integer                     |
| Int32    | A signed 32-bit integer                        |
| UInt32   | An unsigned 32-bit integer                     |
| Int64    | A signed 64-bit integer                        |
| UInt64   | An unsigned 64-bit integer                     |
| DateTime | A date/time structure                          |
| Guid     | Represents a globally unique identifier (GUID) |
| Blob     | A variable-length byte-array                   |
| String   | A variable-length string                       |

### Complex Types

The following complex types are supported:

| DocType  | Description                                                     |
| -------- | --------------------------------------------------------------- |
| Array    | An array or collection of values. Supports indexing of elements |
| Document | An associative array of key / value elements                    |

### Creating Documents

Simple documents can be created by simply assigning the appropriate value to a new `DocumentValue` variable, for example:

``` c#
    DocumentValue doc = "foobar"; // doc.Type = DocumentType.String
    DocumentValue doc = (Int32)123; // doc.Type = DocumentType.Int32
    DocumentValue doc = DateTime.Now;   // doc.Type = DocumentType.DateTime 
```

Arrays can be created using the DocumentArray class:

``` c#
    int[] arr = new int[] { 1, 2, 3, 4, 5 };
    DocumentArray docArr = new DocumentArray(arr.Select(a=>(DocumentValue)a));  // doc.Type = DocumentType.Array 
```

Objects can be modelled using the `DictionaryDocument` class:

``` c#
    DictionaryDocument dictDoc = new DictionaryDocument();  // doc.Type = DocumentType.Document
    dictDoc["foo"] = 123;
    dictDoc["bar"] = DateTime.Now;
```

## Serialisation

Documents can be serialised to / deserialised from byte arrays. The `IDocumentSerializer` interface defines serialisation operations.

Multiple serialisation methods are supported based on whether the document has a predefined schema, or is a no-schema document.

### Document Schema

A document schema is a set of rules that define the structure of a document. Schema rules include:
- The permitted data type of a value (one of the above native or complex types)
- Whether a null value is permitted
- The permitted document key values with their associated data types
- Whether an array of elements is permitted, with the optional data type of each element

There are 2 classes used to define schemas: `SchemaElement` and `SchemaAttribute`.

### Schema-Defined Document

Documents can be serialised with a predefined document schema. If a schema is defined the document is validated against the schema. In addition, a serialisation optimisation is employed to map document key names with an index value. This index value is the AttributeId of the corresponding `SchemaAttribute` rule found in the provided schema. This optimisation reduces the overall serialised data size as object key values are encoded as numbers instead of serialising the entire key name for each row / object.