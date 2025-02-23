namespace Flashcards.Liblary.Repository
{
    /// <summary>Базовый класс с идентификатором</summary>
    public abstract class IdDto(int id)
    {
        /// <summary>Целочисленный идентификатор</summary>
        public int Id { get; set; } = id;
    }
}