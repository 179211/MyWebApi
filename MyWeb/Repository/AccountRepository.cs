using Microsoft.AspNetCore.Http;
using MyWeb.Models;
using MyWeb.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyWeb.Repository
{
    public class AccountRepository : Repository<User>, IAccountRepository
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly IHttpContextAccessor _httpContext;

        public AccountRepository(IHttpClientFactory clientFactory, JsonSerializerOptions jsonSerializerOptions, IHttpContextAccessor httpContext = null)
            : base(clientFactory, jsonSerializerOptions, httpContext)
        {
            _clientFactory = clientFactory;
            _jsonSerializerOptions = jsonSerializerOptions ?? throw new ArgumentNullException(nameof(jsonSerializerOptions));
            _httpContext = httpContext;
        }

        public async Task<User> LoginAsync(string url, User objToCreate)
        {

            var request = new HttpRequestMessage(HttpMethod.Post, url);
            if (objToCreate != null)
            {
                request.Content = new StringContent(
                    JsonSerializer.Serialize(objToCreate, _jsonSerializerOptions), Encoding.UTF8, "application/json");
            }
            else
            {
                return new User();
            }

            var client = _clientFactory.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<User>(jsonString, _jsonSerializerOptions);
            }
            else
            {
                return new User();
            }
        }

        public async Task<bool> RegisterAsync(string url, User objToCreate)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            if (objToCreate != null)
            {
                request.Content = new StringContent(
                    JsonSerializer.Serialize(objToCreate, _jsonSerializerOptions), Encoding.UTF8, "application/json");
            }
            else
            {
                return false;
            }

            var client = _clientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
