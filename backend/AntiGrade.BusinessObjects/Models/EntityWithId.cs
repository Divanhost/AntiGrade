namespace AntiGrade.BusinessObjects.Models
{
    public interface IEntityWithId<T> : IEntity<T>
    {
        T Id { get; set; }
    }
}