namespace IpLogger.Domain.Filters.Abstract
{
    public abstract class FilterBase<TModel> where TModel : class
    {
        public abstract IEnumerable<Predicate<TModel>> GetPredicates();
    }
}
