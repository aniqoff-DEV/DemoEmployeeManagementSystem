using BaseLibrary.DTOs;
using ClientLibrary.Services.Contracts;
using System.Net;

namespace ClientLibrary.Helpers
{
    public class CustomHtppHandler : DelegatingHandler
    {
        private readonly GetHttpClient getHttpClient;
        private readonly LocalStorageService localStorageService;
        private readonly IUserAccountService accountService;

        public CustomHtppHandler(GetHttpClient getHttpClient,
            LocalStorageService localStorageService,
            IUserAccountService accountService)
        {
            this.getHttpClient = getHttpClient;
            this.localStorageService = localStorageService;
            this.accountService = accountService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            bool loginUrl = request.RequestUri!.AbsoluteUri.Contains("login");
            bool registerUrl = request.RequestUri!.AbsoluteUri.Contains("register");
            bool refreshToken = request.RequestUri!.AbsoluteUri.Contains("refresh-token");

            if(loginUrl || registerUrl || refreshToken) 
                return await base.SendAsync(request, cancellationToken);

            var result = await base.SendAsync(request, cancellationToken);
            if (result.StatusCode == HttpStatusCode.Unauthorized) 
            {
                var stringToken = await localStorageService.GetToken();
                if (stringToken == null) return result;

                string token = string.Empty;
                try
                {
                    token = request.Headers.Authorization!.Parameter!;
                }
                catch
                {

                }

                var desarializedToken = Serializations.DeserializeJsonString<UserSession>(stringToken);
                if (desarializedToken is null) return result;

                if (string.IsNullOrEmpty(token))
                {
                    request.Headers.Authorization = 
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", desarializedToken.Token);
                    return await base.SendAsync(request, cancellationToken);
                }

                var newJwtToken = await GetReshToken(desarializedToken.RefreshToken!);
                if(string.IsNullOrEmpty(newJwtToken)) return result;

                request.Headers.Authorization = 
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", newJwtToken);
                return await base.SendAsync(request, cancellationToken);
            }
            return result;
        }

        private async Task<string> GetReshToken(string refreshToken)
        {
            var result = await accountService.RefreshToken(new RefreshToken()
            {
                Token = refreshToken
            });
            string serializedToken = Serializations.SerializeObj(new UserSession()
            {
                Token = result.Token,
                RefreshToken = result.RefreshToken
            });
            await localStorageService.SetToken(serializedToken);

            return result.Token;
        }
    }
}
