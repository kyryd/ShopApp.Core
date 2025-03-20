using Microsoft.AspNetCore.Mvc;
using RepositoryAndServicies.Services.Network.Absract;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace RepositoryAndServicies.Services.Network
{
    public sealed class HttpService : IHttpService
    {
        private readonly string _serverBaseAddress = string.Empty;
        public JsonSerializerOptions JsonOptions { get; }
        private HttpStatusCode _lastStatusCode = HttpStatusCode.OK;
        private HttpStatusCode StatusCode => _lastStatusCode;
        //private HttpClient Client { get; }

        
        public string? ErrorMessage => StatusCode switch
        {
            HttpStatusCode.OK => null,
            HttpStatusCode.BadRequest => "Bad Request",
            HttpStatusCode.NotFound => "Not Found",
            HttpStatusCode.InternalServerError => "Internal Server Error",
            HttpStatusCode.Forbidden => "Forbidden. Service is down, please try later",
            _ => "Unknown"
        };

         

        private static string AdjustPath(string path) => path.StartsWith("/") ? path : $"/{path}";
        public HttpService(string serverBaseAddress)
        {
            _serverBaseAddress = serverBaseAddress;
            //_serviceName = serviceName;
            JsonOptions = new()
            {
                PropertyNameCaseInsensitive = true
            };
          //  Client = new();

        }
        public async Task<ActionResult<string>> GetAsyncFrom(string path)
        {
            string responseContent = string.Empty;
            path = AdjustPath(path);

            HttpClient client = new ();
            try
            {
                client.BaseAddress = new($"{_serverBaseAddress}{path}");
                Debug.WriteLine(client.BaseAddress.OriginalString);
                HttpResponseMessage response = await client.GetAsync(path);
                _lastStatusCode = response.StatusCode;
                response.EnsureSuccessStatusCode();
                responseContent = await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
                return new BadRequestObjectResult(e.Message);
            }
            finally 
            { 
                client.Dispose(); 
            }

            return new OkObjectResult(responseContent);
        }
        public async Task<ActionResult<string>> CreateAsyncBy(string path, HttpContent content)
        {
            using HttpClient client = new();
            string responseContent = string.Empty;

            path = AdjustPath(path);
            try
            {
                client.BaseAddress = new($"{_serverBaseAddress}{path}");
                Debug.WriteLine(client.BaseAddress.OriginalString);
                HttpResponseMessage response = await client.PostAsync(path, content);
                _lastStatusCode = response.StatusCode;
                response.EnsureSuccessStatusCode();
                responseContent = await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return new BadRequestObjectResult(e.Message);

            }
            finally
            {
                client.Dispose();
            }


            return new OkObjectResult(responseContent);
        }



        public async Task<ActionResult<string>> UpdateAsyncBy(string path, HttpContent content)
        {
            string responseContent = string.Empty;
            HttpClient client = new();
            path = AdjustPath(path);
            try
            {
                client.BaseAddress = new($"{_serverBaseAddress}{path}");
                Debug.WriteLine(client.BaseAddress.OriginalString);
                HttpResponseMessage response = await client.PutAsync(path, content);
                _lastStatusCode = response.StatusCode;
                response.EnsureSuccessStatusCode();
                responseContent = await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return new BadRequestObjectResult(e.Message);

            }
            finally
            {
                client.Dispose();
            }
            return new OkObjectResult(responseContent);
        }

        public async Task<ActionResult<string>> DeleteAsyncBy(string path)
        {
            string responseContent = string.Empty;
            path = AdjustPath(path);
            HttpClient client = new();
            try
            {
                client.BaseAddress = new($"{_serverBaseAddress}{path}");
                Debug.WriteLine(client.BaseAddress.OriginalString);
                HttpResponseMessage response = await client.DeleteAsync(path);
                _lastStatusCode = response.StatusCode;
                response.EnsureSuccessStatusCode();
                responseContent = await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return new BadRequestObjectResult(e.Message);
            }
            finally
            {
                client.Dispose();
            }

            return new OkObjectResult(responseContent);
        }

        public void Dispose()
        {
            //Client.Dispose();
        }
    }
}
