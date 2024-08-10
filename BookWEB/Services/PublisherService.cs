using BookWEB.Models;
using BookWEB.Models.DTO;
using BookWEB.Services.IServices;
using BookAPI_Utility;

namespace BookWEB.Services
{
	public class PublisherService : BaseService, IPublisherService
	{
		private readonly IHttpClientFactory _clientFactory;
		private string publisherUrl;
        public PublisherService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
			_clientFactory = clientFactory;
			publisherUrl = configuration.GetValue<string>("ServiceUrls:BookAPI");
            
        }
        public Task<T> CreateAsync<T>(PublisherCreateDTO dto)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = dto,
				Url = publisherUrl + "api/APIPublisher"
			});
		}

		public Task<T> DeleteAsync<T>(int id)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType= SD.ApiType.DELETE,
				Url = publisherUrl + "api/APIPublisher/" + id
			});
		}

		public Task<T> GetAllAsync<T>()
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				Url = publisherUrl + "api/APIPublisher"
			});
		}

		public Task<T> GetAsync<T>(int id)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				Url = publisherUrl + "api/APIPublisher/" + id
			});
		}

		public Task<T> UpdateAsync<T>(PublisherUpdateDTO dTO)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.PUT,
				Data = dTO,
				Url = publisherUrl + "api/APIPublisher/" + dTO.Id
			});
		}
	}
}
