namespace sample_api_csharp.Abstracts
{
    public abstract class AbstractService
    {
        protected readonly HttpClient _httpClient;

        protected AbstractService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}
