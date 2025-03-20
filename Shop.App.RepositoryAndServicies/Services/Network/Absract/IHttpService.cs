using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace RepositoryAndServicies.Services.Network.Absract
{
    public interface IHttpService:IDisposable
    {
        string? ErrorMessage { get; }
        JsonSerializerOptions JsonOptions { get; }

        Task<ActionResult<string>> CreateAsyncBy(string path, HttpContent content);
        Task<ActionResult<string>> DeleteAsyncBy(string path);
        Task<ActionResult<string>> GetAsyncFrom(string path);
        Task<ActionResult<string>> UpdateAsyncBy(string path, HttpContent content);
    }
}