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
        private HttpClient Client { get; }


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

        /// <summary>
        /// Creates a new instance of HttpService with the specified server base address.
        /// </summary>
        /// <param name="serverBaseAddress"></param>
        public HttpService(string serverBaseAddress)
        {
            _serverBaseAddress = serverBaseAddress;
            JsonOptions = new()
            {
                PropertyNameCaseInsensitive = true
            };

            Client = new()
            {
                BaseAddress = new($"{_serverBaseAddress}")
            };

        }
        /// <summary>
        /// Sends a GET request to the specified path and returns the response content as a string.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task<ActionResult<string>> GetAsyncFrom(string path)
        {
            string responseContent = string.Empty;
            path = AdjustPath(path);

            try
            {
                Debug.WriteLine(Client.BaseAddress?.OriginalString);
                HttpResponseMessage response = await Client.GetAsync(path);
                _lastStatusCode = response.StatusCode;
                response.EnsureSuccessStatusCode();
                responseContent = await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
                return new BadRequestObjectResult(e.Message);
            }

            return new OkObjectResult(responseContent);
        }
        /// <summary>
        /// Sends a POST request to the specified path with the given content and returns the response content as a string.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<ActionResult<string>> CreateAsyncBy(string path, HttpContent content)
        {
            string responseContent = string.Empty;

            path = AdjustPath(path);
            try
            {
                Debug.WriteLine(Client.BaseAddress?.OriginalString);
                HttpResponseMessage response = await Client.PostAsync(path, content);
                _lastStatusCode = response.StatusCode;
                response.EnsureSuccessStatusCode();
                responseContent = await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return new BadRequestObjectResult(e.Message);

            }

            return new OkObjectResult(responseContent);
        }


        /// <summary>
        /// Sends a PUT request to the specified path with the given content and returns the response content as a string.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<ActionResult<string>> UpdateAsyncBy(string path, HttpContent content)
        {
            string responseContent = string.Empty;
            path = AdjustPath(path);
            try
            {
                HttpResponseMessage response = await Client.PutAsync(path, content);
                _lastStatusCode = response.StatusCode;
                response.EnsureSuccessStatusCode();
                responseContent = await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return new BadRequestObjectResult(e.Message);

            }
            return new OkObjectResult(responseContent);
        }

        /// <summary>
        /// Sends a DELETE request to the specified path and returns the response content as a string.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task<ActionResult<string>> DeleteAsyncBy(string path)
        {
            string responseContent = string.Empty;
            path = AdjustPath(path);
            try
            {
                HttpResponseMessage response = await Client.DeleteAsync(path);
                _lastStatusCode = response.StatusCode;
                response.EnsureSuccessStatusCode();
                responseContent = await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return new BadRequestObjectResult(e.Message);
            }

            return new OkObjectResult(responseContent);
        }

        public void Dispose()
        {
            Client.Dispose();
        }
    }
}
