using Microsoft.AspNetCore.Mvc;
using RepositoryAndServicies.Services.Network.Absract;
using ShopApp.Core.Models.Models.Core.Abstract;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace RepositoryAndServicies.Services.Network
{
    public enum CrudApiValues
    {
        Create,
        GetById,
        GetAll,
        Update,
        GetNewId,
        Delete,
        SaveRange
    }

    public static class CrudApiEndpointsExtensiion
    {
        private static readonly string CrudApiNetService = "CrudApi2";
        public static string Path(this CrudApiValues endpoint)
        => $"{CrudApiNetService}/{endpoint switch
        {
            CrudApiValues.Create => "Create",
            CrudApiValues.GetById => "GetById",
            CrudApiValues.GetAll => "GetAll",
            CrudApiValues.Update => "Update",
            CrudApiValues.GetNewId => "GetNewId",
            CrudApiValues.Delete => "Delete",
            CrudApiValues.SaveRange => "SaveRange",
            _ => throw new Exception("Invalid endpoint")
        }}";


    }

    public sealed class CrudApi<T>(string serverBaseAddress) : ICrudApi<T> where T : class, IEntity
    {
        private readonly IHttpService _httpService = new HttpService(serverBaseAddress);
        public IHttpService HttpService => _httpService;

        public async Task<ActionResult<string>> SaveRange(IEnumerable<T> item)
        {
            var content = new StringContent(JsonSerializer.Serialize(item), Encoding.UTF8, "application/json");
            var response = await HttpService.CreateAsyncBy(CrudApiValues.SaveRange.Path(), content);
            return response;

        }
        public async Task<ActionResult<string>> Create(T item)
        {
            var content = new StringContent(JsonSerializer.Serialize(item), Encoding.UTF8, "application/json");
            var response = await HttpService.CreateAsyncBy(CrudApiValues.Create.Path(), content);
            return response;

        }

        public async Task<ActionResult<string>> Update(T item)
        {
            var content = new StringContent(JsonSerializer.Serialize(item), Encoding.UTF8, "application/json");
            var response = await HttpService.UpdateAsyncBy(CrudApiValues.Update.Path(), content);
            return response;
        }
        public async Task<ActionResult<T?>> GetById(int id)
        {
            var response = await HttpService.GetAsyncFrom($"{CrudApiValues.GetById.Path()}/{id}");

            Debug.Assert(response != null);

            if (response.Result is OkObjectResult)
            {
                var item = JsonSerializer.Deserialize<T>(response.Value, HttpService.JsonOptions);
                Debug.Assert(item != null);
                return new OkObjectResult(item);
            }

            return new BadRequestObjectResult(default);
        }

        public async Task<ActionResult<IEnumerable<T>>> GetRange()
        {
            var response = await HttpService.GetAsyncFrom(CrudApiValues.GetAll.Path());

            Debug.Assert(response != null);

            if (response.Result is OkObjectResult resultOk)
            {
                string jsonString = (resultOk?.Value as string) ?? string.Empty;

                Debug.Assert(jsonString != string.Empty);

                var items = JsonSerializer.Deserialize<IEnumerable<T>>(jsonString, HttpService.JsonOptions) ?? [];
                Debug.Assert(items != null);
                return new OkObjectResult(items);
            }


            return new BadRequestObjectResult([]);
        }

        public async Task<ActionResult<int>> GetNewId()
        {
            var response = await HttpService.GetAsyncFrom(CrudApiValues.GetNewId.Path());
            Debug.Assert(response != null);
            if (response.Result is OkObjectResult resultOk)
            {
                string jsonString = (resultOk?.Value as string) ?? string.Empty;

                Debug.Assert(jsonString != string.Empty);

                var id = JsonSerializer.Deserialize<int>(jsonString, HttpService.JsonOptions);
                Debug.Assert(id > 0);
                return new OkObjectResult(id);
            }
            return new BadRequestObjectResult(default);
        }

        public async Task<ActionResult<int?>> DeleteItem(int id)
        {
            var response = await HttpService.DeleteAsyncBy($"{CrudApiValues.Delete.Path()}/{id}");
            Debug.Assert(response != null);

            if (response.Result is OkObjectResult)
            {
                return new OkObjectResult(id);
            }

            return new BadRequestObjectResult(default);
        }

        public void Dispose()
        {
            HttpService.Dispose();
        }
    }


}
