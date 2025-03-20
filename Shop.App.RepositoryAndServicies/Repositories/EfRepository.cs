using Microsoft.EntityFrameworkCore;
using ShopApp.Core.Models.Models.Core.Abstract;
using RepositoryAndServicies.Repositories.Abstract;
using RepositoryAndServicies.Services.Database;
using System.Collections;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RepositoryAndServicies.Repositories
{
    public sealed class EfRepository<T> : IBackendRepository<T> where T : Entity
    {
        public IContextProvider<T> ContextProvider {get; }
        

        
        private readonly bool? _hasIdentity;
        private bool? HasIdentity()
        {
            var tableData = ContextProvider.Context.Model.FindEntityType(typeof(T));
            if (tableData == null)
            {
                return null;
            }
            var hasIdentity = tableData.GetKeys().Any(x => x.Properties.Any(y => y.ValueGenerated == Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.OnAdd));
            return hasIdentity;
        }
        public EfRepository(ContextProvider<T> contextProvider)
        {
            ContextProvider = contextProvider;


            contextProvider.Context.Database.EnsureCreated();
            contextProvider.Context.Database.OpenConnection();
            contextProvider.Context.Database.CloseConnection();
            _hasIdentity = HasIdentity();
        }

        private T SetKey(T entity) => entity with { Id = ContextProvider.DbSet.Max(e => e.Id) + 1 };
        private IEnumerable<T> ContitionalSetKey(IEnumerable<T> entities) 
        {
            foreach (T entity in entities)
            {
                yield return entity switch
                {
                    { Id: null } => entity,
                    { Id: < 1 } => SetKey(entity),
                    _ => entity
                };
            }
        }

        private T ContitionalKey(T entity) => entity switch
        {
            _ when !_hasIdentity!.Value && entity.Id == null => SetKey(entity),
            { Id: null } => entity,
            { Id: < 1 } => SetKey(entity),
            _ => entity
        };

        private IEnumerable<T> ContitionalKeys(IEnumerable<T> entities) {
            if (_hasIdentity!.Value)
            {
                return entities;
            }
            return ContitionalSetKey(entities);
        }
        

        public IResponse<T?> Add(T entity)
        {
            try
            {
                ContextProvider.InitContext();
                ContextProvider.DbSet.Add(ContitionalKey(entity));
                ContextProvider.Context.SaveChanges();
                return new DataResponse<T?>(entity);
            }
            catch (Exception ex)
            {
                return new DataResponse<T?>(default, ex.Message);
            }
        }


        public async Task<IResponse<IEnumerable<T>>> AddAllAsync(IEnumerable<T> entities)
        {
            try
            {
                ContextProvider.InitContext();
                await ContextProvider.DbSet.AddRangeAsync(ContitionalKeys(entities));
                await ContextProvider.Context.SaveChangesAsync();
                return new DataResponse<IEnumerable<T>>(entities);
            }
            catch (Exception ex)
            {
                return new DataResponse<IEnumerable<T>>(default, ex.Message);
            }
        }

        public async Task<IResponse<T?>> AddAsync(T entity)
        {
            try
            {
                ContextProvider.InitContext();
                await ContextProvider.DbSet.AddAsync(ContitionalKey(entity));
                await ContextProvider.Context.SaveChangesAsync();
                return new DataResponse<T?>(entity);
            }
            catch (Exception ex)
            {
                return new DataResponse<T?>(default, ex.Message);
            }
        }

        public IResponse<T?> Delete(int id)
        {
            try
            {
                ContextProvider.InitContext();
                var entity = ContextProvider.DbSet.Find(id);
                if (entity == null)
                {
                    return new DataResponse<T?>(default, "Entity not found");
                }
                ContextProvider.DbSet.Remove(entity);
                ContextProvider.Context.SaveChanges();
                return new DataResponse<T?>(entity);
            }
            catch (Exception ex)
            {
                return new DataResponse<T?>(default, ex.Message);
            }
        }

        public async Task<IResponse<IEnumerable<T>>> DeleteAllAsync(IEnumerable<T> entities)
        {
            try
            {
                ContextProvider.InitContext();
                ContextProvider.DbSet.RemoveRange(entities);
                await ContextProvider.Context.SaveChangesAsync();
                return new DataResponse<IEnumerable<T>>(entities);
            }
            catch (Exception ex)
            {
                return new DataResponse<IEnumerable<T>>(default, ex.Message);
            }
        }

        public async Task<IResponse<T?>> DeleteAsync(int id)
        {
            try
            {
                ContextProvider.InitContext();
                var entity = await ContextProvider.DbSet.FindAsync(id);
                if (entity == null)
                {
                    return new DataResponse<T?>(default, "Entity not found");
                }
                ContextProvider.DbSet.Remove(entity);
                await ContextProvider.Context.SaveChangesAsync();
                return new DataResponse<T?>(entity);
            }
            catch (Exception ex)
            {
                return new DataResponse<T?>(default, ex.Message);
            }
        }

        public IResponse<bool> Exists(T entity)
        {
            try
            {
                ContextProvider.InitContext();
                var exists = ContextProvider.DbSet.Contains(entity);
                return new DataResponse<bool>(exists);
            }
            catch (Exception ex)
            {
                return new DataResponse<bool>(false, ex.Message);
            }
        }

        public async Task<IResponse<IEnumerable<T>>> GetAllAsync()
        {
            try
            {
                ContextProvider.InitContext();
                var entities = await ContextProvider.DbSet.ToListAsync();
                return new DataResponse<IEnumerable<T>>(entities);
            }
            catch (Exception ex)
            {
                return new DataResponse<IEnumerable<T>>(default, ex.Message);
            }
        }

        public IResponse<IEnumerable<T>> GetAll()
        {
            try
            {
                ContextProvider.InitContext();
                var entities = ContextProvider.DbSet.ToList();
                return new DataResponse<IEnumerable<T>>(entities);
            }
            catch (Exception ex)
            {
                return new DataResponse<IEnumerable<T>>(default, ex.Message);
            }
        }

        public IResponse<int> GetNewId()
        {
            try
            {
                var newId = ContextProvider.DbSet.Any() ? ContextProvider.DbSet.Max(e => e.Id) + 1 : 1;
                return new DataResponse<int>(newId!.Value);
            }
            catch (Exception ex)
            {
                return new DataResponse<int>(0, ex.Message);
            }
        }

        public IResponse<T?> Get(int id)
        {
            try
            {
                ContextProvider.InitContext();
                var entity = ContextProvider.DbSet.Find(id);
                return new DataResponse<T?>(entity);
            }
            catch (Exception ex)
            {
                return new DataResponse<T?>(default, ex.Message);
            }
        }

        public async Task<IResponse<T?>> GetAsync(int id)
        {
            try
            {
                ContextProvider.InitContext();
                var entity = await ContextProvider.DbSet.FindAsync(id);
                return new DataResponse<T?>(entity);
            }
            catch (Exception ex)
            {
                return new DataResponse<T?>(default, ex.Message);
            }
        }

        public async Task<IResponse<bool>> IsEmpty()
        {
            try
            {
                ContextProvider.InitContext();
                var isEmpty = !await ContextProvider.DbSet.AnyAsync();
                return new DataResponse<bool>(isEmpty);
            }
            catch (Exception ex)
            {
                return new DataResponse<bool>(false, ex.Message);
            }
        }

        public IResponse<T?> Save(T entity)
        {
            return Task.Run(() => SaveAsync(entity)).Result;
        }

        public async Task<IResponse<IEnumerable<T>>> SaveAllAsync(IEnumerable<T> entities)
        {
            try
            {
                var toUpdate = entities.Where(e => e.Id >= 1);
                if (toUpdate.Any()) 
                {
                    await UpdateAllAsync(toUpdate);
                }
                
            }
            catch (Exception)
            {
                return new DataResponse<IEnumerable<T>>(null, "Saving failure.");
            }
            try
            {
                var toAdd = entities.Where(item => item.Id == null || item.Id < 1);

                if (toAdd.Any())
                {
                    await AddAllAsync(toAdd);
                }
            }
            catch (Exception)
            {
                return new DataResponse<IEnumerable<T>>(null, "Saving failure");
            }

            return new DataResponse<IEnumerable<T>>(entities);
        }

        public async Task<IResponse<T?>> SaveAsync(T entity)
        {
            if (entity.Id == null || entity.Id < 1)
            {
                return await AddAsync(entity);
            }
            return await UpdateAsync(entity);
        }

        public IResponse<T?> Update(T entity)
        {
            try
            {
                ContextProvider.InitContext();
                ContextProvider.DbSet.Update(entity);
                ContextProvider.Context.SaveChanges();
                return new DataResponse<T?>(entity);
            }
            catch (Exception ex)
            {
                return new DataResponse<T?>(default, ex.Message);
            }
        }

        public async Task<IResponse<IEnumerable<T>>> UpdateAllAsync(IEnumerable<T> entities)
        {
            try
            {
                ContextProvider.InitContext();
                ContextProvider.DbSet.UpdateRange(entities);
                await ContextProvider.Context.SaveChangesAsync();
            }
            catch (Exception e)
            {

                Debug.WriteLine($"Error:{nameof(EfRepository<T>)}.{nameof(UpdateAllAsync)}:Entity update failure");
                Debug.WriteLine($"Error message:{e.Message}");
                return new DataResponse<IEnumerable<T>>(null, e.Message);
            }

            return new DataResponse<IEnumerable<T>>(entities);
        }

        public async Task<IResponse<T?>> UpdateAsync(T entity)
        {
            try
            {
                ContextProvider.InitContext();
                ContextProvider.DbSet.Update(entity);
                await ContextProvider.Context.SaveChangesAsync();
                return new DataResponse<T?>(entity);
            }
            catch (Exception ex)
            {
                return new DataResponse<T?>(default, ex.Message);
            }
        }

        public void Dispose()
        {
            ContextProvider.Dispose();
        }
    }
}
