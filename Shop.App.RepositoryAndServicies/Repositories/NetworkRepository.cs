using Microsoft.AspNetCore.Mvc;
using ShopApp.Core.Models.Models.Core.Abstract;
using RepositoryAndServicies.Repositories.Abstract;
using RepositoryAndServicies.Repositories.Metadata;
using RepositoryAndServicies.Services.Network;
using RepositoryAndServicies.Services.Network.Absract;
using System.Diagnostics;

namespace RepositoryAndServicies.Repositories
{
    public class NetworkRepository<T> : IFrontendRepository<T> where T : Entity
    {
        private readonly ICrudApi<T> _api;
        public NetworkRepository(string serverBaseAddress) => _api = new CrudApi<T>(serverBaseAddress);
        public NetworkRepository(ICrudApi<T> api) => _api = api;

        public IResponse<T?> Add(T entity)
        {
            try
            {
                var response = Task.Run(async () => await AddAsync(entity)).Result;
                return response;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{nameof(NetworkRepository<T>)}.{nameof(Add)}:{MessagesOFRepository.ENTITY_CREATION_FAILURE}");
                Debug.WriteLine($"Error message:{e.Message}");
                return new DataResponse<T?>(null, MessagesOFRepository.INETRNAL_ERROR);
            }
        }

        public IResponse<T?> Get(int id)
        {
            return Task.Run(async () => await GetAsync(id)).Result;
        }

        public IResponse<IEnumerable<T>> GetAll()
        {
            return Task.Run(async () => await GetAllAsync()).Result;
        }

        public IResponse<T?> Update(T entity)
        {
            return Task.Run(async () => await UpdateAsync(entity)).Result;
        }

        public IResponse<T?> Delete(int id)
        {
            return Task.Run(async () => await DeleteAsync(id)).Result;
        }

        public async Task<IResponse<T?>> AddAsync(T entity)
        {
            try
            {
                var response = await _api.Create(entity);
                Debug.Assert(response != null);
                if (response.Result is BadRequestResult)
                {
                    Debug.WriteLine($"Error:{nameof(NetworkRepository<T>)}.{nameof(AddAllAsync)}: {_api.HttpService.ErrorMessage}");
                    return new DataResponse<T?>(null, _api.HttpService.ErrorMessage);
                }
                return new DataResponse<T?>(entity);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return new DataResponse<T?>(null, MessagesOFRepository.INETRNAL_ERROR);
            }
        }

