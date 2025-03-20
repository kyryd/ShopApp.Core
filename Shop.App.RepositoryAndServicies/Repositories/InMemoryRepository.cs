using ShopApp.Core.Models.Models.Core.Abstract;
using RepositoryAndServicies.Repositories.Abstract;
using RepositoryAndServicies.Repositories.Metadata;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace RepositoryAndServicies.Repositories
{
    public sealed class InMemoryRepository<T> : IBackendRepository<T> where T : Entity
    {
        private readonly ConcurrentDictionary<int, T> _entities;

        public InMemoryRepository()
        {
            _entities = [];
        }

        public IResponse<T?> Add(T entity)
        {
            try
            {
                var res = GetNewId();
                if (res.IsError)
                {
                    return new DataResponse<T?>(null, res.ErrorMessage);
                }
                entity = entity with
                {
                    Id = res.Value
                };
                _entities[entity.Id!.Value] = entity;
                return new DataResponse<T?>(entity, res.ErrorMessage);
            }
            catch (Exception)
            {

                return new DataResponse<T?>(null, "Entity creation failure");
            }
        }

        public async Task<IResponse<IEnumerable<T>>> AddAllAsync(IEnumerable<T> entities)
        {
            var response = await Task.Run(async () =>
               {
                   foreach (T entity in entities)
                   {
                       var res = await AddAsync(entity);
                       if (res.IsError)
                       {
                           Debug.WriteLine($"Error:{nameof(InMemoryRepository<T>)}.{nameof(AddAllAsync)}:Entity creation failure");
                           Debug.WriteLine($"Error message:{res.ErrorMessage}");
                       }
                   }

                   return new DataResponse<IEnumerable<T>>(entities);
               });

            return response;
        }

        public async Task<IResponse<T?>> AddAsync(T entity)
        {
            return await Task.Run(() => Add(entity));
        }

        public IResponse<T?> Delete(int id)
        {
            try
            {
                var res = _entities.TryGetValue(id, out T? value);
                if (!res || value == null)
                {
                    return new DataResponse<T?>(null, $"{MessagesOFRepository.ENTITY_DELETION_FAILURE}");
                }
                _entities.TryRemove(new KeyValuePair<int, T>(id, value!));
                return new DataResponse<T?>(value);
            }
            catch (Exception e)
            {

                return new DataResponse<T?>(null, e.Message);
            }
        }

        public Task<IResponse<IEnumerable<T>>> DeleteAllAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<IResponse<T?>> DeleteAsync(int id)
        {
            return await Task.Run(() => Delete(id));
        }

        public IResponse<bool> Exists(T entity)
        {
            return new DataResponse<bool>(_entities.ContainsKey(entity.Id!.Value));
        }

        public IResponse<IEnumerable<T>> GetAll()
        {
            return new DataResponse<IEnumerable<T>>(_entities.Values);
        }

        public async Task<IResponse<IEnumerable<T>>> GetAllAsync()
        {
            return await Task.Run(GetAll);
        }

        public IResponse<int> GetNewId()
        {

            try
            {
                return new DataResponse<int>(!_entities.IsEmpty ? _entities.Keys.Max(key => key + 1) : 1);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{nameof(InMemoryRepository<T>)}.{nameof(GetNewId)}:{MessagesOFRepository.FAILED_TO_GET_NEW_ID}");
                Debug.WriteLine($"Error message:{e.Message}");
                return new DataResponse<int>(Value: -1, MessagesOFRepository.FAILED_TO_GET_NEW_ID);
            }
        }

        public IResponse<T?> Get(int id)
        {
            try
            {
                return new DataResponse<T?>(_entities[id]);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{nameof(InMemoryRepository<T>)}.{nameof(GetNewId)}:{MessagesOFRepository.FAILED_TO_GET_NEW_ID}");
                Debug.WriteLine($"Error message:{e.Message}");
                return new DataResponse<T?>(null, MessagesOFRepository.FAILED_TO_GET_VALUE_BY_ID);
            }
        }

        public IResponse<T?> Update(T entity)
        {
            try
            {
                _entities[entity.Id!.Value] = entity;
                return new DataResponse<T?>(_entities[entity.Id!.Value]);
            }
            catch (Exception)
            {

                return new DataResponse<T?>(null, MessagesOFRepository.ENTITY_UPDATE_FAILURE);
            }

        }

        public async Task<IResponse<T?>> GetAsync(int id)
        {
            return await Task.Run(() => Get(id));
        }

        public async Task<IResponse<T?>> UpdateAsync(T entity)
        {
            return await Task.Run(() => Update(entity));
        }

        public async Task<IResponse<bool>> IsEmpty()
        {
            return await Task.Run(() => new DataResponse<bool>(_entities.IsEmpty));
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

        public async Task<IResponse<IEnumerable<T>>> UpdateAllAsync(IEnumerable<T> entities)
        {
            try
            {
                await Parallel.ForEachAsync(entities, async (entity, cancellationToken) =>
                {
                    await UpdateAsync(entity);
                });
            }
            catch (Exception e)
            {

                Debug.WriteLine($"Error:{nameof(EfRepository<T>)}.{nameof(UpdateAllAsync)}:{MessagesOFRepository.ENTITY_UPDATE_FAILURE}");
                Debug.WriteLine($"Error message:{e.Message}");
                return new DataResponse<IEnumerable<T>>(null, MessagesOFRepository.ENTITY_UPDATE_FAILURE);
            }

            return new DataResponse<IEnumerable<T>>(entities);
        }

        public async Task<IResponse<IEnumerable<T>>> SaveAllAsync(IEnumerable<T> entities)
        {
            try
            {
                await UpdateAllAsync(entities.Where(entity => entity.Id >= 1));
            }
            catch (Exception)
            {
                return new DataResponse<IEnumerable<T>>(null, MessagesOFRepository.FAILED_TO_UPDATE_ALL);
            }

            try
            {
                await AddAllAsync(entities.Where(entity => entity.Id == null || entity.Id < 1));
            }
            catch (Exception)
            {
                return new DataResponse<IEnumerable<T>>(null, MessagesOFRepository.FAILED_TO_ADD_ALL);
            }

            return new DataResponse<IEnumerable<T>>(entities);
        }

        public void Dispose()
        {
            _entities.Clear();
        }
    }
}
