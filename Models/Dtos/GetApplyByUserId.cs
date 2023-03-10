namespace Models.Dtos
{
    public class GetApplysByUserId<TEntity> where TEntity : class, new ()
    {
        public List<TEntity> Pending { get; set; } = default!;
        public List<TEntity> NoComplete { get; set; } = default!;
        public List<TEntity> Completed { get; set; } = default!;
    }
}
