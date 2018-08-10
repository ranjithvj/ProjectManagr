using Models;
using RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace Repositories
{
    public class EntityStatusRepository : IEntityStatusRepository
    {
        public void Delete(int id)
        {
            using (var context = new PmDbContext())
            {
                EntityStatus item = new EntityStatus { Id = id };
                context.EntityStatuses.Attach(item);
                context.EntityStatuses.Remove(item);
                context.SaveChanges();
            }
        }

        List<EntityStatus> IRepository<EntityStatus>.GetAll()
        {
            List<EntityStatus> entityStatuses;
            using (var context = new PmDbContext())
            {
                entityStatuses = context.EntityStatuses.ToList();
                return entityStatuses;
            }
        }

        EntityStatus IRepository<EntityStatus>.Get(int id)
        {
            EntityStatus returnValue;
            using (var context = new PmDbContext())
            {
                returnValue = context.EntityStatuses.FirstOrDefault(x => x.Id == id);
            }
            return returnValue;
        }

        public void Insert(EntityStatus item)
        {
            using (var context = new PmDbContext())
            {
                context.EntityStatuses.Add(item);
                context.SaveChanges();
            }
        }

        public void Update(EntityStatus item)
        {
            using (var context = new PmDbContext())
            {
                EntityStatus originalItem = context.EntityStatuses.FirstOrDefault(x => x.Id == item.Id);
                Helper.TransferData(item, originalItem);
                context.Entry(originalItem).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}