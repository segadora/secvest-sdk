using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Segadora.Secvest
{
    public class Secvest
    {
        private readonly string _endpoint;
        private readonly string _username;
        private readonly string _password;

        public Secvest(string endpoint, string username, string password)
        {
            _endpoint = endpoint;
            _username = username;
            _password = password;
        }

        public Zone GetZone(string zone)
        {
            return JsonSerializer.Deserialize<Zone>(Get($"/system/partition-{zone}/zone"));
        }

        private string? Get(string url)
        {
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => true;

            var client = new HttpClient(handler);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                "Basic",
                Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_username}:{_password}"))
            );

            var response = client
                .Send(
                    new HttpRequestMessage(HttpMethod.Get, _endpoint + url),
                    HttpCompletionOption.ResponseHeadersRead
                );

            response.EnsureSuccessStatusCode();

            var result = string.Empty;
            var reader = new StreamReader(response.Content.ReadAsStream());
            while (!reader.EndOfStream)
            {
                result += reader.ReadLine();
            }

            return result;
        }
    }
}
