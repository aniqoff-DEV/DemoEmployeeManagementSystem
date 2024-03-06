using BaseLibrary.Responses;

namespace ClientLibrary.Services.Contracts
{
    public interface IGenericService<T>
    {
        Task<List<T>> GetAll(string baseUrl);
        Task<T> GetById(int id, string baseUrl);
        Task<GeneralResponse> Insert(T item, string baseUrl);
        Task<GeneralResponse> Update(T item, string baseUrl);
        Task<GeneralResponse> DeleteById(int id, string baseUrl);
    }
}
