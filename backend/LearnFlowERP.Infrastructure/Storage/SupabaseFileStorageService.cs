using System.Net.Http.Headers;
using LearnFlowERP.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LearnFlowERP.Infrastructure.Storage
{
    public class SupabaseFileStorageService : IFileStorageService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly string _apiKey;
        private readonly string _bucket;

        public SupabaseFileStorageService(IConfiguration config)
        {
            _httpClient = new HttpClient();

            _baseUrl = config["Supabase:Url"];
            _apiKey = config["Supabase:ApiKey"];
            _bucket = config["Supabase:Bucket"];
        }

        public async Task<string> UploadAsync(Stream fileStream, string fileName, string contentType)
        {
            var path = fileName.Replace(" ", "_"); // avoid spaces

            var url = $"{_baseUrl}/storage/v1/object/{_bucket}/{path}";

            using var content = new StreamContent(fileStream);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);

            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = content;

            request.Headers.Add("apikey", _apiKey);
            request.Headers.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);

            // 🔥 IMPORTANT
            request.Headers.Add("x-upsert", "true");

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Upload failed: {error}");
            }

            return $"{_baseUrl}/storage/v1/object/public/{_bucket}/{path}";
        }

        public async Task<bool> DeleteAsync(string filePath)
        {
            var fileName = Path.GetFileName(filePath);

            var url = $"{_baseUrl}/storage/v1/object/{_bucket}/{fileName}";

            var request = new HttpRequestMessage(HttpMethod.Delete, url);
            request.Headers.Add("apikey", _apiKey);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

            var response = await _httpClient.SendAsync(request);

            return response.IsSuccessStatusCode;
        }

        public async Task<Stream> DownloadAsync(string fileUrl)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, fileUrl);

            request.Headers.Add("apikey", _apiKey);
            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", _apiKey);

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to download file");
            }

            return await response.Content.ReadAsStreamAsync();
        }
    }
}