        public async Task<IResponse<T?>> GetAsync(int id)
        {
            try
            {
                var response = await _api.GetById(id);
                if (response.Result is OkObjectResult resultOk)
                {
                    return new DataResponse<T?>((T?)resultOk?.Value);
                }
                else
                {
                    Debug.WriteLine($"Error:{nameof(NetworkRepository<T>)}.{nameof(GetAsync)}:{MessagesOFRepository.FAILED_TO_GET_VALUE_BY_ID}");
                    Debug.WriteLine($"Error message:{((response.Result as BadRequestObjectResult)?.Value as String)}");
                    return new DataResponse<T?>(null, _api.HttpService.ErrorMessage);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{nameof(NetworkRepository<T>)}.{nameof(GetAsync)}:{MessagesOFRepository.FAILED_TO_GET_VALUE_BY_ID}");
                Debug.WriteLine($"Error message:{e.Message}");
                return new DataResponse<T?>(null, MessagesOFRepository.FAILED_TO_GET_VALUE_BY_ID);
            }
        }

        public async Task<IResponse<IEnumerable<T>>> GetAllAsync()
        {
            try
            {
                var response = await _api.GetRange();
                Debug.Assert(response != null);
                if (response.Result is OkObjectResult resultOk)
                {
                    Debug.Assert(resultOk.Value != null);
                    return new DataResponse<IEnumerable<T>>((IEnumerable<T>?)resultOk?.Value ?? Array.Empty<T>());
                }
                else
                {
                    Debug.WriteLine($"Error:{nameof(NetworkRepository<T>)}.{nameof(GetAllAsync)}:{MessagesOFRepository.FAILED_TO_GET_ALL}");
                    Debug.WriteLine($"Error message:{((response.Result as BadRequestObjectResult)?.Value as String)}");
                    return new DataResponse<IEnumerable<T>>([], _api.HttpService.ErrorMessage);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{nameof(NetworkRepository<T>)}.{nameof(GetAllAsync)}:{MessagesOFRepository.FAILED_TO_GET_ALL}");
                Debug.WriteLine($"Error message:{e.Message}");
                return new DataResponse<IEnumerable<T>>([], MessagesOFRepository.FAILED_TO_GET_ALL);
            }
        }

        public async Task<IResponse<T?>> UpdateAsync(T entity)
        {
            try
            {
                var response = await _api.Update(entity);
                Debug.Assert(response != null);
                if (response.Result is OkObjectResult)
                {
                    Debug.WriteLine($"{nameof(NetworkRepository<T>)}.{nameof(UpdateAsync)}:{response}");
                    return new DataResponse<T?>(entity);
                }
                else
                {
                    Debug.WriteLine($"Error:{nameof(NetworkRepository<T>)}.{nameof(UpdateAsync)}:{MessagesOFRepository.ENTITY_UPDATE_FAILURE}");
                    Debug.WriteLine($"Error message:{((response.Result as BadRequestObjectResult)?.Value as String)}");
                    return new DataResponse<T?>(null);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{nameof(NetworkRepository<T>)}.{nameof(UpdateAsync)}:{MessagesOFRepository.ENTITY_UPDATE_FAILURE}");
                Debug.WriteLine($"Error message:{e.Message}");
                return new DataResponse<T?>(null);
            }
        }

        public async Task<IResponse<T?>> DeleteAsync(int id)
        {
            try
            {
                var response = await _api.DeleteItem(id);
                Debug.Assert(response != null);
                if (response.Result is OkObjectResult)
                {
                    return new DataResponse<T?>(null);
                }
                else
                {
                    Debug.WriteLine($"Error:{nameof(NetworkRepository<T>)}.{nameof(DeleteAsync)}:{MessagesOFRepository.ENTITY_DELETION_FAILURE}");
                    Debug.WriteLine($"Error message:{((response.Result as BadRequestObjectResult)?.Value as String)}");
                    return new DataResponse<T?>(null);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{nameof(NetworkRepository<T>)}.{nameof(DeleteAsync)}:{MessagesOFRepository.ENTITY_DELETION_FAILURE}");
                Debug.WriteLine($"Error message:{e.Message}");
                return new DataResponse<T?>(null);
            }
        }

        public async Task<IResponse<bool>> IsEmpty()
        {
            var res = await GetAllAsync();
            if (res.IsError)
            {
                return new DataResponse<bool>(true, res.ErrorMessage);
            }
            return new DataResponse<bool>(!res.Value!.Any());
        }

        public async Task<IResponse<IEnumerable<T>>> AddAllAsync(IEnumerable<T> entities)
        {
            try
            {
                var addedEntities = new List<T>();
                foreach (var entity in entities)
                {
                    var response = await _api.Create(entity);
                    if (response.Result is BadRequestResult)
                    {
                        Debug.WriteLine($"Error:{nameof(NetworkRepository<T>)}.{nameof(AddAllAsync)}: {MessagesOFRepository.FAILED_TO_ADD_ALL}");
                        return new DataResponse<IEnumerable<T>>(Array.Empty<T>());
                    }
                    addedEntities.Add(entity);
                }
                return new DataResponse<IEnumerable<T>>(addedEntities);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return new DataResponse<IEnumerable<T>>([], MessagesOFRepository.FAILED_TO_DELETE_ALL);
            }
        }

        public async Task<IResponse<IEnumerable<T>>> DeleteAllAsync(IEnumerable<T> entities)
        {
            try
            {
                var deletedEntities = new List<T>();
                foreach (var entity in entities)
                {
                    if(entity.Id == null)
                    {
                        Debug.WriteLine($"Error:{nameof(NetworkRepository<T>)}.{nameof(DeleteAllAsync)}: {MessagesOFRepository.FAILED_TO_DELETE_ALL}");
                        return new DataResponse<IEnumerable<T>>([]);
                    }
                    var response = await _api.DeleteItem(entity.Id!.Value);

                    if (response.Result is BadRequestResult)
                    {
                        Debug.WriteLine($"Error:{nameof(NetworkRepository<T>)}.{nameof(DeleteAllAsync)}: {MessagesOFRepository.FAILED_TO_DELETE_ALL}");
                        return new DataResponse<IEnumerable<T>>([]);
                    }
                    deletedEntities.Add(entity);
                }
                return new DataResponse<IEnumerable<T>>(deletedEntities);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return new DataResponse<IEnumerable<T>>([], MessagesOFRepository.FAILED_TO_DELETE_ALL);
            }
        }

        public IResponse<bool> Exists(T entity)
        {
            if (entity.Id == null)
            {
                return new DataResponse<bool>(false, MessagesOFRepository.ENTITY_DOES_NOT_EXIST);
            }
            var res = Get(entity.Id!.Value);
            if (res.IsError)
            {
                return new DataResponse<bool>(false, res.ErrorMessage);
            }
            var existingEntity = res.Value;
            return new DataResponse<bool>(existingEntity != null);
        }

        public IResponse<T?> Save(T entity)
        {
            return Task.Run(() => SaveAsync(entity)).Result;
        }

        public async Task<IResponse<T?>> SaveAsync(T entity)
        {
            if (entity.Id == null || entity.Id < 1)
            {
                return await AddAsync(entity);
            }
            return await UpdateAsync(entity);
        }

        public async Task<IResponse<IEnumerable<T>>> SaveAllAsync(IEnumerable<T> entities)
        {
            try
            {
               var res= await  _api.SaveRange(entities);
            }
            catch (Exception)
            {
                return new DataResponse<IEnumerable<T>>(null, MessagesOFRepository.SAVING_FAILURE);
            }

            return new DataResponse<IEnumerable<T>>(entities);
        }

        public void Dispose()
        {
            _api.Dispose();
        }
    }
}
