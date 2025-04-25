namespace Flashcards.Liblary.Repository
{
    /// <summary>Базовый класс с идентификатором</summary>
    public abstract class IdDto(int id, string name)
    {
        /// <summary>Целочисленный идентификатор</summary>
        public int Id { get; set; } = id;

        /// <summary>Название</summary>
        public string Name { get; set; } = name;
    }
}