using System.Collections;

namespace FlashcardsLiblary
{
    /// <summary>Методы расширения для коллекций.</summary>
    public static partial class CollectionHelper
    {
        /// <summary>Добавляет элементы последовательности <paramref name="source"/> в конец коллекции <paramref name="collection"/>.</summary>
        /// <typeparam name="T">Тип элемента коллекции.</typeparam>
        /// <param name="collection">Коллекция в которую добавляются элементы.</param>
        /// <param name="source">Последовательность добавляемых элементов.</param>
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> source)
        {
            if (collection is ICollection coll)
            {
                lock (coll.SyncRoot)
                    collection.PrivateAddRange(source);
            }
            else
            {
                collection.PrivateAddRange(source);
            }
        }

        private static void PrivateAddRange<T>(this ICollection<T> collection, IEnumerable<T> source)
        {
            foreach (var item in source)
                collection.Add(item);
        }

        /// <summary>Инициализирует коллекцию <paramref name="collection"/> элементами последовательности <paramref name="source"/>.</summary>
        /// <typeparam name="T">Тип элемента коллекции.</typeparam>
        /// <param name="collection">Инициализируемая коллекция.</param>
        /// <param name="source">Последовательность элементов помещаемых в коллекцию <paramref name="collection"/>.</param>
        /// <remarks>Коллекция <paramref name="collection"/> очищается методом <see cref="ICollection{T}.Clear"/>
        /// после чего в неё добавляются все элементы последовательноссти <paramref name="source"/>.</remarks>
        public static void Initial<T>(this ICollection<T> collection, IEnumerable<T> source)
        {
            if (collection is ICollection coll)
            {
                lock (coll.SyncRoot)
                    collection.PrivateInitial(source);
            }
            else
            {
                collection.PrivateInitial(source);
            }
        }

        /// <inheritdoc cref="Initial{T}(ICollection{T}, IEnumerable{T})"/>
        public static void Initial<T>(this ICollection<T> collection, params T[] source)
            => collection.Initial((IEnumerable<T>)source);

        private static void PrivateInitial<T>(this ICollection<T> collection, IEnumerable<T> source)
        {
            collection.Clear();
            collection.PrivateAddRange(source);
        }

        /// <summary>Инициализирует коллекцию <paramref name="collection"/> элементами последовательности <paramref name="source"/>.</summary>
        /// <typeparam name="T">Тип элемента коллекции.</typeparam>
        /// <param name="list">Инициализируемая индексированная коллекция (список).</param>
        /// <param name="source">Последовательность элементов помещаемых в коллекцию <paramref name="collection"/>.</param>
        /// <remarks>Коллекция <paramref name="list"/> не очищается.
        /// Элементы в ней последовательно заменяются элементами коллекции <paramref name="source"/>.
        /// Элементы которые не помещаются в <paramref name="list"/> - добавляются методом <see cref="ICollection{T}.Add(T)"/>.
        /// Элементы <paramref name="list"/>, которые не замещены - удаляются из конца коллекции методом <see cref="IList{T}.RemoveAt(int)"/>.</remarks>
        public static void InitialByReplace<T>(this IList<T> list, IEnumerable<T> source)
        {
            if (list is ICollection coll)
            {
                lock (coll.SyncRoot)
                    list.PrivateInitialByReplace(source);
            }
            else
            {
                list.PrivateInitialByReplace(source);
            }
        }

        /// <inheritdoc cref="InitialByReplace{T}(IList{T}, IEnumerable{T})"/>
        public static void InitialByReplace<T>(this IList<T> list, params T[] source)
            => list.InitialByReplace((IEnumerable<T>)source);

        private static void PrivateInitialByReplace<T>(this IList<T> list, IEnumerable<T> source)
        {
            int index = 0;
            foreach (var item in source)
            {
                if (index < list.Count)
                    list[index] = item;
                else
                    list.Add(item);
                index++;
            }
            for (int i = list.Count - 1; i >= index; i--)
            {
                list.RemoveAt(i);
            }
        }

        /// <summary>Метод заменяющий элемент в индексированной коллекции.</summary>
        /// <typeparam name="T">Тип элемента коллекции.</typeparam>
        /// <param name="list">Индексированная коллекция.</param>
        /// <param name="predicate">Предикат для поиска элемента в коллекции, котороый надо заменить.</param>
        /// <param name="newItem">Элемент на который будет заменён найденный элемент.</param>
        /// <returns><see langword="true"/> если элемент был найден и заменён, иначе - <see langword="false"/>.</returns>
        public static bool Replace<T>(this IList<T> list, Predicate<T> predicate, T newItem)
        {
            if (list is ICollection coll)
            {
                lock (coll.SyncRoot)
                    return list.PrivateReplace(predicate, newItem);
            }
            else
            {
                return list.PrivateReplace(predicate, newItem);
            }
        }

