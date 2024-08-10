using BookWEB.Models.DTO;
using BookWEB.Services.IServices;
using BookWEB.Models;
using BookAPI_Utility;
using System.Net.Http;

namespace BookWEB.Services
{
    public class BookService : BaseService, IBookService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string bookUrl;
        public BookService(IHttpClientFactory clientFactory, IConfiguration configuration ) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            bookUrl = configuration.GetValue<string>("ServiceUrls:BookAPI");
        }
        public Task<T> CreateAsync<T>(BookCreateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = bookUrl + "api/API",
                Token = token

            });
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = bookUrl + "api/API/" + id,
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = bookUrl + "api/API",
                Token = token
            });
        }

        public Task<T> GetAsync<T>( int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = bookUrl + "api/API/" + id,
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(BookUpdateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = bookUrl + "api/API/" + dto.Id,
                Token = token
                
            });
        }
        public Task<T> GetByCategoryAsync <T>(int categoryId, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = bookUrl + "api/API/category=" + categoryId,
                Token = token

            });
        }
        public Task<T> GetByPublisherAsync <T>(int publisherId, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = bookUrl + "api/API/publisher=" + publisherId,
                Token = token
            });
        }
    }
}
