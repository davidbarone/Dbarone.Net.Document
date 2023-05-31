using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Dbarone.Net.Document
{
    public class DocArray : DocValue, IList<DocValue>
    {
        public DocArray()
            : base(DocType.Array, new List<DocValue>())
        {
        }

        public DocArray(List<DocValue> array)
            : this()
        {
            if (array == null) throw new ArgumentNullException(nameof(array));

            this.AddRange(array);
        }

        public DocArray(params DocValue[] array)
            : this()
        {
            if (array == null) throw new ArgumentNullException(nameof(array));

            this.AddRange(array);
        }

        public DocArray(IEnumerable<DocValue> items)
            : this()
        {
            if (items == null) throw new ArgumentNullException(nameof(items));

            this.AddRange(items);
        }

        public new IList<DocValue> RawValue => (IList<DocValue>)base.RawValue;

        public override DocValue this[int index]
        {
            get
            {
                return this.RawValue[index];
            }
            set
            {
                this.RawValue[index] = value ?? DocValue.Null;
            }
        }

        public int Count => this.RawValue.Count;

        public bool IsReadOnly => false;

        public void Add(DocValue item) => this.RawValue.Add(item ?? DocValue.Null);

        public void AddRange<TCollection>(TCollection collection)
            where TCollection : ICollection<DocValue>
        {
            if(collection == null)
                throw new ArgumentNullException(nameof(collection));

            var list = (List<DocValue>)base.RawValue;

            var listEmptySpace = list.Capacity - list.Count;
            if (listEmptySpace < collection.Count)
            {
                list.Capacity += collection.Count;
            }

            foreach (var bsonValue in collection)
            {
                list.Add(bsonValue ?? Null);    
            }
            
        }
        
        public void AddRange(IEnumerable<DocValue> items)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));

            foreach (var item in items)
            {
                this.Add(item ?? DocValue.Null);
            }
        }

        public void Clear() => this.RawValue.Clear();

        public bool Contains(DocValue item) => this.RawValue.Contains(item ?? DocValue.Null);

        public void CopyTo(DocValue[] array, int arrayIndex) => this.RawValue.CopyTo(array, arrayIndex);

        public IEnumerator<DocValue> GetEnumerator() => this.RawValue.GetEnumerator();

        public int IndexOf(DocValue item) => this.RawValue.IndexOf(item ?? DocValue.Null);

        public void Insert(int index, DocValue item) => this.RawValue.Insert(index, item ?? DocValue.Null);

        public bool Remove(DocValue item) => this.RawValue.Remove(item);

        public void RemoveAt(int index) => this.RawValue.RemoveAt(index);

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var value in this.RawValue)
            {
                yield return value;
            }
        }

        public override int CompareTo(DocValue other)
        {
            // if types are different, returns sort type order
            if (other.Type != DocType.Array) return this.Type.CompareTo(other.Type);

            var otherArray = other.AsArray;

            var result = 0;
            var i = 0;
            var stop = Math.Min(this.Count, otherArray.Count);

            // compare each element
            for (; 0 == result && i < stop; i++)
                result = this[i].CompareTo(otherArray[i]);

            if (result != 0) return result;
            if (i == this.Count) return i == otherArray.Count ? 0 : -1;
            return 1;
        }

        private int _length;

        internal override int GetBytesCount(bool recalc)
        {
            if (recalc == false && _length > 0) return _length;

            var length = 5;
            var array = this.RawValue;
            
            for (var i = 0; i < array.Count; i++)
            {
                length += this.GetBytesCountElement(i.ToString(), array[i]);
            }

            return _length = length;
        }
    }
}