        private static bool PrivateReplace<T>(this IList<T> list, Predicate<T> predicate, T newItem)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (predicate(list[i]))
                {
                    list[i] = newItem;
                    return true;
                }
            }
            return false;
        }

        /// <summary>Метод заменяющий или добавляющий элемент в индексированную коллекции.</summary>
        /// <typeparam name="T">Тип элемента коллекции.</typeparam>
        /// <param name="list">Индексированная коллекция.</param>
        /// <param name="predicate">Предикат для поиска элемента в коллекции, котороый надо заменить.</param>
        /// <param name="newItem">Элемент на который будет заменён найденный элемент.</param>
        /// <returns><see langword="true"/> если элемент был найден и заменён,
        /// <see langword="false"/> - элемент не был найден и <paramref name="newItem"/> добаяляется в коллекцию.</returns>
        public static bool ReplaceOrAdd<T>(this IList<T> list, Predicate<T> predicate, T newItem)
        {
            if (list is ICollection coll)
            {
                lock (coll.SyncRoot)
                    return list.PrivateReplaceOrAdd(predicate, newItem);
            }
            else
            {
                return list.PrivateReplaceOrAdd(predicate, newItem);
            }
        }

        private static bool PrivateReplaceOrAdd<T>(this IList<T> list, Predicate<T> predicate, T newItem)
        {
            if (list.PrivateReplace(predicate, newItem))
                return true;

            list.Add(newItem);
            return false;
        }

        /// <summary>Возвращает индекс первого элемента индексированной коллекцию,
        /// удовлетворяющего предикату <paramref name="predicate"/>.</summary>
        /// <typeparam name="T">Тип элемента коллекции.</typeparam>
        /// <param name="list">Индексированная коллекция.</param>
        /// <param name="index">Индекс (с нуля) начальной позиции поиска. Значение 0 (ноль) действительно в пустом списке.</param>
        /// <param name="count">Число элементов в диапазоне, в котором выполняется поиск.</param>
        /// <param name="predicate">Предикат для поиска элемента в коллекции, индекс которого надо вернуть.</param>
        /// <returns> -1 - если элемент не был найден, иначе - индекс элемента.</returns>
        public static int IndexOf<T>(this IList<T> list, int index, int count, Predicate<T> predicate)
        {
            if (list is ICollection coll)
            {
                lock (coll.SyncRoot)
                    return list.PrivateIndexOf(index, count, predicate);
            }
            else
            {
                return list.PrivateIndexOf(index, count, predicate);
            }
        }

        /// <inheritdoc cref="IndexOf{T}(IList{T}, int, int, Predicate{T})"/>
        public static int IndexOf<T>(this IList<T> list, int index, Predicate<T> predicate)
            => list.IndexOf(index, list.Count - index, predicate);

        /// <inheritdoc cref="IndexOf{T}(IList{T}, int, int, Predicate{T})"/>
        public static int IndexOf<T>(this IList<T> list, Predicate<T> predicate)
            => list.IndexOf(0, list.Count, predicate);

        private static int PrivateIndexOf<T>(this IList<T> list, int index, int count, Predicate<T> predicate)
        {
            if (index < 0 || index >= list.Count)
                throw new ArgumentOutOfRangeException(nameof(index));
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            count += index;
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, list.Count);

            for (int i = index; i < count; i++)
            {
                if (predicate(list[i]))
                {
                    list.RemoveAt(i);
                    return i;
                }
            }
            return -1;
        }

        /// <summary>Возвращает коллекцию поэлементно.</summary>
        /// <typeparam name="T">Тип элемента коллекции.</typeparam>
        /// <param name="collection">Исходная коллекция.</param>
        /// <returns>Последовательность элементов коллекции.</returns>
        /// <remarks>Используется для защиты исходной коллекции от изменений.</remarks>
        public static IEnumerable<T> GetEnumerable<T>(this IEnumerable<T> collection)
        {
            foreach (T item in collection)
                yield return item;
        }

        /// <summary>Удаляет первый элемент в индексированной коллекцию,
        /// удовлетворяющий предикату <paramref name="predicate"/>.</summary>
        /// <typeparam name="T">Тип элемента коллекции.</typeparam>
        /// <param name="list">Индексированная коллекция.</param>
        /// <param name="predicate">Предикат для поиска элемента в коллекции, котороый надо удалить.</param>
        /// <returns><see langword="true"/> если элемент был найден и удалён, иначе - <see langword="false"/>.</returns>
        public static bool RemoveFirst<T>(this IList<T> list, Predicate<T> predicate)
        {
            if (list is ICollection coll)
            {
                lock (coll.SyncRoot)
                    return list.PrivateRemoveFirst(predicate);
            }
            else
            {
                return list.PrivateRemoveFirst(predicate);
            }
        }

        private static bool PrivateRemoveFirst<T>(this IList<T> list, Predicate<T> predicate)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (predicate(list[i]))
                {
                    list.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        /// <summary>Удаляет из индексированной коллекции все элементы удобвлетворяющие условию.</summary>
        /// <typeparam name="T">Тип элемента коллекции.</typeparam>
        /// <param name="list">Индексированная коллекция.</param>
        /// <param name="predicate">Предикат с условием.</param>
        /// <returns>Количество удалённых элементов.</returns>
        public static int RemoveAll<T>(this IList<T> list, Predicate<T> predicate)
        {
            if (list is ICollection coll)
            {
                lock (coll.SyncRoot)
                    return list.PrivateRemoveAll(predicate);
            }
            else
            {
                return list.PrivateRemoveAll(predicate);
            }
        }

        private static int PrivateRemoveAll<T>(this IList<T> list, Predicate<T> predicate)
        {
            if (list is List<T> _list)
            {
                return _list.RemoveAll(predicate);
            }

            int count = 0;
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (predicate(list[i]))
                {
                    list.RemoveAt(i);
                    count++;
                }
            }

            return count;
        }
    }
}