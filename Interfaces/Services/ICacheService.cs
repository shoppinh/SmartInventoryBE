using System;

namespace SmartInventoryBE.Interfaces.Services;

public interface ICacheService
{
    public Task<T?> GetOrCreateAsync<T>(string key, Func<Task<T>> factory);
    public void Remove(string key);

}
