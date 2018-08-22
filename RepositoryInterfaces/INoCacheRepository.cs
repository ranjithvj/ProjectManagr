using Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RepositoryInterfaces
{
    public interface INoCacheRepository<T>
    {
        List<T> GetWithFilter(Expression<Func<T, bool>> request);
    }
}