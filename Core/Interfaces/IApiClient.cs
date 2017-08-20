using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace Core.Interfaces
{
    public interface IApiClient
    {
        Task<bool?> PostAsync<T>(T entity) where T : EntityBase;
        Task<bool?> DeleteAsync<T>(T entity) where T : EntityBase;
        Task<bool?> PutAsync<T>(T entity) where T : EntityBase;
        Task<List<T>> GetAsync<T>() where T : EntityBase;
    }
}