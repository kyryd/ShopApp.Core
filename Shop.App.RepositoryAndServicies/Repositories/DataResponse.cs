using RepositoryAndServicies.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryAndServicies.Repositories
{
    record DataResponse<T>(T? Value, string? ErrorMessage = null) : IResponse<T>;
}