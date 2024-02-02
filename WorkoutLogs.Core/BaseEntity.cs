namespace WorkoutLogs.Core
{
    public abstract class BaseEntity
    {
        public DateTime? DateModified { get; set; }
        public int Id { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}