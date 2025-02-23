using System.Collections;

namespace Flashcards.Liblary.SyncedList
{
    public partial class SyncedList<T> : IEnumerable, ICollection, IList, IReadOnlyCollection<T>, IReadOnlyList<T>, IEnumerable<T>, ICollection<T>, IList<T>, ISyncedList, ISyncedList<T>
    {
        private readonly IList<T> privateList;

        public SyncedList()
            : this([], new ReaderWriterLock())
        { }

        public SyncedList(ReaderWriterLock readerWriterLocker) : this([], readerWriterLocker)
        { }

        public SyncedList(IList<T> list) : this(list, new ReaderWriterLock())
        { }

        public SyncedList(IList<T> list, ReaderWriterLock readerWriterLocker)
        {
            ArgumentNullException.ThrowIfNull(list);
            if (list is not ICollection) throw new ArgumentException("There must be an implementation of the ICollection interface.", nameof(list));
            if (list is not IList) throw new ArgumentException("There must be an implementation of the IList interface.", nameof(list));

            privateList = list;
            ReaderWriterLocker = readerWriterLocker ?? throw new ArgumentNullException(nameof(readerWriterLocker));
        }

        public T this[int index]
        {
            get
            {
                ReaderWriterLocker.AcquireReaderLock(Timeout.InfiniteTimeSpan);
                T t = privateList[index];
                ReaderWriterLocker.ReleaseReaderLock();
                return t;
            }

            set
            {
                ReaderWriterLocker.AcquireWriterLock(Timeout.InfiniteTimeSpan);
                privateList[index] = value;
                ReaderWriterLocker.ReleaseWriterLock();
            }
        }

        object? IList.this[int index]
        {
            get => this[index];
            set
            {
                ReaderWriterLocker.AcquireWriterLock(Timeout.InfiniteTimeSpan);
                ((IList)privateList)[index] = value;
                ReaderWriterLocker.ReleaseWriterLock();
            }
        }

        public int Count
        {
            get
            {
                ReaderWriterLocker.AcquireReaderLock(Timeout.InfiniteTimeSpan);
                int count = privateList.Count;
                ReaderWriterLocker.ReleaseReaderLock();
                return count;
            }
        }

        public bool IsReadOnly => privateList.IsReadOnly;

        public ReaderWriterLock ReaderWriterLocker { get; }
        public bool IsFixedSize => ((IList)privateList).IsFixedSize;
        public bool IsSynchronized { get; } = true;
        public object SyncRoot => throw new NotImplementedException();

        public void Add(T item)
        {
            ReaderWriterLocker.AcquireWriterLock(Timeout.InfiniteTimeSpan);
            privateList.Add(item);
            ReaderWriterLocker.ReleaseWriterLock();
        }

        public int Add(object? value)
        {
            ReaderWriterLocker.AcquireWriterLock(Timeout.InfiniteTimeSpan);
            int index = ((IList)privateList).Add(value);
            ReaderWriterLocker.ReleaseWriterLock();
            return index;
        }

        public void Clear()
        {
            ReaderWriterLocker.AcquireWriterLock(Timeout.InfiniteTimeSpan);
            privateList.Clear();
            ReaderWriterLocker.ReleaseWriterLock();
        }

        public bool Contains(T item)
        {
            ReaderWriterLocker.AcquireReaderLock(Timeout.InfiniteTimeSpan);
            bool contains = privateList.Contains(item);
            ReaderWriterLocker.ReleaseReaderLock();
            return contains;
        }

        public bool Contains(object? value)
        {
            ReaderWriterLocker.AcquireReaderLock(Timeout.InfiniteTimeSpan);
            bool contains = ((IList)privateList).Contains(value);
            ReaderWriterLocker.ReleaseReaderLock();
            return contains;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            ReaderWriterLocker.AcquireReaderLock(Timeout.InfiniteTimeSpan);
            privateList.CopyTo(array, arrayIndex);
            ReaderWriterLocker.ReleaseReaderLock();
        }

        public void CopyTo(Array array, int index)
        {
            ReaderWriterLocker.AcquireReaderLock(Timeout.InfiniteTimeSpan);
            ((ICollection)privateList).CopyTo(array, index);
            ReaderWriterLocker.ReleaseReaderLock();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return privateList.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            ReaderWriterLocker.AcquireReaderLock(Timeout.InfiniteTimeSpan);
            int index = privateList.IndexOf(item);
            ReaderWriterLocker.ReleaseReaderLock();
            return index;
        }

        public int IndexOf(object? value)
        {
            ReaderWriterLocker.AcquireWriterLock(Timeout.InfiniteTimeSpan);
            int index = ((IList)privateList).IndexOf(value);
            ReaderWriterLocker.ReleaseWriterLock();
            return index;
        }

        public void Insert(int index, T item)
        {
            ReaderWriterLocker.AcquireWriterLock(Timeout.InfiniteTimeSpan);
            privateList.Insert(index, item);
            ReaderWriterLocker.ReleaseWriterLock();
        }

        public void Insert(int index, object? value)
        {
            ReaderWriterLocker.AcquireWriterLock(Timeout.InfiniteTimeSpan);
            ((IList)privateList).Insert(index, value);
            ReaderWriterLocker.ReleaseWriterLock();
        }

        public bool Remove(T item)
        {
            ReaderWriterLocker.AcquireWriterLock(Timeout.InfiniteTimeSpan);
            bool remove = privateList.Remove(item);
            ReaderWriterLocker.ReleaseWriterLock();
            return remove;
        }

        public void Remove(object? value)
        {
            ReaderWriterLocker.AcquireWriterLock(Timeout.InfiniteTimeSpan);
            ((IList)privateList).Remove(value);
            ReaderWriterLocker.ReleaseWriterLock();
        }

        public void RemoveAt(int index)
        {
            ReaderWriterLocker.AcquireWriterLock(Timeout.InfiniteTimeSpan);
            privateList.RemoveAt(index);
            ReaderWriterLocker.ReleaseWriterLock();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}