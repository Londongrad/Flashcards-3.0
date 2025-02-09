using System.Collections;

namespace FlashcardsLiblary.SyncedList
{
    public partial class SyncedList<T> : IEnumerable, ICollection, IList, IReadOnlyCollection<T>, IReadOnlyList<T>, IEnumerable<T>, ICollection<T>, IList<T>, ISyncedList, ISyncedList<T>
    {
        public bool TryGetValue(int index, out T? value, TimeSpan timeout)
        {
            try
            {
                ReaderWriterLocker.AcquireReaderLock(timeout);
                value = privateList[index];
                ReaderWriterLocker.ReleaseReaderLock();
                return true;
            }
            catch (ApplicationException)
            {
                value = default;
                return false;
            }
        }

        public bool TrySetValue(int index, T value, TimeSpan timeout)
        {
            try
            {
                ReaderWriterLocker.AcquireWriterLock(timeout);
                privateList[index] = value;
                ReaderWriterLocker.ReleaseWriterLock();
                return true;
            }
            catch (ApplicationException)
            {
                return false;
            }
        }

        public bool TryCount(out int count, TimeSpan timeout)
        {
            try
            {
                ReaderWriterLocker.AcquireReaderLock(timeout);
                count = privateList.Count;
                ReaderWriterLocker.ReleaseReaderLock();
                return true;
            }
            catch (ApplicationException)
            {
                count = -1;
                return false;
            }
        }

        public bool TryAdd(T value, TimeSpan timeout)
        {
            try
            {
                ReaderWriterLocker.AcquireWriterLock(timeout);
                privateList.Add(value);
                ReaderWriterLocker.ReleaseWriterLock();
                return true;
            }
            catch (ApplicationException)
            {
                return false;
            }
        }

        public bool TryClear(TimeSpan timeout)
        {
            try
            {
                ReaderWriterLocker.AcquireWriterLock(timeout);
                privateList.Clear();
                ReaderWriterLocker.ReleaseWriterLock();
                return true;
            }
            catch (ApplicationException)
            {
                return false;
            }
        }

        public bool TryContains(T item, out bool contains, TimeSpan timeout)
        {
            try
            {
                ReaderWriterLocker.AcquireReaderLock(timeout);
                contains = privateList.Contains(item);
                ReaderWriterLocker.ReleaseReaderLock();
                return true;
            }
            catch (ApplicationException)
            {
                contains = false;
                return false;
            }
        }

        public bool TryCopyTo(T[] array, int arrayIndex, TimeSpan timeout)
        {
            try
            {
                ReaderWriterLocker.AcquireReaderLock(timeout);
                privateList.CopyTo(array, arrayIndex);
                ReaderWriterLocker.ReleaseReaderLock();
                return true;
            }
            catch (ApplicationException)
            {
                return false;
            }
        }

        public bool TryIndexOf(T item, out int index, TimeSpan timeout)
        {
            try
            {
                ReaderWriterLocker.AcquireReaderLock(timeout);
                index = privateList.IndexOf(item);
                ReaderWriterLocker.ReleaseReaderLock();
                return true;
            }
            catch (ApplicationException)
            {
                index = -1;
                return false;
            }
        }

        public bool TryInsert(int index, T item, TimeSpan timeout)
        {
            try
            {
                ReaderWriterLocker.AcquireWriterLock(timeout);
                privateList.Insert(index, item);
                ReaderWriterLocker.ReleaseWriterLock();
                return true;
            }
            catch (ApplicationException)
            {
                return false;
            }
        }

        public bool TryRemove(T item, out bool isRemove, TimeSpan timeout)
        {
            try
            {
                ReaderWriterLocker.AcquireWriterLock(timeout);
                isRemove = privateList.Remove(item);
                ReaderWriterLocker.ReleaseWriterLock();
                return true;
            }
            catch (ApplicationException)
            {
                isRemove = false;
                return false;
            }
        }

        public bool TryRemoveAt(int index, TimeSpan timeout)
        {
            try
            {
                ReaderWriterLocker.AcquireWriterLock(timeout);
                privateList.RemoveAt(index);
                ReaderWriterLocker.ReleaseWriterLock();
                return true;
            }
            catch (ApplicationException)
            {
                return false;
            }
        }
    }
}