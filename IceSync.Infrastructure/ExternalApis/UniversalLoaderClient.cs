using System.Net.Http.Headers;
using System.Net.Http.Json;
using IceSync.Domain;
using IceSync.Domain.Constants;
using IceSync.Domain.Models.Caching;
using IceSync.Domain.Models.Configuration;
using IceSync.Domain.Models.Dtos;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace IceSync.Infrastructure.ExternalApis
{
    public class UniversalLoaderClient
    {
        private readonly HttpClient _httpClient;
        private readonly UniversalLoaderConfig _configuration;
        private readonly string _basePath;

        public UniversalLoaderClient(HttpClient httpClient,
            IOptions<UniversalLoaderConfig> configuration)
        {
            _configuration = configuration.Value;
            _httpClient = httpClient;
            _basePath = _configuration.BaseUrl;
        }

        public async Task<BearerCacheData> Authenticate()
        {
            var body = new
            {
                ApiCompanyId = _configuration.ApiCompanyId,
                ApiUserId = _configuration.ApiUserId,
                ApiUserSecret = _configuration.ApiUserSecret,
            };
            var response = await _httpClient.PostAsJsonAsync($"{_basePath}/authenticate", body);
            var bearer = await response.Content.ReadAsStringAsync();
            var expiration = Common.GetBearerExpiration(bearer);
            return new BearerCacheData { Token = bearer, ExpirationDate = expiration };
        }

        public async Task<List<WorkflowDto>> GetWorkflowsList(string? bearer)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.BearerKey, bearer);
            var response = await _httpClient.GetAsync($"{_basePath}/workflows");
            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<WorkflowDto>>(json);
        }

        public async Task<bool> Run(string? bearer, int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.BearerKey, bearer);

            var body = new
            {
                WorkflowId = id,
            };

            var response = await _httpClient.PostAsJsonAsync($"{_basePath}/workflows/{id}/run", body);

            return response.IsSuccessStatusCode;
        }
    }
}