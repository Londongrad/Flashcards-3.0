namespace Flashcards.Liblary.Cloneable
{
    public interface ICloneable<T> : ICloneable
    {
        new T Clone();
    }
}