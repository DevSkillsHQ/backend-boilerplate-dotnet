using System.Net.Http;
using System.Threading.Tasks;
using System;

namespace backend.Data
{
    public class PresentationProxy
    {
        private readonly HttpClient httpClient;

        public PresentationProxy()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://infra.devskills.app/api/interactive-presentation/v4");
        }

        public async Task<HttpResponseMessage> CreatePresentationAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/presentations");
            return await httpClient.SendAsync(request);
        }

        public async Task<HttpResponseMessage> GetPresentationAsync(string presentationId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/presentations/{presentationId}");
            return await httpClient.SendAsync(request);
        }
    }
}
