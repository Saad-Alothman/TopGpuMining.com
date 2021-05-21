using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using CreaDev.Framework.Core.Exceptions;
using CreaDev.Framework.Core.Extensions;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CreaDev.Framework.Core.Models
{
    public class ServiceBase<TModel,TService,TDbContext, TUser> : SingletonBase<TService>, IServiceBase<TModel> 
        where TDbContext:DbContext, new()
        where TModel: EntityBase
        where TService: class, new()
        where TUser : IdentityUser, new()
    {
    
        protected List<string> DeleteIncludes { get; set; }
        protected List<string> GetIncludes { get; set; }
        protected List<string> UpdateIncludes { get; set; }
        protected List<string> SearchIncludes { get; set; }
        protected List<string> Includes { get; set; }

        protected internal GenericRepository<TDbContext,TUser> Repository;
        protected LocalizableText ModelNameSingular { get; set; }
        private LocalizableText ModelNamePlural { get; set; }
        protected ServiceBase()
        {
            this.Repository = new GenericRepository<TDbContext,TUser>();
            this.Includes = new List<string>();
            this.SearchIncludes = new List<string>();
            this.GetIncludes = new List<string>();
            this.DeleteIncludes = new List<string>();
            this.UpdateIncludes = new List<string>();
            this.ModelNameSingular = new LocalizableText(typeof(TModel).Name, typeof(TModel).Name);
            this.ModelNamePlural = new LocalizableText(typeof(TModel).Name, typeof(TModel).Name);
        }

        public List<string> GetIncludesList()
        {
            return Includes;
        }
        static ServiceBase()
        {
            instance = new TService();
        }
        public virtual void Add(TModel model)
        {
            model.ValidateAdd();
            Repository.Create(model);
        }

        public virtual void Add(List<TModel> models)
        {
            models.ForEach(m=>m.ValidateAdd());
            Repository.Create(models);
        }

        public virtual TModel GetById(int id)
        {
            Guard.AgainstFalse<ArgumentException>(id > 0, ModelNameSingular?.ToString());

            var model = Search(new SearchCriteria<TModel>() {FilterExpression = a => a.Id == id}).Result.FirstOrDefault();
            
            Guard.AgainstNull<NotFoundException>(model, ModelNameSingular?.ToString());
            Guard.AgainstFalse(model.IsUserCanView());

            return model;
        }
        public virtual SearchResult<TModel> Search(SearchCriteria<TModel> searchCriteria)
        {
            //searchCriteria.ApplyFilterBasedOnPermission();
            return Repository.Search(searchCriteria, Includes.ToArray());
        }
        public virtual SearchResult<TModel> Search(SearchCriteria<TModel> searchCriteria, CrudOperationType crudOperationType)
        {
            //searchCriteria.ApplyFilterBasedOnPermission();
            return Repository.Search(searchCriteria, Includes.ToArray());
        }
        public virtual void Delete(int id)
        {
            using (UnitOfWork<TDbContext, TUser> unitOfWork = new UnitOfWork<TDbContext, TUser>())
            {
                var forum = unitOfWork.GenericRepository.Get<TModel>(f => f.Id == id, includeProperties: Includes).FirstOrDefault();

                Guard.AgainstNull<NotFoundException>(forum, ModelNameSingular.ToString());
                // ReSharper disable once PossibleNullReferenceException
                Guard.AgainstFalse<PermissionException>(forum.IsUserCanModify(CrudOperationType.Delete), ModelNameSingular.ToString());
                
                unitOfWork.GenericRepository.Delete(forum);
                unitOfWork.Commit();
            }
        }

        public virtual void Update(TModel model)
        {
            using (UnitOfWork<TDbContext, TUser> unitOfWork = new UnitOfWork<TDbContext, TUser>())
            {
                var dbmodel = unitOfWork.GenericRepository.Get<TModel>(f => f.Id == model.Id, includeProperties: Includes).FirstOrDefault();
                Guard.AgainstNull<NotFoundException>(dbmodel, ModelNameSingular.ToString());
                // ReSharper disable once PossibleNullReferenceException
                Guard.AgainstFalse<PermissionException>(dbmodel.IsUserCanModify(), ModelNameSingular.ToString());

                dbmodel.Update(model);
                unitOfWork.Commit();
            }
        }

        public virtual List<TModel> GetAll()
        {
            
            return Search(new SearchCriteria<TModel>(int.MaxValue,1)).Result;
        }
        public virtual List<TModel> GetAllForCommand()
        {
            return SearchForCommand(new SearchCriteria<TModel>(int.MaxValue, 1)).Result;
        }
        public virtual SearchResult<TModel> SearchForCommand(SearchCriteria<TModel> searchCriteria)
        {
            return Search(new SearchCriteria<TModel>(int.MaxValue, 1),CrudOperationType.IncludeInAnotherCommand);
        }
    }



}
