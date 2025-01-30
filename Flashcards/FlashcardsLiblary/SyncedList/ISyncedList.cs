using System.Collections;

namespace FlashcardsLiblary.SyncedList
{
    public interface ISyncedList<T> : IList<T>
    {
        ReaderWriterLock ReaderWriterLocker { get; }
    }
    public interface ISyncedList : IList
    {
        ReaderWriterLock ReaderWriterLocker { get; }
    }
}
