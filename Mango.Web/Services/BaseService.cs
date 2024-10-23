using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Mango.Web.Utility;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Mango.Web.Services
{
	public class BaseService : IBaseService
	{
		private IHttpClientFactory _httpClientFactory;
		public BaseService(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<ResponseDto> SendAsync(RequestDto request)
		{
			try
			{

				HttpClient client = _httpClientFactory.CreateClient("MangoApi");
				HttpRequestMessage requestMessage = new();
				requestMessage.Headers.Add("Accept", "Aplication/json");

				requestMessage.RequestUri = new Uri(request.Url!);
				if (request.Data is not null)
				{
					requestMessage.Content = new StringContent(JsonConvert.SerializeObject(request.Data), Encoding.UTF8, "application/json");
				}
				HttpResponseMessage? responseMessage = null;

				switch (request.ApiType)
				{
					case SD.ApiType.POST:
						requestMessage.Method = HttpMethod.Post;
						break;
					case SD.ApiType.PUT:
						requestMessage.Method = HttpMethod.Put;
						break;
					case SD.ApiType.DELETE:
						requestMessage.Method = HttpMethod.Delete;
						break;
					default:
						requestMessage.Method = HttpMethod.Get; break;
				}

				responseMessage = await client.SendAsync(requestMessage);

				switch (responseMessage.StatusCode)
				{
					case HttpStatusCode.NotFound:
						return new() { IsSucces = false, ErrorMessage = "Not Found" };

					case HttpStatusCode.Unauthorized:
						return new() { IsSucces = false, ErrorMessage = "Unauthorized" };

					case HttpStatusCode.Forbidden:
						return new() { IsSucces = false, ErrorMessage = "Access Denied" };

					case HttpStatusCode.InternalServerError:
						return new() { IsSucces = false, ErrorMessage = "Internal Error Server" };
					default:
						var content = await responseMessage.Content.ReadAsStringAsync();
						var apiResponsetDto = JsonConvert.DeserializeObject<ResponseDto>(content);
						return apiResponsetDto!;
				}

			}
			catch (Exception ex)
			{

				return new ResponseDto()
				{
					IsSucces = false,
					ErrorMessage = ex.Message.ToString()
				};
			}
		}
	}
}
