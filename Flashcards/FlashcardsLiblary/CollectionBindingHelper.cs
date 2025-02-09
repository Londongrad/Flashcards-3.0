using FlashcardsLiblary.SyncedList;
using System.Collections;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;

namespace FlashcardsLiblary
{
    public static partial class CollectionBindingHelper
    {
        /// <summary>
        /// Нужно чтобы ObservableCollection можно было обновлять из любого потока. <br/>
        /// Без этого методы обновления БД в Репозитории будут выкидывать исключения. <br/>
        /// Это специфика платформы WPF. В UWP, например, в таком нет необходимости.
        /// </summary>
        /// <param name="collection"></param>
        public static void EnableCollectionSynchronization(this ICollection collection)
            => collection.EnableCollectionSynchronization(Timeout.InfiniteTimeSpan);

        public static void EnableCollectionSynchronization(this ICollection collection, TimeSpan timeout)
        {
            if (collection is null)
                return;
            if (SynchronizationContext.Current is DispatcherSynchronizationContext context)
            {
                if (collection is ISyncedList syncCollection)
                {
                    syncCollection.EnableCollectionSynchronization(timeout);
                }
                else
                {
                    BindingOperations.EnableCollectionSynchronization(collection, collection.SyncRoot);
                }
            }
            else
            {
                Application.Current.Dispatcher.Invoke(collection.EnableCollectionSynchronization);
            }
        }

        public static void DisableCollectionSynchronization(this ICollection collection)
        {
            if (collection is null)
                return;
            if (SynchronizationContext.Current is DispatcherSynchronizationContext context)
            {
                BindingOperations.DisableCollectionSynchronization(collection);
            }
            else
            {
                Application.Current.Dispatcher.Invoke(collection.DisableCollectionSynchronization);
            }
        }

        public static void EnableCollectionSynchronization<T>(this ISyncedList collection)
            => collection.EnableCollectionSynchronization(TimeSpan.FromSeconds(1));

        public static void EnableCollectionSynchronization(this ISyncedList syncCollection, TimeSpan timeout)
        {
            if (syncCollection is null)
                return;
            if (SynchronizationContext.Current is DispatcherSynchronizationContext context)
            {
                BindingOperations.EnableCollectionSynchronization(syncCollection, timeout, OnCollectionSynchronization);
            }
            else
            {
                Application.Current.Dispatcher.Invoke(syncCollection.EnableCollectionSynchronization);
            }
            static void OnCollectionSynchronization(IEnumerable collection, object context, Action accessMethod, bool writeAccess)
            {
                ISyncedList syncCollection = (ISyncedList)collection;

                TimeSpan timeout = (TimeSpan)context;

                CollectionSynchronization(syncCollection, timeout, accessMethod, writeAccess);
            }
        }

        public static void CollectionSynchronization(ISyncedList syncCollection, TimeSpan timeout, Action accessMethod, bool writeAccess)
        {
            if (writeAccess)
            {
                syncCollection.ReaderWriterLocker.AcquireWriterLock(timeout);
                accessMethod();
                syncCollection.ReaderWriterLocker.ReleaseWriterLock();
            }
            else
            {
                syncCollection.ReaderWriterLocker.AcquireReaderLock(timeout);
                accessMethod();
                syncCollection.ReaderWriterLocker.ReleaseReaderLock();
            }
        }
    }
}