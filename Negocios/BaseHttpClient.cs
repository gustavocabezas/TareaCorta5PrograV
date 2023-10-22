using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Negocios
{
    public class BaseHttpClient : HttpClient, IDisposable
    {
        public BaseHttpClient(string baseUrl = "https://localhost:44311/")
        {
            Timeout = TimeSpan.FromSeconds(15);
            BaseAddress = new Uri(baseUrl);
            DefaultRequestHeaders.Accept.Clear();
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // No es necesario llamar a base.Dispose()
            }
        }
    }


}
