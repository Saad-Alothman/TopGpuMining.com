using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreaDev.Framework.Core.Models;

namespace CreaDev.Framework.Core.Utils
{
    public class DatabaseEntityCollectionUpdateResult<TEntity>
    {
        public List<TEntity> EntitesToUpdate;
        public List<TEntity> EntitesToRemove;
        public List<TEntity> NewEntities;

        public DatabaseEntityCollectionUpdateResult(List<TEntity> entitesToUpdate, List<TEntity> entitesToRemove, List<TEntity> newEntities)
        {
            this.EntitesToUpdate = entitesToUpdate;
            this.EntitesToRemove = entitesToRemove;
            this.NewEntities = newEntities;
        }
    }
    public static class DatabaseEntityCollectionUpdateHelper
    {
        public static DatabaseEntityCollectionUpdateResult<TEntity> GetUpdateCollectionResult<TEntity>(this List<TEntity> theDbList, List<TEntity> theNewList) where  TEntity:EntityBase
        {
            var existingEntitesFromNewList = theNewList.Where(e => e.Id > 0);

            var entitesToRemove =theDbList.Where(e => existingEntitesFromNewList.FirstOrDefault(nl => nl.Id == e.Id) == null).ToList();
            var entitesToUpdate = theDbList.Where(e => existingEntitesFromNewList.FirstOrDefault(nl => nl.Id == e.Id) != null).ToList();
            var newEntities = theNewList.Where(e => e.Id < 1).ToList();
            return new DatabaseEntityCollectionUpdateResult<TEntity>(entitesToUpdate,entitesToRemove,newEntities);
        }

        public static void UpdateCollection<TEntity>(this List<TEntity> theDbList, List<TEntity> theNewList)
            where TEntity : EntityBase
        {
            DatabaseEntityCollectionUpdateResult<TEntity> databaseEntityCollectionUpdateResult=theDbList.GetUpdateCollectionResult(theNewList);
            databaseEntityCollectionUpdateResult.EntitesToRemove.ForEach(er=> theDbList.Remove(er));
            //Remove
            theDbList.RemoveAll(entity =>databaseEntityCollectionUpdateResult.EntitesToRemove.Select(s => s.Id).Contains(entity.Id));
            //Add the new Ones
            theDbList.AddRange(databaseEntityCollectionUpdateResult.NewEntities);
            //Update Existing
            foreach (var entityToUpdate in theDbList.Where(e=>databaseEntityCollectionUpdateResult.EntitesToRemove.Select(s => s.Id).Contains(e.Id)))
            {
                var model = theNewList.FirstOrDefault(e => e.Id == entityToUpdate.Id);
                Guard.AgainstNull(model);
                entityToUpdate.Update(model);
            }

        }
    }
}

