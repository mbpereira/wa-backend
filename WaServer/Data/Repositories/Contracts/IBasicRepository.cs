using System.Collections.Generic;
using System.Threading.Tasks;

namespace WaServer.Data.Repositories.Contracts
{
    public interface IBasicRepository<T>
    {
        Task<IList<T>> GetAll(int? skip = null, int? take = null);
        Task Create(T data);
        Task Remove(T data);
        Task Update(T data);
        Task<T> FindByKey(params object[] key);
    }
}
