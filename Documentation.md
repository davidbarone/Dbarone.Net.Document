<a id='top'></a>
# Assembly: Dbarone.Net.Document
## Contents
- [DictionaryDocument](#dbaronenetdocumentdictionarydocument)
  - [#ctor](#dbaronenetdocumentdictionarydocument#ctor)
  - [#ctor](#dbaronenetdocumentdictionarydocument#ctor(systemcollectionsconcurrentconcurrentdictionary{systemstring,dbaronenetdocumentvaluedocument}))
  - [#ctor](#dbaronenetdocumentdictionarydocument#ctor(systemcollectionsgenericidictionary{systemstring,dbaronenetdocumentvaluedocument}))
  - [RawValue](#dbaronenetdocumentdictionarydocumentrawvalue)
  - [Item](#dbaronenetdocumentdictionarydocumentitem(systemstring))
  - [GetElements](#dbaronenetdocumentdictionarydocumentgetelements)
  - [Add](#dbaronenetdocumentdictionarydocumentadd(systemstring,dbaronenetdocumentvaluedocument))
  - [Remove](#dbaronenetdocumentdictionarydocumentremove(systemstring))
  - [GetBytesCount](#dbaronenetdocumentdictionarydocumentgetbytescount(systemboolean))
- [DocumentType](#dbaronenetdocumentdocumenttype)
  - [Null](#dbaronenetdocumentdocumenttypenull)
  - [Boolean](#dbaronenetdocumentdocumenttypeboolean)
  - [Byte](#dbaronenetdocumentdocumenttypebyte)
  - [SByte](#dbaronenetdocumentdocumenttypesbyte)
  - [Char](#dbaronenetdocumentdocumenttypechar)
  - [Decimal](#dbaronenetdocumentdocumenttypedecimal)
  - [Double](#dbaronenetdocumentdocumenttypedouble)
  - [Single](#dbaronenetdocumentdocumenttypesingle)
  - [Int16](#dbaronenetdocumentdocumenttypeint16)
  - [UInt16](#dbaronenetdocumentdocumenttypeuint16)
  - [Int32](#dbaronenetdocumentdocumenttypeint32)
  - [UInt32](#dbaronenetdocumentdocumenttypeuint32)
  - [Int64](#dbaronenetdocumentdocumenttypeint64)
  - [UInt64](#dbaronenetdocumentdocumenttypeuint64)
  - [DateTime](#dbaronenetdocumentdocumenttypedatetime)
  - [Guid](#dbaronenetdocumentdocumenttypeguid)
  - [Array](#dbaronenetdocumentdocumenttypearray)
  - [Blob](#dbaronenetdocumentdocumenttypeblob)
  - [String](#dbaronenetdocumentdocumenttypestring)
  - [Document](#dbaronenetdocumentdocumenttypedocument)
  - [VarInt](#dbaronenetdocumentdocumenttypevarint)
- [ValueDocument](#dbaronenetdocumentvaluedocument)
  - [Null](#dbaronenetdocumentvaluedocumentnull)
  - [Type](#dbaronenetdocumentvaluedocumenttype)
  - [RawValue](#dbaronenetdocumentvaluedocumentrawvalue)
  - [Item](#dbaronenetdocumentvaluedocumentitem(systemstring))
  - [Item](#dbaronenetdocumentvaluedocumentitem(systemint32))
  - [GetBytesCount](#dbaronenetdocumentvaluedocumentgetbytescount(systemboolean))
  - [GetBytesCountElement](#dbaronenetdocumentvaluedocumentgetbytescountelement(systemstring,dbaronenetdocumentvaluedocument))
- [VarInt](#dbaronenetdocumentvarint)
  - [Value](#dbaronenetdocumentvarintvalue)
  - [Bytes](#dbaronenetdocumentvarintbytes)
  - [Size](#dbaronenetdocumentvarintsize)
- [Collation](#dbaronenetdocumentcollation)
  - [LCID](#dbaronenetdocumentcollationlcid)
  - [Culture](#dbaronenetdocumentcollationculture)
  - [SortOptions](#dbaronenetdocumentcollationsortoptions)
- [LCID](#dbaronenetdocumentlcid)
  - [Current](#dbaronenetdocumentlcidcurrent)
  - [DocumentType](#serialtypedocumenttype)
  - [Length](#serialtypelength)



---
>## <a id='dbaronenetdocumentdictionarydocument'></a>type: DictionaryDocument
### Namespace:
`Dbarone.Net.Document`
### Summary
 Represents a document as a dictionary of string / [ValueDocument](#dbaronenetdocumentvaluedocument) pairs. 

### Type Parameters:
None

>### <a id='dbaronenetdocumentdictionarydocument#ctor'></a>method: #ctor
#### Signature
``` c#
DictionaryDocument.#ctor()
```
#### Summary
 Creates an empty document. 

#### Type Parameters:
None

#### Parameters:
None

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetdocumentdictionarydocument#ctor(systemcollectionsconcurrentconcurrentdictionary{systemstring,dbaronenetdocumentvaluedocument})'></a>method: #ctor
#### Signature
``` c#
DictionaryDocument.#ctor(System.Collections.Concurrent.ConcurrentDictionary<System.String,Dbarone.Net.Document.ValueDocument> dict)
```
#### Summary
 Creates a new document using a dictionary of values. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|dict: |The dictionary containing the values.|

#### Exceptions:

Exception thrown: [T:System.ArgumentNullException](#T:System.ArgumentNullException): Throws an error if a null dictionary value is passed in.

#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetdocumentdictionarydocument#ctor(systemcollectionsgenericidictionary{systemstring,dbaronenetdocumentvaluedocument})'></a>method: #ctor
#### Signature
``` c#
DictionaryDocument.#ctor(System.Collections.Generic.IDictionary<System.String,Dbarone.Net.Document.ValueDocument> dict)
```
#### Summary
 Creates a new document using a dictionary of values. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|dict: |The dictionary containing the values.|

#### Exceptions:

Exception thrown: [T:System.ArgumentNullException](#T:System.ArgumentNullException): Throws an error if a null dictionary value is passed in.

#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetdocumentdictionarydocumentrawvalue'></a>property: RawValue
#### Summary
 Returns the raw value of the document, as an IDictionary. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetdocumentdictionarydocumentitem(systemstring)'></a>property: Item
#### Summary
 Get / set a field for document. Fields are case sensitive 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetdocumentdictionarydocumentgetelements'></a>method: GetElements
#### Signature
``` c#
DictionaryDocument.GetElements()
```
#### Summary
 Get all document elements - Return "_id" as first of all (if exists) 

#### Type Parameters:
None

#### Parameters:
None

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetdocumentdictionarydocumentadd(systemstring,dbaronenetdocumentvaluedocument)'></a>method: Add
#### Signature
``` c#
DictionaryDocument.Add(System.String key, Dbarone.Net.Document.ValueDocument value)
```
#### Summary
 Adds a new member to the document. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|key: |The new member key.|
|value: |The new member value.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetdocumentdictionarydocumentremove(systemstring)'></a>method: Remove
#### Signature
``` c#
DictionaryDocument.Remove(System.String key)
```
#### Summary
 Removes a member from the document. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|key: |The member key to remove.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetdocumentdictionarydocumentgetbytescount(systemboolean)'></a>method: GetBytesCount
#### Signature
``` c#
DictionaryDocument.GetBytesCount(System.Boolean recalc)
```
#### Summary
 Gets the byte count of the document. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|recalc: |Set to true to recalc.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetdocumentdocumenttype'></a>type: DocumentType
### Namespace:
`Dbarone.Net.Document`
### Summary
 Represents data types allowed in Dbarone.Net.Document. 

### Type Parameters:
None

>### <a id='dbaronenetdocumentdocumenttypenull'></a>field: Null
#### Summary
 Null value object. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetdocumentdocumenttypeboolean'></a>field: Boolean
#### Summary
 A boolean value. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetdocumentdocumenttypebyte'></a>field: Byte
#### Summary
 A single-byte value (0 to 255). 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetdocumentdocumenttypesbyte'></a>field: SByte
#### Summary
 A signed single byte value (-128 to 127). 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetdocumentdocumenttypechar'></a>field: Char
#### Summary
 A Unicode UTF-16 character. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetdocumentdocumenttypedecimal'></a>field: Decimal
#### Summary
 A 16-byte floating point numeric type. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetdocumentdocumenttypedouble'></a>field: Double
#### Summary
 An 8-byte floating point numeric type. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetdocumentdocumenttypesingle'></a>field: Single
#### Summary
 A 4-byte floating point numeric type. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetdocumentdocumenttypeint16'></a>field: Int16
#### Summary
 A signed 16-bit integer. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetdocumentdocumenttypeuint16'></a>field: UInt16
#### Summary
 An unsigned 16-bit integer. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetdocumentdocumenttypeint32'></a>field: Int32
#### Summary
 A signed 32-bit integer. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetdocumentdocumenttypeuint32'></a>field: UInt32
#### Summary
 An unsigned 32-bit integer. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetdocumentdocumenttypeint64'></a>field: Int64
#### Summary
 A signed 64-bit integer. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetdocumentdocumenttypeuint64'></a>field: UInt64
#### Summary
 An unsigned 64-bit integer. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetdocumentdocumenttypedatetime'></a>field: DateTime
#### Summary
 A date/time structure. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetdocumentdocumenttypeguid'></a>field: Guid
#### Summary
 Represents a globally unique identifier (GUID). 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetdocumentdocumenttypearray'></a>field: Array
#### Summary
 An array or collection of values. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetdocumentdocumenttypeblob'></a>field: Blob
#### Summary
 A variable-length byte-array. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetdocumentdocumenttypestring'></a>field: String
#### Summary
 A variable-length string. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetdocumentdocumenttypedocument'></a>field: Document
#### Summary
 A document with key/value pairs. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetdocumentdocumenttypevarint'></a>field: VarInt
#### Summary
 A variable-length integer. 


<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetdocumentvaluedocument'></a>type: ValueDocument
### Namespace:
`Dbarone.Net.Document`
### Summary
 Represent a simple value used in Document. 

### Type Parameters:
None

>### <a id='dbaronenetdocumentvaluedocumentnull'></a>field: Null
#### Summary
 Represents a Null type. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetdocumentvaluedocumenttype'></a>property: Type
#### Summary
 Indicate DataType of this value. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetdocumentvaluedocumentrawvalue'></a>property: RawValue
#### Summary
 Get internal .NET value object. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetdocumentvaluedocumentitem(systemstring)'></a>property: Item
#### Summary
 Get / set a field for document. Fields are case sensitive. Only permitted for DataType.Document. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetdocumentvaluedocumentitem(systemint32)'></a>property: Item
#### Summary
 Get / set value in array by position. Only permitted for DataType.Array 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetdocumentvaluedocumentgetbytescount(systemboolean)'></a>method: GetBytesCount
#### Signature
``` c#
ValueDocument.GetBytesCount(System.Boolean recalc)
```
#### Summary
 Returns how many bytes this BsonValue will consume when converted into binary BSON If recalc = false, use cached length value (from Array/Document only) 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|recalc: |Set to true to force recalculation.|

#### Exceptions:

Exception thrown: [T:System.ArgumentException](#T:System.ArgumentException): Throws an exception if an invalid document type.

#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetdocumentvaluedocumentgetbytescountelement(systemstring,dbaronenetdocumentvaluedocument)'></a>method: GetBytesCountElement
#### Signature
``` c#
ValueDocument.GetBytesCountElement(System.String key, Dbarone.Net.Document.ValueDocument value)
```
#### Summary
 Get how many bytes one single element will used in BSON format 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|key: |The key.|
|value: |The value.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetdocumentvarint'></a>type: VarInt
### Namespace:
`Dbarone.Net.Document`
### Summary
 Represents a variable-length unsigned integer using base-128 representation. https://en.wikipedia.org/wiki/Variable-length_quantity 

### Type Parameters:
None

>### <a id='dbaronenetdocumentvarintvalue'></a>property: Value
#### Summary
 The integer value of the VarInt. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetdocumentvarintbytes'></a>property: Bytes
#### Summary
 The byte[] array representation of the VarInt value. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetdocumentvarintsize'></a>property: Size
#### Summary
 The length in bytes that the VarInt uses to store the integer value. 


<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetdocumentcollation'></a>type: Collation
### Namespace:
`Dbarone.Net.Document`
### Summary
 Specifies collation comparisons for defined culture settings. Default CurrentCulture with IgnoreCase. 

### Type Parameters:
None

>### <a id='dbaronenetdocumentcollationlcid'></a>property: LCID
#### Summary
 Get LCID code from culture 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetdocumentcollationculture'></a>property: Culture
#### Summary
 Get database language culture 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetdocumentcollationsortoptions'></a>property: SortOptions
#### Summary
 Get options to how string should be compared in sort 


<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetdocumentlcid'></a>type: LCID
### Namespace:
`Dbarone.Net.Document`
### Summary
 Get CultureInfo object from LCID code (not avaiable in .net standard 1.3). https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-lcid/70feba9f-294e-491e-b6eb-56532684c37f 

### Type Parameters:
None

>### <a id='dbaronenetdocumentlcidcurrent'></a>property: Current
#### Summary
 Get current system operation LCID culture 


<small>[Back to top](#top)</small>
>### <a id='serialtypedocumenttype'></a>property: DocumentType
#### Summary
 The DocumentType of the value. 


<small>[Back to top](#top)</small>
>### <a id='serialtypelength'></a>property: Length
#### Summary
 Byte length of the data if string or blob. 


<small>[Back to top](#top)</small>
