using BookWEB.Models;

namespace BookWEB.Services.IServices
{
    public interface IBaseService
    {
        APIResponse ResponseModel { get; set; }
        Task<T>SendAsync<T>(APIRequest apiRequest);

    }
}
