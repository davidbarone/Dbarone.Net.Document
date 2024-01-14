# Dbarone.Net.Document
A .NET library for data serialisation. Provides a .NET specific data interchange format.

This library will be used for another of my projects, Dbarone.Net.Database, and has been heavily influenced by the https://github.com/mbdavid/LiteDB project.

Dbarone.Net.Document is a document library offering the following services:
- Document data structure allows for representing data structures as associative arrays (name value pairs or Dictionaries)
- Serialisation and deserialisation

For a full reference of this library, please refer to the [documentation](https://github.com/davidbarone/Dbarone.Net.Document/blob/main/Documentation.md).

## DocValue

The DocValue class represents a single value. This includes scalar values, and complex data types like arrays and documents. The following types are supported

| DocType  | Description                                    |
| -------- | ---------------------------------------------- |
| Null     | Special type representing null values          |
| Boolean  | Boolean type                                   |
| Byte     | Single byte                                    |
| SByte    | Signed byte                                    |
| Char     | Single unicode UTF-16 character                |
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
| Array    | An array or collection of values               |
| Blob     | A variable-length byte-array                   |
| String   | A variable-length string                       |
| Document | A document with key/value pairs                |

