using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class BaseRepository
    {
        public void Insert<T>(T item, int id, PmDbContext context) where T : class
        {

            context.SaveChanges();
            //TODO: update cache
        }
    }
}
