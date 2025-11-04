

namespace Store_X.Domain.Contracts
{
    public interface ICacheRepository
    {
        Task<string> GetAsync(string key);
        Task<string> SetAsync(string key, object value, TimeSpan lifeTime);
    }
}
