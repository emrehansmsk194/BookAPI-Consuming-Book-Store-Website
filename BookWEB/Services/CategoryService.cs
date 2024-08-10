using BookAPI_Utility;
using BookWEB.Models;
using BookWEB.Models.DTO;
using BookWEB.Services.IServices;

namespace BookWEB.Services
{
	
	public class CategoryService : BaseService, ICategoryService
	{
		private readonly IHttpClientFactory _clientFactory;
		private string categoryUrl;
        public CategoryService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
			categoryUrl = configuration.GetValue<string>("ServiceUrls:BookAPI");
        }
        public Task<T> CreateAsync<T>(CategoryCreateDTO dto, string token)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = dto,
				Url = categoryUrl + "api/APICategory",
				Token = token
			});
		}

		public Task<T> DeleteAsync<T>(int id, string token)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.DELETE,
				Url = categoryUrl + "api/APICategory/" + id,
				Token = token
			});
		}

		public Task<T> GetAllAsync<T>(string token)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				Url = categoryUrl + "api/APICategory",
				Token = token
			});
		}

		public Task<T> GetAsync<T>(int id, string token)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				Url = categoryUrl + "api/APICategory/" + id,
				Token = token
			});
		}

		public Task<T> UpdateAsync<T>(CategoryUpdateDTO dTO, string token)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.PUT,
				Data = dTO,
				Url = categoryUrl + "api/APICategory/" + dTO.Id,
				Token = token
			});
		}
	}
}
