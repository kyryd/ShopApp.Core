using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryAndServicies.Repositories.Abstract
{
    public interface IResponse<T>
    {
        T? Value { get; }
        String? ErrorMessage { get; }
        virtual bool IsError => ErrorMessage != null;
    }
}
