using BaseLibrary.DTOs;

namespace ClientLibrary.Helpers
{
    public class GetHttpClient
    {
        private const string HeaderKey = "Authorization";
        private readonly IHttpClientFactory httpClientFactory;
        private readonly LocalStorageService localStorageService;

        public GetHttpClient(IHttpClientFactory httpClientFactory, LocalStorageService localStorageService)
        {
            this.httpClientFactory = httpClientFactory;
            this.localStorageService = localStorageService;
        }

        public async Task<HttpClient> GetPrivateHttpClient()
        {
            var client = httpClientFactory.CreateClient("SystemApiClient");
            var stringToken = await localStorageService.GetToken();
            if (string.IsNullOrEmpty(stringToken)) return client;

            var deserializeToken = Serializations.DeserializeJsonString<UserSession>(stringToken);
            if(deserializeToken == null) return client;

            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",deserializeToken.Token);
            return client;
        }

        public HttpClient GetPublicHttpClient()
        {
            var client = httpClientFactory.CreateClient("SystemApiClient");
            client.DefaultRequestHeaders.Remove(HeaderKey);
            return client;
        }
    }
}
