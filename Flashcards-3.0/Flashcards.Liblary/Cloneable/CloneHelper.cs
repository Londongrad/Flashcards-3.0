namespace Flashcards.Liblary.Cloneable
{
    public static partial class CloneHelper
    {
        /// <summary>Создаёт клон экземпляра класса <typeparamref name="T"/>.</summary>
        /// <typeparam name="T">Тип экземпляра.</typeparam>
        /// <param name="obj">Исходный экземпляр.</param>
        /// <returns>Возвращает новый экземпляр типа <typeparamref name="T"/>
        /// являющийся неглубокой копией исходного экземляра.</returns>
        /// <remarks>Если тип <typeparamref name="T"/> реализует интерфейс <see cref="ICloneable"/> или <see cref="ICloneable{T}"/>,
        /// то копия создаётся методом <see cref="ICloneable.Clone"/> или <see cref="ICloneable{T}.Clone"/>.
        /// Иначе - методом <see cref="object.MemberwiseClone"/>.</remarks>
        public static T Clone<T>(this T obj)
        {
            T clone;
            if (obj is ICloneable<T> clnT)
            {
                clone = clnT.Clone();
            }
            else if (obj is ICloneable cln && cln.Clone() is T t)
            {
                clone = t;
            }
            else
            {
                clone = (T)MemberwiseCloneHandler.memberwiseClone(obj!);
            }
            return clone;
        }

        private class MemberwiseCloneHandler
        {
            private MemberwiseCloneHandler()
            { }

            public static readonly Func<object, object> memberwiseClone
                = (Func<object, object>)((Func<object>)new MemberwiseCloneHandler().MemberwiseClone)
                    .Method.CreateDelegate(typeof(Func<object, object>));
        }
    }
}