using RepositoryAndServicies.Repositories.Abstract;

namespace RepositoryAndServicies.Repositories
{
    record DataResponse<T>(T? Value, string? ErrorMessage = null) : IResponse<T>;
}