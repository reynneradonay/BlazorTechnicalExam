namespace BlazorTechnicalExam.Shared.Data
{
    public class DataSourceResult<TEntity>
    {
        public int Count { get; set; }

        public IEnumerable<TEntity> Items { get; set; }
    }
}
