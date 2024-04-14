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
### Document Schema

Documents can be schema-less, meaning that they can take on any arbitrary structure. However, you can impose rules on how a document structure should be. This is done by creating a schema.

A schema is a set of rules that define the structure of a document. Schema rules include:
- The permitted data type of a value (one of the above native or complex types)
- Whether a null value is permitted
- The permitted document key values with their associated data types
- Whether an array of elements is permitted, with the optional data type of each element

There are 2 classes used to define schemas: `SchemaElement` and `SchemaAttribute`.

The example below creates a schema, then validates the schema against 2 documents:

``` c#
    // Document passing schema validation

    SchemaElement schema = new SchemaElement(DocumentType.Document, false, null, new List<SchemaAttribute>{
        new SchemaAttribute(1, "a", new SchemaElement(DocumentType.String, false)),
        new SchemaAttribute(2, "b", new SchemaElement(DocumentType.DateTime, false)),
        new SchemaAttribute(3, "c", new SchemaElement(DocumentType.Int32, false))
    });

    DictionaryDocument dd = new DictionaryDocument();
    dd["a"] = new DocumentValue("foobar");
    dd["b"] = new DocumentValue(DateTime.Now);
    dd["c"] = new DocumentValue((int)123);

    Assert.True(schema.Validate(dd));   // returns 'true'. Document successfully validated.
```

``` c#
    // Document failing schema validation
    
    SchemaElement schema = new SchemaElement(DocumentType.Document, false, null, new List<SchemaAttribute>{
        new SchemaAttribute(1, "a", new SchemaElement(DocumentType.String, false)),
        new SchemaAttribute(2, "b", new SchemaElement(DocumentType.DateTime, false)),
        new SchemaAttribute(3, "c", new SchemaElement(DocumentType.Int32, false))
    });

    DictionaryDocument dd = new DictionaryDocument();
    dd["a"] = new DocumentValue("foobar");
    dd["b"] = new DocumentValue(DateTime.Now);
    dd["c"] = new DocumentValue("baz"); // this should be an Int32!

    Assert.True(schema.Validate(dd));   // throws an exception. Document not validated.
```

## Serialisation

Documents can be serialised to / deserialised from byte arrays. The `IDocumentSerializer` interface defines serialisation operations.

Multiple serialisation methods are supported based on whether the document has a predefined schema, or is a no-schema document.

### Variable Length Integers (VarInt)

Before discussing serialisation in more depth, we need to cover a couple of topics. Firstly is Variable length integers (VarInts). These are a mechanism of storing integers in the least amount of bytes possible. VarInts are used extensively in this project to store things such as data types and data sizes. The use of VarInts allows the serialised data to be compressed - for example, using Int32 values to store sizes of data would require 4 bytes of storage, even for small values. However, using a VarInt value, small values can be cleverly encoded to require 1 byte of storage only. VarInts are also used in systems like SQLite, and you can read more about them [here](https://en.wikipedia.org/wiki/Variable-length_quantity).

### Serial Types

When decoding documents, metadata like the data types and data sizes must be encoded with the data to enable readers to deserialise the data afterwards. The 'Serial Type' encodes the data type and data size of the subsequent serialised data. Serial types are encoded as VarInts to ensure efficient compression when stored in files. The following table describes how the serial type values are calculated:

| Serial Type      | Meaning                                                                                             |
| ---------------- | --------------------------------------------------------------------------------------------------- |
| 0                | Value is NULL                                                                                       |
| 1                | Value is Boolean                                                                                    |
| 2                | Value is Byte                                                                                       |
| 3                | Value is SByte                                                                                      |
| 4                | Value is Char                                                                                       |
| 5                | Value is Decimal                                                                                    |
| 6                | Value is Double                                                                                     |
| 7                | Value is Single                                                                                     |
| 8                | Value is Int16                                                                                      |
| 9                | Value is UInt16                                                                                     |
| 10               | Value is Int32                                                                                      |
| 11               | Value is UInt32                                                                                     |
| 12               | Value is Int64                                                                                      |
| 13               | Value is UInt64                                                                                     |
| 14               | Value is DateTime                                                                                   |
| 15               | Value is Guid                                                                                       |
| N>=20 and N%5==0 | Array. Value is a byte array that is (N-20)/5 bytes long                                            |
| N>=21 and N%5==1 | Blob. Value is a byte array that is (N-21)/5 bytes long.                                            |
| N>=22 and N%5==2 | String. Value is a string that is (N-22)/5 bytes long, stored in the text encoding of the database. |
| N>=23 and N%5==3 | Document. value is a document that is (N-23)/5 bytes long                                           |
| N>=24 and N%5==4 | VarInt. value is (N-24)/5 bytes long                                                                |

### Schema-Defined Document

Documents can be schema-less or schema-bound. This affects how the document is serialized.

Schema-less documents are those without any fixed schema. Schema-less `DictionaryDocument` objects can contain any arbitrary keys and values. This allows for flexible / unstructed data to be stored. When these documents are serialised, the key associated with each value is serialised with the value in much the same fashion as text serialisation protocols like Json or XML. This serialisaton technique, allows for unstructured data to be fully self-describing. However, it is not efficient with regards to data storage.

Alternatively, documents can be serialised with a predefined document schema. If a schema is defined the document is also validated against the schema before being serialised. When schema-bound documents are serialised, the schema is encoded at the start of the serialised output. The schema includes all dictionary key names and types (attributes). Each attribute requires a unique AttributeId to be assigned. The data is serialised after the schema. When serialising the data, the attribute / key names are replaced with the AttributeId which is stored as a VarInt. This results in a much compressed serialised output.

### ColumnStoreDocumentArray

A common document structure is a tabluar model comprised of a 2 dimensional array of rows and columns. This can be modelled using a DocumentArray containing zero or more DictionaryDocument objects. This can be thought of as a 'row based' document. Column storage can be thought of as having all the rows for a particular column adjacent. This is like having a DictionaryDocument, where each element is array with the same number of elements. When tables are stored in columnar format, some additional storage optimisations can be done: 



TO DO
-----

CSV library
byte compression - Huffman
ColumnStore class - dictionary encoding / RLE / Huffman encoding / VarInt encoding


ColumnStore

Run Length Encoding
Dictionary Encoding
Huffman (string / byte) encoding
Delta Encoding???


Only write page once - when get to 1M 2^20 (1,048,576)
No updates - but rows can be deleted? (logical delete)


N Columns, split into M row groups

each M row group = 2"^20 rows

MEtadata
- Location of all column metadata start locations

Column Chunk = 1 column 2^20 rows = multiple pages

https://parquet.apache.org/docs/file-format/
Model: https://parquet.apache.org/docs/file-format/metadata/

https://cloudsqale.com/2020/05/29/how-parquet-files-are-written-row-groups-pages-required-memory-and-flush-operations/

Footer:
- SChema (columns + types
- all row group info (size, rows, min/max/null for each column)


SQL Server
----------
https://sqlespresso.com/2019/06/26/understanding-columnstore-indexes-in-sql-server-part-1/#:~:text=Columnstore%20is%20simply%20the%20way%20the%20data%20is,columns%20and%20logically%20organized%20in%20rows%20and%20columns.

Min row group 102400, max 1M
COlumn segments

Delta group = remainder of rows on b-tree index
Delta store = multiple row groups

Tuple-Mover Process = moves rows from delta store to columnstore index
looks for delta groups > 1M rows (closed group)

