using System.Collections;

namespace Flashcards.Liblary.SyncedList
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